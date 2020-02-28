using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Contrato
{
    #region Clase Base Documento
    [Serializable()]
    public class Documento : Disposed
    {

        #region Dispose

        ~Documento()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }


        #endregion

        /**
         * Variables de la instancia
         * */
        #region Variables de la instancia
        private string _numDoc;

        public string NumDoc
        {
            get { return _numDoc; }
            set { _numDoc = value; }
        }
        private Int16 _codigoDocumento;

        public Int16 CodigoDocumento
        {
            get { return _codigoDocumento; }
            set { _codigoDocumento = value; }
        }

        private string _abrevDocumento;

        public string AbrevDocumento
        {
            get { return _abrevDocumento; }
            set { _abrevDocumento = value; }
        }

        private bool _docADP;

        public bool DocADP
        {
            get { return _docADP; }
            set { _docADP = value; }
        }
        
        private string _codAbrevPais;

        public string CodAbrevPais
        {
            get { return _codAbrevPais; }
            set { _codAbrevPais = value; }
        }

        private Int16? _Pais_PK;

        public Int16? Pais_PK
        {
            get { return _Pais_PK; }
            set { _Pais_PK = value; }
        }

        private DateTime _fechaAlta;

        public DateTime FechaAlta
        {
            get { return _fechaAlta; }
            set { _fechaAlta = value; }
        }
        private DateTime? _fechaBaja;

        public DateTime? FechaBaja
        {
            get { return _fechaBaja; }
            set { _fechaBaja = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Documento()
        {
        }
        public Documento(string numDoc, Int16 codigoDocumento, string abrevDocuemnto, bool docADP, string codAbrevPais, Int16? Pais_PK, DateTime fechaAlta, DateTime? fechaBaja)
        {
            this._codAbrevPais = codAbrevPais;
            this._codigoDocumento = codigoDocumento;
            this._docADP = docADP;
            this._Pais_PK = Pais_PK;
            this._fechaAlta = fechaAlta;
            this._fechaBaja = fechaBaja;
            this._numDoc = numDoc;
            this._abrevDocumento = abrevDocuemnto;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Documento Causante
    [Serializable()]
    public class Documento_Causante : Documento
    {

        #region Dispose

        ~Documento_Causante()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }


        #endregion

        /**
         * Variables de la instancia
         * */
        #region Variables de la instancia
        private Int64 _id_causante;

        public Int64 Id_causante
        {
          get { return _id_causante; }
          set { _id_causante = value; }
        }

        
        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Documento_Causante()
        {
        }
        public Documento_Causante(Int64 id_causante, string numDoc, Int16 codigoDocumento, string abrevDocuemnto, bool docADP, string codAbrevPais, Int16? Pais_PK, DateTime fechaAlta, DateTime? fechaBaja)
            : base(numDoc,  codigoDocumento, abrevDocuemnto, docADP, codAbrevPais, Pais_PK, fechaAlta, fechaBaja)
        {
            
            this._id_causante = id_causante;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Documento Beneficiario
    [Serializable()]
    public class Documento_Beneficiario : Documento
    {

        #region Dispose

        ~Documento_Beneficiario()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }


        #endregion

        /**
         * Variables de la instancia
         * */
        #region Variables de la instancia
        private Int64 _id_Beneficiario;

        public Int64 Id_Beneficiario
        {
            get { return _id_Beneficiario; }
            set { _id_Beneficiario = value; }
        }

        

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Documento_Beneficiario()
        {
        }

        public Documento_Beneficiario(Int64 id_Beneficiario, string numDoc, Int16 codigoDocumento, string abrevDocuemnto, bool docADP, string codAbrevPais, Int16? Pais_PK, DateTime fechaAlta, DateTime? fechaBaja)
            : base(numDoc, codigoDocumento, abrevDocuemnto, docADP, codAbrevPais, Pais_PK, fechaAlta, fechaBaja)
        {
            this._id_Beneficiario = id_Beneficiario;
        }

        #endregion Constructores
    }
    #endregion Clase Base
}
