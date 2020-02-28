using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using ActoresWS;


public partial class Controles_Busquedabeneficiario : System.Web.UI.UserControl
{
    #region Variables miembro
    private TipoConsultaBeneficioario tipoCriterio = TipoConsultaBeneficioario.CodigoSIACI;

    private string _CodigoCiaci;
    [Browsable(false)]
    [Description("Asigna el nuemro del CodigoCiaci")]
    public string CodigoCiaci
    {
        get { return _CodigoCiaci = txtCodCiaci.Text; }
        set
        {
            _CodigoCiaci = value;
        }
    }

    private string _tramite;
    [Browsable(false)]
    [Description("Asigna el nuemro de tramite")]
    public string TramiteNro
    {
        get { return _tramite = txtNroTramite.Text; }
        set
        {
            _tramite = value;
        }
    }

    private string _ApellidoNombre;
    [Browsable(false)]
    [Description("Asigna el ApellidoNombre")]
    public string ApellidoNombre
    {
        get { return _ApellidoNombre = txtNomApe.Text; }
        set
        {
            _ApellidoNombre = value;
        }
    }

    private string _Documento;
    [Browsable(false)]
    [Description("Asigna el Documento")]
    public string Documento
    {
        get { return _Documento = txtDocumento.Text; }
        set
        {
            _Documento = value;
        }
    }

    private string _TipoDoc;
    [Browsable(false)]
    [Description("Asigna el Documento y tipo doc")]
    public string TipoDoc
    {
        //get { return _TipoDoc = ddlTipoDocumento.SelectedItem.Text+txtDocumento.Text; }
        get { return _TipoDoc = ddlTipoDocumento.SelectedItem.Value; }
        set
        {
            _TipoDoc = value;
        }
    }

    private string _TipoDocDescripcion;
    [Browsable(false)]
    [Description("Asigna el Documento y tipo doc")]
    public string TipoDocDescripcion
    {
        //get { return _TipoDoc = ddlTipoDocumento.SelectedItem.Text+txtDocumento.Text; }
        get { return _TipoDocDescripcion = ddlTipoDocumento.SelectedItem.Text; }
        set
        {
            _TipoDocDescripcion = value;
        }
    }

    #region Atributos de Expediente

    private string _ExpeOrg;
    [Browsable(false)]
    [Description("Asigna el organismo expediente")]
    public string ExpeOrg
    {
        get { return _ExpeOrg = txtorg.Text; }
        set
        {
            _ExpeOrg = value;
        }
    }

    private string _ExpePre;
    [Browsable(false)]
    [Description("Asigna el precuil")]
    public string ExpePre
    {
        get { return _ExpePre = txtpre.Text; }
        set
        {
            _ExpePre = value;
        }
    }

    private string _ExpeDoc;
    [Browsable(false)]
    [Description("Asigna el doc")]
    public string ExpeDoc
    {
        get { return _ExpeDoc = txtddoc.Text; }
        set
        {
            _ExpeDoc = value;
        }
    }

    private string _ExpeDig;
    [Browsable(false)]
    [Description("Asigna el dig")]
    public string ExpeDig
    {
        get { return _ExpeDig = txtdig.Text; }
        set
        {
            _ExpeDig = value;
        }
    }

    private string _ExpeTram;
    [Browsable(false)]
    [Description("Asigna el tram")]
    public string ExpeTram
    {
        get { return _ExpeTram = txttram.Text; }
        set
        {
            _ExpeTram = value;
        }
    }

    private string _ExpeSec;
    [Browsable(false)]
    [Description("Asigna el sec")]
    public string ExpeSecu
    {
        get { return _ExpeSec = txtsec.Text; }
        set
        {
            _ExpeSec = value;
        }
    }

    //private string _ExpeComp;
    [Browsable(false)]
    [Description("Devuelve el expediente completo")]
    public string ExpeComp
    {
        get { return _ExpeOrg + "-" + _ExpePre + "-" + _ExpeDoc  + "-" + _ExpeDig + "-" + _ExpeTram + "-" + _ExpeSec; }
        //set
        //{
        //    _ExpeSec = value;
        //}
    }

    #endregion Atributos de Expediente

    #region Atributos de Cuip

