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
   
    public class PaisesDatos : Disposed
    {
        #region Dispose
        ~PaisesDatos()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        /* Esto se declara a nivel general*/
        //private DbParameterCollection dbParametros = null;
        //private string sql = string.Empty;

        #region TraePaises

        public List<Pais> TraePaises(Boolean conConvenio)
        {
            List<Pais> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraePaises");

            try
            {
                Pais oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@tieneConvenio", DbType.Boolean, conConvenio);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Pais>();
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

        public Pais TraerPaisXPaisPK(Int16 codPais)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraePaisXPaisPK");
            Pais oParam = null;

            try
            {
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@codPais", DbType.Int16, codPais);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oParam, ds);
                        
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }
                /* Se retorna la lista convirtiendola en Array */
                return oParam;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Pais.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }

        
        #endregion
   
        public void ModificaPais(Int32 codPais, bool conConvenio)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("ModificaPais");

            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Caracteristica Financiera
                     * */
                    db.AddInParameter(dbCommand, "@paisPK", DbType.Int32, codPais);
                    db.AddInParameter(dbCommand, "@tieneConvenio", DbType.Boolean, conConvenio);
                    
                    db.ExecuteNonQuery(dbCommand);

                    oScope.Complete();
                }
                //graba rutina del log
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = codPais.ToString(),
                //    Datos = conConvenio.ToString(),
                //    Tabla = "Paises",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Pais.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }

        
        private void Fill(out Pais oObj, NullableDataReader dr)
        {
            oObj = new Pais(dr.GetInt16("codigoPais"),
                dr.GetNullableString("descripcion") == null ? "" : dr.GetString("descripcion"),
                dr.GetNullableString("gentilicio") == null ? "" : dr.GetString("gentilicio"),
                dr.GetBoolean("tieneConvenio"),
                dr.GetBoolean("mercosur")
                );
        }
    }
}