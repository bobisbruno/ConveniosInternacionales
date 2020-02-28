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




public partial class Paginas_ProvisoriasAVencer : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(typeof(Paginas_ProvisoriasAVencer).Name);

    #region Sesiones

    private List<SolicitudProvisoriaExtendida> sesSolicitudesAVencer
    {
        get { return (List<SolicitudProvisoriaExtendida>)Session["ProvisoriosAVencer"]; }
        set {
            Session["ProvisoriosAVencer"] = value;
        }
        
    }
    #endregion Sesiones

    #region Variables Ordenamiento
    protected string SortExpression
    {
        get { return ViewState["SortExpression"] as string; }
        set { ViewState["SortExpression"] = value; }
    }

    protected SortDirection SortDirection
    {
        get
        {
            object o = ViewState["SortDirection"];
            if (o == null)
                return SortDirection.Ascending;
            return (SortDirection)o;
        }
        set { ViewState["SortDirection"] = value; }
    }

    #endregion Variables Ordenamiento



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
        //mensaje.ClickSi += new Mensaje.Click_UsuarioSi(ClickearonSi);
        //mensaje.ClickNo += new Mensaje.Click_UsuarioNo(ClickearonNo);

        if (!IsPostBack)
        {
            try
            {
                //Check de Seguridad tanto para ingresar como modificar instrumento
                if (!AplicarSeguridad())
                    Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegadoNBtn"].ToString());

                LlenaGrilla(string.Empty, string.Empty);
                lbElementosEncontrados.Text = sesSolicitudesAVencer.Count.ToString();
            }
            catch (Exception er)
            {
                if (log.IsErrorEnabled)
                    log.ErrorFormat("Se generó una excepción : {0}", er.Message);
            }
        }
    }

    private void LlenaGrilla(string campoFiltro, string contenidoFiltro)
    {
        try
        {
            DataBindGrid();
        }
        catch (Exception err)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", err.Message);
        }
    }

    protected void gv_Grilla_Sorted(object sender, EventArgs e)
    {
        try
        {
            DataBindGrid();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
        }
    }


    private void InicializarOrdenamientoGrilla(string sortExpression, SortDirection sortDirection)
    {
        SortExpression = sortExpression;
        SortDirection = sortDirection;
    }

    private void Ordenar()
    {
        bool sortAscending =
            SortDirection == SortDirection.Ascending;
        var list = from u in sesSolicitudesAVencer
                   select u;

        switch (SortExpression)
        {
            case "ApellidoyNombre":
                list = sortAscending ? list.OrderBy(u => u.ApellildoynombreDeclarado) :
                    list.OrderByDescending(u => u.ApellildoynombreDeclarado);
                break;
            case "Desc_Prestacion":
                list = sortAscending ? list.OrderBy(u => u.PrestacionSolicitada == null ? "" : u.PrestacionSolicitada.Descripcion) :
                    list.OrderByDescending(u => u.PrestacionSolicitada == null ? "" : u.PrestacionSolicitada.Descripcion);
                break;
            case "Desc_Pais":
                list = sortAscending ? list.OrderBy(u => u.PaisConvenio == null ? "" : u.PaisConvenio.Descripcion) :
                    list.OrderByDescending(u => u.PaisConvenio == null ? "" : u.PaisConvenio.Descripcion);
                break;
            case "Nro_SolicitudProvisoria":
                list = sortAscending ? list.OrderBy(u => u.Nro_SolicitudProvisoria) :
                    list.OrderByDescending(u => u.Nro_SolicitudProvisoria);
                break;
            case "Sectorderiva":
                list = sortAscending ? list.OrderBy(u => u.Sectorderiva == null ? "" : u.Sectorderiva.Descripcion) :
                    list.OrderByDescending(u => u.Sectorderiva == null ? "" : u.Sectorderiva.Descripcion);
                break;

            case "DiasCaducidad":
                list = sortAscending ? list.OrderBy(u => !u.DiasCaducidad.HasValue ? "" : u.DiasCaducidad.Value.ToString()) :
                    list.OrderByDescending(u => !u.DiasCaducidad.HasValue ? "" : u.DiasCaducidad.Value.ToString());
                break;
        }
        sesSolicitudesAVencer = list.ToList();
    }


    private void DataBindGrid()
    {
        Ordenar();
        gridListadoSolicitudes.DataSource = ToDatatable.toDataTable(sesSolicitudesAVencer);
        gridListadoSolicitudes.DataBind();

        UtilsPresentacion.EmptyGridFix(gridListadoSolicitudes, sesSolicitudesAVencer, "No hay datos para la consulta");
    }

    protected void gv_Grilla_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (SortExpression == e.SortExpression)
            {
                SortDirection = SortDirection == SortDirection.Ascending ?
                    SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                SortDirection = SortDirection.Ascending;
            }
            SortExpression = e.SortExpression;

            gridListadoSolicitudes.EditIndex = -1;
            gridListadoSolicitudes.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            //throw ex;
        }
    }

    #region RowCommand
    protected void RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    #endregion RowCommand

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        String script;
        script = "<script type='text/javascript'>" + "hidden = window.close();" + "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "xxx", script, false);
    }
}