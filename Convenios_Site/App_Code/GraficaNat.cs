using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Generic;
using log4net;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

/// <summary>
/// Summary description for Grafica
/// </summary>
public class GraficaNat
{
    public GraficaNat()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    #region Grafica


    public static void ChartUnicaSerie(Chart Chart, DataTable dtDatos, string nomColX, string nomColY, Boolean toPrint
        , String Titulo
        ,String NombreSerie)
    {
        string colorfondo = toPrint ? "#ffffff" : "#D9E6F4"; //el color del style "fondo claro"

        // Set titulo
        if(Titulo.Equals(string.Empty))
            Chart.Titles.Clear();
        else
            Chart.Titles["Titulo"].Text = Titulo;
        // fin Set titulo

        Chart.BackColor = System.Drawing.ColorTranslator.FromHtml(colorfondo);
        // Set titulo
        //Chart.Legends["Leyenda"].Title = "Totales por voces período xxxxx";
        Chart.Legends.Clear();
        // Set series chart type
        Chart.Series.Clear();
        Chart.Series.Add(NombreSerie);
        Chart.Series[NombreSerie].ChartType = SeriesChartType.Column;

        // Set series point width
        Chart.Series[NombreSerie]["PointWidth"] = "0.6";

        Chart.Series[NombreSerie]["BorderColor"]="180, 26, 59, 105";
        Chart.Series[NombreSerie]["Color"] = "220, 65, 140, 240";

        // Show data points labels
        //Chart.Series[NombreSerie].IsValueShownAsLabel = true;

        // Set data points label style
        Chart.Series[NombreSerie]["BarLabelStyle"] = "Center";
    
        // Show as 3D
        //Chart.ChartAreas[NombreSerie].Area3DStyle.Enable3D = false;
    
        // Draw as 3D Cylinder
        Chart.Series[NombreSerie]["DrawingStyle"] = "Cylinder";

        Chart.Series[NombreSerie].XValueType = ChartValueType.String;
        Chart.Series[NombreSerie].YValueType = ChartValueType.Int32;



        //Chart.DataSource = dtDatos;

        int cantSeries = 1; //Convert.ToInt32(dtDatos.Rows[0]["antal"].ToString());
        string color = string.Empty;
        for (int i = 0; i < cantSeries; i++)
        {
            int j = 0;
            foreach (DataRow dr in dtDatos.Rows)
            {
                j++;
                color = toPrint ? alternateColorDarkGrays(j) : alternateColorBlues(j);
                try
                {
                    DataPoint dp = new DataPoint();
                    dp.AxisLabel = dr[nomColX].ToString();
                    dp.YValues[0] = Convert.ToDouble(dr[nomColY].ToString());
                    dp.XValue = Convert.ToDouble(j);
                    dp.Color = System.Drawing.ColorTranslator.FromHtml(color);

                    Chart.Series[NombreSerie].Points.Add(dp);
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Fallo al ingresar puntos del Gráfico");
                }
            }
        }


        Chart.DataBind();
        Chart.Visible = true;

    }


