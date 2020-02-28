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
using System.Threading;
using ConsultasWS;
using log4net;

public partial class Paginas_DevolucionesNotifFueraPlazo : System.Web.UI.Page
{
    #region Propiedades / Variables publicas

    private static readonly ILog log = LogManager.GetLogger(typeof(Paginas_DevolucionesNotifFueraPlazo).Name);

    private List<NotificacionesVencidas> sesNotificacionesVencidas
    {
        get { return Session["__sesNotificacionesVencidas"] == null ? new List<NotificacionesVencidas>() : (List<NotificacionesVencidas>)Session["__sesNotificacionesVencidas"]; }
        set { Session["__sesNotificacionesVencidas"] = value; }
    }

    #endregion

    #region variables publicas

    private long PageNum
    {
        get { return Convert.ToInt64(ViewState["PageNum"]); }
        set { ViewState["PageNum"] = value; }
    }

    private long PageSize
    {
        get { return Convert.ToInt64(ViewState["PageSize"]); }
        set { ViewState["PageSize"] = value; }
    }

    private long TotalRowsNum
    {
        get { return Convert.ToInt64(ViewState["TotalRowsNum"]); }
        set { ViewState["TotalRowsNum"] = value; }
    }

    #endregion

    #region Navigation
    private void Navigation(long totalRecords)
    {

        //------------
        txtPageTotalRowsNum.Text = totalRecords.ToString();

        if (totalRecords != 0)
        {
            Double totalPages = Math.Ceiling(((double)totalRecords / PageSize));

            if ((totalRecords == 1) || (totalPages == 0))
            {
                totalPages = 1;
            }

            //if (PageSizeMAE > totalRecords)
            //{
            //    PageSizeMAE = (int)totalPages;
            //}
            //else
            //    PageSizeMAE = long.Parse(ConfigurationSettings.AppSettings["RowsPorPagina"].ToString());

            txtGoToPage.Text = PageNum.ToString();
            lblCurrentPage.Text = PageNum.ToString();
            lblTotalPages.Text = totalPages.ToString();
        }
        else
        {
            txtPageTotalRowsNum.Text = "0";
            txtGoToPage.Text = "0";
            lblCurrentPage.Text = "0";
            lblTotalPages.Text = "0";
        }

        //control de teclas
        if (lblCurrentPage.Text.Trim().Equals(lblTotalPages.Text.Trim()))
        {
            NextPage.Enabled = false;
            LastPage.Enabled = false;
        }
        else
        {
            NextPage.Enabled = true;
            LastPage.Enabled = true;
        }

        if (lblCurrentPage.Text.Trim().Equals("1"))
        {
            FirstPage.Enabled = false;
            PreviousPage.Enabled = false;
        }
        else
        {
            FirstPage.Enabled = true;
            PreviousPage.Enabled = true;
        }
    }
    #endregion Navigation


    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }


    #region APlica Seguridad

    private bool AplicarSeguridadPagina()
    {
        bool permiso = false;
        try
        {
            permiso = TienePermiso("accesoPagina");
        }
        catch (ThreadAbortException)
        { }
        return permiso;
    }
    #endregion APlica Seguridad Pagina

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnToExcell);

        if (!IsPostBack)
        {
            try
            {
                sesNotificacionesVencidas = null;
                if (!AplicarSeguridadPagina())
                    Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"]);


                #region Devoluciones Notificadas vencidas
                //Trae la primera pagina de notirficaciones Vencidas

                
                cargarDevolucionesNotificadasVencidas(1);
                #endregion Devoluciones Notificadas vencidas
            }
            catch (Exception err)
            {
                log.ErrorFormat("Error al cargar la pagina Main.aspx error: {0}", err.Message);
                //Response.Redirect("../Servicios/Error.htm");
                MError.MensajeError = "Ocurrio un error al intentar efectuar la consulta. Revisar log para mayor informacion.";
                //Response.End();
            }
        }

    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Main.aspx", false);
    }

    #region Cargar devoluciones vencidas

    private void cargarDevolucionesNotificadasVencidas(Int64 pageNum)
    {
        string mensaje = "";
        //PageNum = 1;
        PageSize = long.Parse(ConfigurationManager.AppSettings["RowsPorPagina"].ToString());
        Int64 Total;
        List<NotificacionesVencidas> n = null;

        n = InvocaWsDao.TraeDevolucionesNotificadasVencidasXPlazo(pageNum, PageSize, 1, out Total, out mensaje);

        TotalRowsNum = Total;
        txtPageTotalRowsNum.Text = TotalRowsNum.ToString();

        MError.MensajeError = mensaje;
        if (n != null && n.Count > 0)
        {
            sesNotificacionesVencidas = (List<NotificacionesVencidas>)n;
            gridListadoDevolucionesNotificadasVencidas.DataSource = (DataTable)ToDatatable.toDataTable(n);
            gridListadoDevolucionesNotificadasVencidas.DataBind();
            divListadoNoNotificados.Visible = true;
            dvNoDevNotifVencidas.Visible = false;
            btnToExcell.Visible = true;
            Navigation(TotalRowsNum);
        }
        else
        {
            divListadoNoNotificados.Visible = false;
            dvNoDevNotifVencidas.Visible = true;
            btnToExcell.Visible = false;
            lblTotalPages.Text = "0";
        }
    }

    #endregion Cargar devoluciones vencidas

    protected void btnToExcell_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = ToDatatable.toDataTable(sesNotificacionesVencidas);
        dt.TableName = "ListadoDevolucionesFueraDePlazo-Pagina-" + PageNum.ToString() + "-" + System.DateTime.Now.ToShortDateString();


        try
        {
            string strBody = "";
            strBody = Exportar.DataTable2ExcelString(dt);

            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-disposition", "attachment; filename=DevolucionesFueraDePlazo_Consulta_Pagina_" + PageNum.ToString() + "_" + System.DateTime.Now.ToShortDateString() + "_Pag_" + lblCurrentPage.Text + ".xls");
            Response.Write(strBody);
        }
        catch (Exception er)
        {
            log.Error("Ocurrio un error al generar el listado de exportacion." + er.Message);
            MError.MensajeError = "Ocurrio un error al generar el listado de exportacion.";
        }
    }

    
    #region Navegacion
    protected void NavigationLink_Click(object sender, CommandEventArgs e)
    {
        try
        {
            PageSize = Convert.ToInt16(txtPageSize.Text);

            switch (e.CommandName)
            {
                case "First":
                    PageNum = 1;
                    break;
                case "Last":
                    PageNum = Convert.ToInt16(lblTotalPages.Text);
                    break;
                case "Next":
                    PageNum = Convert.ToInt16(lblCurrentPage.Text) + 1;
                    break;
                case "Prev":
                    PageNum = Convert.ToInt16(lblCurrentPage.Text) - 1;
                    break;
            }

            cargarDevolucionesNotificadasVencidas(PageNum);
        }
        catch (Exception ex)
        {
            log.ErrorFormat("Error en la consulta de Devoluciones Fuera de plazo  -> {0}", ex.Message);
            MError.MensajeError = "Error en la consulta de Devoluciones Fuera de plazo";
            //Response.Redirect("../error.htm");
        }
        finally
        {
            //wsConsulta.Dispose();
        }
    }

    protected void PageSizeImb_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            PageNum = 1;
            //PageSizeMAE = Convert.ToInt16(txtPageSizeMAE.Text);

            //ConsultarOperacionesMAE(cargaParametrosMAE());

        }

        catch (Exception ex)
        {
            log.ErrorFormat("Error en la consulta de Devoluciones Fuera de plazo  -> {0}", ex.Message);
            MError.MensajeError = "Error en la consulta de Devoluciones Fuera de plazo";
        }
        finally
        {

        }

    }
    protected void GoToPageImb_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtGoToPage.Text != "")
            {
                int maxPage = Convert.ToInt32(lblTotalPages.Text);
                int goToPage = Convert.ToInt32(txtGoToPage.Text);

                if (goToPage <= maxPage)
                {
                    PageNum = goToPage;
                    cargarDevolucionesNotificadasVencidas(PageNum);
                }
            }
        }
        catch (Exception ex)
        {
            log.ErrorFormat("Error en la consulta de Devoluciones Fuera de plazo  -> {0}", ex.Message);
            MError.MensajeError = "Error en la consulta de Devoluciones Fuera de plazo";
        }
        finally
        {

        }
    }
    #endregion Navegacion 
}
