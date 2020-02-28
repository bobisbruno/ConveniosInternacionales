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
using AuxiliaresWS;
using System.Transactions;



public partial class AltaProvisoria : DocumentacioIE
{
    private static readonly ILog log = LogManager.GetLogger(typeof(AltaProvisoria).Name);

    #region Sesiones - Viewstate

    public String NroTramite
    {
        get { return ViewState["_tram"].Equals(string.Empty) ? String.Empty : (String)ViewState["_tram"]; }
        set { ViewState["_tram"] = value; }
    }

    public List<DocumentacioIE> sesIlistaDocumentacion
    {
        get
        {
            return (List<DocumentacioIE>)ViewState["lDocumentacion"];
            
        }
        set
        {
            ViewState["lDocumentacion"] = value;
            
        }
    }

    #endregion Sesiones

    #region Trae Variable Ruta a Grabar
    public String RutaAGrabar
    {
        get
        {
            if (ViewState["RutaGr"] == null || ViewState["RutaGr"] == "")
            {
                ViewState["RutaGr"] = Invocar_RutaAGrabar();
                return (String)ViewState["RutaGr"];
            }
            else
                return (String)ViewState["RutaGr"];

        }
        set { ViewState["RutaGr"] = value; }
    }

    private String Invocar_RutaAGrabar()
    {
        //SolicitudProvisoriaMovimiento
        return InvocaWSExternos.ObtenerRutaAGrabarDeSistema(ConfigurationManager.AppSettings["Sistema"]).ToString();

    }
    #endregion Trae Variable Ruta a Grabar


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

