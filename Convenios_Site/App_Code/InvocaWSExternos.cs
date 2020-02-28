using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using log4net;
using DatosdePersonaporCuip;
using ObtenerDatosxApeyNom;
using ObtenerDatosxDocumento;
using ObtenerRelacionesxCuil;
using DigitalizacionServicio;
using ExpedienteWS;


/// <summary>
/// Summary description for InvocaArchivosWS
/// </summary>
/// 

public static class InvocaWSExternos
{
    static readonly ILog log = LogManager.GetLogger(typeof(InvocaWSExternos));

    //public static string URLExpedientesANME
    //{
    //    get
    //    {
    //        ExpedienteWS.ExpedienteWS oServicio = new ExpedienteWS.ExpedienteWS();
    //        //oServicio.Url = ConfigurationManager.AppSettings["ExpedienteWS.ExpedienteWS"];

    //        string wsUrl = oServicio.Url;
    //        oServicio.Dispose();
    //        return wsUrl;
    //    }
    //}
    

    //public static string URLADPWSConsultaXCuipWS
    //{
    //    get
    //    {
    //        ObtenerDatosPersonaADPWS.ObtenerDatosPersonaADPWS oServicio = new ObtenerDatosPersonaADPWS.ObtenerDatosPersonaADPWS();
    //        string wsUrl = oServicio.Url;
    //        oServicio.Dispose();
    //        return wsUrl;
    //    }
    //}

    //public static string URLADPWSConsultaXDocWS
    //{
    //    get
    //    {
    //        ADPWS.WsPw02 oServicio = new ADPWS.WsPw02();
    //        string wsUrl = oServicio.Url;
    //        oServicio.Dispose();
    //        return wsUrl;
    //    }
    //}


    //public static string URLDigiWeb
    //{
    //    get
    //    {
    //        DigitalizacionServicio.DigitalizacionServicio oServicio = new DigitalizacionServicio.DigitalizacionServicio();
            
    //        string wsUrl = oServicio.Url;
    //        oServicio.Dispose();
    //        return wsUrl;
    //    }
    //}
    

    #region  Ansesdigi

