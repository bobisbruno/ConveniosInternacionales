using System;
using System.Text;
using System.Data;
using System.Web;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.Diagnostics;
using System.Web.SessionState;
//using System.Collections;
//using System.Collections.Specialized;
//using System.Security.Principal;

using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security;
using System.Reflection;
using System.Threading;
using System.Linq;
using System.Globalization;
using System.Collections.Specialized;


/// <summary>
/// Provee de funciones grales. de validacion de datos y administracion
/// 
/// </summary>
public class Util
{

    #region Contructores
    public Util()
    {

    }
    #endregion Contructores   

    #region Set Focus - Da el foco a un control
    public static void SetFocus(System.Web.UI.Page page, System.Web.UI.Control ctrl)
    {
        string s = "<script language='javascript'>" +
                   "document.getElementById('" + ctrl.ID + "').focus() </script>";

        if (!page.IsStartupScriptRegistered("focus"))
            page.RegisterStartupScript("focus", s);
    }
    #endregion

    #region habilita o deshabilita los items de un combo
    public static void EnableDisableValoresCombo(DropDownList iCombo, bool habilita)
    {
        foreach (ListItem ddlI in iCombo.Items)
            ddlI.Enabled = habilita;
        iCombo.DataBind();

    }
    #endregion habilita o deshabilita los items de un combo

    #region Tiene Acceso
    /// <summary>
    /// Valida el acceso del usuario que ingreso y lo guarda en un 
    /// variable de sesion para Luego validar a que funciones puede
    /// acceder. retorna Verdadero o falso si puede acceder a formulario
    /// </summary>
    //		public static bool TieneAcceso()
    //		{
    //return true;
    //			bool bAcesso = false;
    //			bool bHabilitarSeguridad = false;
    //			string sDominio = String.Empty;
    //
    //			
    //			string Admin = String.Empty;						//Para los grupos en el Web.config
    //			string Operador1 = String.Empty;
    //			string Operador3 = String.Empty;
    //			string Supervisor1 = String.Empty;
    //			string Supervisor4 = String.Empty;
    //
    //			System.Web.HttpContext _Context = System.Web.HttpContext.Current;
    //			WindowsPrincipal UsuarioAutenticado = System.Web.HttpContext.Current.User as WindowsPrincipal;
    //
    //			// Leo desde el Web.Config  para determinar si habilito o no 
    //			// la seguridad
    //			bHabilitarSeguridad = bool.Parse(ConfigurationManager.AppSettings["AplicarSeguridad"].ToString());
    //			sDominio = ConfigurationManager.AppSettings["Dominio"].ToString();
    //
    //			//Tomo los grupos
    //			Admin			=	ConfigurationManager.AppSettings["SISA-Admin"].ToString();
    //			Operador1	= ConfigurationManager.AppSettings["SISA-Operador1"].ToString();
    //			Operador3	= ConfigurationManager.AppSettings["SISA-Operador3"].ToString();
    //			Supervisor1	= ConfigurationManager.AppSettings["SISA-Supervisor1"].ToString();
    //			Supervisor4	= ConfigurationManager.AppSettings["SISA-Supervisor4"].ToString();
    //			
    //			if (bHabilitarSeguridad)
    //			{
    //				if (UsuarioAutenticado.IsInRole( sDominio + @"\" + Admin ))
    //				{
    //					_Context.Session["NIVEL"]="SISA-Admin";
    //				}
    //				else if (UsuarioAutenticado.IsInRole( sDominio+ @"\" +Operador1 ) )
    //				{
    //					_Context.Session["NIVEL"]="SISA-Operador1";
    //				}
    //				else if (UsuarioAutenticado.IsInRole( sDominio+ @"\" + Operador3 ) )
    //				{
    //					_Context.Session["NIVEL"]="SISA-Operador3";
    //				}
    //				else if (UsuarioAutenticado.IsInRole( sDominio+ @"\" + Supervisor1 ) )
    //				{
    //					_Context.Session["NIVEL"]="SISA-Supervisor1";
    //				}
    //				else if (UsuarioAutenticado.IsInRole( sDominio+ @"\" + Supervisor4 ) )
    //				{
    //					_Context.Session["NIVEL"]="SISA-Supervisor4";
    //				}
    //				else
    //				{
    //					_Context.Session["NIVEL"]=String.Empty;
    //			
    //				}
    //
    //
    //				// Obtengo el nombre del formulario al que se esta intentando
    //				// ingresar
    //				string Formulario =_Context.Request.RawUrl.Split(char.Parse("/"))[2].ToUpper();
    //
    //				// Este es el DataSet donde guardo los datos leido del XML.
    //				DataSet _dsAcceso = new DataSet( ).ReadXml(AppDomain.CurrentDomain.BaseDirectory + @"\Acceso.config") ;
    //
    //				_dsAcceso.Tables[0].DefaultView.RowFilter= "IDGrupo = '" + _Context.Session["NIVEL"].ToString().ToUpper() +"'" ;
    //				_dsAcceso.Tables[0].DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
    //
    //				if ( _dsAcceso.Tables[0].DefaultView.Count > 0 )
    //				{
    //					DataView _dv	= new DataView( _dsAcceso.Tables[0].DefaultView ) ;
    //					
    //					_Context.Session["Acceso"] = _dv;
    //
    //					_dv.Dispose();
    //
    //					bAcesso=true; 
    //				} 
    //				
    //				//				switch (_Context.Session["NIVEL"].ToString().ToUpper())
    //				//				{
    //				//
    //				//					case "SISA-ADMIN" :
    //				//						if(Formulario == "AUDITORES.ASPX" ||
    //				//							Formulario == "CAPITULO.ASPX" ||
    //				//							Formulario == "ESTADONOTAS.ASPX" ||
    //				//							Formulario == "ESTSEGUIMIENTO.ASPX" ||
    //				//							Formulario == "ORGANISMO.ASPX" ||
    //				//							Formulario == "PAA.ASPX" ||
    //				//							Formulario == "RIESGO.ASPX" ||
    //				//							Formulario == "TIPOAUDITORIA.ASPX" ||
    //				//							Formulario == "TIPODOCUMENTO.ASPX" ||
    //				//							Formulario == "TIPOINFORME.ASPX" ||
    //				//							Formulario == "TEMARIESGO.ASPX" ||
    //				//							Formulario == "TIPOTAREA.ASPX" )
    //				//						{
    //				//							bAcesso = true;
    //				//						}
    //				//						break;
    //				//				
    //				//					case "SISA-OPERADOR1":
    //				//
    //				//						if (Formulario == "INFORME.ASPX" ||
    //				//							Formulario == "CONSULTA.ASPX" )
    //				//						{
    //				//							bAcesso=true;
    //				//						}
    //				//						break;
    //				//
    //				//					case "SISA-OPERADOR3":
    //				//
    //				//						if (Formulario == "INFORME.ASPX" ||
    //				//							Formulario == "CONSULTA.ASPX" )
    //				//						{
    //				//							bAcesso=true;
    //				//						}
    //				//						break;
    //				//
    //				//					case "SISA-SUPERVISOR1":
    //				//					case "SISA-SUPERVISOR4":
    //				//
    //				//						if (Formulario == "INFORME.ASPX" ||
    //				//							Formulario == "CONSULTA.ASPX" ||
    //				//							Formulario == "CONFIRMACION.ASPX" ||							//	de aqui para abajo hay que quitarlo 
    //				//							Formulario == "AUDITORES.ASPX" ||
    //				//							Formulario == "CAPITULO.ASPX" ||
    //				//							Formulario == "ESTADONOTAS.ASPX" ||
    //				//							Formulario == "ESTSEGUIMIENTO.ASPX" ||
    //				//							Formulario == "ORGANISMO.ASPX" ||
    //				//							Formulario == "PAA.ASPX" ||
    //				//							Formulario == "RIESGO.ASPX" ||
    //				//							Formulario == "TIPOAUDITORIA.ASPX" ||
    //				//							Formulario == "TIPODOCUMENTO.ASPX" ||
    //				//							Formulario == "TIPOINFORME.ASPX" ||
    //				//							Formulario == "TEMARIESGO.ASPX" ||
    //				//							Formulario == "TIPOTAREA.ASPX" )
    //				//						{
    //				//							bAcesso=true;
    //				//						}
    //				//						break;
    //				//				}
    //
    //				return bAcesso;
    //			}
    //			else
    //			{
    //				return true;
    //			}
    //		}

