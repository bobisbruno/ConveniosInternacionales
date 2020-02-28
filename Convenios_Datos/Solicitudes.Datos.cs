using System;
using System.Collections.Generic;
using NullableReaders;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Contrato;
//using LoggingAnses.Servicio;
////using LoggingAnses.Servicio.Comun;
//using LoggingAnses.Servicio.Entidad;


namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Datos
{

    public class MovimientosDatos : Disposed
    { 
        #region Dispose
        ~MovimientosDatos()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        /* clase interna para mapear cada movimiento por completo
         * */
        internal class fill
        {
            /**
         * 31/05/2013 Se quita Devolucion e ingreso del mapeo ya que se las obtiene desde sus clases particulares por fecha
         * **/

            //private void Fill(out Movimiento_Solicitud oObj, NullableDataReader dr)
            //{
            //    Estado oEstado = new Estado(dr.GetInt32("cod_estado"), dr.GetString("descripcionEstado"));
            //    Sector oSector = new Sector(dr.GetInt32("cod_sector"), dr.GetString("descripcionSector"));
            //    DevolucionesDatos oDevolucionDao = new DevolucionesDatos();
            //    IngresosDatos oIngresoDao = new IngresosDatos();
                
            //    oObj = new Movimiento_Solicitud(dr.GetDateTime("fecha_Movimiento"),
            //        oEstado,
            //        oSector,
            //        dr.GetNullableString("observaciones") == null  ? "" : dr.GetString("observaciones"),
            //        oDevolucionDao.TraeDevolucionXMovimientoSolicitud(dr.GetInt64("id_Beneficiario"),dr.GetInt16("codPrestacion"), dr.GetDateTime("fecha_Movimiento").ToShortDateString()),
            //        oIngresoDao.TraeIngresoXMovimientoSolicitud(dr.GetInt64("id_Beneficiario"), dr.GetInt16("codPrestacion"), dr.GetDateTime("fecha_Movimiento").ToShortDateString())
            //        );
            //}

            private void Fill(out Movimiento_Solicitud oObj, NullableDataReader dr)
            {
                Estado oEstado = new Estado(dr.GetInt32("cod_estado"), dr.GetString("descripcionEstado"));
                Sector oSector = new Sector(dr.GetInt32("cod_sector"), dr.GetString("descripcionSector"));
                oObj = new Movimiento_Solicitud(dr.GetDateTime("fecha_Movimiento"),
                    oEstado,
                    oSector,
                    dr.GetNullableString("observaciones") == null ? "" : dr.GetString("observaciones")
                    );
            }
        }

        #region Trae Movimientos

        public Movimiento_Solicitud TraeMovimientoXFechaMovimiento(Int64 idBeneficiario, Int16 codPrestacion, String FechaMovimiento)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeMivimientosSolicitudXFechaMovimiento");

            Movimiento_Solicitud oParam;
            try
            {

                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@idBeneficiario", DbType.Int64, idBeneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                db.AddInParameter(dbCommand, "@fechaMovimiento", DbType.String, FechaMovimiento);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {

                    ds.Read();
                    this.Fill(out oParam, ds);
                    ds.Close();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return oParam;
        }

        public List<Movimiento_Solicitud> TraeMovimientosXSolicitud(Int64 idBeneficiario, Int16 codPrestacion)
        {
            List<Movimiento_Solicitud> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeMivimientosSolicitudXIdBeneficiario_codPrestacion");

            try
            {
                Movimiento_Solicitud oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@idBeneficiario", DbType.Int64, idBeneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Movimiento_Solicitud>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        public List<IngDevMov> TraeMovimientosResumen(Int64 idBeneficiario, Int16 codPrestacion)
        {
            List<IngDevMov> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeIDMXSolicitud");

            try
            {
                IngDevMov oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@idBeneficiario", DbType.Int64, idBeneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<IngDevMov>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion

        private void Fill(out Movimiento_Solicitud oObj, NullableDataReader dr)
        {
            Estado oEstado = new Estado(dr.GetInt32("cod_estado"), dr.GetString("descripcionEstado"));
            Sector oSector = new Sector(dr.GetInt32("cod_sector"), dr.GetString("descripcionSector"));
            oObj = new Movimiento_Solicitud(dr.GetDateTime("fecha_Movimiento"),
                oEstado,
                oSector,
                dr.GetNullableString("observaciones") == null  ? "" : dr.GetString("observaciones")
                );
        }

        private void Fill(out IngDevMov oObj, NullableDataReader dr)
        {
            oObj = new IngDevMov(dr.GetInt64("id_Beneficiario"),
                dr.GetInt16("codPrestacion"),
                dr.GetDateTime("FechaMovimiento"),
                dr.GetString("TipoMovimiento"),
                dr.GetString("TipoIngreso"),
                dr.GetString("Destino"),
                dr.GetString("Estado"),
                dr.GetString("Sector")
                );
        }

        #region Transacciones
        #region Alta de Ingreso
        public void AltaIngreso(Int64 id_Beneficiario, Int16 codPrestacion, String fIngreso, Byte? idTipoIngreso, List<TipoDocumentacion> iListTipoDocumentacion, String observacionIng)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AltaIngreso");
            DbCommand dbCommand2 = db.GetStoredProcCommand("AltaIngresadosMovimiento");
            DbCommand dbCommand3 = db.GetStoredProcCommand("ModificaDevolucion_SetFPresentacion");
            
            //DocRepetida = new List<string>();

            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de ingreso
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                    db.AddInParameter(dbCommand, "@idTipoIngreso", DbType.Byte, idTipoIngreso);
                    //db.AddInParameter(dbCommand, "@FechaMovimiento", DbType.DateTime, fechaMovimiento);
                    db.AddInParameter(dbCommand, "@fechaIngreso", DbType.DateTime, DateTime.Parse(fIngreso));
                    db.AddInParameter(dbCommand, "@observacion", DbType.String, observacionIng.Equals(string.Empty)?null:observacionIng);
                    
                    
                    db.ExecuteNonQuery(dbCommand);
                    db.DiscoverParameters(dbCommand);

                    /**
                     * Alta de documentacion de ingreso
                     * */
                    db.AddInParameter(dbCommand2, "@id_Beneficiario", DbType.Int64);
                    db.AddInParameter(dbCommand2, "@codPrestacion", DbType.Int16);
                    db.AddInParameter(dbCommand2, "@codTipoDocumentacion", DbType.Byte);
                    db.AddInParameter(dbCommand2, "@FechaIngreso", DbType.DateTime);
                    

                    foreach (TipoDocumentacion tDoc in iListTipoDocumentacion)
                    {
                        db.SetParameterValue(dbCommand2, "@id_Beneficiario", id_Beneficiario);
                        db.SetParameterValue(dbCommand2, "@codPrestacion", codPrestacion);
                        db.SetParameterValue(dbCommand2, "@codTipoDocumentacion", tDoc.CodTipoDocumentacion);
                        db.SetParameterValue(dbCommand2, "@FechaIngreso", DateTime.Parse(fIngreso));
                        
                        db.ExecuteNonQuery(dbCommand2);

                        //msj = (string)db.GetParameterValue(dbCommand2, "@mensaje");
                        //if (!msj.Equals(string.Empty))
                        //    DocRepetida.Add(msj);
                    }

                    /**
                     * Alta de fecha presentacion documentacion
                     * */

                    DateTime fechaPresentacion = DateTime.Parse(System.DateTime.Today.ToShortDateString());
                    db.DiscoverParameters(dbCommand2);

                    db.AddInParameter(dbCommand3, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                    db.AddInParameter(dbCommand3, "@codPrestacion", DbType.Int16, codPrestacion);
                    //db.AddInParameter(dbCommand3, "@fechaMovimiento", DbType.String, fechaMovimiento);
                    db.AddInParameter(dbCommand3, "@fechaPresentacion", DbType.String, fechaPresentacion);

                    db.ExecuteNonQuery(dbCommand3);

                    oScope.Complete();
                }
                //graba rutina del log
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString()+codPrestacion.ToString(),
                //    Datos = iListTipoDocumentacion,
                //    Tabla = "Ingresos",
                //    TipoAccion = TipoAction.AGREGAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Movimientos.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion Alta de Ingreso

        #region Alta de Devolucion
        public void AltaDevolucion(Int64 id_Beneficiario, Int16 codPrestacion, String destino, String observaciones, String certificado, List<TipoDocumentacion> iListTipoDocumentacion)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AltaDevolucion");
            DbCommand dbCommand2 = db.GetStoredProcCommand("AltaFaltantes_Devolucion");
            DateTime fechaMovimiento = DateTime.Parse(System.DateTime.Today.ToShortDateString());

            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Devolucion
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                    db.AddInParameter(dbCommand, "@destino", DbType.String, destino.Equals(string.Empty)?null:destino);
                    db.AddInParameter(dbCommand, "@observaciones", DbType.String, observaciones.Equals(string.Empty)?null:observaciones);
                    db.AddInParameter(dbCommand, "@certificado", DbType.String, certificado.Equals(string.Empty)?null:certificado);
                    db.AddInParameter(dbCommand, "@FechaMovimiento", DbType.DateTime, fechaMovimiento);

                    db.ExecuteNonQuery(dbCommand);

                    db.DiscoverParameters(dbCommand);
                    /**
                     * Alta de documentacion de devolucion
                     * */
                    db.AddInParameter(dbCommand2, "@id_Beneficiario", DbType.Int64);
                    db.AddInParameter(dbCommand2, "@codPrestacion", DbType.Int16);
                    db.AddInParameter(dbCommand2, "@codTipoDocumentacion", DbType.Byte);
                    db.AddInParameter(dbCommand2, "@FechaMovimiento", DbType.DateTime);
                    
                    
                    foreach (TipoDocumentacion tDoc in iListTipoDocumentacion)
                    {
                        db.SetParameterValue(dbCommand2, "@id_Beneficiario", id_Beneficiario);
                        db.SetParameterValue(dbCommand2, "@codPrestacion", codPrestacion);
                        db.SetParameterValue(dbCommand2, "@codTipoDocumentacion", tDoc.CodTipoDocumentacion);
                        db.SetParameterValue(dbCommand2, "@FechaMovimiento", fechaMovimiento);
                        
                        db.ExecuteNonQuery(dbCommand2);
                    }

                    //db.DiscoverParameters(dbCommand2);


                    oScope.Complete();
                }
                //graba rutina del log
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString() + codPrestacion.ToString(),
                //    Datos = iListTipoDocumentacion,
                //    Tabla = "Devoluciones",
                //    TipoAccion = TipoAction.AGREGAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Movimientos.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion Alta

        #region Alta de Movimiento
         
        public void AltaMovimiento(Int64 id_Beneficiario, Int16 codPrestacion, Int32 codEstado, Int32 codsector, String observaciones)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AltaMovimiento");

            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Beneficiario
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                    db.AddInParameter(dbCommand, "@cod_estado", DbType.Int32, codEstado);
                    db.AddInParameter(dbCommand, "@cod_sector", DbType.Int32, codsector);
                    db.AddInParameter(dbCommand, "@observaciones", DbType.String, observaciones.Equals(string.Empty)?null:observaciones);

                    db.ExecuteNonQuery(dbCommand);

                    oScope.Complete();
                }
                //graba rutina del log
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString() + codPrestacion.ToString(),
                //    Datos = codEstado.ToString()+codsector.ToString()+observaciones,
                //    Tabla = "Movimientos_Solicitud",
                //    TipoAccion = TipoAction.AGREGAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Movimientos.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion Alta

        #region NotificaDevolucion
        public void NotificaDevolucion(Int64 id_Beneficiario, Int16 codPrestacion, String fechaMovimiento, String fechaNotificacion)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("NotificaDevolucion");

            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Beneficiario
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                    db.AddInParameter(dbCommand, "@fechaMovimiento", DbType.String, fechaMovimiento);
                    db.AddInParameter(dbCommand, "@fechaNotificacion", DbType.String, fechaNotificacion);
                    
                    db.ExecuteNonQuery(dbCommand);

                    oScope.Complete();
                }
                //graba rutina del log
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString() + codPrestacion.ToString(),
                //    Datos = fechaMovimiento+fechaNotificacion,
                //    Tabla = "Devoluciones",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Movimientos.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion 

        #region ModificaDevolucion_SetFPresentacion
        public void ModificaDevolucion_SetFPresentacion(Int64 id_Beneficiario, Int16 codPrestacion, String fechaMovimiento, String fechaPresentacion)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("ModificaDevolucion_SetFPresentacion");

            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Beneficiario
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                    db.AddInParameter(dbCommand, "@fechaMovimiento", DbType.String, fechaMovimiento);
                    db.AddInParameter(dbCommand, "@fechaPresentacion", DbType.String, fechaPresentacion);

                    db.ExecuteNonQuery(dbCommand);

                    oScope.Complete();
                }
                //graba rutina del log
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = id_Beneficiario.ToString() + codPrestacion.ToString(),
                //    Datos = fechaMovimiento + fechaPresentacion,
                //    Tabla = "Devoluciones",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Movimientos.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion

        #endregion Transacciones
    }

    public class SolicitudesDatos : Disposed
    {
        #region Dispose
        ~SolicitudesDatos()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        /* Esto se declara a nivel general*/
        //private DbParameterCollection dbParametros = null;
        //private string sql = string.Empty;

        #region TraePrestacionesNoIngresadasXIdBeneficiario
        public List<Prestacion> TraePrestacionesNoIngresadasXIdBeneficiario(Int64 idBeneficiario)
        {
            List<Prestacion> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraePrestacionesNoIngresadasXBeneficiario");

            try
            {
                Prestacion oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@idBeneficiario", DbType.Int64, idBeneficiario);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Prestacion>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        private void Fill(out Prestacion oObj, NullableDataReader dr)
        {
            //List<TipoDocumentacion> iLista = traer
            oObj = new Prestacion(dr.GetInt16("codPrestacion"),
                dr.GetNullableString("descripcion") == null ? "" : dr.GetString("descripcion")
                );
        }
        #endregion TraePrestacionesXIdBeneficiario
        
        #region TraePrestacionesXIdBeneficiario
        public List<PrestacionBeneficiario> TraePrestacionesXIdBeneficiario(Int64 idBeneficiario)
        {
            List<PrestacionBeneficiario> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraePrestacionesXIdBeneficiario");

            try
            {
                PrestacionBeneficiario oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@idBeneficiario", DbType.Int64, idBeneficiario);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<PrestacionBeneficiario>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion TraePrestacionesXIdBeneficiario

        #region SolicitudProvisoriaMovimiento_Alta



        private void SolicitudProvisoriaMovimiento_Alta(SolicitudProvisoriaMovimiento iSolicitudProvisoriaMovimiento)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("SolicitudProvisoriaMovimiento_Alta");
            try
            {
                db.AddInParameter(dbCommand, "@nroSolicitudProvisoria", DbType.String, iSolicitudProvisoriaMovimiento.Nro_SolicitudProvisoria);
                db.AddInParameter(dbCommand, "@codTipoDocumentacion", DbType.Int32, iSolicitudProvisoriaMovimiento.TipoDocumentacion.CodTipoDocumentacion);
                db.AddInParameter(dbCommand, "@digitalizado", DbType.Boolean, iSolicitudProvisoriaMovimiento.Digitalizado);
                db.AddInParameter(dbCommand, "@secuencia", DbType.Int16, iSolicitudProvisoriaMovimiento.SecuenciaDocumento);
                db.AddInParameter(dbCommand, "@descripcionBreve", DbType.String, iSolicitudProvisoriaMovimiento.DescripcionBreve.Equals(string.Empty) ? null : iSolicitudProvisoriaMovimiento.DescripcionBreve);
                
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
 
        }

        #endregion SolicitudProvisoriaMovimiento_Alta
        
        #region SolicitudProvisoriaMovimientos_Alta

        public void SolicitudesProvisoriaMovimiento_Alta(List<SolicitudProvisoriaMovimiento> iMovimientosSolicitudProvisoria)
        {
            if (iMovimientosSolicitudProvisoria != null)
            {
                foreach (SolicitudProvisoriaMovimiento aux in iMovimientosSolicitudProvisoria)
                {
                    aux.Nro_SolicitudProvisoria = aux.Nro_SolicitudProvisoria;
                    SolicitudProvisoriaMovimiento_Alta(aux);
                }
            }
        }

        #endregion SolicitudProvisoriaMovimientos_Alta

        #region SolicitudProvisoria_Alta

        public void SolicitudProvisoria_Alta(SolicitudProvisoria iSolicitudProvisoria, out String newNroSolicitud)
        {
            newNroSolicitud = string.Empty;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("SolicitudProvisoria_Alta");
            try
            {
                    db.AddInParameter(dbCommand, "@nombreDeclarado", DbType.String, iSolicitudProvisoria.ApellildoynombreDeclarado);
                    db.AddInParameter(dbCommand, "@documentoNumDeclarado", DbType.String, iSolicitudProvisoria.DocumentoDeclarado.Equals(string.Empty) ? null : iSolicitudProvisoria.DocumentoDeclarado.ToUpper());
                    db.AddInParameter(dbCommand, "@documentoTipoDeclarado", DbType.Int16, iSolicitudProvisoria.TipoDocumentoDeclarado);
                    
                    if(iSolicitudProvisoria.PrestacionSolicitada == null)
                        db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, null);
                    else
                        db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, iSolicitudProvisoria.PrestacionSolicitada.Cod_Prestacion);

                    db.AddInParameter(dbCommand, "@referencia_provisoria", DbType.String, iSolicitudProvisoria.Referencia_Provisoria);
                    db.AddInParameter(dbCommand, "@fAltaProvisoria", DbType.DateTime, iSolicitudProvisoria.FAltaProvisoria);
                    
                    db.AddInParameter(dbCommand, "@tipoIngreso", DbType.String, iSolicitudProvisoria.TIngresoProvisorio == TipoIngresoProvisorio.Ingreso ? "I" : "D");

                    if (iSolicitudProvisoria.Sectorderiva == null)
                        db.AddInParameter(dbCommand, "@sectorDerivacion", DbType.Int32, null);
                    else
                        db.AddInParameter(dbCommand, "@sectorDerivacion", DbType.Int32, iSolicitudProvisoria.Sectorderiva.Cod_sector);

                    db.AddOutParameter(dbCommand, "@nro_SolicitudProvisoria", DbType.String, 8);

                    db.ExecuteNonQuery(dbCommand);

                    newNroSolicitud = (String)db.GetParameterValue(dbCommand, "@nro_SolicitudProvisoria");
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
 
        }

        #endregion SolicitudProvisoria_Alta

        #region TraeSolicitudProvisoriaXNroSolicitudProvisoria
        public SolicitudProvisoria TraeSolicitudProvisoriaXNroSolicitudProvisoria(String NroSolicitudProvisoria)
        { 
            SolicitudProvisoria result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeSolicitudProvisoriaXNroSolicitudProvisoria");

            try
            {
                SolicitudProvisoria oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@nro_SolicitudProvisoria", DbType.String, NroSolicitudProvisoria);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new SolicitudProvisoria();
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oParam, ds);
                        /* Se carga el objeto en la lista que se va a devolver */
                        result  = oParam;
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion
        
        #region TraeSolicitudesProvisoriasXIdBeneficiario
        public List<Beneficiario_SolicitudProvisoria> SolicitudProvisoria_TraeXIdBeneficiario(Int64 idBeneficiario)
        {
            List<Beneficiario_SolicitudProvisoria> result = null;
            
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("SolicitudProvisoria_TraeXIdBeneficiario");

            try
            {
                SolicitudProvisoria oParam;
                Beneficiario_SolicitudProvisoria oParamFinal;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Beneficiario_SolicitudProvisoria>();
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oParam, ds);
                        /* Se carga el objeto en la lista que se va a devolver */
                        oParamFinal = new Beneficiario_SolicitudProvisoria(idBeneficiario, oParam);
                        result.Add(oParamFinal);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion TraePrestacionesXIdBeneficiario
        
        #region TraeSolicitudesProvisoriasXAnioymes
        public List<SolicitudProvisoria> TraeSolicitudesProvisorias(String anio, String mes, Int16? codPais, Int16? codPrestacion, Boolean soloProvisorias, Int32 plazoDiasCaducidad)
        {
            List<SolicitudProvisoria> result = null;
            
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeSolicitudesProvisorias");
            
            try
            {
                SolicitudProvisoria oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@anioIngreso", DbType.String, anio);

                if(mes.Equals("0"))
                    db.AddInParameter(dbCommand, "@mesIngreso", DbType.String, null);
                else
                    db.AddInParameter(dbCommand, "@mesIngreso", DbType.String, mes);

                if(codPais.HasValue)
                    db.AddInParameter(dbCommand, "@codPais", DbType.Int16, codPais.Value);
                else
                    db.AddInParameter(dbCommand, "@codPais", DbType.Int16, null);

                if(codPrestacion.HasValue)
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion.Value);
                else
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, null);
                    
                db.AddInParameter(dbCommand, "@soloProvisorias", DbType.Boolean, soloProvisorias);

                db.AddInParameter(dbCommand, "@plazoDiasCaducidad", DbType.Int32, plazoDiasCaducidad);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<SolicitudProvisoria>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion TraePrestacionesXIdBeneficiario

        #region TraeSolicitudesProvisoriasAVencerEnPlazo
        public List<SolicitudProvisoriaExtendida> TraeSolicitudesProvisoriasAVencerEnPlazo(Int32 plazoDiasCaducidad, Int32 plazoADiasVenvimiento)
        {
            List<SolicitudProvisoriaExtendida> result = null;

            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeSolicitudesProvisoriasAVencerEnPlazo");

            try
            {
                SolicitudProvisoriaExtendida oParam;
                /* Se cargan los parámetros del store */
                
                db.AddInParameter(dbCommand, "@plazoDiasCaducidad", DbType.Int32, plazoDiasCaducidad);
                db.AddInParameter(dbCommand, "@plazoADiasVenvimiento", DbType.Int32, plazoADiasVenvimiento);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<SolicitudProvisoriaExtendida>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion 

        #region SolicitudProvisoriaMovimiento_TraeXMovimiento

        private List<SolicitudProvisoriaMovimiento> SolicitudProvisoriaMovimiento_TraeXMovimiento(String nroSolicitudProvisoria)
        {
            List<SolicitudProvisoriaMovimiento> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("SolicitudProvisoriaMovimiento_TraeXMovimiento");

            try
            {
                SolicitudProvisoriaMovimiento oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@nroSolicitudProvisoria", DbType.String, nroSolicitudProvisoria);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<SolicitudProvisoriaMovimiento>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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


        #endregion SolicitudProvisoriaMovimiento_TraeXMovimiento

        #region Trae Solicitudes por beneficiario
        public List<Solicitud> TraeSolicitudesXIdBenefPrestac(Int64 idBeneficiario, Int16 codPrestacion)
        {
            List<Solicitud> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeSolicitudesXIdBenefPrestac");

            try
            {
                Solicitud oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@idBeneficiario", DbType.Int64, idBeneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Solicitud>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion Trae Solicitudes por beneficiario

        #region Trae Documentacion faltante por solicitud
        public List<TipoDocumentacion_Prestacion> TraeTipoDocumentacionFaltanteXSolicitud(Int64 idBeneficiario, Int16 codPrestacion)
        {
            List<TipoDocumentacion_Prestacion> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTipoDocumentacionFaltanteXSolicitud");

            try
            {
                TipoDocumentacion_Prestacion oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@idBeneficiario", DbType.Int64, idBeneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<TipoDocumentacion_Prestacion>();
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
                throw new Exception("Error en Auxiliares.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        public void Fill(out TipoDocumentacion_Prestacion oObj, NullableDataReader dr)
        {
            oObj = new TipoDocumentacion_Prestacion(new Prestacion(dr.GetInt16("codPrestacion"), dr.GetString("descripcionprestacion")),
                new TipoDocumentacion(dr.GetInt32("codTipoDocumentacion"), dr.GetString("descripcionTipoDocumantacion")),
                dr.GetBoolean("requeridoInicioTramite"),
                dr.GetNullableString("comentario") == null ? "" : dr.GetString("comentario")
                );
        }

        #endregion Trae Documentacion faltante por solicitud

        #region Trae listado de Expedientes por Solicitud
        public List<Expediente_Solicitud> TraeExpedientesXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion)
        {
            List<Expediente_Solicitud> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeExpedientesXSolicitud");

            try
            {
                Expediente_Solicitud oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                db.AddInParameter(dbCommand, "@cod_Prestacion", DbType.Int16, codPrestacion);


                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Expediente_Solicitud>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        #endregion Trae
        
        #region Trae listado de Beneficios por Solicitud
        public List<Beneficio_Solicitud> TraeBeneficiosXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion)
        {
            List<Beneficio_Solicitud> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBeneficiosXSolicitud");

            try
            {
                Beneficio_Solicitud oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                db.AddInParameter(dbCommand, "@cod_Prestacion", DbType.Int16, codPrestacion);


                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Beneficio_Solicitud>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        #endregion Trae

        #region Trae listado de Solicitudes denegadas
        public List<SolicitudDenegada> TraeSolicitudesDenegadasXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion)
        {
            List<SolicitudDenegada> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeSolicitudesDenegadasXSolicitud");

            try
            {
                SolicitudDenegada oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);


                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<SolicitudDenegada>();
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
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        public void Fill(out SolicitudDenegada oObj, NullableDataReader dr)
        {
            oObj = new SolicitudDenegada(dr.GetInt64("id_Beneficiario"),
                dr.GetInt16("codPrestacion"),
                dr.GetDateTime("fechaDenegatoria"),
                dr.GetNullableDateTime("fechaBajaDenegatoria"),
                dr.GetInt16("codMotivo"),
                dr.GetString("DescMotivo"),
                dr.GetNullableString("observaciones") == null ? "" : dr.GetString("observaciones")
                );
        }

        #endregion Trae
        
        #region AMAllDatosSolicitud

        public void AMAllDatosSolicitud(Int64 idBenef, Int16 codPrestacion, Int16 codPais, List<Solicitud> ilSolicitud, List<Expediente_Solicitud> ilExpediente, List<Beneficio_Solicitud> ilBeneficio, List<Ingresos> iLingresos, List<Devolucion> iLdevolucion, List<Movimiento_Solicitud> ilMovimientos)
        {
            try
            {
                MovimientosDatos objDao = new MovimientosDatos();
                //using (TransactionScope oScope = new TransactionScope())
                //{
                    /**
                     * Alta de solicitudes
                     * */
                    
                    //Agrega o modifica solicitudes a la solicitud
                if (ilSolicitud != null)
                {
                    foreach (Solicitud isol in ilSolicitud)
                        AMSolicitud(isol);
                }
                else //verifica si no existe alguna solicitud anterior
                {
                    ilSolicitud = TraeSolicitudesXIdBenefPrestac(idBenef, codPrestacion);
                    if (ilSolicitud == null || ilSolicitud.Count == 0) //no existen solicitudes anteriores
                    {
                        ilSolicitud = new List<Solicitud>();
                        Solicitud iSol = new Solicitud();
                        iSol.IdBeneficiario = idBenef;
                        iSol.CodPrestacion = codPrestacion;
                        iSol.CodigoPais = codPais;
                        iSol.CodMotivo = null;
                        iSol.DescripcionMotivo = string.Empty;
                        iSol.FAMSolicitud = System.DateTime.Today;
                        iSol.FechaIngreso = null;
                        iSol.Mercosur = false;
                        iSol.Observaciones = string.Empty;
                        iSol.PaisDescCompleto = string.Empty;
                        iSol.Referencia_exterior = string.Empty;
                        iSol.Ubicacion_Fisica = string.Empty;
                        iSol.DescripcionPrestacion = string.Empty;
                        AMSolicitud(iSol);
                    }
 
                }

                //Agrega o modifica expedientes
                if (ilExpediente != null)
                {
                    foreach(Expediente_Solicitud iExpe in ilExpediente)
                        AMExpedientesSolicitud(idBenef, codPrestacion, iExpe);
                }

                //Agrega o modifica beneficios a la solicitud
                if (ilBeneficio != null)
                {
                    foreach (Beneficio_Solicitud iBenef in ilBeneficio)
                        AMBeneficiosSolicitud( idBenef, codPrestacion, iBenef);
                }

                //Agrega o modifica ingresos a la solicitud
                if (iLingresos != null)
                {
                    foreach (Ingresos iingr in iLingresos)
                        objDao.AltaIngreso(idBenef, codPrestacion, iingr.FechaIngreso.HasValue ? iingr.FechaIngreso.Value.ToShortDateString() : "", iingr.TipoIngreso.IdTipoIngreso, iingr.LTipoDocumentacion, iingr.Observacion);
                }

                //Agrega o modifica beneficios a la solicitud
                if (iLdevolucion != null)
                {
                    foreach (Devolucion idev in iLdevolucion)
                        objDao.AltaDevolucion(idBenef, codPrestacion, idev.Destino, idev.Observaciones, idev.Certificado, idev.TipoDocumentacionFaltante.LTipoDocumentacionFaltante);
                }

                //Agrega o modifica beneficios a la solicitud
                if (ilMovimientos != null)
                {
                    foreach (Movimiento_Solicitud imov in ilMovimientos)
                        objDao.AltaMovimiento(idBenef, codPrestacion, imov.Estado.Cod_estado, imov.Sector.Cod_sector, imov.Observaciones);
                }

                  //  oScope.Complete();
                //}
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = iSolicitud.IdBeneficiario.ToString()+iSolicitud.Prestacion.Cod_Prestacion.ToString(),
                //    Datos = iSolicitud,
                //    Tabla = "Solicitudes",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                
            }
        }

        #endregion AMAllDatosSolicitud

        #region AMSolicitud

        private void AMSolicitud(Solicitud iSolicitud)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMSolicitud");
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de solicitud
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, iSolicitud.IdBeneficiario);
                    db.AddInParameter(dbCommand, "@cod_Prestacion", DbType.Int16, iSolicitud.CodPrestacion);
                    db.AddInParameter(dbCommand, "@codPais", DbType.Int16, iSolicitud.CodigoPais);
                    db.AddInParameter(dbCommand, "@mercosur", DbType.Boolean, iSolicitud.Mercosur);
                    db.AddInParameter(dbCommand, "@referencia_exterior", DbType.String, iSolicitud.Referencia_exterior.Equals(string.Empty)? null:iSolicitud.Referencia_exterior);
                    db.AddInParameter(dbCommand, "@ubicacion_Fisica", DbType.String, iSolicitud.Ubicacion_Fisica.Equals(string.Empty)?null:iSolicitud.Ubicacion_Fisica);
                    db.AddInParameter(dbCommand, "@fechaIngreso", DbType.DateTime, iSolicitud.FechaIngreso.HasValue? iSolicitud.FechaIngreso:System.DateTime.Today);
                    if(iSolicitud.CodMotivo.HasValue)
                        db.AddInParameter(dbCommand, "@codMotivoDeniega", DbType.Int16, iSolicitud.CodMotivo.Value);
                    else
                        db.AddInParameter(dbCommand, "@codMotivoDeniega", DbType.Int16, null);
                    db.AddInParameter(dbCommand, "@observaciones", DbType.String, iSolicitud.Observaciones.Equals(string.Empty)?null:iSolicitud.Observaciones);
                    
                    db.ExecuteNonQuery(dbCommand);

                    
                    oScope.Complete();
                }
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = iSolicitud.IdBeneficiario.ToString()+iSolicitud.Prestacion.Cod_Prestacion.ToString(),
                //    Datos = iSolicitud,
                //    Tabla = "Solicitudes",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }

        #endregion AMSolicitud

        #region Baja solicitud (logiva)

        public void BajaSolicitud(Int64 idBeneficiario, Int16 codPrestacion)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("BajaSolicitud");
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de solicitud
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);
                    db.AddInParameter(dbCommand, "@cod_Prestacion", DbType.Int16, codPrestacion);
                    
                    db.ExecuteNonQuery(dbCommand);
                    oScope.Complete();
                }
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = iSolicitud.IdBeneficiario.ToString()+iSolicitud.Prestacion.Cod_Prestacion.ToString(),
                //    Datos = iSolicitud,
                //    Tabla = "Solicitudes",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }

        #endregion BajaSolicitud
        
        #region AM Denegatoria Solicitud
        //public void AMSolicitudesDenegadas(Int64 idBeneficiario, Int16 codPrestacion, Int16 codMotivo, String observaciones, Boolean habilitaSolicitud)
        ////public void AMSolicitudesDenegadas(SolicitudDenegada iParam, bool habilita)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
        //    /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
        //    DbCommand dbCommand = db.GetStoredProcCommand("AMSolicitudesDenegadas");

        //    //LogAplicaciones logging = new LogAplicaciones();
        //    try
        //    {
        //        using (TransactionScope oScope = new TransactionScope())
        //        {
        //            /**
        //             * Alta de Beneficiario
        //             * */
        //            db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);
        //            db.AddInParameter(dbCommand, "@cod_Prestacion", DbType.Int16, codPrestacion);
        //            db.AddInParameter(dbCommand, "@codMotivo", DbType.Int16, codMotivo);
        //            db.AddInParameter(dbCommand, "@observaciones", DbType.String, observaciones);
        //            db.AddInParameter(dbCommand, "@habilita", DbType.String, habilitaSolicitud);
                    
        //            db.ExecuteNonQuery(dbCommand);

        //            oScope.Complete();
        //        }
        //        //logging.Log(new OnlineLog
        //        //{
        //        //    ClavePrincipal = idBeneficiario.ToString() + codPrestacion.ToString(),
        //        //    Datos = iExpediente_Solicitud,
        //        //    Tabla = "Expedientes_Solicitud",
        //        //    TipoAccion = TipoAction.ACTUALIZAR
        //        //});    
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
        //    }
        //    finally
        //    {
        //        db = null;
        //        dbCommand.Dispose();
        //    }
        //}
        #endregion

        #region AMExpedientesSolicitud
        private void AMExpedientesSolicitud(Int64 idBeneficiario, Int16 codPrestacion, Expediente_Solicitud iExpediente_Solicitud)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMExpedienteSolicitud");

            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Beneficiario
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);
                    db.AddInParameter(dbCommand, "@cod_Prestacion", DbType.Int16, codPrestacion);
                    db.AddInParameter(dbCommand, "@fAltaexpediente", DbType.String, iExpediente_Solicitud.FAltaexpediente.ToShortDateString());
                    db.AddInParameter(dbCommand, "@org", DbType.String, iExpediente_Solicitud.Expediente_org);
                    db.AddInParameter(dbCommand, "@precu", DbType.String, iExpediente_Solicitud.Expediente_precu);
                    db.AddInParameter(dbCommand, "@doccu", DbType.String, iExpediente_Solicitud.Expediente_doccu);
                    db.AddInParameter(dbCommand, "@digcu", DbType.String, iExpediente_Solicitud.Expediente_digcu);
                    db.AddInParameter(dbCommand, "@ctipo", DbType.String, iExpediente_Solicitud.Expediente_ctipo);
                    db.AddInParameter(dbCommand, "@sec", DbType.String, iExpediente_Solicitud.Expediente_sec);
                    db.AddInParameter(dbCommand, "@caratula", DbType.String, iExpediente_Solicitud.Caratula);
                    db.AddInParameter(dbCommand, "@observacion", DbType.String, iExpediente_Solicitud.Observacion);

                    db.ExecuteNonQuery(dbCommand);

                    oScope.Complete();
                }
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = idBeneficiario.ToString() + codPrestacion.ToString(),
                //    Datos = iExpediente_Solicitud,
                //    Tabla = "Expedientes_Solicitud",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion

        #region AMBeneficiosSolicitud
        private void AMBeneficiosSolicitud(Int64 idBeneficiario, Int16 codPrestacion, Beneficio_Solicitud iBeneficio_Solicitud)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMBeneficioSolicitud");

            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Beneficiario
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);
                    db.AddInParameter(dbCommand, "@cod_Prestacion", DbType.Int16, codPrestacion);
                    db.AddInParameter(dbCommand, "@fAltabeneficio", DbType.String, iBeneficio_Solicitud.FAltaBeneficio.ToShortDateString());
                    db.AddInParameter(dbCommand, "@BenExCaja", DbType.String, iBeneficio_Solicitud.BenExCaja);
                    db.AddInParameter(dbCommand, "@BenTipo", DbType.String, iBeneficio_Solicitud.BenTipo);
                    db.AddInParameter(dbCommand, "@BenNumero", DbType.String, iBeneficio_Solicitud.BenNumero);
                    db.AddInParameter(dbCommand, "@BenCopart", DbType.String, iBeneficio_Solicitud.BenCopart);
                    db.AddInParameter(dbCommand, "@BenDigVerif", DbType.String, iBeneficio_Solicitud.BenDigVerif.Equals(string.Empty) ? null : iBeneficio_Solicitud.BenDigVerif);
                    db.AddInParameter(dbCommand, "@observacion", DbType.String, iBeneficio_Solicitud.Observacion.Equals(string.Empty) ? null : iBeneficio_Solicitud.Observacion);
                    if (iBeneficio_Solicitud.TipoTrDerivado.HasValue)
                        db.AddInParameter(dbCommand, "@tipoTrDerivado", DbType.Byte, iBeneficio_Solicitud.TipoTrDerivado.Value);
                    else
                        db.AddInParameter(dbCommand, "@tipoTrDerivado", DbType.Byte, null);
                    db.ExecuteNonQuery(dbCommand);

                    oScope.Complete();
                }
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = idBeneficiario.ToString() + codPrestacion.ToString(),
                //    Datos = iBeneficio_Solicitud,
                //    Tabla = "Expedientes_Solicitud",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Solicitudes.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion

        private void Fill(out SolicitudProvisoria oObj, NullableDataReader dr)
        {
            TipoIngresoProvisorio iTipo = TipoIngresoProvisorio.Ingreso;
            if(dr.GetNullableString("tipo_ingreso") != null)
                iTipo = dr.GetString("tipo_ingreso").ToUpper().Equals("I") ? TipoIngresoProvisorio.Ingreso : TipoIngresoProvisorio.Devolucion;
            
            AuxiliaresDatos iAuxiliar = new AuxiliaresDatos();
            PaisesDatos iPaisesDatos = new PaisesDatos();

            //mapeo sector
            Sector iSector = null;
            if (dr.GetNullableInt32("sector_derivacion") != null)
            {
                List<Sector> iSectores = iAuxiliar.TraeSectores();


                if (dr.GetNullableInt32("sector_derivacion").HasValue)
                {

                    iSector = iSectores.Find(delegate(Sector auxSector)
                        {
                            return auxSector.Cod_sector == dr.GetNullableInt32("sector_derivacion").Value;
                        }
                            );
                }
            }


            //mapeo prestaciones
            Prestacion iPrestacion = null;
            if (dr.GetNullableInt16("cod_Prestacion") != null)
            {
                List<Prestacion> iPrestaciones = iAuxiliar.TraePrestaciones();


                if (dr.GetNullableInt16("cod_Prestacion").HasValue)
                {

                    iPrestacion = iPrestaciones.Find(delegate(Prestacion auxPrestacion)
                    {
                        return auxPrestacion.Cod_Prestacion == dr.GetNullableInt16("cod_Prestacion").Value;
                    }
                            );
                }
            }

            //mapeo PaisConvenio
            Pais iPais = null;
            if (dr.GetNullableInt16("cod_PaisConvenio") != null)
            {
                iPais = iPaisesDatos.TraerPaisXPaisPK(dr.GetNullableInt16("cod_PaisConvenio").Value);
            }

            oObj = new SolicitudProvisoria(
                //dr.GetInt64("id_Beneficiario"),
                //dr.GetInt16("cod_Prestacion"),
                dr.GetString("nro_SolicitudProvisoria"),
                dr.GetString("nombreDeclarado"),
                dr.GetNullableString("documentoNumDeclarado") == null ? string.Empty : dr.GetString("documentoNumDeclarado"),
                dr.GetNullableInt16("documentoTipoDeclarado"),
                dr.GetDateTime("fAltaProvisoria"),
                dr.GetNullableDateTime("fBajaProvisoria"),
                dr.GetString("referencia_Provisoria"),
                iTipo,
                iSector,
                iPrestacion,
                SolicitudProvisoriaMovimiento_TraeXMovimiento(dr.GetString("nro_SolicitudProvisoria")),
                iPais
                );
        }

        private void Fill(out SolicitudProvisoriaExtendida oObj, NullableDataReader dr)
        {
            SolicitudProvisoria tSolicitud = null;
            this.Fill(out tSolicitud, dr);
            
            oObj = new SolicitudProvisoriaExtendida(
                tSolicitud.Nro_SolicitudProvisoria,
                tSolicitud.ApellildoynombreDeclarado,
                tSolicitud.DocumentoDeclarado,
                tSolicitud.TipoDocumentoDeclarado,
                tSolicitud.FAltaProvisoria,
                tSolicitud.FBajaProvisoria,
                tSolicitud.Referencia_Provisoria,
                tSolicitud.TIngresoProvisorio,
                tSolicitud.Sectorderiva,
                tSolicitud.PrestacionSolicitada,
                tSolicitud.LMovimientos,
                tSolicitud.PaisConvenio,
                dr.GetNullableInt32("DiasVence")
                );
                
        }


        private void Fill(out SolicitudProvisoriaMovimiento oObj, NullableDataReader dr)
        {
            AuxiliaresDatos iAuxiliar = new AuxiliaresDatos();
            List<TipoDocumentacion> iDocs = iAuxiliar.TraeTipoDocumentacion();
            TipoDocumentacion iDoc = null;

            iDoc = iDocs.Find(delegate(TipoDocumentacion auxDoc)
            {
                return auxDoc.CodTipoDocumentacion == dr.GetInt32("codTipoDocumentacion");
            }
                    );
            

            oObj = new SolicitudProvisoriaMovimiento(dr.GetString("nro_SolicitudProvisoria"),
                iDoc,
                dr.GetBoolean("digitalizado"),
                dr.GetInt16("secuenciaDigitaDocumento"),
                dr.GetNullableString("breveDescripcion") == null ? string.Empty : dr.GetString("breveDescripcion")
                );
        }


        private void Fill(out Expediente_Solicitud oObj, NullableDataReader dr)
        {
            oObj = new Expediente_Solicitud(dr.GetDateTime("fAltaexpediente"),
                dr.GetString("expediente_org"),
                dr.GetString("expediente_precu"),
                dr.GetString("expediente_doccu"),
                dr.GetString("expediente_digcu"),
                dr.GetString("expediente_ctipo"),
                dr.GetString("expediente_sec"),
                dr.GetNullableString("caratula") == null ? "" : dr.GetString("caratula"),
                dr.GetNullableString("observacion") == null ? "" : dr.GetString("observacion")
                );
        }

        private void Fill(out Beneficio_Solicitud oObj, NullableDataReader dr)
        {
            oObj = new Beneficio_Solicitud(dr.GetDateTime("fAltabeneficio"),
                dr.GetString("BenExCaja"),
                dr.GetString("BenTipo"),
                dr.GetString("BenNumero"),
                dr.GetString("BenCopart"),
                dr.GetNullableString("BenDigVerif") == null ? "" : dr.GetString("BenDigVerif"),
                dr.GetNullableString("observacion") == null ? "" : dr.GetString("observacion"),
                dr.GetNullableByte("tipoTrDerivado"),
                dr.GetNullableString("dsctipoTrDerivado") == null ? "" : dr.GetString("dsctipoTrDerivado")
                );
        }


        private void Fill(out PrestacionBeneficiario oObj, NullableDataReader dr)
        {
            oObj = new PrestacionBeneficiario(dr.GetInt64("id_Beneficiario"),
                dr.GetInt16("codPrestacion"),
                dr.GetString("DescripcionPrestacion"),
                dr.GetInt16("pais_PK"),
                dr.GetString("PaisDescCompleto"),
                dr.GetBoolean("Mercosur"));
        }

        private void Fill(out Solicitud oObj, NullableDataReader dr)
        {
            oObj = new Solicitud(dr.GetInt64("id_Beneficiario"),
                dr.GetInt16("codPrestacion"),
                dr.GetString("DescripcionPrestacion"),
                dr.GetInt16("pais_PK"),
                dr.GetString("PaisDescCompleto"),
                dr.GetBoolean("Mercosur"),
                dr.GetString("referencia_exterior"),
                dr.GetString("ubicacion_Fisica"),
                dr.GetDateTime("fAMSolicitud"),
                dr.GetNullableDateTime("fechaIngreso"),
                dr.GetNullableInt16("codMotivo"),
                dr.GetString("descripcionMotivo"),
                dr.GetString("observaciones"));

            //AuxiliaresDatos oAuxDao = new AuxiliaresDatos();
            //PaisesDatos oPaisDao = new PaisesDatos();
            //MovimientosDatos oMovimientosDao = new MovimientosDatos();
            //SolicitudesDatos oSolicitudesDao = new SolicitudesDatos();
            //oObj = new Solicitud(dr.GetInt64("id_Beneficiario"),
            //    oAuxDao.TraePrestacionXId(dr.GetInt16("codPrestacion")),
            //    oPaisDao.TraerPaisXPaisPK(dr.GetInt16("pais_PK")),
            //    dr.GetNullableString("referencia_exterior") == null ? "" : dr.GetString("referencia_exterior"),
            //    dr.GetNullableString("ubicacion_Fisica") == null ? "" : dr.GetString("ubicacion_Fisica"),
            //    dr.GetBoolean("Mercosur"),
            //    dr.GetDateTime("fAMSolicitud"),
            //    oMovimientosDao.TraeMovimientosResumen(dr.GetInt64("id_Beneficiario"), dr.GetInt16("codPrestacion")),
            //    oSolicitudesDao.TraeExpedientesXSolicitud(dr.GetInt64("id_Beneficiario"), dr.GetInt16("codPrestacion")),
            //    oSolicitudesDao.TraeBeneficiosXSolicitud(dr.GetInt64("id_Beneficiario"), dr.GetInt16("codPrestacion")),
            //    oSolicitudesDao.TraeSolicitudesDenegadasXSolicitud(dr.GetInt64("id_Beneficiario"), dr.GetInt16("codPrestacion"))
            //    );
        }
    }
}