using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using AuxiliaresWS;
using ActoresWS;
using ConsultasWS;
using System.Threading;
using System.IO;
using log4net;


//public partial class Paginas_Main : PageBase
public partial class Paginas_Main : System.Web.UI.Page
{
    private readonly ILog log = LogManager.GetLogger(typeof(Paginas_Main).Name);

    #region variables publicas
    
    private long? TotalRowsNum
    {
        get { return Convert.ToInt64(ViewState["TotalRowsNum"]); }
        set { ViewState["TotalRowsNum"] = value; }
    }

    protected string encabezadoCriterio
    {
        get { return (string)ViewState["encabCriter"]; }
        set { ViewState["encabCriter"] = value; }
    }

    #endregion

    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnBuscar);

        if (!IsPostBack)
        {
            MError.MensajeError = string.Empty;
            try
            {
                #region seguridad
                
                if (!AplicarSeguridadPagina())
                    Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"]);

                //if (AplicarSeguridadAccesoMenu())
                //    Menu1.CargarMenu(Boolean.Parse(ConfigurationManager.AppSettings["usarMenuDinamico"].ToString()) ? ObtenerMenu() : ObtenerMenu2());

                #endregion seguridad

                InicializarDatosPagina("Búsqueda de Solicitante", " ");
                
                divNoConsulta.Visible = false;
                btnNueva.Visible = false;
                //throw new Exception();
                #region Devoluciones Notificadas vencidas
                ////Trae la primera pagina de notirficaciones Vencidas
                //if (AplicarSeguridadConsultaNotificacionesVencidas())
                //{
                //    dvDevNotifVencidas.Visible = true;
                //    cargarDevolucionesNotificadasVencidas();
                //}
                //else
                //    dvDevNotifVencidas.Visible = false;
                #endregion Devoluciones Notificadas vencidas

                #region Tramites provisorios
                //Trae la primera pagina de notirficaciones Vencidas
                if (AplicarSeguridadConsultaProvisorios())
                {
                    string mensaje = "";
                    
                    List<SolicitudProvisoria> iLSolicitudesProvisorias = InvocaWsDao.TraeSolicitudesProvisorias(DateTime.Today.Year.ToString(), DateTime.Today.Month.ToString(), null, null, true, out mensaje);
                    List<SolicitudProvisoriaExtendida> iLSolicitudesExtendidas = InvocaWsDao.TraeSolicitudesProvisoriasAVencerEnPlazo(out mensaje);
                    Int32 totProvisorias = iLSolicitudesProvisorias.Count;
                    Int32 totProvisoriasAVencer = iLSolicitudesExtendidas.Count;
                    
                    if(totProvisorias == 0 && totProvisoriasAVencer == 0)
                        dvSolicitudesProvisorias.Visible = false;
                    else
                    {
                        Session["ProvisoriosAVencer"] = totProvisoriasAVencer == 0 ? null : (List<SolicitudProvisoriaExtendida>)iLSolicitudesExtendidas;
                        btnVerProvisoriosaVencer.Visible = totProvisoriasAVencer > 0;
                        Session["Provisorios"] = totProvisorias == 0 ? null : (List<SolicitudProvisoria>)iLSolicitudesProvisorias;
                        btnverProvisorios.Visible = totProvisorias > 0;
                        dvSolicitudesProvisorias.Visible = true;
                        lbCantidadTrProvisoriosActual.Text = totProvisorias.ToString();
                        lbCantidadTrProvisoriosAVencer.Text = totProvisoriasAVencer.ToString();
                    }
                }
                else
                    dvSolicitudesProvisorias.Visible = false;
                ////Trae la primera pagina de notirficaciones Vencidas
                //if (AplicarSeguridadConsultaNotificacionesVencidas())
                //{
                //    dvDevNotifVencidas.Visible = true;
                //    cargarDevolucionesNotificadasVencidas();
                //}
                //else
                //    dvDevNotifVencidas.Visible = false;
                #endregion
            }

            catch (ThreadAbortException err)
            { log.ErrorFormat("Error al cargar la pagina Main.aspx error: {0}", err.Message); }
            catch (Exception err)
            {
                log.ErrorFormat("Error al cargar la pagina Main.aspx error: {0}", err.Message);
                MError.MensajeError = "Ocurrio un error al intentar efectuar la consulta. Revisar log para mayor informacion.";
                //dvDevNotifVencidas.Visible = false;
            }
        }
    }


    private void InicializarDatosPagina(string titulo, string txtBarraNav)
    {
        UtilsPresentacionX5.SetTextLabel(LblTituloPagina, titulo);
        BNav.Text = txtBarraNav;
        Page.Title = titulo;
    }

    #region APlica Seguridad
    private bool AplicarSeguridadPagina()
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
    private bool AplicarSeguridadConsultaNotificacionesVencidas()
    {
        bool permiso = false;
        try
        {
            permiso = TienePermiso("accesoNotificacionesVencidas");
        }
        catch (ThreadAbortException)
        { }
        return permiso;
    }

    private bool AplicarSeguridadConsultaProvisorios()
    {
        bool permiso = false;
        try
        {
            permiso = TienePermiso("accesoDatosProvisorios");
        }
        catch (ThreadAbortException)
        { }
        return permiso;
    }
    #endregion APlica Seguridad Pagina

    
    #region Cargar devoluciones vencidas

    //private void cargarDevolucionesNotificadasVencidas()
    //{
    //    string mensaje = "";
    //    Int64 Total;

    //    List<NotificacionesVencidas> n = null;
    //    if (TotalRowsNum == null || TotalRowsNum == 0)
    //    {
    //        n = InvocaWsDao.TraeDevolucionesNotificadasVencidasXPlazo(1, 0, 1, out Total, out mensaje);
    //        TotalRowsNum = Total;
    //    }
        
    //    MError.MensajeError = mensaje;
    //    if (TotalRowsNum != null && TotalRowsNum > 0)
    //    {
    //        divListadoNoNotificados.Visible = true;
    //        lbElementosEncontrados.Text = TotalRowsNum.ToString();
    //    }
    //    else
    //        divListadoNoNotificados.Visible = false;
    //}

    #endregion Cargar devoluciones vencidas


    protected void btnConsultarDNFP_Click(object sender, EventArgs e)
    {
        Response.Redirect("DevolucionesNotifFueraPlazo.aspx", false);
    }
    protected void btnAltaBeneficio_Click(object sender, EventArgs e)
    {
        Master.sesEsUnicoRegistro = true;
        Response.Redirect("AMBeneficiario.aspx?acceso=INSERT", false);
    }
    

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        //pequeña validacion de ingreso
        string mensaje = busben.validaParams;
        MError.MensajeError = mensaje;

        if (mensaje.Equals(string.Empty))
        {
            List<ActoresWS.LsBeneficiario> oLista = null;
            switch (busben.TipoCriterio)
            {
                case TipoConsultaBeneficioario.NombreoApellidos:
                    oLista = InvocaWsDao.TraerBeneficiarios(TipoConsultaBeneficioario.NombreoApellidos, busben.ApellidoNombre, string.Empty, out mensaje);
                    encabezadoCriterio = "nombre o apellido " + busben.ApellidoNombre;
                    break;
                case TipoConsultaBeneficioario.Documento:
                    oLista = InvocaWsDao.TraerBeneficiarios(TipoConsultaBeneficioario.Documento, busben.Documento, string.Empty, out mensaje);
                    encabezadoCriterio = "documento " + busben.Documento;
                    break;
                case TipoConsultaBeneficioario.DocumentoYTipo:
                    oLista = InvocaWsDao.TraerBeneficiarios(TipoConsultaBeneficioario.DocumentoYTipo, busben.Documento, busben.TipoDoc, out mensaje);
                    encabezadoCriterio = "documento y tipo " + busben.Documento + "-" + busben.TipoDocDescripcion;
                    break;
                case TipoConsultaBeneficioario.CodigoSIACI:
                    oLista = InvocaWsDao.TraerBeneficiarios(TipoConsultaBeneficioario.CodigoSIACI, busben.CodigoCiaci, string.Empty, out mensaje);
                    encabezadoCriterio = "Código SIACI " + busben.CodigoCiaci;
                    break;
                case TipoConsultaBeneficioario.Expediente:
                    oLista = InvocaWsDao.TraeBeneficiariosXExpteANSES(busben.ExpeOrg, busben.ExpePre, busben.ExpeDoc, busben.ExpeDig, busben.ExpeTram, busben.ExpeSecu, out mensaje);
                    encabezadoCriterio = "Expediente " + busben.ExpeComp;
                    break;
                case TipoConsultaBeneficioario.Beneficio:
                    oLista = InvocaWsDao.TraeBeneficiariosXNroBeneficioANSES(busben.BenExCaja, busben.BenTipo, busben.BenNumero, busben.BenCopart, busben.BenDigVerif, out mensaje);
                    encabezadoCriterio = "Beneficio " + busben.BenComp;
                    
                    break;
                case TipoConsultaBeneficioario.CUIP:
                    oLista = InvocaWsDao.TraeBeneficiariosXCUIP(busben.PreCUIP, busben.DocCUIP, busben.DigCUIP, out mensaje);
                    encabezadoCriterio = "CUIL " + busben.CUIPComp;

                    break;
                case TipoConsultaBeneficioario.Tramite:
                    oLista = InvocaWsDao.TraeBeneficiariosXNroSolicitudProvisoria(busben.TramiteNro, out mensaje);
                    encabezadoCriterio = "Trámite " + busben.TramiteNro;
                    break;
            }
            //Muestra aviso de Error no controlado
            busben.Limpiar();
            MError.MensajeError = mensaje;
            
            if (oLista == null || oLista.Count == 0)
            {
                divNoConsulta.Visible = true;
                btnNueva.Visible = true;
            }
            else
            {
                btnNueva.Visible = false;
                if (oLista.Count == 1)
                {
                    Master.sesEsUnicoRegistro = true;
                    Master.parametrosConsulta = "InformacionCompletaBeneficio.aspx?idBeneficiario=" + oLista[0].Id_Beneficiario.ToString().Trim();
                }
                else
                {
                    Session["__sesBeneficiarios"] = (List<ActoresWS.LsBeneficiario>)oLista;
                    Master.sesEsUnicoRegistro = false;
                    Master.parametrosConsulta = "ListaBeneficiarios.aspx?tituloCriterio=" + encabezadoCriterio;
                }
                Response.Redirect(Master.parametrosConsulta);
            }
        }
    }
    protected void verProvisorios_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConsultaTProvisorios.aspx", false);
    }
    protected void btnVerProvisoriosaVencer_Click(object sender, EventArgs e)
    {
        String script = "<script type='text/javascript'>" + "hidden = open('ProvisoriasAVencer.aspx?');" + "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", script, false);
    }
}
