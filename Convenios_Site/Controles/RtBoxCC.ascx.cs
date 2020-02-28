using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controles_RtBoxCC : System.Web.UI.UserControl
{
    internal String _TextoControl;

    #region Propiedades

    public String TextoControl
    {
        get
        {
            return _TextoControl;
        }
        set
        {
            _TextoControl = value;
            TextoControl = txttextarea.Value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txttextarea.Value = string.Empty;
    }


    #endregion

}
