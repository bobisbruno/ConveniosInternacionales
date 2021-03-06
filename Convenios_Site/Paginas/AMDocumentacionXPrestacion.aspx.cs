﻿using System;
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

public partial class Paginas_TiposDocumentacionXPrestacion : System.Web.UI.Page
{
    readonly ILog log = LogManager.GetLogger(typeof(Paginas_TiposDocumentacionXPrestacion));

    #region Propiedades

    
    protected List<AuxiliaresWS.TipoDocumentacion_Prestacion> LstDatosDocumentacionPrestacion
    {
        get { return (List<AuxiliaresWS.TipoDocumentacion_Prestacion>)ViewState["lstDatosTD"]; }
        set { ViewState["lstDatosTD"] = value; }
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

    private DropDownList DdlTipoDoc
    {
        get { return (DropDownList)Session["ddlTdoc"]; }
        set { Session["ddlTdoc"] = value; }
    }

    private DropDownList DdlPrestacion
    {
        get { return (DropDownList)Session["ddlPres"]; }
        set { Session["ddlPres"] = value; }
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

    protected void Page_Load(object sender, EventArgs e)
    {
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
                if (!AplicarSeguridadPagina())
                {
                    Response.Redirect("~/" + ConfigurationManager.AppSettings["urlAccesoDenegado"].ToString());
                }

                InicializarDatosPagina("Asignación de documentación por prestaciónes", "> Administración > Relación documentación / prestación");

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
            TextBox tbComentario = row.FindControl("Comentario") as TextBox;
            tbComentario.Focus();
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

    protected void gv_Grilla_DataBound(object sender, EventArgs e)
    {
        try
        {
            BindDdl();
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
                DropDownList ddlDescripcion = gv_Grilla.FooterRow.FindControl("Descripcion") as DropDownList;
                DropDownList ddlPrestacion = gv_Grilla.FooterRow.FindControl("Prestacion") as DropDownList;
                DropDownList ddlRequeridoInicioTramite = gv_Grilla.FooterRow.FindControl("RequeridoInicioTramite") as DropDownList;
                TextBox txtComentario = gv_Grilla.FooterRow.FindControl("Comentario") as TextBox;

                //Label lbComentario = gv_Grilla.FooterRow.FindControl("Comentario") as Label;

                string msjError = "";
                
                //Alta
                InvocaWsDao.AMTipodeDocumentacion_Prestacion(int.Parse( ddlDescripcion.SelectedValue), short.Parse(ddlPrestacion.SelectedValue) ,txtComentario.Text, ddlRequeridoInicioTramite.SelectedValue.Equals("S") ? true : false, out msjError);

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

    protected void gv_Grilla_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            IndexGrilla = Convert.ToInt32(e.RowIndex);

            mensaje.QuienLLama = "btnEliminar";
            mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
            mensaje.DescripcionMensaje = "Desea eliminar la asignación de documentación?";
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


    protected void gv_Grilla_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            IndexGrilla = UtilsPresentacionX5.CalcularIndiceGrilla(gv_Grilla, e.RowIndex);
            GridViewRow row = gv_Grilla.Rows[e.RowIndex];

            TextBox tbComentario = row.FindControl("Comentario") as TextBox;

            DropDownList ddlRequeridoInicioTramite = row.FindControl("RequeridoInicioTramite") as DropDownList;

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
            InvocaWsDao.AMTipodeDocumentacion_Prestacion(LstDatosDocumentacionPrestacion[IndexGrilla].TDocumentacion.CodTipoDocumentacion, LstDatosDocumentacionPrestacion[IndexGrilla].Prestacion.Cod_Prestacion, tbComentario.Text, ddlRequeridoInicioTramite.SelectedValue.Equals("S") ? true : false , out mensajeer);

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
            case "Prestacion":
                LstDatosDocumentacionPrestacion = VariableSession.oTipoDocumentacion_Prestacion;
                break;
            case "DescripcionTD":
                LstDatosDocumentacionPrestacion = VariableSession.oTipoDocumentacion_Prestacion;
                break;
            case "Comentario":
                LstDatosDocumentacionPrestacion = VariableSession.oTipoDocumentacion_Prestacion;
                break;
            case "RequeridoInicioTramite":
                LstDatosDocumentacionPrestacion = VariableSession.oTipoDocumentacion_Prestacion;
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
        var list = from u in LstDatosDocumentacionPrestacion
                    select u;

        switch (SortExpression)
        {
            case "RequeridoInicioTramite":
                list = sortAscending ? list.OrderBy(u => u.RequeridoInicioTramite) :
                    list.OrderByDescending(u => u.RequeridoInicioTramite);
                break;
            case "Comentario":
                list = sortAscending ? list.OrderBy(u => u.Comentario) :
                    list.OrderByDescending(u => u.Comentario);
                break;
            case "DescripcionTD":
                list = sortAscending ? list.OrderBy(u => u.TDocumentacion.Descripcion) :
                    list.OrderByDescending(u => u.TDocumentacion.Descripcion);
                break;
            case "Prestacion":
                list = sortAscending ? list.OrderBy(u => u.Prestacion.Descripcion) :
                    list.OrderByDescending(u => u.Prestacion.Descripcion);
                break;
        }
        LstDatosDocumentacionPrestacion = list.ToList();
    }

    private void DataBindGrid()
    {
        Ordenar();
        gv_Grilla.DataSource = ToDatatable.toDataTable(LstDatosDocumentacionPrestacion);
        gv_Grilla.DataBind();
        UtilsPresentacionX5.EmptyGridFix(gv_Grilla, LstDatosDocumentacionPrestacion, "Ingresar Documentación");
    }

    private void ObtenerDatos(string campoFiltro, string contenidoFiltro)
    {
        if (string.IsNullOrEmpty(contenidoFiltro))
        {
            LstDatosDocumentacionPrestacion = InvocaWsDao.TraeTodoTipoDocumentacionPrestacion();
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

    private void EliminarFila()
    {
        try
        {
            GridViewRow row = gv_Grilla.Rows[IndexGrilla];
            

            //Label lblCArea = row.FindControl("cod_area_NPA") as Label;
            //Label lblNLinea = row.FindControl("nro_linea") as Label;

            //Label lblConc = row.FindControl("desc_concepto") as Label;
            //Label lblCReg = row.FindControl("cod_registro") as Label;

            //ServicioYT.YT_Linea_Anses_YT_Concepto lineaAnsesConcepto = new ServicioYT.YT_Linea_Anses_YT_Concepto();

            //lineaAnsesConcepto.cod_area_NPA = lblCArea.Text;
            //lineaAnsesConcepto.nro_linea = lblNLinea.Text;
            //lineaAnsesConcepto.cod_servicio = "Telefonia Basica";//lblServ.Text;
            //lineaAnsesConcepto.desc_concepto = lblConc.Text;
            //lineaAnsesConcepto.cod_registro = Convert.ToInt32(lblCReg.Text);

            //Baja
            string msjError = "";
            InvocaWsDao.BajaTipodeDocumentacion_Prestacion(LstDatosDocumentacionPrestacion[IndexGrilla].TDocumentacion.CodTipoDocumentacion, LstDatosDocumentacionPrestacion[IndexGrilla].Prestacion.Cod_Prestacion, out msjError);

            LlenaGrilla(string.Empty, string.Empty);
            //LlenaGrilla(busquedaGrilla.BusquedaSeleccionada(), busquedaGrilla.BusquedaIngresada());
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            throw ex;
        }
    }

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

    protected void BindDdl()
    {
        try
        {
            GridViewRow footrow = gv_Grilla.FooterRow;
            if (footrow != null)
            {
                DdlTipoDoc = (DropDownList)footrow.FindControl("Descripcion");
                BindDdlDescripcion(DdlTipoDoc);


                DdlPrestacion = (DropDownList)footrow.FindControl("Prestacion");
                BindDdlPrestacion(DdlPrestacion);
                
            }
        }
        catch (Exception ex)
        {
            log.ErrorFormat("BinDdl devolvio el siguiente Error => {0} ", ex.Message);
            throw ex;
        }
    }

    protected void BindDdlDescripcion(DropDownList ddl)
    {
        ddl.DataSource = VariableSession.oTipoDocumentacion;
        ddl.DataValueField = "CodTipoDocumentacion";
        ddl.DataTextField = "Descripcion";
        ddl.DataBind();
    }

    protected void BindDdlPrestacion(DropDownList ddl)
    {
        ddl.DataSource = VariableSession.oPrestaciones;
        ddl.DataValueField = "Cod_Prestacion";
        ddl.DataTextField = "Descripcion";
        ddl.DataBind();
    }


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
                EliminarFila();
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