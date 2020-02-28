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

/// <summary>
/// Descripción breve de UtilsPresentacionYt
/// </summary>
public class UtilsPresentacionX5
{
    public UtilsPresentacionX5()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
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
                        if (sortDirection == SortDirection.Ascending)
                        {
                            img.ImageUrl = "~/App_Themes/Imagenes/arrow-up.gif";
                        }
                        else
                        {
                            img.ImageUrl = "~/App_Themes/Imagenes/arrow-down.gif";
                        }

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
                dt = ToDatatable.toDataTable(lst);
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

}