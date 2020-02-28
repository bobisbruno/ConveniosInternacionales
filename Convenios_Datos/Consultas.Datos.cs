using System;
using System.Collections.Generic;
using System.Text;
using NullableReaders;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Contrato;


namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Datos
{

    public class ConsultasDatos : Disposed
    {
        #region Dispose
        ~ConsultasDatos()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        /* Esto se declara a nivel general*/
        //private DbParameterCollection dbParametros = null;
        //private string sql = string.Empty;


        #region TraeNotificacionesVencidas
        public List<NotificacionesVencidas> TraeDevolucionesNotificadasVencidasXPlazo(Int64 PageNum, Int64 PageSize, out Int64 TotalRowsNum, Byte ordenPor, Int16 DiasPlazo)
        {
            List<NotificacionesVencidas> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeDevolucionesNotificadasVencidasXPlazo");
            
            try
            {
                NotificacionesVencidas oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@DiasPlazo", DbType.Int16, DiasPlazo);
                db.AddInParameter(dbCommand, "@PageNum", DbType.Int64, PageNum);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int64, PageSize);
                db.AddOutParameter(dbCommand, "@TotalRowsNum", DbType.Int64, int.MaxValue);
                db.AddInParameter(dbCommand, "@ordenPor", DbType.Byte, ordenPor);
                
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<NotificacionesVencidas>();
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oParam, ds);
                        /* Se carga el objeto en la lista que se va a devolver */
                        result.Add(oParam);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }
                TotalRowsNum = (Int64)db.GetParameterValue(dbCommand, "@TotalRowsNum");
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Consultas.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return result;
        }

        private void Fill(out NotificacionesVencidas oObj, NullableDataReader dr)
        {
            oObj = new NotificacionesVencidas(dr.GetInt64("id_Beneficiario")
                ,dr.GetString("NomApe")
                ,dr.GetInt16("codPrestacion")
                ,dr.GetString("DescripcionPrestacion")
                ,dr.GetDateTime("fechaMovimiento")
                ,dr.GetString("destino")
                ,dr.GetString("observaciones")
                ,dr.GetString("certificado")
                ,dr.GetNullableDateTime("fechaNotificacion")
                );
        }
        #endregion TraeNotificacionesVencidas

        #region TraeSolicitudesEFechasSolicitud
        public List<SolicitudesEFechasSolicitud> TraeSolicitudesEFechasSolicitud(String fechaDesde, String fechaHasta, Int16? codPrestacion, Int16? codPais, Boolean Mercosur, Byte orderBy)
        {
            List<SolicitudesEFechasSolicitud> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeSolicitudesEFechasSolicitud");

            try
            {
                SolicitudesEFechasSolicitud oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@fechaAMSolD", DbType.String, fechaDesde);
                db.AddInParameter(dbCommand, "@fechaAMSolH", DbType.String, fechaHasta);
                if(codPrestacion.HasValue)
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion.Value);
                else
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, null);
                if(codPais.HasValue)
                    db.AddInParameter(dbCommand, "@codPais", DbType.Int16, codPais.Value);
                else
                    db.AddInParameter(dbCommand, "@codPais", DbType.Int16, null);
                db.AddInParameter(dbCommand, "@mercosur", DbType.Boolean, Mercosur);
                db.AddInParameter(dbCommand, "@orden", DbType.Byte, orderBy);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<SolicitudesEFechasSolicitud>();
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oParam, ds);
                        /* Se carga el objeto en la lista que se va a devolver */
                        result.Add(oParam);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Consultas.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return result;
        }

        private void Fill(out SolicitudesEFechasSolicitud oObj, NullableDataReader dr)
        {
            oObj = new SolicitudesEFechasSolicitud(
                dr.GetInt64("id_Beneficiario")
                , dr.GetInt16("codPrestacion")
                , dr.GetString("ApeNomCompleto")
                , dr.GetNullableString("cuip") == null ? "" : dr.GetString("cuip")
                , dr.GetString("DescripcionPrestacion")
                , dr.GetInt16("pais_PK")
                , dr.GetString("PaisDescCompleto")
                , dr.GetBoolean("Mercosur")
                , dr.GetString("referencia_exterior")
                , dr.GetString("ubicacion_Fisica")
                , dr.GetDateTime("fAMSolicitud")
                , dr.GetDateTime("fechaIngreso")
                );
        }
        #endregion TraeSolicitudesEFechasSolicitud

        #region TraeUltimoEstadoYSectorSolicitud
        public void TraeUltimoEstadoYSectorSolicitud(Int64 idBeneficiario, Int16 codPrestacion, out Int32 codEstado, out Int32 codSector)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeUltimoMovimientoSolicitud");
            Movimiento_Solicitud result = null;

            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /* Se cargan los parámetros del store */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);
                    db.AddInParameter(dbCommand, "@cod_Prestacion", DbType.Int16, codPrestacion);
                    db.AddOutParameter(dbCommand, "@codSector", DbType.Int32, 0);
                    db.AddOutParameter(dbCommand, "@codEstado", DbType.Int32, 0);
                    db.AddOutParameter(dbCommand, "@modo", DbType.Byte, 2);
                    /* Se ejecuta el store */

                    db.ExecuteNonQuery(dbCommand);

                    codEstado = (Int32)db.GetParameterValue(dbCommand, "@codEstado");
                    codSector = (Int32)db.GetParameterValue(dbCommand, "@codSector");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Consultas.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion TraeUltimoEstadoYSectorSolicitud

        #region TraeMovimiento_Solicitud
        public Movimiento_Solicitud TraeUltimoMovimientoSolicitud(Int64 idBeneficiario, Int16 codPrestacion)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeUltimoMovimientoSolicitud");
            Movimiento_Solicitud result = null;

            try
            {
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);
                db.AddInParameter(dbCommand, "@cod_Prestacion", DbType.Int16, codPrestacion);
                db.AddOutParameter(dbCommand, "@codSector", DbType.Int32, 0);
                db.AddOutParameter(dbCommand, "@codEstado", DbType.Int32, 0);
                db.AddInParameter(dbCommand, "@modo", DbType.Byte, 1);  
                /* Se ejecuta el store */

                
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {

                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out result, ds);

                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Consultas.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return result;
        }

        private void Fill(out Movimiento_Solicitud  oObj, NullableDataReader dr)
        {
            Estado iEstado = new Estado(dr.GetInt32("cod_estado"), dr.GetString("descripcionEstado"));
            Sector iSector = new Sector(dr.GetInt32("cod_sector"), dr.GetString("descripcionSector"));
            oObj = new Movimiento_Solicitud(dr.GetDateTime("fecha_Movimiento")
                , iEstado
                , iSector
                , dr.GetNullableString("observaciones") == null ? "":dr.GetString("observaciones")
                );
        }
        #endregion TraeMovimiento_Solicitud

        #region TraeIndicadorPorSolicitudesPaisConvenio
        public List<IndicadorPorSolicitudesPaisConvenio> TraeIndicadorPorSolicitudesPaisConvenio(Byte criteriotemporal, Byte param_Temporal, String anio)
        {
            List<IndicadorPorSolicitudesPaisConvenio> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeIndicadorPorSolicitudesPaisConvenio");

            try
            {
                IndicadorPorSolicitudesPaisConvenio oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@criteriotemporal", DbType.Byte, criteriotemporal);
                db.AddInParameter(dbCommand, "@param_Temporal", DbType.Byte, param_Temporal);
                db.AddInParameter(dbCommand, "@anio", DbType.String, anio);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<IndicadorPorSolicitudesPaisConvenio>();
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oParam, ds);
                        /* Se carga el objeto en la lista que se va a devolver */
                        result.Add(oParam);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error en Consultas.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return result;
        }

        private void Fill(out IndicadorPorSolicitudesPaisConvenio oObj, NullableDataReader dr)
        {
            oObj = new IndicadorPorSolicitudesPaisConvenio(dr.GetString("totalPais")
                , dr.GetString("porcentualPais")
                , dr.GetInt16("pais_PK")
                , dr.GetString("DPais")
                );
        }
        #endregion TraeIndicadorPorSolicitudesPaisConvenio

        #region TraeIndicadorPorSolicitudesPrestaciones
        public List<IndicadorPorSolicitudesPrestaciones> TraeIndicadorPorSolicitudesPrestaciones(Byte criteriotemporal, Byte param_Temporal, String anio)
        {
            List<IndicadorPorSolicitudesPrestaciones> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeIndicadorPorSolicitudesPrestaciones");

            try
            {
                IndicadorPorSolicitudesPrestaciones oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@criteriotemporal", DbType.Byte, criteriotemporal);
                db.AddInParameter(dbCommand, "@param_Temporal", DbType.Byte, param_Temporal);
                db.AddInParameter(dbCommand, "@anio", DbType.String, anio);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<IndicadorPorSolicitudesPrestaciones>();
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oParam, ds);
                        /* Se carga el objeto en la lista que se va a devolver */
                        result.Add(oParam);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error en Consultas.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return result;
        }

        private void Fill(out IndicadorPorSolicitudesPrestaciones oObj, NullableDataReader dr)
        {
            oObj = new IndicadorPorSolicitudesPrestaciones(dr.GetString("totalPrestacion")
                , dr.GetString("porcentualPrestacion")
                , dr.GetInt16("codPrestacion")
                , dr.GetString("DPrestacion")
                );
        }
        #endregion TraeIndicadorPorSolicitudesPrestaciones


        #region TraeIndicadorTotalesEstadoAFechaX
        public List<IndicadorTotalesEstado> TraeIndicadorTotalesEstadoAFechaX(String Fecha)
        {
            List<IndicadorTotalesEstado> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeIndicadorTotalesEstadoAFechaX");

            try
            {
                IndicadorTotalesEstado oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@fechaX", DbType.String, Fecha);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<IndicadorTotalesEstado>();
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oParam, ds);
                        /* Se carga el objeto en la lista que se va a devolver */
                        result.Add(oParam);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error en Consultas.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return result;
        }

        private void Fill(out IndicadorTotalesEstado oObj, NullableDataReader dr)
        {
            oObj = new IndicadorTotalesEstado(dr.GetString("totalEstado")
                , dr.GetString("porcentualEstado")
                , dr.GetInt32("cod_estado")
                , dr.GetString("Destado")
                );
        }
        #endregion TraeIndicadorPorSolicitudesPrestaciones

        #region TraeIndicadorTotalesSectorAFechaX
        public List<IndicadorTotalesSector> TraeIndicadorTotalesSectorAFechaX(String Fecha)
        {
            List<IndicadorTotalesSector> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeIndicadorTotalesSectorAFechaX");

            try
            {
                IndicadorTotalesSector oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@fechaX", DbType.String, Fecha);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<IndicadorTotalesSector>();
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oParam, ds);
                        /* Se carga el objeto en la lista que se va a devolver */
                        result.Add(oParam);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error en Consultas.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return result;
        }

        private void Fill(out IndicadorTotalesSector oObj, NullableDataReader dr)
        {
            oObj = new IndicadorTotalesSector(dr.GetString("totalSector")
                , dr.GetString("porcentualSector")
                , dr.GetInt32("cod_sector")
                , dr.GetString("Dsector")
                );
        }
        #endregion TraeIndicadorPorSolicitudesPrestaciones


    }
}