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
using System.Threading;
using ConsultasWS;
using log4net;

public partial class Paginas_ConsultaTProvisorios : System.Web.UI.Page
{
    #region Propiedades / Variables publicas

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


    private string nroTramite
    {
        get { return (string)ViewState["_nroTramite"]; }
        set { ViewState["_nroTramite"] = value; }
    }

    //private string codPrestacion
    //{
    //    get { return (string)ViewState["_codPrestacion"]; }
    //    set { ViewState["_codPrestacion"] = value; }
    //}

    //private string codPais
    //{
    //    get { return (string)ViewState["_codPais"]; }
    //    set { ViewState["_codPais"] = value; }
    //}

    //private string idBeneficiario
    //{
    //    get { return (string)ViewState["_idBen"]; }
    //    set { ViewState["_idBen"] = value; }
    //}

    //private string ApenomBeneficiario
    //{
    //    get { return (string)ViewState["_APNB"]; }
    //    set { ViewState["_APNB"] = value; }
    //}

    //private string FIngSol
    //{
    //    get { return (string)ViewState["_fis"]; }
    //    set { ViewState["_fis"] = value; }
    //}

    //private string Cuip
    //{
    //    get { return (string)ViewState["_cuip"]; }
    //    set { ViewState["_cuip"] = value; }
    //}

    //private string RefExt
    //{
    //    get { return (string)ViewState["_re"]; }
    //    set { ViewState["_re"] = value; }
    //}

    //private string UbicFisica
    //{
    //    get { return (string)ViewState["_uf"]; }
    //    set { ViewState["_uf"] = value; }
    //}
    //private string descPrestacion
    //{
    //    get { return (string)ViewState["_descPrestacion"]; }
    //    set { ViewState["_descPrestacion"] = value; }
    //}

    //private string descPais
    //{
    //    get { return (string)ViewState["_dPais"]; }
    //    set { ViewState["_dPais"] = value; }
    //}


    private static readonly ILog log = LogManager.GetLogger(typeof(Paginas_ConsultaTProvisorios).Name);

