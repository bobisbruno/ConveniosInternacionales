using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Comun_Controles_BotonesGraficos : System.Web.UI.UserControl
{
    private TiposEnumerados.TipoGrafico tipoGraficoSelected = TiposEnumerados.TipoGrafico.COLUMNA;

    #region Eventos Expuestos

    public delegate void Click_Torta(object sender, TiposEnumerados.TipoGrafico tGrafico);
    public delegate void Click_Columna(object sender, TiposEnumerados.TipoGrafico tGrafico);
    public delegate void Click_Linea(object sender, TiposEnumerados.TipoGrafico tGrafico);
    public delegate void Click_Barra(object sender, TiposEnumerados.TipoGrafico tGrafico);
    public delegate void Click_Segmento(object sender, TiposEnumerados.TipoGrafico tGrafico);
    public delegate void Click_Funnel(object sender, TiposEnumerados.TipoGrafico tGrafico);
    

    //Definimos los eventos que puede disparar este control
    public event Click_Torta clTorta;
    public event Click_Columna clColumna;
    public event Click_Linea clLinea;
    public event Click_Funnel clFunnel;
    public event Click_Segmento clSegmento;
    public event Click_Barra clBarra;
    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnColumna_Click(object sender, ImageClickEventArgs e)
    {
        clColumna(this, TiposEnumerados.TipoGrafico.COLUMNA);
        TipoGrafico = TiposEnumerados.TipoGrafico.COLUMNA;
    }

    protected void btnSecmento_Click(object sender, ImageClickEventArgs e)
    {
        clSegmento(this, TiposEnumerados.TipoGrafico.SEGMENTO);
        TipoGrafico = TiposEnumerados.TipoGrafico.SEGMENTO;
    }

    protected void btnTorta_Click(object sender, ImageClickEventArgs e)
    {
        clTorta(this, TiposEnumerados.TipoGrafico.TORTA);
        TipoGrafico = TiposEnumerados.TipoGrafico.TORTA;
    }
    protected void btnLinea_Click(object sender, ImageClickEventArgs e)
    {
        clLinea(this, TiposEnumerados.TipoGrafico.LINEA);
        TipoGrafico = TiposEnumerados.TipoGrafico.LINEA;
    }
    protected void btnBarra_Click(object sender, ImageClickEventArgs e)
    {
        clBarra(this, TiposEnumerados.TipoGrafico.BARRA);
        TipoGrafico = TiposEnumerados.TipoGrafico.BARRA;
    }
    protected void btnFunnel_Click(object sender, ImageClickEventArgs e)
    {
        clFunnel(this, TiposEnumerados.TipoGrafico.FUNEL);
        TipoGrafico = TiposEnumerados.TipoGrafico.FUNEL;

    }

    public void SetControl(bool torta, bool linea, bool barra, bool funnel, bool circular, bool col)
    {
        this.btnTorta.Visible = torta;
        this.btnLinea.Visible = linea;
        this.btnBarra.Visible = barra;
        this.btnFunnel.Visible = funnel;
        this.btnSegmento.Visible = circular;
        this.btnCol.Visible = col;
    }

    public TiposEnumerados.TipoGrafico TipoGrafico
    {
        get
        {
            return tipoGraficoSelected;
        }
        set
        {
            tipoGraficoSelected = value;

        }
    }
    
}
