using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Contrato
{
    #region Clase Base NotificacionesVencidas
    [Serializable()]
    public class NotificacionesVencidas : Disposed
    {

        #region Dispose

        ~NotificacionesVencidas()
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

        private String _NomApe;

        public String NomApe
        {
            get { return _NomApe; }
            set { _NomApe = value; }
        }

        private Int16 _codPrestacion;

        public Int16 CodPrestacion
        {
            get { return _codPrestacion; }
            set { _codPrestacion = value; }
        }

        private String _DescripcionPrestacion;

        public String DescripcionPrestacion
        {
            get { return _DescripcionPrestacion; }
            set { _DescripcionPrestacion = value; }
        }

        private DateTime _fechaMovimiento;

        public DateTime FechaMovimiento
        {
            get { return _fechaMovimiento; }
            set { _fechaMovimiento = value; }
        }

        private String _destino;

        public String Destino
        {
            get { return _destino; }
            set { _destino = value; }
        }

        private String _observaciones;

        public String Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        private String _certificado;

        public String Certificado
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

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public NotificacionesVencidas()
        {
        }
        public NotificacionesVencidas(Int64 id_Beneficiario, String NomApe, Int16 codPrestacion, String DescripcionPrestacion
            , DateTime fechaMovimiento, String destino, String observaciones, String certificado, DateTime? fechaNotificacion)
        {
            this._certificado = certificado;
            this._codPrestacion = codPrestacion;
            this._DescripcionPrestacion = DescripcionPrestacion;
            this._destino = destino;
            this._fechaMovimiento = fechaMovimiento;
            this._fechaNotificacion = fechaNotificacion;
            this._id_Beneficiario = id_Beneficiario;
            this._NomApe = NomApe;
            this._observaciones = observaciones;

        }

        #endregion Constructores
    }
    #endregion Clase Base NotificacionesVencidas

    #region Clase Base SolicitudesEFechasSolicitud
    [Serializable()]
    public class SolicitudesEFechasSolicitud : Disposed
    {

        #region Dispose

        ~SolicitudesEFechasSolicitud()
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

        private String _ApeNomCompleto;

        public String ApeNomCompleto
        {
            get { return _ApeNomCompleto; }
            set { _ApeNomCompleto = value; }
        }

        private String _cuip;

        public String Cuip
        {
            get { return _cuip; }
            set { _cuip = value; }
        }

        private Int16 _codPrestacion;

        public Int16 CodPrestacion
        {
            get { return _codPrestacion; }
            set { _codPrestacion = value; }
        }

        private String _DescripcionPrestacion;

        public String DescripcionPrestacion
        {
            get { return _DescripcionPrestacion; }
            set { _DescripcionPrestacion = value; }
        }

        private Int16 _pais_PK;

        public Int16 Pais_PK
        {
          get { return _pais_PK; }
          set { _pais_PK = value; }
        }

        private String _PaisDescCompleto;

        public String PaisDescCompleto
        {
          get { return _PaisDescCompleto; }
          set { _PaisDescCompleto = value; }
        }

        private Boolean _Mercosur;

        public Boolean Mercosur
        {
          get { return _Mercosur; }
          set { _Mercosur = value; }
        }

        private String _referencia_exterior;

        public String Referencia_exterior
        {
          get { return _referencia_exterior; }
          set { _referencia_exterior = value; }
        }

        private String _ubicacion_Fisica;

        public String Ubicacion_Fisica
        {
          get { return _ubicacion_Fisica; }
          set { _ubicacion_Fisica = value; }
        }

        private DateTime _fAMSolicitud;

        public DateTime FAMSolicitud
        {
          get { return _fAMSolicitud; }
          set { _fAMSolicitud = value; }
        }

        private DateTime _fechaIngreso;

        public DateTime FechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }


        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public SolicitudesEFechasSolicitud()
        {
        }
        public SolicitudesEFechasSolicitud(
            Int64 idBeneficiario
            ,Int16 codPrestacion
            , String ApeNomCompleto
            , String Cuip
            , String DescripcionPrestacion
            , Int16 codPais
            , String PaisDescCompleto
            , bool Mercosur
            , string referencia_exterior
            , string Ubicacion_Fisica
            , DateTime fAMSolicitud
            , DateTime fechaIngreso
            )
        {
            this.Id_Beneficiario = idBeneficiario;
            this.Pais_PK = codPais;
            this._codPrestacion = codPrestacion;
            this._DescripcionPrestacion = DescripcionPrestacion;
            this._cuip = Cuip;
            this._ApeNomCompleto = ApeNomCompleto;
            this._referencia_exterior = referencia_exterior;
            this._Mercosur = Mercosur;
            this._ubicacion_Fisica = Ubicacion_Fisica;
            this._fAMSolicitud = fAMSolicitud;
            this._fechaIngreso = fechaIngreso;
            this._PaisDescCompleto = PaisDescCompleto;
        }
        #endregion Constructores
    }

    
    #endregion Clase Base SolicitudesEFechasSolicitud

    #region Clase Base IndicadorPorSolicitudesPaisConvenio
    [Serializable()]
    public class IndicadorPorSolicitudesPaisConvenio : Disposed
    {

        #region Dispose

        ~IndicadorPorSolicitudesPaisConvenio()
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

        private string _totalPais;

        public String TotalPais
        {
            get { return _totalPais; }
            set { _totalPais = value; }
        }

        private string _porcentualPais;

        public String PorcentualPais
        {
            get { return _porcentualPais; }
            set { _porcentualPais = value; }
        }

        private Int16 _pais_PK;

        public Int16 Pais_PK
        {
            get { return _pais_PK; }
            set { _pais_PK = value; }
        }

        private string _DPais;

        public String DPais
        {
            get { return _DPais; }
            set { _DPais = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public IndicadorPorSolicitudesPaisConvenio()
        {
        }
        public IndicadorPorSolicitudesPaisConvenio(String totPais, String porcPais, Int16 pais_PK, String DescripcionPais)
        {
            this._totalPais = totPais;
            this._porcentualPais = porcPais;
            this._pais_PK = pais_PK;
            this._DPais = DescripcionPais;
        }

        #endregion Constructores
    }


    #endregion Clase Base IndicadorPorSolicitudesPaisConvenio

    #region Clase Base IndicadorPorSolicitudesPrestaciones
    [Serializable()]
    public class IndicadorPorSolicitudesPrestaciones : Disposed
    {

        #region Dispose

        ~IndicadorPorSolicitudesPrestaciones()
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

        private string _totalPrestacion;

        public String TotalPrestacion
        {
            get { return _totalPrestacion; }
            set { _totalPrestacion = value; }
        }

        private string _porcentualPrestacion;

        public String PorcentualPrestacion
        {
            get { return _porcentualPrestacion; }
            set { _porcentualPrestacion = value; }
        }

        private Int16 _codPrestacion;

        public Int16 CodPrestacion
        {
            get { return _codPrestacion; }
            set { _codPrestacion = value; }
        }

        private string _DPrestacion;

        public String DPrestacion
        {
            get { return _DPrestacion; }
            set { _DPrestacion = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public IndicadorPorSolicitudesPrestaciones()
        {
        }
        public IndicadorPorSolicitudesPrestaciones(String totPrestacion, String porcPrestacion, Int16 codPrestacion, String DescripcionPrestacion)
        {
            this._totalPrestacion = totPrestacion;
            this._porcentualPrestacion = porcPrestacion;
            this._codPrestacion = codPrestacion;
            this._DPrestacion = DescripcionPrestacion;
        }

        #endregion Constructores
    }


    #endregion Clase Base IndicadorPorSolicitudesPrestaciones

    #region Clase Base IndicadorTotalesEstado
    [Serializable()]
    public class IndicadorTotalesEstado : Disposed
    {

        #region Dispose

        ~IndicadorTotalesEstado()
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

        private string _totalEstado;

        public String TotalEstado
        {
            get { return _totalEstado; }
            set { _totalEstado = value; }
        }

        private string _porcentualEstado;

        public String PorcentualEstado
        {
            get { return _porcentualEstado; }
            set { _porcentualEstado = value; }
        }

        private Int32 _cod_estado;

        public Int32 Cod_estado
        {
            get { return _cod_estado; }
            set { _cod_estado = value; }
        }

        private string _Destado;

        public String Destado
        {
            get { return _Destado; }
            set { _Destado = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public IndicadorTotalesEstado()
        {
        }
        public IndicadorTotalesEstado(String totEstado, String porcEstado, Int32 codEstado, String DescripcionEstado)
        {
            this._totalEstado = totEstado;
            this._porcentualEstado = porcEstado;
            this._cod_estado = codEstado;
            this._Destado = DescripcionEstado;
        }

        #endregion Constructores
    }


    #endregion Clase Base IndicadorTotalesEstado
    
    #region Clase Base IndicadorTotalesSector
    [Serializable()]
    public class IndicadorTotalesSector : Disposed
    {

        #region Dispose

        ~IndicadorTotalesSector()
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

        private string _totalSector;

        public String TotalSector
        {
            get { return _totalSector; }
            set { _totalSector = value; }
        }

        private string _porcentualSector;

        public String PorcentualSector
        {
            get { return _porcentualSector; }
            set { _porcentualSector = value; }
        }

        private Int32 _cod_sector;

        public Int32 Cod_sector
        {
            get { return _cod_sector; }
            set { _cod_sector = value; }
        }

        private string _Dsector;

        public String Dsector
        {
            get { return _Dsector; }
            set { _Dsector = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public IndicadorTotalesSector()
        {
        }
        public IndicadorTotalesSector(String totsector, String porcsector, Int32 codsector, String Descripcionsector)
        {
            this._totalSector = totsector;
            this._porcentualSector = porcsector;
            this._cod_sector = codsector;
            this._Dsector = Descripcionsector;
        }

        #endregion Constructores
    }


    #endregion Clase Base IndicadorTotalesEstado
}
