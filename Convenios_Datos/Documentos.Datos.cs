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

    public class DocumentosDatos : Disposed
    {
        #region Dispose
        ~DocumentosDatos()
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

        public List<Documento_Beneficiario> TraeDocumentosBenefXid_Beneficiario(Int64 id_Beneficiario)
        {
            List<Documento_Beneficiario> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeDocumentosBenefXid_Beneficiario");

            try
            {
                Documento_Beneficiario oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Documento_Beneficiario>();
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
                throw new Exception("Error en Documentos.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        #region Trae documentos de Causante por id Beneficiario
        public List<Documento_Causante> TraeDocumentosCausanteXid_Beneficiario(Int64 id_Beneficiario)
        {
            List<Documento_Causante> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeDocumentosCausanteXid_Beneficiario");

            try
            {
                Documento_Causante oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Documento_Causante>();
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
                throw new Exception("Error en Documentos.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        #region TraeDocumentosApoderadoXid_Apoderado comentado

        //public List<Documento_Apoderado> TraeDocumentosApoderadoXid_Apoderado(Int64 idApoderado)
        //{
        //    List<Documento_Apoderado> result = null;
        //    Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
        //    /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
        //    DbCommand dbCommand = db.GetStoredProcCommand("TraeDocumentosApoderadoXid_Apoderado");

        //    try
        //    {
        //        Documento_Apoderado oParam;
        //        /* Se cargan los parámetros del store */
        //        db.AddInParameter(dbCommand, "@id_Apoderado", DbType.Int64, idApoderado);


        //        /* Se ejecuta el store */
        //        using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
        //        {
        //            result = new List<Documento_Apoderado>();
        //            while (ds.Read())
        //            {
        //                /* Se llena el objeto con los datos del datareader */
        //                this.Fill(out oParam, ds);
        //                /* Se carga el objeto en la lista que se va a devolver */
        //                result.Add(oParam);
        //            }
        //            ds.Close();
        //            //}
        //            /* Se recorre el datareader */

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
        //    }
        //    //catch(sql)
        //    finally
        //    {
        //        db = null;
        //        dbCommand.Dispose();
        //    }

        //    /* Se retorna la lista convirtiendola en Array */
        //    return result;
        //}

        #endregion TraeDocumentosApoderadoXid_Apoderado

        private void Fill(out Documento_Beneficiario oObj, NullableDataReader dr)
        {
            oObj = new Documento_Beneficiario(dr.GetInt64("id_Beneficiario"),
                dr.GetString("numDoc"),
                dr.GetInt16("codigoDocumento"),
                dr.GetString("abrevDTDoc"),
                dr.GetBoolean("docADP"),
                dr.GetNullableString("abrevPais") == null ? "" : dr.GetString("abrevPais"),
                dr.GetNullableInt16("PaisPK"),
                dr.GetDateTime("fechaAlta"),
                dr.GetNullableDateTime("fechaBaja")
                );
        }

        private void Fill(out Documento_Causante oObj, NullableDataReader dr)
        {
            oObj = new Documento_Causante(dr.GetInt64("id_Beneficiario"),
                dr.GetString("numDoc"),
                dr.GetInt16("codigoDocumento"),
                dr.GetString("abrevDTDoc"),
                dr.GetBoolean("docADP"),
                dr.GetNullableString("abrevPais") == null ? "" : dr.GetString("abrevPais"),
                dr.GetNullableInt16("PaisPK"),
                dr.GetDateTime("fechaAlta"),
                dr.GetNullableDateTime("fechaBaja")
                );
        }
    }
}