    #endregion

    #region LLenar Combo
    /// <summary>
    /// Procedimiento que llena un DropDownList en base a un Tabla especificada
    /// </summary>
    /// <param name="ComboBox">Objeto de tipo DropDowList.</param>
    /// <param name="ObjTabla">Objeto de tipo String que representa el nombre de la tabla.</param>
    public static void LLenarCombo(DropDownList comboBox, string ObjTabla)
    {
        //LLenarCombo(comboBox, ObjTabla, null);
        //DropDownList _Combo = (DropDownList)ComboBox;
        switch (ObjTabla.ToUpper())
        {
            case "CRITERIOBUSQUEDA":
                comboBox.Items.Insert(0, new ListItem("[ Seleccione ]", "0"));
                comboBox.Items.Insert(1, new ListItem("Descuento Total", "1"));
                comboBox.Items.Insert(2, new ListItem("Descuento Proporcional", "2"));
                comboBox.Items.Insert(3, new ListItem("No Descontado", "3"));
                break;
            case "CRITERIOFILTRADO":
                comboBox.Items.Insert(0, new ListItem("Sin Filtro", "0"));
                comboBox.Items.Insert(1, new ListItem("Nro Beneficiario", "1"));
                comboBox.Items.Insert(2, new ListItem("Tipo Concepto", "3"));
                comboBox.Items.Insert(3, new ListItem("Concepto", "4"));
                break;
            case "CRITERIOFILTRADO_AGENCIAS":
                //_Combo.Items.Insert(0,new ListItem("Sin Filtro","0") );
                comboBox.Items.Insert(0, new ListItem("Nro Legajo", "1"));
                comboBox.Items.Insert(1, new ListItem("Nombre de Agencia", "2"));
                break;
            case "CRITERIOFILTRADO_CONSNOVEDADES":
                LLenarCombo(comboBox, "CRITERIOFILTRADO");
                comboBox.Items.Insert(4, new ListItem("Entre Fechas", "5"));
                break;
            default:
                LLenarCombo(comboBox, ObjTabla, null);
                break;
        }
    }

