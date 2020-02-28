using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security.Principal;
//using Ar.Gov.Anses.Microinformatica.ConveniosX5.Negocio;
//using Ar.Gov.Anses.Microinformatica.ConveniosX5.Datos;

namespace Ar.Gov.Anses.Microinformatica.AnsesConveniosInternacionalesX5.Servicios
{
    /// <summary>
    /// Brinda servicios de Consulta
    /// </summary>
    [WebService(Namespace = "http://AnsesConveniosInternacionalesX5.anses.gov.ar")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class MenuWS : System.Web.Services.WebService
    {

        public MenuWS()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        /*Grupo Consultas*/

        [WebMethod(Description = "Opcion Grupo Consultas")]
        public bool OpMenuGrupConsultas()
        {
            return true;
        }

        [WebMethod(Description = "Opcion Grupo Consultas - Solicitudes")]
        public bool OpSolicitudes()
        { return true;

        }

        [WebMethod(Description = "Opcion Grupo Consultas - Devoluciones Notificadas fuera de plazo")]
        public bool OpDevolNotifFueraPlazo()
        {
            return true;

        }

        [WebMethod(Description = "Opcion Grupo Consultas - Notas Beneficiario")]
        public bool OpConNotas()
        {
            return true;

        }

        [WebMethod(Description = "Opcion Grupo Consultas - Datos provisorios")]
        public bool OpConDatosProvisorios()
        {
            return true;

        }


        /*FIN Grupo Consultas*/


        /*Grupo Gestion*/
        [WebMethod(Description = "Opcion Grupo Gestión")]
        public bool OpMenuGrupGestion()
        { return true;

        }

        [WebMethod(Description = "Opcion Grupo Gestión - AMBeneficiarioSolicitud")]
        public bool OpAMBeneficiarioSolicitud()
        {
            return true;

        }

        [WebMethod(Description = "Opcion Grupo Gestión - AltaProvisoria")]
        public bool OpAltaProvisoria()
        { return true;

        }


        [WebMethod(Description = "Opcion Grupo Gestión - AltaRapida")]
        public bool OpAltaRapida()
        { return true;

        }

        [WebMethod(Description = "Opcion Grupo Gestión - AMBeneficiario")]
        public bool OpAMBeneficiario()
        { return true;

        }

        [WebMethod(Description = "Opcion Grupo Gestión - AMNotas")]
        public bool OpAMNotasBeneficiario()
        {
            return true;

        }


        /*FIN Grupo Gestion*/

        /*Grupo Admin tablas*/

        [WebMethod(Description = "Opcion Grupo Admin tablas")]
        public bool OpMenuGrupadminTablas()
        { return true;

        }

        [WebMethod(Description = "Opcion Grupo Admin tablas - Paises convenio")]
        public bool OpPaisesConvenio()
        { return true;

        }

        [WebMethod(Description = "Opcion Grupo Admin tablas - Tipos documentacion")]
        public bool OpTiposDocumentacion()
        { return true;

        }

        [WebMethod(Description = "Opcion Grupo Admin tablas - Tipos documentacion x prestacion")]
        public bool OpAMDocumentacionXPrestacion()
        { return true;

        }


        [WebMethod(Description = "Opcion Grupo Admin tablas - Am bancos")]
        public bool OpAMBancos()
        { return true;

        }


        /*FIN Grupo Admin tablas*/

        /*Grupo Indicadores*/
        
        [WebMethod(Description = "Opcion Grupo Indicadores")]
        public bool OpMenuGrupIndicadores()
        { return true;

        }

        [WebMethod(Description = "Opcion Grupo Indicadores - OpConIndicadoresXSolPrestaciones")]
        public bool OpConIndicadoresXSolPrestaciones()
        {
            return true;

        }
        [WebMethod(Description = "Opcion Grupo Indicadores - OpConIndicadoresXSolPais")]
        public bool OpConIndicadoresXSolPais()
        {
            return true;

        }
        [WebMethod(Description = "Opcion Grupo Indicadores - OpConIndicadoresXEstado")]
        public bool OpConIndicadoresXEstado()
        {
            return true;

        }
        [WebMethod(Description = "Opcion Grupo Indicadores - OpConIndicadoresXSector")]
        public bool OpConIndicadoresXSector()
        {
            return true;

        }

        /*FIN Grupo Indicadores*/

    }
}