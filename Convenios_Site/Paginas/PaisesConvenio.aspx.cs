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
using log4net;
using System.Threading;
using PaisWS;

public partial class Paginas_PaisesConvenio : System.Web.UI.Page
{
    readonly ILog log = LogManager.GetLogger(typeof(Paginas_PaisesConvenio));

    #region ListaChecks
    public DataTable ListaChecks
    {
        get
        {
            return Session["_listaChecks"] == null ? crearListaChecks() : (DataTable)Session["_listaChecks"];
            //return (string)Session["acceso"];
        }
        set
        {
            Session["_listaChecks"] = value;
        }
    }
    #endregion

    private bool TienePermiso(string Valor)
    {
        return DirectorManager.traerPermiso(Valor, Page.Request.FilePath.Substring(Page.Request.FilePath.LastIndexOf("/") + 1).ToLower()).Value.accion != null;
    }

    #region APlica Seguridad Pagina
    private bool AplicarSeguridad()
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
        mensaje.ClickSi += new Mensaje.Click_UsuarioSi(ClickearonSi);
        mensaje.ClickNo += new Mensaje.Click_UsuarioNo(ClickearonNo);
        if (!IsPostBack)
        {
            //Check de Seguridad tanto para ingresar como modificar instrumento
            #region Seguridad
            if (!AplicarSeguridad())
                Response.Redirect(ConfigurationManager.AppSettings["urlAccesoDenegado"].ToString());
            #endregion Seguridad

            InicializarDatosPagina("Modificación de País  / Convenio", "> Administración > Convenios - Países");
            ListaChecks = null;

            MError.MensajeError = string.Empty;
            
            CargarGridView();
        }
    }


    private void InicializarDatosPagina(string titulo, string txtBarraNav)
    {
        UtilsPresentacionX5.SetTextLabel(LblTituloPagina, titulo);
        BNav.Text = txtBarraNav;
        //UtilsPresentacionX5.SetPaginationProperties(gv_Grilla);
        Page.Title = titulo;
    }

    private DataTable crearListaChecks()
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("Pais_PK", typeof(Int32));
        _dt.Columns.Add("Check", typeof(Boolean));

        _dt.AcceptChanges();

        return _dt;
        
    }

    protected void chkConConvenio_CheckedChanged(Object sender, EventArgs args)
    {
            CheckBox chk = sender as CheckBox;
            Boolean itemState = chk.Checked;
            GridViewRow row = ((GridViewRow)((DataControlFieldCell) chk.Parent).Parent);
            Int32 id = int.Parse(gvPaises.DataKeys[row.RowIndex].Value.ToString());
            ActualizarListaMod(id, itemState);
            //Int32 itemId = Int32.Parse(linkedItem.InputAttributes["Value"].ToString());
            //DataAccessLayer.UpdateLinkedItem(m_linkingItem, Utilities.GetCategoryItemFromId(itemId), itemState);
    }

    private void ActualizarListaMod(Int32 idPais, Boolean check)
    {
        DataTable _dt = ListaChecks;
        DataRow _drTemp;
        _drTemp = _dt.NewRow();
        _drTemp["Pais_PK"] = idPais;
        _drTemp["Check"] = check;

        _dt.Rows.Add(_drTemp);

        _dt.AcceptChanges();

        ListaChecks = _dt;
    }

    #region Carga GridViews
    private void CargarGridView()
    {
        string mensajeCarga = string.Empty;

        try
        {
            List<Pais> oPais = VariableSession.oPaisTodos;

            if (oPais != null)
            {
                gvPaises.DataSource = (DataTable)ToDatatable.toDataTable(oPais.ToList());
                gvPaises.DataBind();
            }
            else
                mensajeCarga = "Ocurrio un error al traer listado de países.";

            if (!mensajeCarga.Equals(string.Empty))
                MError.MensajeError = mensajeCarga;
        }
        catch (Exception ex)
        {
            log.Error("Ocurrió un error al intentar traer una lista. " + ex.Message);
            MError.MensajeError = "Ocurrió un error al intentar traer una lista. Intente mas tarde.";
            //btnGuardar.Enabled = false;
            btnGuardar.Enabled = false;
        }

    }
    #endregion Carga Combos

    protected void gvPaises_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            // Retrieve the state value for the current row. 
            CheckBox chkConvenio = (CheckBox)e.Row.Cells[1].FindControl("chkConConvenio");
            chkConvenio.Checked = (Boolean)rowView["ConConvenio"];
        }
    }
    

    #region Regresar
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (ListaChecks == null || ListaChecks.Rows.Count == 0)
            Response.Redirect("Main.aspx", false);
        else
        {
            mensaje.DescripcionMensaje = "Existen " + ListaChecks.Rows.Count + " países para guardar. </br>¿ Desea salir de todas formas ?";
            mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
            mensaje.QuienLLama = "btnRegresar_Click";
            mensaje.Mostrar();
        }
    }
    #endregion Regresar

    #region SI
    protected void ClickearonSi(object sender, string quienLlamo)
    {
        switch (quienLlamo.Trim())
        {
            case "btnRegresar_Click":
                ListaChecks = null;
                Response.Redirect("Main.aspx", false);
                break;
            case "btnGrabar_Click":
                GrabarModificaciones();
                break;
        }
        //Master.MensajeError = "";

    }
    #endregion SI

    #region NO
    protected void ClickearonNo(object sender, string quienLlamo)
    {
        //switch (quienLlamo.Trim())
        //{
        //    case "cmbRegresar_Click":

        //        #region Regresa sin guardar trabajo
        //        Response.Redirect("TCIndexExterno.aspx", false);

        //        #endregion
        //        break;
        //}
        //Master.MensajeError = "";
    }
    #endregion NO

    private void GrabarModificaciones()
    {
        int mensajeTot = 0;
        foreach(DataRow dr in ListaChecks.Rows)
        {
            string mensaje = ""; 
            InvocaWsDao.ModificaPais(Int32.Parse(dr["Pais_PK"].ToString()), Boolean.Parse(dr["Check"].ToString()), out mensaje);
            if(! mensaje.Equals(string.Empty))
                mensajeTot = mensajeTot + 1;

        }
        if (mensajeTot == 0)
        {
            ListaChecks = null;
            //Resetea la session para traer las modificaxciones
            VariableSession.oPaisConvenios = null;
            VariableSession.oPaisTodos = null;
            MError.MensajeError = "";
        }
        else
            MError.MensajeError = mensajeTot.ToString()+" países no pudieron modificarse por un error inesperado.</br> Revise el Log para mayor detalle.";
    }

    #region Boton Grabar
    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        if (ListaChecks == null || ListaChecks.Rows.Count == 0)
            MError.MensajeError = "No se han realizado cambios para guardar.";
        else
        {
            MError.MensajeError = string.Empty;

            mensaje.DescripcionMensaje = "Se van a guardar " + ListaChecks.Rows.Count + "países modificados. </br>¿ Desea proceder ?";
            mensaje.TipoMensaje = Mensaje.infoMensaje.Pregunta;
            mensaje.QuienLLama = "btnGrabar_Click";
            mensaje.Mostrar();
        }
        
    }
    #endregion Boton
}