    /// <summary>
    /// Procedimiento que llena un DropDownList en base a un Tabla especificada
    /// </summary>
    /// <param name="ComboBox">Objeto de tipo DropDowList.</param>
    /// <param name="ObjTabla">Objeto de tipo String que representa el nombre de la tabla.</param>
    /// <param name="aParam">Array de objetos que para ejecutar los metodos de traer.</param>
    public static void LLenarCombo(DropDownList comboBox, string objTabla, Object[] aParam)
    {   
        //DataSet _datos = new DataSet();
        System.Web.HttpContext oContext = System.Web.HttpContext.Current;
        //_datos = (DataSet)_Context.Session["Prestador"];

        //List<WSTipoConcConcLiq.ConceptoLiquidacion> unaListaTipoConcepto = null;
        //comboBox.Items.Clear();	//Elimina todos los elementos de Combo.

        //switch (objTabla.ToString().ToUpper())
        //{
        //    case "TIPOCONCEPTO":
        //        unaListaTipoConcepto = (List<WSTipoConcConcLiq.ConceptoLiquidacion>)IsInCache("TIPOCONCEPTO", 3, aParam, false);

        //        var groupTipoConcepto = from o in unaListaTipoConcepto
        //                                group o by o.UnTipoConcepto.IdTipoConcepto into groupTC
        //                                select groupTC.ToList();

        //        unaListaTipoConcepto = new List<WSTipoConcConcLiq.ConceptoLiquidacion>();
        //        groupTipoConcepto.ToList().ForEach(delegate(List<WSTipoConcConcLiq.ConceptoLiquidacion> lsttc) { unaListaTipoConcepto.Add(lsttc[0]); });

        //        if (unaListaTipoConcepto.Count() > 0)
        //        {
        //            comboBox.DataSource = unaListaTipoConcepto;                    
        //            comboBox.DataTextField = "DescTipoConcepto";
        //            comboBox.DataValueField = "IdTipoConcepto";
        //            comboBox.DataBind();
        //        }
        //        break;

        //    case "CONCEPTOOPP":
        //        unaListaTipoConcepto = (List<WSTipoConcConcLiq.ConceptoLiquidacion>)IsInCache("CONCEPTOOPP");

        //        var unGroup = from o in unaListaTipoConcepto
        //                     where o.UnTipoConcepto.IdTipoConcepto.ToString() == aParam[0].ToString()
        //                     group o by o.CodConceptoLiq into groupCL                         
        //                     select groupCL.ToList();
                
        //        List<WSTipoConcConcLiq.ConceptoLiquidacion> unConceptoLiquidacion = new List<WSTipoConcConcLiq.ConceptoLiquidacion>();

        //        unGroup.ToList().ForEach(delegate(List<WSTipoConcConcLiq.ConceptoLiquidacion> tc) { unConceptoLiquidacion.Add(tc.ToList()[0]); });

        //        if (unConceptoLiquidacion.Count() > 0)
        //        {
        //            comboBox.DataSource = unConceptoLiquidacion;                    
        //            comboBox.DataTextField = "DescConceptoLiq";
        //            comboBox.DataValueField = "CodConceptoLiq";
        //            comboBox.DataBind();
        //        }

        //        break;

        //    case "CIERRES":
        //        List<WSCierre.Cierre> unaListaCierres = new List<WSCierre.Cierre>((WSCierre.Cierre[]) IsInCache("CIERRES"));

        //        //unaListaCierres = ;  
        //        //_datos = IsInCache("CIERRES", 3, aParam, false);

        //        if (unaListaCierres.Count > 0)
        //        {
        //            comboBox.DataSource = unaListaCierres;
        //            comboBox.DataTextField = "Mensual";
        //            comboBox.DataValueField = "FecCierre";
        //            comboBox.DataBind();
        //        }
        //        break;
        //}
        //if (_datos.Tables.Count > 0) { comboBox.DataBind(); }
        comboBox.Items.Insert(0, "[ Seleccione ]");
        comboBox.SelectedIndex = -1;

        //datos.Dispose();
        comboBox.Dispose();
    }

