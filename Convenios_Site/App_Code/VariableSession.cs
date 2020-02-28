using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ar.Gov.Anses.Microinformatica;
using System.Configuration;
using Anses.Director.Session;
using System.Data;

/// <summary>
/// Summary description for VariableSession
/// </summary>
public partial class VariableSession
{
    private static System.Web.SessionState.HttpSessionState Session
    {
        get
        {
            return (System.Web.SessionState.HttpSessionState)HttpContext.Current.Session;
        }
    }

    #region Parametria


    public static String oVsSistema
    {
        get
        {
            if (Session["_oVSis"] != null)
                return (String)Session["_oVSis"];
            else
            {
                Session["_oVSis"] = InvocaWsDao.VersionSistema();
                return (String)Session["_oVSis"];
            }
        }
        set
        {
            Session["_oVSis"] = value;
        }
    }


    public static String oVsSitio
    {
        get
        {
            if (Session[
                "_oVSit"] != null)
                return (String)Session["_oVSit"];
            else
            {
                Session["_oVSit"] = InvocaWsDao.VersionSitio();
                return (String)Session["_oVSit"];
            }
        }
        set
        {
            Session["_oVSit"] = value;
        }
    }


    public static List<AuxiliaresWS.TipoDocumentacion_Prestacion> oTipoDocumentacion_Prestacion
    {
        get
        {
            if (Session["_oTDP"] != null)
                return (List<AuxiliaresWS.TipoDocumentacion_Prestacion>)Session["_oTDP"];
            else
            {
                Session["_oTDP"] = InvocaWsDao.TraeTodoTipoDocumentacionPrestacion();
                return (List<AuxiliaresWS.TipoDocumentacion_Prestacion>)Session["_oTDP"];
            }
        }
        set
        {
            Session["_oTDP"] = value;
        }
    }

    public static List<AuxiliaresWS.MotivoDenegacion> oMotivoDenegacion
    {
        get
        {
            if (Session["_oTipoMD"] != null)
                return (List<AuxiliaresWS.MotivoDenegacion>)Session["_oTipoMD"];
            else
            {
                Session["_oTipoMD"] = InvocaWsDao.TraeMotivosDenegacion();
                return (List<AuxiliaresWS.MotivoDenegacion>)Session["_oTipoMD"];
            }
        }
        set
        {
            Session["_oTipoMD"] = value;
        }
    }

    public static List<AuxiliaresWS.TipoDocumento> oTiposDocumentoAll
    {
        get
        {
            if (Session["_oTipoDoc"] != null)
                return (List<AuxiliaresWS.TipoDocumento>)Session["_oTipoDoc"];
            else
            {
                Session["_oTipoDoc"] = InvocaWsDao.TraerTiposDocumentoAll();
                return (List<AuxiliaresWS.TipoDocumento>)Session["_oTipoDoc"];
            }
        }
        set
        {
            Session["_oTipoDoc"] = value;
        }
    }

    public static List<AuxiliaresWS.TipoDocumento> oTiposDocumentoFrecuentes
    {
        get
        {
            if (Session["_oTipoDocf"] != null)
                return (List<AuxiliaresWS.TipoDocumento>)Session["_oTipoDocf"];
            else
            {
                Session["_oTipoDocf"] = InvocaWsDao.TraerTiposDocumentoFrecuente();
                return (List<AuxiliaresWS.TipoDocumento>)Session["_oTipoDocf"];
            }
        }
        set
        {
            Session["_oTipoDocf"] = value;
        }
    }

    public static List<AuxiliaresWS.Tramitesderivados> oTramitesDerivados
    {
        get
        {
            if (Session["_otramDer"] != null)
                return (List<AuxiliaresWS.Tramitesderivados>)Session["_otramDer"];
            else
            {
                Session["_otramDer"] = InvocaWsDao.TraeTramitesderivados();
                return (List<AuxiliaresWS.Tramitesderivados>)Session["_otramDer"];
            }
        }
        set
        {
            Session["_oTipoDoc"] = value;
        }
    }

