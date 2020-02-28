using System;
using System.Collections.Generic;
using NullableReaders;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Contrato;

namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Datos
{
    public class ActoresDatos : Disposed
    {
        #region Dispose
        ~ActoresDatos()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        #region Trae

        #region Trae Notas por Beneficiario
        public List<BeneficiarioNotas> TraeBeneficiario_Notas(Int64 idBeneficiario)
        {
            List<BeneficiarioNotas> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBeneficiario_Notas");

            try
            {
                BeneficiarioNotas oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<BeneficiarioNotas>();
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
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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


        #region Trae Listado de Beneficiarios por nombre apellido documento (titular o causante) o expediente siaci
        public List<LsBeneficiario> TraeBeneficiarios(TipoConsultaBeneficioario iTipoCons, String parametro, String codDoc)
        {
            List<LsBeneficiario> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBeneficiarios");

            try
            {

                LsBeneficiario oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@tipoConosulta", DbType.Byte, iTipoCons);
                db.AddInParameter(dbCommand, "@parametro", DbType.String, parametro);
                if(codDoc.Equals(""))
                    db.AddInParameter(dbCommand, "@codDoc", DbType.Int16, null);
                else
                    db.AddInParameter(dbCommand, "@codDoc", DbType.Int16, Int16.Parse(codDoc.Trim()));

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<LsBeneficiario>();
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
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion Trae Listado de Beneficiarios

        #region Trae Listado de Beneficiarios por expediente ANSES
        public List<LsBeneficiario> TraeBeneficiariosXExpteANSES(string expediente_org
            , string expediente_precu
            , string expediente_doccu
            , string expediente_digcu
            , string expediente_ctipo
            , string expediente_sec)
        {
            List<LsBeneficiario> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBeneficiariosXExpteANSES");

            try
            {
                LsBeneficiario oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@org", DbType.String, expediente_org.Equals(string.Empty)?null:expediente_org);
                db.AddInParameter(dbCommand, "@pre", DbType.String, expediente_precu.Equals(string.Empty)?null:expediente_precu);
                db.AddInParameter(dbCommand, "@dig", DbType.String, expediente_digcu.Equals(string.Empty)?null:expediente_digcu);
                db.AddInParameter(dbCommand, "@doc", DbType.String, expediente_doccu);
                db.AddInParameter(dbCommand, "@ctipo", DbType.String, expediente_ctipo.Equals(string.Empty)?null:expediente_ctipo);
                db.AddInParameter(dbCommand, "@secu", DbType.String, expediente_sec.Equals(string.Empty)?null:expediente_sec);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<LsBeneficiario>();
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
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion Trae Listado de Beneficiarios

        #region Trae Listado de Beneficiarios por Numero de Beneficio
        public List<LsBeneficiario> TraeBeneficiariosXNroBeneficioANSES(string BenExCaja, string BenTipo, string BenNumero, string BenCopart, string BenDigVerif)
        {
            List<LsBeneficiario> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBeneficiariosXNroBeneficioANSES");

            try
            {
                LsBeneficiario oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@BenExCaja", DbType.String, BenExCaja.Equals(string.Empty) ? null : BenExCaja);
                db.AddInParameter(dbCommand, "@BenTipo", DbType.String, BenTipo.Equals(string.Empty) ? null : BenTipo);
                db.AddInParameter(dbCommand, "@BenNumero", DbType.String, BenNumero);
                db.AddInParameter(dbCommand, "@BenCopart", DbType.String, BenCopart.Equals(string.Empty) ? null : BenCopart);
                db.AddInParameter(dbCommand, "@BenDigVerif", DbType.String, BenDigVerif.Equals(string.Empty) ? null : BenDigVerif);
                

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<LsBeneficiario>();
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
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion Trae Listado de Beneficiarios

        #region Trae Listado de Beneficiarios por CUIP
        //puede ocurrir el caso de ingresar solo el documento y devolvera mas de un beneficiario  de existir
        public List<LsBeneficiario> TraeBeneficiariosXCUIP(string preCUIP, string docCUIP, string digCUIP)
        {
            List<LsBeneficiario> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBeneficiariosXCUIP");

            try
            {
                LsBeneficiario oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@preCUIP", DbType.String, preCUIP.Equals(string.Empty) ? null : preCUIP);
                db.AddInParameter(dbCommand, "@digCUIP", DbType.String, digCUIP.Equals(string.Empty) ? null : digCUIP);
                db.AddInParameter(dbCommand, "@docCUIP", DbType.String, docCUIP);
                
                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<LsBeneficiario>();
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
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion Trae Listado de Beneficiarios


        #region TraeBeneficiariosXNroSolicitudProvisoria
        public List<LsBeneficiario> TraeBeneficiariosXNroSolicitudProvisoria(string nro_SolicitudProvisoria)
        {
            List<LsBeneficiario> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBeneficiariosXNroSolicitudProvisoria");

            try
            {
                LsBeneficiario oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@nro_SolicitudProvisoria", DbType.String, nro_SolicitudProvisoria);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<LsBeneficiario>();
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
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
        #endregion Trae Listado de Beneficiarios

        #region Trae listado de Expedientes por Beneficio
        public List<Expediente_Solicitud> TraeExpedientesXBeneficio(Int64 id_Beneficiario)
        {
            List<Expediente_Solicitud> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeExpedientesXBeneficio");

            try
            {
                Expediente_Solicitud oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                

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
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        #endregion Trae listado de Expedientes por Beneficio

        #region Datos Causante

        public Causante TraeCausanteXIDBeneficiario(Int64 id_Beneficiario)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeCausanteXid_Beneficiario");

            Causante oBenef = null;
            try
            {

                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oBenef, ds);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return oBenef;
        }

        #endregion Datos Causante

        #region Domicilio extranjero beneficiario

        public oDireccionExtranjera TraeDireccionExtranjeraXBeneficiario(Int64 id_Beneficiario)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeDomicilioExtranjeroBeneficiario");

            oDireccionExtranjera odir = null;
            try
            {

                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out odir, ds);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return odir;
        }

        #endregion Datos dir
        
        #region Trae listado de Apoderados por Beneficio
        public List<Apoderado> TraeApoderadosXid_Beneficiario(Int64 id_Beneficiario)
        {
            List<Apoderado> result = null;
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeApoderadosXid_Beneficiario");

            try
            {
                Apoderado oParam;
                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);
                db.AddInParameter(dbCommand, "@historico", DbType.Boolean, false);
                


                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    result = new List<Apoderado>();
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
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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

        

        #endregion


        #region Fill
        public void Fill(out LsBeneficiario oObj, NullableDataReader dr)
        {
            DocumentosDatos objDocumentosDao = new DocumentosDatos();
            List<Documento_Beneficiario> oDocs = objDocumentosDao.TraeDocumentosBenefXid_Beneficiario(dr.GetInt64("id_Beneficiario"));
            string documento;
            if (oDocs.Count != 0)
                documento = oDocs[0].NumDoc + "(" + oDocs[0].AbrevDocumento + ")";
            else
                documento = string.Empty;

            oObj = new LsBeneficiario(dr.GetInt64("id_Beneficiario"),
                dr.GetString("apeNom"),
                dr.GetNullableString("ApellidoMaterno") == null ? "" : dr.GetString("ApellidoMaterno"),
                //dr.GetString("documento"),
                documento,
                dr.GetNullableString("expedienteExt") == null ? "" : dr.GetString("expedienteExt"),
                dr.GetNullableString("sexo") == null ? "":dr.GetString("sexo"),
                dr.GetNullableDateTime("fecha_nac")
                );
        }

        private void Fill(out BeneficiarioNotas oObj, NullableDataReader dr)
        {
            oObj = new BeneficiarioNotas(dr.GetInt64("id_Beneficiario"),
                dr.GetDateTime("fRecepcion"),
                dr.GetNullableString("nroNota") == null ? "" : dr.GetString("nroNot da"),
                dr.GetNullableString("Asunto") == null ? "" : dr.GetString("Asunto"),
                dr.GetNullableString("Descripcion") == null ? "" : dr.GetString("Descripcion")
                );
        }

        
        private void Fill(out Causante oObj, NullableDataReader dr)
        {
            DocumentosDatos objDaoDoc = new DocumentosDatos();
            oObj = new Causante(dr.GetInt64("id_Beneficiario"),
                dr.GetString("apeNom"),
                dr.GetNullableString("cuip") == null ? "" : dr.GetString("cuip"),
                dr.GetString("sexo"),
                dr.GetDateTime("Fecha_Def"),
                dr.GetNullableDateTime("Fecha_Nac"),
                objDaoDoc.TraeDocumentosCausanteXid_Beneficiario(dr.GetInt64("id_Beneficiario"))
                );
        }

        private void Fill(out oDireccionExtranjera oObj, NullableDataReader dr)
        {
            oObj = new oDireccionExtranjera(dr.GetInt64("id_Beneficiario"),
                dr.GetNullableString("estado") == null ? "" : dr.GetString("estado"),
                dr.GetNullableString("CountryCode") == null ? "" : dr.GetString("CountryCode"),
                dr.GetNullableInt32("ID_Ciudad"),
                dr.GetNullableString("ciudad") == null ? "" : dr.GetString("ciudad"),
                dr.GetNullableString("DirCalle") == null ? "" : dr.GetString("DirCalle"),
                dr.GetNullableString("DirNum") == null ? "" : dr.GetString("DirNum"),
                dr.GetNullableString("entreCalle1") == null ? "" : dr.GetString("entreCalle1"),
                dr.GetNullableString("entreCalle2") == null ? "" : dr.GetString("entreCalle2"),
                dr.GetNullableString("piso") == null ? "" : dr.GetString("piso"),
                dr.GetNullableString("departamento") == null ? "" : dr.GetString("departamento"),
                dr.GetNullableString("cod_postal") == null ? "" : dr.GetString("cod_postal"),
                dr.GetNullableString("dpais") == null ? "" : dr.GetString("dpais"),
                dr.GetNullableString("dciudad") == null ? "" : dr.GetString("dciudad"),
                dr.GetNullableString("dDistrito") == null ? "" : dr.GetString("dDistrito")
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

        private void Fill(out Apoderado oObj, NullableDataReader dr)
        {
            AuxiliaresDatos oAuxDao = new AuxiliaresDatos();
            TipoApoderado iTipoApod = new TipoApoderado(dr.GetByte("id_TipodeApoderado"), dr.GetString("descripcionTApoderado"), dr.GetBoolean("poderTramitar"), dr.GetBoolean("poderPercibir"));
            SubTipoApoderado isTipoApod = new SubTipoApoderado(dr.GetByte("id_STipoApoderado"), dr.GetString("descripcionSTApoderado"));
            BancosDatos oBancoDao = new BancosDatos();
            oObj = new Apoderado(dr.GetString("numDoc"),
                dr.GetInt16("codigoDocumento"),
                dr.GetString("descripcionDoc"),
                dr.GetString("abreviaturaDoc"),
                dr.GetString("apeNom"),
                dr.GetNullableString("DirCalle") == null ? "" : dr.GetString("DirCalle"),
                dr.GetNullableString("DirNum") == null ? "" : dr.GetString("DirNum"),
                dr.GetNullableString("entreCalle1") == null ? "" : dr.GetString("entreCalle1"),
                dr.GetNullableString("entreCalle2") == null ? "" : dr.GetString("entreCalle2"),
                dr.GetNullableString("piso") == null ? "" : dr.GetString("piso"),
                dr.GetNullableString("departamento") == null ? "" : dr.GetString("departamento"),
                dr.GetNullableInt32("codLocalidad"),
                dr.GetNullableString("codPostal") == null ? "" : dr.GetString("codPostal"),
                dr.GetNullableString("ciudad") == null ? "" : dr.GetString("ciudad"),
                dr.GetNullableString("Sexo") == null ? "" : dr.GetString("Sexo"),
                dr.GetNullableString("Telefono") == null  ? "" : dr.GetString("Telefono"),
                dr.GetNullableString("EMail") == null  ? "" : dr.GetString("EMail"),
                dr.GetNullableInt32("codLocalidad") == null ? null : oAuxDao.TraeDirUbicacion(dr.GetInt32("codLocalidad")),
                iTipoApod,
                isTipoApod,
                dr.GetNullableString("comentarios")==null ? "" : dr.GetString("comentarios"),
                dr.GetNullableInt16("id_Banco") == null ? null : oBancoDao.TraerBancos(dr.GetInt16("id_Banco")),
                dr.GetDateTime("fAlta"),
                dr.GetNullableDateTime("fBaja")
                );
        }
        #endregion Fill
    }

    public class ActorDatos : ActoresDatos
    {
        
        #region Trae Datos de Beneficiario

        public LsBeneficiario TraeBeneficiarioSimpleXID(Int64 id_Beneficiario)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBeneficiarioXId");

            LsBeneficiario oBenef = null;
            try
            {

                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        //lo hereda de la superclase, solo se necesitan en este caso los datos basicos
                        base.Fill(out oBenef, ds);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return oBenef;
        }

        public Beneficiario TraeBeneficiarioXID(Int64 id_Beneficiario)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("TraeBeneficiarioXId");

            Beneficiario oBenef = null;
            try
            {

                /* Se cargan los parámetros del store */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, id_Beneficiario);

                /* Se ejecuta el store */
                using (NullableDataReader ds = new NullableDataReader(db.ExecuteReader(dbCommand)))
                {
                    while (ds.Read())
                    {
                        /* Se llena el objeto con los datos del datareader */
                        this.Fill(out oBenef, ds);
                    }
                    ds.Close();
                    //}
                    /* Se recorre el datareader */

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                db = null;
                dbCommand.Dispose();
            }

            /* Se retorna la lista convirtiendola en Array */
            return oBenef;
        }
        #endregion Trae Datos de Beneficiario

        #region Fill beneficiario
        private void Fill(out Beneficiario oObj, NullableDataReader dr)
        {
            PaisesDatos objPaisesDao = new PaisesDatos();
            SolicitudesDatos objSolicitudesDao = new SolicitudesDatos();
            DevolucionesDatos objDevolucionesDao = new DevolucionesDatos();
            DocumentosDatos objDocumentosDao = new DocumentosDatos();
            AuxiliaresDatos oAuxDao = new AuxiliaresDatos();

            oObj = new Beneficiario(dr.GetInt64("id_Beneficiario"),
                dr.GetNullableString("expedienteExt") == null ? "" : dr.GetString("expedienteExt"),
                dr.GetString("apeNom"),
                dr.GetNullableString("ApellidoMaterno") == null  ? "" : dr.GetString("ApellidoMaterno"),
                dr.GetNullableDateTime("fecha_nac"),
                dr.GetNullableString("cuip") == null ? "" : dr.GetString("cuip"),
                dr.GetNullableString("sexo") == null  ? "" : dr.GetString("sexo"),
                dr.GetNullableString("DirCalle") == null ? "" : dr.GetString("DirCalle"),
                dr.GetNullableString("DirNum") == null ? "" : dr.GetString("DirNum"),
                dr.GetNullableString("entreCalle1") == null ? "" : dr.GetString("entreCalle1"),
                dr.GetNullableString("entreCalle2") == null ? "" : dr.GetString("entreCalle2"),
                dr.GetNullableString("piso") == null ? "" : dr.GetString("piso"),
                dr.GetNullableString("departamento") == null ? "" : dr.GetString("departamento"),
                dr.GetNullableString("cod_postal") == null ? "" : dr.GetString("cod_postal"),
                dr.GetNullableString("ciudad") == null ? "" : dr.GetString("ciudad"),
                dr.GetNullableInt32("codLocalidad") == null ? null : oAuxDao.TraeDirUbicacion(dr.GetInt32("codLocalidad")),
                base.TraeDireccionExtranjeraXBeneficiario(dr.GetInt64("id_Beneficiario")),
                dr.GetNullableInt16("pais_PK_nacionalidad") == null  ? null : objPaisesDao.TraerPaisXPaisPK(dr.GetInt16("pais_PK_nacionalidad")),
                objSolicitudesDao.TraePrestacionesXIdBeneficiario(dr.GetInt64("id_Beneficiario")), //solo las solicitudes
                objDocumentosDao.TraeDocumentosBenefXid_Beneficiario(dr.GetInt64("id_Beneficiario")),
                base.TraeCausanteXIDBeneficiario(dr.GetInt64("id_Beneficiario")),
                base.TraeApoderadosXid_Beneficiario(dr.GetInt64("id_Beneficiario")),
                objSolicitudesDao.SolicitudProvisoria_TraeXIdBeneficiario(dr.GetInt64("id_Beneficiario")) //solo las solicitudes provisorias

                );
        }
        #endregion Fill beneficiario

        #region Transacciones

        
        #region AMBeneficiario
        public void AMBeneficiario(Beneficiario iBeneficiario, out Int64 idBenef)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMBeneficiario");
               
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Beneficiario
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, iBeneficiario.IdBeneficio);
                    db.AddInParameter(dbCommand, "@expedienteExt", DbType.String, iBeneficiario.ExpedienteExterno);
                    db.AddInParameter(dbCommand, "@apeNom", DbType.String, iBeneficiario.ApeNom);
                    db.AddInParameter(dbCommand, "@cuip", DbType.String, iBeneficiario.Cuip.Equals("") ? null : iBeneficiario.Cuip);
                    db.AddInParameter(dbCommand, "@Apellido_materno", DbType.String, iBeneficiario.ApellMaterno.Equals("") ? null : iBeneficiario.ApellMaterno);
                    db.AddInParameter(dbCommand, "@DirCalle", DbType.String, iBeneficiario.DirCalle.Equals("") ? null : iBeneficiario.DirCalle);
                    db.AddInParameter(dbCommand, "@DirNum", DbType.String, iBeneficiario.DirNum.Equals("") ? null : iBeneficiario.DirNum);
                    db.AddInParameter(dbCommand, "@entreCalle1", DbType.String, iBeneficiario.ECalle1.Equals("") ? null : iBeneficiario.ECalle1);
                    db.AddInParameter(dbCommand, "@entreCalle2", DbType.String, iBeneficiario.ECalle2.Equals("") ? null : iBeneficiario.ECalle2);
                    db.AddInParameter(dbCommand, "@piso", DbType.String, iBeneficiario.Piso.Equals("") ? null : iBeneficiario.Piso);
                    db.AddInParameter(dbCommand, "@departamento", DbType.String, iBeneficiario.Departamento.Equals("") ? null : iBeneficiario.Departamento);
                    if(iBeneficiario.Ubicacion == null)
                        db.AddInParameter(dbCommand, "@codLocalidad", DbType.Int32, null);
                    else
                        db.AddInParameter(dbCommand, "@codLocalidad", DbType.Int32, iBeneficiario.Ubicacion.Cod_Localidad);
                    db.AddInParameter(dbCommand, "@cod_postal", DbType.String, iBeneficiario.CodPostal.Equals("") ? null : iBeneficiario.CodPostal);
                    db.AddInParameter(dbCommand, "@ciudad", DbType.String, iBeneficiario.Ciudad.Equals("") ? null : iBeneficiario.Ciudad);
                    db.AddInParameter(dbCommand, "@Sexo", DbType.String, iBeneficiario.Sexo);
                    db.AddInParameter(dbCommand, "@fecha_nac", DbType.DateTime, iBeneficiario.Fecha_nac);
                    if(iBeneficiario.Pais_Nacionalidad == null)
                        db.AddInParameter(dbCommand, "@pais_PK_nacionalidad", DbType.Int32, null);
                    else
                        db.AddInParameter(dbCommand, "@pais_PK_nacionalidad", DbType.Int32, iBeneficiario.Pais_Nacionalidad.Pais_PK);
                    db.AddOutParameter(dbCommand, "@idNew", DbType.Int64, 26000);
                    
                    db.ExecuteNonQuery(dbCommand);

                 

                    idBenef = iBeneficiario.IdBeneficio.HasValue?iBeneficiario.IdBeneficio.Value:(long)db.GetParameterValue(dbCommand, "@idNew");

                    //grabo los documentos del beneficiario
                    foreach (Documento_Beneficiario iDoc in iBeneficiario.LDocumentosBeneficiario)
                    {
                        AMbeneficiarios_documentos(iDoc, idBenef);
                    }
                    
                    oScope.Complete();
                }

                //graba datos causante
                if (iBeneficiario.Causante != null)
                {
                    iBeneficiario.Causante.Id_causante = idBenef;
                    AMCausante(iBeneficiario.Causante);
                }

                //graba apoderados
                if (iBeneficiario.LApoderado != null && iBeneficiario.LApoderado.Count != 0)
                {
                    foreach (Apoderado apod in iBeneficiario.LApoderado)
                    {
                        AMApoderado(apod, idBenef);
                    }
                }
                //graba domicilio extranjero
                if (iBeneficiario.OdirExtranjera != null)
                {
                    iBeneficiario.OdirExtranjera.Id_Beneficiario = idBenef;
                    AMDireccionExtranjera(iBeneficiario.OdirExtranjera);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }   
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion AMBeneficiario

        #region AM Direccion extranjera

        public void AMDireccionExtranjera(iDireccionExtranjera iDirE)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMDireccionesExtranjerasBeneficiarios");

            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Beneficiario
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, iDirE.Id_Beneficiario);
                    db.AddInParameter(dbCommand, "@CountryCode", DbType.String, iDirE.CountryCode.Equals("") ? null : iDirE.CountryCode);
                    if(iDirE.IdCiudad.HasValue)
                        db.AddInParameter(dbCommand, "@ID_Ciudad", DbType.Int32, iDirE.IdCiudad.Value);
                    else
                        db.AddInParameter(dbCommand, "@ID_Ciudad", DbType.Int32, null);
                    db.AddInParameter(dbCommand, "@estado", DbType.String, iDirE.Estado.Equals("") ? null : iDirE.Estado);
                    db.AddInParameter(dbCommand, "@ciudad", DbType.String, iDirE.Ciudad.Equals("") ? null : iDirE.Ciudad);
                    db.AddInParameter(dbCommand, "@DirCalle", DbType.String, iDirE.Dircalle.Equals("") ? null : iDirE.Dircalle);
                    db.AddInParameter(dbCommand, "@DirNum", DbType.String, iDirE.Dirnum.Equals("") ? null : iDirE.Dirnum);
                    db.AddInParameter(dbCommand, "@entreCalle1", DbType.String, iDirE.Ecalle1.Equals("") ? null : iDirE.Ecalle1);
                    db.AddInParameter(dbCommand, "@entreCalle2", DbType.String, iDirE.Ecalle2.Equals("") ? null : iDirE.Ecalle2);
                    db.AddInParameter(dbCommand, "@piso", DbType.String, iDirE.Piso.Equals("") ? null : iDirE.Piso);
                    db.AddInParameter(dbCommand, "@departamento", DbType.String, iDirE.Depto.Equals("") ? null : iDirE.Depto);
                    db.AddInParameter(dbCommand, "@cod_postal", DbType.String, iDirE.CodPostal.Equals("") ? null : iDirE.CodPostal);
                    
                    db.ExecuteNonQuery(dbCommand);

                    oScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
 
        }

        #endregion AM Direccion extranjera

        #region AMCausante
        public void AMCausante(Causante iCausante)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMCausante");
            
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Beneficiario
                     * */
                    db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, iCausante.Id_causante);
                    db.AddInParameter(dbCommand, "@apeNom", DbType.String, iCausante.ApeNom);
                    db.AddInParameter(dbCommand, "@cuip", DbType.String, iCausante.Cuip.Equals("") ? null : iCausante.Cuip);
                    db.AddInParameter(dbCommand, "@Sexo", DbType.String, iCausante.Sexo);
                    db.AddInParameter(dbCommand, "@fecha_def", DbType.DateTime, iCausante.Fecha_Def);
                    if(iCausante.Fecha_Nacimiento.HasValue)
                        db.AddInParameter(dbCommand, "@fecha_nac", DbType.DateTime, iCausante.Fecha_Nacimiento.Value);
                    else
                        db.AddInParameter(dbCommand, "@fecha_nac", DbType.DateTime, null);
                    
                    db.ExecuteNonQuery(dbCommand);

                    foreach (Documento_Causante iDoc in iCausante.LDocCausante)
                    {
                        AMcausantes_documentos(iDoc, iCausante.Id_causante);
                    }

                    oScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion AMCausante

        #region AMApoderado
        //public void AMApoderado(Apoderado iApoderado, out Int64 idApod, Int64 idBeneficiario)
        public void AMApoderado(Apoderado iApoderado, Int64 idBeneficiario)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMApoderado");
            
            try
            {
                using (TransactionScope oScope = new TransactionScope())
                {
                    /**
                     * Alta de Apoderado
                     * */
                    db.AddInParameter(dbCommand, "@numDoc", DbType.String, iApoderado.NumDoc);
                    db.AddInParameter(dbCommand, "@codigoDocumento", DbType.String, iApoderado.CodigoDocumento);
                    db.AddInParameter(dbCommand, "@apeNom", DbType.String, iApoderado.ApeNom);
                    db.AddInParameter(dbCommand, "@DirCalle", DbType.String, iApoderado.DirCalle);
                    db.AddInParameter(dbCommand, "@DirNum", DbType.String, iApoderado.DirNum);
                    db.AddInParameter(dbCommand, "@entreCalle1", DbType.String, iApoderado.EntreCalle1);
                    db.AddInParameter(dbCommand, "@entreCalle2", DbType.String, iApoderado.EntreCalle2);
                    db.AddInParameter(dbCommand, "@piso", DbType.String, iApoderado.Piso);
                    db.AddInParameter(dbCommand, "@departamento", DbType.String, iApoderado.Departamento);
                    //db.AddInParameter(dbCommand, "@codLocalidad", DbType.Int32, iApoderado.CodLocalidad);
                    db.AddInParameter(dbCommand, "@cod_postal", DbType.String, iApoderado.Cod_postal);
                    db.AddInParameter(dbCommand, "@ciudad", DbType.String, iApoderado.Ciudad);
                    
                    if(iApoderado.DirUbicacion == null)
                        db.AddInParameter(dbCommand, "@codLocalidad", DbType.Int32, null);
                    else
                        db.AddInParameter(dbCommand, "@codLocalidad", DbType.Int32, iApoderado.DirUbicacion.Cod_Localidad);
                    db.AddInParameter(dbCommand, "@Sexo", DbType.String, iApoderado.Sexo);
                    db.AddInParameter(dbCommand, "@Telefono", DbType.String, iApoderado.Telefono);
                    db.AddInParameter(dbCommand, "@EMail", DbType.String, iApoderado.EMail);
                    
                    db.ExecuteNonQuery(dbCommand);
                    
                    //graba relacion beneficiario / apoderado
                    AMBeneficiario_Apoderado(iApoderado, idBeneficiario);

                    oScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion AMApoderado

        #region AMBeneficiario_Apoderado

        //public void AMBeneficiario_Apoderado(Apoderado iApoderado, Int64 idApoderado, Int64 idBeneficiario)
        public void AMBeneficiario_Apoderado(Apoderado iApoderado, Int64 idBeneficiario)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMBeneficiario_Apoderado");

            try
            {

                /**
                    * Alta rel apoderado beneficiario
                    * */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);
                //db.AddInParameter(dbCommand, "@id_Tipopoder", DbType.Byte, iApoderado.TipoPoder.Id_TipoPoder);
                //db.AddInParameter(dbCommand, "@id_Apoderado", DbType.Int64, idApoderado);
                db.AddInParameter(dbCommand, "@comentarios", DbType.String, iApoderado.Comentario);
                db.AddInParameter(dbCommand, "@id_TipodeApoderado", DbType.Byte, iApoderado.TipoApoderado.Id_TipodeApoderado);
                db.AddInParameter(dbCommand, "@id_STipodeApoderado", DbType.Byte, iApoderado.StipoApoderado.Id_STipodeApoderado);
                if(iApoderado.Banco == null)
                    db.AddInParameter(dbCommand, "@id_Banco", DbType.Int16, null);
                else
                        db.AddInParameter(dbCommand, "@id_Banco", DbType.Int16, iApoderado.Banco.Id_Banco);
                //db.AddInParameter(dbCommand, "@fBaja", DbType.DateTime, iApoderado.Fbaja);
                db.AddInParameter(dbCommand, "@numDoc", DbType.String, iApoderado.NumDoc);
                db.AddInParameter(dbCommand, "@codigoDocumento", DbType.String, iApoderado.CodigoDocumento);

                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
 
        }

        #endregion AMBeneficiario_Apoderado

        #region BajaBeneficiario_Apoderado

        public void BajaBeneficiario_Apoderado(Apoderado iApoderado, Int64 idBeneficiario)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("BajaBeneficiario_Apoderado");

            try
            {

                /**
                    * Alta rel apoderado beneficiario
                    * */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);
                db.AddInParameter(dbCommand, "@id_TipodeApoderado", DbType.Byte, iApoderado.TipoApoderado.Id_TipodeApoderado);
                db.AddInParameter(dbCommand, "@id_STipodeApoderado", DbType.Byte, iApoderado.StipoApoderado.Id_STipodeApoderado);
                db.AddInParameter(dbCommand, "@fAlta", DbType.String, iApoderado.FAlta.ToShortDateString().Trim());

                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }

        #endregion BajaBeneficiario_Apoderado

        #region AMcausantes_documentos
        private void AMcausantes_documentos(Documento_Causante iDocumento_Causante, Int64 idBeneficiario)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMcausantes_documentos");

            try
            {
                
                /**
                    * Alta de doc causante
                    * */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);
                db.AddInParameter(dbCommand, "@numDoc", DbType.String, iDocumento_Causante.NumDoc);
                db.AddInParameter(dbCommand, "@codigoDocumento", DbType.Int16, iDocumento_Causante.CodigoDocumento);
                db.AddInParameter(dbCommand, "@docADP", DbType.Boolean, iDocumento_Causante.DocADP);
                if (iDocumento_Causante.Pais_PK.HasValue)
                    db.AddInParameter(dbCommand, "@PaisPK", DbType.Int32, iDocumento_Causante.Pais_PK.Value);
                else
                    db.AddInParameter(dbCommand, "@PaisPK", DbType.Int32, null);
                db.AddInParameter(dbCommand, "@fechaAlta", DbType.DateTime, iDocumento_Causante.FechaAlta);
                //if (iDocumento_Causante.FechaBaja.HasValue)
                //    db.AddInParameter(dbCommand, "@fechaBaja", DbType.DateTime, iDocumento_Causante.FechaBaja.Value);
                //else
                //    db.AddInParameter(dbCommand, "@fechaBaja", DbType.DateTime, null);
                db.AddInParameter(dbCommand, "@fechaBaja", DbType.DateTime, iDocumento_Causante.FechaBaja);

                db.ExecuteNonQuery(dbCommand);

                
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion


        #region AMbeneficiarios_documentos

        public Boolean ExisteDocumento(String doc, Int16 tdoc)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("ExisteDocumento");
            
            try
            {   
                /**
                    * Alta de doc Beneficiario
                    * */
                db.AddInParameter(dbCommand, "@numDoc", DbType.String, doc);
                db.AddInParameter(dbCommand, "@codigoDocumento", DbType.Int16, tdoc);
                db.AddOutParameter(dbCommand, "@existe", DbType.Boolean,1);
                db.ExecuteNonQuery(dbCommand);

                return (Boolean)db.GetParameterValue(dbCommand, "@existe");
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }

        private void AMbeneficiarios_documentos(Documento_Beneficiario iDocumento_Beneficiario, Int64 idBeneficiario)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMbeneficiarios_documentos");

            try
            {   
                /**
                    * Alta de doc Beneficiario
                    * */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, idBeneficiario);
                db.AddInParameter(dbCommand, "@numDoc", DbType.String, iDocumento_Beneficiario.NumDoc);
                db.AddInParameter(dbCommand, "@codigoDocumento", DbType.Int16, iDocumento_Beneficiario.CodigoDocumento);
                db.AddInParameter(dbCommand, "@docADP", DbType.Boolean, iDocumento_Beneficiario.DocADP);
                if (iDocumento_Beneficiario.Pais_PK.HasValue)
                    db.AddInParameter(dbCommand, "@PaisPK", DbType.Int32, iDocumento_Beneficiario.Pais_PK.Value);
                else
                    db.AddInParameter(dbCommand, "@PaisPK", DbType.Int32, null);
                db.AddInParameter(dbCommand, "@fechaAlta", DbType.DateTime, iDocumento_Beneficiario.FechaAlta);
                //if(iDocumento_Beneficiario.FechaBaja.HasValue)
                //    db.AddInParameter(dbCommand, "@fechaBaja", DbType.DateTime, System.DateTime.Today);
                //else
                //    db.AddInParameter(dbCommand, "@fechaBaja", DbType.DateTime, null);
                db.AddInParameter(dbCommand, "@fechaBaja", DbType.DateTime, iDocumento_Beneficiario.FechaBaja);


                db.ExecuteNonQuery(dbCommand);

                
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                db = null;
                dbCommand.Dispose();
            }
        }
        #endregion

        #region AMbeneficiario Notas
        public void AMBeneficiarioNotas(BeneficiarioNotas iParam)
        {
            Database db = DatabaseFactory.CreateDatabase("ConveniosInt_V02");
            /* Definimos el nombre de la base que definimos en el Machine.config que luego se explicará. */
            DbCommand dbCommand = db.GetStoredProcCommand("AMBeneficiario_Nota");

            try
            {

                /**
                    * Alta de nota
                    * */
                db.AddInParameter(dbCommand, "@id_Beneficiario", DbType.Int64, iParam.Id_Beneficiario);
                db.AddInParameter(dbCommand, "@fRecepcion", DbType.DateTime, iParam.FRecepcion);
                db.AddInParameter(dbCommand, "@nroNota", DbType.String, iParam.NroNota == string.Empty ? null : iParam.NroNota);
                db.AddInParameter(dbCommand, "@Asunto", DbType.String, iParam.Asunto == string.Empty ? null : iParam.Asunto);
                db.AddInParameter(dbCommand, "@Descripcion", DbType.String, iParam.Descripcion == string.Empty ? null : iParam.Descripcion);
                
                db.ExecuteNonQuery(dbCommand);


            }
            catch (Exception ex)
            {
                throw new Exception("Error en Actores.Datos " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
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
}