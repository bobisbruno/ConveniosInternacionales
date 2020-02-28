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
using System.Threading;
using System.IO;

public partial class InformacionCompletaBeneficioPrint : System.Web.UI.Page
{
    protected Beneficiario iBeneficiario = null;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
        }

    }

    #region Cargar Datos

    protected void CargarDatos()
    {
        try
        {
            if(Session["_toPrint"] != null)
            {
                iBeneficiario = (Beneficiario)Session["_toPrint"];

                //datos del beneficiario
                lblNomApeBeneficiario.Text = iBeneficiario.ApeNom;
                lblCodSIACI.Text = iBeneficiario.ExpedienteExterno;
                lblCuipB.Text = iBeneficiario.Cuip;
                lbFechaNacimiento.Text = iBeneficiario.Fecha_nac.HasValue ? iBeneficiario.Fecha_nac.Value.ToShortDateString() : "";
                lbApellidoM.Text = iBeneficiario.ApellMaterno;
                lbDirCalleBen.Text = iBeneficiario.DirCalle;
                lbDirNumBen.Text = iBeneficiario.DirNum;
                lbDirPisoBen.Text = iBeneficiario.Piso;
                lbDirDeptoBen.Text = iBeneficiario.Departamento;
                lbEcalleB1.Text = iBeneficiario.ECalle1;
                lbEcalleB2.Text = iBeneficiario.ECalle2;
                lbProvLocalidadBen.Text = iBeneficiario.Ubicacion == null ? iBeneficiario.Ciudad : iBeneficiario.Ubicacion.DescripcionLocalidad + "-" + iBeneficiario.Ciudad + "(" + iBeneficiario.Ubicacion.DescripcionProvincia + ")" + "CP:" + iBeneficiario.CodPostal;
                LbPais.Text = iBeneficiario.Pais_Nacionalidad == null?"": iBeneficiario.Pais_Nacionalidad.Gentilicio;
                lblSexoBen.Text = iBeneficiario.Sexo.Equals(string.Empty) ? "" : iBeneficiario.Sexo.ToUpper().Equals("M") ? "Masculino" : "Femenino";

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

                if (iBeneficiario.LDocumentosBeneficiario.Length == 0 || iBeneficiario.LDocumentosBeneficiario == null)
                    gridListadoDocBeneficiarios.Visible = false;
                else
                {
                    gridListadoDocBeneficiarios.DataSource = ToDatatable.toDataTable( iBeneficiario.LDocumentosBeneficiario.ToList());
                    gridListadoDocBeneficiarios.DataBind();
                    gridListadoDocBeneficiarios.Visible = true;
                }

                //Datos solicitudes
                if (iBeneficiario.LPrestacionBeneficiario == null || iBeneficiario.LPrestacionBeneficiario.Length == 0)
                {
                    dvConSolicitud.Visible = false;
                    dvSinSolicitud.Visible = true;
                }

                else
                {
                    dvSinSolicitud.Visible = false;
                    dvConSolicitud.Visible = true;
                    rptSolicitudes.DataSource = iBeneficiario.LPrestacionBeneficiario;
                    rptSolicitudes.DataBind();
                }

                //Datos Causante
                if (iBeneficiario.Causante == null)
                    dvConCausante.Visible = false;
                else
                {
                    dvConCausante.Visible = true;
                    lblApellidoC.Text = iBeneficiario.Causante.ApeNom;
                    lblSexoCaus.Text = iBeneficiario.Causante.Sexo.Equals(string.Empty) ? "" : iBeneficiario.Causante.Sexo.ToUpper().Equals("M") ? "Masculino" : "Femenino";
                    lblFechaNacC.Text = iBeneficiario.Causante.Fecha_Nacimiento.HasValue ? iBeneficiario.Causante.Fecha_Nacimiento.Value.ToShortDateString() : "";
                    lblFechaDefuncionC.Text = iBeneficiario.Causante.Fecha_Def.ToShortDateString();
                    lblCuipC.Text = iBeneficiario.Causante.Cuip;
                    if (iBeneficiario.Causante.LDocCausante == null || iBeneficiario.Causante.LDocCausante.Length == 0)
                        gridListadoDocCausantes.Visible = false;
                    else
                    {   
                        gridListadoDocCausantes.DataSource = ToDatatable.toDataTable(iBeneficiario.Causante.LDocCausante.ToList());
                        gridListadoDocCausantes.DataBind();
                        gridListadoDocCausantes.Visible = true;
                    }
                }

                //DatosApoderados
                if (iBeneficiario.LApoderado == null || iBeneficiario.LApoderado.Length == 0)
                    dvConApoderados.Visible = false;
                else
                {
                    List<Apoderado> oApoderadosVigentes = new List<Apoderado>();
                    foreach (Apoderado iApo in iBeneficiario.LApoderado)
                    {
                        if (!iApo.Fbaja.HasValue)
                            oApoderadosVigentes.Add(iApo);
                    }
                    if (oApoderadosVigentes.Count == 0)
                        dvConApoderados.Visible = false;
                    else
                    {
                        dvConApoderados.Visible = true;
                        rptApoderados.DataSource = oApoderadosVigentes; //trae solo los habiliatados
                        rptApoderados.DataBind();
                    }
                }
            }
            else
            {
                //lblMsjEncabezado.Text = "Error";
                //lblTextoInforme.Text = "Ocurrio un error al traer los datos del Instrumento";
            }
        }
        catch
        {
            //Si no se encuentran los datos
            //lblMsjEncabezado.Text = "Error";
            //lblTextoInforme.Text = "Ocurrio un error al traer los datos del Instrumento";
        }
    }
    #endregion

    protected void rptSolicitudes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item; // elemento del Repeater
        ConsultasWS.Movimiento_Solicitud oMov = null;
        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        {
            PrestacionBeneficiario oPrestacionBenef = (PrestacionBeneficiario)e.Item.DataItem;

            Label lblPaisPrest = (Label)item.FindControl("lblPaisPrest"); // obtenemos el control.
            lblPaisPrest.Text = oPrestacionBenef.PaisDescCompleto + " - " + oPrestacionBenef.DescripcionPrestacion;

            string merror = "";
            Label lblfmovestSect = (Label)item.FindControl("lblfmovestSect"); // obtenemos el control.
            try
            {
                oMov = InvocaWsDao.TraeUltimoMovimientoSolicitud(oPrestacionBenef.IdBeneficiario, oPrestacionBenef.CodPrestacion, out merror);
                if (oMov != null && merror == "")
                    lblfmovestSect.Text = oMov.Fecha_Movimiento.ToShortDateString() + " - " + oMov.Estado.Descripcion + " - " + oMov.Sector.Descripcion;
                else
                    lblfmovestSect.Text = "";
            }
            catch (Exception)
            {
                lblfmovestSect.Text = "";
            }
            
            Label lblMercosur = (Label)item.FindControl("lblMercosur"); // obtenemos el control.
            lblMercosur.Text = oPrestacionBenef.Mercosur ? "SI" : "NO";
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

    protected void rptApoderados_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item; // elemento del Repeater

        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        {
            Apoderado oApoderado = (Apoderado)e.Item.DataItem;
            
            Label lblNomApeApod = (Label)item.FindControl("lblApeNomAp"); // obtenemos el control.
            lblNomApeApod.Text = oApoderado.ApeNom;

            Label lbFalta = (Label)item.FindControl("lbFaltaAp"); // obtenemos el control.
            lbFalta.Text = oApoderado.FAlta.ToShortDateString();

            Label lbDirCalle = (Label)item.FindControl("lbDirCalleAp"); // obtenemos el control.
            lbDirCalle.Text = oApoderado.DirCalle;

            Label lbDirNum = (Label)item.FindControl("lbDirNumAp"); // obtenemos el control.
            lbDirNum.Text = oApoderado.DirNum;

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

            Label lblBancoAp = (Label)item.FindControl("lblBancoAp"); // obtenemos el control.
            lblBancoAp.Text = oApoderado.Banco == null ? "" : oApoderado.Banco.Descripcion;

            Label lblTipoPoderAp = (Label)item.FindControl("lbTipoPoder"); // obtenemos el control.
            string tPoder = oApoderado.TipoApoderado.PoderPercibir ? "Percibir" : "" ;
            tPoder += oApoderado.TipoApoderado.PoderTramitar ? " - Tramitar" : "";
            lblTipoPoderAp.Text = tPoder;

            Label lblSTipoApod = (Label)item.FindControl("lblSTipoApod"); // obtenemos el control.
            lblSTipoApod.Text = oApoderado.StipoApoderado == null ? "" : oApoderado.StipoApoderado.Descripcion;

            Label lblTipoApoderadoAp = (Label)item.FindControl("lblTipoApoderadoAp"); // obtenemos el control.
            lblTipoApoderadoAp.Text = oApoderado.TipoApoderado.Descripcion;

            Label lblEmailAp = (Label)item.FindControl("lblEmailAp"); // obtenemos el control.
            lblEmailAp.Text = oApoderado.EMail;

            Label lblTelefonoAp = (Label)item.FindControl("lblTelefonoAp"); // obtenemos el control.
            lblTelefonoAp.Text = oApoderado.Telefono;

            Label lblObservaciones = (Label)item.FindControl("lblObservaciones"); // obtenemos el control.
            lblObservaciones.Text = oApoderado.Comentario;

            Label lbDocA = (Label)item.FindControl("lbDocA"); // obtenemos el control.
            lbDocA.Text = oApoderado.NumDoc;

            Label lbcodAbrevTdocA = (Label)item.FindControl("lbcodAbrevTdocA"); // obtenemos el control.
            lbcodAbrevTdocA.Text = oApoderado.AbrevDoc;
        }
    }
}
