using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using log4net;
using System.Threading;
using ActoresWS;
using AuxiliaresWS;


public partial class Paginas_InformacionCompletaPeticion : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(typeof(Paginas_InformacionCompletaPeticion).Name);

    #region Sesiones

    private List<ActoresWS.TipoDocumentacion_Prestacion> sesDocFaltante
    {
        get { return (List<ActoresWS.TipoDocumentacion_Prestacion>)ViewState["_dtDocFaltante"]; }
        set { ViewState["_dtDocFaltante"] = value; }
    }

    private List<ActoresWS.IngDevMov> sesMovimientos
    {
        get { return (List<ActoresWS.IngDevMov>)ViewState["_dtMov"]; }
        set { ViewState["_dtMov"] = value; }
    }

    private List<ActoresWS.Expediente_Solicitud> sesExpedientes
    {
        get { return (List<ActoresWS.Expediente_Solicitud>)ViewState["_dtExpe"]; }
        set { ViewState["_dtExpe"] = value; }
    }

    private List<ActoresWS.Beneficio_Solicitud> sesBeneficios
    {
        get { return (List<ActoresWS.Beneficio_Solicitud>)ViewState["_dtBeneficio"]; }
        set { ViewState["_dtBeneficio"] = value; }
    }

    private List<ActoresWS.Solicitud> sesSolicitudes
    {
        get { return (List<ActoresWS.Solicitud>)ViewState["_dtSolicitud"]; }
        set { ViewState["_dtSolicitud"] = value; }
    }

    private List<ActoresWS.Ingresos> sesIngresos
    {
        get { return (List<ActoresWS.Ingresos>)ViewState["_dtIng"]; }
        set { ViewState["_dtIng"] = value; }
    }


    private ConsultasWS.Movimiento_Solicitud sesUltMovimiento
    {
        get { return (ConsultasWS.Movimiento_Solicitud)ViewState["_dtultMov"]; }
        set { ViewState["_dtultMov"] = value; }
    }

    private Int16 codPrestacion
    {
        get { return (Int16)ViewState["_codPrestacion"]; }
        set { ViewState["_codPrestacion"] = value; }
    }

    private String descPrestacion
    {
        get { return (String)ViewState["_dPrestacion"]; }
        set { ViewState["_dPrestacion"] = value; }
    }

    private String descPais
    {
        get { return (String)ViewState["_dPais"]; }
        set { ViewState["_dPais"] = value; }
    }

    private String ApeNomBenef
    {
        get { return (String)ViewState["_apenom"]; }
        set { ViewState["_apenom"] = value; }
    }

    private Int64? idBeneficiario
    {
        get { return (Int64?)ViewState["_idBen"]; }
        set { ViewState["_idBen"] = value; }
    }

    #endregion Sesiones

    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }

    #region APlica Seguridad Pagina
    private bool AplicarSeguridad()
    {
        bool permiso = false;
        try
        {
            permiso = TienePermiso("accesoPagina");

        }
        catch (ThreadAbortException)
        { }
        return permiso;
    }
    #endregion APlica Seguridad Pagina


    protected void Page_Load(object sender, EventArgs e)
    {
        #region Set validacion fechas
        #endregion Set validacion fechas

        if (!IsPostBack)
        {
            //Check de Seguridad tanto para ingresar como modificar instrumento
            if (!AplicarSeguridad())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegadoNBtn"].ToString());

            MError.MensajeError = string.Empty;

            idBeneficiario = Int64.Parse(Request.QueryString["idBeneficiario"].ToString());
            ApeNomBenef = (string)Request.QueryString["descApeNom"];
            lbEncabezadoBeneficiario.Text = ApeNomBenef;
            codPrestacion = Int16.Parse(Request.QueryString["codPrestacion"].ToString());
            descPrestacion = Request.QueryString["descPrestacion"].ToString();
            lbdescPrestacion.Text = " " + descPrestacion;
            descPais = Request.QueryString["descPais"].ToString();
            lbDescPais.Text = descPais;

            
            
            String mensajeer = "";

            #region obtiene ultimo estado y sector
             sesUltMovimiento = InvocaWsDao.TraeUltimoMovimientoSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);
             if (sesUltMovimiento != null && mensajeer == "")
             {
                 lbUltMovEstado.Text = sesUltMovimiento.Estado.Descripcion;
                 lbUltMovSector.Text = sesUltMovimiento.Sector.Descripcion;
                 lbUltMovObserv.Text = sesUltMovimiento.Observaciones;
                 lbFechaUltMov.Text = sesUltMovimiento.Fecha_Movimiento.ToShortDateString();
             }
             else
             {
                 MError.MensajeError = mensajeer;
                 lbUltMovEstado.Text = "";
                 lbUltMovSector.Text = "";
                 lbUltMovObserv.Text = "";
                 lbFechaUltMov.Text = "";
             }

            #endregion obtiene ultimo estado y sector

            #region obtiene las prestaciones ingresadas

            sesSolicitudes = InvocaWsDao.TraerSolicitudesXIdBeneficiario(idBeneficiario.Value, codPrestacion, out mensajeer);

            rptSolicitudes.Visible = false;
            if (sesSolicitudes != null && mensajeer == "")
                CargarSolicitudes();
            else
                MError.MensajeError = mensajeer;
            #endregion obtiene las prestaciones ingresadas
            mensajeer = "";
            #region obtiene los expedientes cargados

            sesExpedientes = InvocaWsDao.TraeExpedientesXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);
            gridListadoexpedienteBen.Visible = false;
            if (sesExpedientes != null && mensajeer == "")
                CargarExpedientes();
            else
            {
                gridListadoexpedienteBen.Visible = false;
                MError.MensajeError = mensajeer;
            }
            #endregion 

            #region obtiene los movimientos

            sesMovimientos = InvocaWsDao.TraeMovimientosResumen(idBeneficiario.Value, codPrestacion, out mensajeer);
            gridMovimientosSol.Visible = false;
            if (sesMovimientos != null && mensajeer == "")
                CargarMovimientos();
            else
            {
                gridMovimientosSol.Visible = false;
                MError.MensajeError = mensajeer;
            }
            #endregion 

            mensajeer = "";

            #region obtiene los beneficios cargados

            sesBeneficios = InvocaWsDao.TraeBeneficiosXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);
            gridListadoBeneficio.Visible = false;
            if (sesBeneficios != null && mensajeer == "")
                CargarBeneficios();
            else
            {
                gridListadoBeneficio.Visible = false;
                MError.MensajeError = mensajeer;
            }

            #endregion obtiene las prestaciones ingresadas

            #region obtiene los ingresos de xdocumentacion

            sesIngresos = InvocaWsDao.TraeIngresosXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);

            rptIngresos.Visible = false;
            if (sesIngresos != null && mensajeer == "")
                CargarIngresos();
            else
                MError.MensajeError = mensajeer;
            #endregion obtiene los ingresos

            #region obtiene la documentacion faltante de ingresar

            sesDocFaltante = InvocaWsDao.TraeTipoDocumentacionFaltanteXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);
            gridDocFaltante.Visible = false;
            if (sesDocFaltante != null && mensajeer == "")
                CargarDocFaltante();
            else
            {
                gridDocFaltante.Visible = false;
                MError.MensajeError = mensajeer;
            }

            #endregion obtiene las prestaciones ingresadas

            mensajeer = "";

        }
    }

    #region Cargar datos del beneficiario


    protected void CargarDocFaltante()
    {
        try
        {
            #region Documentacion faltante
            if (sesDocFaltante == null || sesDocFaltante.Count == 0)
                gridDocFaltante.Visible = false;
            else
            {
                gridDocFaltante.DataSource = ToDatatable.toDataTable(sesDocFaltante);
                gridDocFaltante.DataBind();
                gridDocFaltante.Visible = true;
            }
            #endregion

        }
        catch
        {
            //blanquearPantalla();
            MError.MensajeError = "No se han podido obtener la documentación faltante.";
        }
    }


    protected void CargarBeneficios()
    {
        try
        {
            #region Datos beneficios
            if (sesBeneficios == null || sesBeneficios.Count == 0)
                gridListadoBeneficio.Visible = false;
            else
            {
                gridListadoBeneficio.DataSource = ToDatatable.toDataTable(sesBeneficios);
                gridListadoBeneficio.DataBind();
                gridListadoBeneficio.Visible = true;
            }
            #endregion

        }
        catch
        {
            //blanquearPantalla();
            MError.MensajeError = "No se han podido obtener beneficios.";
        }
    }

    protected void rptSolicitudes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item; // elemento del Repeater

        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        {
            Solicitud oSolicitud = (Solicitud)e.Item.DataItem;

            Label lblPais = (Label)item.FindControl("lblPais"); // obtenemos el control.
            lblPais.Text = oSolicitud.PaisDescCompleto;

            Label lbPrestacion = (Label)item.FindControl("lbPrestacion"); // obtenemos el control.
            lbPrestacion.Text = oSolicitud.DescripcionPrestacion;

            Label lblFechaSolicitud = (Label)item.FindControl("lblFechaSolicitud"); // obtenemos el control.
            lblFechaSolicitud.Text = oSolicitud.FechaIngreso.HasValue ? oSolicitud.FechaIngreso.Value.ToShortDateString() : "";

            Label lbObservacion = (Label)item.FindControl("lbObservacion"); // obtenemos el control.
            lbObservacion.Text = oSolicitud.Observaciones;

            Label lbRefExterior = (Label)item.FindControl("lbRefExterior"); // obtenemos el control.
            lbRefExterior.Text = oSolicitud.Referencia_exterior;

            Label lbUbicdoc = (Label)item.FindControl("lbUbicdoc"); // obtenemos el control.
            lbUbicdoc.Text = oSolicitud.Ubicacion_Fisica;

            Label lbDenegada = (Label)item.FindControl("lbDenegada"); // obtenemos el control.
            lbDenegada.Text = oSolicitud.CodMotivo.HasValue ? "DENEGADA - " + oSolicitud.DescripcionMotivo : "";
        }
    }

    protected void rptIngresos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item; // elemento del Repeater

        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        {
            Ingresos oIngresos = (Ingresos)e.Item.DataItem;

            Label lbIngFIng = (Label)item.FindControl("lbIngFIng"); // obtenemos el control.
            lbIngFIng.Text = oIngresos.FechaIngreso.HasValue ? oIngresos.FechaIngreso.Value.ToShortDateString() : "";

            Label lbIngVia = (Label)item.FindControl("lbIngVia"); // obtenemos el control.
            lbIngVia.Text = oIngresos.TipoIngreso.Descripcion;

            Label lbComentarioIng = (Label)item.FindControl("lbComentarioIng"); // obtenemos el control.
            lbComentarioIng.Text = oIngresos.Observacion;

            GridView gridDocRecibida = (GridView)item.FindControl("gridDocRecibida"); // obtenemos el control.
            if (oIngresos.LTipoDocumentacion.Length == 0)
                gridDocRecibida.Visible = false;
            else
            {
                gridDocRecibida.DataSource = oIngresos.LTipoDocumentacion;
                gridDocRecibida.DataBind();
                gridDocRecibida.Visible = true;
            }

        }
    }


    protected void CargarExpedientes()
    {
        try
        {
            #region Datos expedientes
            if (sesExpedientes == null || sesExpedientes.Count == 0)
                gridListadoexpedienteBen.Visible = false;
            else
            {
                gridListadoexpedienteBen.DataSource = ToDatatable.toDataTable(sesExpedientes);
                gridListadoexpedienteBen.DataBind();
                gridListadoexpedienteBen.Visible = true;
            }
            #endregion

        }
        catch
        {
            //blanquearPantalla();
            MError.MensajeError = "No se han podido obtener expedientes.";
        }
    }

    protected void CargarSolicitudes()
    {
        try
        {
            #region Datos Solicitudes
            if (sesSolicitudes == null || sesSolicitudes.Count == 0)
                rptSolicitudes.Visible = false;
            else
            {
                //rptSolicitudes.DataSource = ToDatatable.toDataTable(sesSolicitudes);
                rptSolicitudes.DataSource = sesSolicitudes;
                rptSolicitudes.DataBind();
                rptSolicitudes.Visible = true;
            }
            #endregion Datos Solicitudes

        }
        catch
        {
            MError.MensajeError = "No se han podido obtener las solicitudes.";
        }
    }


    protected void CargarIngresos()
    {
        try
        {
            #region Datos Ingresos
            if (sesIngresos == null || sesIngresos.Count == 0)
                rptIngresos.Visible = false;
            else
            {
                //rptSolicitudes.DataSource = ToDatatable.toDataTable(sesSolicitudes);
                rptIngresos.DataSource = sesIngresos;
                rptIngresos.DataBind();
                rptIngresos.Visible = true;
            }
            #endregion Datos ingresos

        }
        catch
        {
            MError.MensajeError = "No se han podido obtener los ingresos.";
        }
    }

    protected void CargarMovimientos()
    {
        try
        {
            #region Datos movimientos
            if (sesMovimientos == null || sesMovimientos.Count == 0)
                gridMovimientosSol.Visible = false;
            else
            {
                gridMovimientosSol.DataSource = ToDatatable.toDataTable(sesMovimientos);
                gridMovimientosSol.DataBind();
                gridMovimientosSol.Visible = true;
            }
            #endregion Datos Solicitudes

        }
        catch
        {
            MError.MensajeError = "No se han podido obtener los movimientos ingresados.";
        }
    }
    #endregion

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        String script = "<script type='text/javascript'>" + "hidden = window.close();" + "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "xxx", script, false);
    }


    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Session["lBenef"] = sesBeneficios;
        Session["lMovim"] = sesMovimientos;
        Session["lExped"] = sesExpedientes;
        Session["lSolic"] = sesSolicitudes;
        Session["ultMov"] = sesUltMovimiento;
        Session["ldocFaltante"] = sesDocFaltante;
        Session["lIngresos"] = sesIngresos;

        String script = "";
        script = "<script type='text/javascript'>" + "hidden = open('../Impresiones/PrintInfoCompletaPeticion.aspx?descPrestacion=" + descPrestacion + "&descApeNom=" + ApeNomBenef + "');" + "</script>";
        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", script, false);
    }
}