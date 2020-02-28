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
//using LoggingAnses.Servicio;
////using LoggingAnses.Servicio.Comun;
//using LoggingAnses.Servicio.Entidad;


namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Datos
{

    public class BancosDatos : Disposed
    {
        #region Dispose
        ~BancosDatos()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        /* Esto se declara a nivel general*/
        //private DbParameterCollection dbParametros = null;
        //private string sql = string.Empty;


        #region AMBanco
        public void AMBanco(Int16? idBanco, bool frecuente, string descripcion, string webSite)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMBanco");

            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Caracteristica Financiera
                     * */
                    db.AddInParameter(dbCommand, "@id_Banco", DbType.Int16, idBanco);
                    db.AddInParameter(dbCommand, "@Frecuente", DbType.Boolean, frecuente);
                    db.AddInParameter(dbCommand, "@descripcion", DbType.String, descripcion);
                    db.AddInParameter(dbCommand, "@webSite", DbType.String, webSite);

                    db.ExecuteNonQuery(dbCommand);

                    oScope.Complete();
                }
                //graba rutina del log
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = idBanco.HasValue ? idBanco.ToString() : "",
                //    Datos = frecuente.ToString() + descripcion + webSite,
                //    Tabla = "Bancos",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Banco.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion AMBanco


        #region traeBancos
        public List<Banco> TraerBancos(Boolean frecuentes)
        {
            List<Banco> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBancos");

            try
            {
                Banco oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@soloFrecuentes", DbType.Boolean, frecuentes);
                db.AddInParameter(dbCommand, "@id_Banco", DbType.Int16, null);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Banco>();
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
                /* Se retorna la lista convirtiendola en Array */
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Banco.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }

        public Banco TraerBancos(Int16 id_Banco)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBancos");
            Banco oParam;

            try
            {
                
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@soloFrecuentes", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@id_Banco", DbType.Int16, id_Banco);

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
                throw new Exception("Error en Banco.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            return oParam;
        }
        #endregion TraeBanco

        private void Fill(out Banco oObj, NullableDataReader dr)
        {
            oObj = new Banco(dr.GetInt16("id_Banco"),
                dr.GetNullableString("descripcion") == null  ? "" : dr.GetString("descripcion"),
                dr.GetBoolean("frecuente"),
                dr.GetNullableString("webSite") == null ?"":dr.GetString("webSite")
                );
        }
    }
}