    private string _PreCuip;
    [Browsable(false)]
    [Description("Asigna el precuip")]
    public string PreCUIP
    {
        get { return _PreCuip = txtPrecuip.Text; }
        set
        {
            _PreCuip = value;
        }
    }

    private string _DocCuip;
    [Browsable(false)]
    [Description("Asigna el DocCuip")]
    public string DocCUIP
    {
        get { return _DocCuip = txtDocCuip.Text; }
        set
        {
            _DocCuip = value;
        }
    }

    private string _DigCuip;
    [Browsable(false)]
    [Description("Asigna el digito cuip")]
    public string DigCUIP
    {
        get { return _DigCuip = txtDigCuip.Text; }
        set
        {
            _DigCuip = value;
        }
    }

    [Browsable(false)]
    [Description("Devuelve el CUIP completo")]
    public string CUIPComp
    {
        get { return _PreCuip + "-" + _DocCuip + "-" + _DigCuip; }
        
    }

    #endregion Atributos de Expediente

    #region Atributos de Beneficio

    private string _BenExCaja;
    [Browsable(false)]
    [Description("Asigna el ex caja")]
    public string BenExCaja
    {
        get { return _BenExCaja = txtBenExCaja.Text; }
        set
        {
            _BenExCaja = value;
        }
    }

    private string _BenTipo;
    [Browsable(false)]
    [Description("Asigna el tipo")]
    public string BenTipo
    {
        get { return _BenTipo = txtBenTipo.Text; }
        set
        {
            _BenTipo = value;
        }
    }

    private string _BenNumero;
    [Browsable(false)]
    [Description("Asigna el numero")]
    public string BenNumero
    {
        get { return _BenNumero = txtBenNumero.Text; }
        set
        {
            _BenNumero = value;
        }
    }

    private string _BenCopart;
    [Browsable(false)]
    [Description("Asigna el copart")]
    public string BenCopart
    {
        get { return _BenCopart = txtBenCopart.Text; }
        set
        {
            _BenCopart = value;
        }
    }


    private string _BenDigVerif;
    [Browsable(false)]
    [Description("Asigna el dig verif")]
    public string BenDigVerif
    {
        get { return _BenDigVerif = txtBenDigVerif.Text; }
        set
        {
            _BenDigVerif = value;
        }
    }

    [Browsable(false)]
    [Description("Devuelve el beneficio completo")]
    public string BenComp
    {
        get { return _BenExCaja + "-" +  _BenTipo + "-" + _BenNumero + "-" + _BenCopart + "-" + _BenDigVerif; }
        
    }
    
    
    
    #endregion Atributos de Beneficio

    #endregion


    #region Propiedades
    /// <summary>
    /// Tipo de mensaje a mostrar ALERTA, ERROR, PREGUNTA
    /// </summary>


    public TipoConsultaBeneficioario TipoCriterio
    {
        get
        {

            switch (listaBusqueda.SelectedValue)
            {
                case "0":
                    tipoCriterio = TipoConsultaBeneficioario.NombreoApellidos;
                    break;
                case "1":
                    tipoCriterio = ddlTipoDocumento.SelectedItem.Text.Equals(string.Empty) ? TipoConsultaBeneficioario.Documento : TipoConsultaBeneficioario.DocumentoYTipo;
                    //TipoCriterio = TipoConsultaBeneficioario.Documento;
                    break;
                case "2":
                    tipoCriterio = TipoConsultaBeneficioario.CodigoSIACI;
                    break;
                case "4":
                    tipoCriterio = TipoConsultaBeneficioario.Beneficio;
                    break;
                case "3":
                    tipoCriterio = TipoConsultaBeneficioario.Expediente;
                    break;
                case "5":
                    tipoCriterio = TipoConsultaBeneficioario.CUIP;
                    break;
                case "6":
                    tipoCriterio = TipoConsultaBeneficioario.Tramite;
                    break;
            }

            return tipoCriterio;
        }
        set
        {
            tipoCriterio = value;

        }
    }

    #endregion

    #region Métodos
    