    #endregion

    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }


    public List<ActoresWS.SolicitudProvisoria> sesToExporte
    {
        get { return (List<ActoresWS.SolicitudProvisoria>)Session["_solicToExport"]; }
        set { Session["_solicToExport"] = value; }
    }

    public List<ActoresWS.SolicitudProvisoriaMovimiento> sesToExporteMovimientos
    {
        get { return (List<ActoresWS.SolicitudProvisoriaMovimiento>)Session["_solicToExportMov"]; }
        set { Session["_solicToExportMov"] = value; }
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
    #endregion APlica Seguridad Pagina

    protected void Page_Load(object sender, EventArgs e)
    {
        //rvtxtFechaDesde.MaximumValue = DateTime.Today.AddYears(10).ToString("dd/MM/yyyy");
        //rvtxtFechaDesde.MinimumValue = DateTime.Today.AddYears(-50).ToString("dd/MM/yyyy");
        //rvtxtFechaHasta.MaximumValue = DateTime.Today.AddYears(10).ToString("dd/MM/yyyy");
        //rvtxtFechaHasta.MinimumValue = DateTime.Today.AddYears(-50).ToString("dd/MM/yyyy");
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnToExcell);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(gridListadoSolicitudes);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(gvMovimientos);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnCerrar);


        if (!IsPostBack)
        {
            nroTramite = string.Empty;
            txtAnio.Text = System.DateTime.Today.Year.ToString();
            ddlMeses.ClearSelection();
            ddlMeses.Items.FindByValue(System.DateTime.Today.Month.ToString()).Selected = true;
            sesToExporte = null;
            dvDatosConsulta.Visible = false;
            dvNODatosConsulta.Visible = false;
            #region Seguridad
            if (!AplicarSeguridadPagina())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"]);
            #endregion Seguridad
            InicializarDatosPagina("Consulta de trámites provisorios", "> Consultas > Trámites provisorios");
            InicializarOrdenamientoGrilla("ApellidoyNombre", SortDirection.Ascending);

            btnImprimir.Enabled = false;
            btnToExcell.Enabled = false;

            CargarCombos();

        }

    }

    private bool CargarCombos()
    {
        string mensajeCarga = string.Empty;

        try
        {
            ////Combo Paises
            List<PaisWS.Pais> oPaisConvenios = VariableSession.oPaisConvenios;

            if (oPaisConvenios != null)
            {
                //De solicitud solo carga los paises con convenio
                ddlPaisConvenio.DataSource = oPaisConvenios;
                ddlPaisConvenio.DataBind();
                ddlPaisConvenio.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
                mensajeCarga += "Paises" + "</br>";

            List<AuxiliaresWS.Prestacion> oPrestacion = VariableSession.oPrestaciones;

            if (oPrestacion != null)
            {
                ddlPrestacionesS.DataSource = oPrestacion;
                ddlPrestacionesS.DataBind();
                ddlPrestacionesS.Items.Insert(0, new ListItem("Todas", "0"));

            }
            else
                mensajeCarga += "Prestaciones" + "</br>";

            if (!mensajeCarga.Equals(string.Empty))
            {
                MError.MensajeError = "Las siguientes listas no pudieron cargarse:" + "</br>" + mensajeCarga;
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            log.Error("Ocurrió un error al intentar traer una o mas Listas. " + ex.Message);
            MError.MensajeError = "Ocurrió un error al intentar traer una o mas Listas. Intente mas tarde.";
            return false;
        }

    }


    private void InicializarDatosPagina(string titulo, string txtBarraNav)
    {
        UtilsPresentacionX5.SetTextLabel(LblTituloPagina, titulo);
        BNav.Text = txtBarraNav;
        //UtilsPresentacionX5.SetPaginationProperties(gv_Grilla);
        Page.Title = titulo;
    }

    protected void btnToExcell_Click(object sender, EventArgs e)
    {
        try
        {
            string strBody = "";
            strBody = Exportar.DataTable2ExcelString(ToDatatable.toDataTable((List<ActoresWS.SolicitudProvisoria>)sesToExporte));
            string provisorio = chksoloProvisorios.Checked ? "Provisorios" : "";
            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-disposition", "attachment; filename=Listado_Solicitudes_" + txtAnio.Text + "-" + ddlMeses.SelectedValue.ToString() + "-" + provisorio + ".xls");
            Response.Write(strBody);
        }
        catch (Exception er)
        {
            log.Error("Ocurrio un error al generar el listado de exportacion." + er.Message);
            MError.MensajeError = "Ocurrio un error al generar el listado de exportacion.";
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Main.aspx", false);
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //armo encabezado de la consulta
            string chequeado = chksoloProvisorios.Checked ? "(No verificados)" : "(Todos)";
            string pais = ddlPaisConvenio.SelectedValue != "0" ? " - " + ddlPaisConvenio.SelectedItem.Text : "";
            string prestacion = ddlPrestacionesS.SelectedValue != "0" ? " - " + ddlPrestacionesS.SelectedItem.Text : "";
            hfEncabezado.Value = "Trámites provisorios: " + ddlMeses.SelectedItem.Text + "-" + txtAnio.Text + "   " +  chequeado  + "  " + pais + prestacion ;
            lbTituloConsulta.Text = hfEncabezado.Value;
            sesToExporte = null;
            string mensaje = "";
            Int16? codPrestacion, codPais;
            if (ddlPrestacionesS.SelectedItem.Value.Equals("0"))
                codPrestacion = null;
            else
                codPrestacion = Int16.Parse(ddlPrestacionesS.SelectedItem.Value);

            if (ddlPaisConvenio.SelectedItem.Value.Equals("0"))
                codPais = null;
            else
                codPais = Int16.Parse(ddlPaisConvenio.SelectedItem.Value);
            List<ActoresWS.SolicitudProvisoria> oList = InvocaWsDao.TraeSolicitudesProvisorias(txtAnio.Text, ddlMeses.SelectedValue,  codPais, codPrestacion,   chksoloProvisorios.Checked, out mensaje);
            
            MError.MensajeError = mensaje;
            if ((oList == null) || (oList.Count == 0))
            {
                dvNODatosConsulta.Visible = true;
                dvDatosConsulta.Visible = false;
                hfEncabezado.Value = "";
                btnImprimir.Enabled = false;
                btnToExcell.Enabled = false;
            }
            else
            {
                sesToExporte = oList;
                LlenaGrilla(string.Empty, string.Empty);
                dvDatosConsulta.Visible = true;
                lbElementosEncontrados.Text = oList.Count.ToString();
                
                dvNODatosConsulta.Visible = false;
                btnImprimir.Enabled = true;
                btnToExcell.Enabled = true;
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
        var list = from u in sesToExporte
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
                
        }
        sesToExporte = list.ToList();
    }


    private void DataBindGrid()
    {
        Ordenar();
        gridListadoSolicitudes.DataSource = ToDatatable.toDataTable(sesToExporte);
        gridListadoSolicitudes.DataBind();

        //UtilsPresentacion.EmptyGridFix(gridListadoSolicitudes, sesToExporte, "No hay datos para la consulta");
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


    protected void btnCerrarMovimientos_Click(object sender, EventArgs e)
    {
        gridListadoSolicitudes.Focus();
        mpeShowDocumentos.Hide();
        Master.MenuVisible = true;
    }


    protected void RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String script = "";
        if (e.CommandName == "ImprimirComprobante")
        {
            string[] arg = new string[1];
            arg = e.CommandArgument.ToString().Split(';');
            nroTramite = arg[0];
            script = "<script type='text/javascript'>" + "hidden = open('../Impresiones/ComprobanteTramite.aspx?nroSolicitud=" + nroTramite +"');" + "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", script, false);
        }



        if (e.CommandName == "Verdocumentos")
        {
            // Se obtiene indice de la row seleccionada
            int index = Convert.ToInt32(e.CommandArgument);

            sesToExporteMovimientos =sesToExporte[index].LMovimientos.ToList();

            List<ActoresWS.SolicitudProvisoriaMovimiento> iLista = sesToExporteMovimientos;

            if (iLista != null)
            {
                lbTrProvisorioEncab.Text = sesToExporte[index].Nro_SolicitudProvisoria;
                Master.MenuVisible = false;
                mpeShowDocumentos.Show();
                gvMovimientos.DataSource = ToDatatable.toDataTable(iLista);
                gvMovimientos.DataBind();
            }
        }


        if (e.CommandName == "VerDigitalizado")
        {
            string rutaTotalArchivo = string.Empty;
            Session["ruta"] = string.Empty;

            int  IndexGrilla = Int32.Parse(e.CommandArgument.ToString());
            string codigoExterno = sesToExporteMovimientos[IndexGrilla].Nro_SolicitudProvisoria + sesToExporteMovimientos[IndexGrilla].TipoDocumentacion.CodTipoDocumentacion.ToString() + sesToExporteMovimientos[IndexGrilla].SecuenciaDocumento.ToString();
            InvocaWSExternos.ObtenerRutaArchivo(codigoExterno, ConfigurationManager.AppSettings["Sistema"], out rutaTotalArchivo);
            if (!string.IsNullOrWhiteSpace(rutaTotalArchivo))
            {
                if (File.Exists(rutaTotalArchivo))
                {
                    Session["ruta"] = rutaTotalArchivo;
                    String scriptDoc = "";
                    scriptDoc = "<script type='text/javascript'>" + "hidden = open('ViewDocument.aspx?dn=" + sesToExporteMovimientos[IndexGrilla].Nro_SolicitudProvisoria + " - " + sesToExporteMovimientos[IndexGrilla].TipoDocumentacion.Descripcion + "');" + "</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", scriptDoc, false);
                    mpeShowDocumentos.Show();
                }
            }

            MError.MensajeError = "";
        }
        

    }


    //protected void gridListadoSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView rowView = (DataRowView)e.Row.DataItem;
    //        // Retrieve the state value for the current row. 
    //        Image img = (Image)e.Row.Cells[3].FindControl("imgMercosur");
    //        img.Visible = (Boolean)rowView["Mercosur"];
    //    }

    //}


    protected void dgMovimientos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                // Retrieve the state value for the current row. 

                ImageButton btnVerDocs = (ImageButton)e.Row.Cells[5].FindControl("btnVerDocs");

                btnVerDocs.Visible = e.Row.Cells[2].Text.ToUpper().Equals("SI");
            }
            catch (Exception er)
            {
                if (log.IsErrorEnabled)
                    log.ErrorFormat("Error : {0}", er.Message);

                if (!er.InnerException.GetType().Name.Equals("NullReferenceException"))
                    MError.MensajeError = "Error de ejecución, revisar log para obtener detalles.";
            }
        }
    }

}
