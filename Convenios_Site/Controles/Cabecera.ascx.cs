using System;
using System.Collections;
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
using log4net;

public partial class comun_controles_cabecera : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger(typeof(comun_controles_cabecera).Name);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {   //retorna la versión del WS instalado
                lblVersion.InnerText = VariableSession.oVsSistema  + '-' + VariableSession.oVsSitio;
            }
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat("Se generó una excepción : {0}", ex.Message);
            //throw ex;
        }
    }
}
