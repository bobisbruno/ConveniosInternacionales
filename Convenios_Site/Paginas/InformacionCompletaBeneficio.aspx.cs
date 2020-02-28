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
using ActoresWS;
//using ActoresWS;
using System.Threading;
using System.IO;

public partial class Paginas_InformacionCompleta : System.Web.UI.Page
{
    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }

    protected Beneficiario iBeneficiario;

    private Beneficiario BeneficiarioActual
    {
        get { return Session["_BenefActual"] == null ? new Beneficiario() : (Beneficiario)Session["_BenefActual"]; }
        set { Session["_BenefActual"] = value; }
    }

    private string codPrestacion
    {
        get { return (string)ViewState["_codPrestacion"]; }
        set { ViewState["_codPrestacion"] = value; }
    }

    private string codPais
    {
        get { return (string)ViewState["_codPais"]; }
        set { ViewState["_codPais"] = value; }
    }

    private string idBeneficiario
    {
        get { return (string)ViewState["_idBen"]; }
        set { ViewState["_idBen"] = value; }
    }

    private string descPrestacion
    {
        get { return (string)ViewState["_descPrestacion"]; }
        set { ViewState["_descPrestacion"] = value; }
    }

    private string descPais 
    {
        get { return (string)ViewState["_dPais"]; }
        set { ViewState["_dPais"] = value; }
    }

    private bool verBtnModificaSolicitud
    {
        get { return (bool)ViewState["_vBms"]; }
        set { ViewState["_vBms"] = value; }
    }

    #region APlica Seguridad
    private bool AplicarSeguridadBotonModificar()
    {
        bool permiso = false;
        try
        {
            permiso = TienePermiso("botonModifica");
        }
        catch (ThreadAbortException)
        { }
        return permiso;
    }

    private bool AplicarSeguridadColEliminar()
    {
        bool permiso = false;
        try
        {
            permiso = TienePermiso("colEliminar");
        }
        catch (ThreadAbortException)
        { }
        return permiso;
    }

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
    #endregion APlica Seguridad Pagina


    protected void Page_Load(object sender, EventArgs e)
    {
        mensaje.ClickSi += new Mensaje.Click_UsuarioSi(ClickearonSi);
        mensaje.ClickNo += new Mensaje.Click_UsuarioNo(ClickearonNo);

        if (!IsPostBack)
        {
            #region Aplica segurida de la pagina y componentes
            if (!AplicarSeguridadPagina())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"]);

            bool botonesAltayModifica = AplicarSeguridadBotonModificar();
            bool columnaEliminar = AplicarSeguridadColEliminar();
            btnModificarBeneficiario.Visible = botonesAltayModifica;
            btnAgregarSolicitud.Visible = botonesAltayModifica;
            verBtnModificaSolicitud = botonesAltayModifica;
            gvPrestacionesBeneficiario.Columns[8].Visible = columnaEliminar;
            
            #endregion Aplica segurida de la pagina y componentes

            InicializarDatosPagina("Datos del Solicitante", "> Datos del Solicitante");
            //Session["_toPrintDetalle"] = null;
            Buscar();
        }
            
    }

    #region Inicializa Datos Pagina
    private void InicializarDatosPagina(string titulo, string txtBarraNav)
    {
        BNav.Text = txtBarraNav;
        //UtilsPresentacionX5.SetPaginationProperties(gv_Grilla);
        Page.Title = titulo;
    }
    #endregion Inicializa Datos Pagina

    #region Buscar

    protected void Buscar()
    {
        string mensaje = "";
        try
        {
            idBeneficiario = Request.QueryString["idBeneficiario"];
            iBeneficiario = InvocaWsDao.TraerBeneficiario(Int64.Parse(idBeneficiario), out mensaje);
            
            //Aviso de error no controlado
            if(! mensaje.Equals(string.Empty))
                MError.MensajeError = mensaje;
            else
                MError.MensajeError = string.Empty;

            if (iBeneficiario != null)
            {
                //guarda en la sesion los datos de la consulta actual
                BeneficiarioActual = iBeneficiario;
                //Guarda en session los datos del instrumento para tomarlos en la pag de impresion
                Session["_toPrint"] = (Beneficiario)iBeneficiario;
                //Datos del Besneficiario
                lblCodSIACI.Text = iBeneficiario.ExpedienteExterno.ToUpper();
                lblCuipB.Text = iBeneficiario.Cuip;
                lbApellidoM.Text = iBeneficiario.ApellMaterno;
                lbApeNom.Text = iBeneficiario.ApeNom;
                lbFechaNacimiento.Text = iBeneficiario.Fecha_nac.HasValue ? iBeneficiario.Fecha_nac.Value.ToShortDateString() : "";
                LbPais.Text = iBeneficiario.Pais_Nacionalidad == null ? "" : iBeneficiario.Pais_Nacionalidad.Gentilicio;
                if (iBeneficiario.Sexo != "")
                    LbSexo.Text = iBeneficiario.Sexo.ToUpper().Equals("M") ? "Masculino" : "Femenino";
                else
                    LbSexo.Text = "";
                lbDirCalleBen.Text = iBeneficiario.DirCalle;
                lbDirNumBen.Text = iBeneficiario.DirNum;
                lbDirPisoBen.Text = iBeneficiario.Piso;
                lbDirDeptoBen.Text = iBeneficiario.Departamento;
                lbEcalleB1.Text = iBeneficiario.ECalle1;
                lbEcalleB2.Text = iBeneficiario.ECalle2;
                lbProvLocalidadBen.Text = iBeneficiario.Ubicacion == null ? iBeneficiario.Ciudad : iBeneficiario.Ubicacion.DescripcionLocalidad + "-" + iBeneficiario.Ciudad + "(" + iBeneficiario.Ubicacion.DescripcionProvincia + ")" + "CP:" + iBeneficiario.CodPostal;

                //Direccion extranjera
                if (iBeneficiario.OdirExtranjera == null)
                {
                    lbDirExtranjera.Text = "";
                    lbCiudadExtranjera.Text = "";
                }
                else
                {
                    lbDirExtranjera.Text = iBeneficiario.OdirExtranjera.Dircalle + " " + iBeneficiario.OdirExtranjera.Dirnum + " piso " +
                    iBeneficiario.OdirExtranjera.Piso + " dpto " + iBeneficiario.OdirExtranjera.Depto + ". Entre " + iBeneficiario.OdirExtranjera.Ecalle1 + " y " + iBeneficiario.OdirExtranjera.Ecalle2;
                    lbCiudadExtranjera.Text = iBeneficiario.OdirExtranjera.Ciudad + " - " + 
                    iBeneficiario.OdirExtranjera.Distrito + " (" + iBeneficiario.OdirExtranjera.CodPostal + ") - " + iBeneficiario.OdirExtranjera.NomCiudad + " - " + iBeneficiario.OdirExtranjera.Estado + " - " + iBeneficiario.OdirExtranjera.NomPais;
                }

                if (iBeneficiario.LDocumentosBeneficiario == null || iBeneficiario.LDocumentosBeneficiario.Length == 0)
                    dvconDocumentos.Visible = false;
                else
                {
                    dvconDocumentos.Visible = true;
                    gridListadoDocBeneficiarios.DataSource = ToDatatable.toDataTable(iBeneficiario.LDocumentosBeneficiario.ToList());
                    gridListadoDocBeneficiarios.DataBind();
                }

                //Datos Solicitudes realizadas
                if (iBeneficiario.LPrestacionBeneficiario == null || iBeneficiario.LPrestacionBeneficiario.Length == 0)
                {
                    dvConSolicitudes.Visible = false;
                    dvSinSolicitudes.Visible = true;
                }
                else
                {
                    dvConSolicitudes.Visible = true;
                    dvSinSolicitudes.Visible = false;
                    //cargar gridview en solicitudes
                    gvPrestacionesBeneficiario.DataSource = (DataTable)ToDatatable.toDataTable(iBeneficiario.LPrestacionBeneficiario.ToList());
                    gvPrestacionesBeneficiario.DataBind();

                }

                #region Otros Datos
                //Datos del Causante
                if (iBeneficiario.Causante != null)
                {
                    lblApeNomC.Text = iBeneficiario.Causante.ApeNom;
                    lblFechaNacC.Text = iBeneficiario.Causante.Fecha_Nacimiento.HasValue ? iBeneficiario.Causante.Fecha_Nacimiento.Value.ToShortDateString() : "";
                    lblCuipC.Text = iBeneficiario.Causante.Cuip;
                    lblFechaDefuncionC.Text = iBeneficiario.Causante.Fecha_Def.ToShortDateString();
                    if(iBeneficiario.Causante.Sexo != "")
                        lblsexoC.Text = iBeneficiario.Causante.Sexo.ToUpper().Equals("M") ? "Masculino":"Femenino";
                    else 
                        lblsexoC.Text = "";
                    
                    if (iBeneficiario.Causante.LDocCausante == null || iBeneficiario.Causante.LDocCausante.Length == 0)
                        dvCconDocumentos.Visible = false;
                    else
                    {
                        dvCconDocumentos.Visible = true;
                        gridListadoDocCausantes.DataSource = (DataTable)ToDatatable.toDataTable(iBeneficiario.Causante.LDocCausante.ToList());
                        gridListadoDocCausantes.DataBind();
                    }
                    dvConCausante.Visible = true;
                }
                else
                    dvConCausante.Visible = false;

                //DatosApoderados
                if (iBeneficiario.LApoderado == null || iBeneficiario.LApoderado.Length == 0)
                {
                    dvConApoderados.Visible = false;
                    
                }
                else
                {
                    dvConApoderados.Visible = true;
                    rptApoderados.DataSource = iBeneficiario.LApoderado;
                    rptApoderados.DataBind();
                }

                
                MError.MensajeError = mensaje;

                #endregion Otros Datos


                //nuevo -------Solicitudes provisorias
                #region Solicitudes provisorias
                if (iBeneficiario.LSolicitudProvisoria == null || iBeneficiario.LSolicitudProvisoria.Length == 0)
                {
                    //dvConSolicitudesProvisorias.Visible = false;
                    //dvSinSolicitudesProvisorias.Visible = true;
                }
                else
                {
                    //dvConSolicitudesProvisorias.Visible = true;
                    //dvSinSolicitudesProvisorias.Visible = false;
                    //cargar gridview en solicitudes
                    //gvSolictudesProvisorias.DataSource = (DataTable)ToDatatable.toDataTable(iBeneficiario.LSolicitudProvisoria.ToList());
                    //gvSolictudesProvisorias.DataBind();
                }

                #endregion Solicitudes provisorias

                divdatosBeneficiario.Visible = true;
                divBeneficiarioErr.Visible = false;
                btnImprimir.Enabled = true;
            }
            else
            {
                BeneficiarioActual = null;
                Session["_toPrint"] = null;
                Session["_toPrintNotas"] = null;
                divBeneficiarioErr.Visible = true;
                divdatosBeneficiario.Visible = false;
                btnImprimir.Enabled = false;
            }
        }
        catch
        {
            BeneficiarioActual = null;
            Session["_toPrint"] = null;
            Session["_toPrintNotas"] = null;
        }
    }
    #endregion

    #region Databound Solicitudes

    #region SI
    protected void ClickearonSi(object sender, string quienLlamo)
    {
        switch (quienLlamo.Trim())
        {
            case "CommandEliminarSolicitud":
                //elimina la solicitud
                BajaSolicitud(Int64.Parse(idBeneficiario), Int16.Parse(codPrestacion));

                break;
        }
    }
    #endregion SI

    #region NO
    protected void ClickearonNo(object sender, string quienLlamo)
    {

    }
    #endregion NO

    private void BajaSolicitud(Int64 idBeneficiario, Int16 codPrestacion)
    {
        string merror = "";
        InvocaWsDao.BajaSolicitud(idBeneficiario, codPrestacion, out merror);
        MError.MensajeError = merror;
        if (merror == string.Empty)
            CargarSolicitudes();
    }

    protected void gvPrestacionesBeneficiario_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            // Retrieve the state value for the current row. 
            Image btnMercoS = (Image)e.Row.Cells[6].FindControl("imgMercoS");
            btnMercoS.Visible = (Boolean)rowView["Mercosur"];

            Image btnModificaSol = (Image)e.Row.Cells[0].FindControl("btnModificarDatosPrestacion");
            Image btnNotifica = (Image)e.Row.Cells[0].FindControl("btnNotificar");
            btnModificaSol.Visible = verBtnModificaSolicitud;
            btnNotifica.Visible = verBtnModificaSolicitud;
        }
    }
    #endregion

    #region RowCommand Solicitudes

    protected void RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] arg = new string[3];
        arg = e.CommandArgument.ToString().Split(';');
        codPrestacion = arg[0];
        descPrestacion = arg[1];
        descPais = arg[2];
        codPais = arg[3];

        if (e.CommandName == "Eliminar")
        {
            mensaje.DescripcionMensaje = "Esta acción eliminará TODOS los datos correspondientes a la solicitud" + "</br>"
                + descPrestacion + " - " + descPais + "</br>" + "¿ Procede a eliminarla ?";
            mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
            mensaje.QuienLLama = "CommandEliminarSolicitud";
            mensaje.Mostrar();
        }
        else
        {
            string encabezadoBeneficiario = "";
            encabezadoBeneficiario += BeneficiarioActual.ApeNom;
            encabezadoBeneficiario += BeneficiarioActual.Cuip.Equals("") ? "" : " - " + BeneficiarioActual.Cuip;
            encabezadoBeneficiario += BeneficiarioActual.ExpedienteExterno.Equals("") ? "" : " - C. SIACI " + BeneficiarioActual.ExpedienteExterno;
            //Response.Redirect("AMSolicitud.aspx?encabezadoBenef=" + encabezadoBeneficiario + "&idBeneficiario=" + HFidBeneficiario.Value, false);
            //tomo los parametros del row selected
            

            
            String script = "";
            if (e.CommandName == "VerDatos")
            {
                script = "<script type='text/javascript'>" + "hidden = open('InfoCompletaPeticion.aspx?codPrestacion=" + codPrestacion + "&idBeneficiario=" + idBeneficiario + "&descPrestacion=" + descPrestacion + "&descApeNom=" + encabezadoBeneficiario + "&codPais=" + codPais + "&descPais=" + descPais + "');" + "</script>";
            }

            if (e.CommandName == "ModificarDatos")
            {
                script = "<script type='text/javascript'>" + "hidden = open('AMSolicitud.aspx?codPrestacion=" + codPrestacion + "&idBeneficiario=" + idBeneficiario + "&descPrestacion=" + descPrestacion + "&descApeNom=" + encabezadoBeneficiario + "&codPais=" + codPais + "&descPais=" + descPais + "&cuip=" + BeneficiarioActual.Cuip + "');" + "</script>";
            }

            if (e.CommandName == "Notificar")
            {
                script = "<script type='text/javascript'>" + "hidden = open('ANotificacion.aspx?codPrestacion=" + codPrestacion + "&idBeneficiario=" + idBeneficiario + "&descPrestacion=" + descPrestacion + "&descApeNom=" + encabezadoBeneficiario + "&codPais=" + codPais + "&descPais=" + descPais + "');" + "</script>";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", script, false);
        }
    }



    protected void btnActualizarSolicitud_Click(object sender, EventArgs e)
    {
        CargarSolicitudes();
    }

    private void CargarSolicitudes()
    {
        string mensajeErr = "";
        List<ActoresWS.PrestacionBeneficiario> oList = InvocaWsDao.TraePrestacionesXIdBeneficiario(Int64.Parse(idBeneficiario), out mensajeErr);
        if (oList == null || oList.Count == 0)
        {
            dvConSolicitudes.Visible = false;
            dvSinSolicitudes.Visible = true;
        }
        else
        {
            gvPrestacionesBeneficiario.DataSource = ToDatatable.toDataTable(oList);
            gvPrestacionesBeneficiario.DataBind();
            dvConSolicitudes.Visible = true;
            dvSinSolicitudes.Visible = false;
        }
    }

    protected void btnAgregarSolicitud_Click(object sender, EventArgs e)
    {
        string encabezadoBeneficiario = "";
        encabezadoBeneficiario += BeneficiarioActual.ApeNom;
        encabezadoBeneficiario += BeneficiarioActual.Cuip.Equals("") ? "" : " - " + BeneficiarioActual.Cuip;
        encabezadoBeneficiario += BeneficiarioActual.ExpedienteExterno.Equals("") ? "" : " - C. SIACI " + BeneficiarioActual.ExpedienteExterno;
        String script = "<script type='text/javascript'>" + "hidden = open('AMSolicitud.aspx?codPrestacion=" + "" + "&idBeneficiario=" + idBeneficiario + "&descPrestacion=" + "" + "&descApeNom=" + encabezadoBeneficiario + "&codPais=" + "" + "&descPais=" + "" + "&cuip=" + BeneficiarioActual.Cuip + "');" + "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", script, false);
    }

    #endregion RowCommand Solicitudes

    #region Item Databound Repeater

    protected void rptProvisorias_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item; // elemento del Repeater

        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        {
            SolicitudProvisoria oSolicitud = (SolicitudProvisoria)e.Item.DataItem;

            //HiddenField hfIdCodPrestacion = (HiddenField)item.FindControl("hfIdCodPrestacion"); // obtenemos el control.
            //hfIdCodPrestacion.Value = oSolicitud.Cod_Prestacion.ToString();

            //Label lblnroSolicitud = (Label)item.FindControl("lblnroSolicitud"); // obtenemos el control.
            //lblnroSolicitud.Text = oSolicitud.Nro_SolicitudProvisoria;

            //Label lblTramite = (Label)item.FindControl("lblTramite"); // obtenemos el control.
            //lblTramite.Text = oSolicitud.PrestacionSolicitada == null ? string.Empty : oSolicitud.PrestacionSolicitada.Descripcion;

            //Label lblSTipoApod = (Label)item.FindControl("lblSTipoApod"); // obtenemos el control.
            //lblSTipoApod.Text = oApoderado.StipoApoderado == null ? "" : oApoderado.StipoApoderado.Descripcion;

            //Label lblTipoApoderadoAp = (Label)item.FindControl("lblTipoApoderadoAp"); // obtenemos el control.
            //lblTipoApoderadoAp.Text = oApoderado.TipoApoderado == null ? "" : oApoderado.TipoApoderado.Descripcion;

            //Label lblEmailAp = (Label)item.FindControl("lblEmailAp"); // obtenemos el control.
            //lblEmailAp.Text = oApoderado.EMail;

            //Label lblTelefonoAp = (Label)item.FindControl("lblTelefonoAp"); // obtenemos el control.
            //lblTelefonoAp.Text = oApoderado.Telefono;

            //Label lblObservaciones = (Label)item.FindControl("lblObservaciones"); // obtenemos el control.
            //lblObservaciones.Text = oApoderado.Comentario;

            //Label lbNumDocA = (Label)item.FindControl("lbNumDocA"); // obtenemos el control.
            //lbNumDocA.Text = oApoderado.NumDoc;

            //Label lbCodAbrevTdocA = (Label)item.FindControl("lbCodAbrevTdocA"); // obtenemos el control.
            //lbCodAbrevTdocA.Text = oApoderado.AbrevDoc;

            //Label lbFalta = (Label)item.FindControl("lbFalta"); // obtenemos el control.
            //lbFalta.Text = oApoderado.FAlta.ToShortDateString();

            //Label lbDirCalleAp = (Label)item.FindControl("lbDirCalleAp"); // obtenemos el control.
            //lbDirCalleAp.Text = oApoderado.DirCalle;

            //Label lbDirNumAp = (Label)item.FindControl("lbDirNumAp"); // obtenemos el control.
            //lbDirNumAp.Text = oApoderado.DirNum;

            //Label lbDirPisoAp = (Label)item.FindControl("lbDirPisoAp"); // obtenemos el control.
            //lbDirPisoAp.Text = oApoderado.Piso;

            //Label lbDirDeptoAp = (Label)item.FindControl("lbDirDeptoAp"); // obtenemos el control.
            //lbDirDeptoAp.Text = oApoderado.Departamento;

            //Label lbDirEC1Ap = (Label)item.FindControl("lbDirEC1Ap"); // obtenemos el control.
            //lbDirEC1Ap.Text = oApoderado.EntreCalle1;

            //Label lbDirEC2Ap = (Label)item.FindControl("lbDirEC2Ap"); // obtenemos el control.
            //lbDirEC2Ap.Text = oApoderado.EntreCalle2;

            //Label lbProvLocalidadAp = (Label)item.FindControl("lbProvLocalidadAp"); // obtenemos el control.
            //lbProvLocalidadAp.Text = oApoderado.DirUbicacion == null ? oApoderado.Ciudad : oApoderado.DirUbicacion.DescripcionLocalidad + "-" + oApoderado.Ciudad + "(" + oApoderado.DirUbicacion.DescripcionProvincia + ")" + "CP:" + oApoderado.Cod_postal;

        }
    }

    protected void rptApoderados_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item; // elemento del Repeater

        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        {
            Apoderado oApoderado = (Apoderado)e.Item.DataItem;

            Label lblNomApeApod = (Label)item.FindControl("lblApeNomAp"); // obtenemos el control.
            lblNomApeApod.Text = oApoderado.ApeNom;

            Label lblBancoAp = (Label)item.FindControl("lblBancoAp"); // obtenemos el control.
            lblBancoAp.Text = oApoderado.Banco == null?"":oApoderado.Banco.Descripcion;

            Label lblTipoPoderAp = (Label)item.FindControl("lblTipoPoderAp"); // obtenemos el control.
            //lblTipoPoderAp.Text = oApoderado.TipoPoder == null ? "" : oApoderado.TipoPoder.Descripcion;
            string tPoder = oApoderado.TipoApoderado.PoderTramitar ? "Tramitar" : "";
            tPoder += oApoderado.TipoApoderado.PoderPercibir ? " Percibir" : "";
            lblTipoPoderAp.Text = tPoder;

            Label lblSTipoApod = (Label)item.FindControl("lblSTipoApod"); // obtenemos el control.
            lblSTipoApod.Text = oApoderado.StipoApoderado == null ? "" : oApoderado.StipoApoderado.Descripcion;

            Label lblTipoApoderadoAp = (Label)item.FindControl("lblTipoApoderadoAp"); // obtenemos el control.
            lblTipoApoderadoAp.Text = oApoderado.TipoApoderado == null ? "" : oApoderado.TipoApoderado.Descripcion;

            Label lblEmailAp = (Label)item.FindControl("lblEmailAp"); // obtenemos el control.
            lblEmailAp.Text = oApoderado.EMail;

            Label lblTelefonoAp = (Label)item.FindControl("lblTelefonoAp"); // obtenemos el control.
            lblTelefonoAp.Text = oApoderado.Telefono;

            Label lblObservaciones = (Label)item.FindControl("lblObservaciones"); // obtenemos el control.
            lblObservaciones.Text = oApoderado.Comentario;

            Label lbNumDocA = (Label)item.FindControl("lbNumDocA"); // obtenemos el control.
            lbNumDocA.Text = oApoderado.NumDoc;

            Label lbCodAbrevTdocA = (Label)item.FindControl("lbCodAbrevTdocA"); // obtenemos el control.
            lbCodAbrevTdocA.Text = oApoderado.AbrevDoc;

            Label lbFalta = (Label)item.FindControl("lbFalta"); // obtenemos el control.
            lbFalta.Text = oApoderado.FAlta.ToShortDateString();

            Label lbDirCalleAp = (Label)item.FindControl("lbDirCalleAp"); // obtenemos el control.
            lbDirCalleAp.Text = oApoderado.DirCalle;

            Label lbDirNumAp = (Label)item.FindControl("lbDirNumAp"); // obtenemos el control.
            lbDirNumAp.Text = oApoderado.DirNum;

            Label lbDirPisoAp = (Label)item.FindControl("lbDirPisoAp"); // obtenemos el control.
            lbDirPisoAp.Text = oApoderado.Piso;

            Label lbDirDeptoAp = (Label)item.FindControl("lbDirDeptoAp"); // obtenemos el control.
            lbDirDeptoAp.Text = oApoderado.Departamento;

            Label lbDirEC1Ap = (Label)item.FindControl("lbDirEC1Ap"); // obtenemos el control.
            lbDirEC1Ap.Text = oApoderado.EntreCalle1;

            Label lbDirEC2Ap = (Label)item.FindControl("lbDirEC2Ap"); // obtenemos el control.
            lbDirEC2Ap.Text = oApoderado.EntreCalle2;

            Label lbProvLocalidadAp = (Label)item.FindControl("lbProvLocalidadAp"); // obtenemos el control.
            lbProvLocalidadAp.Text = oApoderado.DirUbicacion == null ? oApoderado.Ciudad : oApoderado.DirUbicacion.DescripcionLocalidad + "-" + oApoderado.Ciudad + "(" + oApoderado.DirUbicacion.DescripcionProvincia + ")" + "CP:" + oApoderado.Cod_postal;
            
        }
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

            //Button btnImprimirNota = (Button)item.FindControl("btnImprimirNota"); // obtenemos el control.
            //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnImprimirNota);
        }
    }

    #endregion

    #region boton Regresar
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        BeneficiarioActual = null;
        if (Master.sesEsUnicoRegistro)
            Response.Redirect("Main.aspx", false);
        else
            Response.Redirect("ListaBeneficiarios.aspx", false);
    }
    #endregion boton Regresar

    #region Mofificar Beneficiario
    protected void btnModificarBeneficiario_Click(object sender, EventArgs e)
    {
        //Response.Redirect("AMBeneficiarioSolicitud.aspx?acceso=UPDATE&idBeneficiario="+HFidBeneficiario.Value, false);
        Response.Redirect("AMBeneficiario.aspx?acceso=UPDATE&idBeneficiario=" + idBeneficiario, false);
    }

    #endregion Mofificar Beneficiario

}
