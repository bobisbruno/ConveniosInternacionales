using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mensaje : UserControl
{
    #region Variables miembro
    private infoMensaje tipoMensaje = infoMensaje.Alerta;
    private string descripcionMensaje;
    private string textobotonaceptar = "Aceptar";
    private string textobotoncancelar = "Cancelar";
    private int mensajeancho = 300;

    public enum infoMensaje
    {
        Alerta = 1,
        Error = 2,
        Pregunta = 3,
        Info = 4
    }

    #endregion

    #region Eventos Expuestos

    public delegate void Click_UsuarioSi(object sender, string quienLlamo);
    public delegate void Click_UsuarioNo(object sender, string quienLlamo);

    //Definimos los eventos que puede disparar este control
    public event Click_UsuarioSi ClickSi;
    public event Click_UsuarioNo ClickNo;
    //private Page currentPage;
    //private bool ocultarPopUpASPBH = false;
    #endregion

    #region Propiedades
    /// <summary>
    /// Tipo de mensaje a mostrar ALERTA, ERROR, PREGUNTA
    /// </summary>

    //public bool OcultarPopUpASPBH
    //{
    //    get { return ViewState["__OcultarPopUpASPBH"]  == null ? false : (bool)ViewState["__OcultarPopUpASPBH"]; }
    //    set { ViewState["__OcultarPopUpASPBH"] = value; }
    //}

    //public Page CurrentPage 
    //{
    //    get { return (Page)Session["__currentPage"]; }
    //    set { Session["__currentPage"] = value; }
    //}

    public infoMensaje TipoMensaje
    {
        get { return tipoMensaje; }
        set { tipoMensaje = value; }
    }

    public string TextoBotonAceptar
    {
        get { return textobotonaceptar; }
        set { textobotonaceptar = value; }
    }

    public string TextoBotonCancelar
    {
        get { return textobotoncancelar; }
        set { textobotoncancelar = value; }
    }

    public string DescripcionMensaje
    {
        get { return descripcionMensaje; }
        set { descripcionMensaje = value; }
    }

    public string QuienLLama
    {
        get
        {
            return (string)this.ViewState["QuienLlama"];
        }
        set
        {
            this.ViewState["QuienLlama"] = value;
        }
    }

    public int MensajeAncho
    {
        get { return mensajeancho; }
        set
        {
            if (value < 300)
            {
                value = 300;
            }

            mensajeancho = value;
        }
    }

    #endregion

    #region Métodos
    /// <summary>
    /// Muestra la caja de mensaje
    /// </summary>
    public void Mostrar()
    {
        Page.MaintainScrollPositionOnPostBack = false;
        pnlMensaje.Width = new Unit(MensajeAncho.ToString() + "px");

        if (this.DescripcionMensaje != String.Empty)
        {
            lblMensaje.Text = this.DescripcionMensaje;

            if (QuienLLama == null)
            {
                QuienLLama = "Nadie";
            }

            btnNo.Text = TextoBotonCancelar;
            btnAceptar.Text = TextoBotonAceptar;

            switch (TipoMensaje)
            {
                case infoMensaje.Alerta:
                    btnNo.Visible = false;

                    Image1.ImageUrl = "~/App_Themes/imagenes/atencion_gde.gif";
                    lblMensajeTitulo.Text = "Información";
                    break;

                case infoMensaje.Error:
                    btnNo.Visible = false;
                    lblMensajeTitulo.Text = "Atención";
                    Image1.ImageUrl = "~/App_Themes/Imagenes/error.gif";
                    break;

                case infoMensaje.Pregunta:
                    btnNo.Visible = true;
                    lblMensajeTitulo.Text = "Atención";
                    Image1.ImageUrl = "~/App_Themes/Imagenes/pregunta.gif";
                    break;

                case infoMensaje.Info:
                    btnNo.Visible = false;

                    Image1.ImageUrl = "~/App_Themes/Imagenes/infoMed.png";
                    lblMensajeTitulo.Text = "Información";
                    break;

                default:
                    btnNo.Visible = false;
                    Image1.ImageUrl = "~/App_Themes/Imagenes/atencion_gde.gif";
                    lblMensajeTitulo.Text = "Información";
                    break;


            }
            ModalPopupExtenderMensaje.Show();
        }

    }

    /// <summary>
    /// Este metodo oculta el PopUp AsyncPostBackHandler
    /// siendo no siempre oportuno en todos los posback el mismo se muestre.
    /// </summary>
    /// <param name="curentPage">La pagina actual</param>
    public enum PostBackBoton
    {
        Si_Aceptar = 0,
        No_Cancelar = 1
    }
    public void OcultarPopUpASPBH(Page curentPage, PostBackBoton cancelaBoton)
    {
        if (cancelaBoton == PostBackBoton.Si_Aceptar)
        {
            string popupScript = "var aspbh_exceptions = new Array('" + btnAceptar.ClientID + "');";
            ScriptManager.RegisterStartupScript(curentPage, curentPage.GetType(), "aspbh", popupScript, true);
        }
        if (cancelaBoton == PostBackBoton.Si_Aceptar)
        {
            string popupScript = "var aspbh_exceptions = new Array('" + btnNo.ClientID + "');";
            ScriptManager.RegisterStartupScript(curentPage, curentPage.GetType(), "aspbh", popupScript, true);
        }
    }

    #endregion

    #region Eventos manejados

    protected void Page_Load(object sender, EventArgs e)
    {
        //Panel1MensajeErrores.Visible = !(this.lblMensaje.Text == String.Empty || IsPostBack);        
    }

    protected void cmdAceptar_Click(object sender, EventArgs e)
    {
        ClickSi(this, QuienLLama);
        Cerrar();
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        ClickNo(this, QuienLLama);
        Cerrar();
    }
    #endregion

    protected void imgCerrar_Click(object sender, ImageClickEventArgs e)
    {
        Cerrar();
    }

    protected void Cerrar()
    {
        Page.MaintainScrollPositionOnPostBack = true;

        this.lblMensaje.Text = string.Empty;
        this.DescripcionMensaje = string.Empty;
        ModalPopupExtenderMensaje.Hide();
    }

}

