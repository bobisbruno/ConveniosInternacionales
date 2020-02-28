using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security.Principal;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Negocio;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Datos;

namespace Ar.Gov.Anses.Microinformatica.AnsesConveniosInternacionalesX5.Servicios
{
    /// <summary>
    /// Brinda servicios de Consulta
    /// </summary>
    [WebService(Namespace = "http://AnsesConveniosInternacionalesX5.anses.gov.ar")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class ConsultasWS : System.Web.Services.WebService
    {

        public ConsultasWS()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod(Description = "Trae Devoluciones Notificadas Vencidas por Plazo")]
        public List<NotificacionesVencidas> TraeDevolucionesNotificadasVencidasXPlazo(Int64 PageNum, Int64 PageSize, out Int64 TotalRowsNum, Byte ordenPor, Int16 DiasPlazo)
        {
            List<NotificacionesVencidas> oResult = null;

            ConsultasDatos objDao = new ConsultasDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                oResult = objDao.TraeDevolucionesNotificadasVencidasXPlazo(PageNum, PageSize, out TotalRowsNum, ordenPor, DiasPlazo);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }
            return oResult;

        }

        [WebMethod(Description = "Trae solicitudes entre fechas de solicitud")]
        public List<SolicitudesEFechasSolicitud> TraeSolicitudesEFechasSolicitud(String fechaDesde, String fechaHasta, Int16? codPrestacion, Int16? codPais, Boolean Mercosur, Byte orderBy)
        {
            List<SolicitudesEFechasSolicitud> oResult = null;

            ConsultasDatos objDao = new ConsultasDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                oResult = objDao.TraeSolicitudesEFechasSolicitud( fechaDesde, fechaHasta, codPrestacion, codPais, Mercosur, orderBy);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }
            return oResult;

        }

        [WebMethod(Description = "Trae ultimos codigos de estado y Sector de una solicitud")]
        public Int32 TraeUltimoEstadoYSectorSolicitud(Int64 idBeneficiario, Int16 codPrestacion, out Int32 codEstado)
        {
            ConsultasDatos objDao = new ConsultasDatos();
            Int32 codSector;
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                objDao.TraeUltimoEstadoYSectorSolicitud(idBeneficiario, codPrestacion, out codEstado, out codSector);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }
            return codSector;
        }

        [WebMethod(Description = "Trae ultimo movimiento de solicitud")]
        public Movimiento_Solicitud TraeUltimoMovimientoSolicitud(Int64 idBeneficiario, Int16 codPrestacion)
        {
            Movimiento_Solicitud oResult = null;

            ConsultasDatos objDao = new ConsultasDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                oResult = objDao.TraeUltimoMovimientoSolicitud(idBeneficiario, codPrestacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }
            return oResult;
        }

        [WebMethod(Description = "Trae totales stock Indicador Por Solicitudes Pais - Convenio segun criterio temporal anual - semestral - trimestral o mensual para un anio")]
        public List<IndicadorPorSolicitudesPaisConvenio> TraeIndicadorPorSolicitudesPaisConvenio(Byte criteriotemporal, Byte param_Temporal, String anio)
        {
            List<IndicadorPorSolicitudesPaisConvenio> oResult = null;

            ConsultasDatos objDao = new ConsultasDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                oResult = objDao.TraeIndicadorPorSolicitudesPaisConvenio(criteriotemporal, param_Temporal, anio);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }
            return oResult;

        }


        [WebMethod(Description = "Trae totales stock Indicador Por Solicitudes Prestaciones segun criterio temporal anual - semestral - trimestral o mensual para un anio")]
        public List<IndicadorPorSolicitudesPrestaciones> TraeIndicadorPorSolicitudesPrestaciones(Byte criteriotemporal, Byte param_Temporal, String anio)
        {
            List<IndicadorPorSolicitudesPrestaciones> oResult = null;

            ConsultasDatos objDao = new ConsultasDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                oResult = objDao.TraeIndicadorPorSolicitudesPrestaciones(criteriotemporal, param_Temporal, anio);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }
            return oResult;

        }

        [WebMethod(Description = "Trae totales stock Indicador Por estado de solicitudes a una fecha")]
        public List<IndicadorTotalesEstado> TraeIndicadorTotalesEstadoAFechaX(String AFecha)
        {
            List<IndicadorTotalesEstado> oResult = null;

            ConsultasDatos objDao = new ConsultasDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                oResult = objDao.TraeIndicadorTotalesEstadoAFechaX(AFecha);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }
            return oResult;

        }

        [WebMethod(Description = "Trae totales stock Indicador Por sectores a una fecha")]
        public List<IndicadorTotalesSector> TraeIndicadorTotalesSectorAFechaX(String AFecha)
        {
            List<IndicadorTotalesSector> oResult = null;

            ConsultasDatos objDao = new ConsultasDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                oResult = objDao.TraeIndicadorTotalesSectorAFechaX(AFecha);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }
            return oResult;

        }
    }
}