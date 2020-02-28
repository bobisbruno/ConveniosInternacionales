using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security.Principal;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Negocio;
using Ar.Gov.Anses.Microinformatica.ConveniosX5.Datos;
using LoggingAnses.Servicio;
using LoggingAnses.Servicio.Entidad;

namespace Ar.Gov.Anses.Microinformatica.AnsesConveniosInternacionalesX5.Servicios
{
    /// <summary>
    /// Brinda servicios de Consulta
    /// </summary>
    [WebService(Namespace = "http://AnsesConveniosInternacionalesX5.anses.gov.ar")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class PaisWS : System.Web.Services.WebService
    {

        public PaisWS()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        //[WebMethod(Description = "Trae paises miembro del mercosur")]
        //public List<Mercosur_Paises> TraerPaisesMiembroMercosur()
        //{
        //    PaisesDatos objDao = new PaisesDatos();

        //    try
        //    {
        //        // Creo un Objeto Windows Identity para enviarle al thread asyncronico
        //        //(lo necesita para poder impersonar el thread nuevo)
        //        WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

        //        return objDao.TraerPaisesMiembroMercosur();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        objDao.Dispose();
        //    }

        //}

        [WebMethod(Description = "Modifica pais (solo si tiene convenio o no)")]
        public void ModificaPais(Int32 codPais, bool conConvenio)
        {
            PaisesDatos objDao = new PaisesDatos();
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                objDao.ModificaPais(codPais, conConvenio);
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
                throw ex;
            }
            finally
            {
                objDao.Dispose();
            }

        }

        [WebMethod(Description = "Trae listado de Pais")]
        public List<Pais> TraePaises(bool conConvenio)
        {
            PaisesDatos objdao = new PaisesDatos();
            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();
                return objdao.TraePaises(conConvenio);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdao.Dispose();
            }
        }

    }
}