    //public static void LLenarCombo(Object ComboBox, string ObjTabla, Object[] aParam, DataSet _datos)
    public static void LLenarCombo(Object ComboBox, string ObjTabla, Object[] aParam, object _datos)
    {

        DropDownList _Combo = (DropDownList)ComboBox;

        _Combo.Items.Clear();  //Elimina todos los elementos de Combo.
        string filtro = string.Empty;
        //String conceptoPCAbuelito = new Clases.Parametro().ConceptoPCAbuelito();
        //String conceptoCPAT = new Clases.Parametro().ConceptoCPAT();
        //String conceptoVamosPaseo = new Clases.Parametro().ConceptoVamosPaseo();
        
        //List<WSPrestador.ConceptoLiquidacion> unaListaConceptoLiquidacion = null;

        //switch (ObjTabla.ToString().ToUpper())
        //{
        //    case "TIPOCONCEPTO":
        //        #region
        //        /* Si el parametro es:
        //         *	 0 trae todo
        //         *	 1 todo menos lo indeterminado
        //         *	 2 solo indeterminado		
        //         *	 3 solo en cuotas
        //         * */

        //        //var x = (from i in _datos select i).ToList();
        //        //List<WSPrestador.ConceptoLiquidacion> __datos = new List<WSPrestador.ConceptoLiquidacion>((WSPrestador.ConceptoLiquidacion[])_datos);

        //        //var y = (from i in __datos
        //        //     select new { IdTipoConcepto = i.UnTipoConcepto.IdTipoConcepto, DescTipoConcepto = i.UnTipoConcepto.DescTipoConcepto }).Distinct();

        //        unaListaConceptoLiquidacion = new List<WSPrestador.ConceptoLiquidacion>((WSPrestador.ConceptoLiquidacion[])_datos);
        //        var unaListaTipoConcepto = (from o in unaListaConceptoLiquidacion                                            
        //                                    select new { IdTipoConcepto = o.UnTipoConcepto.IdTipoConcepto, DescTipoConcepto = o.UnTipoConcepto.DescTipoConcepto } ).Distinct().ToList();
                                            
        //        switch (byte.Parse(aParam[0].ToString()))
        //        {
        //            case 1:
        //                filtro = "TipoConcepto not in (1,6)";
        //                unaListaTipoConcepto = (from o in unaListaConceptoLiquidacion
        //                                        where o.UnTipoConcepto.IdTipoConcepto != 1 && o.UnTipoConcepto.IdTipoConcepto != 6
        //                                        select new { IdTipoConcepto = o.UnTipoConcepto.IdTipoConcepto, DescTipoConcepto = o.UnTipoConcepto.DescTipoConcepto }).Distinct().ToList();
        //                break;
        //            case 2:
        //                filtro = "TipoConcepto in (1,6)";
        //                unaListaTipoConcepto = (from o in unaListaConceptoLiquidacion
        //                                        where o.UnTipoConcepto.IdTipoConcepto == 1 || o.UnTipoConcepto.IdTipoConcepto == 6
        //                                        select new { IdTipoConcepto = o.UnTipoConcepto.IdTipoConcepto, DescTipoConcepto = o.UnTipoConcepto.DescTipoConcepto }).Distinct().ToList();

        //                break;
        //            case 3:
        //                filtro = "TipoConcepto = 3 ";
        //                unaListaTipoConcepto = (from o in unaListaConceptoLiquidacion
        //                                        where o.UnTipoConcepto.IdTipoConcepto == 3
        //                                        select new { IdTipoConcepto = o.UnTipoConcepto.IdTipoConcepto, DescTipoConcepto = o.UnTipoConcepto.DescTipoConcepto }).Distinct().ToList();

        //                break;
        //            default:
        //                break;
        //        }

        //        if (unaListaTipoConcepto.Count > 0)
        //        {
        //            _Combo.DataSource = unaListaTipoConcepto;
        //            _Combo.DataTextField = "DescTipoConcepto";
        //            _Combo.DataValueField = "IdTipoConcepto";
        //        }

        //        break;
        //        #endregion
        //    case "CONCEPTOOPP":
        //        #region
        //        //fga 20090610 Resolucion 257/09
        //        //filtro = "TipoConcepto = " + aParam[0] + " and CodConceptoLiq not in (" + conceptoPCAbuelito + "," + conceptoCPAT + "," + conceptoVamosPaseo + ")";

        //        unaListaConceptoLiquidacion = new List<WSPrestador.ConceptoLiquidacion>((WSPrestador.ConceptoLiquidacion[])_datos);

        //        //var x = (from i in __datos1
        //        //         where aParam[0].ToString() == i.UnTipoConcepto.IdTipoConcepto.ToString()
        //        //         select new { CodConceptoLiq = i.CodConceptoLiq, DescConceptoLiq = i.DescConceptoLiq }).Distinct();
        //        var listConceptoLiquidacion = (from i in unaListaConceptoLiquidacion
        //                                       where aParam[0].ToString() == i.UnTipoConcepto.IdTipoConcepto.ToString()
        //                                       select new {ConceptoLiquidacion = i}.ConceptoLiquidacion).ToList().Distinct();

        //        if (listConceptoLiquidacion.ToList().Count > 0)
        //        {
        //            _Combo.DataSource = listConceptoLiquidacion.ToList();
        //            _Combo.DataTextField = "DescConceptoLiq";
        //            _Combo.DataValueField = "CodConceptoLiq";
        //        }

        //        break;
        //        #endregion
        //    case "CONCEPTOPCABUELITO":
        //        #region
        //        //filtro = "TipoConcepto = " + aParam[0] + " and CodConceptoLiq in (" + conceptoPCAbuelito + ")";
                
        //        //_datos.Tables[1].DefaultView.RowFilter = filtro;

        //        //if (_datos.Tables[0].Rows.Count > 0)
        //        //{
        //        //    _Combo.DataSource = _datos.Tables[1].DefaultView;
        //        //    _Combo.DataTextField = "DescConceptoLiq";
        //        //    _Combo.DataValueField = "CodConceptoLiq";
        //        //}

        //        break;
        //    //FGA 20090610 Resolucion 257/09 - Caja Popular de ahorros de pcia Tucuman
        //    #endregion
        //    case "CONCEPTOCPAT":
        //        #region

        //        //filtro = "TipoConcepto = " + aParam[0] + " and CodConceptoLiq in (" + conceptoCPAT + ")";

        //        //_datos.Tables[1].DefaultView.RowFilter = filtro;

        //        //if (_datos.Tables[0].Rows.Count > 0)
        //        //{
        //        //    _Combo.DataSource = _datos.Tables[1].DefaultView;
        //        //    _Combo.DataTextField = "DescConceptoLiq";
        //        //    _Combo.DataValueField = "CodConceptoLiq";
        //        //}

        //        break;
        //    //FGA 20100121 - Vamos de paseo
        //    #endregion
        //    case "CONCEPTOVAMOSPASEO":
        //        #region
        //        //filtro = "TipoConcepto = " + aParam[0] + " and CodConceptoLiq in (" + conceptoVamosPaseo + ")";
                
        //        //_datos.Tables[1].DefaultView.RowFilter = filtro;

        //        //if (_datos.Tables[0].Rows.Count > 0)
        //        //{
        //        //    _Combo.DataSource = _datos.Tables[1].DefaultView;
        //        //    _Combo.DataTextField = "DescConceptoLiq";
        //        //    _Combo.DataValueField = "CodConceptoLiq";
        //        //}

        //        break;
        //    #endregion
        //}
 
        if (_datos!=null) { _Combo.DataBind(); }

        _Combo.Items.Insert(0, "[ Seleccione ]");
        _Combo.SelectedIndex = -1;
        _Combo.Dispose();
    }

    #endregion LLenar Combo

