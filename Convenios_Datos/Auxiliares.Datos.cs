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
//using LoggingAnses.Servicio.Entidad;
    
namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Datos
{
    #region Auxiliares Datos
    public class AuxiliaresDatos : Disposed
    {
        #region Dispose
        ~AuxiliaresDatos()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        /* Esto se declara a nivel general*/
        //private DbParameterCollection dbParametros = null;
        //private string sql = string.Empty;

        #region Trae TipoTramite

        public List<TiposTramiteConvenios> TraeTiposTramiteConvenios(Int16 codPais, Int16 codPrestacion)
        {
            List<TiposTramiteConvenios> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTiposTramiteConvenios");

            try
            {
                TiposTramiteConvenios oParam;
                //db.AddInParameter(dbCommand, "@idTipoIngreso", DbType.Int16, null);

                db.AddInParameter(dbCommand, "@codPais", DbType.Int16, codPais);
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<TiposTramiteConvenios>();
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

        public TipoIngreso Traer_TipoIngreso(byte id_TipoIngreso)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTiposIngreso");

            TipoIngreso oParam;
            try
            {

                db.AddInParameter(dbCommand, "@idTipoIngreso", DbType.Int16, id_TipoIngreso);

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
                throw new Exception("Error en Auxiliares.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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


        #endregion

        #region Trae TraeTipoIngreso

        public List<TipoIngreso> Traer_TipoIngreso()
        {
            List<TipoIngreso> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTiposIngreso");

            try
            {
                TipoIngreso oParam;
                //db.AddInParameter(dbCommand, "@idTipoIngreso", DbType.Int16, null);

                /* Se ejecuta el store */
                using ( NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<TipoIngreso>();
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

        #endregion

        #region Trae TraeTipoPoder

        //public List<TipoPoder> Traer_TipoPoder()
        //{
        //    List<TipoPoder> result = null;
        //    Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
        //    /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
        //    DbCommand dbCommand = db.GetStoredProcCommand("TraeTiposdePoder");

        //    try
        //    {
        //        //db.AddInParameter(dbCommand, "@referencia", DbType.String, referencia);

        //        TipoPoder oParam;
        //        /* Se cargan los parámetros del store */

        //        /* Se ejecuta el store */
        //        using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
        //        {
        //            result = new List<TipoPoder>();
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
        //        throw new Exception("Error en Auxiliares.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        //public TipoPoder Traer_TipoPoder(Byte id_TipoPoder)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
        //    /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
        //    DbCommand dbCommand = db.GetStoredProcCommand("TraeTiposdePoder");
        //    TipoPoder oParam;
        //    try
        //    {
        //        db.AddInParameter(dbCommand, "@id_TipoPoder", DbType.Byte, id_TipoPoder);

                
        //        /* Se cargan los parámetros del store */

        //        /* Se ejecuta el store */
        //        using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
        //        {
        //            ds.Read();
        //            this.Fill(out oParam, ds);
        //            ds.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error en Auxiliares.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
        //    }
        //    //catch(sql)
        //    finally
        //    {
        //        db = null;
        //        dbCommand.Dispose();
        //    }

        //    /* Se retorna la lista convirtiendola en Array */
        //    return oParam;
        //}


        #endregion

        #region Trae TraeTipoDocumento

        public List<TipoDocumento> Traer_TipoDocumento(bool soloFrecuentes)
        {
            List<TipoDocumento> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTiposDocumento");

            try
            {
                TipoDocumento oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@soloFrecuentes", DbType.Boolean, soloFrecuentes);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<TipoDocumento>();
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

        #endregion

        #region Trae TrDerivado

        public List<Tramitesderivados> TraeTrDerivado()
        {
            List<Tramitesderivados> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTrDerivado");

            try
            {
                Tramitesderivados oParam;
                /* Se cargan los parámetros del store */

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Tramitesderivados>();
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

        #endregion

        #region TraeTipoDocumentacion

        public List<TipoDocumentacion> TraeTipoDocumentacion()
        {
            List<TipoDocumentacion> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTiposdeDocumentacion");

            try
            {
                TipoDocumentacion oParam;
                /* Se cargan los parámetros del store */

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

        public List<TipoDocumentacion> TraeTipoDocumentacionXPrestacion(Int16 codPrestacion)
        {
            List<TipoDocumentacion> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTipoDocumentacionXPrestacion");

            try
            {
                TipoDocumentacion oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);

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

        public List<TipoDocumentacion_Prestacion> TraeTodoTipoDocumentacionPrestacion()
        {
            List<TipoDocumentacion_Prestacion> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTipoDocumentacion_Prestacion");

            try
            {
                TipoDocumentacion_Prestacion oParam;
                /* Se cargan los parámetros del store */
                
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

        #endregion

        #region TraeTipoApoderado

        public List<TipoApoderado> TraeTipoApoderado()
        {
            List<TipoApoderado> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTiposApoderado");

            try
            {
                TipoApoderado oParam;
                db.AddInParameter(dbCommand, "@id_TipodeApoderado", DbType.Byte, null);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<TipoApoderado>();
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

        public TipoApoderado TraeTipoApoderado(Byte id_TipodeApoderado)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeTiposApoderado");
            TipoApoderado oParam;

            try
            {
                db.AddInParameter(dbCommand, "@id_TipodeApoderado", DbType.Byte, id_TipodeApoderado);

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
                throw new Exception("Error en Auxiliares.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        public List<SubTipoApoderado> TraeSubTiposApoderado()
        {
            List<SubTipoApoderado> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeSubTiposApoderado");

            try
            {
                SubTipoApoderado oParam;
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<SubTipoApoderado>();
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

        #endregion

        #region TraeSectores

        public List<Sector> TraeSectores()
        {
            List<Sector> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeSectores");

            try
            {
                Sector oParam;
                /* Se cargan los parámetros del store */

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Sector>();
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

        #endregion

        #region TraePrestaciones

        public List<Prestacion> TraePrestaciones()
        {
            List<Prestacion> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraePrestaciones");

            try
            {
                Prestacion oParam;
                /* Se cargan los parámetros del store */

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

        //public List<Prestacion> TraePrestacionesC()
        //{
        //    List<Prestacion> result = null;
        //    Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
        //    /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
        //    DbCommand dbCommand = db.GetStoredProcCommand("TraePrestaciones");

        //    try
        //    {
        //        Prestacion oParam;
        //        /* Se cargan los parámetros del store */

        //        /* Se ejecuta el store */
        //        using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
        //        {
        //            result = new List<Prestacion>();
        //            while (ds.Read())
        //            {
        //                /* Se llena el objeto con los datos del datareader */
        //                this.FillC(out oParam, ds);
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
        //        throw new Exception("Error en Auxiliares.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        public Prestacion TraePrestacionXId(Int16 codPrestacion)
        {
            Prestacion result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraePrestacionXID");

            try
            {   
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int16, codPrestacion);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    ds.Read();
                    this.Fill(out result, ds);
                    
                    ds.Close();
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Auxiliares.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return result;
        }

        #endregion

        #region TraeEstados

        public List<Estado> TraeEstados()
        {
            List<Estado> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeEstados");

            try
            {
                Estado oParam;
                /* Se cargan los parámetros del store */
                
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Estado>();
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

        #endregion

        #region Trae Motivos Denegacion

        public List<MotivoDenegacion> TraeMotivosDenegacion()
        {
            List<MotivoDenegacion> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeMotivoDenegacion");

            try
            {
                MotivoDenegacion oParam;
                /* Se cargan los parámetros del store */

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<MotivoDenegacion>();
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

        #endregion

        #region TraeBancos

        public List<Banco> TraeBancos(Boolean soloFrecuentes)
        {
            List<Banco> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBancos");

            try
            {
                Banco oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@soloFrecuentes", DbType.Boolean, soloFrecuentes);
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

        #endregion

        #region TraePaises Extranjero

        public List<Pais2> TraePaises2()
        {
            List<Pais2> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraePaises2");

            try
            {
                Pais2 oParam;
                /* Se cargan los parámetros del store */
                
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Pais2>();
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

        #endregion

        #region Trae ciudades extranjeras x pais

        public List<Ciudad> TraeCiudadesExtXPais(string codPais3c)
        {
            List<Ciudad> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeCiudadesXPais");

            try
            {
                Ciudad oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@CountryCode", DbType.String, codPais3c);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Ciudad>();
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

        #endregion

        #region TraeUbicacion

        public DirUbicacion TraeDirUbicacion(Int32 codLocalidad)
        {
            DirUbicacion result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeUbicacionXcodLocalidad");

            try
            {
                //DirUbicacion oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@codLocalidad", DbType.Int32, codLocalidad);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new DirUbicacion();
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

        #endregion

        #region TraeLocalidadesXProvincia

        public List<Localidad> TraeLocalidadesXProvincia(Int16 codProvincia)
        {
            List<Localidad> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeLocalidadesXProvincia");

            try
            {
                Localidad oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@codProvincia", DbType.Int16, codProvincia);
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Localidad>();
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

        #endregion

        #region TraeProvincias

        public List<Provincia> TraeProvincias()
        {
            List<Provincia> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeProvincias");

            try
            {
                Provincia oParam;
                /* Se cargan los parámetros del store */
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Provincia>();
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

        #endregion

        #region Altas y Modificaciones

        public void AMTipodeDocumentacion_Prestacion(Int32 codTipoDocumentacion, Int16 codPrestacion, string comentario, bool requeridoinicioTramite)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            DbCommand dbCommand = db.GetStoredProcCommand("AMTipodeDocumentacion_Prestacion");

            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {

                    /**
                     * Alta o Modificacion de Tablas
                     * */
                    db.AddInParameter(dbCommand, "@codTipoDocumentacion", DbType.Int32, codTipoDocumentacion);
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int32, codPrestacion);
                    db.AddInParameter(dbCommand, "@comentario", DbType.String, comentario);
                    db.AddInParameter(dbCommand, "@requeridoinicioTramite", DbType.Boolean, requeridoinicioTramite);
                    
                    db.ExecuteNonQuery(dbCommand);
                    oScope.Complete();
                }
                //graba rutina del log
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = codTipoDocumentacion.ToString(),
                //    Datos = codTipoDocumentacion + codPrestacion,
                //    Tabla = "TipodeDocumentacion_Prestacion",
                //    TipoAccion = TipoAction.AGREGAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Auxiliares.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }


        public void AMTipodeDocumentacion(Int32? codTipoDocumentacion, string descripcion)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            DbCommand dbCommand = db.GetStoredProcCommand("AMTiposdeDocumentacion");
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {

                    /**
                     * Alta o Modificacion de Tablas
                     * */
                    db.AddInParameter(dbCommand, "@codTipoDocumentacion", DbType.Int32, codTipoDocumentacion);
                    db.AddInParameter(dbCommand, "@descripcion", DbType.String, descripcion);
                    
                    db.ExecuteNonQuery(dbCommand);
                    oScope.Complete();
                }
                //graba rutina del log
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = codTipoDocumentacion.HasValue ? codTipoDocumentacion.Value.ToString() : "",
                //    Datos = descripcion,
                //    Tabla = "TipodeDocumentacion",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Auxiliares.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }

        public void BajaTipodeDocumentacion_Prestacion(Int32 codTipoDocumentacion, Int32 codPrestacion)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            DbCommand dbCommand = db.GetStoredProcCommand("BajaTipodeDocumentacion_Prestacion");
            //LogAplicaciones logging = new LogAplicaciones();
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {

                    /**
                     * Alta o Modificacion de Tablas
                     * */
                    db.AddInParameter(dbCommand, "@codTipoDocumentacion", DbType.Int32, codTipoDocumentacion);
                    db.AddInParameter(dbCommand, "@codPrestacion", DbType.Int32, codPrestacion);
                    
                    db.ExecuteNonQuery(dbCommand);
                    oScope.Complete();
                }
                //graba rutina del log
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = codTipoDocumentacion.ToString() + codPrestacion.ToString(),
                //    Datos = codTipoDocumentacion.ToString() + codPrestacion.ToString(),
                //    Tabla = "TipodeDocumentacion_Prestacion",
                //    TipoAccion = TipoAction.ACTUALIZAR
                //});    
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Auxiliares.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        

        #endregion

        #region Fill

        private void Fill(out TiposTramiteConvenios oObj, NullableDataReader dr)
        {
            oObj = new TiposTramiteConvenios(dr.GetString("tipoTram"),
                dr.GetNullableString("comentario")==null?"":dr.GetString("comentario")
                );
        }

        private void Fill(out Provincia oObj, NullableDataReader dr)
        {
            oObj = new Provincia(dr.GetInt16("cod_Provincia"),
                dr.GetString("descripcion_Prov")
                );
        }

        private void Fill(out Pais2 oObj, NullableDataReader dr)
        {
            oObj = new Pais2(dr.GetString("Code"),
                dr.GetString("Name"),
                dr.GetString("Continent"),
                dr.GetString("Region"),
                dr.GetString("LocalName"),
                dr.GetString("GovernmentForm"),
                dr.GetNullableInt32("Capital"),
                dr.GetString("Code2")
                );
        }

        private void Fill(out Ciudad oObj, NullableDataReader dr)
        {
            oObj = new Ciudad(dr.GetInt32("ID"),
                dr.GetString("CountryCode"),
                dr.GetString("Name"),
                dr.GetString("District"),
                dr.GetString("estadoCiudad")
                );
        }

        private void Fill(out Tramitesderivados oObj, NullableDataReader dr)
        {
            oObj = new Tramitesderivados(dr.GetByte("tipoTrDerivado"),
                dr.GetString("descripcion")
                );
        }

        private void Fill(out MotivoDenegacion oObj, NullableDataReader dr)
        {
            oObj = new MotivoDenegacion(dr.GetInt16("codMotivo"),
                dr.GetString("descripcion")
                );
        }

        private void Fill(out Localidad oObj, NullableDataReader dr)
        {
            oObj = new Localidad(dr.GetInt32("codLocalidad"),
                dr.GetString("Descripcion"),
                dr.GetString("codPostal")
                );
        }

        private void Fill(out DirUbicacion oObj, NullableDataReader dr)
        {
            oObj = new DirUbicacion(dr.GetInt32("codLocalidad"),
                dr.GetString("Descripcion"),
                dr.GetInt16("codProvincia"),
                dr.GetString("descripcionCompuesta")
                );
        }

        private void Fill(out TipoIngreso oObj, NullableDataReader dr)
        {
            oObj = new TipoIngreso(dr.GetByte("idTipoIngreso"),
                dr.GetNullableString("Descripcion") == null  ? "" : dr.GetString("Descripcion")
                );
        }

        //private void Fill(out TipoPoder oObj, NullableDataReader dr)
        //{
        //    oObj = new TipoPoder(dr.GetByte("id_TipoPoder"),
        //        dr.GetNullableString("descripcion") == null  ? "" : dr.GetString("descripcion")
        //        );
        //}

        private void Fill(out TipoDocumento oObj, NullableDataReader dr)
        {
            oObj = new TipoDocumento(dr.GetInt16("codigoDocumento"),
                dr.GetNullableString("abreviatura") == null ? "" : dr.GetString("abreviatura"),
                dr.GetNullableString("descripcion") == null  ? "" : dr.GetString("descripcion"),
                dr.GetBoolean("frecuente")
                );
        }
        
        private void Fill(out TipoDocumentacion oObj, NullableDataReader dr)
        {
            oObj = new TipoDocumentacion(dr.GetInt32("codTipoDocumentacion"),
                dr.GetNullableString("descripcion") == null  ? "" : dr.GetString("descripcion")
                );
        }

        public void Fill(out TipoDocumentacion_Prestacion oObj, NullableDataReader dr)
        {
            //Prestacion oPrestacion;
            oObj = new TipoDocumentacion_Prestacion(new Prestacion(dr.GetInt16("codPrestacion"), dr.GetString("descripcionprestacion")),
                new TipoDocumentacion(dr.GetInt32("codTipoDocumentacion"), dr.GetString("descripcionTipoDocumantacion")),
                dr.GetBoolean("requeridoInicioTramite"),
                dr.GetNullableString("comentario") == null ? "" : dr.GetString("comentario")
                );
        }

        private void Fill(out TipoApoderado oObj, NullableDataReader dr)
        {
            oObj = new TipoApoderado(dr.GetByte("id_TipodeApoderado"),
                dr.GetString("descripcion"),
                dr.GetBoolean("poderTramitar"),
                dr.GetBoolean("poderPericibir")
                );
        }

        private void Fill(out SubTipoApoderado oObj, NullableDataReader dr)
        {
            oObj = new SubTipoApoderado(dr.GetByte("id_STipoApoderado"),
                dr.GetString("descripcion")
                );
        }

        private void Fill(out Sector oObj, NullableDataReader dr)
        {
            oObj = new Sector(dr.GetInt32("cod_sector"),
                dr.GetNullableString("descripcion") == null  ? "" : dr.GetString("descripcion")
                );
        }

        private void Fill(out Prestacion oObj, NullableDataReader dr)
        {
            //List<TipoDocumentacion> iLista = traer
            oObj = new Prestacion(dr.GetInt16("codPrestacion"),
                dr.GetNullableString("descripcion") == null ? "" : dr.GetString("descripcion")
                );
        }

        //private void FillC(out Prestacion oObj, NullableDataReader dr)
        //{
        //    //List<TipoDocumentacion_Prestacion> iLista = ;
        //    oObj = new Prestacion(dr.GetInt16("codPrestacion"),
        //        dr.GetNullableString("descripcion") == null ? "" : dr.GetString("descripcion"),
        //        TraeTipoDocumentacionXPrestacion(dr.GetInt16("codPrestacion"))
        //        );
        //}

        private void Fill(out Estado oObj, NullableDataReader dr)
        {
            oObj = new Estado(dr.GetInt32("cod_estado"),
                dr.GetNullableString("descripcion") == null  ? "" : dr.GetString("descripcion")
                );
        }

        private void Fill(out Banco oObj, NullableDataReader dr)
        {
            oObj = new Banco(dr.GetInt16("id_Banco"),
                dr.GetNullableString("descripcion") == null  ? "" : dr.GetString("descripcion"),
                dr.GetBoolean("frecuente"),
                dr.GetNullableString("webSite") == null  ? "" : dr.GetString("webSite")
                );
        }

        #endregion Fill
    }
    #endregion Parametria DAO

}