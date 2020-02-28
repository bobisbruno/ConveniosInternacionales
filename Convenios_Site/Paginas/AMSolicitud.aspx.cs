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
using log4net;
using System.Threading;
using ActoresWS;
using ExpedienteWS;
using AuxiliaresWS;


public partial class AMSolicitud : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(typeof(AMSolicitud).Name);

    #region Sesiones

    private List<IngDevMov> sesMovimShow
    {
        get { return (List<IngDevMov>)ViewState["_idm"]; }
        set { ViewState["_idm"] = value; }
    }

    private List<AuxiliaresWS.TiposTramiteConvenios> sesTramConvenio
    {
        get { return (List<AuxiliaresWS.TiposTramiteConvenios>)ViewState["_ttc"]; }
        set { ViewState["_ttc"] = value; }
    }

    private List<ActoresWS.Devolucion> sesDevoluciones
    {
        get { return (List<ActoresWS.Devolucion>)ViewState["_dtdevol"]; }
        set { ViewState["_dtdevol"] = value; }
    }

    private List<ActoresWS.Movimiento_Solicitud> sesMovimientos
    {
        get { return (List<ActoresWS.Movimiento_Solicitud>)ViewState["_dtMov"]; }
        set { ViewState["_dtMov"] = value; }
    }

    private TiposEnumerados.tipoMovimiento sesTipoMov
    {
        get { return (TiposEnumerados.tipoMovimiento)ViewState["_dttMov"]; }
        set { ViewState["_dttMov"] = value; }
    }

    private List<ActoresWS.Expediente_Solicitud> sesExpedientes
    {
        get { return (List<ActoresWS.Expediente_Solicitud>)ViewState["_dtExpe"]; }
        set { ViewState["_dtExpe"] = value; }
    }

    private List<ActoresWS.Beneficio_Solicitud> sesBeneficios
    {
        get { return (List<ActoresWS.Beneficio_Solicitud>)ViewState["_dtBeneficio"]; }
        set { ViewState["_dtBeneficio"] = value; }
    }

    private ActoresWS.Solicitud sesSolicitudnueva
    {
        get { return (ActoresWS.Solicitud)ViewState["_Solicitud"]; }
        set { ViewState["_Solicitud"] = value; }
    }

    private List<ActoresWS.Solicitud> sesLSolicitud
    {
        get { return (List<ActoresWS.Solicitud>)ViewState["_listSol"]; }
        set { ViewState["_listSol"] = value; }
    }

    private List<ActoresWS.Ingresos> sesIngresos
    {
        get { return (List<ActoresWS.Ingresos>)ViewState["_dtIngresos"]; }
        set { ViewState["_dtIngresos"] = value; }
    }

    #region Variables de la solicitud
    private Int16 codPrestacion
    {
        get { return (Int16)ViewState["_codPrestacion"]; }
        set { ViewState["_codPrestacion"] = value; }
    }

    private Int16 codPais
    {
        get { return (Int16)ViewState["_codPais"]; }
        set { ViewState["_codPais"] = value; }
    }

    private String descPrestacion
    {
        get { return (String)ViewState["_dPrestacion"]; }
        set { ViewState["_dPrestacion"] = value; }
    }

    private String descPais
    {
        get { return (String)ViewState["_dPais"]; }
        set { ViewState["_dPais"] = value; }
    }

    private String cuip
    {
        get { return (String)ViewState["_cuip"]; }
        set { ViewState["_cuip"] = value; }
    }

    private String ApeNomBenef
    {
        get { return (String)ViewState["_apenom"]; }
        set { ViewState["_apenom"] = value; }
    }

    private Int64? idBeneficiario
    {
        get { return (Int64?)ViewState["_idBen"]; }
        set { ViewState["_idBen"] = value; }
    }

    #endregion Variables de la solicitud

    #endregion Sesiones

    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }

    #region APlica Seguridad Pagina
    private bool AplicarSeguridad()
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
        mensaje.ClickSi += new Mensaje.Click_UsuarioSi(ClickearonSi);
        mensaje.ClickNo += new Mensaje.Click_UsuarioNo(ClickearonNo);

        if (!IsPostBack)
        {
            //Check de Seguridad tanto para ingresar como modificar instrumento
            if (!AplicarSeguridad())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegadoNBtn"].ToString());

            MError.MensajeError = string.Empty;

            CargarCombos();
            
            blanquearPantalla();
            
            idBeneficiario = Int64.Parse(Request.QueryString["idBeneficiario"].ToString());
            cuip = Request.QueryString["cuip"].ToString();

            //anulo boton de ingreso de expedientes - si no tengo el cuil no puedo consultar expedientes
            btntraeExptes.Enabled = !cuip.Equals(string.Empty);

            lbEncabezadoBeneficiario.Text = (string)Request.QueryString["descApeNom"];
            //si hay cod Prestacion y cod pais no es nueva prestacion
            if (!Request.QueryString["codPrestacion"].ToString().Equals("") && !Request.QueryString["codPais"].ToString().Equals(""))
            {
                pnlAllDato.Visible = true;
                dvSelNewPrestacionPais.Visible = false;
                codPrestacion = Int16.Parse(Request.QueryString["codPrestacion"].ToString());
                codPais = Int16.Parse(Request.QueryString["codPais"].ToString());
                descPrestacion = Request.QueryString["descPrestacion"].ToString();
                lbdescPrestacion.Text = " - " + descPrestacion;
                descPais = Request.QueryString["descPais"].ToString();
                lbDescPaisS.Text = " - " + descPais;

                //carga la documentacion de acuerdo a la prestacion
                cargargridDocPrestacion(codPrestacion);
                //setea la info del ultimo movimiento registrado
                SetVarMovimiento(idBeneficiario.Value, codPrestacion);
                //trae los datos correspondientes a la prestacion ya registrados
                TraerInformacionPrestacion();
            }
            else
            {
                pnlAllDato.Visible = false;
                dvSelNewPrestacionPais.Visible = true;
                //carga combo prestaciones no implementadas
                string mensajeErr = "";
                ddlPrestacionesS.DataSource = InvocaWsDao.TraePrestacionesNoIngresadasXIdBeneficiario(idBeneficiario.Value, out mensajeErr);
                ddlPrestacionesS.DataBind();
                ddlPrestacionesS.Items.Insert(0, new ListItem("Seleccione", "0"));
            }
        }
    }

    private void TraerInformacionPrestacion()
    {

        String mensajeer = "";
        #region obtiene las prestaciones ingresadas
        //por el momento solo se cargaran nuevas
        //sesLSolicitud = InvocaWsDao.TraerSolicitudesXIdBeneficiario(idBeneficiario.Value, codPrestacion, out mensajeer);
        //gridListadoSolicitudes.Visible = false;
        //if (sesLSolicitud != null && mensajeer == "")
        //{
        //    dvNoSolicitud.Visible = false;
        //    CargarSolicitudes();
        //}
        //else
        //{
        //    dvNoSolicitud.Visible = true;
        //    MError.MensajeError = mensajeer;
        //}
        #endregion obtiene las prestaciones ingresadas
        mensajeer = "";
        #region obtiene los expedientes cargados

        sesExpedientes = InvocaWsDao.TraeExpedientesXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);
        gridListadoexpedienteBen.Visible = false;
        if (sesExpedientes != null && mensajeer == "")
            CargarExpedientes();
        else
        {
            gridListadoexpedienteBen.Visible = false;
            MError.MensajeError = mensajeer;
        }
        #endregion obtiene las prestaciones ingresadas

        mensajeer = "";

        #region obtiene los beneficios cargados

        sesBeneficios = InvocaWsDao.TraeBeneficiosXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);
        gridListadoBeneficio.Visible = false;
        if (sesBeneficios != null && mensajeer == "")
            CargarBeneficios();
        else
        {
            gridListadoBeneficio.Visible = false;
            MError.MensajeError = mensajeer;
        }
        mensajeer = "";
        #endregion obtiene las prestaciones ingresadas
    }


    #region Metodos movimientos

    private void limpiarTildesGv()
    {
        if (gridDocPrestacion.Rows.Count != 0)
        {
            foreach (GridViewRow gvr in gridDocPrestacion.Rows)
            {
                CheckBox chk = (CheckBox)gvr.Cells[1].FindControl("chkSelDoc");
                chk.Checked = false;
            }
        }
    }

    protected void ddlSectorSelectedIndexCh(object sender, EventArgs e)
    {
        txtObservacionesM.Focus();
    }

    protected void ddlEstadoSelectedIndexCh(object sender, EventArgs e)
    {
        //si se selecciono estado derivado se habilita el cambio de sector

        if (ddlEstado.SelectedValue.Equals("7"))
        {
            ddlSector.Enabled = true;
            ddlSector.Focus();
        }
        else
        {
            ddlSector.Enabled = false;
            txtObservacionesM.Focus();
        }
    }

    protected void ddlTipoMovSelIndexCh(object sender, EventArgs e)
    {
        if (ddlTipoMovimiento.SelectedValue.Equals("0"))
        {
            dvDatosMovimientos.Visible = false;
            btntraeExptes.Focus();
        }
        else
        {
            dvDatosMovimientos.Visible = true;
            switch (ddlTipoMovimiento.SelectedValue)
            {
                case "1":
                    sesTipoMov = TiposEnumerados.tipoMovimiento.Ingreso;
                    ddlTipoIngreso.Focus();
                    break;
                case "2":
                    sesTipoMov = TiposEnumerados.tipoMovimiento.Devolucion;
                    txtDestino.Focus();
                    break;
                case "3":
                    sesTipoMov = TiposEnumerados.tipoMovimiento.DerivaCambioEstado;
                    ddlEstado.Focus();
                    break;
            }
            setDv(sesTipoMov);

        }
        
    }

    private void cargargridDocPrestacion(Int16 codPrestacion)
    {
        List<ActoresWS.TipoDocumentacion> oTipo = InvocaWsDao.TraeTipoDocumentacionXPrestacion(codPrestacion);
        if (oTipo != null)
        {
            gridDocPrestacion.DataSource = ToDatatable.toDataTable(oTipo);
            gridDocPrestacion.DataBind();
        }
        else
            MError.MensajeError = "Error al traer listado de documentación";
    }

    private Boolean validarContenidoGV()
    {
        bool existenChecks = false;
        foreach (GridViewRow gvr in gridDocPrestacion.Rows)
        {
            CheckBox chk = (CheckBox)gvr.Cells[1].FindControl("chkSelDoc");
            if (chk.Checked)
                existenChecks = true;
        }

        return existenChecks;
    }

    //private void setDv( bool ingreso, bool devolucion, bool movimiento, bool setNotif)
    private void setDv(TiposEnumerados.tipoMovimiento iTipoMov)
    {
        dvIngreso.Visible = iTipoMov == TiposEnumerados.tipoMovimiento.Ingreso;
        dvDevolucion.Visible = iTipoMov == TiposEnumerados.tipoMovimiento.Devolucion;
        dvMovimiento.Visible = iTipoMov == TiposEnumerados.tipoMovimiento.DerivaCambioEstado;

        //gridDocPrestacion.Visible = iTipoMov == TiposEnumerados.tipoMovimiento.Ingreso || iTipoMov == TiposEnumerados.tipoMovimiento.Devolucion;
        gridDocPrestacion.Visible = false;

        switch (iTipoMov)
        {
            case TiposEnumerados.tipoMovimiento.Ingreso:
                gridDocPrestacion.Visible = true;
                limpiarTildesGv();
                break;

            case TiposEnumerados.tipoMovimiento.Devolucion:
                gridDocPrestacion.Visible = true;
                limpiarTildesGv();
                break;
        }
    }

    private void GrabarModificaciones(List<ActoresWS.TipoDocumentacion> iListTipo)
    {
        string mensajeer = "";
        try
        {
            switch (sesTipoMov)
            {
                case TiposEnumerados.tipoMovimiento.Ingreso: //ingreso
                    SetSessionIngresos(idBeneficiario.Value, codPrestacion,DateTime.Today.ToShortDateString() , iListTipo, Byte.Parse( ddlTipoIngreso.SelectedValue), ddlTipoIngreso.SelectedItem.Text, txtObservacionIngreso.Text);
                    break;
                case TiposEnumerados.tipoMovimiento.Devolucion: //devolucuion
                    SetSessionDevoluciones(idBeneficiario.Value, codPrestacion,txtDestino.Text, txtObservacionesD.Text, txtCertificado.Text, iListTipo);
                    break;
                case TiposEnumerados.tipoMovimiento.DerivaCambioEstado: //movimiento

                    Boolean conExpediente = sesExpedientes != null;
                    Int32? sectAct = null;
                    Int32? estSig = null;
                    Int32? sectSig = null;
                    if (!HFcodSectorAct.Value.Equals(""))
                        sectAct = Int32.Parse(HFcodSectorAct.Value.ToString());
                    if (!ddlEstado.SelectedValue.Equals("0"))
                        estSig = Int32.Parse(ddlEstado.SelectedValue);
                    if (!ddlSector.SelectedValue.Equals("0"))
                        sectSig = Int32.Parse(ddlSector.SelectedValue);


                    mensajeer = Logica.ValidaOpcionesMovimiento(sectAct,
                        estSig, sectSig, conExpediente);


                    if (!mensajeer.Equals(string.Empty))
                    {
                        mensaje.TipoMensaje = Mensaje.infoMensaje.Error;
                        mensaje.DescripcionMensaje = mensajeer;
                        mensaje.Mostrar();
                        mensajeer = "";
                    }
                    else
                    {
                        SetSessionMovimientos(idBeneficiario.Value, codPrestacion, Int32.Parse(ddlEstado.SelectedValue), ddlEstado.SelectedItem.Text, ddlSector.SelectedValue == "0" ? "" : ddlSector.SelectedItem.Text, Int32.Parse(ddlSector.SelectedValue), txtObservacionesM.Text);
                        LimpiarDatosMovimiento();
                    }
                    break;
            }
            MError.MensajeError = mensajeer;
            if (mensajeer.Equals(string.Empty))
            {
                LimpiarDatosMovimiento();
                dvDatosMovimientos.Visible = false;
            }
            limpiarTildesGv();
        }
        catch (Exception er)
        {
            MError.MensajeError = "Ocurrio un error la procesar la solicitud." + "</br>" + er.Message;
        }
    }
    
    private void SetVarMovimiento(Int64 idBeneficiario, Int16 codPrestacion)
    {
        string mensajeer = string.Empty;
        ConsultasWS.Movimiento_Solicitud oUltimoMovimiento = InvocaWsDao.TraeUltimoMovimientoSolicitud(idBeneficiario, codPrestacion, out mensajeer);
        MError.MensajeError = mensajeer;
        ddlEstado.ClearSelection();
        ddlSector.ClearSelection();
        ddlSector.Enabled = false;

        if (oUltimoMovimiento != null)
        {
            ddlEstado.Items.FindByValue(oUltimoMovimiento.Estado.Cod_estado.ToString()).Selected = true;
            ddlSector.Items.FindByValue(oUltimoMovimiento.Sector.Cod_sector.ToString()).Selected = true;
            HFcodEstadoAct.Value = oUltimoMovimiento.Estado.Cod_estado.ToString();
            HFcodSectorAct.Value = oUltimoMovimiento.Sector.Cod_sector.ToString();
            ddlSector.Enabled = oUltimoMovimiento.Estado.Cod_estado.Equals(7); //si estado esta en derivar

            lbEstadoActual.Text = oUltimoMovimiento.Estado.Descripcion;
            lbSectorActual.Text = oUltimoMovimiento.Sector.Descripcion;
        }
        else
        {
            lbEstadoActual.Text = "No asignado";
            lbSectorActual.Text = "No asignado";
        }

    }

    #region Boton Grabar
    protected void btnGuardarMov_Click(object sender, EventArgs e)
    {
        string mensaje = ValidaMovimiento(sesTipoMov);
        if (mensaje.Equals(string.Empty))
        {
            MError.MensajeError = string.Empty;
            //si no notifica ni deriva solo pasa el listado de doc selected
            GrabarModificaciones(sesTipoMov == TiposEnumerados.tipoMovimiento.DerivaCambioEstado ? null : TraerDocumentacionSelected());
            ddlTipoMovimiento.ClearSelection();
            btntraeExptes.Focus();//pone el foco en el alta de expedientes si se graba el movimiento ok
        }
        else
            MError.MensajeError = mensaje;
    }

    private string ValidaMovimiento(TiposEnumerados.tipoMovimiento iMov)
    {
        string mensajeErr = "";

        switch (iMov)
        {
            case TiposEnumerados.tipoMovimiento.Ingreso:
                if (ddlTipoIngreso.SelectedValue.Equals("0"))
                    mensajeErr = "Seleccione tipo de ingreso.";
                //else if (!txtFechaIngDoc.Text.Equals(string.Empty))
                //{
                //    if(!Util.EsFecha(txtFechaIngDoc.Text))
                //        mensajeErr = "Fecha de ingreso de documentación no válida";
                //    else if (!Util.esRangoFechaValido(txtFechaIngDoc.Text, 1, 0))
                //        mensajeErr = "La fecha de ingreso de documentación debe ser no inferior a un año atrás, ni superior a la fecha actual.";
                //}
                else if(!validarContenidoGV())
                    mensajeErr = "Se debe seleccionar documentación";
                break;
            case TiposEnumerados.tipoMovimiento.Devolucion:
                //if (txtObservacionesD.Text.Equals(string.Empty))
                //    mensajeErr = "Debe ingresar observaciónes";
                //else 
                if (!validarContenidoGV())
                    mensajeErr = "Se debe seleccionar documentación";
                break;
            case TiposEnumerados.tipoMovimiento.DerivaCambioEstado:
                if (ddlEstado.SelectedValue.Equals(string.Empty))
                    mensajeErr = "Se debe seleccionar estado.";
                else if (ddlSector.SelectedValue.Equals(string.Empty))
                    mensajeErr = "Se debe seleccionar sector.";
                //else if (txtObservacionesM.Text.Equals(string.Empty))
                //    mensajeErr = "Debe ingresar observaciónes";
                break;
        }

        return mensajeErr;
    }

    #endregion Boton

    private List<ActoresWS.TipoDocumentacion> TraerDocumentacionSelected()
    {

        List<ActoresWS.TipoDocumentacion> iListTipoDoc = new List<ActoresWS.TipoDocumentacion>();
        //recorro y guardo lo tipos seleccionados para grabar al tx
        foreach (GridViewRow gvr in gridDocPrestacion.Rows)
        {
            CheckBox chk = (CheckBox)gvr.Cells[1].FindControl("chkSelDoc");
            if (chk.Checked)
            {
                ActoresWS.TipoDocumentacion iTdoc = new ActoresWS.TipoDocumentacion();
                iTdoc.CodTipoDocumentacion = Int32.Parse(gridDocPrestacion.DataKeys[gvr.RowIndex].Values["CodTipoDocumentacion"].ToString());
                iTdoc.Descripcion = gridDocPrestacion.DataKeys[gvr.RowIndex].Values["Descripcion"].ToString();
                iListTipoDoc.Add(iTdoc);
            }

        }
        return iListTipoDoc;
    }


    #endregion


    #region Cargar datos del beneficiario

    protected void CargarBeneficios()
    {
        try
        {
            #region Datos beneficios
            if (sesBeneficios == null || sesBeneficios.Count == 0)
                gridListadoBeneficio.Visible = false;
            else
            {
                gridListadoBeneficio.DataSource = ToDatatable.toDataTable(sesBeneficios);
                gridListadoBeneficio.DataBind();
                gridListadoBeneficio.Visible = true;
            }
            #endregion

        }
        catch
        {
            //blanquearPantalla();
            MError.MensajeError = "No se ha/n podido obtener el/los beneficio/s.";
        }
    }

    protected void CargarExpedientes()
    {
        try
        {
            #region Datos expedientes
            if (sesExpedientes == null || sesExpedientes.Count == 0)
                gridListadoexpedienteBen.Visible = false;
            else
            {
                gridListadoexpedienteBen.DataSource = ToDatatable.toDataTable(sesExpedientes);
                gridListadoexpedienteBen.DataBind();
                gridListadoexpedienteBen.Visible = true;
            }
            #endregion

        }
        catch
        {
            //blanquearPantalla();
            MError.MensajeError = "No se ha/n podido obtener el/los expediente/s.";
        }
    }

    protected void CargarSolicitudes()
    {
        try
        {
            #region Datos Solicitudes
            //dvDatosSelSolicitud.Visible = false;
            if (sesLSolicitud == null)
            {
                gridListadoSolicitudes.Visible = false;
            }
            else
            {
                gridListadoSolicitudes.DataSource = ToDatatable.toDataTable(sesLSolicitud);
                gridListadoSolicitudes.DataBind();
                gridListadoSolicitudes.Visible = true;
            }
            #endregion Datos Solicitudes

        }
        catch
        {
            //blanquearPantalla();
            MError.MensajeError = "No se ha/n podido obtener la/s solicitud/es.";
        }
    }
    #endregion

    //#region Setea valores combos

    //private void ActualizaCMBLocalidades(DropDownList combo, Int16 valorProvincia, Int32 valorSeleccionLocalidad)
    //{
    //    combo.DataSource = InvocaWsDao.TraerLocalidadesXProvincia(valorProvincia);
    //    combo.DataBind();
    //    combo.Items.FindByValue(valorSeleccionLocalidad.ToString()).Selected = true;
    //}

    //#endregion Setea valores combos

    #region RowCommand
    protected void RowCommand(object sender, GridViewCommandEventArgs e)
    {

        #region Modifica Expediente
        if (e.CommandName == "ModificarE")
        {
            HFtipoTxExpe.Value = TiposEnumerados.TipoTx.Modificacion.ToString();
            string fAltaExpe = e.CommandArgument.ToString();
            HFFaltaExpe.Value = fAltaExpe;

            if (sesExpedientes != null)
            {
                List<ActoresWS.Expediente_Solicitud> listExpe = (List<ActoresWS.Expediente_Solicitud>)sesExpedientes;
                foreach (ActoresWS.Expediente_Solicitud iExpe in listExpe)
                {
                    if (iExpe.FAltaexpediente.ToShortDateString() == HFFaltaExpe.Value)
                    {
                        //txtExpteCaratula.Text = iExpe.Caratula;
                        //txtExpteDigCu.Text = iExpe.Expediente_digcu;
                        //txtExpteDocCu.Text = iExpe.Expediente_doccu;
                        //txtExpteObservacion.Text = iExpe.Observacion;
                        //txtExpteOrg.Text = iExpe.Expediente_org;
                        //txtExptePreCu.Text = iExpe.Expediente_precu;
                        //txtExpteSec.Text = iExpe.Expediente_sec;
                        //txtExpteTram.Text = iExpe.Expediente_ctipo;

                        mpeIngExpediente.Show();
                    }
                }
            }
        }
        #endregion Modifica Expediente

        #region Modifica Beneficio
        //if (e.CommandName == "ModificarBen")
        //{
        //    HFtipoTxBeneficio.Value = TiposEnumerados.TipoTx.Modificacion.ToString();
        //    string fAltaBen = e.CommandArgument.ToString();
        //    HFFaltaBeneficio.Value = fAltaBen;
        //    ddlTipoTrDerivadoBen.ClearSelection();

        //    if (sesBeneficios != null)
        //    {
        //        List<ActoresWS.Beneficio_Solicitud> listBenef = (List<ActoresWS.Beneficio_Solicitud>)sesBeneficios;
        //        foreach (ActoresWS.Beneficio_Solicitud iBenef in listBenef)
        //        {
        //            if (iBenef.FAltaBeneficio.ToShortDateString() == HFFaltaBeneficio.Value)
        //            {
        //                txtBenExCaja.Text = iBenef.BenExCaja;
        //                txtBenCopart.Text = iBenef.BenCopart;
        //                txtBenDigVerif.Text = iBenef.BenDigVerif;
        //                txtBenNumero.Text = iBenef.BenNumero;
        //                txtBenObservacion.Text = iBenef.Observacion;
        //                txtBenTipo.Text = iBenef.BenTipo;
        //                if (iBenef.TipoTrDerivado.HasValue)
        //                    ddlTipoTrDerivadoBen.Items.FindByValue(iBenef.TipoTrDerivado.Value.ToString()).Selected = true;

        //                mpeIngBeneficio.Show();
        //            }
        //        }
        //    }
        //}
        #endregion Modifica Beneficio

        #region Borra solicitud de la session
        if (e.CommandName == "BorrarS")
        {
            string fingreso = e.CommandArgument.ToString();
            
            if (sesLSolicitud != null)
            {
                List<ActoresWS.Solicitud> listSol = (List<ActoresWS.Solicitud>)sesLSolicitud;
                ActoresWS.Solicitud acsol = null;
                foreach (ActoresWS.Solicitud iSol in listSol)
                {
                    if (iSol.FechaIngreso.HasValue)
                    {
                        if (iSol.FechaIngreso.Value.ToShortDateString() == fingreso)
                        {
                            acsol = iSol;
                        }
                    }
                }
                if(acsol != null)
                    listSol.RemoveAt(listSol.IndexOf(acsol));
                
                sesLSolicitud = listSol;
                gridListadoSolicitudes.DataSource = ToDatatable.toDataTable(sesLSolicitud);
                gridListadoSolicitudes.DataBind();
            }
            
        }
        #endregion Modifica Beneficio
    }
    #endregion RowCommand

    #region Carga Combos
    private bool CargarCombos()
    {
        string mensajeCarga = string.Empty;

        try
        {
            ////Combo Paises
            List<PaisWS.Pais> oPaisConvenios = VariableSession.oPaisConvenios;
            
            if (oPaisConvenios != null)
            {
                //De solicitud solo carga los paises con convenio
                ddlPaisS.DataSource = oPaisConvenios;
                ddlPaisS.DataBind();
                ddlPaisS.Items.Insert(0, new ListItem("Seleccione", "0"));
            }
            else
                mensajeCarga += "Paises" + "</br>";

            //////Combo tramites derivados
            //List<AuxiliaresWS.Tramitesderivados> oTramitesDer = VariableSession.oTramitesDerivados;

            //if (oTramitesDer != null)
            //{
            //    ddlTipoTrDerivadoBen.DataSource = oTramitesDer;
            //    ddlTipoTrDerivadoBen.DataBind();
            //    ddlTipoTrDerivadoBen.Items.Insert(0, new ListItem("Otro", "0"));
            //}
            //else
            //    mensajeCarga += "TramitesDerivados" + "</br>";

            List<AuxiliaresWS.TipoIngreso> oTipoIngreso = VariableSession.oTipoIngreso;

            if (oTipoIngreso != null)
            {
                ddlTipoIngreso.DataTextField = "Descripcion";
                ddlTipoIngreso.DataValueField = "IdTipoIngreso";
                ddlTipoIngreso.DataSource = oTipoIngreso;
                ddlTipoIngreso.DataBind();
                ddlTipoIngreso.Items.Insert(0, new ListItem("Seleccione", "0"));
            }
            else
                mensajeCarga += "Tipos de ingreso" + "</br>";

            ////Combo estados
            List<AuxiliaresWS.Estado> oTipoEstado = VariableSession.oEstados;
            if (oTipoEstado != null)
            {
                ddlEstado.DataTextField = "Descripcion";
                ddlEstado.DataValueField = "Cod_estado";
                ddlEstado.DataSource = oTipoEstado;
                ddlEstado.DataBind();
                ddlEstado.Items.Insert(0, new ListItem("Seleccione", "0"));
            }
            else
                mensajeCarga += "Estados" + "</br>";

            ////Combo Sectores
            List<AuxiliaresWS.Sector> oTipoSector = VariableSession.oSectores;
            if (oTipoSector != null)
            {
                ddlSector.DataTextField = "Descripcion";
                ddlSector.DataValueField = "Cod_sector";
                ddlSector.DataSource = oTipoSector;
                ddlSector.DataBind();
                ddlSector.Items.Insert(0, new ListItem("Seleccione", "0"));
            }
            else
                mensajeCarga += "Sectores" + "</br>";

            List<AuxiliaresWS.MotivoDenegacion> oMotivoDenegacion = VariableSession.oMotivoDenegacion;

            if (oMotivoDenegacion != null)
            {
                ddlMotivoDeniega.DataTextField = "Descripcion";
                ddlMotivoDeniega.DataValueField = "CodMotivo";
                ddlMotivoDeniega.DataSource = oMotivoDenegacion;
                ddlMotivoDeniega.DataBind();
                ddlMotivoDeniega.Items.Insert(0, new ListItem("", "0"));

            }
            else
                mensajeCarga += "Motivos denegatoria" + "</br>";

            if (!mensajeCarga.Equals(string.Empty))
            {
                MError.MensajeError = "Las siguientes listas no pudieron cargarse:" + "</br>" + mensajeCarga;
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            log.Error("Ocurrió un error al intentar traer una o mas Listas. " + ex.Message);
            MError.MensajeError = "Ocurrió un error al intentar traer una o mas Listas. Intente mas tarde.";
            return false;
        }

    }
    #endregion Carga Combos

    #region SI
    protected void ClickearonSi(object sender, string quienLlamo)
    {
        String script;
        switch (quienLlamo.Trim())
        {
            case "btnCerrar_Click":
                script = "<script type='text/javascript'>" + "hidden = window.close();" + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "xxx", script, false);
                break;


            case "btnGuardar_Click":
                script = "<script type='text/javascript'>" + "hidden = window.close();" + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "xxx", script, false);
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

    #region Guardar todo
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string mensajeError = string.Empty;
        
        try
        {
            InvocaWsDao.AMAllDatosSolicitud(idBeneficiario.Value
                    , codPrestacion
                    ,codPais
                    , sesLSolicitud
                    , sesExpedientes
                    , sesBeneficios
                    , sesIngresos
                    , sesDevoluciones
                    , sesMovimientos
                    , out mensajeError);
            
            if (mensajeError.Equals(string.Empty))
            {
                sesMovimShow = null;
                sesLSolicitud = null;
                sesIngresos = null;
                sesDevoluciones = null;
                sesMovimientos = null;

                btnImprimir.Enabled = true;

                gridListadoSolicitudes.Visible = false;
                gridMovimientosSol.Visible = false;

                mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
                mensaje.TextoBotonCancelar = "Continuar";
                mensaje.DescripcionMensaje = "Los cambios se han guardado con éxito." + "</br>" + "¿ Deséa cerrar ?";
                mensaje.QuienLLama = "btnGuardar_Click";
                mensaje.Mostrar();
            }
            else
                MError.MensajeError = mensajeError;
        }
        catch (Exception er)
        {
            log.Error(er.Message);
            MError.MensajeError = er.Message;
        }
    }
    #endregion Guardar

    #region Verifica ingreso documentacion

    private bool HayDocIngresada()
    {
        bool checkAny = false;
        foreach (GridViewRow gvr in gridDocPrestacion.Rows)
        {
            CheckBox chk = (CheckBox)gvr.Cells[1].FindControl("chkSelDoc");
            if (chk.Checked)
                checkAny = true;
        }
        return checkAny;
    }

    #endregion Verifica ingreso documentacion

    #region Regresar del PopUp


    protected void btn_cerrarPopExpe_Click(object sender, EventArgs e)
    {
        mpeIngExpediente.Hide();
        btnGuardar.Focus();
    }

    //protected void btn_cerrarPopben_Click(object sender, EventArgs e)
    //{
    //    mpeIngBeneficio.Hide();
    //}

    #endregion Regresar del PopUp

    #region Guardar datos del PopUp

    //protected void btn_AgregarPopben_Click(object sender, EventArgs e)
    //{
    //    //valida datos expediente
    //    string mensaje = ValidarBeneficio();
    //    if (mensaje.Equals(string.Empty))
    //    {
    //        MErrorBenef.Text = "";
    //        SetSessionBeneficio();
    //        if (sesBeneficios.Count != 0)
    //        {
    //            gridListadoBeneficio.DataSource = ToDatatable.toDataTable((List<ActoresWS.Beneficio_Solicitud>)sesBeneficios.ToList());
    //            gridListadoBeneficio.DataBind();
    //            gridListadoBeneficio.Visible = true;


    //        }
    //        else
    //            gridListadoBeneficio.Visible = false;
    //        LimpiaControlesBeneficio();
    //        HFtipoTxBeneficio.Value = string.Empty;

    //        //setControlesPopUpBenef(false);
    //        mpeIngBeneficio.Hide();
    //    }
    //    else
    //    {
    //        MErrorBenef.Text = mensaje;
    //        mpeIngBeneficio.Show();
    //    }
    //}

    //private string ValidarBeneficio()
    //{
    //    string msjError = "";
    //    if (txtBenExCaja.Text.Equals(string.Empty))
    //        msjError = "Ingresar Ex-Caja";
    //    else if (txtBenTipo.Text.Equals(string.Empty))
    //        msjError = "Ingresar tipo beneficio";
    //    else if (txtBenNumero.Text.Equals(string.Empty))
    //        msjError = "Ingresar número beneficio";
    //    else if (txtBenCopart.Text.Equals(string.Empty))
    //        msjError = "Ingresar copartícipe";
    //    else if (txtBenDigVerif.Text.Equals(string.Empty))
    //        msjError = "Ingresar dígito verificador";
        
    //    return msjError;
    //}



    protected void btn_AgregarPopExpe_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow iExpeANME in gvexpedtesANME.Rows)
        {
            CheckBox chk = (CheckBox)iExpeANME.Cells[1].FindControl("chbSeleccion");
            if (chk.Checked)
            {
                //Beneficio_Solicitud
                //SetSessionExpte()

                //iTdoc.CodTipoDocumentacion = Int32.Parse(gridDocPrestacion.DataKeys[gvr.RowIndex].Values["CodTipoDocumentacion"].ToString());
                //iTdoc.Descripcion = gridDocPrestacion.DataKeys[gvr.RowIndex].Values["Descripcion"].ToString();
                //iListTipoDoc.Add(iTdoc);
            }
        }
        //valida datos expediente
        //string mensaje = ValidarExpediente();
        //if (mensaje.Equals(string.Empty))
        //{
          //  //MErrorExpe.Text = "";
            //SetSessionExpte();
            if (sesExpedientes.Count != 0)
            {
                gridListadoexpedienteBen.DataSource = ToDatatable.toDataTable((List<ActoresWS.Expediente_Solicitud>)sesExpedientes.ToList());
                gridListadoexpedienteBen.DataBind();
                gridListadoexpedienteBen.Visible = true;


            }
            else
                gridListadoexpedienteBen.Visible = false;
            LimpiaControlesExpte();
            HFtipoTxExpe.Value = string.Empty;

            //setControlesPopUpExped(false);
            mpeIngExpediente.Hide();
            btnGuardar.Focus();




            //agrega los beneficios
            //SetSessionBeneficio();
            if (sesBeneficios.Count != 0)
            {
                gridListadoBeneficio.DataSource = ToDatatable.toDataTable((List<ActoresWS.Beneficio_Solicitud>)sesBeneficios.ToList());
                gridListadoBeneficio.DataBind();
                gridListadoBeneficio.Visible = true;


            }
            else
                gridListadoBeneficio.Visible = false;
            //LimpiaControlesBeneficio();
            HFtipoTxBeneficio.Value = string.Empty;
    }

    //private string ValidarExpediente()
    //{
    //    string msjError = "";
    //    //if (txtExpteOrg.Text.Equals(string.Empty))
    //    //    msjError = "Ingresar organismo";
    //    //else if (txtExptePreCu.Text.Equals(string.Empty))
    //    //    msjError = "Ingresar pref. CUIL";
    //    //else if (txtExpteDocCu.Text.Equals(string.Empty))
    //    //    msjError = "Ingresar documento";
    //    //else if (txtExpteDigCu.Text.Equals(string.Empty))
    //    //    msjError = "Ingresar dígito";
    //    //else if (txtExpteTram.Text.Equals(string.Empty))
    //    //    msjError = "Ingresar trámite";
    //    //else if (txtExpteSec.Text.Equals(string.Empty))
    //    //    msjError = "Ingresar secuencia";

    //    return msjError;
    //}

    #endregion

    #region Regresar
    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        mensaje.DescripcionMensaje = "¿ Desea cerrar ? Los cambios no guardados se perderán!!";
        mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
        mensaje.TextoBotonCancelar = "Continuar";
        mensaje.QuienLLama = "btnCerrar_Click";
        mensaje.Mostrar();
    }
    #endregion Regresar

    #region Blanqueo Pantall

    private void blanquearPantalla()
    {
        lbdescPrestacion.Text = "";
        MError.MensajeError = string.Empty;
        
        LimpiaControlesSolicitud();

        //sessiones
        sesExpedientes = null;
        sesBeneficios = null;
        sesLSolicitud = null;
        sesIngresos = null;
        sesDevoluciones = null;
        sesMovimientos = null;
        sesMovimShow = null;

        idBeneficiario = null;
        
        //gridviews
        gridListadoexpedienteBen.Visible = false;
        gridListadoSolicitudes.Visible = false;
        gridListadoBeneficio.Visible = false;
        gridDocPrestacion.Visible = false;
        gridMovimientosSol.Visible = false;
        
        //hidden
        HFFaltaExpe.Value = string.Empty;
        //HFIdPrestacion.Value = string.Empty;
        HFtipoTxExpe.Value = string.Empty;
        HFtipoTxSolicitud.Value = string.Empty;
        
        //div
        //dvDatosSelSolicitud.Visible = false;
        btnGuardar.Enabled = true;

        //Inhabilita combo sectores por default
        ddlSector.Enabled = false;


        dvDatosMovimientos.Visible = false;
        btnImprimir.Enabled = false;

    }

    private void LimpiarDatosMovimiento()
    {
        //datos movimiento
        txtCertificado.Text = string.Empty;
        txtDestino.Text = string.Empty;
        txtObservacionesD.Text = string.Empty;
        txtObservacionesM.Text = string.Empty;

        ddlEstado.ClearSelection();
        ddlSector.ClearSelection();
        ddlTipoIngreso.ClearSelection();
    }

    #endregion Blanqueo Pantall

    #region Ingreso de beneficio
    //protected void btnIngBeneficios_Click(object sender, EventArgs e)
    //{
    //    setControlesPopUpBenef();
    //    mpeIngBeneficio.Show();
    //}
    #endregion Ingreso de beneficio


    #region Ingreso de expediente
    protected void btnSelExpediente_Click(object sender, EventArgs e)
    {
        //trae tipo tramites pais/prestacion
        string Msjerror = "";
        List<TiposTramiteConvenios> oLTipoTrConvenios = InvocaWsDao.TraeTiposTramiteConvenios(codPais, codPrestacion, out Msjerror);
        MError.MensajeError = Msjerror;
        if (oLTipoTrConvenios.Count > 0)
        {
            string _tipos = "";
            bool _first = true;
            foreach (TiposTramiteConvenios _tp in oLTipoTrConvenios)
            {
                if (_first)
                {
                    _tipos = "'" + _tp.TipoTram.Trim() + "'";
                    _first = false;
                }
                else
                    _tipos += ",'"+_tp.TipoTram.Trim()+"'";
            }

            
            //trae Expedientes
            cuip = cuip.PadLeft(11, '0');
            ExpedienteWS.CuilDTO iCuil = new CuilDTO();
            iCuil.preCuil = cuip.Substring(0,2);
            iCuil.docCuil = cuip.Substring(2, 8);
            iCuil.digCuil = cuip.Substring(10, 1);

            List<ExpedienteDTO> oexpediennteDto = InvocaWSExternos.TraerExpedientesPorOrganismoYCuilYTiposDeTramites(iCuil, _tipos, out Msjerror);
            
            MError.MensajeError = Msjerror;

            if (oexpediennteDto != null && oexpediennteDto.Count > 0)
            {
                //setControlesPopUpExped();
                gvexpedtesANME.DataSource = ToDatatable.toDataTable(oexpediennteDto);
                gvexpedtesANME.DataBind();
                mpeIngExpediente.Show();
            }
            else
            {
                mensaje.DescripcionMensaje = "No existen expedientes en ANME para el solicitante / convenio requerido.";
                mensaje.TipoMensaje = Mensaje.infoMensaje.Alerta;
                mensaje.Mostrar();
            }
        }
    }
    #endregion Ingreso de expediente

    #region Controles de solicitud

    protected void btnGuardarSolicitud_Click(object sender, EventArgs e)
    {
        string mensaje = ValidaSolicitud();
        if(mensaje.Equals(string.Empty))
        {
            MError.MensajeError = "";
            GuardarSolicitud();
        }
        else
            MError.MensajeError = mensaje;

    }

    private string ValidaSolicitud()
    {
        string mensajeError = string.Empty;

        if (txtFechaIngSolicitud.Text.Equals(string.Empty))
            mensajeError = "Se debe ingresar una fecha de ingreso";
        else if(!Util.EsFecha(txtFechaIngSolicitud.Text))
            mensajeError = "Fecha de ingreso no válida";
        else if (!Util.esRangoFechaValido(txtFechaIngSolicitud.Text, 2, 0))
            mensajeError = "La fecha de ingreso debe ser no inferior a dos años atrás, ni superior a la fecha actual.";
        else if (txtObservacionS.Text.Length > 500)
            mensajeError = "El campo observación no debe superar los 500 caracteres.";
        return mensajeError;
    }

    
    private void GuardarSolicitud()
    {
        SetSessionSolicitudes();

        //InvocaWsDao.AltaIngreso(
        if ((sesLSolicitud.Count > 0) || (sesLSolicitud == null))
        {
            gridListadoSolicitudes.DataSource = ToDatatable.toDataTable(sesLSolicitud.ToList());
            gridListadoSolicitudes.DataBind();
            gridListadoSolicitudes.Visible = true;
        }
        else
            gridListadoSolicitudes.Visible = false;
        LimpiaControlesSolicitud();
        btnGuardar.Enabled = true;
    }

    
    #endregion Controles de solicitud

    #region Session Expedientes
    private void SetSessionExpte(string ExpteCaratula, string ExpteTram, string ExpteDigCu, string ExpteDocCu, string ExpteOrg, string ExptePreCu, string ExpteSec, string ExpteObservacion)
    {
        ActoresWS.Expediente_Solicitud _expe = new ActoresWS.Expediente_Solicitud();

        _expe.Caratula = ExpteCaratula;
        _expe.Expediente_ctipo = ExpteTram;
        _expe.Expediente_digcu = ExpteDigCu;
        _expe.Expediente_doccu = ExpteDocCu;
        _expe.Expediente_org = ExpteOrg;
        _expe.Expediente_precu = ExptePreCu;
        _expe.Expediente_sec = ExpteSec;
        _expe.Observacion = ExpteObservacion;

        List<ActoresWS.Expediente_Solicitud> le = sesExpedientes == null ? new List<ActoresWS.Expediente_Solicitud>() : (List<ActoresWS.Expediente_Solicitud>)sesExpedientes;
        if (HFtipoTxExpe.Value.ToString() != TiposEnumerados.TipoTx.Modificacion.ToString()) //agrega expediente a la lista
        {
            _expe.FAltaexpediente = System.DateTime.Today;
            le.Add(_expe);
        }
        else
        {
            foreach (ActoresWS.Expediente_Solicitud eb in le) //modifica expediente
            {
                if (eb.FAltaexpediente.ToShortDateString() == HFFaltaExpe.Value.ToString())
                {
                    le[le.IndexOf(eb)].Caratula = _expe.Caratula;
                    le[le.IndexOf(eb)].Expediente_ctipo = _expe.Expediente_ctipo;
                    le[le.IndexOf(eb)].Expediente_digcu = _expe.Expediente_digcu;
                    le[le.IndexOf(eb)].Expediente_doccu = _expe.Expediente_doccu;
                    le[le.IndexOf(eb)].Expediente_org = _expe.Expediente_org;
                    le[le.IndexOf(eb)].Expediente_precu = _expe.Expediente_precu;
                    le[le.IndexOf(eb)].Expediente_sec = _expe.Expediente_sec;
                    le[le.IndexOf(eb)].FAltaexpediente = eb.FAltaexpediente;
                    le[le.IndexOf(eb)].Observacion = _expe.Observacion;
                }
            }

        }

        sesExpedientes = le; //actualiza session de expedientes
    }
    #endregion


    #region Session Beneficios
    private void SetSessionBeneficio(string BenCopart, string BenDigVerif, string BenExCaja, string BenNumero, string BenTipo, string BenObservacion, byte? TipoTrDerivadoBen)
    {
        ActoresWS.Beneficio_Solicitud _ben = new ActoresWS.Beneficio_Solicitud();

        _ben.Observacion = BenObservacion;
        _ben.BenCopart = BenCopart;
        _ben.BenDigVerif = BenDigVerif;
        _ben.BenExCaja = BenExCaja;
        _ben.BenNumero = BenNumero;
        _ben.BenTipo = BenTipo;
        _ben.TipoTrDerivado = TipoTrDerivadoBen.Value;

        List<ActoresWS.Beneficio_Solicitud> lb = sesBeneficios == null ? new List<ActoresWS.Beneficio_Solicitud>() : (List<ActoresWS.Beneficio_Solicitud>)sesBeneficios;
        if (HFtipoTxBeneficio.Value.ToString() != TiposEnumerados.TipoTx.Modificacion.ToString()) //agrega beneficio a la lista
        {
            _ben.FAltaBeneficio = System.DateTime.Today;
            lb.Add(_ben);
        }
        else
        {
            foreach (ActoresWS.Beneficio_Solicitud eb in lb) //modifica beneficio
            {
                if (eb.FAltaBeneficio.ToShortDateString() == HFFaltaBeneficio.Value.ToString())
                {
                    lb[lb.IndexOf(eb)].BenCopart = _ben.BenCopart;
                    lb[lb.IndexOf(eb)].BenDigVerif = _ben.BenDigVerif;
                    lb[lb.IndexOf(eb)].BenExCaja = _ben.BenExCaja;
                    lb[lb.IndexOf(eb)].BenNumero = _ben.BenNumero;
                    lb[lb.IndexOf(eb)].BenTipo = _ben.BenTipo;
                    lb[lb.IndexOf(eb)].Observacion = _ben.Observacion;
                    lb[lb.IndexOf(eb)].TipoTrDerivado = _ben.TipoTrDerivado;
                    lb[lb.IndexOf(eb)].DTipoTrDerivado = _ben.DTipoTrDerivado;
                    lb[lb.IndexOf(eb)].FAltaBeneficio = eb.FAltaBeneficio; //pone la misma fecha
                }
            }
        }
        sesBeneficios = lb; //actualiza session de beneficios
    }
    #endregion

    #region Session Ingresos
    private void SetSessionIngresos(Int64 idBeneficiario, Int16 codPrestacion, String fIngreso, List<ActoresWS.TipoDocumentacion> iListTipo, byte cTipoIngreso, String dTipoIngreso, String ObservacionIng)
    {
        //Carga ingreso
        Ingresos ing = new Ingresos();
        ing.FechaIngreso = fIngreso.Equals(string.Empty) ? System.DateTime.Today : DateTime.Parse(fIngreso);
        ActoresWS.TipoIngreso  itipoing= new ActoresWS.TipoIngreso();
        itipoing.IdTipoIngreso = cTipoIngreso;
        itipoing.Descripcion = dTipoIngreso;
        ing.TipoIngreso = itipoing;
        ing.Observacion = ObservacionIng;
        
        ing.LTipoDocumentacion = iListTipo.ToArray();
        //FINCarga ingreso
        
        List<Ingresos> ei = new List<Ingresos>();
        if (sesIngresos != null)
            ei = (List<Ingresos>)sesIngresos;

        #region Actualiza la session
        ei.Add(ing);
        sesIngresos = ei;

        #endregion Actualiza la session

        //ACTUALIZA LA LISTA PARA MOSTRAR DE MOVIMIENTOS
        SetSessionIngDevMovShow(idBeneficiario, codPrestacion, string.Empty, string.Empty, ing.FechaIngreso.Value , string.Empty, ing.TipoIngreso.Descripcion, "RECEPCIÓN");

    }
    #endregion Session Ingresos

    #region Session Devoluciones
    private void SetSessionDevoluciones(Int64 idBeneficiario, Int16 codPrestacion, String destino, String observaciones, String certificado, List<ActoresWS.TipoDocumentacion> iListTipo)
    {
        //Carga devolucion
        Devolucion dev = new Devolucion();
        dev.Certificado = certificado;
        dev.Destino = destino;
        dev.FechaNotificacion = null;
        dev.FechaMovimiento = DateTime.Today;
        dev.FechaPresentacion = null;
        dev.Observaciones = observaciones;
        FaltanteDevolucion iFaltante = new FaltanteDevolucion();
        iFaltante.LTipoDocumentacionFaltante = iListTipo.ToArray();
        dev.TipoDocumentacionFaltante = iFaltante;
        //FINCarga devolucion
        
        List<Devolucion> ld = new  List<Devolucion>();
        if (sesDevoluciones != null)
            ld = (List<Devolucion>)sesDevoluciones;

        #region Actualiza la session
        ld.Add(dev);
        sesDevoluciones = ld;

        #endregion Actualiza la session

        //ACTUALIZA LA LISTA PARA MOSTRAR DE MOVIMIENTOS
        SetSessionIngDevMovShow(idBeneficiario, codPrestacion, dev.Destino, string.Empty, dev.FechaMovimiento, string.Empty, string.Empty, "DEVOLUCIÓN");

    }
    #endregion Session devoluciones

    #region Session Movimientos
    private void SetSessionMovimientos(Int64 idBeneficiario, Int16 codPrestacion, Int32 cestadon, String dEstado, String dSector , Int32 csector, String observaciones)
    {
        Movimiento_Solicitud _mov = new Movimiento_Solicitud();
        ActoresWS.Estado iEst = new ActoresWS.Estado();
        iEst.Cod_estado = cestadon;
        iEst.Descripcion = dEstado;
        ActoresWS.Sector iSec = new ActoresWS.Sector();
        iSec.Cod_sector = csector;
        iSec.Descripcion = dSector;
        _mov.Estado = iEst;
        _mov.Sector = iSec;
        _mov.Fecha_Movimiento = DateTime.Today;
        _mov.Observaciones = observaciones;

        List<Movimiento_Solicitud> ms = new List<Movimiento_Solicitud>();
        if (sesMovimientos != null)
            ms = (List<Movimiento_Solicitud>)sesMovimientos;

        #region Actualiza la session
        ms.Add(_mov);
        sesMovimientos = ms;

        #endregion Actualiza la session

        //ACTUALIZA LA LISTA PARA MOSTRAR DE MOVIMIENTOS
        SetSessionIngDevMovShow(idBeneficiario, codPrestacion, string.Empty, _mov.Estado.Descripcion, _mov.Fecha_Movimiento, _mov.Sector.Descripcion, string.Empty, "DERIVAR");

        

    }
    #endregion Session Movimientos

    #region Actualiza IngDevMov Para mostrar

    private void SetSessionIngDevMovShow(Int64 idBeneficiario, Int16 codPrestacion, String destino, String estado, DateTime fmov, String sector, String tingreso, String tmovimiento)
    {
        IngDevMov _idm = new IngDevMov();
        _idm.Cod_Prestacion = codPrestacion;
        _idm.Destino = destino;
        _idm.Estado = estado;
        _idm.Fecha_Movimiento = fmov;
        _idm.IdBeneficiario = idBeneficiario;
        _idm.Sector = sector;
        _idm.TipoIngreso = tingreso;
        _idm.TipoMovimiento = tmovimiento;
        
        List<IngDevMov> idm = new List<IngDevMov>();
        if (sesMovimShow != null)
            idm = (List<IngDevMov>)sesMovimShow;

        #region Actualiza la session
        idm.Add(_idm);
        sesMovimShow = idm;

        #endregion Actualiza la session

        //muestra lops movimientos
        if (sesMovimShow != null)
        {
            gridMovimientosSol.DataSource = ToDatatable.toDataTable(sesMovimShow);
            gridMovimientosSol.DataBind();
            gridMovimientosSol.Visible = true;
        }
        else
            gridMovimientosSol.Visible = false;
        dvDatosMovimientos.Visible = false;
    }

    #endregion Actualiza IngDevMov Para mostrar

    #region Session Solicitudes
    //private void SetSessionSolicitudes(bool deniega)
    private void SetSessionSolicitudes()
    {
        ActoresWS.Solicitud _Sol = new ActoresWS.Solicitud();

        _Sol.Mercosur = chkMercosurS.Checked;
        _Sol.CodigoPais = codPais;
        _Sol.PaisDescCompleto = descPais;

        _Sol.CodPrestacion = codPrestacion;
        _Sol.DescripcionPrestacion = descPrestacion;


        _Sol.Referencia_exterior = txtRefExteriorS.Text;
        _Sol.Ubicacion_Fisica = txtUbicFisicaS.Text;
        
        if (ddlMotivoDeniega.SelectedValue == "0")
        {
            _Sol.CodMotivo = null;
            _Sol.DescripcionMotivo = "";
        }
        else
        {
            _Sol.CodMotivo = Int16.Parse(ddlMotivoDeniega.SelectedValue);
            _Sol.DescripcionMotivo = ddlMotivoDeniega.SelectedItem.Text;
        }

        _Sol.Observaciones = txtObservacionS.Text.Trim();

        DateTime fecha;
        if (!txtFechaIngSolicitud.Text.Equals(string.Empty))
        {
            if (DateTime.TryParse(txtFechaIngSolicitud.Text, out fecha))
            {
                _Sol.FechaIngreso = Convert.ToDateTime(txtFechaIngSolicitud.Text);
            }
        }
        
        _Sol.IdBeneficiario = !idBeneficiario.HasValue ? 0 : idBeneficiario.Value;

        sesSolicitudnueva = _Sol;

        List<ActoresWS.Solicitud> ls = new List<ActoresWS.Solicitud>();
        if (sesLSolicitud != null)
            ls = (List<ActoresWS.Solicitud>)sesLSolicitud;

        #region Actualiza la session
        ls.Add(sesSolicitudnueva);
        LimpiaControlesSolicitud();
        sesLSolicitud = ls;

        #endregion Actualiza la session
    }


    #endregion

    #region Limpia controles
    private void LimpiaControlesExpte()
    {
        //txtExpteCaratula.Text = string.Empty;
        //txtExpteDigCu.Text = string.Empty;
        //txtExpteDocCu.Text = string.Empty;
        //txtExpteObservacion.Text = string.Empty;
        //txtExpteOrg.Text = string.Empty;
        //txtExptePreCu.Text = string.Empty;
        //txtExpteSec.Text = string.Empty;
        //txtExpteTram.Text = string.Empty;
    }

    //private void LimpiaControlesBeneficio()
    //{
    //    txtBenCopart.Text = string.Empty;
    //    txtBenDigVerif.Text = string.Empty;
    //    txtBenExCaja.Text = string.Empty;
    //    txtBenNumero.Text = string.Empty;
    //    txtBenObservacion.Text = string.Empty;
    //    txtBenTipo.Text = string.Empty;
    //    ddlTipoTrDerivadoBen.ClearSelection();
    //}

    private void LimpiaControlesSolicitud()
    {
        txtRefExteriorS.Text = string.Empty;
        txtUbicFisicaS.Text = string.Empty;
        chkMercosurS.Checked = false;
        chkMercosurS.Visible = false;
        ddlMotivoDeniega.ClearSelection();
        txtObservacionS.Text = string.Empty;
        txtFechaIngSolicitud.Text = string.Empty;
    }

    #endregion Limpia controles

    #region setControlesPopUpSSS
    //private void setControlesPopUpBenef()
    //{
    //    txtBenExCaja.Text = string.Empty;
    //    txtBenNumero.Text = string.Empty;
    //    txtBenTipo.Text = string.Empty;
    //    txtBenCopart.Text = string.Empty;
    //    txtBenDigVerif.Text = string.Empty;
    //    txtBenObservacion.Text = string.Empty;
    //    MErrorBenef.Text = string.Empty;

    //    ddlTipoTrDerivadoBen.ClearSelection();
    //}


    private void setControlesPopUpExped()
    {
        //txtExpteCaratula.Text = string.Empty;
        //txtExpteDigCu.Text = string.Empty;
        //txtExpteDocCu.Text = string.Empty;
        //txtExpteObservacion.Text = string.Empty;
        //txtExpteOrg.Text = string.Empty;
        //txtExptePreCu.Text = string.Empty;
        //txtExpteSec.Text = string.Empty;
        //txtExpteTram.Text = string.Empty;
        //MErrorExpe.Text = string.Empty;
    }

    #endregion setControlesPopUp

    #region Seleccio de pais y prestacion
    protected void ddlPrestacionesS_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerificarPaisPrestacion();
    }


    protected void ddlTipoIngreso_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtObservacionIngreso.Focus();
    }

    protected void ddlPaisS_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerificarPaisPrestacion();
    }


    protected void ddlMotivoDeniega_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtObservacionS.Focus();
    }



    private void VerificarPaisPrestacion()
    {
        //Trae documentacion exigida segun prestacion seleccionada
        if (!ddlPrestacionesS.SelectedValue.Equals("0") && !ddlPaisS.SelectedValue.Equals("0"))
        {
            chkMercosurS.Focus();
            btnGuardar.Enabled = true;
            pnlAllDato.Visible = true;
            dvSelNewPrestacionPais.Visible = false;
            codPrestacion = Int16.Parse(ddlPrestacionesS.SelectedValue);
            descPrestacion = ddlPrestacionesS.SelectedItem.Text;
            codPais = Int16.Parse(ddlPaisS.SelectedValue);
            descPais = ddlPaisS.SelectedItem.Text;
            cargargridDocPrestacion(codPrestacion);
            ddlTipoIngreso.Visible = true;
            ddlEstado.ClearSelection();
            ddlSector.ClearSelection();

            //setea la visibilidad del control mercosur dependiendo de si el pais seleccionado pertenece al mismo.
            List<PaisWS.Pais> iLPais = VariableSession.oPaisConvenios;
            PaisWS.Pais pmercos = iLPais.Find(delegate(PaisWS.Pais pais)
            {
                return pais.Pais_PK == codPais;
            }
            );
            if (pmercos != null)
                chkMercosurS.Visible = pmercos.Mercosur;
            else
                chkMercosurS.Visible = false;

            lbEstadoActual.Text = "No asignado";
            lbSectorActual.Text = "No asignado";
        }
        else
        {
            ddlPrestacionesS.Focus();
            btnGuardar.Enabled = false;
            pnlAllDato.Visible = false;
            descPrestacion = "";
            descPais = "";
            gridDocPrestacion.Visible = false;
            ddlTipoIngreso.Visible = false;
        }
        lbdescPrestacion.Text = descPrestacion;
        lbDescPaisS.Text = descPais;
    }

    #endregion Seleccio de pais y prestacion


    #region Boton Imprimir
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        #region Trae todos los datos a imprimir

        try
        {
            string mensajeer = "";
            Session["lBenef"] = InvocaWsDao.TraeBeneficiosXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);
            Session["lMovim"] = InvocaWsDao.TraeMovimientosResumen(idBeneficiario.Value, codPrestacion, out mensajeer);
            Session["lExped"] = InvocaWsDao.TraeExpedientesXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);
            Session["lSolic"] = InvocaWsDao.TraerSolicitudesXIdBeneficiario(idBeneficiario.Value, codPrestacion, out mensajeer);
            Session["ultMov"] = InvocaWsDao.TraeUltimoMovimientoSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);
            Session["ldocFaltante"] = InvocaWsDao.TraeTipoDocumentacionFaltanteXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);
            Session["lIngresos"] = InvocaWsDao.TraeIngresosXSolicitud(idBeneficiario.Value, codPrestacion, out mensajeer);

            if (mensajeer.Equals(string.Empty))
            {
                String script = "";
                script = "<script type='text/javascript'>" + "hidden = open('../Impresiones/PrintInfoCompletaPeticion.aspx?descPrestacion=" + descPrestacion + "&descApeNom=" + ApeNomBenef + "');" + "</script>";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", script, false);

            }
            else
            {
                MError.MensajeError = "Ocurrio un error intentar imprimir." + "</br>";
            }

        }
        catch (Exception er)
        {
            MError.MensajeError = "Ocurrio un error intentar imprimir." + "</br>" + er.Message;
        }
        #endregion Trae todos los datos a imprimir
        
    }
    #endregion Boton Imprimir

    #region Check expedientes
    protected void chbSeleccionTodo_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow fila in gvexpedtesANME.Rows)
        {
            CheckBox seleccion = (CheckBox)fila.FindControl("chbSeleccion");
            if (((CheckBox)sender).Checked == true) { seleccion.Checked = true; }
            else { seleccion.Checked = false; }
        }
        mpeIngExpediente.Show();
    }

    #endregion Check expedientes
}