    #region IsInCache - Abre tablas que seran utilizadas frecuentemente"

    /// <summary>
    /// Permite abrir las tablas, llenar un dataset y colocarlas en el Cache
    /// </summary>
    /// <param name="NombreTabla">Tipo string.Representa el nombre de la tabla
    ///	con un tiempo por default de 10 Minutos.</param>
    /// <returns>Un DataSet</returns>
    public static object IsInCache(string NombreTabla)
    {
        return IsInCache(NombreTabla, 5);
    }

    /// <summary>
    /// Permite abrir las tablas, llenar un dataset y colocarlas en el Cache
    /// </summary>
    /// <param name="NombreTabla">Tipo string.Representa el nombre de la tabla</param>
    /// <param name="TiempoMinCache">Tipo int. Representa el tiempo en Minutos del cache</param>		
    /// <returns>Un DataSet</returns>
    public static object IsInCache(string NombreTabla, int TiempoMinCache)
    {
        return IsInCache(NombreTabla, TiempoMinCache, null, false);
    }

    /// <summary>
    /// Permite abrir las tablas, llenar un dataset y colocarlas en el Cache
    /// </summary>
    /// <param name="NombreTabla">Tipo string.Representa el nombre de la tabla</param>
    /// <param name="Refrescar">Tipo Boolean. Representa si se desea volver a traer los datos desde la base</param>
    /// <returns>Un DataSet</returns>
    public static object IsInCache(string NombreTabla, bool Refrescar)
    {
        return IsInCache(NombreTabla, 5, null, Refrescar);
    }

    /// <summary>
    /// Permite abrir las tablas, llenar un dataset y colocarlas en el Cache
    /// </summary>
    /// <param name="NombreTabla">Tipo string.Representa el nombre de la tabla</param>
    /// <param name="TiempoMinCache">Tipo int. Representa el tiempo en Minutos del cache</param>		
    /// <param name="Refrescar">Tipo Boolean. Representa si se desea volver a traer los datos desde la base</param>
    /// <returns>Un DataSet</returns>
    public static object IsInCache(string NombreTabla, int TiempoMinCache, Object[] aParam, bool Refrescar)
    {
        object datos = new object();
        System.Web.HttpContext oContext = System.Web.HttpContext.Current;

        // Elimino del Cache el Item seleccionado para volver a cargarlo.
        if (Refrescar) 
        { oContext.Cache.Remove(NombreTabla); }

        if (oContext.Cache[NombreTabla] != null)
        {
            datos = (object)oContext.Cache[NombreTabla];
        }
        else
        {
            //string Valor;
            switch (NombreTabla.ToString().ToUpper())
            {
                case "TIPOCONCEPTO":
                case "CONCEPTOOPP":
                    //List<WSTipoConcConcLiq.ConceptoLiquidacion> unaListaTipoConcepto = InvocaWsDao.Traer_ConceptosLiq_TxPrestador(long.Parse(aParam[0].ToString())); //Corresponde al ID Prestador
                    //datos = (object)unaListaTipoConcepto;
                    break;
                
                case "CIERRES":
                    //WSCierre.CierreWS cie = new WSCierre.CierreWS();
                    //cie.Url = ConfigurationManager.AppSettings["url.Servicio.Cierres"];
                    //cie.Credentials = CredentialCache.DefaultCredentials;
                    //datos = cie.TraerCierresAnteriores();
                    //cie.Dispose();
                    break;
            }

            // Agrego al Cache
            oContext.Cache.Insert(NombreTabla, datos, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(TiempoMinCache));
        }

        return datos;
    }
   
    #endregion  IsInCache - Abre tablas que seran utilizadas frecuentemente

    #region Retorna el Path relativo
    /// <summary>
    /// Este método returna el path relativo 
    /// </summary>
    /// <param name="request">Objeto System.Web.HttpRequest </param>
    /// <returns>El Path relativo</returns>
    /// <example>Util.GetRelativePath( Request )</example>
    public static string GetRelativePath(HttpRequest request)
    {

        string sPath = String.Empty;

        try
        {
            if (request.ApplicationPath != "/") { sPath = request.ApplicationPath; }

        }
        catch (Exception err)
        {
            throw err;
        }

        return sPath;

    }
    #endregion

    #region validoIP
    public static bool ValidoIP(string IP)
    {
        try
        {
            IPAddress ip = IPAddress.Parse(IP);
        }
        catch
        {
            return false;
        }
        return true;
    }





    #endregion

    #region CleanInput - Limpia todos los caracteres No-alfanumericos
    public static string CleanInput(string strIn)
    {
        //Reemplaza Caracteres no Alfa Por Blancos. 
        Regex _patron = new Regex("[^A-Za-z0-9]");
        return _patron.Replace(strIn, "");
    }
    #endregion

    #region Convertir a Double - Respetando separador de decimales
    public static double ConvertToDouble(string Valor)
    {
        string Separador = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        return double.Parse(Valor.Replace(",", Separador).Replace(".", Separador));
    }
    #endregion

    #region Diferencia entre 2 (dos) fechas
    public static string DateDiff(string FechaDesde, string FechaHasta)
    {

        try
        {
            TimeSpan Dif = DateTime.Parse(FechaHasta).Subtract(DateTime.Parse(FechaDesde));
            return Dif.Days.ToString();
        }
        catch (Exception)
        {
            return "";
        }

    }


    #endregion

    #region Valida Fecha

