using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security.Principal;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Negocio;
using LoggingAnses.Servicio;
using LoggingAnses.Servicio.Entidad;


namespace Ar.Gov.Anses.Microinformatica.AnsesConveniosInternacionalesX5.Servicios
{
    /// <summary>
    /// Brinda servicios de Consulta
    /// </summary>
    [WebService(Namespace = "http://AnsesConveniosInternacionalesX5.anses.gov.ar")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class ActoresWS : System.Web.Services.WebService
    {
        

        public ActoresWS()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        
        [WebMethod(Description = "Trae listado de Beneficioarios por Nombre - Documento (Beneficiario o Causante) - ExpedienteExterno SIACI")]
        public List<LsBeneficiario> TraeBeneficiarios(TipoConsultaBeneficioario iTipoCons, String parametro, String codDoc)
        {
            ActoresDatos objdao = new ActoresDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeBeneficiarios(iTipoCons, parametro, codDoc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "Trae listado de Beneficioarios por Expediente ANSES")]
        public List<LsBeneficiario> TraeBeneficiariosXExpteANSES(string expediente_org
            , string expediente_precu
            , string expediente_doccu
            , string expediente_digcu
            , string expediente_ctipo
            , string expediente_sec)
        {
            ActoresDatos objdao = new ActoresDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeBeneficiariosXExpteANSES( expediente_org
            ,  expediente_precu
            ,  expediente_doccu
            ,  expediente_digcu
            ,  expediente_ctipo
            ,  expediente_sec);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "Trae Beneficios de una Peticion")]

        public List<Beneficio_Solicitud> TraeBeneficiosXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion)
        {
            SolicitudesDatos objDao = new SolicitudesDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeBeneficiosXSolicitud(id_Beneficiario, codPrestacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae Expedientes de una Peticion")]
        public List<Expediente_Solicitud> TraeExpedientesXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion)
        {
            SolicitudesDatos objDao = new SolicitudesDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeExpedientesXSolicitud(id_Beneficiario, codPrestacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }


        [WebMethod(Description = "Trae listado de Beneficiarios por cuip")]
        public List<LsBeneficiario> TraeBeneficiariosXCUIP(string preCUIP, string docCUIP, string digCUIP)
        {
            ActoresDatos objdao = new ActoresDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeBeneficiariosXCUIP(preCUIP, docCUIP, digCUIP);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }


        [WebMethod(Description = "Trae de Beneficiarios por solicitudprovisoria")]
        public List<LsBeneficiario> TraeBeneficiariosXNroSolicitudProvisoria(string nro_SolicitudProvisoria)
        {
            ActoresDatos objdao = new ActoresDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeBeneficiariosXNroSolicitudProvisoria(nro_SolicitudProvisoria);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        
        [WebMethod(Description = "Trae listado de Beneficioarios por Expediente ANSES")]
        public List<LsBeneficiario> TraeBeneficiariosXNroBeneficioANSES(string BenExCaja, string BenTipo, string BenNumero, string BenCopart, string BenDigVerif)
        {
            ActoresDatos objdao = new ActoresDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeBeneficiariosXNroBeneficioANSES( BenExCaja,  BenTipo,  BenNumero,  BenCopart,  BenDigVerif);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "Trae datos de un Beneficioario completos por idBeneficiario")]
        public Beneficiario TraeBeneficiarioXId(Int64 idBeneficiario)
        {
            ActorDatos objdao = new ActorDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeBeneficiarioXID(idBeneficiario);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "Trae datos de un Beneficioario por idBeneficiario")]
        public LsBeneficiario TraeBeneficiarioSimpleXId(Int64 idBeneficiario)
        {
            ActorDatos objdao = new ActorDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeBeneficiarioSimpleXID(idBeneficiario);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }



        [WebMethod(Description = "Trae las solicitudes de un Beneficioario por idBeneficiario")]
        public List<PrestacionBeneficiario> TraePrestacionesXIdBeneficiario(Int64 idBeneficiario)
        {
            SolicitudesDatos objdao = new SolicitudesDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraePrestacionesXIdBeneficiario(idBeneficiario);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }


        [WebMethod(Description = "Trae las solicitudes de un Beneficioario por idBeneficiario")]
        public List<Solicitud> TraeSolicitudesXIdBenefPrestac(Int64 idBeneficiario, Int16 codPrestacion)
        {
            SolicitudesDatos objdao = new SolicitudesDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeSolicitudesXIdBenefPrestac(idBeneficiario, codPrestacion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }


        [WebMethod(Description = "Trae notas ingresadas por idBeneficiario")]
        public List<BeneficiarioNotas> TraeBeneficiario_Notas(Int64 idBeneficiario)
        {
            ActorDatos objdao = new ActorDatos();http://10.86.36.116/Convenios_WS/App_Code/BancoWS.cs
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeBeneficiario_Notas(idBeneficiario);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }


        [WebMethod(Description = "Ingresa una nota para un idBeneficiario")]
        public void AMBeneficiario_Notas(BeneficiarioNotas iParam)
        {
            ActorDatos objdao = new ActorDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                objdao.AMBeneficiarioNotas(iParam);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        
        [WebMethod(Description = "Ingresa o Modifica datos de un Beneficiario")]
        public Int64  AMBeneficiario(Beneficiario iBeneficiario)
        {
            ActorDatos objdao = new ActorDatos();
            Int64 idBeneficiario;
            //LogAplicaciones logging = new LogAplicaciones();
            try
            {   
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                objdao.AMBeneficiario(iBeneficiario, out idBeneficiario);

                //graba rutina del log
                    //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = iBeneficiario.IdBeneficio.HasValue ? iBeneficiario.IdBeneficio.ToString() : "",
                //    Datos = iBeneficiario,
                //    Tabla = "Beneficiarios",
                //    TipoAccion = iBeneficiario.IdBeneficio.HasValue ? TipoAction.ACTUALIZAR : TipoAction.AGREGAR
                //});    

                return idBeneficiario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }


        [WebMethod(Description = "Ingresa o Modifica datos de un Causante")]
        public void AMCausante(Causante iCausante)
        {
            ActorDatos objdao = new ActorDatos();
            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                objdao.AMCausante(iCausante);

                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = iCausante.Id_causante.ToString(),
                //    Datos = iCausante,
                //    Tabla = "Causantes",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "Ingresa o Modifica datos de un Apoderado")]
        //public Int64 AMApoderado(Apoderado iApoderado, Int64 idBeneficiario)
        public void AMApoderado(Apoderado iApoderado, Int64 idBeneficiario)
        {
            
            ActorDatos objdao = new ActorDatos();
            //Int64 idApod;
            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                //objdao.AMApoderado(iApoderado, out idApod, idBeneficiario);
                objdao.AMApoderado(iApoderado, idBeneficiario);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = iApoderado.Id_apoderado.HasValue ? iApoderado.Id_apoderado.ToString() : "",
                //    Datos = iApoderado,
                //    Tabla = "Apoderados",
                //    TipoAccion = iApoderado.Id_apoderado.HasValue ? TipoAction.ACTUALIZAR : TipoAction.AGREGAR
                //});    

                //return idApod;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "Establece la baja de un Apoderado")]
        //public Int64 AMApoderado(Apoderado iApoderado, Int64 idBeneficiario)
        public void BajaBeneficiario_Apoderado(Apoderado iApoderado, Int64 idBeneficiario)
        {

            ActorDatos objdao = new ActorDatos();
            //Int64 idApod;
            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                //objdao.AMApoderado(iApoderado, out idApod, idBeneficiario);
                objdao.BajaBeneficiario_Apoderado(iApoderado, idBeneficiario);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = iApoderado.Id_apoderado.HasValue ? iApoderado.Id_apoderado.ToString() : "",
                //    Datos = iApoderado,
                //    Tabla = "Apoderados",
                //    TipoAccion = iApoderado.Id_apoderado.HasValue ? TipoAction.ACTUALIZAR : TipoAction.AGREGAR
                //});    

                //return idApod;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "Trae apoderados por idBeneficiario")]
        public List<Apoderado> TraeApoderadosXid_Beneficiario(Int64 id_Beneficiario)
        {
            ActoresDatos objdao = new ActoresDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeApoderadosXid_Beneficiario(id_Beneficiario);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "TraePrestacionesNoIngresadasXIdBeneficiario")]
        public List<Prestacion> TraePrestacionesNoIngresadasXIdBeneficiario(Int64 idBeneficiario)
        {
            SolicitudesDatos objDao = new SolicitudesDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraePrestacionesNoIngresadasXIdBeneficiario(idBeneficiario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae movimientos resumen por benef y prestacion")]
        public List<IngDevMov> TraeMovimientosResumen(Int64 idBeneficiario, Int16 codPrestacion)
        {
            MovimientosDatos objDao = new MovimientosDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeMovimientosResumen(idBeneficiario, codPrestacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae Movimiento de una solicitud por fecha de movimiento")]
        public Movimiento_Solicitud TraeMovimientoXFechaMovimiento(Int64 idBeneficiario, Int16 codPrestacion, String FechaMovimiento)
        {
            MovimientosDatos objDao = new MovimientosDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeMovimientoXFechaMovimiento(idBeneficiario, codPrestacion, FechaMovimiento);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae Movimientos de una solicitud")]
        public List<Movimiento_Solicitud> TraeMovimientosXSolicitud(Int64 idBeneficiario, Int16 codPrestacion)
        {
            MovimientosDatos objDao = new MovimientosDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeMovimientosXSolicitud(idBeneficiario, codPrestacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae Solicitudes Denegadas X Solicitud")]
        public List<SolicitudDenegada> TraeSolicitudesDenegadasXSolicitud(Int64 idBeneficiario, Int16 codPrestacion)
        {
            SolicitudesDatos objDao = new SolicitudesDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeSolicitudesDenegadasXSolicitud(idBeneficiario, codPrestacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Efectua el alta de una devolucion sin notificar grabando la documentacion")]
        public void AltaDevolucion(Int64 id_Beneficiario, Int16 codPrestacion, String destino, String observaciones, String certificado, List<TipoDocumentacion> iListTipoDocumentacion)
        {
            MovimientosDatos objDao = new MovimientosDatos();
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                objDao.AltaDevolucion(id_Beneficiario, codPrestacion, destino, observaciones, certificado, iListTipoDocumentacion);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString()+codPrestacion.ToString(),
                //    Datos = destino + observaciones + certificado,
                //    Tabla = "Devoluciones",
                //    TipoAccion = TipoAction.AGREGAR
                //});    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Efectua el alta de un ingreso de documentacion guardando la documentacion ingresada")]
        public void AltaIngreso(Int64 id_Beneficiario, Int16 codPrestacion, String fIngreso, Byte? idTipoIngreso, List<TipoDocumentacion> iListTipoDocumentacion, String observacion)
        {
            MovimientosDatos objDao = new MovimientosDatos();
            //List<String> lDocRepetida;
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                objDao.AltaIngreso(id_Beneficiario, codPrestacion, fIngreso, idTipoIngreso, iListTipoDocumentacion, observacion);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString() + codPrestacion.ToString(),
                //    Datos = idTipoIngreso.HasValue ? idTipoIngreso.Value.ToString() : "",
                //    Tabla = "Ingresos",
                //    TipoAccion = TipoAction.AGREGAR
                //});    
                //return lDocRepetida;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Efectua el alta de una solicitud mas listado de expedientes y beneficios")]
        public void AMAllDatosSolicitud(Int64 idBenef, Int16 codPrestacion,Int16 codPais, List<Solicitud> ilSolicitud, List<Expediente_Solicitud> ilExpediente, List<Beneficio_Solicitud> ilBeneficio, List<Ingresos> iLingresos, List<Devolucion> iLdevolucion, List<Movimiento_Solicitud> ilMovimientos)
        {
            SolicitudesDatos objDao = new SolicitudesDatos();
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                objDao.AMAllDatosSolicitud(idBenef, codPrestacion, codPais, ilSolicitud, ilExpediente, ilBeneficio, iLingresos, iLdevolucion, ilMovimientos);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString() + codPrestacion.ToString(),
                //    Datos = codEstado.ToString() + codsector.ToString() + observaciones,
                //    Tabla = "Movimientos_Solicitud",
                //    TipoAccion = TipoAction.AGREGAR
                //});    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }


        [WebMethod(Description = "Efectua la baja logica de una solicitud, que posteriormente puede volver a seleccionarse")]
        public void BajaSolicitud(Int64 idBenef, Int16 codPrestacion)
        {
            SolicitudesDatos objDao = new SolicitudesDatos();
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                objDao.BajaSolicitud(idBenef, codPrestacion);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString() + codPrestacion.ToString(),
                //    Datos = codEstado.ToString() + codsector.ToString() + observaciones,
                //    Tabla = "Movimientos_Solicitud",
                //    TipoAccion = TipoAction.AGREGAR
                //});    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Efectua el alta de un movimiento del tramite")]
        public void AltaMovimiento(Int64 id_Beneficiario, Int16 codPrestacion, Int32 codEstado, Int32 codsector, String observaciones)
        {
            MovimientosDatos objDao = new MovimientosDatos();
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                objDao.AltaMovimiento(id_Beneficiario, codPrestacion, codEstado, codsector, observaciones);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString() + codPrestacion.ToString(),
                //    Datos = codEstado.ToString() + codsector.ToString() + observaciones,
                //    Tabla = "Movimientos_Solicitud",
                //    TipoAccion = TipoAction.AGREGAR
                //});    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Notifica una devolucion para establecer el control de plazo")]
        public void NotificaDevolucion(Int64 id_Beneficiario, Int16 codPrestacion, String fechaMovimiento, String fechaNotificacion)
        {
            MovimientosDatos objDao = new MovimientosDatos();
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                objDao.NotificaDevolucion(id_Beneficiario, codPrestacion, fechaMovimiento, fechaNotificacion);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString() + codPrestacion.ToString() + fechaMovimiento,
                //    Datos = fechaNotificacion,
                //    Tabla = "Devoluciones",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae listado de tipo de Documentacion por prestacion")]
        public List<TipoDocumentacion> TraeTipoDocumentacionXPrestacion(Int16 codPrestacion)
        {
            AuxiliaresDatos objdao = new AuxiliaresDatos();
            return objdao.TraeTipoDocumentacionXPrestacion(codPrestacion);

        }


        [WebMethod(Description = "Establece fecha de entrega de Documentacion para una devolucion")]
        public void ModificaDevolucion_SetFPresentacion(Int64 id_Beneficiario, Int16 codPrestacion, String fechaMovimiento, String fechaPresentacion)
        {
            MovimientosDatos objDao = new MovimientosDatos();
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                objDao.ModificaDevolucion_SetFPresentacion(id_Beneficiario, codPrestacion, fechaMovimiento, fechaPresentacion);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString() + codPrestacion.ToString() + fechaMovimiento,
                //    Datos = fechaPresentacion,
                //    Tabla = "Devoluciones",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae devolucion por fecha de devolucion")]
        public Devolucion TraeDevolucionXMovimientoSolicitud(Int64 id_Beneficiario, Int16 codPrestacion, String fMovimiento)
        {
            DevolucionesDatos objDao = new DevolucionesDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeDevolucionXMovimientoSolicitud(id_Beneficiario, codPrestacion, fMovimiento);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae devoluciones X Solicitud")]
        public List<Devolucion> TraeDevolucionesXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion)
        {
            DevolucionesDatos objDao = new DevolucionesDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeDevolucionesXSolicitud(id_Beneficiario, codPrestacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }


        [WebMethod(Description = "Trae ingresos por solicitud con documentación recibida")]
        public List<Ingresos> TraeIngresosXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion)
        {
            IngresosDatos objDao = new IngresosDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeIngresosXSolicitud(id_Beneficiario, codPrestacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae ingreso por fecha de devolucion")]
        public Ingresos TraeIngresoXMovimientoSolicitud(Int64 id_Beneficiario, Int16 codPrestacion, String fMovimiento)
        {
            IngresosDatos objDao = new IngresosDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeIngresoXMovimientoSolicitud(id_Beneficiario, codPrestacion, fMovimiento);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae Documentacion faltante por solicitud")]
        public List<TipoDocumentacion_Prestacion> TraeTipoDocumentacionFaltanteXSolicitud(Int64 idBeneficiario, Int16 codPrestacion)
        {
            SolicitudesDatos objDao = new SolicitudesDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraeTipoDocumentacionFaltanteXSolicitud(idBeneficiario, codPrestacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }
        }

        [WebMethod(Description = "Devuelve true si existe el documento")]
        public Boolean ExisteDocumento(String doc, Int16 tdoc)
        {
            ActorDatos objdao = new ActorDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.ExisteDocumento(doc, tdoc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }


        #region Solictudes provisorias

        [WebMethod(Description = "Ingresa una solicitud provisoria para un beneficiario sin cuil, con los movimientos")]
        public String SolicitudProvisoria_Alta(SolicitudProvisoria iSolicitudProvisoria)
        {
            String newNroSolicitud;
            SolicitudesDatos objdao = new SolicitudesDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                objdao.SolicitudProvisoria_Alta(iSolicitudProvisoria, out newNroSolicitud);
                return newNroSolicitud;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }


        [WebMethod(Description = "Ingresa la lista de movimientos de una solicitud provisoria")]
        public void SolicitudesProvisoriaMovimiento_Alta(List<SolicitudProvisoriaMovimiento> iMovimientosSolicitudProvisoria)
        {
            SolicitudesDatos objdao = new SolicitudesDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                objdao.SolicitudesProvisoriaMovimiento_Alta(iMovimientosSolicitudProvisoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }



        [WebMethod(Description = "Trae solicitudes provisorias por beneficiario")]
        public List<Beneficiario_SolicitudProvisoria> SolicitudProvisoria_TraeXIdBeneficiario(Int64 idBeneficiario)
        {
            SolicitudesDatos objdao = new SolicitudesDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.SolicitudProvisoria_TraeXIdBeneficiario(idBeneficiario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "Trae solicitud provisoria x solicitud provisoria")]
        public SolicitudProvisoria TraeSolicitudProvisoriaXNroSolicitudProvisoria(String NroSolicitudProvisoria)
        {
            SolicitudesDatos objdao = new SolicitudesDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeSolicitudProvisoriaXNroSolicitudProvisoria(NroSolicitudProvisoria);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "Trae solicitudes provisorias x año y mes de ingreso")]
        public List<SolicitudProvisoria> TraeSolicitudesProvisorias(String anio, String mes, Int16? codPais, Int16? codPrestacion, Boolean soloProvisorias, Int32 diasPlazoCaducidad)
        {
            SolicitudesDatos objdao = new SolicitudesDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeSolicitudesProvisorias(anio, mes, codPais, codPrestacion, soloProvisorias, diasPlazoCaducidad);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

        [WebMethod(Description = "Trae solicitudes provisorias a vencerse en plazo")]
        public List<SolicitudProvisoriaExtendida> TraeSolicitudesProvisoriasAVencerEnPlazo(Int32 diasPlazoCaducidad, Int32 plazoaDiasVencimiento)
        {
            SolicitudesDatos objdao = new SolicitudesDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraeSolicitudesProvisoriasAVencerEnPlazo(diasPlazoCaducidad, plazoaDiasVencimiento);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }



        #endregion Solictudes provisorias


    }


}