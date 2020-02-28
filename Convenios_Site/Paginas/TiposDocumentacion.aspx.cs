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

public partial class Paginas_TiposDocumentacion : System.Web.UI.Page
{
    readonly ILog log = LogManager.GetLogger(typeof(Paginas_TiposDocumentacion));

    #region Propiedades

    protected List<TipoDocumentacion> LstTiposDocumentacion
    {
        get { return (List<TipoDocumentacion>)ViewState["lstDatos"]; }
        set { ViewState["lstDatos"] = value; }
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

    #region Page Load Eventos


    public void Page_PreRender(object sender, EventArgs e)
    {
        //
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        MError.MensajeError = "";
        try
        {
            //Cargo todos los mensajes posibles
            mensaje.ClickSi += new Mensaje.Click_UsuarioSi(ClickearonSi);
            mensaje.ClickNo += new Mensaje.Click_UsuarioNo(ClickearonNo);
            mensajeOk.ClickSi += new Mensaje.Click_UsuarioSi(ClickearonSi);
            mensajeOk.ClickNo += new Mensaje.Click_UsuarioNo(ClickearonNo);

            //busquedaGrilla.ClickBuscar += new comun_controles_BusquedaGrilla.Click_Buscar(ClickearonBuscar);

            if (!IsPostBack)
            {
                if (!AplicarSeguridad())
                {
                    Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"].ToString());
                }

                InicializarDatosPagina("Tipos de documentación", "> Administración > Tipos de documentación");

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

    #region Eventos de grilla

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

    protected void gv_Grilla_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gv_Grilla.EditIndex = e.NewEditIndex;
            DataBindGrid();

            //Hacer foco
            GridViewRow row = gv_Grilla.Rows[e.NewEditIndex];
            TextBox tbDescripcion = row.FindControl("Descripcion") as TextBox;
            tbDescripcion.Focus();
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

    protected void gv_Grilla_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            IndexGrilla = UtilsPresentacionX5.CalcularIndiceGrilla(gv_Grilla, e.RowIndex);
            GridViewRow row = gv_Grilla.Rows[e.RowIndex];

            TextBox tbDescripcion = row.FindControl("Descripcion") as TextBox;
            //TextBox tbCodArea = row.FindControl("cod_area_NPA") as TextBox;

            //TextBox tbNroLinea = row.FindControl("nro_linea") as TextBox;

            //TextBox tbFechaBaja = row.FindControl("fech_baja") as TextBox;

            //TextBox tbTipoLinea = row.FindControl("tipo_linea") as TextBox;

            //LstDatos[IndexGrilla].tipo_linea = tbTipoLinea.Text;

            //TextBox tbDelegacion = row.FindControl("delegacion") as TextBox;

            //LstDatos[IndexGrilla].delegacion = tbDelegacion.Text;

            //TextBox tbTipo = row.FindControl("tipo") as TextBox;

            //LstDatos[IndexGrilla].tipo = tbTipo.Text;


            //TextBox tbDomicilio = row.FindControl("domicilio") as TextBox;

            //LstDatos[IndexGrilla].domicilio = tbDomicilio.Text;

            //TextBox tbLocalidad = row.FindControl("localidad") as TextBox;

            //LstDatos[IndexGrilla].localidad = tbLocalidad.Text;

            //DateTime fecha;

            //LstDatos[IndexGrilla].fech_baja = null;
            //if (DateTime.TryParse(tbFechaBaja.Text, out fecha))
            //{
            //    LstDatos[IndexGrilla].fech_baja = Convert.ToDateTime(tbFechaBaja.Text);
            //}

            //Modificar
            string mensajeer = "";
            InvocaWsDao.AMTiposdeDocumentacion(LstTiposDocumentacion[IndexGrilla].CodTipoDocumentacion, tbDescripcion.Text, out mensajeer);

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


    protected void gv_Grilla_DataBound(object sender, EventArgs e)
    {
        try
        {
            //BindDdl();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            throw ex;
        }
    }

    protected void gv_Grilla_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("Insert"))
            {
                TextBox tbDescripcion = gv_Grilla.FooterRow.FindControl("Descripcion") as TextBox;
                
                TipoDocumentacion tDoc = new TipoDocumentacion();

                tDoc.Descripcion = tbDescripcion.Text;
                string mensajeer = "";

                InvocaWsDao.AMTiposdeDocumentacion(null, tDoc.Descripcion, out mensajeer);

                mensaje.QuienLLama = "btnAlerta";
                mensaje.TipoMensaje = Mensaje.infoMensaje.Alerta;
                mensaje.DescripcionMensaje = "La nueva linea se ha cargado correctamente.";
                mensaje.Mostrar();

                LstTiposDocumentacion.Add(tDoc);
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


    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Main.aspx", false);
    }

    protected void gv_Grilla_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            IndexGrilla = Convert.ToInt32(e.RowIndex);

            mensaje.QuienLLama = "btnEliminar";
            mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
            mensaje.DescripcionMensaje = "Desea eliminar la linea?";
            mensaje.TextoBotonAceptar = "Si";
            mensaje.TextoBotonCancelar = "No";

            mensaje.Mostrar();

        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            throw ex;
        }
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

    #endregion Eventos de grilla

    #region Eventos de Busqueda

    private void FiltrarGrilla(string campoFiltro, string contenidoFiltro)
    {
        switch (campoFiltro)
        {
            case "Descripcion":
                LstTiposDocumentacion = InvocaWsDao.TraerTipoDocumentacion();
                break;
        }
    }

    //protected void ClickearonBuscar(object sender)
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
        var list = from u in LstTiposDocumentacion
                   select u;

        switch (SortExpression)
        {
            case "Descripcion":
                list = sortAscending ? list.OrderBy(u => u.Descripcion) :
                    list.OrderByDescending(u => u.Descripcion);
                break;
        }
        LstTiposDocumentacion = list.ToList();
    }


    private void DataBindGrid()
    {
        Ordenar();
        gv_Grilla.DataSource = LstTiposDocumentacion;
        gv_Grilla.DataBind();
        UtilsPresentacionX5.EmptyGridFix(gv_Grilla, LstTiposDocumentacion, "Ingresar documentación");
    }

    private void ObtenerDatos(string campoFiltro, string contenidoFiltro)
    {
        if (string.IsNullOrEmpty(contenidoFiltro))
        {
            LstTiposDocumentacion = InvocaWsDao.TraerTipoDocumentacion();
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

    //        Label lblDescripcion = row.FindControl("Descripcion") as Label;
            
    //        TipoDocumentacion tDoc = new TipoDocumentacion();

    //        tDoc.Descripcion = lblDescripcion.Text;
            
    //        //Baja
    //        InvocaWsDao.bo.BajaLineaAnses(lineaAnses);

    //        LlenaGrilla(busquedaGrilla.BusquedaSeleccionada(), busquedaGrilla.BusquedaIngresada());
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
    //    campos.Add("Tipo Linea");
    //    campos.Add("Delegación");
    //    campos.Add("Domicilio");
    //    campos.Add("Localidad");

    //    busquedaGrilla.CargarCamposBusqueda(campos);
    //}

    #endregion Metodos Privados

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

}