    public static List<AuxiliaresWS.Provincia> oProvincias
    {
        get
        {
            if (Session["_oProvincias"] != null)
                return (List<AuxiliaresWS.Provincia>)Session["_oProvincias"];
            else
            {
                Session["_oProvincias"] = InvocaWsDao.TraerProvincias();
                return (List<AuxiliaresWS.Provincia>)Session["_oProvincias"];
            }
        }
        set
        {
            Session["_oProvincias"] = value;
        }
    }


    //public static List<AuxiliaresWS.Prestacion> oPrestacionesCListadoTDoc
    //{
    //    get
    //    {
    //        if (Session["_oPrestacionesC"] != null)
    //            return (List<AuxiliaresWS.Prestacion>)Session["_oPrestacionesC"];
    //        else
    //        {
    //            Session["_oPrestacionesC"] = InvocaWsDao.TraerPrestacionesCListadoTDoc();
    //            return (List<AuxiliaresWS.Prestacion>)Session["_oPrestacionesC"];
    //        }
    //    }
    //    set
    //    {
    //        Session["_oPrestacionesC"] = value;
    //    }
    //}


    public static List<AuxiliaresWS.Prestacion> oPrestaciones
    {
        get
        {
            if (Session["_oPrestaciones"] != null)
                return (List<AuxiliaresWS.Prestacion>)Session["_oPrestaciones"];
            else
            {
                Session["_oPrestaciones"] = InvocaWsDao.TraerPrestaciones();
                return (List<AuxiliaresWS.Prestacion>)Session["_oPrestaciones"];
            }
        }
        set
        {
            Session["_oPrestaciones"] = value;
        }
    }


    //public static List<AuxiliaresWS.TipoPoder> oTiposPoder
    //{
    //    get
    //    {
    //        if (Session["_oTipoPoder"] != null)
    //            return (List<AuxiliaresWS.TipoPoder>)Session["_oTipoPoder"];
    //        else
    //        {
    //            Session["_oTipoPoder"] = InvocaWsDao.TraerTiposDePoder();
    //            return (List<AuxiliaresWS.TipoPoder>)Session["_oTipoPoder"];
    //        }
    //    }
    //    set
    //    {
    //        Session["_oTipoPoder"] = value;
    //    }
    //}

    public static List<AuxiliaresWS.TipoApoderado> oTiposApoderado
    {
        get
        {
            if (Session["_oTipoApoderado"] != null)
                return (List<AuxiliaresWS.TipoApoderado>)Session["_oTipoApoderado"];
            else
            {
                Session["_oTipoApoderado"] = InvocaWsDao.TraerTiposDeApoderado();
                return (List<AuxiliaresWS.TipoApoderado>)Session["_oTipoApoderado"];
            }
        }
        set
        {
            Session["_oTipoApoderado"] = value;
        }
    }

    public static List<AuxiliaresWS.SubTipoApoderado> oSubTiposApoderado
    {
        get
        {
            if (Session["_osTipoApoderado"] != null)
                return (List<AuxiliaresWS.SubTipoApoderado>)Session["_osTipoApoderado"];
            else
            {
                Session["_osTipoApoderado"] = InvocaWsDao.TraerSubTiposDeApoderado();
                return (List<AuxiliaresWS.SubTipoApoderado>)Session["_osTipoApoderado"];
            }
        }
        set
        {
            Session["_osTipoApoderado"] = value;
        }
    }


    public static List<PaisWS.Pais> oPaisTodos
    {
        get
        {
            if (Session["_oPais"] != null)
                return (List<PaisWS.Pais>)Session["_oPais"];
            else
            {
                Session["_oPais"] = InvocaWsDao.TraerPaisesTodos();
                return (List<PaisWS.Pais>)Session["_oPais"];
            }
        }
        set
        {
            Session["_oPais"] = value;
        }
    }

    public static List<PaisWS.Pais> oPaisConvenios
    {
        get
        {
            if (Session["_oPaisC"] != null)
                return (List<PaisWS.Pais>)Session["_oPaisC"];
            else
            {
                Session["_oPaisC"] = InvocaWsDao.TraerPaisesConConvenio();
                return (List<PaisWS.Pais>)Session["_oPaisC"];
            }
        }
        set
        {
            Session["_oPaisC"] = value;
        }
    }