    public static bool EsFecha(string Valor)
    {
        DateTime unFechavalida;
        bool esValido = false;

        if (DateTime.TryParse(Valor, out unFechavalida))
        {
            String Patron = "^\\d{2}/\\d{2}/\\d{4}$";
            Regex ExpresionRegular = new Regex(Patron);
            esValido = ExpresionRegular.IsMatch(Valor);
        }

        return esValido;

        //return DateTime.TryParse(Valor, out unFechavalida);

    }

    public static bool esRangoFechaValido(string Fecha, Int32 minValue, Int32 maxValue)
    {
        DateTime unFechavalida;
        bool esValido = false;

        if (DateTime.TryParse(Fecha, out unFechavalida))
        {
            if (  (unFechavalida >= DateTime.Today.AddYears(-minValue)) && (unFechavalida <= DateTime.Today)   )
                esValido = true;
        }

        return esValido;

    }

    public static bool esFechaValida(string Fecha)
    {
        if (Fecha.Length != 10)
            return false;

        string dia = Fecha.Trim().Substring(0, 2);
        string mes = Fecha.Trim().Substring(3, 2);
        string ano = Fecha.Trim().Substring(6, 4);

        if ((dia.Length == 0) || (dia.Length != 2) || (mes.Length == 0) || (mes.Length != 2) || (ano.Length == 0) || (ano.Length != 4))
            return false;
        else if ((!esNumerico(dia)) || (!esNumerico(mes)) || (!esNumerico(ano)))
            return false;

        else if ((int.Parse(mes) > 12) || (int.Parse(mes) < 1))
            return false;

        else if (int.Parse(dia) < 1)
            return false;

        else if ((int.Parse(mes) == 1) || (int.Parse(mes) == 3) || (int.Parse(mes) == 5) || (int.Parse(mes) == 7) || (int.Parse(mes) == 8) || (int.Parse(mes) == 10) || (int.Parse(mes) == 12))
        {
            if (int.Parse(dia) > 31)
                return false;
        }
        else if ((int.Parse(mes) == 4) || (int.Parse(mes) == 6) || (int.Parse(mes) == 9) || (int.Parse(mes) == 11))
        {
            if (int.Parse(dia) > 30)
                return false;
        }
        else
        {
            int anio = int.Parse(ano);
            if (((anio % 4) == 0) && (((anio % 100) != 0) || (anio % 400) == 0))
            {
                if (int.Parse(dia) > 29)
                    return false;
                else if (int.Parse(dia) > 28)
                    return false;
            }
        }
        return true;
    }


    #endregion Valida Fecha

    #region Valida Ingreso de Numeros

    public static bool esNumerico(string Valor, Int16 cifras)
    {
        bool ValidoDatos = false;

        Regex numeros = new Regex(@"^[0-9]{"+ cifras + "," + cifras + "}?$");
        

        if (Valor.Length != 0)
        {
            ValidoDatos = numeros.IsMatch(Valor);
        }
        return ValidoDatos;
    }

    public static bool esNumerico(string Valor)
    {
        bool ValidoDatos = false;

        Regex numeros = new Regex(@"^\d{1,}$");

        if (Valor.Length != 0)
        {
            ValidoDatos = numeros.IsMatch(Valor);
        }
        return ValidoDatos;
    }

    #endregion

    #region Es Numerio con decimales
    /// <summary>
    /// Controla si es un numero válido con o sin Decimales
    /// </summary>
    /// <param name="Valor"></param>
    /// <returns></returns>
    public static bool EsNumerioConDecimales(string Valor)
    {
        bool ValidoDatos = false;

        Regex numeros = new Regex(@"^\d{1,}\.\d{2}$|^\d{1,}$");

        if (Valor.Length != 0)
        {
            ValidoDatos = numeros.IsMatch(Valor);
        }
        return ValidoDatos;
        //			bool ValidoDatos=false;
        //
        //			string Separador = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator ;
        //
        //			
        //		
        //			Regex numeros = new Regex(@"^\d{1,}\" + Separador + @"\d{2}$|^\d{1,}$");
        //
        //			if ( Valor.Length != 0 )
        //			{
        //				ValidoDatos =numeros.IsMatch( Valor.Replace(",", Separador ).Replace(".",Separador ) ) ;
        //			}
        //			return ValidoDatos;
    }

    #endregion

    #region ValidaMail
    /// <summary>
    /// Controla si es un numero válido con o sin Decimales
    /// </summary>
    /// <param name="Valor"></param>
    /// <returns></returns>
    public static bool ValidaMail(string Valor)
    {
        bool ValidoDatos = false;

        Regex numeros = new Regex(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})");

