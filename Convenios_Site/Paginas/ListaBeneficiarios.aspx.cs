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
using ActoresWS;
using log4net;

public partial class Paginas_ListaBeneficiarios : System.Web.UI.Page
{
    #region Propiedades / Variables publicas

    private static readonly ILog log = LogManager.GetLogger(typeof(Paginas_ListaBeneficiarios).Name);

    private List<LsBeneficiario> sesBeneficiarios
    {
        get { return Session["__sesBeneficiarios"] == null ? new List<LsBeneficiario>() : (List<LsBeneficiario>)Session["__sesBeneficiarios"]; }
        set { Session["__sesBeneficiarios"] = value; }
    }
    #endregion

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
            if (!AplicarSeguridadPagina())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"]);
            InicializarDatosPagina("Listado de solicitantes", "> Listado de solicitantes");
            consultar();
        }
       
    }

    #region Inicializa Datos Pagina
    private void InicializarDatosPagina(string titulo, string txtBarraNav)
    {
        BNav.Text = txtBarraNav;
        //UtilsPresentacionX5.SetPaginationProperties(gv_Grilla);
        Page.Title = titulo;
    }
    #endregion Inicializa Datos Pagina

    private void consultar()
    {
        try
        {
            
            lbElementosEncontrados.Text = sesBeneficiarios.Count().ToString();
            gridListadoBeneficiarios.DataSource = (DataTable)toDataTable( sesBeneficiarios);
            gridListadoBeneficiarios.DataBind();
            lbTituloCriterio.Text = Request.QueryString["tituloCriterio"];
            btnToExcell.Visible = true;
        }
        catch (Exception er)
        {
            log.Error("Ocurrio un error al consultar Beneficiarios." + er.Message);
            gridListadoBeneficiarios.Visible = false;
            MError.MensajeError = "Ocurrio un error al consultar Beneficiarios.";
        }
    }

    private DataTable toDataTable(List<LsBeneficiario> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("ApeNom", typeof(String));
        _dt.Columns.Add("Documento", typeof(String));
        _dt.Columns.Add("ExpedienteExt", typeof(String));
        _dt.Columns.Add("Id_Beneficiario", typeof(Int64));
        _dt.Columns.Add("sexo", typeof(String));
        _dt.Columns.Add("fecha_nac", typeof(String));

        
        foreach( LsBeneficiario oBenef in iParam )
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["ApeNom"] = oBenef.apeNom;
            _drTemp["Documento"] = oBenef.Documento;
            _drTemp["ExpedienteExt"] = oBenef.ExpedienteExt;
            _drTemp["Id_Beneficiario"] = oBenef.Id_Beneficiario;
            _drTemp["sexo"] = oBenef.Sexo;
            _drTemp["fecha_nac"] = oBenef.Fecha_nac == null ? "":oBenef.Fecha_nac.Value.ToShortDateString();
            
            _dt.Rows.Add(_drTemp);
        }
        


        return _dt;
    }

    protected void btnToExcell_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string strBody = "";
            strBody = Exportar.DataTable2ExcelString(toDataTable((List<LsBeneficiario>)sesBeneficiarios));

            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-disposition", "attachment; filename=Listado_Beneficios_" + System.DateTime.Now.ToShortDateString() + ".xls");
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
        sesBeneficiarios = null;
        Response.Redirect("Main.aspx", false);
    }
}
