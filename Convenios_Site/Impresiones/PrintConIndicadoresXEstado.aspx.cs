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
using System.Net;
using ConsultasWS;
using System.Threading;
using System.IO;

public partial class PrintConIndicadoresXEstado : System.Web.UI.Page
{
    protected List<IndicadorTotalesEstado> iList = null;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dvEvolutivo.Visible = false;
            CargarDatos();
        }

    }

    #region Cargar Datos

    private DataTable toDataTable(List<IndicadorTotalesEstado> iParam, Int16 MaxItemsShow, out Int64 totalGral)
    {
        //calculo del total
        totalGral = 0;
        foreach (IndicadorTotalesEstado i in iParam)
            totalGral += long.Parse(i.TotalEstado);

        IComparer<IndicadorTotalesEstado> comparer = new MiClaseOrdenada();
        iParam.Sort(comparer); //se ordena de menor a mayor para totalizar los de inferior valor

        DataTable _dt = new DataTable();
        _dt.Columns.Add("Descripcion", typeof(String));
        _dt.Columns.Add("Porcentual", typeof(String));
        _dt.Columns.Add("Total", typeof(String));


        Int16 contadorItems = 0;
        Int32 totCatOtros = 0;
        int aAgrupar = iParam.Count - MaxItemsShow;
        bool agrupado = true;

        foreach (IndicadorTotalesEstado oSol in iParam)
        {

            if (aAgrupar > 1 && contadorItems < aAgrupar)
            {
                totCatOtros += Int32.Parse(oSol.TotalEstado);
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
                porc = Int32.Parse(oSol.TotalEstado) * 100 / totalGral;
                _drTemp["Descripcion"] = oSol.Destado;
                _drTemp["Porcentual"] = porc.ToString() + " %";
                _drTemp["Total"] = oSol.TotalEstado;
                _dt.Rows.Add(_drTemp);
            }
        }
        return _dt;
    }

    public class MiClaseOrdenada : IComparer<IndicadorTotalesEstado>
    {
        public int Compare(IndicadorTotalesEstado x, IndicadorTotalesEstado y)
        {
            int compareTot = Int32.Parse(x.TotalEstado).CompareTo(Int32.Parse(y.TotalEstado));
            if (compareTot == 0)
            {
                return Int32.Parse(x.TotalEstado).CompareTo(Int32.Parse(y.TotalEstado));
            } return compareTot;
        }
    }


    protected void CargarDatos()
    {
        litGrafico.Text = "";
        try
        {
            if (Session["_solicToExport"] != null)
            {

                iList = (List<IndicadorTotalesEstado>)Session["_solicToExport"];

                lblCaption.Text = Request.QueryString["caption"].ToString();
                long totalEstados = 0;

                DataTable dt = toDataTable(iList, Int16.Parse(ConfigurationManager.AppSettings["MaxItemsShowAgrupados"]), out totalEstados);

                gridListado.DataSource = dt;
                gridListado.DataBind();
                lblTotal.Text = totalEstados.ToString();

                //if (Request.QueryString["tGrafico"].ToUpper() == TiposEnumerados.TipoGrafico.COLUMNA.ToString().ToUpper())
                //    litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.COLUMNA,
                //    Request.QueryString["caption"].ToString(),
                //    "Estado", "Total",
                //    "EstadoCol", dt, 0, 2, 600, 400, true);
                //if (Request.QueryString["tGrafico"].ToUpper() == TiposEnumerados.TipoGrafico.TORTA.ToString().ToUpper())
                //    litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.TORTA,
                //    Request.QueryString["caption"].ToString(),
                //    "Estado", "Total",
                //    "EstadoTorta", dt, 0, 2, 600, 400, true);
                //if (Request.QueryString["tGrafico"].ToUpper() == TiposEnumerados.TipoGrafico.SEGMENTO.ToString().ToUpper())
                //    litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.SEGMENTO,
                //    Request.QueryString["caption"].ToString(),
                //    "Estado", "Total",
                //    "EstadoCirc", dt, 0, 2, 600, 400, true);

                //if (Request.QueryString["tGrafico"].ToUpper() == TiposEnumerados.TipoGrafico.FUNEL.ToString().ToUpper())
                //    litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.FUNEL,
                //    Request.QueryString["caption"].ToString(),
                //    "Estado", "Total",
                //    "EstadoFunnel", dt, 0, 2, 600, 400, true);

                //Si se enviaron datos evolutivos los informa

                if (Session["_dtFinalToGrid"] != null && Request.QueryString["EvolutivoSN"].Equals("S"))
                {
                    dvEvolutivo.Visible = true;
                    lbCaption2.Text = Request.QueryString["caption2"].ToString();
                    SettingDatosCons(Request.QueryString["fechaIng"].ToString());

                    gridEvolutivo.DataSource = (DataTable)Session["_dtFinalToGrid"];
                    gridEvolutivo.DataBind();
                    //litGraficoLinea.Text = Grafica.traerScriptGraficaMultiSerieLinea(Request.QueryString["caption2"].ToString(), "En totales por Estado / período", "Período", "Total", "", "DivLineal", (DataTable)Session["_dtFinalToGrid"], (DataTable)dtPeriodos, 900, 300, true);
                }

            }
            else
            {

            }
        }
        catch
        {
            //Si no se encuentran los datos
            //lblMsjEncabezado.Text = "Error";
            //lblTextoInforme.Text = "Ocurrio un error al traer los datos del Instrumento";
        }
    }
    #endregion

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
        columna1.DataField = "DEstado";
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

    private DataTable toDataTable(List<IndicadorTotalesEstado> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("DEstado", typeof(String));
        _dt.Columns.Add("PorcentualEstado", typeof(Int16));
        _dt.Columns.Add("TotalEstado", typeof(String));


        foreach (IndicadorTotalesEstado oSol in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["DEstado"] = oSol.Destado;
            _drTemp["PorcentualEstado"] = oSol.PorcentualEstado;
            _drTemp["TotalEstado"] = oSol.TotalEstado;

            _dt.Rows.Add(_drTemp);
        }



        return _dt;
    }

}
