using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Text.RegularExpressions;

public partial class ControlCuil : System.Web.UI.UserControl
{
    private string _Codigo;
    private string _Numero;
    private string _Digito;
    //private bool _Enabled;

    private bool _MostrarError;
    private string _MensajeError;
    private bool _Obligatorio;

    [Browsable(false)]
    [Description("Asigna el nuemro del Codigo")]
    public string Codigo
    {
        get { return _Codigo; }
        set
        {
            _Codigo = value;
            txtCodigo.Text = _Codigo;
        }
    }

    public string AnchoCodigo
    {
        set
        {
            txtCodigo.Width = Unit.Pixel(int.Parse(value));
        }
    }


    public string AnchoNumero
    {
        set
        {
            txtNumero.Width = Unit.Pixel(int.Parse(value));
        }
    }
    public string AnchoDigito
    {
        set
        {
            txtDigito.Width = Unit.Pixel(int.Parse(value));
        }
    }

    [Browsable(false)]
    [Description("Asigna el nuemro del documento")]
    public string Numero
    {
        get { return _Numero; }
        set
        {
            _Numero = value;
            txtNumero.Text = value;
        }
    }

    [Browsable(false)]
    [Description("Asigna el digito verificador")]
    public string Digito
    {
        get { return _Digito; }
        set
        {
            _Digito = value;
            txtDigito.Text = value;
        }
    }



    [Description("Asignar el número de cuil completo sin giones")]
    public string Text
    {
        get
        {
            return txtCodigo.Text + txtNumero.Text + txtDigito.Text;
        }
        set
        {
            try
            {
                if (value.Trim().Length == 11 || value.Trim().Length == 13)
                {
                    value = value.Replace("-", "");

                    txtCodigo.Text = value.Substring(0, 2);
                    txtNumero.Text = value.Substring(2, 8);
                    txtDigito.Text = value.Substring(10, 1);
                }
                else
                {
                    Limpiar();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }

    [Browsable(false)]
    [Description("Retorna un cul sin formato")]
    public string ValueSinFormato
    {
        get
        {
            return txtCodigo.Text + txtNumero.Text + txtDigito.Text;
        }
    }

    [Browsable(false)]
    [Description("Retorna un cul con formato")]
    public string ValueConFormato
    {
        get
        {
            return txtCodigo.Text + "-" + txtNumero.Text + "-" + txtDigito.Text;
        }
    }

    public bool Enabled
    {
        //get { return _Enabled; }
        set
        {
            //_Enabled = value;
            txtCodigo.ReadOnly = !value;
            txtNumero.ReadOnly = !value;
            txtDigito.ReadOnly = !value;
        }
    }

    public void Limpiar()
    {
        txtCodigo.Text = string.Empty;
        txtNumero.Text = string.Empty;
        txtDigito.Text = string.Empty;
        lbl_Error.Visible = false;
    }

    public string ValidarCUIL()
    {
        string cuil = ValueSinFormato;
        string error = string.Empty;

        if (cuil.Length < 11)
        {
            error = "El CUIL/T ingresado está incompleto.";
        }
        else if (!ValidaCUIL(cuil))
        {
            error = "El CUIL/T ingresado no es válido.";
        }

        return error;
    }

    private bool ValidaCUIL(string CUIL)
    {

        string patron = @"^\d{11}$";
        Regex re = new Regex(patron);
        bool resp = re.IsMatch(CUIL);

        if (resp)
        {
            string FACTORES = "54327654321";
            double dblSuma = 0;

            //if (!(CUIL.Substring(0, 1).ToString() != "3" && CUIL.Substring(0, 1).ToString() != "2"))
            if (long.Parse(CUIL)!= 0)            
            {
                for (int i = 0; i < 11; i++)
                    dblSuma = dblSuma + int.Parse(CUIL.Substring(i, 1).ToString()) * int.Parse(FACTORES.Substring(i, 1).ToString());

                resp = Math.IEEERemainder(dblSuma, 11) == 0;
            }
            else
            {
                resp = false; 
            }            
        }

        return resp;
    }

    public static bool esNumerico(string Valor)
    {
        bool ValidoDatos = false;

        Regex numeros = new Regex(@"^\d{1,}$");

        if (Valor.Length != 0)
        {
            ValidoDatos = numeros.IsMatch(Valor);
        }
        return ValidoDatos;
    }  

    public bool Obligatorio
    {
        set
        {
            _Obligatorio = value;
            lbl_Obligatorio.Visible = value;
        }
    }
    public string Mensaje_Error
    {
        set
        {
            _MensajeError = value;
            lbl_Error.Text = value;

        }
        get
        {
            return lbl_Error.Text = _MensajeError;
        }
    }

    public bool HayErrores
    {
        get
        {
            if (_Obligatorio && Text.Length == 0)
            {
                lbl_Error.Text = "Campo Obligatorio";
                _MensajeError = "Campo Obligatorio";
                lbl_Error.Visible = true;
                return true;
            }
            else if (ValidarCUIL().Length != 0)
            {
                _MensajeError = ValidarCUIL();
                lbl_Error.Text = _MensajeError;
                lbl_Error.Visible = true;
                return true;
            }
            else
            {
                lbl_Error.Visible = false;
                return false;
            }
        }
    }

    public bool MostrarError
    {
        set
        {
            if (_Obligatorio && Text.Length == 0)
            {
                lbl_Error.Text = "Campo Obligatorio";
            }
            else
            {
                lbl_Error.Text = _MensajeError;
            }

            _MostrarError = value;
            lbl_Error.Visible = value;

        }
        get
        {
            return lbl_Error.Visible = _MostrarError;
        }
    }

    public void SetFocus()
    {
        txtCodigo.Focus();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //lbl_Error.Visible = false;
    }
}
