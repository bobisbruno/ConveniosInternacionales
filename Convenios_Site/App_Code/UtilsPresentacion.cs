using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.ComponentModel;

/// <summary>
/// Descripción breve de UtilsPresentacionYt
/// </summary>
public class UtilsPresentacion
{
    public UtilsPresentacion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    public static string ReturnContentExtension(string fileExtension)
    {
        switch (fileExtension.ToUpper())
        {
            case ".HTM":
            case ".HTML":
            case ".LOG":
                return "text/HTML";
            case ".TXT":
                return "text/plain";
            case ".DOC":
                return "application/ms-word";
            case ".TIFF":
            case ".TIF":
                return "image/tiff";
            case ".ASF":
                return "video/x-ms-asf";
            case ".AVI":
                return "video/avi";
            case ".ZIP":
                return "application/zip";
            case ".XLS":
            case ".CSV":
                return "application/vnd.ms-excel";
            case ".GIF":
                return "image/gif";
            case ".JPG":
            case "JPEG":
                return "image/jpeg";
            case ".BMP":
                return "image/bmp";
            case ".WAV":
                return "audio/wav";
            case ".MP3":
                return "audio/mpeg3";
            case ".MPG":
            case "MPEG":
                return "video/mpeg";
            case ".RTF":
                return "application/rtf";
            case ".ASP":
                return "text/asp";
            case ".PDF":
                return "application/pdf";
            case ".FDF":
                return "application/vnd.fdf";
            case ".PPT":
                return "application/mspowerpoint";
            case ".DWG":
                return "image/vnd.dwg";
            case ".MSG":
                return "application/msoutlook";
            case ".XML":
            case ".SDXL":
                return "application/xml";
            case ".XDP":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }



    public static void ActualizarCheck(TreeNode node, bool check)
    {
        //actualizo los check de los nodos hijos, del nodo que se envío como parametro y a con el valor de parametro
        //treenode n;
        foreach (TreeNode n in node.ChildNodes)
        {
            if (n.Checked)
                n.Checked = check;
            if (n.ChildNodes.Count != 0)
                ActualizarCheck(n, check);
        }
    }

    private static void ArbolRecursivo(TreeNode treeNode)
    {
        // Print the node.
        //System.Diagnostics.Debug.WriteLine(treeNode.Text);
        //MessageBox.Show(treeNode.Text);
        if (treeNode.Checked)
            treeNode.Checked = false;
        // Print each node recursively.

        foreach (TreeNode tn in treeNode.ChildNodes)
        {
            ArbolRecursivo(tn);
        }
    }

    // Call the procedure using the TreeView.
    public static void LimpiarArbol(TreeView treeView)
    {
        treeView.Nodes.Clear();
        treeView.DataBind();
        // Print each node recursively.
        //TreeNodeCollection nodes = treeView.Nodes;
        //foreach (TreeNode n in nodes)
        //{
        //    ArbolRecursivo(n);
        //}
    }



    public static void InicializarCultura()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ConfigurationManager.AppSettings["CultureInfo"].ToString());
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ConfigurationManager.AppSettings["CultureInfo"].ToString());
    }

    public static void SetPaginationProperties(GridView grilla)
    {
        grilla.AllowPaging = false;
        if (Convert.ToInt32(ConfigurationManager.AppSettings["AllowPaging"]) == 1)
        {
            grilla.AllowPaging = true;
            grilla.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
        }
    }


        public static int CalcularIndiceGrilla(GridView grilla, int rowIndex)
            {
                return Convert.ToInt32(rowIndex) + (grilla.PageSize * (grilla.PageIndex));
            }

    

    public static void SetTextLabel(Label lbl, string titulo)
    {
        lbl.Text = titulo;
    }

    
    public static void SetImagenesOrdenamiento(object sender, GridViewRowEventArgs e, string sortExpression, SortDirection sortDirection)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell tableCell in e.Row.Cells)
            {
                if (tableCell.HasControls())
                {
                    LinkButton columnLinkButton = (LinkButton)tableCell.Controls[0];
                    if (columnLinkButton != null)
                    {
                        Image img = new Image();
                        img.ImageUrl = "~/App_Themes/Imagenes/up-down.png";
                        img.Height = Unit.Pixel(14);
                        img.Width = Unit.Pixel(14);
                        if (sortDirection == SortDirection.Ascending)
                            img.AlternateText = "Orden";
                        else
                            img.AlternateText = "Orden inverso";
                        
                        if (sortExpression == columnLinkButton.CommandArgument)
                        {
                            tableCell.Controls.Add(new System.Web.UI.LiteralControl(" "));
                            tableCell.Controls.Add(img);
                        }
                    }
                }
            }
        }
    }

    public static void EmptyGridFix<T>(GridView grdView, IList<T> lst, string leyenda)
    {
        if (grdView.Rows.Count == 0 &&
            grdView.DataSource != null)
        {
            DataTable dt = null;

            if (grdView.DataSource is DataSet)
            {
                dt = ((DataSet)grdView.DataSource).Tables[0].Clone();
            }
            else if (grdView.DataSource is DataTable)
            {
                dt = ((DataTable)grdView.DataSource).Clone();
            }
            else if (grdView.DataSource is IList)
            {
                dt = toDataTable(lst);
            }

            if (dt == null)
            {
                return;
            }

            dt.Rows.Add(dt.NewRow()); // agregar row vacio
            grdView.DataSource = dt;
            grdView.DataBind();

            // esconder row
            //grdView.Rows[0].Visible = false;
            //grdView.Rows[0].Controls.Clear();
            int columncount = grdView.Rows[0].Cells.Count;
            grdView.Rows[0].Cells.Clear();
            grdView.Rows[0].Cells.Add(new TableCell());
            //grdView.Rows[0].Cells[0].ColumnSpan = columncount;
            grdView.Rows[0].Cells[0].Text = leyenda;

        }

        if (grdView.Rows.Count == 1 &&
            grdView.DataSource == null)
        {
            bool bIsGridEmpty = true;

            for (int i = 0; i < grdView.Rows[0].Cells.Count; i++)
            {
                if (grdView.Rows[0].Cells[i].Text != string.Empty)
                {
                    bIsGridEmpty = false;
                }
            }
            // esconder row
            if (bIsGridEmpty)
            {
                grdView.Rows[0].Visible = false;
                grdView.Rows[0].Controls.Clear();
            }
        }

    }


    

    public static DataTable toDataTable<T>(IList<T> data)// T is any generic type
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

        DataTable table = new DataTable();
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];

            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }
        object[] values = new object[props.Count];
        foreach (T item in data)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = props[i].GetValue(item);
            }
            table.Rows.Add(values);
        }
        return table;
    }

    public static void ExportToExcel(GridView grdView, int allowPaging, HttpResponse response, string nombreArchivo, Action dataBind )
    {
        if (allowPaging == 1)
        {
            grdView.AllowPaging = false;
            grdView.EnableViewState = false;
            dataBind();
        }
        //grdView.AllowPaging = false;
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        Page page = new Page();
        HtmlForm form = new HtmlForm();

        //grdView.EnableViewState = false;
        //DataBindGridFacturacionLinea();

        // Deshabilitar la validación de eventos, sólo asp.net 2
        page.EnableEventValidation = false;

        // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
        page.DesignerInitialize();

        page.Controls.Add(form);
        form.Controls.Add(grdView);

        page.RenderControl(htw);

        response.Clear();
        response.Buffer = true;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=" + nombreArchivo);
        response.Charset = "UTF-8";
        response.ContentEncoding = Encoding.Default;
        response.Write(sb.ToString());
        response.End();

        if (allowPaging == 1)
        {
            grdView.AllowPaging = true;
            dataBind();
        }

        //grdView.AllowPaging = true;
        //DataBindGridFacturacionLinea();
    }

    public enum TipoConsultaDictamen
    {
        Referencia = 1,
        DictamenNroAnio = 2,
        Vocero = 3,
        TextoDictamen = 4,
        MesYAnio = 5
    }

    public enum PerfilConsultaVoceros
    {
        Previsional = 1,
        AsignacionesFamiliares = 220,
        Contrataciones = 252,
        Laboral = 359,
        ObrasSociales = 401,
        Desempleo = 410,
        Otros = 440,
        FGS = 482
    }

}