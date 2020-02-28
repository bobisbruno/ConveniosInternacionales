using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Generic;
using log4net;
using System.Data.SqlClient;

//using ObtenerRelacionPersonaWS;

/// <summary>
/// Contiene instancia de los web metodos para traer datos de la DB.
/// </summary>
public class InvocaWsDao
{
    
    

    public InvocaWsDao()
    {
    }

    private static System.Web.SessionState.HttpSessionState Session
    {
        get
        {
            return (System.Web.SessionState.HttpSessionState)HttpContext.Current.Session;
        }
    }
    
    #region URLs
    public static string URLAuxiliares
    {
        get
        {
            AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
            string wsUrl = oServicio.Url;
            oServicio.Dispose();
            return wsUrl;
        }
    }

    

    public static string URLConsultas
    {
        get
        {
            ConsultasWS.ConsultasWS oServicio = new ConsultasWS.ConsultasWS();
            string wsUrl = oServicio.Url;
            oServicio.Dispose();
            return wsUrl;
        }
    }

    public static string URLActores
    {
        get
        {
            ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
            string wsUrl = oServicio.Url;
            oServicio.Dispose();
            return wsUrl;
        }
    }

    public static string URLPaises
    {
        get
        {
            PaisWS.PaisWS oServicio = new PaisWS.PaisWS();
            string wsUrl = oServicio.Url;
            oServicio.Dispose();
            return wsUrl;
        }
    }

    public static string URLBancos
    {
        get
        {
            BancoWS.BancoWS oServicio = new BancoWS.BancoWS();
            string wsUrl = oServicio.Url;
            oServicio.Dispose();
            return wsUrl;
        }
    }

    public static string URLSolicitudes
    {
        get
        {
            ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
            string wsUrl = oServicio.Url;
            oServicio.Dispose();
            return wsUrl;
        }
    }

    public static string URLEnvios
    {
        get
        {
            ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
            string wsUrl = oServicio.Url;
            oServicio.Dispose();
            return wsUrl;
        }
    }


    
    #endregion

