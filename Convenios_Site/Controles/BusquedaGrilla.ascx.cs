using System;
using System.Collections.Generic;
using System.Web.UI;

public partial class comun_controles_BusquedaGrilla : System.Web.UI.UserControl
{
    #region Eventos Expuestos

    public delegate void Click_Buscar(object sender);

    //Definimos los eventos que puede disparar este control
    public event Click_Buscar ClickBuscar   ;

    #endregion  Eventos Expuestos

    #region Metodos

    public void CargarCamposBusqueda(List<string> campos)
    {
        ddlCamposBusqueda.DataSource = campos;
        ddlCamposBusqueda.DataBind();
    }

    public string BusquedaSeleccionada()
    {
        return ddlCamposBusqueda.SelectedValue;
    }

    public string BusquedaIngresada()
    {
        return txtFiltro.Text;
    }

    #endregion Metodos

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void imgabtnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        ClickBuscar(this);
    }
}