    public static void ChartMultipleSerie(Chart Chart, DataSet dsDatos, string nomColX, string nomColY, Boolean toPrint
        , String Titulo
        , Int32 ancho
        , Int32 alto
        , TiposEnumerados.TipoGrafico tGrafico
        )
    {
        string colorfondo = toPrint ? "#ffffff" : "#D9E6F4"; //el color del style "fondo claro"
        //string[] colorsLegend = new string[2];
        //colorsLegend[0] = alternateColorBlues(0);
        //colorsLegend[1] = alternateColorBlues(3);

        // Set titulo
        if (Titulo.Equals(string.Empty))
            Chart.Titles.Clear();
        else
            Chart.Titles["Titulo"].Text = Titulo;
        // fin Set titulo
        Chart.BackColor = System.Drawing.ColorTranslator.FromHtml(colorfondo);

        //tamaño
        Chart.Width = ancho;
        Chart.Height = alto;
        
        // Set series chart type
        if(dsDatos == null)
            Chart.Series.Clear();
        else
        {
            int cantSeries = dsDatos.Tables.Count;
            string NombreSerie;
            int i = 0;
            foreach(DataTable dt in dsDatos.Tables)
            {
                NombreSerie = dt.TableName;
                Chart.Series.Add(NombreSerie);

                //tipo de grafico
                if(tGrafico == TiposEnumerados.TipoGrafico.COLUMNA)
                    Chart.Series[NombreSerie].ChartType =    SeriesChartType.Column;
                if (tGrafico == TiposEnumerados.TipoGrafico.BARRA)
                    Chart.Series[NombreSerie].ChartType = SeriesChartType.Bar;
                if (tGrafico == TiposEnumerados.TipoGrafico.LINEA)
                    Chart.Series[NombreSerie].ChartType = SeriesChartType.Line;

                #region chartarea
                //FontStyle fontStyle = FontStyle.Bold;
                //ChartArea cha = new ChartArea();
                ////cha.Name = "ChartAreaMulti";
                //cha.AxisX.IsMarginVisible = false;
                //cha.AxisX.LineColor = System.Drawing.ColorTranslator.FromHtml(alternateColorDarkGrays(0));
                //cha.AxisX.LabelStyle.Font = toPrint ? new Font("Trebuchet MS", float.Parse("5"), fontStyle) : new Font("Trebuchet MS", float.Parse("8"), fontStyle);
                //cha.AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(alternateColorDarkGrays(3));

                //cha.AxisY.IsMarginVisible = false;
                //cha.AxisY.LineColor = System.Drawing.ColorTranslator.FromHtml(alternateColorDarkGrays(0));
                //cha.AxisY.LabelStyle.Font = toPrint ? new Font("Trebuchet MS", float.Parse("5"), fontStyle) : new Font("Trebuchet MS", float.Parse("8"), fontStyle);
                //cha.AxisY.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(alternateColorDarkGrays(3));

                //Chart.ChartAreas.Add(cha);
                #endregion ChartArea

                Chart.Series[NombreSerie].ChartArea = "ChartArea1";
                // Set series point width
                Chart.Series[NombreSerie]["PointWidth"] = "0.8";
                Chart.Series[NombreSerie]["BorderColor"] = "180, 26, 59, 105";
                //Chart.Series[NombreSerie].BackGradientStyle = GradientStyle.None;
                //Chart.Series[NombreSerie]["Color"] = "220, 65, 140, 240";

                if (!toPrint) //no es para impresion byn
                {
                    if (tGrafico == TiposEnumerados.TipoGrafico.LINEA)
                        Chart.Series[NombreSerie].Color = System.Drawing.ColorTranslator.FromHtml(alternateColor(i));
                    else
                        Chart.Series[NombreSerie].Color = System.Drawing.ColorTranslator.FromHtml(i == 0 ? alternateColorBlues(i) : alternateColorAmarelos(i));

                }
                else
                {
                    Chart.Series[NombreSerie].Color = System.Drawing.ColorTranslator.FromHtml(i == 0 ? alternateColorDarkGrays(i) : alternateColorLightGrays(i));
                }
                i++;
                // Set data points label style
                Chart.Series[NombreSerie]["BarLabelStyle"] = "Center";
                // Draw as 3D Cylinder
                Chart.Series[NombreSerie]["DrawingStyle"] = "Cylinder";

                Chart.Series[NombreSerie].XValueType = ChartValueType.String;
                Chart.Series[NombreSerie].YValueType = ChartValueType.Int32;

                
                int j = 0;
                foreach(DataRow dr in dt.Rows)
                {
                    j++;
                    try
                    {
                        DataPoint dp = new DataPoint();
                        dp.AxisLabel = dr[nomColX].ToString();
                        dp.YValues[0] = Convert.ToDouble(dr[nomColY].ToString());
                        dp.XValue = Convert.ToDouble(j);
                        //dp.Color = System.Drawing.ColorTranslator.FromHtml(colorfondo);

                        Chart.Series[NombreSerie].Points.Add(dp);
                    }
                    catch(Exception)
                    {
                        throw new InvalidOperationException("Fallo al ingresar puntos del Gráfico");
                    }
                }

            }
        }
        Chart.DataBind();
        Chart.Visible = true;

    }

