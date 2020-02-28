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

    public class DevolucionesDatos : Disposed
    {
        #region Dispose
        ~DevolucionesDatos()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        /* Esto se declara a nivel general*/
        //private DbParameterCollection dbParametros = null;
        //private string sql = string.Empty;

        #region Trae

        public Devolucion TraeDevolucionXMovimientoSolicitud(Int64 id_Beneficiario, Int16 codPrestacion, String fMovimiento)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeDevolucionesXMovimientoSolicitud");
            Devolucion oParam;

            try
            {
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                db.AddInParameter(dbCommand, "@fechaMovimiento", DbType.String, fMovimiento);
                

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    ds.Read();
                    /* Se llena el objeto con los datos del datareader */
                    this.Fill(out oParam, ds);
                    /* Se carga el objeto en la lista que se va a devolver */
                    ds.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Envios.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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



        public List<Devolucion> TraeDevolucionesXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion)
        {
            List<Devolucion> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeDevolucionesXSolicitud");

            try
            {
                Devolucion oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Devolucion>();
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
                throw new Exception("Error en Envios.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        public FaltanteDevolucion TraeFaltanteDevolucionXFechaMovimiento(Int64 id_Beneficiario, Int16 codPrestacion, string fechaMovimiento)
        {
            FaltanteDevolucion oFaltanteD = null;
            List<TipoDocumentacion> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeDevolucionesXFechaMovimiento");

            try
            {
                TipoDocumentacion oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                db.AddInParameter(dbCommand, "@fechaMovimiento", DbType.String, fechaMovimiento);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    oFaltanteD = new FaltanteDevolucion();
                    result = new List<TipoDocumentacion>();
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        Fill(out oParam, ds);
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
                throw new Exception("Error en Envios.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
            oFaltanteD.LTipoDocumentacionFaltante = result;
            /* Se retorna la lista convirtiendola en Array */
            return oFaltanteD;
        }

        #endregion

        private void Fill(out TipoDocumentacion oObj, NullableDataReader dr)
        {
            oObj = new TipoDocumentacion(dr.GetInt32("codTipoDocumentacion"),
                dr.GetNullableString("descripcion") == null ? "" : dr.GetString("descripcion")
                );
        }

        private void Fill(out Devolucion oObj, NullableDataReader dr)
        {
            oObj = new Devolucion(dr.GetDateTime("fechaMovimiento"),
                dr.GetNullableString("destino") == null  ? "" : dr.GetString("destino"),
                dr.GetNullableString("Observaciones")==null?"":dr.GetString("Observaciones"),
                dr.GetNullableString("Certificado") == null ? "" : dr.GetString("Certificado"),
                this.TraeFaltanteDevolucionXFechaMovimiento(dr.GetInt64("id_Beneficiario"), dr.GetInt16("codPrestacion"), dr.GetDateTime("fechaMovimiento").ToShortDateString()),
                dr.GetNullableDateTime("fechaNotificacion"),
                dr.GetNullableDateTime("fechaPresentacion")
                );
        }
    }

    public class IngresosDatos : Disposed
    {
        #region Dispose
        ~IngresosDatos()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        /* Esto se declara a nivel general*/
        //private DbParameterCollection dbParametros = null;
        //private string sql = string.Empty;

        #region Trae

        public Ingresos TraeIngresoXMovimientoSolicitud(Int64 id_Beneficiario, Int16 codPrestacion, String fMovimiento)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeIngresoXMovimientoSolicitud");
            Ingresos oParam;

            try
            {
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                db.AddInParameter(dbCommand, "@fechaMovimiento", DbType.String, fMovimiento);


                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    ds.Read();
                    /* Se llena el objeto con los datos del datareader */
                    this.Fill(out oParam, ds);
                    /* Se carga el objeto en la lista que se va a devolver */
                    ds.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Envios.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        public List<Ingresos> TraeIngresosXSolicitud(Int64 id_Beneficiario, Int16 codPrestacion)
        {
            List<Ingresos> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeIngresosXSolicitud");

            try
            {
                Ingresos oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Ingresos>();
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
                throw new Exception("Error en Envios.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        public List<TipoDocumentacion> TraeIngresadosXFechaMovimiento(Int64 id_Beneficiario, Int16 codPrestacion, DateTime fechaIngreso)
        {
            List<TipoDocumentacion> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeIngresadosXFechaMovimiento");

            try
            {
                TipoDocumentacion oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                db.AddInParameter(dbCommand, "@fechaIngreso", DbType.String, fechaIngreso.ToShortDateString());

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<TipoDocumentacion>();
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
                throw new Exception("Error en Envios.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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


        private void Fill(out TipoDocumentacion oObj, NullableDataReader dr)
        {
            oObj = new TipoDocumentacion(dr.GetInt32("codTipoDocumentacion"),
                dr.GetNullableString("descripcion") == null ? "" : dr.GetString("descripcion")
                );
        }

        private void Fill(out Ingresos oObj, NullableDataReader dr)
        {
            AuxiliaresDatos oAuxDao = new AuxiliaresDatos();
            oObj = new Ingresos(dr.GetNullableByte("idTipoIngreso") == null ? null : oAuxDao.Traer_TipoIngreso(dr.GetByte("idTipoIngreso")),
                dr.GetNullableDateTime("fechaIngreso"),
                this.TraeIngresadosXFechaMovimiento(dr.GetInt64("id_Beneficiario"), dr.GetInt16("codPrestacion"), dr.GetDateTime("fechaIngreso")),
                dr.GetNullableString("observacion") == null ? "" : dr.GetString("observacion")
                );
        }
    }
}