    public static List<AuxiliaresWS.Pais2> oPaisDomExtranjero
    {
        get
        {
            if (Session["_oPaisDExtr"] != null)
                return (List<AuxiliaresWS.Pais2>)Session["_oPaisDExtr"];
            else
            {
                Session["_oPaisDExtr"] = InvocaWsDao.TraePaisesExtranjeros();
                return (List<AuxiliaresWS.Pais2>)Session["_oPaisDExtr"];
            }
        }
        set
        {
            Session["_oPaisDExtr"] = value;
        }
    }

    
    //public static List<PaisWS.Mercosur_Paises> oMercosurPaises
    //{
    //    get
    //    {
    //        if (Session["_oMPais"] != null)
    //            return (List<PaisWS.Mercosur_Paises>)Session["_oMPais"];
    //        else
    //        {
    //            Session["_oMPais"] = InvocaWsDao.TraerPaisesMiembroMercosur();
    //            return (List<PaisWS.Mercosur_Paises>)Session["_oMPais"];
    //        }
    //    }
    //    set
    //    {
    //        Session["_oMPais"] = value;
    //    }
    //}

    public static List<AuxiliaresWS.TipoDocumentacion> oTipoDocumentacion
    {
        get
        {
            if (Session["_oTdocum"] != null)
                return (List<AuxiliaresWS.TipoDocumentacion>)Session["_oTdocum"];
            else
            {
                Session["_oTdocum"] = InvocaWsDao.TraerTipoDocumentacion();
                return (List<AuxiliaresWS.TipoDocumentacion>)Session["_oTdocum"];
            }
        }
        set
        {
            Session["_oTdocum"] = value;
        }
    }

    public static List<BancoWS.Banco> oBancoFrecuentes
    {
        get
        {
            if (Session["_oBanco"] != null)
                return (List<BancoWS.Banco>)Session["_oBanco"];
            else
            {
                Session["_oBanco"] = InvocaWsDao.TraerBancosFrecuentes();
                return (List<BancoWS.Banco>)Session["_oBanco"];
            }
        }
        set
        {
            Session["_oBanco"] = value;
        }
    }

    public static List<BancoWS.Banco> oBancoTodos
    {
        get
        {
            if (Session["_oBancoT"] != null)
                return (List<BancoWS.Banco>)Session["_oBancoT"];
            else
            {
                Session["_oBancoT"] = InvocaWsDao.TraerBancosTodos();
                return (List<BancoWS.Banco>)Session["_oBancoT"];
            }
        }
        set
        {
            Session["_oBancoT"] = value;
        }
    }

    public static List<AuxiliaresWS.TipoIngreso> oTipoIngreso
    {
        get
        {
            if (Session["_otipoIng"] != null)
                return (List<AuxiliaresWS.TipoIngreso>)Session["_otipoIng"];
            else
            {
                Session["_otipoIng"] = InvocaWsDao.Traer_TipoIngreso();
                return (List<AuxiliaresWS.TipoIngreso>)Session["_otipoIng"];
            }
        }
        set
        {
            Session["_otipoIng"] = value;
        }
    }

    public static List<AuxiliaresWS.Estado> oEstados
    {
        get
        {
            if (Session["_oestado"] != null)
                return (List<AuxiliaresWS.Estado>)Session["_oestado"];
            else
            {
                Session["_oestado"] = InvocaWsDao.TraerEstados();
                return (List<AuxiliaresWS.Estado>)Session["_oestado"];
            }
        }
        set
        {
            Session["_oestado"] = value;
        }
    }

    public static List<AuxiliaresWS.Sector> oSectores
    {
        get
        {
            if (Session["_osectores"] != null)
                return (List<AuxiliaresWS.Sector>)Session["_osectores"];
            else
            {
                Session["_osectores"] = InvocaWsDao.TraerSectores();
                return (List<AuxiliaresWS.Sector>)Session["_osectores"];
            }
        }
        set
        {
            Session["_osectores"] = value;
        }
    }

    #endregion

}

    