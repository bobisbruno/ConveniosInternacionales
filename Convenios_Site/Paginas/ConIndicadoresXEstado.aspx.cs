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

public partial class Paginas_ConIndicadoresXEstado : System.Web.UI.Page
{
    #region Propiedades / Variables publicas

    private static readonly ILog log = LogManager.GetLogger(typeof(Paginas_ConIndicadoresXEstado).Name);

    #endregion

    #region Tomo seteo periodos de la consulta

    public DataTable dtPeriodos
    {
        get
        {
            return Session["_periodo"] == null ? null : (DataTable)Session["_periodo"];
            //return (string)Session["acceso"];
        }
        set
        {
            Session["_periodo"] = value;
        }
    }
    #endregion

    #region

    public DataTable dtFinalToGrid
    {
        get
        {
            return Session["_dtFinalToGrid"] == null ? null : (DataTable)Session["_dtFinalToGrid"];
            //return (string)Session["acceso"];
        }
        set
        {
            Session["_dtFinalToGrid"] = value;
        }
    }
    #endregion


    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }


    public List<IndicadorTotalesEstado> sesToExporte
    {
        get { return (List<IndicadorTotalesEstado>)Session["_solicToExport"]; }
        set { Session["_solicToExport"] = value; }
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
        Response.Redirect("../Servicios/Mantenimiento.htm");
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnConsultar);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnImprimir);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnToExcell);

        rvtxtFecha.MaximumValue = DateTime.Today.ToString("dd/MM/yyyy");
        rvtxtFecha.MinimumValue = DateTime.Today.AddYears(-50).ToString("dd/MM/yyyy");

        if (!IsPostBack)
        {
            if (!AplicarSeguridadPagina())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"]);

            txtFecha.Text = System.DateTime.Today.ToShortDateString();
            sesToExporte = null;
            dvDatosConsulta.Visible = false;
            dvNODatosConsulta.Visible = false;

            Consultar();
        }

    }

    private DataTable toDataTable(List<IndicadorTotalesEstado> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("Destado", typeof(String));
        _dt.Columns.Add("PorcentualEstado", typeof(String));
        _dt.Columns.Add("TotalEstado", typeof(String));


        foreach (IndicadorTotalesEstado oSol in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Destado"] = oSol.Destado;
            _drTemp["PorcentualEstado"] = oSol.PorcentualEstado;
            _drTemp["TotalEstado"] = oSol.TotalEstado;

            _dt.Rows.Add(_drTemp);
        }



        return _dt;
    }

    protected void btnToExcell_Click(object sender, EventArgs e)
    {
        try
        {
            string strBody = "";
            strBody = Exportar.DataTable2ExcelString(toDataTable((List<IndicadorTotalesEstado>)sesToExporte));

            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-disposition", "attachment; filename=" + hfcaption.Value + ".xls");
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

    private void Consultar()
    {
        sesToExporte = null;
        string mensaje = string.Empty;
        string caption = "Totales por estados a la fecha " + txtFecha.Text;
        hffechaing.Value = txtFecha.Text;

        List<IndicadorTotalesEstado> oList = InvocaWsDao.TraeIndicadorTotalesEstado(txtFecha.Text, out mensaje);
        sesToExporte = oList;
        MError.MensajeError = mensaje;
        if ((oList == null) || (oList.Count == 0))
        {
            dvNODatosConsulta.Visible = true;
            dvDatosConsulta.Visible = false;
            btnImprimir.Enabled = false;
            btnToExcell.Enabled = false;
        }
        else
        {
            btnImprimir.Enabled = true;
            btnToExcell.Enabled = true;
            hfcaption.Value = caption;
            dvDatosConsulta.Visible = true;
            gridListadoSolicitudes.DataSource = toDataTable(oList);
            gridListadoSolicitudes.DataBind();
            //litGraficoBarras.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.BARRA,
            //    caption,
            //    "Estado", "Total",
            //    "Estado", toDataTable(oList), 0, 2, 600, 300, false);
            //litGraficoTorta.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.TORTA,
            //caption,
            //"Estado", "Total",
            //"EstadoT", toDataTable(oList), 0, 2, 500, 250,false);
            dvNODatosConsulta.Visible = false;

            #region Datos Serie Lineal

            hfcaption2.Value = "Evolución de estados en los ultimos " + ConfigurationManager.AppSettings["PeriodosConsultaSeries"].ToString() + " períodos a fecha " + txtFecha.Text;
            lbTituloLineal.Text = hfcaption2.Value;

            SettingDatosCons(txtFecha.Text);
            TraerGridEvolucion();
            TraerGraficaMultiSerie();

            #endregion Datos Serie Lineal
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Consultar();
        }
    }


    private void TraerGraficaMultiSerie()
    {
        if (dtFinalToGrid != null)
        {
            litGraficoLinea.Visible = true;
            //litGraficoLinea.Text = Grafica.traerScriptGraficaMultiSerieLinea(hfcaption2.Value, "En totales por estado / período", "Período", "Total", "", "DivLineal", dtFinalToGrid, dtPeriodos, 900, 350, false);
        }
        else
        {
            //Mensaje.Text = "Ocurrio un error al consultar los datos";
            litGraficoLinea.Visible = false;
        }

    }

    private void SettingDatosCons(string fechaIng)
    {
        DataTable _dtPeriodos = new DataTable();
        _dtPeriodos.Columns.Add("Periodo", typeof(String));

        #region Settings
        DateTime periodobase = System.DateTime.Parse(fechaIng).AddMonths(-10);
        int cantperiodos = int.Parse(ConfigurationManager.AppSettings["PeriodosConsultaSeries"]);
        gridEvolutivo.Columns.Clear();
        BoundField columna1 = new BoundField();
        columna1.HeaderText = "Estado";
        columna1.DataField = "Destado";
        columna1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        columna1.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        gridEvolutivo.Columns.Add(columna1);

        for (int i = 1; i < cantperiodos + 1; i++)
        {
            DateTime header = periodobase.AddMonths(i);

            //Datatable de anios
            DataRow _drTemp;
            _drTemp = _dtPeriodos.NewRow();
            _drTemp["Periodo"] = header.ToShortDateString();
            _dtPeriodos.Rows.Add(_drTemp);
            //Datatable de periodos


            BoundField columna = new BoundField();
            columna.HeaderText = header.ToShortDateString();
            //columna.DataField = "TotalXTipoInstrumento";
            columna.DataField = header.ToShortDateString();
            columna.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            columna.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            //columna.HeaderStyle.Width = 15;
            //columna.HeaderStyle = 
            gridEvolutivo.Columns.Add(columna);

        }
        gridEvolutivo.DataBind();
        dtPeriodos = _dtPeriodos;
        #endregion Settings
    }

    private DataTable creaDataTable()
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("Destado", typeof(String));
        foreach (DataRow dRowPeriodo in dtPeriodos.Rows)
        {
            _dt.Columns.Add(dRowPeriodo.ItemArray[0].ToString(), typeof(String));
        }
        return _dt;

    }

    private void TraerGridEvolucion()
    {
        //DataTable dtFinal = new DataTable();
        dtFinalToGrid = creaDataTable();
        //carga la primera columna
        CargaEstados(VariableSession.oEstados);
        foreach (DataRow dRowAnio in dtPeriodos.Rows)
        {
            string mensaje="";
            //traer datos del anio ingresado y carga las sucesivas columnas por anio
            List<IndicadorTotalesEstado> oList = InvocaWsDao.TraeIndicadorTotalesEstado(dRowAnio.ItemArray[0].ToString().Trim(), out mensaje);
            //dtFinal = toDataTable(oList);
            CargaColumnas(oList, dtPeriodos.Rows.IndexOf(dRowAnio) + 1);
        }

        //gridEvolutivo.DataSource = (DataTable)CalcularFilaTotales(dtFinalToGrid);
        gridEvolutivo.DataSource = dtFinalToGrid;
        gridEvolutivo.DataBind();
        //gridEvolutivo.Rows[gridEvolutivo.Rows.Count - 1].CssClass = "GrillaHeadPanel";
        gridEvolutivo.Visible = true;
    }

    private void CargaEstados(List<AuxiliaresWS.Estado> iParam)
    {
        DataTable _dt = (DataTable)dtFinalToGrid;

        //_dt.Columns.Add("NombreTX", typeof(String));


        foreach (AuxiliaresWS.Estado oCot in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Destado"] = oCot.Descripcion;
            _dt.Rows.Add(_drTemp);
        }

        dtFinalToGrid = _dt;
        //return _dt;
    }

    private void CargaColumnas(List<IndicadorTotalesEstado> iParam, int columna)
    {
        //DataTable _dt = new DataTable();
        DataTable _dtGrid = (DataTable)dtFinalToGrid;

        foreach (DataRow dr in _dtGrid.Rows)
        {
            bool coincide = false;

            foreach (IndicadorTotalesEstado oCot in iParam)
            {
                if (dr.ItemArray[0].ToString().ToUpper().Equals(oCot.Destado.ToUpper()))
                {
                    dr[columna] = oCot.TotalEstado;
                    coincide = true;
                }
            }
            if (!coincide)
                dr[columna] = "0";
        }
        dtFinalToGrid = _dtGrid;
    }

}
