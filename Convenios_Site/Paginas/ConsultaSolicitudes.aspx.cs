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

public partial class Paginas_ConsultaSolicitudes : System.Web.UI.Page
{
    #region Propiedades / Variables publicas

    private string codPrestacion
    {
        get { return (string)ViewState["_codPrestacion"]; }
        set { ViewState["_codPrestacion"] = value; }
    }

    private string codPais
    {
        get { return (string)ViewState["_codPais"]; }
        set { ViewState["_codPais"] = value; }
    }

    private string idBeneficiario
    {
        get { return (string)ViewState["_idBen"]; }
        set { ViewState["_idBen"] = value; }
    }

    private string ApenomBeneficiario
    {
        get { return (string)ViewState["_APNB"]; }
        set { ViewState["_APNB"] = value; }
    }

    private string FIngSol
    {
        get { return (string)ViewState["_fis"]; }
        set { ViewState["_fis"] = value; }
    }

    private string Cuip
    {
        get { return (string)ViewState["_cuip"]; }
        set { ViewState["_cuip"] = value; }
    }

    private string RefExt
    {
        get { return (string)ViewState["_re"]; }
        set { ViewState["_re"] = value; }
    }

    private string UbicFisica
    {
        get { return (string)ViewState["_uf"]; }
        set { ViewState["_uf"] = value; }
    }
    private string descPrestacion
    {
        get { return (string)ViewState["_descPrestacion"]; }
        set { ViewState["_descPrestacion"] = value; }
    }

    private string descPais
    {
        get { return (string)ViewState["_dPais"]; }
        set { ViewState["_dPais"] = value; }
    }


    private static readonly ILog log = LogManager.GetLogger(typeof(Paginas_ConsultaSolicitudes).Name);

