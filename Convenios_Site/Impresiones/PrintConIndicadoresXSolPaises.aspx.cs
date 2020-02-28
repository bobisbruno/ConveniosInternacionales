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

public partial class PrintConIndicadoresXSolPaises : System.Web.UI.Page
{
    protected List<IndicadorPorSolicitudesPaisConvenio> iList = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
        }

    }

    #region Cargar Datos

    protected void CargarDatos()
    {
        litGrafico.Text = "";
        try
        {
            if (Session["_solicToExport"] != null)
            {
                iList = (List<IndicadorPorSolicitudesPaisConvenio>)Session["_solicToExport"];

                lblCaption.Text = Request.QueryString["caption"].ToString();
                long totalPaises = 0;

                DataTable dt = toDataTable(iList, Int16.Parse(ConfigurationManager.AppSettings["MaxItemsShowAgrupados"]), out totalPaises);

                gridListado.DataSource = dt;
                gridListado.DataBind();

                lblTotalPais.Text = totalPaises.ToString();

                //if (Request.QueryString["tGrafico"].ToUpper() == TiposEnumerados.TipoGrafico.COLUMNA.ToString().ToUpper())
                //    litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.COLUMNA,
                //    Request.QueryString["caption"].ToString(),
                //    "Pais", "Total",
                //    "Pais", dt, 0, 2, 600, 400,true);
                //if (Request.QueryString["tGrafico"].ToUpper() == TiposEnumerados.TipoGrafico.TORTA.ToString().ToUpper())
                //    litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.TORTA,
                //    Request.QueryString["caption"].ToString(),
                //    "Pais", "Total",
                //    "Pais", dt, 0, 2, 600, 400,true);
                //if (Request.QueryString["tGrafico"].ToUpper() == TiposEnumerados.TipoGrafico.SEGMENTO.ToString().ToUpper())
                //    litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.SEGMENTO,
                //    Request.QueryString["caption"].ToString(),
                //    "Pais", "Total",
                //    "Pais", dt, 0, 2, 600, 400,true);

                //if (Request.QueryString["tGrafico"].ToUpper() == TiposEnumerados.TipoGrafico.FUNEL.ToString().ToUpper())
                //    litGrafico.Text = Grafica.traerScriptGrafica(TiposEnumerados.TipoGrafico.FUNEL,
                //    Request.QueryString["caption"].ToString(),
                //    "Pais", "Total",
                //    "Pais", dt, 0, 2, 600, 400, true);
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
}
