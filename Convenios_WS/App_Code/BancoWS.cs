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

    public class BancoWS : System.Web.Services.WebService
    {
        public BancoWS()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod(Description = "Alta y Modificacion de Banco")]
        public void AMBanco(Int16? idBanco, bool frecuente, string descripcion, string webSite)
        {
            BancosDatos objDao = new BancosDatos();
            //LogAplicaciones logging = new LogAplicaciones();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                objDao.AMBanco(idBanco, frecuente, descripcion, webSite);
                //logging.Log(new OnlineLog
                //{
                //    ClavePrincipal = idBanco.HasValue ? idBanco.ToString():"",
                //    Datos = frecuente.ToString() + descripcion + webSite,
                //    Tabla = "Bancos",
                //    TipoAccion = TipoAction.AGREGAR
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

        [WebMethod(Description = "Traer Bancos")]
        public List<Banco> TraerBancos(Boolean frecuentes)
        {
            BancosDatos objDao = new BancosDatos();

            try
            {
                // Creo un Objeto Windows Identity para enviarle al thread asyncronico
                //(lo necesita para poder impersonar el thread nuevo)
                WindowsIdentity mThreadIdentity = WindowsIdentity.GetCurrent();

                return objDao.TraerBancos(frecuentes);

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

    }
}