    #region trae Versiones para mostrar en cabecera
    public static string VersionSistema()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        try
        {
            AuxiliaresWS.AuxiliaresWS servicio = new  AuxiliaresWS.AuxiliaresWS();

            return servicio.VersionSistema();
        }
        catch (Exception exc)
        {
            log.Error(String.Concat("en VersionSistema se produjo el siguiente error => ", exc.ToString()));
            throw exc;
        }
    }

    public static string VersionSitio()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        try
        {
            return ConfigurationManager.AppSettings["VersionSitio"];
        }
        catch (Exception exc)
        {
            log.Error(String.Concat("en VersionSitio se produjo el siguiente error => ", exc.ToString()));
            throw exc;
        }
    }
    #endregion trae Versiones

    #region Trae para Session de Parametros

    #region Trae Bancos

    public static List<BancoWS.Banco> TraerBancosFrecuentes()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        BancoWS.BancoWS oServicio = new BancoWS.BancoWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<BancoWS.Banco> oList = null;


        try
        {

            BancoWS.Banco[] lista = oServicio.TraerBancos(true);
            oList = (List<BancoWS.Banco>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(BancoWS.Banco[]),
                               typeof(List<BancoWS.Banco>),
                               URLBancos);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerBancosFrecuentes: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    public static List<BancoWS.Banco> TraerBancosTodos()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        BancoWS.BancoWS oServicio = new BancoWS.BancoWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<BancoWS.Banco> oList = null;


        try
        {
            BancoWS.Banco[] lista = oServicio.TraerBancos(false);
            oList = (List<BancoWS.Banco>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(BancoWS.Banco[]),
                               typeof(List<BancoWS.Banco>),
                               URLBancos);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerBancosTodos: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion Traer bancos

    #region Trae TipoDe Documento

    public static List<AuxiliaresWS.TipoDocumento> TraerTiposDocumentoAll()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.TipoDocumento> oList = null;


        try
        {
            AuxiliaresWS.TipoDocumento[] lista = oServicio.Traer_TipoDocumento(false);
            oList = (List<AuxiliaresWS.TipoDocumento>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.TipoDocumento[]),
                               typeof(List<AuxiliaresWS.TipoDocumento>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerTiposDocumento: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    public static List<AuxiliaresWS.TipoDocumento> TraerTiposDocumentoFrecuente()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.TipoDocumento> oList = null;


        try
        {
            AuxiliaresWS.TipoDocumento[] lista = oServicio.Traer_TipoDocumento(true);
            oList = (List<AuxiliaresWS.TipoDocumento>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.TipoDocumento[]),
                               typeof(List<AuxiliaresWS.TipoDocumento>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerTiposDocumento: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    #endregion Trae TipoDe Documento

    #region Trae Prestaciones

    public static List<AuxiliaresWS.Prestacion> TraerPrestaciones()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.Prestacion> oList = null;


        try
        {
            AuxiliaresWS.Prestacion[] lista = oServicio.TraerPrestaciones();
            oList = (List<AuxiliaresWS.Prestacion>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.Prestacion[]),
                               typeof(List<AuxiliaresWS.Prestacion>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerPrestaciones: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    //public static List<AuxiliaresWS.Prestacion> TraerPrestacionesCListadoTDoc()
    //{
    //    ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
    //    AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
    //    oServicio.Credentials = CredentialCache.DefaultCredentials;
    //    List<AuxiliaresWS.Prestacion> oList = null;


    //    try
    //    {
    //        AuxiliaresWS.Prestacion[] lista = oServicio.TraePrestacionesC();
    //        oList = (List<AuxiliaresWS.Prestacion>)
    //                           reSerializer.reSerialize(
    //                           lista,
    //                           typeof(AuxiliaresWS.Prestacion[]),
    //                           typeof(List<AuxiliaresWS.Prestacion>),
    //                           URLAuxiliares);
    //    }
    //    catch (Exception ex)
    //    {
    //        //log.ErrorFormat("TraerPrestacionesCListadoTDoc: {0}", ex.Message);
    //    }
    //    finally
    //    {
    //        oServicio.Dispose();
    //    }
    //    return oList;
    //}


    #endregion Trae Prestaciones

    #region Trae Motivos denegatoria

    public static List<AuxiliaresWS.MotivoDenegacion> TraeMotivosDenegacion()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.MotivoDenegacion> oList = null;


        try
        {
            AuxiliaresWS.MotivoDenegacion[] lista = oServicio.TraeMotivosDenegacion();
            oList = (List<AuxiliaresWS.MotivoDenegacion>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.MotivoDenegacion[]),
                               typeof(List<AuxiliaresWS.MotivoDenegacion>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeMotivosDenegacion: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion

    #region Trae tramites derivados

    public static List<AuxiliaresWS.Tramitesderivados> TraeTramitesderivados()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.Tramitesderivados> oList = null;


        try
        {
            AuxiliaresWS.Tramitesderivados[] lista = oServicio.TraeTramitesDerivados();
            oList = (List<AuxiliaresWS.Tramitesderivados>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.Tramitesderivados[]),
                               typeof(List<AuxiliaresWS.Tramitesderivados>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeTramitesderivados: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion

    #region Trae Provincias y Localidades

    public static List<AuxiliaresWS.Provincia> TraerProvincias()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.Provincia> oList = null;


        try
        {
            AuxiliaresWS.Provincia[] lista = oServicio.TraeProvincias();
            oList = (List<AuxiliaresWS.Provincia>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.Provincia[]),
                               typeof(List<AuxiliaresWS.Provincia>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerProvincias: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    public static List<AuxiliaresWS.Localidad> TraerLocalidadesXProvincia(Int16 codProvincia)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.Localidad> oList = null;


        try
        {
            AuxiliaresWS.Localidad[] lista = oServicio.TraeLocalidadesXProvincia(codProvincia);
            oList = (List<AuxiliaresWS.Localidad>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.Localidad[]),
                               typeof(List<AuxiliaresWS.Localidad>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerLocalidadesXProvincia: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    #endregion

    #region Trae TipoDe Poder

    //public static List<AuxiliaresWS.TipoPoder> TraerTiposDePoder()
    //{
    //    ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
    //    AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
    //    oServicio.Credentials = CredentialCache.DefaultCredentials;
    //    List<AuxiliaresWS.TipoPoder> oList = null;


    //    try
    //    {
    //        AuxiliaresWS.TipoPoder[] lista = oServicio.Traer_TipoPoder();
    //        oList = (List<AuxiliaresWS.TipoPoder>)
    //                           reSerializer.reSerialize(
    //                           lista,
    //                           typeof(AuxiliaresWS.TipoPoder[]),
    //                           typeof(List<AuxiliaresWS.TipoPoder>),
    //                           URLAuxiliares);
    //    }
    //    catch (Exception ex)
    //    {
    //        //log.ErrorFormat("TraerTiposDePoder: {0}", ex.Message);
    //    }
    //    finally
    //    {
    //        oServicio.Dispose();
    //    }
    //    return oList;
    //}

    #endregion Trae TipoDePoder

    #region Trae TipoDe Apoderado

    public static List<AuxiliaresWS.TipoApoderado> TraerTiposDeApoderado()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.TipoApoderado> oList = null;


        try
        {
            AuxiliaresWS.TipoApoderado[] lista = oServicio.TraeTipoApoderado();
            oList = (List<AuxiliaresWS.TipoApoderado>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.TipoApoderado[]),
                               typeof(List<AuxiliaresWS.TipoApoderado>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerTiposDeApoderado: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    public static List<AuxiliaresWS.SubTipoApoderado> TraerSubTiposDeApoderado()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.SubTipoApoderado> oList = null;


        try
        {
            AuxiliaresWS.SubTipoApoderado[] lista = oServicio.TraeSubTipoApoderado();
            oList = (List<AuxiliaresWS.SubTipoApoderado>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.SubTipoApoderado[]),
                               typeof(List<AuxiliaresWS.SubTipoApoderado>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerSubTiposDeApoderado: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion Trae 

    #region Trae TipoDe Documentacion

    public static List<AuxiliaresWS.TipoDocumentacion> TraerTipoDocumentacion()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.TipoDocumentacion> oList = null;


        try
        {
            AuxiliaresWS.TipoDocumentacion[] lista = oServicio.TraeTipoDocumentacion();
            oList = (List<AuxiliaresWS.TipoDocumentacion>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.TipoDocumentacion[]),
                               typeof(List<AuxiliaresWS.TipoDocumentacion>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerTipoDocumentacion: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion Trae

    #region Trae Estados

    public static List<AuxiliaresWS.Estado> TraerEstados()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.Estado> oList = null;


        try
        {
            AuxiliaresWS.Estado[] lista = oServicio.TraeEstados();
            oList = (List<AuxiliaresWS.Estado>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.Estado[]),
                               typeof(List<AuxiliaresWS.Estado>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerEstados: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion Trae

    #region Trae sectores

    public static List<AuxiliaresWS.Sector> TraerSectores()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.Sector> oList = null;


        try
        {
            AuxiliaresWS.Sector[] lista = oServicio.TraeSectores();
            oList = (List<AuxiliaresWS.Sector>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.Sector[]),
                               typeof(List<AuxiliaresWS.Sector>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerSectores: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion Trae

    #region Trae TipoDe Documentacion

    public static List<ActoresWS.TipoDocumentacion> TraeTipoDocumentacionXPrestacion(Int16 codPrestacion)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.TipoDocumentacion> oList = null;


        try
        {
            ActoresWS.TipoDocumentacion[] lista = oServicio.TraeTipoDocumentacionXPrestacion(codPrestacion);
            oList = (List<ActoresWS.TipoDocumentacion>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.TipoDocumentacion[]),
                               typeof(List<ActoresWS.TipoDocumentacion>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerTipoDocumentacionXPrestacion: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    public static List<AuxiliaresWS.TipoDocumentacion_Prestacion> TraeTodoTipoDocumentacionPrestacion()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.TipoDocumentacion_Prestacion> oList = null;


        try
        {
            AuxiliaresWS.TipoDocumentacion_Prestacion[] lista = oServicio.TraeTodoTipoDocumentacionPrestacion();
            oList = (List<AuxiliaresWS.TipoDocumentacion_Prestacion>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.TipoDocumentacion_Prestacion[]),
                               typeof(List<AuxiliaresWS.TipoDocumentacion_Prestacion>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeTodoTipoDocumentacionPrestacion: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    //public static List<ActoresWS.TipoDocumentacion> TraeTipoDocumentacionXPrestacion(Int16 codPrestacion)
    //{
    //    ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
    //    ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
    //    oServicio.Credentials = CredentialCache.DefaultCredentials;
    //    List<ActoresWS.TipoDocumentacion> oList = null;


    //    try
    //    {
    //        ActoresWS.TipoDocumentacion[] lista = oServicio.TraeTipoDocumentacionXPrestacion(codPrestacion);
    //        oList = (List<ActoresWS.TipoDocumentacion>)
    //                           reSerializer.reSerialize(
    //                           lista,
    //                           typeof(ActoresWS.TipoDocumentacion[]),
    //                           typeof(List<ActoresWS.TipoDocumentacion>),
    //                           URLSolicitudes);
    //    }
    //    catch (Exception ex)
    //    {
    //        //log.ErrorFormat("TraerTipoDocumentacionXPrestacionCompleta: {0}", ex.Message);
    //    }
    //    finally
    //    {
    //        oServicio.Dispose();
    //    }
    //    return oList;
    //}
    #endregion Trae

    #region Trae Paises

    public static List<PaisWS.Pais> TraerPaisesConConvenio()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        PaisWS.PaisWS oServicio = new PaisWS.PaisWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<PaisWS.Pais> oList = null;


        try
        {
            PaisWS.Pais[] lista = oServicio.TraePaises(true);
            oList = (List<PaisWS.Pais>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(PaisWS.Pais[]),
                               typeof(List<PaisWS.Pais>),
                               URLPaises);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerPaises: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    public static List<PaisWS.Pais> TraerPaisesTodos()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        PaisWS.PaisWS oServicio = new PaisWS.PaisWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<PaisWS.Pais> oList = null;


        try
        {
            PaisWS.Pais[] lista = oServicio.TraePaises(false);
            oList = (List<PaisWS.Pais>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(PaisWS.Pais[]),
                               typeof(List<PaisWS.Pais>),
                               URLPaises);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerPaises: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    //public static List<PaisWS.Mercosur_Paises> TraerPaisesMiembroMercosur()
    //{
    //    ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
    //    PaisWS.PaisWS oServicio = new PaisWS.PaisWS();
    //    oServicio.Credentials = CredentialCache.DefaultCredentials;
    //    List<PaisWS.Mercosur_Paises> oList = null;


    //    try
    //    {
    //        PaisWS.Mercosur_Paises[] lista = oServicio.TraerPaisesMiembroMercosur();
    //        oList = (List<PaisWS.Mercosur_Paises>)
    //                           reSerializer.reSerialize(
    //                           lista,
    //                           typeof(PaisWS.Mercosur_Paises[]),
    //                           typeof(List<PaisWS.Mercosur_Paises>),
    //                           URLPaises);
    //    }
    //    catch (Exception ex)
    //    {
    //        //log.ErrorFormat("TraerPaisesMiembroMercosur: {0}", ex.Message);
    //    }
    //    finally
    //    {
    //        oServicio.Dispose();
    //    }
    //    return oList;
    //}

    public static List<AuxiliaresWS.Pais2> TraePaisesExtranjeros()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.Pais2> oList = null;


        try
        {
            AuxiliaresWS.Pais2[] lista = oServicio.TraePaises2();
            oList = (List<AuxiliaresWS.Pais2>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.Pais2[]),
                               typeof(List<AuxiliaresWS.Pais2>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraePaisesExtranjeros: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    public static List<AuxiliaresWS.Ciudad> TraeCiudadesExtXPais(string codPais3c)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.Ciudad> oList = null;


        try
        {
            AuxiliaresWS.Ciudad[] lista = oServicio.TraeCiudadesExtXPais(codPais3c);
            oList = (List<AuxiliaresWS.Ciudad>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.Ciudad[]),
                               typeof(List<AuxiliaresWS.Ciudad>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            log.ErrorFormat("{0}-{1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion Trae Paises

    #region Trae TipoIngreso

    public static List<AuxiliaresWS.TipoIngreso> Traer_TipoIngreso()
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.TipoIngreso> oList = null;


        try
        {
            AuxiliaresWS.TipoIngreso[] lista = oServicio.Traer_TipoIngreso();
            oList = (List<AuxiliaresWS.TipoIngreso>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.TipoIngreso[]),
                               typeof(List<AuxiliaresWS.TipoIngreso>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("Traer_TipoIngreso: {0}", ex.Message);
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    #endregion 

    #endregion Trae para Session de Parametros

    
    #region Transacciones

    #region Modifica Aceptacion convenio paises

    public static void ModificaPais(Int32 PaisPK, Boolean ConConvenio, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        PaisWS.PaisWS oServicio = new PaisWS.PaisWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.ModificaPais(PaisPK, ConConvenio);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("ModificaPais: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion Modifica Aceptacion convenio paises

    #region Modifica Rel Prestacion - Tipo documentacion

    public static void AMTipodeDocumentacion_Prestacion(Int32 CodTipoDocumentacion, Int16 CodPrestacion, String Comentario, Boolean RequeridoIniTram, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.AMTipodeDocumentacion_Prestacion( CodTipoDocumentacion, CodPrestacion, Comentario, RequeridoIniTram);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AMTipodeDocumentacion_Prestacion: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }


    public static void BajaTipodeDocumentacion_Prestacion(Int32 CodTipoDocumentacion, Int16 CodPrestacion, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.BajaTipodeDocumentacion_Prestacion(CodTipoDocumentacion, CodPrestacion);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("BajaTipodeDocumentacion_Prestacion: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }
    #endregion

    #region Alta Modificacion de Documentacion

    public static void AMTiposdeDocumentacion(Int32? codTipoDoc, string Descripcion, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new  AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.AMTiposdeDocumentacion(codTipoDoc, Descripcion);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AMTiposdeDocumentacion: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion 

    #region Alta Modificacion de Bancos

    public static void AMBanco(Int16? idBanco, Boolean Frecuente, String Descripcion, String WebSite, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        BancoWS.BancoWS oServicio = new BancoWS.BancoWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.AMBanco( idBanco,  Frecuente, Descripcion, WebSite);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AMBanlco: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion 

    #region Alta Nota

    public static void AMBeneficiario_Notas(ActoresWS.BeneficiarioNotas iParam , out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.AMBeneficiario_Notas(iParam);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AMBeneficiarioNotas: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion 

    #region AM solicitudes denegadas

    //public static void AMSolicitudesDenegadas(Int64 idBeneficiario, Int16 codPrestacion, Int16? codMotivo, String observaciones, Boolean habilitaSolicitud, out string mensaje)
    //{
    //    //
    //    ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
    //    ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
    //    oServicio.Credentials = CredentialCache.DefaultCredentials;
    //    mensaje = string.Empty;

    //    try
    //    {
    //        oServicio.AMSolicitudesDenegadas(idBeneficiario, codPrestacion, codMotivo, observaciones, habilitaSolicitud);
    //    }
    //    catch (Exception ex)
    //    {
    //        //log.ErrorFormat("AMSolicitudesDenegadas: {0}", ex.Message);
    //        mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
    //    }
    //    finally
    //    {
    //        oServicio.Dispose();
    //    }
    //}

    #endregion 

    #region Alta y Modificacion de Benefiario

    public static Int64 AMBeneficiario(ActoresWS.Beneficiario iBeneficiario, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;
        Int64? idBeneficiario = null;
        
        try
        {
            idBeneficiario = oServicio.AMBeneficiario(iBeneficiario);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AMBeneficiario: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return idBeneficiario.Value;
    }
    
    #endregion 

    #region Alta y Modificacion de Causante

    public static void  AMCausante(ActoresWS.Causante iCausante, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.AMCausante(iCausante);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AMCausante: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion 

    #region Alta y Modificacion de Apoderado

    //public static Int64 AMApoderado(ActoresWS.Apoderado iApoderado, Int64 idBeneficiario, out string mensaje)
    public static void AMApoderado(ActoresWS.Apoderado iApoderado, Int64 idBeneficiario, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;
        //Int64? idApod = null;

        try
        {
            //idApod = oServicio.AMApoderado(iApoderado, idBeneficiario);
            oServicio.AMApoderado(iApoderado, idBeneficiario);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AMApoderado: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        //return idApod.Value;

    }

    #endregion 

    #region Baja de Apoderado

    //public static Int64 AMApoderado(ActoresWS.Apoderado iApoderado, Int64 idBeneficiario, out string mensaje)
    public static void BajaApoderado(ActoresWS.Apoderado iApoderado, Int64 idBeneficiario, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;
        //Int64? idApod = null;

        try
        {
            //idApod = oServicio.AMApoderado(iApoderado, idBeneficiario);
            oServicio.BajaBeneficiario_Apoderado(iApoderado, idBeneficiario);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AMApoderado: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        //return idApod.Value;

    }

    #endregion 

    #region Baja de solicitud
    public static void BajaSolicitud(Int64 idBenef, Int16 codPrestacion, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.BajaSolicitud(idBenef, codPrestacion);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AMAllDatosSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }

    }


    #endregion Baja de solicitud


    #region Alta y Modificacion de Solicitud

    public static void AMAllDatosSolicitud(Int64 idBenef, Int16 codPrestacion, Int16 codPais, List<ActoresWS.Solicitud> ilSolicitud, List<ActoresWS.Expediente_Solicitud> ilExpediente, List<ActoresWS.Beneficio_Solicitud> ilBeneficios, List<ActoresWS.Ingresos> ilIngresos, List<ActoresWS.Devolucion> ilDevolucion, List<ActoresWS.Movimiento_Solicitud> ilMovimientos, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;
        
        try
        {
            oServicio.AMAllDatosSolicitud(idBenef, codPrestacion, codPais
                , (ActoresWS.Solicitud[])reSerializer.reSerialize(ilSolicitud, typeof(List<ActoresWS.Solicitud>), typeof(ActoresWS.Solicitud[]), URLSolicitudes)
                , (ActoresWS.Expediente_Solicitud[])reSerializer.reSerialize(ilExpediente, typeof(List<ActoresWS.Expediente_Solicitud>), typeof(ActoresWS.Expediente_Solicitud[]), URLSolicitudes)
                , (ActoresWS.Beneficio_Solicitud[])reSerializer.reSerialize(ilBeneficios, typeof(List<ActoresWS.Beneficio_Solicitud>), typeof(ActoresWS.Beneficio_Solicitud[]), URLSolicitudes)
                , (ActoresWS.Ingresos[])reSerializer.reSerialize(ilIngresos, typeof(List<ActoresWS.Ingresos>), typeof(ActoresWS.Ingresos[]), URLSolicitudes)
                , (ActoresWS.Devolucion[])reSerializer.reSerialize(ilDevolucion, typeof(List<ActoresWS.Devolucion>), typeof(ActoresWS.Devolucion[]), URLSolicitudes)
                , (ActoresWS.Movimiento_Solicitud[])reSerializer.reSerialize(ilMovimientos, typeof(List<ActoresWS.Movimiento_Solicitud>), typeof(ActoresWS.Movimiento_Solicitud[]), URLSolicitudes));
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AMAllDatosSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        
    }

    //public static void AMAllMovimientos(List<ActoresWS.Ingresos> ilIngreso, List<ActoresWS.Devolucion> ilDevolucion, List<ActoresWS.Movimiento_Solicitud> ilMovimientos, out string mensaje)
    //{
    //    //
    //    ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
    //    ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
    //    oServicio.Credentials = CredentialCache.DefaultCredentials;
    //    mensaje = string.Empty;

    //    try
    //    {
    //        oServicio.AMAllDatos(ilSolicitud, ilExpediente, ilBeneficios);
    //    }
    //    catch (Exception ex)
    //    {
    //        //log.ErrorFormat("AMSolicitud: {0}", ex.Message);
    //        mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
    //    }
    //    finally
    //    {
    //        oServicio.Dispose();
    //    }

    //}

    #endregion 

    #region Alta de Ingreso Documentacion

    //public static void AltaIngreso(Int64 idBeneficiario, Int16 codPrestacion, String fIngreso, Byte? idTipoIngreso, List<ActoresWS.TipoDocumentacion> iListTipoDoc, out List<String> docRepetida, out string mensaje)
    public static void AltaIngreso(Int64 idBeneficiario, Int16 codPrestacion, String fIngreso, Byte? idTipoIngreso, List<ActoresWS.TipoDocumentacion> iListTipoDoc, String observacionIng, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;
        //String[] docRepetidaArr = null;
        //docRepetida = null;

        try
        {
            
            ActoresWS.TipoDocumentacion[] iListTDOC = (ActoresWS.TipoDocumentacion[])reSerializer.reSerialize(iListTipoDoc, typeof(List<ActoresWS.TipoDocumentacion>), typeof(ActoresWS.TipoDocumentacion[]), URLSolicitudes);
            //docRepetidaArr = oServicio.AltaIngreso(idBeneficiario, codPrestacion, fIngreso ,idTipoIngreso, iListTDOC);
            oServicio.AltaIngreso(idBeneficiario, codPrestacion, fIngreso, idTipoIngreso, iListTDOC, observacionIng);
            //docRepetida = (List<String>)reSerializer.reSerialize(docRepetidaArr, typeof(List<String>));
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AltaIngreso: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion 

    #region Alta de Devolucion

    public static void AltaDevolucion(Int64 idBeneficiario, Int16 codPrestacion, String destino, String observaciones, String certificado, List<ActoresWS.TipoDocumentacion> iListTipo, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.AltaDevolucion(idBeneficiario, codPrestacion, destino, observaciones, certificado, (ActoresWS.TipoDocumentacion[])reSerializer.reSerialize(iListTipo, typeof(List<ActoresWS.TipoDocumentacion>), typeof(ActoresWS.TipoDocumentacion[]), URLSolicitudes));
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AltaDevolucion: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion 

    #region AltaMovimiento

    public static void AltaMovimiento(Int64 idBeneficiario, Int16 codPrestacion, Int32 codEstado, Int32 codSector, String observaciones, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.AltaMovimiento(idBeneficiario, codPrestacion, codEstado, codSector, observaciones);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("AltaMovimiento: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion 

    #region NotificaDevolucion

    public static void NotificaDevolucion(Int64 idBeneficiario, Int16 codPrestacion, String fechaMovimiento, String fechaNotificacion , out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.NotificaDevolucion(idBeneficiario, codPrestacion, fechaMovimiento, fechaNotificacion);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("NotificaDevolucion: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion 
  
    #region ModificaDevolucion_SetFPresentacion

    public static void ModificaDevolucion_SetFPresentacion(Int64 idBeneficiario, Int16 codPrestacion, String fechaMovimiento, String fechaPresentacion, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.ModificaDevolucion_SetFPresentacion(idBeneficiario, codPrestacion, fechaMovimiento, fechaPresentacion);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("ModificaDevolucion_SetFPresentacion: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion 

    #endregion Transacciones

    #region Consultas

    #region Trae Beneficiarios


    public static List<ActoresWS.LsBeneficiario> TraerBeneficiarios(ActoresWS.TipoConsultaBeneficioario iTipoConsulta, string parametro, string codDoc, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.LsBeneficiario> oList = null;
        mensaje = string.Empty;

        try
        {
            ActoresWS.LsBeneficiario[] lista = oServicio.TraeBeneficiarios(iTipoConsulta, parametro, codDoc);
            oList = (List<ActoresWS.LsBeneficiario>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.LsBeneficiario[]),
                               typeof(List<ActoresWS.LsBeneficiario>),
                               URLActores);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraerBeneficiarios: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    public static List<ActoresWS.LsBeneficiario> TraeBeneficiariosXExpteANSES(string expediente_org
            , string expediente_precu
            , string expediente_doccu
            , string expediente_digcu
            , string expediente_ctipo
            , string expediente_sec, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.LsBeneficiario> oList = null;
        mensaje = string.Empty;


        try
        {
            ActoresWS.LsBeneficiario[] lista = oServicio.TraeBeneficiariosXExpteANSES(expediente_org
            ,  expediente_precu
            ,  expediente_doccu
            ,  expediente_digcu
            ,  expediente_ctipo
            ,  expediente_sec);
            oList = (List<ActoresWS.LsBeneficiario>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.LsBeneficiario[]),
                               typeof(List<ActoresWS.LsBeneficiario>),
                               URLActores);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeBeneficiariosXExpteANSES: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    public static List<ActoresWS.LsBeneficiario> TraeBeneficiariosXNroBeneficioANSES(string BenExCaja
            , string BenTipo
            , string BenNumero
            , string BenCopart
            , string BenDigVerif
            , out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.LsBeneficiario> oList = null;
        mensaje = string.Empty;


        try
        {
            ActoresWS.LsBeneficiario[] lista = oServicio.TraeBeneficiariosXNroBeneficioANSES(BenExCaja
            , BenTipo
            , BenNumero
            , BenCopart
            , BenDigVerif
            );
            oList = (List<ActoresWS.LsBeneficiario>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.LsBeneficiario[]),
                               typeof(List<ActoresWS.LsBeneficiario>),
                               URLActores);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeBeneficiariosXNroBeneficioANSES: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    public static List<ActoresWS.LsBeneficiario> TraeBeneficiariosXNroSolicitudProvisoria(string nroSolicitudProvisoria , out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.LsBeneficiario> oList = null;
        mensaje = string.Empty;


        try
        {
            ActoresWS.LsBeneficiario[] lista = oServicio.TraeBeneficiariosXNroSolicitudProvisoria(nroSolicitudProvisoria);
            oList = (List<ActoresWS.LsBeneficiario>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.LsBeneficiario[]),
                               typeof(List<ActoresWS.LsBeneficiario>),
                               URLActores);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeBeneficiariosXNroBeneficioANSES: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    public static List<ActoresWS.LsBeneficiario> TraeBeneficiariosXCUIP(string preCUIP, string docCUIP, string digCUIP
            , out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.LsBeneficiario> oList = null;
        mensaje = string.Empty;


        try
        {
            ActoresWS.LsBeneficiario[] lista = oServicio.TraeBeneficiariosXCUIP(preCUIP
            , docCUIP
            , digCUIP
            );
            oList = (List<ActoresWS.LsBeneficiario>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.LsBeneficiario[]),
                               typeof(List<ActoresWS.LsBeneficiario>),
                               URLActores);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeBeneficiariosXNroBeneficioANSES: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    public static List<ActoresWS.SolicitudProvisoria> TraeSolicitudesProvisorias(String anio, String mes, Int16? codPais, Int16? codPrestacion, Boolean soloProvisorias, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.SolicitudProvisoria> oList = null;
        mensaje = string.Empty;


        try
        {
            ActoresWS.SolicitudProvisoria[] lista = oServicio.TraeSolicitudesProvisorias(anio, mes, codPais, codPrestacion, soloProvisorias
            , Int32.Parse( ConfigurationManager.AppSettings["LapsoVencimientoDiasProvisorio"]));
            oList = (List<ActoresWS.SolicitudProvisoria>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.SolicitudProvisoria[]),
                               typeof(List<ActoresWS.SolicitudProvisoria>),
                               URLActores);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeBeneficiariosXNroBeneficioANSES: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle"; throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    public static List<ActoresWS.SolicitudProvisoriaExtendida> TraeSolicitudesProvisoriasAVencerEnPlazo(out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.SolicitudProvisoriaExtendida> oList = null;
        mensaje = string.Empty;


        try
        {
            ActoresWS.SolicitudProvisoriaExtendida[] lista = oServicio.TraeSolicitudesProvisoriasAVencerEnPlazo(Int32.Parse(ConfigurationManager.AppSettings["LapsoVencimientoDiasProvisorio"]), Int32.Parse(ConfigurationManager.AppSettings["LapsoParaInformeDiasProvisorio"]));
            oList = (List<ActoresWS.SolicitudProvisoriaExtendida>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.SolicitudProvisoriaExtendida[]),
                               typeof(List<ActoresWS.SolicitudProvisoriaExtendida>),
                               URLActores);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeBeneficiariosXNroBeneficioANSES: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle"; throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    public static ActoresWS.SolicitudProvisoria TraeSolicitudProvisoriaXNroSolicitudProvisoria(string nro_SolicitudProvisoria
            , out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        
        mensaje = string.Empty;

        ActoresWS.SolicitudProvisoria oSolicitud = null;
        try 
        {
            oSolicitud = oServicio.TraeSolicitudProvisoriaXNroSolicitudProvisoria(nro_SolicitudProvisoria);
            
        }
        catch (Exception ex)
        {
            
            ////log.ErrorFormat("TraeBeneficiariosXNroBeneficioANSES: {0}-{1}",System.Reflection.MethodBase.GetCurrentMethod()., ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oSolicitud;
    }



    #endregion

    #region Consulta por documento duplicado
    public static Boolean ExisteDocumento(String numDoc, Int16 codDoc, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;
        Boolean existe = true;

        try
        {
            existe = oServicio.ExisteDocumento(numDoc, codDoc);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("ExisteDocumento: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return existe;
    }


    #endregion Consulta por documento duplicado

    #region Trae Datos de Beneficiario

    public static ActoresWS.Beneficiario TraerBeneficiario(Int64 idBeneficiario, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        ActoresWS.Beneficiario iBeneficiario = null;
        try
        {
            iBeneficiario = oServicio.TraeBeneficiarioXId(idBeneficiario);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraerBeneficiario: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return iBeneficiario;
    }

    public static ActoresWS.LsBeneficiario TraerBeneficiarioSimple(Int64 idBeneficiario, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        ActoresWS.LsBeneficiario iBeneficiario = null;
        try
        {
            iBeneficiario = oServicio.TraeBeneficiarioSimpleXId(idBeneficiario);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraerBeneficiario: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return iBeneficiario;
    }


    #endregion

    #region Trae prestaciones no implementadas por beneficiario

    public static List<ActoresWS.Prestacion> TraePrestacionesNoIngresadasXIdBeneficiario(Int64 idBeneficiario, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        List<ActoresWS.Prestacion> oList = null;
        try
        {
            ActoresWS.Prestacion[] lista = oServicio.TraePrestacionesNoIngresadasXIdBeneficiario(idBeneficiario);
            oList = (List<ActoresWS.Prestacion>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.Prestacion[]),
                               typeof(List<ActoresWS.Prestacion>),
                               URLSolicitudes);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraePrestacionesNoIngresadasXIdBeneficiario: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    #endregion


    #region Traer Prestaciones X Beneficiario
    public static List<ActoresWS.PrestacionBeneficiario> TraePrestacionesXIdBeneficiario(Int64 idBeneficiario, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        List<ActoresWS.PrestacionBeneficiario> oList = null;
        try
        {
            ActoresWS.PrestacionBeneficiario[] lista = oServicio.TraePrestacionesXIdBeneficiario(idBeneficiario);
            oList = (List<ActoresWS.PrestacionBeneficiario>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.PrestacionBeneficiario[]),
                               typeof(List<ActoresWS.PrestacionBeneficiario>),
                               URLActores);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraerBeneficiario: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    #endregion Traer Solicitudes X Beneficiario

    #region Traer Solicitudes X Beneficiario
    public static List<ActoresWS.Solicitud> TraerSolicitudesXIdBeneficiario(Int64 idBeneficiario, Int16 codPrestacion, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        List<ActoresWS.Solicitud> oList = null;
        try
        {
            ActoresWS.Solicitud[] lista = oServicio.TraeSolicitudesXIdBenefPrestac(idBeneficiario, codPrestacion);
            oList = (List<ActoresWS.Solicitud>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.Solicitud[]),
                               typeof(List<ActoresWS.Solicitud>),
                               URLActores);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraerBeneficiario: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    #endregion Traer Solicitudes X Beneficiario

    #region Traer Beneficios X Solicitud
    public static List<ActoresWS.Beneficio_Solicitud> TraeBeneficiosXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        List<ActoresWS.Beneficio_Solicitud> oList = null;
        try
        {
            ActoresWS.Beneficio_Solicitud[] lista = oServicio.TraeBeneficiosXSolicitud(id_Beneficiario, codPrestacion);
            oList = (List<ActoresWS.Beneficio_Solicitud>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.Beneficio_Solicitud[]),
                               typeof(List<ActoresWS.Beneficio_Solicitud>),
                               URLSolicitudes);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraerBeneficios por solicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    #endregion


    #region Traer Expedientes X Solicitud
    public static List<ActoresWS.Expediente_Solicitud> TraeExpedientesXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        List<ActoresWS.Expediente_Solicitud> oList = null;
        try
        {
            ActoresWS.Expediente_Solicitud[] lista = oServicio.TraeExpedientesXSolicitud(id_Beneficiario, codPrestacion);
            oList = (List<ActoresWS.Expediente_Solicitud>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.Expediente_Solicitud[]),
                               typeof(List<ActoresWS.Expediente_Solicitud>),
                               URLSolicitudes);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraerExpedientes por solicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }


    #endregion

    #region Trae Movimiento x fecha movimiento

    public static ActoresWS.Movimiento_Solicitud TraeMovimientoXFechaMovimiento(Int64 idBeneficiario, Int16 codPrestacion, String FechaMovimiento, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        ActoresWS.Movimiento_Solicitud iMovimientoSolicitud = null;
        try
        {
            iMovimientoSolicitud = oServicio.TraeMovimientoXFechaMovimiento(idBeneficiario, codPrestacion, FechaMovimiento);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraeMovimientoXFechaMovimiento: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return iMovimientoSolicitud;
    }

    #endregion

    #region Trae Movimientos x solicitud

    public static List<ActoresWS.Movimiento_Solicitud> TraeMovimientosXSolicitud(Int64 idBeneficiario, Int16 codPrestacion, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;
        List<ActoresWS.Movimiento_Solicitud> oList = null;

        try
        {
            ActoresWS.Movimiento_Solicitud[] lista = oServicio.TraeMovimientosXSolicitud(idBeneficiario, codPrestacion);
            oList = (List<ActoresWS.Movimiento_Solicitud>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.Movimiento_Solicitud[]),
                               typeof(List<ActoresWS.Movimiento_Solicitud>),
                               URLSolicitudes);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraeMovimientosXSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion

    #region Trae Ingreso x fecha movimiento

    public static ActoresWS.Ingresos TraeIngresoXMovimientoSolicitud(Int64 idBeneficiario, Int16 codPrestacion, String FechaMovimiento, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        ActoresWS.Ingresos iIngreso = null;
        try
        {
            iIngreso = oServicio.TraeIngresoXMovimientoSolicitud(idBeneficiario, codPrestacion, FechaMovimiento);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraeIngresoXMovimientoSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return iIngreso;
    }

    #endregion

    #region Trae Ingresos x solicitud

    public static List<ActoresWS.Ingresos> TraeIngresosXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion, out string mensaje)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.Ingresos> oList = null;
        mensaje = string.Empty;

        try
        {
            ActoresWS.Ingresos[] lista = oServicio.TraeIngresosXSolicitud(id_Beneficiario, codPrestacion);
            oList = (List<ActoresWS.Ingresos>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.Ingresos[]),
                               typeof(List<ActoresWS.Ingresos>),
                               URLActores);
        }
        catch (Exception ex)
        {
            ////log.ErrorFormat("TraeDevolucionesXSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion

    #region Trae Devolucion x fecha movimiento

    public static ActoresWS.Devolucion TraeDevolucionXMovimientoSolicitud(Int64 idBeneficiario, Int16 codPrestacion, String FechaMovimiento, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        ActoresWS.Devolucion iDevolucion = null;
        try
        {
            iDevolucion = oServicio.TraeDevolucionXMovimientoSolicitud(idBeneficiario, codPrestacion, FechaMovimiento);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeDevolucionXMovimientoSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return iDevolucion;
    }

    #endregion

    #region Trae Devolucion x solicitud

    public static List<ActoresWS.Devolucion> TraeDevolucionXSolicitud(Int64 idBeneficiario, Int16 codPrestacion, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.Devolucion> oList = null;
        mensaje = string.Empty;

        try
        {
            ActoresWS.Devolucion[] lista = oServicio.TraeDevolucionesXSolicitud(idBeneficiario, codPrestacion);
            oList = (List<ActoresWS.Devolucion>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.Devolucion[]),
                               typeof(List<ActoresWS.Devolucion>),
                               URLActores);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeDevolucionesXSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion

    #region Trae Devoluciones notificadas venciadas
    public static List<ConsultasWS.NotificacionesVencidas> TraeDevolucionesNotificadasVencidasXPlazo(Int64 PageNum, Int64 PageSize, Byte ordenPor, out Int64 TotalRowsNum, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ConsultasWS.ConsultasWS oServicio = new ConsultasWS.ConsultasWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ConsultasWS.NotificacionesVencidas> oList = null;
        mensaje = string.Empty;
        TotalRowsNum = 0;

        try
        {
            ConsultasWS.NotificacionesVencidas[] lista = oServicio.TraeDevolucionesNotificadasVencidasXPlazo(PageNum, PageSize, ordenPor, Int16.Parse(ConfigurationManager.AppSettings["DiasPlazoNotificacion"]), out TotalRowsNum );
            oList = (List<ConsultasWS.NotificacionesVencidas>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ConsultasWS.NotificacionesVencidas[]),
                               typeof(List<ConsultasWS.NotificacionesVencidas>),
                               URLConsultas);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeDevolucionesNotificadasVencidasXPlazo: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }
    #endregion Trae Devoluciones notificadas venciadas

    #region Trae Solicitudes EFechas Solicitud
    public static List<ConsultasWS.SolicitudesEFechasSolicitud> TraeSolicitudesEFechasSolicitud(String fDesde, String fHasta, Int16? codPrestacion, Int16? codPais, Boolean mercosur, Byte orderBy, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ConsultasWS.ConsultasWS oServicio = new ConsultasWS.ConsultasWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ConsultasWS.SolicitudesEFechasSolicitud> oList = null;
        mensaje = string.Empty;
        
        try
        {
            ConsultasWS.SolicitudesEFechasSolicitud[] lista = oServicio.TraeSolicitudesEFechasSolicitud(fDesde, fHasta, codPrestacion, codPais, mercosur, orderBy);
            oList = (List<ConsultasWS.SolicitudesEFechasSolicitud>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ConsultasWS.SolicitudesEFechasSolicitud[]),
                               typeof(List<ConsultasWS.SolicitudesEFechasSolicitud>),
                               URLConsultas);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeSolicitudesEFechasSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }
    #endregion Trae Solicitudes EFechas Solicitud

    #region Trae Solicitudes Denegadas
    public static List<ActoresWS.SolicitudDenegada> TraeSolicitudesDenegadasXSolicitud(Int64 idBeneficiario, Int16 codPrestacion, out String mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.SolicitudDenegada> oList = null;
        mensaje = string.Empty;

        try
        {
            ActoresWS.SolicitudDenegada[] lista = oServicio.TraeSolicitudesDenegadasXSolicitud(idBeneficiario, codPrestacion);
            oList = (List<ActoresWS.SolicitudDenegada>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.SolicitudDenegada[]),
                               typeof(List<ActoresWS.SolicitudDenegada>),
                               URLSolicitudes);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeSolicitudesDenegadasXSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }
    #endregion Trae Solicitudes Denegadas

    #region Trae Ultimo Movimiento Solicitud
    public static ConsultasWS.Movimiento_Solicitud TraeUltimoMovimientoSolicitud(Int64 idBeneficiario, Int16 codPrestacion, out String mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ConsultasWS.ConsultasWS oServicio = new ConsultasWS.ConsultasWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        ConsultasWS.Movimiento_Solicitud oMovimiento = null;
        mensaje = string.Empty;

        try
        {
            oMovimiento = oServicio.TraeUltimoMovimientoSolicitud(idBeneficiario, codPrestacion);
            
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeUltimoMovimientoSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oMovimiento;
    }
    #endregion Trae Ultimo Movimiento Solicitud

    #region Trae Movimiento resumen
    public static List<ActoresWS.IngDevMov> TraeMovimientosResumen(Int64 idBeneficiario, Int16 codPrestacion, out String mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.IngDevMov> oListMovimientosRes = null;
        mensaje = string.Empty;

        try
        {
            ActoresWS.IngDevMov[] lista = oServicio.TraeMovimientosResumen(idBeneficiario, codPrestacion);
            oListMovimientosRes = (List<ActoresWS.IngDevMov>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.IngDevMov[]),
                               typeof(List<ActoresWS.IngDevMov>),
                               URLSolicitudes);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeMovimientosResumen: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oListMovimientosRes;
    }
    #endregion 

    #region Trae ultimos codigos de estado y Sector de una solicitud
    public static void TraeUltimoEstadoYSectorSolicitud(Int64 idBeneficiario, Int16 codPrestacion, out Int32 codEstado, out Int32 codSector, out String mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ConsultasWS.ConsultasWS oServicio = new ConsultasWS.ConsultasWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;
        codEstado = 0;
        codSector = 0;

        try
        {
            codSector = oServicio.TraeUltimoEstadoYSectorSolicitud(idBeneficiario, codPrestacion, out codEstado);

        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeUltimoEstadoYSectorSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        
    }
    #endregion Trae Ultimo Movimiento Solicitud

    #region Trae Documentacion faltante por solicitud
    public static List<ActoresWS.TipoDocumentacion_Prestacion> TraeTipoDocumentacionFaltanteXSolicitud(Int64 idBeneficiario, Int16 codPrestacion, out string mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.TipoDocumentacion_Prestacion> oList = null;
        mensaje = string.Empty;

        try
        {
            ActoresWS.TipoDocumentacion_Prestacion[] lista = oServicio.TraeTipoDocumentacionFaltanteXSolicitud(idBeneficiario, codPrestacion);
            oList = (List<ActoresWS.TipoDocumentacion_Prestacion>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.TipoDocumentacion_Prestacion[]),
                               typeof(List<ActoresWS.TipoDocumentacion_Prestacion>),
                               URLSolicitudes);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeTipoDocumentacionFaltanteXSolicitud: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }
    #endregion Trae Documentacion faltante por solicitud

    #region Trae Indicador Por Solicitudes PaisConvenio
    public static List<ConsultasWS.IndicadorPorSolicitudesPaisConvenio> TraeIndicadorPorSolicitudesPaisConvenio(Byte criteriotemporal, Byte param_Temporal, String anio, out string mensaje)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ConsultasWS.ConsultasWS oServicio = new ConsultasWS.ConsultasWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ConsultasWS.IndicadorPorSolicitudesPaisConvenio> oList = null;
        mensaje = string.Empty;

        try
        {
            ConsultasWS.IndicadorPorSolicitudesPaisConvenio[] lista = oServicio.TraeIndicadorPorSolicitudesPaisConvenio(criteriotemporal, param_Temporal, anio);
            oList = (List<ConsultasWS.IndicadorPorSolicitudesPaisConvenio>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ConsultasWS.IndicadorPorSolicitudesPaisConvenio[]),
                               typeof(List<ConsultasWS.IndicadorPorSolicitudesPaisConvenio>),
                               URLConsultas);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeIndicadorPorSolicitudesPaisConvenio: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }
    #endregion

    #region Trae Indicador Por Solicitudes Prestaciones
    public static List<ConsultasWS.IndicadorPorSolicitudesPrestaciones> TraeIndicadorPorSolicitudesPrestaciones(Byte criteriotemporal, Byte param_Temporal, String anio, out string mensaje)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ConsultasWS.ConsultasWS oServicio = new ConsultasWS.ConsultasWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ConsultasWS.IndicadorPorSolicitudesPrestaciones> oList = null;
        mensaje = string.Empty;

        try
        {
            ConsultasWS.IndicadorPorSolicitudesPrestaciones[] lista = oServicio.TraeIndicadorPorSolicitudesPrestaciones(criteriotemporal, param_Temporal, anio);
            oList = (List<ConsultasWS.IndicadorPorSolicitudesPrestaciones>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ConsultasWS.IndicadorPorSolicitudesPrestaciones[]),
                               typeof(List<ConsultasWS.IndicadorPorSolicitudesPrestaciones>),
                               URLConsultas);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeIndicadorPorSolicitudesPrestaciones: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }
    #endregion

    #region Trae Indicador Por Estados a una fecha
    public static List<ConsultasWS.IndicadorTotalesEstado> TraeIndicadorTotalesEstado(String AFecha, out string mensaje)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ConsultasWS.ConsultasWS oServicio = new ConsultasWS.ConsultasWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ConsultasWS.IndicadorTotalesEstado> oList = null;
        mensaje = string.Empty;

        try
        {
            ConsultasWS.IndicadorTotalesEstado[] lista = oServicio.TraeIndicadorTotalesEstadoAFechaX(AFecha);
            oList = (List<ConsultasWS.IndicadorTotalesEstado>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ConsultasWS.IndicadorTotalesEstado[]),
                               typeof(List<ConsultasWS.IndicadorTotalesEstado>),
                               URLConsultas);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeIndicadorTotalesEstado: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }
    #endregion

    #region Trae Indicador Por Sectores a una fecha
    public static List<ConsultasWS.IndicadorTotalesSector> TraeIndicadorTotalesSector(String AFecha, out string mensaje)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ConsultasWS.ConsultasWS oServicio = new ConsultasWS.ConsultasWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ConsultasWS.IndicadorTotalesSector> oList = null;
        mensaje = string.Empty;

        try
        {
            ConsultasWS.IndicadorTotalesSector[] lista = oServicio.TraeIndicadorTotalesSectorAFechaX(AFecha);
            oList = (List<ConsultasWS.IndicadorTotalesSector>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ConsultasWS.IndicadorTotalesSector[]),
                               typeof(List<ConsultasWS.IndicadorTotalesSector>),
                               URLConsultas);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeIndicadorTotalesSector: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }
    #endregion

    #region Trae Apoderados X Beneficiario
    public static List<ActoresWS.Apoderado> TraeApoderadosXid_Beneficiario(Int64 id_Beneficiario, out string mensaje)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.Apoderado> oList = null;
        mensaje = string.Empty;

        try
        {
            ActoresWS.Apoderado[] lista = oServicio.TraeApoderadosXid_Beneficiario(id_Beneficiario);
            oList = (List<ActoresWS.Apoderado>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.Apoderado[]),
                               typeof(List<ActoresWS.Apoderado>),
                               URLActores);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeApoderadosXid_Beneficiario: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;

    }
    #endregion Trae Apoderados X Beneficiario

    #region Trae Notas X Beneficiario
    public static List<ActoresWS.BeneficiarioNotas> TraeBeneficiario_Notas(Int64 id_Beneficiario, out string mensaje)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<ActoresWS.BeneficiarioNotas> oList = null;
        mensaje = string.Empty;

        try
        {
            ActoresWS.BeneficiarioNotas[] lista = oServicio.TraeBeneficiario_Notas(id_Beneficiario);
            oList = (List<ActoresWS.BeneficiarioNotas>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(ActoresWS.BeneficiarioNotas[]),
                               typeof(List<ActoresWS.BeneficiarioNotas>),
                               URLActores);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeBeneficiario_Notas: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;

    }
    #endregion

    #region Trae Tipos Tramite Expediente por prestacion / pais
    public static List<AuxiliaresWS.TiposTramiteConvenios> TraeTiposTramiteConvenios(Int16 codPais, Int16 codPrestacion, out string mensaje)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        AuxiliaresWS.AuxiliaresWS oServicio = new AuxiliaresWS.AuxiliaresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        List<AuxiliaresWS.TiposTramiteConvenios> oList = null;
        mensaje = string.Empty;

        try
        {
            AuxiliaresWS.TiposTramiteConvenios[] lista = oServicio.TraerTiposTramiteConvenios(codPais, codPrestacion);
            oList = (List<AuxiliaresWS.TiposTramiteConvenios>)
                               reSerializer.reSerialize(
                               lista,
                               typeof(AuxiliaresWS.TiposTramiteConvenios[]),
                               typeof(List<AuxiliaresWS.TiposTramiteConvenios>),
                               URLAuxiliares);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("TraeTiposTramiteConvenios: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";  throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;

    }
    #endregion

    #endregion Consultas

    #region solicitudes Provisorias

    #region SolicitudProvisoria_Alta

    public static void SolicitudProvisoria_Alta(ActoresWS.SolicitudProvisoria iSolicitudProvisoria, out String mensaje, out String nroSolicitud)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;
        nroSolicitud = string.Empty;

        try
        {
            nroSolicitud = oServicio.SolicitudProvisoria_Alta(iSolicitudProvisoria);
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("SolicitudProvisoria_Alta: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle"; 
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion

    #region SolicitudesProvisoriaMovimiento_Alta

    public static void SolicitudesProvisoriaMovimiento_Alta(List<ActoresWS.SolicitudProvisoriaMovimiento> iSolicitudesProvisoriaMovimiento, out String mensaje)
    {
        //
        ILog log = LogManager.GetLogger(typeof(InvocaWsDao).Name);
        ActoresWS.ActoresWS oServicio = new ActoresWS.ActoresWS();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        mensaje = string.Empty;

        try
        {
            oServicio.SolicitudesProvisoriaMovimiento_Alta(iSolicitudesProvisoriaMovimiento.ToArray());
        }
        catch (Exception ex)
        {
            //log.ErrorFormat("SolicitudProvisoria_Alta: {0}", ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle";
            throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
    }

    #endregion

    #endregion solicitudes Provisorias
}