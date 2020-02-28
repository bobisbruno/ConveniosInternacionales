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


public partial class ANotificacion : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ANotificacion).Name);

    #region Sesiones

    private List<Devolucion> sesNotificar
    {
        get { return (List<Devolucion>)ViewState["_dev"]; }
        set { ViewState["_dev"] = value; }
    }

    #region Variables de la solicitud
    private Int16 codPrestacion
    {
        get { return (Int16)ViewState["_codPrestacion"]; }
        set { ViewState["_codPrestacion"] = value; }
    }

    private Int16 codPais
    {
        get { return (Int16)ViewState["_codPais"]; }
        set { ViewState["_codPais"] = value; }
    }

    private DateTime fechaNotifica
    {
        get { return (DateTime)ViewState["_fnot"]; }
        set { ViewState["_fnot"] = value; }
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

    #endregion Variables de la solicitud

    
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
        mensaje.ClickSi += new Mensaje.Click_UsuarioSi(ClickearonSi);
        mensaje.ClickNo += new Mensaje.Click_UsuarioNo(ClickearonNo);

        rvtxtFechaNotificacion.MinimumValue = DateTime.Today.AddYears(-2).ToString("dd/MM/yyyy");
        rvtxtFechaNotificacion.MaximumValue = DateTime.Today.ToString("dd/MM/yyyy");
        if (!IsPostBack)
        {
            //Check de Seguridad tanto para ingresar como modificar instrumento
            if (!AplicarSeguridad())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegadoNBtn"].ToString());
            
            MError.MensajeError = string.Empty;

            txtFechaNotificacion.Text = string.Empty;

            idBeneficiario = Int64.Parse(Request.QueryString["idBeneficiario"].ToString());
            lbEncabezadoBeneficiario.Text = (string)Request.QueryString["descApeNom"];
            codPrestacion = Int16.Parse(Request.QueryString["codPrestacion"].ToString());
            codPais = Int16.Parse(Request.QueryString["codPais"].ToString());
            descPrestacion = Request.QueryString["descPrestacion"].ToString();
            lbdescPrestacion.Text = " - " + descPrestacion;
            descPais = Request.QueryString["descPais"].ToString();
            lbDescPaisS.Text = " - " + descPais;
            btnGuardar.Enabled = false;

            Traer();
        }
    }

    private void Traer()
    {
        string mensajeer = "";
        try
        {   
            List<Devolucion> odev = InvocaWsDao.TraeDevolucionXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);

            if (odev != null || odev.Count != 0)
            {
                List<Devolucion> listemp = new List<Devolucion>();
                foreach (Devolucion dev in odev)
                    if (dev.FechaNotificacion == null)
                        listemp.Add(dev); //temp guarda las devoluciones sin notificar
                if (listemp.Count == 0) //caso de no encontrar notificaciones
                {
                    gridListadoDevoluciones.Visible = false;
                    dvNoDevolucion.Visible = true;
                    dvNotificacion.Visible = false;
                }
                else
                {
                    sesNotificar = listemp;
                    dvNoDevolucion.Visible = false;
                    dvNotificacion.Visible = true;
                    btnGuardar.Enabled = true;
                    gridListadoDevoluciones.DataSource = ToDatatable.toDataTable(sesNotificar);
                    gridListadoDevoluciones.DataBind();
                    gridListadoDevoluciones.Visible = true;
                }
            }
            else
            {
                gridListadoDevoluciones.Visible = false;
                dvNoDevolucion.Visible = true;
                dvNotificacion.Visible = false;
            }
        }
        catch (Exception er)
        {
            log.Error(er.Message);
            MError.MensajeError = er.Message;
        }
 
    }

    #region Guardar todo


    #region SI
    protected void ClickearonSi(object sender, string quienLlamo)
    {
        switch (quienLlamo.Trim())
        {
            case "btnCerrar_Click":
                break;


            case "AltaExitosa":

                break;
        }
        //Master.MensajeError = "";

    }
    #endregion SI

    #region NO
    protected void ClickearonNo(object sender, string quienLlamo)
    {

    }
    #endregion NO


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string mensajeError = string.Empty;

        Page.Validate();
        if(Page.IsValid)
        {
            try
            {
                foreach (Devolucion iDev in sesNotificar)
                {
                    string mensaje = "";
                    InvocaWsDao.NotificaDevolucion(idBeneficiario.Value, codPrestacion, iDev.FechaMovimiento.ToShortDateString().Trim(), txtFechaNotificacion.Text.Trim(), out mensaje);
                    mensajeError += mensaje.Equals(string.Empty) ? "" : mensaje + "-";
                }
                if (mensajeError.Equals(string.Empty))
                {
                    mensaje.DescripcionMensaje = "La/s devolución/es fueron notificadas con éxito";
                    mensaje.TipoMensaje = Mensaje.infoMensaje.Info;
                    mensaje.Mostrar();
                    gridListadoDevoluciones.Visible = false;
                    txtFechaNotificacion.Text = "";
                    btnGuardar.Enabled = false;
                }
                else
                    MError.MensajeError = mensajeError;
            }
            catch (Exception er)
            {
                log.Error(er.Message);
                MError.MensajeError = er.Message;
            }
        }
        
        
    }
    #endregion Guardar

   

    #region Regresar
    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        String script = "<script type='text/javascript'>" + "hidden = window.close();" + "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "xxx", script, false);
    }
    #endregion Regresar
}