        if (Valor.Length != 0)
        {
            ValidoDatos = numeros.IsMatch(Valor);
        }
        return ValidoDatos;
    }


    #endregion

    #region Validacion de CUIL CUIT

    public static bool ValidoCuil(string cuil)
    {
        string FACTORES = "54327654321";
        double dblSuma = 0;
        bool resul = false;

        if (cuil == null)
            return false;

        cuil = cuil.Replace("-", string.Empty);
        if (!(cuil.Substring(0, 1).ToString() != "3" && cuil.Substring(0, 1).ToString() != "2"))
        {
            for (int i = 0; i < 11; i++)
                dblSuma = dblSuma + int.Parse(cuil.Substring(i, 1).ToString()) * int.Parse(FACTORES.Substring(i, 1).ToString());
        }

        resul = Modulo(dblSuma) == 0;
        return resul;
    }
    private static double Modulo(double numero)
    {
        double resul = Math.IEEERemainder(numero, 11);
        return resul;
    }

    public static bool ValidoCuit(string cuit)
    {
        if (cuit == null)
            return false;
        //Quito los guiones, el cuit resultante debe tener 11 caracteres.  
        cuit = cuit.Replace("-", string.Empty);
        if (cuit.Length != 11)
            return false;
        else
        {
            int calculado = CalcularDigitoCuit(cuit);
            int digito = int.Parse(cuit.Substring(10));
            return calculado == digito;
        }
    }
    public static int CalcularDigitoCuit(string cuit)
    {
        int[] mult = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
        char[] nums = cuit.ToCharArray();
        int total = 0;
        for (int i = 0; i < mult.Length; i++)
        {
            total += int.Parse(nums[i].ToString()) * mult[i];
        }
        var resto = total % 11;
        return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;
    }

    #endregion

    #region Vista de Etiquetas
    public static void SetVistaEtiquetas(Label lblCtrl, bool visible)
    {
        lblCtrl.Visible = visible;
        lblCtrl.Text = visible ? "No Informa" : "";
    }
    //public static void SetVistaEtiquetas(Label lblCtrl, String txt)
    //{
    //    lblCtrl.Visible = true;
    //    lblCtrl.Text = txt;
    //}
    public static void SetVistaEtiquetas(Label lblCtrl, String txt)
    {
        lblCtrl.Visible = !txt.Equals(string.Empty);
        lblCtrl.Text = txt;
    }
    public static void SetVistaEtiquetas(Label lblCtrl, String txt, bool visible)
    {
        lblCtrl.Visible = visible;
        lblCtrl.Text = txt;
    }
    #endregion Vista de Etiquetas

    #region Mes to Texto
    public static string MesNumToEqTexto(string numMes)
    {
        switch (numMes)
        {
            case "1":
                return "Enero";
                
            case "2":
                return "Febrero";
                
            case "3":
                return "Marzo";
                
            case "4":
                return "Abril";
                
            case "5":
                return "Mayo";
                
            case "6":
                return "Junio";
                
            case "7":
                return "Julio";
                
            case "8":
                return "Agosto";
                
            case "9":
                return "Septiembre";
                
            case "10":
                return "Octubre";
                
            case "11":
                return "Noviembre";
                
            case "12":
                return "Diciembre";
            default:
                return "";
        }
    }


    #endregion Mes to Texto
    #region Rellena Strings

    public static String RellenaIzquierda(long str, int longitud, char caracter)
    {
        return RellenaIzquierda(str.ToString(), longitud, caracter);
    }

    public static String RellenaIzquierda(int str, int longitud, char caracter)
    {
        return RellenaIzquierda(str.ToString(), longitud, caracter);
    }

    public static String RellenaIzquierda(short str, int longitud, char caracter)
    {
        return RellenaIzquierda(str.ToString(), longitud, caracter);
    }

    public static String RellenaIzquierda(String str, int longitud, char caracter)
    {
        if (str == null)
            str = "";

        str = str.Trim();

        for (int i = str.Length; i < longitud; i++)
            str = caracter.ToString() + str;

        return str;
    }


    public static String RellenaDerecha(long str, int longitud, char caracter)
    {
        return RellenaDerecha(str.ToString(), longitud, caracter);
    }

    public static String RellenaDerecha(int str, int longitud, char caracter)
    {
        return RellenaDerecha(str.ToString(), longitud, caracter);
    }

    public static String RellenaDerecha(short str, int longitud, char caracter)
    {
        return RellenaDerecha(str.ToString(), longitud, caracter);
    }

    public static String RellenaDerecha(String str, int longitud, char caracter)
    {
        if (str == null)
            str = "";

        str = str.Trim();

        for (int i = str.Length; i < longitud; i++)
            str = str + caracter.ToString();

        return str;
    }


    #endregion Rellena Strings


    #region ToPascal()

    public static string ToPascal(string pValue)
    {
        string wRet = string.Empty;
        if (pValue == string.Empty)
        {
            wRet = string.Empty;
        }
        else
        {
            try
            {
                string[] aPalabras = Regex.Split(pValue, " ");
                for (int i = 0; i < aPalabras.Length; i++)
                {
                    wRet += " " + aPalabras[i].Substring(0, 1).ToUpper() + aPalabras[i].Substring(1, aPalabras[i].Length - 1).ToLower();
                }
                wRet = wRet.Trim();
            }
            catch
            {
                wRet = string.Empty;
            }
        }
        return wRet;
    }

    public static string ToPascal(object objeto)
    {
        string wRet = string.Empty;
        string valor = objeto.ToString();
        if (valor == string.Empty)
        {
            wRet = string.Empty;
        }
        else
        {
            try
            {
                string[] aPalabras = Regex.Split(valor, " ");
                for (int i = 0; i < aPalabras.Length; i++)
                {
                    wRet += " " + aPalabras[i].Substring(0, 1).ToUpper() + aPalabras[i].Substring(1, aPalabras[i].Length - 1).ToLower();
                }
                wRet = wRet.Trim();
            }
            catch
            {
                wRet = string.Empty;
            }
        }
        return wRet;
    }
    #endregion

    #region Fromateo CUIL
    /// <summary>
    /// Formatea un Numero de CUIL 12-12345678-1
    /// </summary>
    /// <param name="Numero">el Numero de Expdiente a formatear</param>
    /// <param name="PonerGiones">true para ponerle los giones</param>
    /// <returns>Número de Expediente formateado.</returns>
    public static string FormateoCUIL(string Numero, bool PonerGiones)
    {
        string sCUIL = Numero.Replace("-", "");

        if (!PonerGiones)
        {
            return sCUIL;
        }
        else
        {
            if (sCUIL.Length == 11)
            {
                sCUIL = sCUIL.Substring(0, 2).ToString() + "-" + sCUIL.Substring(2, 8).ToString() +
                        "-" + sCUIL.Substring(10, 1).ToString();
            }
        }
        return sCUIL;
    }
    #endregion

    public static string FormateoFecha(string Fecha, bool PonerBarra)
    {
        string sFecha = Fecha.Replace("/", "");

        if (!PonerBarra)
        {
            return sFecha;
        }
        else
        {
            if (sFecha.Length == 8)
            {
                sFecha = sFecha.Substring(0, 2).ToString() + "/" + sFecha.Substring(2, 2).ToString() +
                        "/" + sFecha.Substring(4, 4).ToString();
            }
        }
        return sFecha;
    }

    #region formato para Error

    public static string FormatoError(string Error)
    {
        return "Errores detectados:<div style='margin-left:20px; margin-bottom:10px'> " + Error + "</div>";
    }

    #endregion

    public static String PutBRs(string strOrigen, int cantCharLine)
    {
        String strdestino = "";
        int cont = 0;
        foreach(char c in strOrigen)
        {
            cont ++;
            if ((c == ' ') && cont > cantCharLine)
            {
                strdestino = strdestino + "<br/>";
                cont = 0;
            }
            else
                strdestino = strdestino + c;
        }
        return strdestino;
    }

    public static void LLenarRadioButtonList(Object Radio, string ObjTabla, Object[] aParam, DataSet _datos)
    {

        RadioButtonList _Radio = (RadioButtonList)Radio;

        _Radio.Items.Clear();				//Elimina todos los elementos de Combo.

        string filtro = string.Empty;
        switch (ObjTabla.ToString().ToUpper())
        {
            case "TIPOCONCEPTO":
                /* Si el parametro es:
                                 *	 0 trae todo
                                 *	 1 todo menos lo indeterminado
                                 *	 2 solo indeterminado					 * 
                                 * */

                switch (byte.Parse(aParam[0].ToString()))
                {
                    case 1:
                        filtro = "TipoConcepto not in (1,6)";
                        break;
                    case 2:
                        filtro = "TipoConcepto in (1,6)";
                        break;
                    default:
                        break;
                }

                _datos.Tables[0].DefaultView.RowFilter = filtro;
                if (_datos.Tables[0].Rows.Count > 0)
                {
                    _Radio.DataSource = _datos.Tables[0].DefaultView;
                    _Radio.DataTextField = "DescTipoConcepto";
                    _Radio.DataValueField = "TipoConcepto";
                }
                break;
        }
    }

    //tratamiento de archivos
    public static List<HttpPostedFile> CargaSoloImagenes(List<HttpPostedFile> iLista)
    {

        List<HttpPostedFile> listaValida = new List<HttpPostedFile>();

        foreach (HttpPostedFile posted in iLista)
        {
            if (tipoImagen(posted.ContentType.ToString()))
                listaValida.Add(posted);
        }
        if (listaValida.Count == 0)
            return null;
        else
            return listaValida;
    }

    private static Boolean tipoImagen(string contentType)
    {
        bool valido = false;
        switch (contentType)
        {
            case "image/jpeg":
                valido = true;
                break;
            case "application/pdf":
                valido = true;
                break;
            case "image/g3fax":
                valido = true;
                break;
            case "image/gif":
                valido = true;
                break;
            case "image/ief":
                valido = true;
                break;
            case "image/tiff":
                valido = true;
                break;
            case "image/png":
                valido = true;
                break;
            case "image/bmp":
                valido = true;
                break;

        }
        return valido;
    }

    public static int calculoTamanioArchivos(List<HttpPostedFile> ilArchivos)
    {
        int ttotal = 0;
        foreach (var archivo in ilArchivos)
        {
            ttotal = archivo.ContentLength + ttotal;

        }
        return ttotal;
    }



    public static List<HttpPostedFile> removeDuplicates(List<HttpPostedFile> inputList)
    {
        Dictionary<string, HttpPostedFile> uniquestore = new Dictionary<string, HttpPostedFile>();
        List<HttpPostedFile> finallist = new List<HttpPostedFile>();
        foreach (HttpPostedFile currvalue in inputList)
        {
            if (!uniquestore.ContainsKey(currvalue.FileName))
            {
                uniquestore.Add(currvalue.FileName, currvalue);
                finallist.Add(currvalue);
            }
        }
        return finallist;
    }

}

