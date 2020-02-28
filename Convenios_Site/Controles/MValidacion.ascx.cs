﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Comun_Controles_MValidacion : System.Web.UI.UserControl
{
    public string Text
    {
        get { return lblMValidacion.Text; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                Visible = true;
            else
                Visible = false;
            lblMValidacion.Text = value;
        }
    }

    public string Value
    {
        get { return lblMValidacion.Text; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                Visible = true;
            else
                Visible = false;
            lblMValidacion.Text = value;
        }
    }

    public bool Visible
    {
        get { return divMValidacion.Visible; }
        set { divMValidacion.Visible = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        imgMValidacion.ImageUrl = "../App_Themes/Imagenes/atencion_gde_ani.gif";
    }
}
