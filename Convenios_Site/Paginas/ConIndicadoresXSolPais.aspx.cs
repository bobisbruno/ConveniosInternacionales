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

public partial class Paginas_ConIndicadoresXSolPais : System.Web.UI.Page
{
    #region Propiedades / Variables publicas

    public string txtBarNav
    {
        get
        {
            return (String)ViewState["bn"];
        }
        set
        { ViewState["bn"] = value; }

    }

    private static readonly ILog log = LogManager.GetLogger(typeof(Paginas_ConIndicadoresXSolPais).Name);

    #endregion

    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }


    public List<IndicadorPorSolicitudesPaisConvenio> sesToExporte
    {
        get { return (List<IndicadorPorSolicitudesPaisConvenio>)Session["_solicToExport"]; }
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

        botonesGraficos.clColumna += new Comun_Controles_BotonesGraficos.Click_Columna(ClickearonBotonGrafico);
        botonesGraficos.clSegmento += new Comun_Controles_BotonesGraficos.Click_Segmento(ClickearonBotonGrafico);
        botonesGraficos.clTorta += new Comun_Controles_BotonesGraficos.Click_Torta(ClickearonBotonGrafico);
        botonesGraficos.clFunnel += new Comun_Controles_BotonesGraficos.Click_Funnel(ClickearonBotonGrafico);

        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnConsultar);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnImprimir);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnToExcell);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(botonesGraficos);
        ddlTPeriodo.Attributes.Add("onChange", "Seleccionar(this);");

        if (!IsPostBack)
        {
            sesToExporte = null;
            dvDatosConsulta.Visible = false;
            dvNODatosConsulta.Visible = false;
            if (!AplicarSeguridadPagina())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"]);
            txtBarNav = " > Indicadores > Paises";
            InicializarDatosPagina("Indicadores por País");
            setBarraNav(txtBarNav + " > " + System.DateTime.Today.Year);
            //muestra y oculta botones graficos
            botonesGraficos.SetControl(true, false, false, false, true, true);
            ConsultarIndicadores(0, 0, System.DateTime.Now.Year.ToString(), "");
        }

    }


    #region Click Boton Gráfico
    protected void ClickearonBotonGrafico(object sender, TiposEnumerados.TipoGrafico iTipoGrafico)
    {
        SetGraficos(iTipoGrafico);
    }
    #endregion

    private void setBarraNav(string txtBarraNav)
    {
        BNav.Text = txtBarraNav;
    }

    private void InicializarDatosPagina(string titulo)
    {
        UtilsPresentacionX5.SetTextLabel(LblTituloPagina, titulo);
        Page.Title = titulo;
    }


    private DataTable toDataTable(List<IndicadorPorSolicitudesPaisConvenio> iParam, Int16 MaxItemsShow, out Int64 totalGral)
    {
        //calculo del total
        totalGral = 0;
        foreach (IndicadorPorSolicitudesPaisConvenio i in iParam)
            totalGral += long.Parse(i.TotalPais);

        IComparer<IndicadorPorSolicitudesPaisConvenio> comparer = new MiClaseOrdenada();
        iParam.Sort(comparer); //se ordena de menor a mayor para totalizar los de inferior valor

        DataTable _dt = new DataTable();
        _dt.Columns.Add("Descripcion", typeof(String));
        _dt.Columns.Add("Porcentual", typeof(String));
        _dt.Columns.Add("Total", typeof(String));


        Int16 contadorItems = 0;
        Int32 totCatOtros = 0;
        int aAgrupar = iParam.Count - MaxItemsShow;
        bool agrupado = true;

        foreach (IndicadorPorSolicitudesPaisConvenio oSol in iParam)
        {

            if (aAgrupar > 1 && contadorItems < aAgrupar)
            {
                totCatOtros += Int32.Parse(oSol.TotalPais);
                contadorItems++;
                agrupado = false;
            }
            decimal porc = 0;
            if (contadorItems >= aAgrupar && agrupado == false) //si hubo totalizacion de cat Otros
            {
                DataRow _drTemp;
                _drTemp = _dt.NewRow();
                porc = totCatOtros * 100 / totalGral;
                _drTemp["Descripcion"] = "Otros";
                _drTemp["Porcentual"] = porc.ToString() + " %";
                _drTemp["Total"] = totCatOtros.ToString();

                _dt.Rows.Add(_drTemp);

                agrupado = true;
            }
            else if (agrupado)
            {
                DataRow _drTemp;
                _drTemp = _dt.NewRow();
                porc = Int32.Parse(oSol.TotalPais) * 100 / totalGral;
                _drTemp["Descripcion"] = oSol.DPais;
                _drTemp["Porcentual"] = porc.ToString() + " %";
                _drTemp["Total"] = oSol.TotalPais;
                _dt.Rows.Add(_drTemp);
            }
        }
        return _dt;
    }

    public class MiClaseOrdenada : IComparer<IndicadorPorSolicitudesPaisConvenio>
    {
        public int Compare(IndicadorPorSolicitudesPaisConvenio x, IndicadorPorSolicitudesPaisConvenio y)
        {
            int compareTot = Int32.Parse(x.TotalPais).CompareTo(Int32.Parse(y.TotalPais));
            if (compareTot == 0)
            {
                return Int32.Parse(x.TotalPais).CompareTo(Int32.Parse(y.TotalPais));
            } return compareTot;
        }
    }
    protected void btnToExcell_Click(object sender, EventArgs e)
    {
        try
        {
            string strBody = "";
            Int64 tot;
            strBody = Exportar.DataTable2ExcelString(toDataTable((List<IndicadorPorSolicitudesPaisConvenio>)sesToExporte, Int16.Parse(ConfigurationManager.AppSettings["MaxItemsShowAgrupados"]),out tot));

            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-disposition", "attachment; filename=" + hfcaption.Value + "-" + tot.ToString() + "casos" + ".xls");
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

    private void ConsultarIndicadores(byte tPeriodo, byte periodo, string anio, string txtcaption)
    {
            sesToExporte = null;
            string mensaje = string.Empty;
            string caption = "Totales por pais ingresados en el período " + txtanio.Text;
            lblTotalPaises.Text = "0";
            caption += " - " + txtcaption;

            List<IndicadorPorSolicitudesPaisConvenio> oList = InvocaWsDao.TraeIndicadorPorSolicitudesPaisConvenio(Byte.Parse(ddlTPeriodo.SelectedValue), periodo, anio, out mensaje);
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
                sesToExporte = oList;
                btnImprimir.Enabled = true;
                btnToExcell.Enabled = true;
                long totalPaises = 0;
                //hfcaption.Value = caption;
                dvDatosConsulta.Visible = true;
                gridListadoSolicitudes.DataSource = toDataTable(oList, Int16.Parse(ConfigurationManager.AppSettings["MaxItemsShowAgrupados"]), out totalPaises);
                gridListadoSolicitudes.DataBind();

                lblTotalPaises.Text = totalPaises.ToString();
                hfcaption.Value = caption;

                SetGraficos(TiposEnumerados.TipoGrafico.COLUMNA);
                
                dvNODatosConsulta.Visible = false;
            }
    }

    private void SetGraficos(TiposEnumerados.TipoGrafico tGafico)
    {
        //dvGrafico.Visible = torta || columna || circular;
        Int64 tot;
        DataTable dt = toDataTable(sesToExporte, Int16.Parse(ConfigurationManager.AppSettings["MaxItemsShowAgrupados"]), out tot);
        hftgraf.Value = tGafico.ToString();
        switch (tGafico)
        {
        //    case TiposEnumerados.TipoGrafico.COLUMNA:

        //        litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.COLUMNA,
        //    hfcaption.Value,
        //    "Pais", "Total",
        //    "PaisCol", dt, 0, 2, 600, 400, false);
        //        break;

        //    case TiposEnumerados.TipoGrafico.TORTA:
        //        litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.TORTA,
        //hfcaption.Value,
        //"Pais", "Total",
        //"PaisT", dt, 0, 2, 600, 400, false);
        //        break;

        //    case TiposEnumerados.TipoGrafico.FUNEL:
        //        litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.FUNEL,
        //hfcaption.Value,
        //"Pais", "Total",
        //"PaisCir", dt, 0, 2, 600, 400, false);
        //        break;

        //    case TiposEnumerados.TipoGrafico.SEGMENTO:
        //        litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.SEGMENTO,
        //hfcaption.Value,
        //"Pais", "Total",
        //"PaisCir", dt, 0, 2, 600, 400, false);
        //        break;


        }

    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            txtBarNav = txtBarNav + " > " + txtanio.Text;
            
            switch (ddlTPeriodo.SelectedValue.ToString())
            {
                case "1":
                    ConsultarIndicadores(Byte.Parse(ddlTPeriodo.SelectedValue), Byte.Parse(ddlSemestre.SelectedItem.Value), txtanio.Text, ddlSemestre.SelectedItem.Text);
                    txtBarNav = txtBarNav +  " > " +  ddlSemestre.SelectedItem.Text;
                    break;
                case "2":
                    ConsultarIndicadores(Byte.Parse(ddlTPeriodo.SelectedValue), Byte.Parse(ddltrimestre.SelectedItem.Value), txtanio.Text, ddltrimestre.SelectedItem.Text);
                    txtBarNav = txtBarNav + " > " + ddltrimestre.SelectedItem.Text;
                    break;
                case "3":
                    ConsultarIndicadores(Byte.Parse(ddlTPeriodo.SelectedValue), Byte.Parse(ddlMeses.SelectedItem.Value), txtanio.Text, ddlMeses.SelectedItem.Text);
                    txtBarNav = txtBarNav + " > " + ddlMeses.SelectedItem.Text;
                    break;
                default:
                    ConsultarIndicadores(0, 0, txtanio.Text, "");
                    txtBarNav = txtBarNav;
                    break;
            }
            setBarraNav(txtBarNav);
            txtBarNav = " > Indicadores > Paises";
        }
    }
 
}
