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
using AuxiliaresWS;

public partial class Paginas_AMBancos : System.Web.UI.Page
{
    readonly ILog log = LogManager.GetLogger(typeof(Paginas_AMBancos));

    #region Propiedades


    protected List<BancoWS.Banco> LstDatosBancos
    {
        get { return (List<BancoWS.Banco>)ViewState["lstDatosban"]; }
        set { ViewState["lstDatosban"] = value; }
    }

    protected int IndexGrilla
    {
        get { return (int)ViewState["indexGrilla"]; }
        set { ViewState["indexGrilla"] = value; }
    }

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

    
    #endregion

    #region APlica Seguridad Pagina
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

    private bool TienePermiso(string valor)
    {
        return true;
    }

    #endregion APlica Seguridad Pagina

    #region Page Load Eventos


    public void Page_PreRender(object sender, EventArgs e)
    {
        //
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //
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
        try
        {
            //Cargo todos los mensajes posibles
            mensaje.ClickSi += new Mensaje.Click_UsuarioSi(ClickearonSi);
            mensaje.ClickNo += new Mensaje.Click_UsuarioNo(ClickearonNo);
            //mensajeOk.ClickSi += new Mensaje.Click_UsuarioSi(ClickearonSi);
            //mensajeOk.ClickNo += new Mensaje.Click_UsuarioNo(ClickearonNo);

            //busquedaGrilla.ClickBuscar += new comun_controles_BusquedaGrilla.Click_Buscar(ClickearonBuscar);

            if (!IsPostBack)
            {
                #region Seguridad
                if (!AplicarSeguridad())
                    Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"].ToString());
                #endregion Seguridad

                InicializarDatosPagina("Administrar Bancos", "> Administración > Bancos");

                //InicializarDatosPagina("Administrar Bancos");

                InicializarOrdenamientoGrilla("Descripcion", SortDirection.Ascending);

                //LlenaDdlCamposBusqueda();

                LlenaGrilla(string.Empty, string.Empty);
            }
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            throw ex;
        }
    }

    #endregion Page Load Eventos


    //private void ActualizarGV(List<AuxiliaresWS.Prestacion> iList, String valorCodPrestacion)
    //{

    //    foreach (AuxiliaresWS.Prestacion P in iList)
    //    {
    //        if (P.Cod_Prestacion.ToString().Equals(valorCodPrestacion))
    //        {
    //            //gvTiposDocumentacion.DataSource = ToDatatable.toDataTable(P.LTipoDocumentacionPrestacion.ToList());
    //            //gvTiposDocumentacion.DataBind();

    //        }
    //    }
    //}



    #region Eventos de grilla


    protected void gv_Grilla_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gv_Grilla.EditIndex = e.NewEditIndex;
            DataBindGrid();

