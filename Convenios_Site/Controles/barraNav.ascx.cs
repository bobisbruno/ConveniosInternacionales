using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class comun_controles_barraNav : System.Web.UI.UserControl
{
    public string Text
    {
        get { return lblTexto.Text; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                Visible = true;
            else
                Visible = false;
            lblTexto.Text = value;
        }
    }

    public string Value
    {
        get { return lblTexto.Text; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                Visible = true;
            else
                Visible = false;
            lblTexto.Text = value;
        }
    }

    public bool Visible
    {
        get { return dvBNav.Visible; }
        set { dvBNav.Visible = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lnkInicio_Click(object sender, EventArgs e)
    {
        Response.Redirect("Main.aspx");
    }
}