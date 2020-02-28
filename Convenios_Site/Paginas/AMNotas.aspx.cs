using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using AuxiliaresWS;
using ActoresWS;
using ConsultasWS;
using System.Threading;
using System.IO;
using log4net;

public partial class Paginas_AMNotas : System.Web.UI.Page
{

    private readonly ILog log = LogManager.GetLogger(typeof(Paginas_AMNotas).Name);

    private String DatoBeneficiario
    {
        get { return (String)ViewState["_dBen"]; }
        set { ViewState["_dBen"] = value; }
    }

    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
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
    #endregion APlica Seguridad


    protected void Page_Load(object sender, EventArgs e)
    {
        mensaje.ClickSi += new Mensaje.Click_UsuarioSi(ClickearonSi);
        mensaje.ClickNo += new Mensaje.Click_UsuarioNo(ClickearonNo);

        rvtxtFechaRec.MaximumValue = DateTime.Today.ToString("dd/MM/yyyy");
        rvtxtFechaRec.MinimumValue = DateTime.Today.AddYears(-1).ToString("dd/MM/yyyy");

        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnBuscar);

        if (!IsPostBack)
        {
            try
            {
                Limpiar();
                #region seguridad

                if (!AplicarSeguridadPagina())
                    Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"]);
                #endregion seguridad

                InicializarDatosPagina("Ingreso de Notas", "> Gestión > Notas");
                
            }
            catch (Exception)
            {
            }
        }

    }

    protected void btnCancelarSel_Click(object sender, EventArgs e)
    {
        mpeSolicitantes.Hide();
        //btnGuardar.Focus();
    }


    protected void btnPru_Click(object sender, EventArgs e)
    {
        if (txtNota.Visible)
            txtNota.Visible = false;
        else
            txtNota.Visible = true;

    }

    private void InicializarDatosPagina(string titulo, string txtBarraNav)
    {
        UtilsPresentacionX5.SetTextLabel(LblTituloPagina, titulo);
        BNav.Text = txtBarraNav;
        //UtilsPresentacionX5.SetPaginationProperties(gv_Grilla);
        Page.Title = titulo;
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Main.aspx", false);
    }


    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        Limpiar();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            try
            {
                string mensajeEr = string.Empty;
                BeneficiarioNotas iParam = new BeneficiarioNotas();
                iParam.Asunto = txtAsunto.Text;
                //iParam.Descripcion = txttextarea.Value;
                iParam.Descripcion = "";
                iParam.FRecepcion = DateTime.Parse(txtFechaRec.Text);
                iParam.Id_Beneficiario = Int64.Parse(HFIdBeneficiario.Value);
                iParam.NroNota = txtNumNota.Text;


                InvocaWsDao.AMBeneficiario_Notas(iParam, out mensajeEr);

                if (mensajeEr.Equals(string.Empty))
                {
                    mensaje.TipoMensaje = Mensaje.infoMensaje.Info;
                    mensaje.DescripcionMensaje = "La nota se guardó satisfactoriamente";
                    mensaje.Mostrar();
                    mensaje.QuienLLama = "OK";
                    TraeNotasXBeneficiario(Int64.Parse(HFIdBeneficiario.Value));
                    Limpiar(true);
                }
                else
                    MError.MensajeError = "Ocurrió un error al guardar la nota.";
            }
            catch (Exception er)
            {
                log.Error(er.Message);
                MError.MensajeError = "Ocurrió un error al guardar la nota. Revisar log para mayor detalle.";
            }

        }


    }

    private void Limpiar(bool soloAlta)
    {   
        txtAsunto.Text = string.Empty;
        //descomentar
        //txttextarea.Value = string.Empty;
        
        txtFechaRec.Text = string.Empty;
        txtNumNota.Text = string.Empty;
    }

    private void Limpiar()
    {
        //luego de todo OK
        busben.Limpiar();

        HFIdBeneficiario.Value = "";
        lbldatosBenef.Text = "";
        btnGuardar.Enabled = false;
        dvAddNota.Visible = false;
        dvShowNotasOld.Visible = false;
        DatoBeneficiario = "";
        Limpiar(true);
    }

    protected void rptNotas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item; // elemento del Repeater

        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        {
            BeneficiarioNotas oNota = (BeneficiarioNotas)e.Item.DataItem;

            Label lblNNota = (Label)item.FindControl("lblNNota"); // obtenemos el control.
            lblNNota.Text = oNota.NroNota;

            Label lbFecha = (Label)item.FindControl("lbFecha"); // obtenemos el control.
            lbFecha.Text = oNota.FRecepcion.ToShortDateString();

            Label lblAsunto = (Label)item.FindControl("lblAsunto"); // obtenemos el control.
            lblAsunto.Text = oNota.Asunto;

            Label lbDescripcion = (Label)item.FindControl("lbDescripcion"); // obtenemos el control.
            lbDescripcion.Text = Util.PutBRs(oNota.Descripcion, 100);

            Button btnImprimirNota = (Button)item.FindControl("btnImprimirNota"); // obtenemos el control.
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnImprimirNota);
        }
    }

    #region RowCommand
    protected void RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddNota")
        {
            string[] arg = new string[3];
            arg = e.CommandArgument.ToString().Split(';');
            DatoBeneficiario = arg[1] + " - " + arg[2] + " - " + arg[3];
            lbldatosBenef.Text = DatoBeneficiario;
            HFIdBeneficiario.Value = arg[0];
            dvAddNota.Visible = true;
            btnGuardar.Enabled = true;
            TraeNotasXBeneficiario(Int64.Parse(arg[0]));
            mpeSolicitantes.Hide();
        }
    }
    #endregion

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        HFIdBeneficiario.Value = "";
        btnGuardar.Enabled = false;
        dvAddNota.Visible = false;
        //pequeña validacion de ingreso
        string mensajeer = busben.validaParams;
        MError.MensajeError = mensajeer;

        if (mensajeer.Equals(string.Empty))
        {

            List<ActoresWS.LsBeneficiario> oLista = null;
            switch (busben.TipoCriterio)
            {
                case TipoConsultaBeneficioario.NombreoApellidos:
                    oLista = InvocaWsDao.TraerBeneficiarios(TipoConsultaBeneficioario.NombreoApellidos, busben.ApellidoNombre, string.Empty, out mensajeer);
                    break;
                case TipoConsultaBeneficioario.Documento:
                    oLista = InvocaWsDao.TraerBeneficiarios(TipoConsultaBeneficioario.Documento, busben.Documento, string.Empty, out mensajeer);
                    break;
                case TipoConsultaBeneficioario.DocumentoYTipo:
                    oLista = InvocaWsDao.TraerBeneficiarios(TipoConsultaBeneficioario.DocumentoYTipo, busben.Documento, busben.TipoDoc, out mensajeer);
                    break;
                case TipoConsultaBeneficioario.CodigoSIACI:
                    oLista = InvocaWsDao.TraerBeneficiarios(TipoConsultaBeneficioario.CodigoSIACI, busben.CodigoCiaci, string.Empty, out mensajeer);
                    break;
                case TipoConsultaBeneficioario.Expediente:
                    oLista = InvocaWsDao.TraeBeneficiariosXExpteANSES(busben.ExpeOrg, busben.ExpePre, busben.ExpeDoc, busben.ExpeDig, busben.ExpeTram, busben.ExpeSecu, out mensajeer);
                    break;
                case TipoConsultaBeneficioario.Beneficio:
                    oLista = InvocaWsDao.TraeBeneficiariosXNroBeneficioANSES(busben.BenExCaja, busben.BenTipo, busben.BenNumero, busben.BenCopart, busben.BenDigVerif, out mensajeer);
                    break;
            }
            //Muestra aviso de Error no controlado
            MError.MensajeError = mensajeer;

            if (oLista == null || oLista.Count == 0)
            {
                mensaje.DescripcionMensaje = "No existen solicitantes para los datos ingresados";
                mensaje.TipoMensaje = Mensaje.infoMensaje.Info;
                mensaje.QuienLLama = "ErrorBusqueda";
                mensaje.Mostrar();
                Limpiar();
            }
            else
            {
                if (oLista.Count == 1)
                {
                    HFIdBeneficiario.Value = oLista[0].Id_Beneficiario.ToString();
                    DatoBeneficiario = oLista[0].apeNom + " - " + oLista[0].Documento;
                    lbldatosBenef.Text = DatoBeneficiario;
                    btnGuardar.Enabled = true;
                    dvAddNota.Visible = true;
                    TraeNotasXBeneficiario(oLista[0].Id_Beneficiario);
                }
                else
                {
                    gridListadoBenefCNota.DataSource = ToDatatable.toDataTable(oLista);
                    gridListadoBenefCNota.DataBind();
                    lbldatosBenef.Text = "";
                    //MError.MensajeError = "Existe mas de un beneficiario para los datos ingresados";
                    btnGuardar.Enabled = false;
                    mpeSolicitantes.Show();
                }
            }
        }
    }


    private void TraeNotasXBeneficiario(Int64 Id_Beneficiario)
    {
        string mensajeer = string.Empty;
        List<BeneficiarioNotas> _Notas = InvocaWsDao.TraeBeneficiario_Notas(Id_Beneficiario, out mensajeer);
        if (mensajeer.Equals(string.Empty))
        {
            if (_Notas.Count == 0)
            {
                dvShowNotasOld.Visible = false;
            }
            else
            {
                //rptNotas.DataSource = ToDatatable.toDataTable( _Notas);
                rptNotas.DataSource = _Notas;
                rptNotas.DataBind();
                dvShowNotasOld.Visible = true;
            }
        }
        else
        {
            MError.MensajeError = "Ocurrió un error al traer las notas del solicitante";
            dvShowNotasOld.Visible = false;
        }
    }



    protected void PrintCommand(object sender, CommandEventArgs e)
    {
        string[] arg = new string[3];
        arg = e.CommandArgument.ToString().Split(';');
        lbldatosBenef.Text = DatoBeneficiario;

        String script = "";
        script = "<script type='text/javascript'>" + "hidden = open('../Impresiones/PrintNota.aspx?DatoBeneficiario=" + DatoBeneficiario + "&NumNota=" + arg[3] + "&descNota=" + arg[1] + "&fNota=" + arg[2] + "&asuntoNota=" + arg[0] + "');" + "</script>";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", script, false);
    }

    #region SI
    protected void ClickearonSi(object sender, string quienLlamo)
    {
        switch (quienLlamo.Trim())
        {
            case "ErrorBusqueda":
                break;

            case "OK":
                break;
        }
        //Master.MensajeError = "";

    }
    #endregion SI

    #region NO
    protected void ClickearonNo(object sender, string quienLlamo)
    {

    }
    #endregion NO

}