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


public partial class InformacionNota : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(typeof(InformacionNota).Name);

    
    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Check de Seguridad tanto para ingresar como modificar instrumento
            //if (!AplicarSeguridad())
            //    Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegadoNBtn"].ToString());

            lbEncabezadoBeneficiario.Text = (string)Request.QueryString["DatoBeneficiario"];
            lbAsunto.Text = (string)Request.QueryString["asuntoNota"];
            lbDescripcion.Text = (string)Request.QueryString["descNota"];
            lbFechaRec.Text = (string)Request.QueryString["fNota"];
            lbDescripcion.Text = Util.PutBRs((string)Request.QueryString["descNota"], 100);
            lbNumNota.Text = (string)Request.QueryString["NumNota"];

            Page.Title = "Nota nro. " + (string)Request.QueryString["NumNota"];
        }
    }
}