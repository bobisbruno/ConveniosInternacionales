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


public partial class ComprobanteTramite : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ComprobanteTramite).Name);


    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try{
            //Check de Seguridad tanto para ingresar como modificar instrumento
            //if (!AplicarSeguridad())
            //    Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegadoNBtn"].ToString());

            string tramite = (string)Request.QueryString["nroSolicitud"];
            string mensaje = "";
            ActoresWS.SolicitudProvisoria Provisoria = null;
            Provisoria = InvocaWsDao.TraeSolicitudProvisoriaXNroSolicitudProvisoria(tramite, out mensaje);

            if (Provisoria != null)
            {
                lbSolicitanteNomApe.Text = Provisoria.ApellildoynombreDeclarado;
                lbSolicitanteDoc.Text = Provisoria.DocumentoDeclarado.Equals(string.Empty) ? "" : Provisoria.DocumentoDeclarado;


                lbTramiteNro.Text = Provisoria.Nro_SolicitudProvisoria.ToUpper();
                //List<AuxiliaresWS.Prestacion> lp = InvocaWsDao.TraerPrestaciones();

                //AuxiliaresWS.Prestacion p = lp.Find(delegate(AuxiliaresWS.Prestacion prestacion)
                //{
                //    return prestacion.Cod_Prestacion == Provisoria.Cod_Prestacion;
                //}
                //        );


                lbTramite.Text = Provisoria.PrestacionSolicitada == null ? "" : Provisoria.PrestacionSolicitada.Descripcion;
                lbFechaIR.Text = Provisoria.FAltaProvisoria.ToShortDateString();
                lbDatosReferencia.Text = Provisoria.Referencia_Provisoria;
                lbTipodeTramite.Text = Provisoria.TIngresoProvisorio.Equals("I") ? "Ingreso de documentación" : "Devolución de documentación";
                lbSectorDeriva.Text = Provisoria.Sectorderiva == null ? "Sin sector asignado." : Provisoria.Sectorderiva.Descripcion;

                if (Provisoria.LMovimientos != null)
                {

                    lbCantidadDoc.Text = "Se ingresaron " + Provisoria.LMovimientos.Count().ToString() + " documentos";
                    rptDocumentacionIR.DataSource = Provisoria.LMovimientos;
                    rptDocumentacionIR.DataBind();
                }
                Page.Title = "Trámite nro. " +  Provisoria.Nro_SolicitudProvisoria.ToUpper();
            }
            if (!mensaje.Equals(string.Empty))
                lbError.Text = mensaje;
            else
                lbError.Text = string.Empty;



            
            }
            catch(Exception er){
                log.Error(er.Message);
            }
        }
    }


    protected void rptDocumemntacionIR_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item; // elemento del Repeater

        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        {
            ActoresWS.SolicitudProvisoriaMovimiento oDocumenatcion = (ActoresWS.SolicitudProvisoriaMovimiento)e.Item.DataItem;

            Label lblDocumentacion = (Label)item.FindControl("lblDocumentacion"); // obtenemos el control.
            lblDocumentacion.Text = oDocumenatcion.TipoDocumentacion.Descripcion;
        }
    }
}