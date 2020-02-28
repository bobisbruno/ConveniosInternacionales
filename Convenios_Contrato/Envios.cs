using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Contrato
{
    #region Clase Base Devolucion
    [Serializable()]
    public class Devolucion : Disposed
    {

        #region Dispose

        ~Devolucion()
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
        //private Int64 _idBeneficiario;

        //public Int64 IdBeneficiario
        //{
        //  get { return _idBeneficiario; }
        //  set { _idBeneficiario = value; }
        //}

        //private _codPrestacion

        private DateTime _fechaMovimiento;

        public DateTime FechaMovimiento
        {
            get { return _fechaMovimiento; }
            set { _fechaMovimiento = value; }
        }
        private string _destino;

        public string Destino
        {
          get { return _destino; }
          set { _destino = value; }
        }
        private string _observaciones;

        public string Observaciones
        {
          get { return _observaciones; }
          set { _observaciones = value; }
        }
        
        private string _certificado;

        public string Certificado
        {
          get { return _certificado; }
          set { _certificado = value; }
        }

        private DateTime? _fechaNotificacion;

        public DateTime? FechaNotificacion
        {
            get { return _fechaNotificacion; }
            set { _fechaNotificacion = value; }
        }

        private DateTime? _fechaPresentacion;

        public DateTime? FechaPresentacion
        {
            get { return _fechaPresentacion; }
            set { _fechaPresentacion = value; }
        }

        private FaltanteDevolucion _TipoDocumentacionFaltante;

        public FaltanteDevolucion TipoDocumentacionFaltante
        {
            get { return _TipoDocumentacionFaltante; }
            set { _TipoDocumentacionFaltante = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Devolucion()
        {
        }
        
        public Devolucion(string destino, DateTime? FechaNotificacion)
        {
            this._destino = destino;
            this._fechaNotificacion = FechaNotificacion;
        }

        public Devolucion(DateTime fechaMovimiento
             ,string destino
            , string observaciones
            , string certificado
            , FaltanteDevolucion TipoDocumentacionFaltante
            , DateTime? FechaNotificacion
            , DateTime? FechaPresentacion)
        {
            this._certificado = certificado;
            this._destino = destino;
            this._fechaMovimiento = fechaMovimiento;
            this._observaciones = observaciones;
            this._TipoDocumentacionFaltante = TipoDocumentacionFaltante;
            this._fechaNotificacion = FechaNotificacion;
            this._fechaPresentacion = FechaPresentacion;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Faltantes Devolucion

    [Serializable()]
    public class FaltanteDevolucion : Disposed
    {

        #region Dispose

        ~FaltanteDevolucion()
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
        private List<TipoDocumentacion> _lTipoDocumentacionFaltante;

        public List<TipoDocumentacion> LTipoDocumentacionFaltante
        {
          get { return _lTipoDocumentacionFaltante; }
          set { _lTipoDocumentacionFaltante = value; }
        }

        //private DateTime _fechaDevolucion;

        //public DateTime FechaDevolucion
        //{
        //  get { return _fechaDevolucion; }
        //  set { _fechaDevolucion = value; }
        //}


        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public FaltanteDevolucion()
        {
        }
        public FaltanteDevolucion(List<TipoDocumentacion> LTipoDocumentacionFaltante)
        {
            this._lTipoDocumentacionFaltante = LTipoDocumentacionFaltante;
        }

        #endregion Constructores
    }

    #endregion Faltantes Devolucion

    #region Clase Ingresos

    [Serializable()]
    public class Ingresos : Disposed
    {

        #region Dispose

        ~Ingresos()
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

        private List<TipoDocumentacion> _lTipoDocumentacion;

        public List<TipoDocumentacion> LTipoDocumentacion
        {
          get { return _lTipoDocumentacion; }
          set { _lTipoDocumentacion = value; }
        }

        private TipoIngreso _tipoIngreso;
            
        public TipoIngreso TipoIngreso
        {
            get { return _tipoIngreso; }
            set { _tipoIngreso = value; }
        }

        private DateTime? _fechaIngreso;

        public DateTime? FechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }

        private String _observacion;

        public String Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Ingresos()
        {
        }
        public Ingresos(TipoIngreso tipoingreso, DateTime? fechaIngreso, List<TipoDocumentacion> lTipoDocumentacion, String observacion)
        {
            this._tipoIngreso = tipoingreso;
            this._lTipoDocumentacion = lTipoDocumentacion;
            this._fechaIngreso = fechaIngreso;
            this._observacion = observacion;
        }

        #endregion Constructores
    }

    #endregion
}
