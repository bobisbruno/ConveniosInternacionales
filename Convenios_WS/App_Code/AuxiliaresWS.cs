using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Services;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Negocio;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Datos;
using LoggingAnses.Servicio;
using LoggingAnses.Servicio.Comun;
using LoggingAnses.Servicio.Entidad;


namespace Ar.Gov.Anses.Microinformatica.AnsesConveniosInternacionalesX5.Servicios
{
    /// <summary>
    /// Brinda servicios de Consulta
    /// </summary>
    [WebService(Namespace = "http://AnsesConveniosInternacionalesX5.anses.gov.ar")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AuxiliaresWS : System.Web.Services.WebService
    {
        
        public AuxiliaresWS()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        #region Consultas

        [WebMethod(Description = "Trae version del sitio")]
        public String VersionSistema()
        {
            return ConfigurationManager.AppSettings["VersionSistema"];
        }

        [WebMethod(Description = "Trae listado de Prestaciones")]
        public List<Prestacion> TraerPrestaciones()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {   
                return objdao.TraePrestaciones();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        [WebMethod(Description = "Trae listado de Tipos de tramite expediente por pais y prestacion")]
        public List<TiposTramiteConvenios> TraerTiposTramiteConvenios(Int16 codPais, Int16 codPrestacion)
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeTiposTramiteConvenios(codPais, codPrestacion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [WebMethod(Description = "Trae tramites derivados")]
        public List<Tramitesderivados> TraeTramitesDerivados()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeTrDerivado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod(Description = "Trae listado de Tipos de Ingreso")]
        public List<TipoIngreso> Traer_TipoIngreso()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.Traer_TipoIngreso();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }


        [WebMethod(Description = "Trae listado de Paises 2 Extranjero")]
        public List<Pais2> TraePaises2()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraePaises2();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        [WebMethod(Description = "Trae listado de ciudades extranjera por codigo de pais 3 c")]
        public List<Ciudad> TraeCiudadesExtXPais(string codPais3c)
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeCiudadesExtXPais(codPais3c);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        //[WebMethod(Description = "Trae listado de TipoPoder")]
        //public List<TipoPoder> Traer_TipoPoder()
        //{
        //    AuxiliaresDatos objdao = new AuxiliaresDatos();
        //    try
        //    {
        //        return objdao.Traer_TipoPoder();
        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw ex;
        //    }
        //}

        [WebMethod(Description = "Trae listado de Sectores")]
        public List<Sector> TraeSectores()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeSectores();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        [WebMethod(Description = "Trae listado de Tipo Apoderado")]
        public List<TipoApoderado> TraeTipoApoderado()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeTipoApoderado();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        [WebMethod(Description = "Trae listado de  Sub Tipos de Apoderado")]
        public List<SubTipoApoderado> TraeSubTipoApoderado()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeSubTiposApoderado();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [WebMethod(Description = "Trae listado de TipoDocumentacion")]
        public List<TipoDocumentacion> TraeTipoDocumentacion()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeTipoDocumentacion();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        [WebMethod(Description = "Trae listado de tipos de Documentacion y su prestacion asociada")]
        public List<TipoDocumentacion_Prestacion> TraeTodoTipoDocumentacionPrestacion()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeTodoTipoDocumentacionPrestacion();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        
        [WebMethod(Description = "Trae listado de tipos de Documento")]
        public List<TipoDocumento> Traer_TipoDocumento(bool soloFrecuentes)
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.Traer_TipoDocumento(soloFrecuentes);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        [WebMethod(Description = "Trae listado de Estados")]
        public List<Estado> TraeEstados()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeEstados();
            }

            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        //[WebMethod(Description = "Trae listado de Tipos de Prestaciones con su tipo de documentacion requerida asociada")]
        //public List<Prestacion> TraePrestacionesC()
        //{
        //    AuxiliaresDatos objdao = new AuxiliaresDatos();
        //    try
        //    {
        //        return objdao.TraePrestacionesC();
        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw ex;
        //    }

        //}

        [WebMethod(Description = "Trae listado de Tipos de Provincias")]
        public List<Provincia> TraeProvincias()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeProvincias();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [WebMethod(Description = "Trae listado de motivos denegacion")]
        public List<MotivoDenegacion> TraeMotivosDenegacion()
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeMotivosDenegacion();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        [WebMethod(Description = "Trae listado de Localidades X Provincia")]
        public List<Localidad> TraeLocalidadesXProvincia(Int16 codProvincia )
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            try
            {
                return objdao.TraeLocalidadesXProvincia(codProvincia);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion Consultas

        #region Altas y Modificaciones

        [WebMethod(Description = "AMTipodeDocumentacion_Prestacion")]
        public void AMTipodeDocumentacion_Prestacion(Int32 codTipoDocumentacion, Int16 codPrestacion, string comentario, bool requeridoinicioTramite)
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            //LogAplicaciones logging = new //LogAplicaciones();

            try
            {
                objdao.AMTipodeDocumentacion_Prestacion(codTipoDocumentacion, codPrestacion, comentario, requeridoinicioTramite);
                
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = codTipoDocumentacion.ToString() + codPrestacion.ToString(),
                //    Datos = comentario + requeridoinicioTramite.ToString(),
                //    Tabla = "TipodeDocumentacion_Prestacion",
                //    TipoAccion = TipoAction.AGREGAR
                //});
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [WebMethod(Description = "AMTiposdeDocumentacion")]
        public void AMTiposdeDocumentacion(Int32? codTipoDocumentacion, string descripcion)
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                objdao.AMTipodeDocumentacion(codTipoDocumentacion, descripcion);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = codTipoDocumentacion.ToString(),
                //    Datos = descripcion,
                //    Tabla = "TiposdeDocumentacion",
                //    TipoAccion = TipoAction.AGREGAR
                //});    
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [WebMethod(Description = "BajaTipodeDocumentacion_Prestacion")]
        public void BajaTipodeDocumentacion_Prestacion(Int32 codTipoDocumentacion, Int32 codPrestacion)
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                objdao.BajaTipodeDocumentacion_Prestacion(codTipoDocumentacion, codPrestacion);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = codTipoDocumentacion.ToString() + codPrestacion.ToString(),
                //    Datos = codTipoDocumentacion.ToString() + codPrestacion.ToString(),
                //    Tabla = "BajaTipodeDocumentacion_Prestacion",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        

        #endregion 

    }
}