    public static Boolean ObtenerRutaArchivo(string CodigoExterno, string codSistema, out string ruta)
    {
        DigitalizacionServicio.DigitalizacionServicio oServicio = new DigitalizacionServicio.DigitalizacionServicio();
        //invoco servicio de digitalizacion paraobtener la dir absoluta del archivo
        ruta = string.Empty;       

        try
        {
            oServicio.Url = ConfigurationManager.AppSettings["DigitalizacionServicio.DigitalizacionServicio"];
            oServicio.Credentials = CredentialCache.DefaultCredentials;
            DigitalizacionServicio.EDocumentoOriginal[] oEdoc = oServicio.BuscarEDocumentosPorCodigoExternoV2(CodigoExterno, codSistema);
            if (oEdoc.Length == 0)
            {
                log.Error(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), "Documento no hallado en digiweb", CodigoExterno));
                ruta = "";
            }
            else
                ruta = oEdoc.First().Ruta;
        }
        catch (Exception ex)
        {
            log.Error(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
            return false;
        }
        finally
        {
            oServicio.Dispose();
        }
        return true;
    }



    public static void GrabarArchivoHost(string codigoExterno, string archivoNombre, string ruta)
    {
        //Graba nuevo documento o actualiza en DIGIWEB
        DigitalizacionServicio.DigitalizacionServicio oServicio = new DigitalizacionServicio.DigitalizacionServicio();

        try
        {
            oServicio.Url = ConfigurationManager.AppSettings["DigitalizacionServicio.DigitalizacionServicio"];
            oServicio.Credentials = CredentialCache.DefaultCredentials;
            DigitalizacionServicio.EDocumentoOriginal ed = new DigitalizacionServicio.EDocumentoOriginal();
            ed.CodigoExterno = codigoExterno;
            ed.CodigoSistema = ConfigurationManager.AppSettings["Sistema"];
            ed.Descripcion = ConfigurationManager.AppSettings["NombreAplicacion"];
            ed.DigitoVerificador = null;
            ed.Entidad = "";
            ed.EstadoEDocumentoId = 1;
            ed.FechaIndexacion = System.DateTime.Today;
            //ed.Id = Guid.NewGuid();
            ed.Metadata = "";
            ed.Nombre = archivoNombre;
            ed.NumeroDocumento = "";
            ed.PreCuil = null;
            ed.Ruta = ruta + "\\" + archivoNombre;
            ed.Secuencia = null;
            ed.TipoEDocumentoId = 1;
            ed.TipoTramite = null;
            ed.Titulo = archivoNombre;
            //verifica que el edoc no exista
            DigitalizacionServicio.EDocumentoOriginal[] oEdoc = oServicio.BuscarEDocumentosPorCodigoExternoV2(codigoExterno, ConfigurationManager.AppSettings["Sistema"]);
            if (oEdoc.Length == 0)
            {
                ed.Id = Guid.NewGuid();
                //no se encuentra el dictamen - Se graba nuevo
                oServicio.GuardarEDocumentoV2(ed);
            }
            else
            {
                ed.Id = oEdoc.First().Id;
                oServicio.ActualizarEDocumentoV2(ed);
            }
        }
        catch (Exception ex)
        {
            log.Error(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }

    }

    #region trae ruta a grabar
    public static String ObtenerRutaAGrabarDeSistema(String codigoSistema)
    {
        DigitalizacionServicio.DigitalizacionServicio oServicio = new DigitalizacionServicio.DigitalizacionServicio();
        String rutraSave = string.Empty;
        try
        {
            oServicio.Url = ConfigurationManager.AppSettings["DigitalizacionServicio.DigitalizacionServicio"];
            oServicio.Credentials = CredentialCache.DefaultCredentials;
            rutraSave = oServicio.CalcularRutaSistema(codigoSistema);
        }
        catch (Exception ex)
        {
            log.Error(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return rutraSave;
    }

    #endregion trae ruta a grabar

    #endregion  Ansesdigi

    #region Trae Datos persona ADP

    #region Trae Datos persona ADP X Doc

    public static ListaPw02 TraerDatosPersonaADPXDoc(string numDoc, short pagina, out String mensaje)
    {
        mensaje = string.Empty;
        ILog log = LogManager.GetLogger(typeof(InvocaWSExternos).Name);
        WsPw02 oServicio = new WsPw02();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        ListaPw02 oDatoPers = null;

        try
        {
            oDatoPers = oServicio.ObtenerDatosxDocumento(numDoc, pagina);
        }
        catch (Exception ex)
        {
            log.ErrorFormat("{0}-{1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle"; throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oDatoPers;
    }

    #endregion Traer

    #region Trae Relacion persona

    public static ListaPw04 ObtenerRelacionesxCuil(string cuip, short pagina, out String mensaje)
    {
        mensaje = string.Empty;
        ILog log = LogManager.GetLogger(typeof(InvocaWSExternos).Name);
        WS_PW04 oServicio = new WS_PW04();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        ListaPw04 oDatoPers = null;

        try
        {
            oDatoPers = oServicio.ObtenerRelacionesxCuil(cuip, pagina);
        }
        catch (Exception ex)
        {
            log.ErrorFormat("{0}-{1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle"; throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oDatoPers;
    }

    #endregion Trae Datos persona ADP X Cuip

    #region Trae Datos persona ADP X Cuip

    public static RetornoDatosPersonaCuip TraerDatosPersonaADPXCuip(string cuip, out String mensaje)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWSExternos).Name);

        DatosdePersonaporCuip.DatosdePersonaporCuip oServicio = new DatosdePersonaporCuip.DatosdePersonaporCuip();
        
        mensaje = string.Empty;

        oServicio.Credentials = CredentialCache.DefaultCredentials;
        oServicio.Url = ConfigurationManager.AppSettings["DatosdePersonaporCuip.DatosdePersonaporCuip"];
        RetornoDatosPersonaCuip oDatoPers = null;
        
        try
        {
            oDatoPers = oServicio.ObtenerPersonaxCUIP(cuip);
            
        }
        catch (Exception ex)
        {
            log.ErrorFormat("{0}-{1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle"; throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oDatoPers;
    }

    #endregion Traer

    #region Trae Datos persona ADP xapellidoynombre

    public static ListaPw03 ObtenerDatosxApeyNom(string ApellidoyNombre, short pagina, out String mensaje)
    {
        mensaje = string.Empty;
        ILog log = LogManager.GetLogger(typeof(InvocaWSExternos).Name);
        WsPw03 oServicio = new WsPw03();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        ListaPw03 oDatoPers = null;

        try
        {
            oDatoPers = oServicio.ObtenerDatosxApeyNom(ApellidoyNombre, pagina);
        }
        catch (Exception ex)
        {
            log.ErrorFormat("{0}-{1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle"; throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oDatoPers;
    }

    #endregion Traer

    #endregion Trae Datos persona ADP

    #region ANME

    #region Trae Expedientes por Org - cuil - tiposTram

    public static List<ExpedienteDTO> TraerExpedientesPorOrganismoYCuilYTiposDeTramites(CuilDTO cuilDto, string tiposTram, out string mensaje)
    {
        ILog log = LogManager.GetLogger(typeof(InvocaWSExternos).Name);
        ExpedienteWS.ExpedienteWS oServicio = new ExpedienteWS.ExpedienteWS();
        oServicio.Url = ConfigurationManager.AppSettings["ExpedienteWS.ExpedienteWS"];
        CuilDTO iCuil = new CuilDTO();
        oServicio.Credentials = CredentialCache.DefaultCredentials;
        TipoError oError = new TipoError();
        List<ExpedienteDTO> oList = null;
        mensaje = string.Empty;

        try
        {
            ExpedienteDTO[] lista = oServicio.BuscarExpedientesPorOrganismoYCuilYTiposDeTramites("024", cuilDto, tiposTram, out oError);
            if (oError.codigo == 0)
                oList = (List<ExpedienteDTO>)
                              reSerializer.reSerialize(
                              lista,
                              typeof(ExpedienteWS.ExpedienteDTO[]),
                              typeof(List<ExpedienteWS.ExpedienteDTO>),
                              oServicio.Url);
            else
                mensaje = oError.descripcion;


        }
        catch (Exception ex)
        {
            log.ErrorFormat("{0}-{1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message);
            mensaje = "Ocurrió un error inesperado. Revise el Log para mayor detalle"; throw new Exception(string.Format("{0} - Error:{1}->{2}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Source, ex.Message));
        }
        finally
        {
            oServicio.Dispose();
        }
        return oList;
    }

    #endregion Traer

    #endregion ANME


    
}