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
using System.Net;
using ConsultasWS;
using System.Threading;
using System.IO;

public partial class ListadosolicitudesProvisorias : System.Web.UI.Page
{
    protected List<ActoresWS.SolicitudProvisoria> iList = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
        }

    }

    #region Cargar Datos

    protected void CargarDatos()
    {
        try
        {
            if (Session["_solicToExport"] != null)
            {
                iList = (List<ActoresWS.SolicitudProvisoria>)Session["_solicToExport"];
                
                lblencabezado.Text = Request.QueryString["ec"].ToString();

                gridListadoSolicitudes.DataSource = ToDatatable.toDataTable(iList);
                gridListadoSolicitudes.DataBind();
            }
            else
            {

            }
        }
        catch
        {
            //Si no se encuentran los datos
            //lblMsjEncabezado.Text = "Error";
            //lblTextoInforme.Text = "Ocurrio un error al traer los datos del Instrumento";
        }
    }
    #endregion

    private DataTable toDataTable(List<SolicitudesEFechasSolicitud> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("ApeNomCompleto", typeof(String));
        //_dt.Columns.Add("CodPrestacion", typeof(Int16));
        _dt.Columns.Add("DescripcionPrestacion", typeof(String));
        _dt.Columns.Add("FAMSolicitud", typeof(String));
        //_dt.Columns.Add("Id_Beneficiario", typeof(Int64));
        _dt.Columns.Add("Mercosur", typeof(String));
        //_dt.Columns.Add("Pais_PK", typeof(Int32));
        _dt.Columns.Add("PaisDescCompleto", typeof(String));


        foreach (SolicitudesEFechasSolicitud oSol in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["ApeNomCompleto"] = oSol.ApeNomCompleto;
            //  _drTemp["CodPrestacion"] = oSol.CodPrestacion;
            _drTemp["DescripcionPrestacion"] = oSol.DescripcionPrestacion;
            _drTemp["FAMSolicitud"] = oSol.FAMSolicitud.ToShortDateString();
            //_drTemp["Id_Beneficiario"] = oSol.Id_Beneficiario;
            _drTemp["Mercosur"] = oSol.Mercosur ? "Si" : "No";
            //_drTemp["Pais_PK"] = oSol.Pais_PK;
            _drTemp["PaisDescCompleto"] = oSol.PaisDescCompleto;

            _dt.Rows.Add(_drTemp);
        }



        return _dt;
    }
}
