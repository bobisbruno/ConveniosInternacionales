using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Paginas_VerDocumento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String rutaArchivo = (String)Session["ruta"];
        Page.Title = Request.Params["dn"].ToString();
        dvError.Visible = false;
        Response.Clear();

        try
        {
            var file = new FileInfo(rutaArchivo);

            if (file.Exists)
            {
                //Response.ContentType = "application/pdf";
                //Response.ContentType = "application/octect-stream";
                Response.ContentType = UtilsPresentacion.ReturnContentExtension(file.Extension);
                Response.AddHeader("Content-Length", file.Length.ToString());
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                //hacer esto en una pagina dedicada abrirla con window open
                Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name);
                Response.WriteFile(rutaArchivo);
                Response.Flush();
                Response.End();
            }
            else
                dvError.Visible = true;
        }
        catch(Exception)
        {}
    }
}