    public string validaParams
    {
        get
        {
            string mensaje = string.Empty;
            mensaje = ((listaBusqueda.SelectedValue.Equals("0") && txtNomApe.Text.Equals(string.Empty))
                || (listaBusqueda.SelectedValue.Equals("1") && txtDocumento.Text.Equals(string.Empty))
                || (listaBusqueda.SelectedValue.Equals("2") && txtCodCiaci.Text.Equals(string.Empty))
                || (listaBusqueda.SelectedValue.Equals("3") && txtddoc.Text.Equals(string.Empty))
                || (listaBusqueda.SelectedValue.Equals("4") && txtBenNumero.Text.Equals(string.Empty))
                || (listaBusqueda.SelectedValue.Equals("5") && txtDocCuip.Text.Equals(string.Empty))
                || (listaBusqueda.SelectedValue.Equals("6") && txtNroTramite.Text.Equals(string.Empty))
                ) ? "Se debe ingresar parámetro de búsqueda" : string.Empty;

            return mensaje;
        }
        
    }

    #endregion

    #region Eventos manejados

    protected void Page_Load(object sender, EventArgs e)
    {
        listaBusqueda.Attributes.Add("onChange", "Seleccionar(this);");
        //Limpiar();
        if (!IsPostBack)
        {
            try
            {
                ddlTipoDocumento.DataSource = VariableSession.oTiposDocumentoFrecuentes;
                ddlTipoDocumento.DataBind();
                ddlTipoDocumento.Items.Insert(0, new ListItem("", "0"));
            }
            catch (Exception)
            {
                
            }
        }

    
    }

    #endregion

    public void Limpiar()
    {
        txtBenDigVerif.Text = string.Empty;
        txtBenCopart.Text = string.Empty;
        txtBenExCaja.Text = string.Empty;
        txtBenNumero.Text = string.Empty;
        txtBenTipo.Text = string.Empty;

        txtCodCiaci.Text = string.Empty;

        txtorg.Text = string.Empty;
        txtpre.Text = string.Empty;
        txtddoc.Text = string.Empty;
        txtdig.Text = string.Empty;
        txttram.Text = string.Empty;
        txtsec.Text = string.Empty;
        
        txtNomApe.Text = string.Empty;

        txtDocumento.Text = string.Empty;
        ddlTipoDocumento.ClearSelection();

        txtNroTramite.Text = string.Empty;

        txtPrecuip.Text = string.Empty;
        txtDocCuip.Text = string.Empty;
        txtDigCuip.Text = string.Empty;

    }


    //public class paramBeneficio
    //{ 
    //    string BenExCaja;
    //    string BenTipo;
    //    string BenNumero;
    //    string BenCopart;
    //    string BenDigVerif;

    //    public paramBeneficio(string BenExCaja,
    //    string BenTipo,
    //    string BenNumero,
    //    string BenCopart,
    //    string BenDigVerif)
    //    {
    //        this.BenCopart = BenCopart;
    //        this.BenExCaja = BenExCaja;
    //        this.BenTipo = BenTipo;
    //        this.BenNumero = BenNumero;
    //        this.BenDigVerif = BenDigVerif;
    //    }
    //}


    //public object ValorParametro()
    //{
    //    object param = string.Empty;
    //    switch (TipoCriterio)
    //    {
    //        case TipoConsultaBeneficioario.NombreoApellidos:
    //            param = txtNomApe.Text;
    //            break;
    //        case TipoConsultaBeneficioario.Documento:
    //            param = txtDocumento.Text;
    //            break;
    //        case TipoConsultaBeneficioario.DocumentoYTipo:
    //            param = ddlTipoDocumento.SelectedItem.Text.Trim() + txtDocumento.Text;
    //            break;
    //        case TipoConsultaBeneficioario.Expediente:
    //            //param = txtorg.Text, txtpre.Text, txtddoc.Text, txtdig.Text, txttram.Text, txtsec.Text;
    //            break;
    //        case TipoConsultaBeneficioario.Beneficio:
    //            paramBeneficio oParam = new paramBeneficio(txtBenExCaja.Text, txtBenTipo.Text, txtBenNumero.Text, txtBenCopart.Text, txtBenDigVerif.Text);
    //            param = oParam;
    //            break;
    //        case TipoConsultaBeneficioario.CodigoSIACI:
    //            param = txtCodCiaci.Text;
    //            break;
    //    }
    //    return (object)param;
    //}

}

