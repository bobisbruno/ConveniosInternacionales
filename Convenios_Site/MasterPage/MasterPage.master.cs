using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using System.Threading;
using Ar.Gov.Anses.Microinformatica;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using Anses.Director.Session;
using System.Linq;
using System.Net;



public partial class MasterPage : MasterPageBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(MasterPage).Name);

    public bool MenuVisible
    {
        set { Menu1.Visible = value; }
    }


    # region VARIABLES SESSION

    public bool sesEsUnicoRegistro
    {
        get { return Session["__sesEsUnico"] == null ? false : (bool)Session["__sesEsUnico"]; }
        set { Session["__sesEsUnico"] = value; }
    }
    public string parametrosConsulta
    {
        get { return Session["_paramConsulta"].ToString(); }
        set { Session["_paramConsulta"] = value; }
    }
    
    public string CUE
    {
        get { return Session["CUE"].ToString(); }
        set { Session["CUE"] = value; }
    }

    public string CUEDescri
    {
        get { return Session["CUEDescri"].ToString(); }
        set { Session["CUEDescri"] = value; }
    }

    public string Usuario_TK
    {
        get { return Session["Usuario_TK"].ToString(); }
        set { Session["Usuario_TK"] = value; }
    }

    public string DescUsuario_TK
    {
        get { return Session["DescUsuario_TK"].ToString(); }
        set { Session["DescUsuario_TK"] = value; }
    }

    public string Oficina_TK
    {
        get { return Session["Oficina_TK"].ToString(); }
        set { Session["Oficina_TK"] = value; }
    }

    public string OficinaDesc_TK
    {
        get { return Session["OficinaDesc_TK"].ToString(); }
        set { Session["OficinaDesc_TK"] = value; }
    }

    public string DirIP_TK
    {
        get { return Session["DirIP_TK"].ToString(); }
        set { Session["DirIP_TK"] = value; }
    }

    public string Grupo_TK
    {
        get { return Session["Grupo_TK"].ToString(); }
        set { Session["Grupo_TK"] = value; }
    }
        
    #endregion

    #region Refer HTTP
    public string vsHttpReferer
    {
        get
        {
            if (ViewState["__httpReferrer"] != null)
                return (string)ViewState["__httpReferrer"];
            else
                return string.Empty;
        }
        set { ViewState["__httpReferrer"] = value; }
    }
    #endregion Refer HTTP

    private bool AplicarSeguridadAccesoMenu()
    {
        bool permiso = false;
        try
        {
            permiso = TienePermiso("accesoMenu");
        }
        catch (ThreadAbortException)
        { }
        return permiso;
    }

    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.LoadComplete += new EventHandler(Page_LoadComplete);
            try
            {
                #region Verificacion del usuario

                try
                {

                    //TODO TOKEN :- Obtiene el usuario para presentarlo ID - Nombre
                    IUsuarioToken usuarioEnDirector = new UsuarioToken();

                    SSOToken sSOToken = Credencial.ObtenerCredencial().SSOToken;
                    LoginElement login = sSOToken.Operation.Login;
                    //de aca
                    usuarioEnDirector.ObtenerUsuario();
                    if (!usuarioEnDirector.VerificarToken())
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"].ToString());
                    }

                    CargarIdentificador(usuarioEnDirector.Oficina + " - " + usuarioEnDirector.OficinaDesc);
                    CargarNombre(usuarioEnDirector.IdUsuario + " - " + usuarioEnDirector.Nombre);
                    CargarFecha();
                    DirIP_TK = usuarioEnDirector.DirIP;
                    Usuario_TK = usuarioEnDirector.IdUsuario;
                    DescUsuario_TK = usuarioEnDirector.Nombre;
                    Oficina_TK = usuarioEnDirector.Oficina;
                    OficinaDesc_TK = usuarioEnDirector.OficinaDesc;

                    foreach (Anses.Director.Session.GroupElement i in usuarioEnDirector.Grupos)
                    {
                        Grupo_TK = i.Name.ToString();
                    }
                    CargarPerfil(Grupo_TK);
                    //hasta aca
                    ////funcion necesaria para la obtencion de los permisos desde el director.
                    //CargarGruposYSistema(DirectorManager.DirGroups, usuarioEnDirector.Sistema);

                    //punto de inyeccion del menú
                    //if (AplicarSeguridadAccesoMenu())
                    Menu1.CargarMenu(Boolean.Parse(ConfigurationManager.AppSettings["usarMenuDinamico"].ToString()) ? ObtenerMenu() : ObtenerMenu2());
                }
                catch (UsuarioTokenException err)
                {
                    if (log.IsErrorEnabled) { log.ErrorFormat("UsuarioTokenException - Se produjo el siguiente error intentando obtener el TOKEN del usuario >> {0}", err); }
                    Response.Redirect(ConfigurationManager.AppSettings["ErrorURL"].ToString());
                }
                catch (UsuarioException err)
                {
                    if (log.IsErrorEnabled) { log.ErrorFormat("UsuarioException - Se produjo el siguiente error intentando obtener datos del usuario >> {0}", err); }
                    Response.Redirect(ConfigurationManager.AppSettings["ErrorURL"].ToString());
                }
                catch (ThreadAbortException) { }
                catch (Exception err)
                {
                    log.Error(err.Message, err);
                }
                #endregion
            }
            catch (Exception err)
            {
                log.ErrorFormat("Error al cargar la pagina Main.aspx error: {0}", err.Message);
            }

        }
    }

    #region Completado
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (!IsPostBack && Request.UrlReferrer != null)
            vsHttpReferer = Request.UrlReferrer.ToString();
    }
    #endregion Completado

    protected void Page_Load(object sender, EventArgs e)
    {
        RegistrarScripts();
        try
        {
            #region Expiracion de la Pagina

            //Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 60)) + "; URL=" + ConfigurationManager.AppSettings["TimeoutURL"].ToString());
            Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 120)) + "; URL=" + ConfigurationManager.AppSettings["TimeoutURL"].ToString());
            Response.Expires = -1;
            Response.Cache.SetNoStore();
            Response.CacheControl = "Private";
            Response.AppendHeader("Pragma", "no-cache");

            #endregion
        }
        catch (ThreadAbortException) { }
        catch (Exception err)
        {
            log.Error(err.Message, err);
        }
    }

    #region IMenuGenerales Members

    public void CargarNombre(string nombre)
    {
        MenuGenerales2.CargarNombre(nombre);
    }

    public void CargarIdentificador(string identificador)
    {
        MenuGenerales2.CargarIdentificador(identificador);
    }

    public void CargarFecha()
    {
        //MenuGenerales2.CargarFecha();
    }

    #endregion

    protected void lnkIconoHome_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Default.aspx");
        }
        catch (ThreadAbortException) { }
    } 

    public void DeshabilitarBotones()
    {
    }

    public void HabilitarMenuPorPerfil()
    {
    }

    public void CargarPerfil(string perfil)
    {
        MenuGenerales2.CargarPerfil(perfil);
    }

    public bool EsNumerico(string NumericText)
    {
        bool isnumber = true;
        foreach (char c in NumericText)
        {
            isnumber = char.IsNumber(c);
            if (!isnumber)
            {
                return isnumber;
            }
        }
        return isnumber;

    }

    private void RegistrarScripts()
    //Chequear
    {
        if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "jquery"))
            this.Page.ClientScript.RegisterClientScriptInclude("jquery", ResolveClientUrl("~/comun/JS/jquery.min.js"));
        //Script del menu
        if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "jquerycssmenu"))
            this.Page.ClientScript.RegisterClientScriptInclude("jquerycssmenu", ResolveClientUrl("~/comun/JS/jquerycssmenu.js"));

        RegisterMenuArrowsScript();

        //Script de solpas
        if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "styleswitcher"))
            this.Page.ClientScript.RegisterClientScriptInclude("styleswitcher", ResolveClientUrl("~/comun/JS/styleswitcher.js"));

        if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "tabber"))
            this.Page.ClientScript.RegisterClientScriptInclude("tabber", ResolveClientUrl("~/comun/JS/tabber.js"));
        //Fin scripts de solpas

    }

    private void RegisterMenuArrowsScript()
    {
        System.Text.StringBuilder script = new System.Text.StringBuilder();

        script.Append("<script language=\"javascript\">\n");
        script.AppendFormat("arrowimages.down[1] = \"{0}\"\n", this.ResolveClientUrl("~/App_Themes/Imagenes/arrow-down.gif"));
        script.AppendFormat("arrowimages.right[1] = \"{0}\"\n", this.ResolveClientUrl("~/App_Themes/Imagenes/arrow-right.gif"));
        script.Append("</script>\n");

        Type type = this.GetType();

        ClientScriptManager ClientScript = this.Page.ClientScript;

        if (!ClientScript.IsClientScriptBlockRegistered(type, "jquerycssmenuarrows"))
            ClientScript.RegisterClientScriptBlock(type, "jquerycssmenuarrows",
        script.ToString());
    }

}
