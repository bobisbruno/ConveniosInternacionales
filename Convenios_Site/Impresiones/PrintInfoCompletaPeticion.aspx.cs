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
using ActoresWS;


public partial class InformacionCompletaPeticionPrint : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(typeof(InformacionCompletaPeticionPrint).Name);

    #region Sesiones

    
    private String descPrestacion
    {
        get { return (String)ViewState["_dPrestacion"]; }
        set { ViewState["_dPrestacion"] = value; }
    }

    private String ApeNomBenef
    {
        get { return (String)ViewState["_apenom"]; }
        set { ViewState["_apenom"] = value; }
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
        if (!IsPostBack)
        {
            //Check de Seguridad tanto para ingresar como modificar instrumento
            //if (!AplicarSeguridad())
            //    Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegadoNBtn"].ToString());

            ApeNomBenef = (string)Request.QueryString["descApeNom"];
            lbEncabezadoBeneficiario.Text = ApeNomBenef;
            descPrestacion = Request.QueryString["descPrestacion"].ToString();
            lbdescPrestacion.Text = " " + descPrestacion;


            String mensajeer = "";

            #region obtiene ultimo estado y sector
            ConsultasWS.Movimiento_Solicitud oMovimiento = null;

            oMovimiento = (ConsultasWS.Movimiento_Solicitud)Session["ultMov"];
            if (oMovimiento != null && mensajeer == "")
            {
                lbUltMovEstado.Text = oMovimiento.Estado.Descripcion;
                lbUltMovSector.Text = oMovimiento.Sector.Descripcion;
                lbUltMovObserv.Text = oMovimiento.Observaciones;
                lbFechaUltMov.Text = oMovimiento.Fecha_Movimiento.ToShortDateString();
            }
            else
            {
                lbUltMovEstado.Text = "";
                lbUltMovSector.Text = "";
                lbUltMovObserv.Text = "";
                lbFechaUltMov.Text = "";
            }

            #endregion obtiene ultimo estado y sector

            #region obtiene las prestaciones ingresadas

            List<ActoresWS.Solicitud> sesSolicitudes = (List<ActoresWS.Solicitud>)Session["lSolic"];
            rptSolicitudes.Visible = false;
            if (sesSolicitudes != null && mensajeer == "")
                CargarSolicitudes(sesSolicitudes);
            
            #endregion obtiene las prestaciones ingresadas
            
            #region obtiene los expedientes cargados

            List<ActoresWS.Expediente_Solicitud> sesExpedientes = (List<ActoresWS.Expediente_Solicitud>)Session["lExped"];
            gridListadoexpedienteBen.Visible = false;
            if (sesExpedientes != null && mensajeer == "")
                CargarExpedientes(sesExpedientes);
            else
                gridListadoexpedienteBen.Visible = false;
            #endregion

            #region obtiene los movimientos

            List<ActoresWS.IngDevMov> sesMovimientos = (List<ActoresWS.IngDevMov>)Session["lMovim"];
            gridMovimientosSol.Visible = false;
            if (sesMovimientos != null && mensajeer == "")
                CargarMovimientos(sesMovimientos);
            else
                gridMovimientosSol.Visible = false;
            #endregion

            #region obtiene los beneficios cargados

            List<ActoresWS.Beneficio_Solicitud> sesBeneficios = (List<ActoresWS.Beneficio_Solicitud>)Session["lBenef"];
            gridListadoBeneficio.Visible = false;
            if (sesBeneficios != null && mensajeer == "")
                CargarBeneficios(sesBeneficios);
            else
                gridListadoBeneficio.Visible = false;
            
            #endregion obtiene las prestaciones ingresadas

            #region obtiene los ingresos

            List<ActoresWS.Ingresos> sesIngresos = (List<ActoresWS.Ingresos>)Session["lIngresos"];
            rptIngresos.Visible = false;
            if (sesIngresos != null && mensajeer == "")
                CargarIngresos(sesIngresos);

            #endregion obtiene las prestaciones ingresadas

            #region obtiene la documentación faltante

            List<ActoresWS.TipoDocumentacion_Prestacion> sesDocFaltante = (List<ActoresWS.TipoDocumentacion_Prestacion>)Session["ldocFaltante"];
            gridDocFaltante.Visible = false;
            if (sesDocFaltante != null && mensajeer == "")
                CargarDocumentacionFaltante(sesDocFaltante);
            else
                gridDocFaltante.Visible = false;
            
            #endregion

            mensajeer = "";

            Session["lBenef"] = null;
            Session["lMovim"] = null;
            Session["lExped"] = null;
            Session["lSolic"] = null;
            Session["ultMov"] = null;
            Session["ldocFaltante"] = null;
            Session["lIngresos"] = null;


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


    #region Cargar datos del beneficiario


    protected void CargarDocumentacionFaltante(List<ActoresWS.TipoDocumentacion_Prestacion> sesDocFaltante)
    {
        try
        {
            #region Datos documentacion faltante
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
            
        }
    }


    protected void CargarBeneficios(List<ActoresWS.Beneficio_Solicitud> sesBeneficios)
    {
        try
        {
            #region Datos expedientes
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
            
        }
    }

    protected void CargarIngresos(List<ActoresWS.Ingresos> sesIngresos)
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
           
        }
    }


    protected void CargarExpedientes(List<ActoresWS.Expediente_Solicitud> sesExpedientes)
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
          
        }
    }

    protected void CargarSolicitudes(List<ActoresWS.Solicitud> sesSolicitudes)
    {
        try
        {
            #region Datos Solicitudes
            if (sesSolicitudes == null || sesSolicitudes.Count == 0)
                rptSolicitudes.Visible = false;
            else
            {
                rptSolicitudes.DataSource = sesSolicitudes;
                rptSolicitudes.DataBind();
                rptSolicitudes.Visible = true;
            }
            #endregion Datos Solicitudes

        }
        catch
        {
           
        }
    }

    protected void CargarMovimientos(List<ActoresWS.IngDevMov> sesMovimientos)
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
            
        }
    }
    #endregion
}