public class ConstructorDeUrl
{
    #region Variables miembro
    private string pagina;
    private string url;
    private NameValueCollection parametros;
    #endregion

    #region Propiedades
    public string Url
    {
        get
        {
            ArmarUrl();
            return url;
        }
    }

    private void ArmarUrl()
    {
        url = pagina;

        if (parametros.Count > 0)
            url += "?";

        foreach (string clave in parametros.AllKeys)
            url += clave + "=" + parametros[clave] + "&";
    }

    public string Pagina
    {
        get
        {
            return pagina;
        }
        set
        {
            pagina = value;
        }
    }

    public NameValueCollection Parametros
    {
        get
        {
            return parametros;
        }
        set
        {
            parametros = value;
        }
    }
    #endregion

    public ConstructorDeUrl()
    {
        pagina = string.Empty;
        parametros = new NameValueCollection();
    }
    public ConstructorDeUrl(string pagina)
    {
        this.pagina = pagina;
        parametros = new NameValueCollection();
    }
    public void AgregarParametro(string nombre, string valor)
    {
        parametros.Add(nombre, valor);
    }
    /*        public void AgregarParametrosModificar(int id)
    {
        parametros.Add(Constantes.Modo, Constantes.Modificar);
        parametros.Add(Constantes.Id, id.ToString());
    }
*/

    
}