        rvtxtFechaIngSolicitud.MaximumValue = DateTime.Today.ToString("dd/MM/yyyy");
        rvtxtFechaIngSolicitud.MinimumValue = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");

        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ddlPrestacionesS);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnAdd);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(dgArchivos);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnClear);
        
        if (!IsPostBack)
        {
            //Check de Seguridad tanto para ingresar como modificar instrumento
            #region Seguridad
            if (!AplicarSeguridad())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"].ToString());
            #endregion Seguridad

            InicializarDatosPagina("Ingreso de trámite provisorio", "> Gestión > Ingreso de trámite provisorio");
            MError.MensajeError = string.Empty;
            CargarCombos();
            blanquearPantalla();
        }
    }

    #region Carga TiposDoc
    
    private void cargarDDLDocPrestacion(Int16 codPrestacion, DropDownList ddl)
    {
        List<ActoresWS.TipoDocumentacion> oTipo = InvocaWsDao.TraeTipoDocumentacionXPrestacion(codPrestacion);
        if (oTipo != null)
        {
            ddl.DataSource = ToDatatable.toDataTable(oTipo);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("[Seleccióne documentación]", "0"));
        }
        else
            MError.MensajeError = "Error al traer listado de documentación";
    }
    #endregion Carga TiposDoc

    #region Carga Combos
    private bool CargarCombos()
    {
        string mensajeCarga = string.Empty;

        try
        {
            #region Combos tipos de documento
            List<TipoDocumento> oTipoDocumentoFrecuente = VariableSession.oTiposDocumentoFrecuentes;

            if (oTipoDocumentoFrecuente != null)
            {
                //de beneficiario
                ddltDocB.DataSource = oTipoDocumentoFrecuente;
                ddltDocB.DataBind();
                
                //ddltDocB.Items.Insert(0, new ListItem("[Seleccióne]", "0"));

            }
            else
                mensajeCarga += "Tipos de documento" + "</br>";
            #endregion

            List<AuxiliaresWS.Prestacion> oTipoPrestacion = VariableSession.oPrestaciones;

            if (oTipoPrestacion != null)
            {
                //de beneficiario
                ddlPrestacionesS.DataSource = oTipoPrestacion;
                ddlPrestacionesS.DataBind();
                ddlPrestacionesS.Items.Insert(0, new ListItem("[Seleccióne]", "0"));

            }
            else
                mensajeCarga += "Prestaciones" + "</br>";
                     
            ///Paises
            List<PaisWS.Pais> oPais = VariableSession.oPaisConvenios;

            if (oPais != null)
            {
                //de beneficiario
                ddlPaisConvenio.DataSource = oPais;
                ddlPaisConvenio.DataBind();
                ddlPaisConvenio.Items.Insert(0, new ListItem("[Sin definir]", "0"));

            }
            else
                mensajeCarga += "Países convenio" + "</br>";

            ////Combo Sectores
            List<AuxiliaresWS.Sector> oTipoSector = VariableSession.oSectores;
            if (oTipoSector != null)
            {
                ddlSector.DataTextField = "Descripcion";
                ddlSector.DataValueField = "Cod_sector";
                ddlSector.DataSource = oTipoSector;
                ddlSector.DataBind();
                ddlSector.Items.Insert(0, new ListItem("[Sin definir]", "0"));

            }
            else
                mensajeCarga += "Sectores" + "</br>";


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

    protected void ddlTingresoSelectedIndexCh(object sender, EventArgs e)
    {
        ddlTipoDocumentacion.Focus();
    }

    
    private void InicializarDatosPagina(string titulo, string txtBarraNav)
    {
        UtilsPresentacionX5.SetTextLabel(LblTituloPagina, titulo);
        BNav.Text = txtBarraNav;
        Page.Title = titulo;
    }

    #region SI
    protected void ClickearonSi(object sender, string quienLlamo)
    {
        switch (quienLlamo.Trim())
        {
            case "btnRegresar_Click":
                Response.Redirect("Main.aspx", false);
                break;

            case "btnAnular_Click":
                blanquearPantalla();
    
                break;
            case "AltaExitosa":

                break;

            case "SoloMuestra":
                break;
            case "btnGuardar_Click":
                GenerarComprobante();
                blanquearPantalla();
                break;

        }
        //Master.MensajeError = "";

    }
    #endregion SI

    #region NO
    protected void ClickearonNo(object sender, string quienLlamo)
    {
        switch (quienLlamo.Trim())
        {
            case "btnGuardar_Click":
                blanquearPantalla();
                break;
        }
    }
    #endregion NO


    #region generar comprobante HTML

    private void GenerarComprobante()
    {
        String script = "";
        script = "<script type='text/javascript'>" + "hidden = open('../Impresiones/ComprobanteTramite.aspx?nroSolicitud=" + NroTramite + "');" + "</script>";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", script, false);
    }


    #endregion generar comprobante HTML

    #region Blanqueo Pantalla

    private void blanquearPantalla()
    {
        MError.MensajeError = string.Empty;
        
        LimpiaControlesBeneficiario();
        LimpiarDatosDocumentacion();

        ddlPrestacionesS.ClearSelection();
        ddlPaisConvenio.ClearSelection();
        txtFechaIngSolicitud.Text = string.Empty;
        txtDatosReferencia.Text = string.Empty;
        
        ddlSector.ClearSelection();
        
        ddlTipoMovimiento.ClearSelection();

        txtApeNomB.Enabled = true;

        dvMovimientos.Visible = false;
        NroTramite = String.Empty;

        sesIlistaDocumentacion = null;
        ActualizarGrillaArchivos(sesIlistaDocumentacion);

        
    }

    
    #endregion Blanqueo Pantall

    #region Limpia controles
    private void LimpiaControlesBeneficiario()
    {
        txtApeNomB.Text = string.Empty;
    
        txtDocB.Text = string.Empty;
        ddltDocB.ClearSelection();
    
    }
    #endregion Limpia controles

   
    
    
    #region Boton Anular
    protected void btnAnular_Click(object sender, EventArgs e)
    {
        mensaje.DescripcionMensaje = "¿ Desea anular la operación ?";
        mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
        mensaje.QuienLLama = "btnAnular_Click";
        mensaje.Mostrar();
    }
    #endregion Boton Anular


    #region Guardar todo
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        MError.MensajeError = "";
        if (Page.IsValid)
        {
            GrabarDatos();
        }
    }


    protected void documentoExiste(object source, ServerValidateEventArgs args)
    {
        
        try
        {
            if (!txtDocB.Text.Equals(string.Empty))
                args.IsValid = !VerificarDocumentoExistente(txtDocB.Text, Int16.Parse( ddltDocB.SelectedValue));
            else
                args.IsValid = true;

        }
        catch (Exception)
        { }


    }

    
    protected void validaDocumentacion(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (sesIlistaDocumentacion==null)
                args.IsValid = false;
            else
                args.IsValid = true;

        }
        catch (Exception)
        { }
    }

    private void GrabarDatos()
    {

        string mensajeError = string.Empty;
        try
        {
            using (TransactionScope oScope = new TransactionScope())
            {

                #region Graba datos sql

                SolicitudProvisoria sp = new SolicitudProvisoria();
                //datos solicitud
                sp.ApellildoynombreDeclarado = txtApeNomB.Text;
                sp.DocumentoDeclarado = txtDocB.Text;
                sp.TipoDocumentoDeclarado = Int16.Parse(ddltDocB.SelectedValue);

                sp.FAltaProvisoria = DateTime.Parse( txtFechaIngSolicitud.Text);
                sp.FBajaProvisoria = null;
            
                ActoresWS.Prestacion iPrestacion = null; 
                if(!ddlPrestacionesS.SelectedValue.Equals("0"))
                {
                    iPrestacion = new ActoresWS.Prestacion();
                    iPrestacion.Cod_Prestacion = Int16.Parse(ddlPrestacionesS.SelectedValue);
                }
                sp.PrestacionSolicitada = iPrestacion;
            
                ActoresWS.Pais iPais = null; 
                if(!ddlPaisConvenio.SelectedValue.Equals("0"))
                {
                    iPais = new ActoresWS.Pais();
                    iPais.Pais_PK = Int16.Parse(ddlPaisConvenio.SelectedValue);
                }
                sp.PaisConvenio = iPais;

                sp.Nro_SolicitudProvisoria = string.Empty;
                sp.Referencia_Provisoria = txtDatosReferencia.Text;

                ActoresWS.Sector iSector = null;
                if (!ddlSector.SelectedValue.Equals("0"))
                {
                    iSector = new ActoresWS.Sector();
                    iSector.Cod_sector = int.Parse(ddlSector.SelectedValue);
                }
                sp.Sectorderiva = iSector;

                sp.TIngresoProvisorio = ddlTipoMovimiento.SelectedValue.Equals("1") ? TipoIngresoProvisorio.Ingreso : TipoIngresoProvisorio.Devolucion;

                // FIN datos solicitud

                string mensajeErr = string.Empty;
                string nroSolicitud = string.Empty;
                InvocaWsDao.SolicitudProvisoria_Alta(sp, out mensajeErr, out nroSolicitud);

                NroTramite = nroSolicitud;

                //movimientos
                List<SolicitudProvisoriaMovimiento> iLMovimiento = new List<SolicitudProvisoriaMovimiento>();
                SolicitudProvisoriaMovimiento movimientoTemp = null;

                //recorro los movimientos registrados en memoria y armo el objeto para tx
                Int16 secuencia = 0;
                string ruta = Invocar_RutaAGrabar();
                foreach (DocumentacioIE doc in sesIlistaDocumentacion)
                {
                    if (doc.LArchivo != null)//carga novedad con archivo digital
                    {
                        foreach (HttpPostedFile archivo in doc.LArchivo)
                        {
                            movimientoTemp = new SolicitudProvisoriaMovimiento();                        

                            movimientoTemp.DescripcionBreve = doc.ComentarioIngreso;
                            movimientoTemp.Digitalizado = true;
                            movimientoTemp.Nro_SolicitudProvisoria = NroTramite;
                            movimientoTemp.TipoDocumentacion = doc.TipoDocumentacion;
                            
                            movimientoTemp.SecuenciaDocumento = secuencia;
                     
                            iLMovimiento.Add(movimientoTemp);

                            //guardo documento
                            archivo.SaveAs(ruta + "\\" + archivo.FileName);
                            if (File.Exists(ruta + "\\" + archivo.FileName))
                            {
                                //grabo datos hosting
                                InvocaWSExternos.GrabarArchivoHost(NroTramite + doc.TipoDocumentacion.CodTipoDocumentacion + secuencia, archivo.FileName, ruta);
                            }
                            else
                            {
                                MError.MensajeError = "Ocurrio un error al grabar documento " + archivo.FileName;
                            }

                            secuencia++;
                            
                        }
                    }
                    else //carga novedad sin archivo digital
                    {
                        movimientoTemp = new SolicitudProvisoriaMovimiento();                        
                        
                        movimientoTemp.DescripcionBreve = doc.ComentarioIngreso;
                        movimientoTemp.Digitalizado = false;
                        movimientoTemp.Nro_SolicitudProvisoria = NroTramite;
                        movimientoTemp.TipoDocumentacion = doc.TipoDocumentacion;
                        
                        movimientoTemp.SecuenciaDocumento = secuencia;
                        iLMovimiento.Add(movimientoTemp);

                        secuencia++;
                    }
                    
                }

                // FINmovimientos

                InvocaWsDao.SolicitudesProvisoriaMovimiento_Alta(iLMovimiento, out mensajeErr);
                
                #endregion Graba datos sql

                
    
                if (mensajeErr.Equals(string.Empty))
                {
                    //generar html adjunto y pdf con el comprobante de trámite
                    mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
                    mensaje.DescripcionMensaje = "Se generó el trámite provisorio con el nro: " + NroTramite + ".</br>" + "¿ Deséa imprimir comprobante ?";

                    mensaje.QuienLLama = "btnGuardar_Click";
                    mensaje.Mostrar();
                }
                else
                    MError.MensajeError = mensajeError;

                oScope.Complete();
                oScope.Dispose();
            }
           
        }
        catch (IOException er)
        {
            //MError.MensajeError = "Ocurrio un error de ES al grabar los dictamenes. Revisar el log para mayores detalles.";
            log.Error(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), er.Source, er.Message, er.GetType().ToString()));

        }
        catch (Exception er)
        {
            log.Error(er.Message);
            MError.MensajeError = er.Message;
        }
    }
    #endregion Guardar todo

    
    #region Verifica ingreso documentacion

    //private bool HayDocIngresada(GridView gv)
    //{
    //    bool checkAny = false;
    //    foreach (GridViewRow gvr in gv.Rows)
    //    {
    //        CheckBox chk = (CheckBox)gvr.Cells[1].FindControl("chkSelDoc");
    //        if (chk.Checked)
    //            checkAny = true;
    //    }
    //    return checkAny;
    //}

    #endregion Verifica ingreso documentacion


    #region Seleccio de pais y prestacion
    protected void ddlPrestacionesS_SelectedIndexChanged(object sender, EventArgs e)
    {
            if (!ddlPrestacionesS.SelectedValue.Equals("0"))
        {
            btnGuardar.Enabled = true;
            ddlTipoMovimiento.Focus();
            dvMovimientos.Visible = true;
            //ddlPrestacionesS.Enabled = false;

            //carga combos documentacion segun prestacion

            cargarDDLDocPrestacion(Int16.Parse(ddlPrestacionesS.SelectedValue), ddlTipoDocumentacion);
        }
        else
        {
            dvMovimientos.Visible = false;
            //ddlPrestacionesS.Enabled = true;
            btnGuardar.Enabled = false;
        }

    }
    

    #endregion Seleccio de pais y prestacion

    #region Regresar
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        mensaje.DescripcionMensaje = "¿ Desea regresar a la pantalla principal ?";
        mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
        mensaje.TextoBotonCancelar = "Continuar";
        mensaje.QuienLLama = "btnRegresar_Click";
        mensaje.Mostrar();
    }
    #endregion Regresar

    #region Verifica Existencia Doc

    private bool VerificarDocumentoExistente(string documento, Int16 codDocumento)
    {
        bool existe = false;
        string msjerror = "";
        existe = InvocaWsDao.ExisteDocumento(documento, codDocumento, out msjerror);
        MError.MensajeError = msjerror;

        return existe;
    }

    #endregion Verifica Existencia Doc

    #region Tratamiento Archivos
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        List<DocumentacioIE> ilista = sesIlistaDocumentacion;
        if (ilista == null)
            ilista = new List<DocumentacioIE>();
        DocumentacioIE documentoNuevo = new DocumentacioIE();
        try
        {
            ActoresWS.TipoDocumentacion itd = new ActoresWS.TipoDocumentacion();
            itd.CodTipoDocumentacion = Int32.Parse(ddlTipoDocumentacion.SelectedValue);
            itd.Descripcion = ddlTipoDocumentacion.SelectedItem.Text;
            documentoNuevo.TipoDocumentacion = itd;

            documentoNuevo.ComentarioIngreso = txtDatosDocumentacion.Text;

            documentoNuevo.LArchivo = IpFile.HasFiles ? Util.CargaSoloImagenes( IpFile.PostedFiles.ToList()) : null;

            ilista.Add(documentoNuevo);
            sesIlistaDocumentacion = ilista; //actualizo la session

            ActualizarGrillaArchivos(sesIlistaDocumentacion);
            dgArchivos.Focus();

            LimpiarDatosDocumentacion();
        }
        catch (Exception er)
        {
            throw er;
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        sesIlistaDocumentacion = null;
        
        ActualizarGrillaArchivos(sesIlistaDocumentacion);
        ddlTipoDocumentacion.Focus();
    }

    private void LimpiarDatosDocumentacion()
    {
        ddlTipoDocumentacion.ClearSelection();
        txtDatosDocumentacion.Text = string.Empty;
        IpFile.Dispose();
    }

    

    #region Actualiza grilla
    private void ActualizarGrillaArchivos(List<DocumentacioIE> ilista)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("TipoDocumentacion", typeof(String));
        _dt.Columns.Add("Tamano", typeof(String));
        _dt.Columns.Add("ArchivosDigitalizados", typeof(String));
        _dt.Columns.Add("Comentario", typeof(String));
        
        
        //foreach (var archivo in IpFile.PostedFiles)
        if (ilista != null)
        {
            if (ilista.Count != 0)
            {
                foreach (DocumentacioIE dIE in ilista)
                {
                    DataRow _drTemp;
                    _drTemp = _dt.NewRow();
                    _drTemp["TipoDocumentacion"] = dIE.TipoDocumentacion.Descripcion;
                    _drTemp["Tamano"] = dIE.LArchivo == null ? "" :  Util.calculoTamanioArchivos(dIE.LArchivo).ToString() + " Kb";
                    _drTemp["ArchivosDigitalizados"] = dIE.LArchivo == null ? "" : dIE.LArchivo.Count > 1 ? dIE.LArchivo == null ? "No registra" : dIE.LArchivo.Count.ToString() + " archivos" : dIE.LArchivo.First().FileName;
                    _drTemp["Comentario"] = dIE.ComentarioIngreso;
                    

                    _dt.Rows.Add(_drTemp);
                }
            }
        }
        dgArchivos.DataSource = _dt;
        dgArchivos.DataBind();
        dgArchivos.Visible = true;
        
    }


    #endregion Actualiza grilla

    #region Comandos grilla archivos
    protected void RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string arg = "";
        arg = e.CommandArgument.ToString();


        if (e.CommandName == "QuitarDoc")
        {
            Int32 indice = Int32.Parse(e.CommandArgument.ToString());
            List<DocumentacioIE> iLista = sesIlistaDocumentacion;
            iLista.RemoveAt(indice);
            sesIlistaDocumentacion = iLista;
            ActualizarGrillaArchivos(sesIlistaDocumentacion);
            dgArchivos.Focus();
        }

    }

    #endregion Comandos grilla


    #endregion Tratamiento Archivos

}