    public static void ChartMultipleSerieMultiAreas(Chart Chart, DataSet dsDatos, string nomColX, string nomColY, Boolean toPrint
        , String Titulo
        , Int32 ancho
        , Int32 alto
        , TiposEnumerados.TipoGrafico tGrafico
        , Int32 SeparacionYChArea
        , Int32 heightYChArea
        )
    {
        //Int32 maxValorY = 0;
        string colorfondo = toPrint ? "#ffffff" : "#D9E6F4"; //el color del style "fondo claro"
        
        // Set titulo
        if (Titulo.Equals(string.Empty))
            Chart.Titles.Clear();
        else
            Chart.Titles["Titulo"].Text = Titulo;
        // fin Set titulo
        Chart.BackColor = System.Drawing.ColorTranslator.FromHtml(colorfondo);

        //tamaño
        Chart.Width = ancho;
        Chart.Height = alto;


        // Set series chart type
        if (dsDatos == null)
            Chart.Series.Clear();
        else
        {
            //obtengo el valor maximo de todas las series para setear en cada chart
            //if (maxValorY == 0)
            //    maxValorY = ObtenerValorMaxYScale(dsDatos, nomColY);

            int cantSeries = dsDatos.Tables.Count;
            string NombreSerie;
            int posicionYChart = 7;
            string NombreSerieAnterior = "";
            foreach (DataTable dt in dsDatos.Tables)
            {
                //nombre serie y area
                NombreSerie = dt.TableName;

                #region chartarea
                ChartArea cha = new ChartArea();
                cha.Name = "ChartArea" + NombreSerie;

                Chart.ChartAreas.Add(cha);
                if (!NombreSerieAnterior.Equals(string.Empty))
                    cha.AlignWithChartArea = NombreSerieAnterior;
                NombreSerieAnterior = cha.Name;
                
                cha.BorderColor = System.Drawing.ColorTranslator.FromWin32(64);
                cha.BackGradientStyle = GradientStyle.Center;
                cha.ShadowColor = System.Drawing.Color.Transparent;
                cha.BackColor = System.Drawing.ColorTranslator.FromHtml(colorfondo);
                cha.BackSecondaryColor = System.Drawing.Color.Transparent;  //System.Drawing.ColorTranslator..FromHtml(alternateColorLightGrays(2));
                
                cha.AlignmentOrientation = AreaAlignmentOrientations.Vertical;
                //cha.AlignmentStyle = AreaAlignmentStyles.None;
                cha.Position.Y = posicionYChart;
                cha.Axes[1].Title = NombreSerie;
                cha.AxisY.Minimum = 0;
                //cha.AxisY.Maximum = maxValorY;

                FontStyle fontStyle = FontStyle.Bold;
                cha.Axes[1].TitleFont = new Font("Trebuchet MS", float.Parse("8"), fontStyle);

                
                //posicionamiento de cada area dentro del chart
                posicionYChart += SeparacionYChArea;
                cha.Position.X = 1;
                cha.Position.Width = 98;
                cha.Position.Height = heightYChArea;

                cha.AxisX.IsMarginVisible = false;
                cha.AxisX.LineColor = System.Drawing.ColorTranslator.FromHtml(alternateColorDarkGrays(0));
                cha.AxisX.LabelStyle.Font = toPrint ? new Font("Trebuchet MS", float.Parse("5"), fontStyle) : new Font("Trebuchet MS", float.Parse("8"), fontStyle);
                cha.AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(alternateColorDarkGrays(3));

                cha.AxisY.IsMarginVisible = false;
                cha.AxisY.LineColor = System.Drawing.ColorTranslator.FromHtml(alternateColorDarkGrays(0));
                cha.AxisY.LabelStyle.Font = toPrint ? new Font("Trebuchet MS", float.Parse("5"), fontStyle) : new Font("Trebuchet MS", float.Parse("8"), fontStyle);
                cha.AxisY.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(alternateColorDarkGrays(3));

                //cha.InnerPlotPosition.Width = 95;
                //cha.InnerPlotPosition.Height = 9;
                
                #endregion chartarea

                Chart.Series.Add(NombreSerie);

                //tipo de grafico
                if (tGrafico == TiposEnumerados.TipoGrafico.COLUMNA)
                    Chart.Series[NombreSerie].ChartType = SeriesChartType.Column;
                if (tGrafico == TiposEnumerados.TipoGrafico.BARRA)
                    Chart.Series[NombreSerie].ChartType = SeriesChartType.Bar;
                if (tGrafico == TiposEnumerados.TipoGrafico.LINEA)
                    Chart.Series[NombreSerie].ChartType = SeriesChartType.Line;

                Chart.Series[NombreSerie].ChartArea = cha.Name;
                
                // Set series point width
                Chart.Series[NombreSerie]["PointWidth"] = "0.8";
                Chart.Series[NombreSerie]["BorderColor"] = "180, 26, 59, 105";
                

                Chart.Series[NombreSerie].Color = System.Drawing.ColorTranslator.FromHtml(alternateColorDarkGrays(0));
                Chart.Series[NombreSerie].BorderDashStyle = ChartDashStyle.Solid;
                Chart.Series[NombreSerie].BorderWidth = 2;

                // Set data points label style
                Chart.Series[NombreSerie]["BarLabelStyle"] = "Center";
                // Draw as 3D Cylinder
                Chart.Series[NombreSerie]["DrawingStyle"] = "Cylinder";

                Chart.Series[NombreSerie].XValueType = ChartValueType.String;
                Chart.Series[NombreSerie].YValueType = ChartValueType.Int32;

                int j = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    j++;
                    try
                    {
                        DataPoint dp = new DataPoint();
                        dp.AxisLabel = dr[nomColX].ToString();
                        //dp.Font = new Font("Trebuchet MS", float.Parse("8"), fontStyle);
                        dp.YValues[0] = Convert.ToDouble(dr[nomColY].ToString());
                        dp.XValue = Convert.ToDouble(j);
                        //dp.Color = System.Drawing.ColorTranslator.FromHtml(colorfondo);

                        Chart.Series[NombreSerie].Points.Add(dp);
                    }
                    catch (Exception)
                    {
                        throw new InvalidOperationException("Fallo al ingresar puntos del Gráfico");
                    }
                }

            }
        }
        Chart.DataBind();
        Chart.Visible = true;

    }

    

    #endregion Grafica

    private static Int32 ObtenerValorMaxYScale(DataSet dsSeries, String columnaValor)
    {
        Int32 maximo = 0;

        foreach (DataTable dt in dsSeries.Tables)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr[columnaValor]) > maximo)
                    maximo = Convert.ToInt32(dr[columnaValor]);
            }
        }

        return maximo;
    }



    public static string alternateColor(int indexitem)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("codcolor");
        dt.Rows.Add("#F6BD0F");
        dt.Rows.Add("#8BBA00");
        dt.Rows.Add("#FF8E46");
        dt.Rows.Add("#008E8E");
        dt.Rows.Add("#D64646");
        dt.Rows.Add("#8E468E");
        dt.Rows.Add("#B3AA00");
        dt.Rows.Add("#008ED6");
        dt.Rows.Add("#9D080D");
        dt.Rows.Add("#A186BE");

        int i;
        if (indexitem > 9)
            i = indexitem % 10;
        else
            i = indexitem;

        return dt.Rows[i].ItemArray[0].ToString();
    }

    public static string alternateColorBlues(int indexitem)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("codcolor");
        dt.Rows.Add("#007fff");
        dt.Rows.Add("#1e90ff");
        dt.Rows.Add("#3f9eff");
        dt.Rows.Add("#1f7fdf");
        dt.Rows.Add("#005fbf");
        

        int i;
        if (indexitem > 4)
            i = indexitem % 5;
        else
            i = indexitem;

        return dt.Rows[i].ItemArray[0].ToString();
    }

    public static string alternateColorDarkGrays(int indexitem)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("codcolor");
        dt.Rows.Add("#000000");
        dt.Rows.Add("#2F4F4F");
        dt.Rows.Add("#696969");
        

        int i;
        if (indexitem > 2)
            i = indexitem % 3;
        else
            i = indexitem;

        return dt.Rows[i].ItemArray[0].ToString();
    }


    public static string alternateColorLightGrays(int indexitem)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("codcolor");
        dt.Rows.Add("#D3D3D3");
        dt.Rows.Add("#C0C0C0");
        dt.Rows.Add("#A9A9A9");


        int i;
        if (indexitem > 2)
            i = indexitem % 3;
        else
            i = indexitem;

        return dt.Rows[i].ItemArray[0].ToString();
    }

    
    public static string alternateColorAmarelos(int indexitem)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("codcolor");
        dt.Rows.Add("#FFFFEO");
        dt.Rows.Add("#EEE8AA");
        dt.Rows.Add("#F0E68C");
        dt.Rows.Add("#FAFAD2");
        dt.Rows.Add("#FFD700");

        int i;
        if (indexitem > 4)
            i = indexitem % 5;
        else
            i = indexitem;

        return dt.Rows[i].ItemArray[0].ToString();
    }
}