    #endregion

    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }


    public List<SolicitudesEFechasSolicitud> sesToExporte
    {
        get { return (List<SolicitudesEFechasSolicitud>)Session["_solicToExport"]; }
        set { Session["_solicToExport"] = value; }
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
        rvtxtFechaDesde.MaximumValue = DateTime.Today.AddYears(10).ToString("dd/MM/yyyy");
        rvtxtFechaDesde.MinimumValue = DateTime.Today.AddYears(-50).ToString("dd/MM/yyyy");
        rvtxtFechaHasta.MaximumValue = DateTime.Today.AddYears(10).ToString("dd/MM/yyyy");
        rvtxtFechaHasta.MinimumValue = DateTime.Today.AddYears(-50).ToString("dd/MM/yyyy");
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnToExcell);

        if (!IsPostBack)
        {
            //txtFechaHasta.Text = System.DateTime.Today.ToShortDateString();
            //txtFechaDesde.Text = System.DateTime.Today.AddDays(-(double.Parse(ConfigurationManager.AppSettings["RangoMaxDiasConsulta"])-1)).ToShortDateString();
            txtFechaHasta.Text = String.Empty;
            txtFechaDesde.Text = String.Empty;
            sesToExporte = null;
            dvDatosConsulta.Visible = false;
            dvNODatosConsulta.Visible = false;
            #region Seguridad
            if (!AplicarSeguridadPagina())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"]);
            #endregion Seguridad
            InicializarDatosPagina("Consulta de Solicitudes", "> Consultas > Solicitudes");

            btnImprimir.Enabled = false;
            btnToExcell.Enabled = false;

            CargarCombos();
    
        }

    }

    private bool CargarCombos()
    {
        string mensajeCarga = string.Empty;

        try
        {
            ////Combo Paises
            List<PaisWS.Pais> oPaisConvenios = VariableSession.oPaisConvenios;
            
            if (oPaisConvenios != null)
            {
                //De solicitud solo carga los paises con convenio
                ddlPaisS.DataSource = oPaisConvenios;
                ddlPaisS.DataBind();
                ddlPaisS.Items.Insert(0, new ListItem("Todo", "0"));
            }
            else
                mensajeCarga += "Paises" + "</br>";

            List<AuxiliaresWS.Prestacion> oPrestacion = VariableSession.oPrestaciones;

            if (oPrestacion != null)
            {
                ddlPrestacionesS.DataSource = oPrestacion;
                ddlPrestacionesS.DataBind();
                ddlPrestacionesS.Items.Insert(0, new ListItem("Todo", "0"));

            }
            else
                mensajeCarga += "Prestaciones" + "</br>";

            if (!mensajeCarga.Equals(string.Empty))
            {
                MError.MensajeError = "Las siguientes listas no pudieron cargarse:" + "</br>" + mensajeCarga;
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            log.Error("Ocurrió un error al intentar traer una o mas Listas. " + ex.Message);
            MError.MensajeError = "Ocurrió un error al intentar traer una o mas Listas. Intente mas tarde.";
            return false;
        }

    }
    

    private void InicializarDatosPagina(string titulo, string txtBarraNav)
    {
        UtilsPresentacionX5.SetTextLabel(LblTituloPagina, titulo);
        BNav.Text = txtBarraNav;
        //UtilsPresentacionX5.SetPaginationProperties(gv_Grilla);
        Page.Title = titulo;
    }

    private DataTable toDataTable(List<SolicitudesEFechasSolicitud> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("ApeNomCompleto", typeof(String));
        _dt.Columns.Add("CodPrestacion", typeof(Int16));
        _dt.Columns.Add("DescripcionPrestacion", typeof(String));
        _dt.Columns.Add("FAMSolicitud", typeof(String));
        _dt.Columns.Add("Id_Beneficiario", typeof(Int64));
        _dt.Columns.Add("Mercosur", typeof(Boolean));
        _dt.Columns.Add("Pais_PK", typeof(Int32));
        _dt.Columns.Add("PaisDescCompleto", typeof(String));
        _dt.Columns.Add("CUIP", typeof(String));
        _dt.Columns.Add("RefExt", typeof(String));
        _dt.Columns.Add("UbicFisica", typeof(String));
        
        


        foreach (SolicitudesEFechasSolicitud oSol in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["ApeNomCompleto"] = oSol.ApeNomCompleto;
            _drTemp["CodPrestacion"] = oSol.CodPrestacion;
            _drTemp["DescripcionPrestacion"] = oSol.DescripcionPrestacion;
            _drTemp["FAMSolicitud"] = oSol.FAMSolicitud.ToShortDateString();
            _drTemp["Id_Beneficiario"] = oSol.Id_Beneficiario;
            _drTemp["Mercosur"] = oSol.Mercosur;
            _drTemp["Pais_PK"] = oSol.Pais_PK;
            _drTemp["PaisDescCompleto"] = oSol.PaisDescCompleto;
            _drTemp["CUIP"] = oSol.Cuip;
            _drTemp["RefExt"] = oSol.Referencia_exterior;
            _drTemp["UbicFisica"] = oSol.Ubicacion_Fisica;
        
            
            
            _dt.Rows.Add(_drTemp);
        }



        return _dt;
    }

    protected void btnToExcell_Click(object sender, EventArgs e)
    {
        try
        {
            string strBody = "";
            strBody = Exportar.DataTable2ExcelString(toDataTable((List<SolicitudesEFechasSolicitud>)sesToExporte));

            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-disposition", "attachment; filename=Listado_Solicitudes_" + txtFechaDesde.Text + "-" + txtFechaHasta.Text + ".xls");
            Response.Write(strBody);
        }
        catch (Exception er)
        {
            log.Error("Ocurrio un error al generar el listado de exportacion." + er.Message);
            MError.MensajeError = "Ocurrio un error al generar el listado de exportacion.";
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Main.aspx", false);
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            sesToExporte = null;
            string mensaje="";
            Int16? codPrestacion, codPais;
            if(ddlPrestacionesS.SelectedItem.Value.Equals("0"))
                codPrestacion = null;
            else
                codPrestacion = Int16.Parse(ddlPrestacionesS.SelectedItem.Value);

            if(ddlPaisS.SelectedItem.Value.Equals("0"))
                codPais = null;
            else
                codPais = Int16.Parse(ddlPaisS.SelectedItem.Value);
            List<SolicitudesEFechasSolicitud> oList = InvocaWsDao.TraeSolicitudesEFechasSolicitud(txtFechaDesde.Text, txtFechaHasta.Text, codPrestacion, codPais, chkMercosurS.Checked, Byte.Parse( ddlOrderBy.SelectedValue), out mensaje);
            sesToExporte = oList;
            MError.MensajeError = mensaje;
            if ((oList == null)||(oList.Count == 0))
            {
                dvNODatosConsulta.Visible = true;
                dvDatosConsulta.Visible = false;
                hffd.Value = "";
                hffh.Value = "";
                btnImprimir.Enabled = false;
                btnToExcell.Enabled = false;
            }
            else
            {
                dvDatosConsulta.Visible = true;
                lbElementosEncontrados.Text = oList.Count.ToString();
                gridListadoSolicitudes.DataSource = toDataTable(oList);
                gridListadoSolicitudes.DataBind();
                dvNODatosConsulta.Visible = false;
                hffd.Value = txtFechaDesde.Text;
                hffh.Value = txtFechaHasta.Text;

                btnImprimir.Enabled = true;
                btnToExcell.Enabled = true;
            }
                
        }
    }

    protected void FechValidateDays(object source, ServerValidateEventArgs args)
    {
        //args.IsValid = (args.Value.Length >= 8);
        args.IsValid = DateTime.Parse(txtFechaDesde.Text).AddDays(int.Parse( ConfigurationManager.AppSettings["RangoMaxDiasConsulta"])) > DateTime.Parse(txtFechaHasta.Text);
    }

    protected void RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] arg = new string[9];
        arg = e.CommandArgument.ToString().Split(';');
        codPrestacion = arg[0];
        descPrestacion = arg[1];
        descPais = arg[2];
        codPais = arg[3];
        idBeneficiario = arg[4];
        ApenomBeneficiario = arg[5];
        FIngSol = arg[6];
        Cuip = arg[7];
        RefExt = arg[8];
        UbicFisica = arg[9];

        string encabezadoBeneficiario = "";
        encabezadoBeneficiario += ApenomBeneficiario;
        encabezadoBeneficiario += Cuip.Equals("") ? "" : " - " + Cuip;
        encabezadoBeneficiario += RefExt.Equals("") ? "" : " - C. SIACI " + RefExt;
        encabezadoBeneficiario += UbicFisica.Equals("") ? "" : " - Ubicación " + RefExt;

        String script = "";
        if (e.CommandName == "VerDatos")
        {
            script = "<script type='text/javascript'>" + "hidden = open('InfoCompletaPeticion.aspx?codPrestacion=" + codPrestacion + "&idBeneficiario=" + idBeneficiario + "&descPrestacion=" + descPrestacion + "&descApeNom=" + encabezadoBeneficiario + "&codPais=" + codPais + "&descPais=" + descPais + "');" + "</script>";
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "xx", script, false);


    }


    //protected void gridListadoSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView rowView = (DataRowView)e.Row.DataItem;
    //        // Retrieve the state value for the current row. 
    //        Image img = (Image)e.Row.Cells[3].FindControl("imgMercosur");
    //        img.Visible = (Boolean)rowView["Mercosur"];
    //    }

    //}
}