            //Hacer foco
            GridViewRow row = gv_Grilla.Rows[e.NewEditIndex];
            TextBox tbWebSite = row.FindControl("WebSite") as TextBox;
            tbWebSite.Focus();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            throw ex;
        }
    }

    protected void gv_Grilla_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gv_Grilla.EditIndex = -1;
            DataBindGrid();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            throw ex;
        }
    }


    protected void gv_Grilla_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            UtilsPresentacionX5.SetImagenesOrdenamiento(sender, e, SortExpression, SortDirection);
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            throw ex;
        }
    }

    //protected void gv_Grilla_DataBound(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindDdl();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (log.IsErrorEnabled)
    //            log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
    //        throw ex;
    //    }
    //}

    protected void gv_Grilla_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("Insert"))
            {
                DropDownList ddlFrecuente = gv_Grilla.FooterRow.FindControl("Frecuente") as DropDownList;
                TextBox txtWebSite = gv_Grilla.FooterRow.FindControl("WebSite") as TextBox;
                TextBox txtDescripcion = gv_Grilla.FooterRow.FindControl("Descripcion") as TextBox;

                
                string msjError = "";

                //Alta
                InvocaWsDao.AMBanco(null, ddlFrecuente.SelectedValue.Equals("S") ? true : false, txtDescripcion.Text, txtWebSite.Text , out msjError);

                VariableSession.oTipoDocumentacion_Prestacion = null;

                mensaje.QuienLLama = "btnAlerta";
                mensaje.TipoMensaje = Mensaje.infoMensaje.Alerta;
                mensaje.DescripcionMensaje = "La nueva asignación de documentación se ha cargado correctamente.";
                mensaje.Mostrar();

                LlenaGrilla(string.Empty, string.Empty);

            }
        }
        catch (Exception err)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", err.Message);

            throw err;
        }
    }

    //protected void gv_Grilla_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        IndexGrilla = Convert.ToInt32(e.RowIndex);

    //        mensaje.QuienLLama = "btnEliminar";
    //        mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
    //        mensaje.DescripcionMensaje = "Desea eliminar la asignación de documentación?";
    //        mensaje.TextoBotonAceptar = "Si";
    //        mensaje.TextoBotonCancelar = "No";

    //        mensaje.Mostrar();

    //    }
    //    catch (Exception ex)
    //    {
    //        if (log.IsErrorEnabled)
    //            log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
    //        throw ex;
    //    }
    //}

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

            gv_Grilla.EditIndex = -1;
            gv_Grilla.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            throw ex;
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
            throw ex;
        }
    }

    protected void gv_Grilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv_Grilla.PageIndex = e.NewPageIndex;
            DataBindGrid();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            throw ex;
        }
    }


    protected void gv_Grilla_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataRowView rowView = (DataRowView)e.Row.DataItem;
        //    // Retrieve the state value for the current row. 
        //    HiddenField hfws = (HiddenField)e.Row.Cells[1].FindControl("hfws");
        //    ImageButton btn = (ImageButton)e.Row.Cells[1].FindControl("WebSite");

        //    btn.OnClientClick = "window.open('" + hfws.Value + "','Banco')";
        //}
    }

    protected void gv_Grilla_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            IndexGrilla = UtilsPresentacionX5.CalcularIndiceGrilla(gv_Grilla, e.RowIndex);
            GridViewRow row = gv_Grilla.Rows[e.RowIndex];

            TextBox tbWebSite = row.FindControl("WebSite") as TextBox;

            DropDownList ddlFrecuente = row.FindControl("Frecuente") as DropDownList;

            TextBox tbDescripcion = row.FindControl("Descripcion") as TextBox;

            
            //Modificar
            string mensajeer = "";
            InvocaWsDao.AMBanco(LstDatosBancos[IndexGrilla].Id_Banco, ddlFrecuente.SelectedValue.Equals("S") ? true : false, tbDescripcion.Text, tbWebSite.Text, out mensajeer);

            gv_Grilla.EditIndex = -1;

            //LlenaGrilla(busquedaGrilla.BusquedaSeleccionada(), busquedaGrilla.BusquedaIngresada());
            LlenaGrilla(string.Empty, string.Empty);
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            throw ex;
        }
    }



    #endregion Eventos de grilla

    #region Eventos de Busqueda

    private void FiltrarGrilla(string campoFiltro, string contenidoFiltro)
    {
        switch (campoFiltro)
        {
            case "Banco":
                LstDatosBancos = VariableSession.oBancoTodos;
                break;
            case "Web":
                LstDatosBancos = VariableSession.oBancoTodos;
                break;
            case "Frecuente":
                LstDatosBancos = VariableSession.oBancoTodos;
                break;
            
        }
    }

    //protected void ClickearonBuscar(object sender)
    //{
    //    LlenaGrilla(busquedaGrilla.BusquedaSeleccionada(), busquedaGrilla.BusquedaIngresada());
    //}

    //protected void imgabtnBuscar_Click(object sender, ImageClickEventArgs e)
    //{
    //    LlenaGrilla(busquedaGrilla.BusquedaSeleccionada(), busquedaGrilla.BusquedaIngresada());
    //}

    #endregion Eventos de Busqueda

    #region Metodos Privados

    private void InicializarOrdenamientoGrilla(string sortExpression, SortDirection sortDirection)
    {
        SortExpression = sortExpression;
        SortDirection = sortDirection;
    }

    private void InicializarDatosPagina(string titulo, string txtBarraNav)
    {
        UtilsPresentacionX5.SetTextLabel(LblTituloPagina, titulo);
        BNav.Text = txtBarraNav;
        //UtilsPresentacionX5.SetPaginationProperties(gv_Grilla);
        Page.Title = titulo;
    }

    private void Ordenar()
    {
        bool sortAscending =
            SortDirection == SortDirection.Ascending;
        var list = from u in LstDatosBancos
                   select u;

        switch (SortExpression)
        {
            case "Banco":
                list = sortAscending ? list.OrderBy(u => u.Descripcion) :
                    list.OrderByDescending(u => u.Descripcion);
                break;
            case "Web":
                list = sortAscending ? list.OrderBy(u => u.WebSite) :
                    list.OrderByDescending(u => u.WebSite);
                break;
            case "Frecuente":
                list = sortAscending ? list.OrderBy(u => u.Frecuente) :
                    list.OrderByDescending(u => u.Frecuente);
                break;
            
        }
        LstDatosBancos = list.ToList();
    }

    private void DataBindGrid()
    {
        Ordenar();
        gv_Grilla.DataSource = ToDatatable.toDataTable(LstDatosBancos);
        gv_Grilla.DataBind();
        UtilsPresentacionX5.EmptyGridFix(gv_Grilla, LstDatosBancos, "Ingresar Banco");
    }

    private void ObtenerDatos(string campoFiltro, string contenidoFiltro)
    {
        if (string.IsNullOrEmpty(contenidoFiltro))
        {
            LstDatosBancos = InvocaWsDao.TraerBancosTodos();
        }
        else
        {
            FiltrarGrilla(campoFiltro, contenidoFiltro);
        }
    }

    private void LlenaGrilla(string campoFiltro, string contenidoFiltro)
    {
        try
        {
            ObtenerDatos(campoFiltro, contenidoFiltro);
            gv_Grilla.Visible = true;

            DataBindGrid();

            VisibilidadPaneles(true);
        }
        catch (Exception err)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", err.Message);

            throw err;
        }
    }

    private void VisibilidadPaneles(bool p)
    {
        pnlGrilla.Visible = p;
    }

    //private void EliminarFila()
    //{
    //    try
    //    {
    //        GridViewRow row = gv_Grilla.Rows[IndexGrilla];


    //        //Baja
    //        string msjError = "";
    //        //realizar funcionalidad de baja de banco
    //        //InvocaWsDao.BajaTipodeDocumentacion_Prestacion(LstDatosDocumentacionPrestacion[IndexGrilla].TDocumentacion.CodTipoDocumentacion, LstDatosDocumentacionPrestacion[IndexGrilla].Prestacion.Cod_Prestacion, out msjError);

    //        LlenaGrilla(string.Empty, string.Empty);
    //        //LlenaGrilla(busquedaGrilla.BusquedaSeleccionada(), busquedaGrilla.BusquedaIngresada());
    //    }
    //    catch (Exception ex)
    //    {
    //        if (log.IsErrorEnabled)
    //            log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
    //        throw ex;
    //    }
    //}

    //private void LlenaDdlCamposBusqueda()
    //{
    //    List<string> campos = new List<string>();
    //    campos.Add("Número Linea Completo");
    //    campos.Add("Código Area");
    //    campos.Add("Número Linea");
    //    campos.Add("Concepto");

    //    busquedaGrilla.CargarCamposBusqueda(campos);
    //}

    #endregion Metodos Privados

    #region Carga de DropDownList en la region footer

    //protected void BindDdl()
    //{
    //    try
    //    {
    //        GridViewRow footrow = gv_Grilla.FooterRow;
    //        if (footrow != null)
    //        {
    //            DdlTipoDoc = (DropDownList)footrow.FindControl("Descripcion");
    //            BindDdlDescripcion(DdlTipoDoc);


    //            DdlPrestacion = (DropDownList)footrow.FindControl("Prestacion");
    //            BindDdlPrestacion(DdlPrestacion);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        log.ErrorFormat("BinDdl devolvio el siguiente Error => {0} ", ex.Message);
    //        throw ex;
    //    }
    //}

    //protected void BindDdlDescripcion(DropDownList ddl)
    //{
    //    ddl.DataSource = VariableSession.oTipoDocumentacion;
    //    ddl.DataValueField = "CodTipoDocumentacion";
    //    ddl.DataTextField = "Descripcion";
    //    ddl.DataBind();
    //}

    //protected void BindDdlPrestacion(DropDownList ddl)
    //{
    //    ddl.DataSource = VariableSession.oPrestaciones;
    //    ddl.DataValueField = "Cod_Prestacion";
    //    ddl.DataTextField = "Descripcion";
    //    ddl.DataBind();
    //}


    #endregion Carga de DropDownList en la region footer

    #region Eventos de Mensajes
    protected void ClickearonSi(object sender, string quienLlamo)
    {
        switch (quienLlamo)
        {
            case "btnAlerta":
                break;
            case "btnErrorServicio":
                break;
            case "btnEliminar":
                //EliminarFila();
                break;
        }
        return;
    }

    protected void ClickearonNo(object sender, string quienLlamo)
    {
        return;
    }

    #endregion Eventos de Mensajes


    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Main.aspx", false);
    }

}