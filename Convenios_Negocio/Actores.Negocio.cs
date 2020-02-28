using System;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Contrato;
using System.Collections.Generic;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Datos;

namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Negocio
{
    public class ActoresNegocio : Disposed
    {
        #region Dispose
        ~ActoresNegocio()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion

        ActoresDatos oDatos = new ActoresDatos();


        #region Trae

        #region Trae Notas por Beneficiario
        public List<BeneficiarioNotas> TraeBeneficiario_Notas(Int64 idBeneficiario)
        {
            try
            {
                return oDatos.TraeBeneficiario_Notas(idBeneficiario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                oDatos.Dispose();
            }
            
        }
        #endregion Trae


        #region Trae Listado de Beneficiarios por nombre apellido documento (titular o causante) o expediente siaci
        public List<LsBeneficiario> TraeBeneficiarios(TipoConsultaBeneficioario iTipoCons, String parametro, String codDoc)
        {
        
        
            try
            {
                return oDatos.TraeBeneficiarios(iTipoCons, parametro, codDoc);
        
            }
            catch (Exception ex)
            {
                throw new Exception("Error en " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                oDatos.Dispose();
            }
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
            try
            {

                return oDatos.TraeBeneficiariosXExpteANSES( expediente_org
            ,  expediente_precu
            ,  expediente_doccu
            ,  expediente_digcu
            ,  expediente_ctipo
            ,  expediente_sec);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                oDatos.Dispose();
            }
            
        }
        #endregion Trae Listado de Beneficiarios

        #region Trae Listado de Beneficiarios por Numero de Beneficio
        public List<LsBeneficiario> TraeBeneficiariosXNroBeneficioANSES(string BenExCaja, string BenTipo, string BenNumero, string BenCopart, string BenDigVerif)
        {
        
            try
            {
                return oDatos.TraeBeneficiariosXNroBeneficioANSES( BenExCaja,  BenTipo,  BenNumero,  BenCopart,  BenDigVerif);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                oDatos.Dispose();
            }
            
        }
        #endregion Trae Listado de Beneficiarios

        #region Trae Listado de Beneficiarios por CUIP
        //puede ocurrir el caso de ingresar solo el documento y devolvera mas de un beneficiario  de existir
        public List<LsBeneficiario> TraeBeneficiariosXCUIP(string preCUIP, string docCUIP, string digCUIP)
        {
            
            try
            {
                return oDatos.TraeBeneficiariosXCUIP( preCUIP,  docCUIP,  digCUIP);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                oDatos.Dispose();
            }
            
        }
        #endregion Trae Listado de Beneficiarios


        #region TraeBeneficiariosXNroSolicitudProvisoria
        public List<LsBeneficiario> TraeBeneficiariosXNroSolicitudProvisoria(string nro_SolicitudProvisoria)
        {
            try
            {
                return oDatos.TraeBeneficiariosXNroSolicitudProvisoria(nro_SolicitudProvisoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                oDatos.Dispose();
            }
            
        }
        #endregion Trae Listado de Beneficiarios

        #region Trae listado de Expedientes por Beneficio
        public List<Expediente_Solicitud> TraeExpedientesXBeneficio(Int64 id_Beneficiario)
        {
            try
            {
                return oDatos.TraeExpedientesXBeneficio(id_Beneficiario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            
            finally
            {
                oDatos.Dispose();
            }

            
        }

        #endregion Trae listado de Expedientes por Beneficio

        #region Datos Causante

        public Causante TraeCausanteXIDBeneficiario(Int64 id_Beneficiario)
        {
            
            try
            {
                return oDatos.TraeCausanteXIDBeneficiario(id_Beneficiario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                oDatos.Dispose();
            }
            
        }

        #endregion Datos Causante

        #region Domicilio extranjero beneficiario

        public oDireccionExtranjera TraeDireccionExtranjeraXBeneficiario(Int64 id_Beneficiario)
        {
            
            try
            {
                return oDatos.TraeDireccionExtranjeraXBeneficiario(id_Beneficiario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            //catch(sql)
            finally
            {
                oDatos.Dispose();
                
            }
            
        }

        #endregion Datos dir

        #region Trae listado de Apoderados por Beneficio
        public List<Apoderado> TraeApoderadosXid_Beneficiario(Int64 id_Beneficiario)
        {
            try
            {
                return oDatos.TraeApoderadosXid_Beneficiario(id_Beneficiario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
            
            finally
            {
                oDatos.Dispose();
                
            }
            
        }

        #endregion



        #endregion

    }

}
