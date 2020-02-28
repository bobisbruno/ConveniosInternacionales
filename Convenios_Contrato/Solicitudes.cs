using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Contrato
{
    #region Clase Base Prestacion X Solicitud
    [Serializable()]
    public class PrestacionBeneficiario : Disposed
    {
        #region Dispose

        ~PrestacionBeneficiario()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }
        #endregion Dispose

        #region Variables de la instancia

        private Int64 _idBeneficiario;

        public Int64 IdBeneficiario
        {
            get { return _idBeneficiario; }
            set { _idBeneficiario = value; }
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

        private Int16 _codPais;

        public Int16 CodigoPais
        {
            get { return _codPais; }
            set { _codPais = value; }
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

        #endregion

        #region Constructores
        public PrestacionBeneficiario()
        {
            
        }
        
        public PrestacionBeneficiario(Int64 idBeneficiario
            ,Int16 codPrestacion
            , String DescripcionPrestacion
            , Int16 codPais
            , String PaisDescCompleto
            , bool Mercosur
        )
        {
            this._idBeneficiario = idBeneficiario;
            this._codPais = codPais;
            this._codPrestacion = codPrestacion;
            this._DescripcionPrestacion = DescripcionPrestacion;
            this._Mercosur = Mercosur;
            this._PaisDescCompleto = PaisDescCompleto;
        }
        #endregion Constructores

        
    }
    #endregion Clase Base Prestacion X Solicitud

    #region Clase Base Solicitud
    [Serializable()]
    public class Solicitud : PrestacionBeneficiario
    {

        #region Dispose

        ~Solicitud()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }


        #endregion

        /**
         * Variables de la instancia
         * */
        #region Variables de instancia

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

        private DateTime? _fechaIngreso;

        public DateTime? FechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }

        private Int16? _codMotivo;

        public Int16? CodMotivo
        {
            get { return _codMotivo; }
            set { _codMotivo = value; }
        }

        private String _descripcionMotivo;

        public String DescripcionMotivo
        {
            get { return _descripcionMotivo; }
            set { _descripcionMotivo = value; }
        }

        private String _observaciones;

        public String Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Solicitud()
        {
            
        }
        
        public Solicitud(Int64 idBeneficiario
            ,Int16 codPrestacion
            , String DescripcionPrestacion
            , Int16 codPais
            , String PaisDescCompleto
            , bool Mercosur
            , string referencia_exterior
            , string Ubicacion_Fisica
            , DateTime fAMSolicitud
            , DateTime? fechaIngreso
            , Int16? codMotivo
            , String DescripcionMotivo
            , String observaciones)
        {
            base.IdBeneficiario = idBeneficiario;
            this._codMotivo = codMotivo;
            base.CodigoPais = codPais;
            base.CodPrestacion = codPrestacion;
            this._descripcionMotivo = DescripcionMotivo;
            base.DescripcionPrestacion = DescripcionPrestacion;
            this._referencia_exterior = referencia_exterior;
            base.Mercosur = Mercosur;
            this._ubicacion_Fisica = Ubicacion_Fisica;
            this._fAMSolicitud = fAMSolicitud;
            this._fechaIngreso = fechaIngreso;
            this._observaciones = observaciones;
            base.PaisDescCompleto = PaisDescCompleto;
        }
        //constructor para parametro
        public Solicitud(Int64 idBeneficiario
            ,Int16 codPrestacion
            , Int16 codPais
            , bool Mercosur
            , string referencia_exterior
            , string Ubicacion_Fisica
            , DateTime? fechaIngreso
            , Int16? codMotivo
            , String observaciones)
        {
            base.IdBeneficiario = idBeneficiario;
            this._codMotivo = codMotivo;
            base.CodigoPais = codPais;
            base.CodPrestacion = codPrestacion;
            this._referencia_exterior = referencia_exterior;
            base.Mercosur = Mercosur;
            this._ubicacion_Fisica = Ubicacion_Fisica;
            this._fechaIngreso = fechaIngreso;
            this._observaciones = observaciones;
        }
        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Expediente_Solicitud
    [Serializable()]
    public class Expediente_Solicitud : Disposed
    {

        #region Dispose

        ~Expediente_Solicitud()
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
        
        private DateTime _fAltaexpediente;

        public DateTime FAltaexpediente
        {
          get { return _fAltaexpediente; }
          set { _fAltaexpediente = value; }
        }
        private string _expediente_org;

        public string Expediente_org
        {
          get { return _expediente_org; }
          set { _expediente_org = value; }
        }
        private string _expediente_precu;

        public string Expediente_precu
        {
          get { return _expediente_precu; }
          set { _expediente_precu = value; }
        }
        private string _expediente_doccu;

        public string Expediente_doccu
        {
          get { return _expediente_doccu; }
          set { _expediente_doccu = value; }
        }
        private string _expediente_digcu;

        public string Expediente_digcu
        {
          get { return _expediente_digcu; }
          set { _expediente_digcu = value; }
        }
        private string _expediente_ctipo;

        public string Expediente_ctipo
        {
          get { return _expediente_ctipo; }
          set { _expediente_ctipo = value; }
        }
        private string _expediente_sec;

        public string Expediente_sec
        {
          get { return _expediente_sec; }
          set { _expediente_sec = value; }
        }

        private string _caratula;

        public string Caratula
        {
            get { return _caratula; }
            set { _caratula = value; }
        }

        private string _observacion;

        public string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Expediente_Solicitud()
        {
        }
        public Expediente_Solicitud(DateTime fAltaexpediente
            , string expediente_org
            , string expediente_precu
            , string expediente_doccu
            , string expediente_digcu
            , string expediente_ctipo
            , string expediente_sec
            , string caratula
            , string observacion)
        {
            this._expediente_ctipo = expediente_ctipo;
            this._expediente_digcu = expediente_digcu;
            this._expediente_doccu = expediente_doccu;
            this._expediente_org = expediente_org;
            this._expediente_precu = expediente_precu;
            this._expediente_sec = expediente_sec;
            this._fAltaexpediente = fAltaexpediente;
            this._caratula = caratula;
            this._observacion = observacion;

        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Beneficio_Solicitud
    [Serializable()]
    public class Beneficio_Solicitud : Disposed
    {

        #region Dispose

        ~Beneficio_Solicitud()
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

        private DateTime _fAltaBeneficio;

        public DateTime FAltaBeneficio
        {
            get { return _fAltaBeneficio; }
            set { _fAltaBeneficio = value; }
        }
        private string _BenExCaja;

        public string BenExCaja
        {
            get { return _BenExCaja; }
            set { _BenExCaja = value; }
        }
        private string _BenTipo;

        public string BenTipo
        {
            get { return _BenTipo; }
            set { _BenTipo = value; }
        }
        private string _BenNumero;

        public string BenNumero
        {
            get { return _BenNumero; }
            set { _BenNumero = value; }
        }
        private string _BenCopart;

        public string BenCopart
        {
            get { return _BenCopart; }
            set { _BenCopart = value; }
        }
        private string _BenDigVerif;

        public string BenDigVerif
        {
            get { return _BenDigVerif; }
            set { _BenDigVerif = value; }
        }
        
        private string _observacion;

        public string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        private byte? _tipoTrDerivado;

        public byte? TipoTrDerivado
        {
            get { return _tipoTrDerivado; }
            set { _tipoTrDerivado = value; }
        }

        private string _dtipoTrDerivado;

        public string DTipoTrDerivado
        {
            get { return _dtipoTrDerivado; }
            set { _dtipoTrDerivado = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Beneficio_Solicitud()
        {
        }
        public Beneficio_Solicitud(DateTime fAltaBeneficio, string BenExCaja, string BenTipo, string BenNumero, string BenCopart, string BenDigVerif, string observacion,
            byte? tipoTrDerivado, string dtipoTrDerivado)
        {
            this._BenCopart = BenCopart;
            this._BenDigVerif = BenDigVerif;
            this._BenExCaja = BenExCaja;
            this._BenNumero = BenNumero;
            this._BenTipo = BenTipo;
            this._fAltaBeneficio = fAltaBeneficio;
            this._observacion = observacion;
            this._dtipoTrDerivado = dtipoTrDerivado;
            this._tipoTrDerivado = tipoTrDerivado;

        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Movimiento Solicitud
    [Serializable()]
    public class Movimiento_Solicitud : Disposed
    {

        #region Dispose

        ~Movimiento_Solicitud()
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
        //    get { return _idBeneficiario; }
        //    set { _idBeneficiario = value; }
        //}

        //private Prestacion _prestacion;

        //public Prestacion Prestacion
        //{
        //    get { return _prestacion; }
        //    set { _prestacion = value; }
        //}


        private DateTime _fecha_Movimiento;

        public DateTime Fecha_Movimiento
        {
          get { return _fecha_Movimiento; }
          set { _fecha_Movimiento = value; }
        }

        private Estado _estado;

        public Estado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private Sector _sector;

        public Sector Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }

        private string _observaciones;

        public string Observaciones
        {
          get { return _observaciones; }
          set { _observaciones = value; }
        }

        /***
         * Propiedades extendidas del movimiento   * */
        /**
         * 31/05/2013 Se quita la propiedad porque se las obtiene desde sus clases particulares por fecha
         * **/
        //private Devolucion _devolucion;

        //public Devolucion Devolucion
        //{
        //  get { return _devolucion; }
        //  set { _devolucion = value; }
        //}

        //private Ingresos _ingreso;

        //public Ingresos Ingreso
        //{
        //    get { return _ingreso; }
        //    set { _ingreso = value; }
        //}

        
        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Movimiento_Solicitud()
        {
        }
        //public Movimiento_Solicitud(Int64 id_beneficiario, Prestacion Prestacion, DateTime fecha_Movimiento, Estado estado, Sector sector, string observaciones, string observaciones)
        public Movimiento_Solicitud(DateTime fecha_Movimiento, Estado estado, Sector sector, string observaciones)
        {
            this._estado = estado;
            this._sector = sector;
            this._fecha_Movimiento = fecha_Movimiento;
            this._observaciones = observaciones;
        }

        //public Movimiento_Solicitud(DateTime fecha_Movimiento, Estado estado, Sector sector, string observaciones, Devolucion iDevolucion, Ingresos iIngreso)
        //{
        //    this._estado = estado;
        //    this._sector = sector;
        //    this._fecha_Movimiento = fecha_Movimiento;
        //    this._observaciones = observaciones;
        //    this._ingreso = iIngreso;
        //    this._devolucion = iDevolucion;
        //}

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Movimiento resumen
    [Serializable()]
    public class IngDevMov : Disposed
    {

        #region Dispose

        ~IngDevMov()
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
        private Int64 _idBeneficiario;

        public Int64 IdBeneficiario
        {
            get { return _idBeneficiario; }
            set { _idBeneficiario = value; }
        }

        private Int16 _codPrestacion;

        public Int16 Cod_Prestacion
        {
            get { return _codPrestacion; }
            set { _codPrestacion = value; }
        }

        private DateTime _fecha_Movimiento;

        public DateTime Fecha_Movimiento
        {
            get { return _fecha_Movimiento; }
            set { _fecha_Movimiento = value; }
        }

        private string _TipoMovimiento;

        public string TipoMovimiento
        {
          get { return _TipoMovimiento; }
          set { _TipoMovimiento = value; }
        }

        private string _TipoIngreso;

        public string TipoIngreso
        {
          get { return _TipoIngreso; }
          set { _TipoIngreso = value; }
        }

        private string _Destino;

        public string Destino
        {
          get { return _Destino; }
          set { _Destino = value; }
        }

        private string _Estado;

        public string Estado
        {
          get { return _Estado; }
          set { _Estado = value; }
        }

        private string _Sector;

        public string Sector
        {
          get { return _Sector; }
          set { _Sector = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public IngDevMov()
        {
        }
        //public Movimiento_Solicitud(Int64 id_beneficiario, Prestacion Prestacion, DateTime fecha_Movimiento, Estado estado, Sector sector, string observaciones, string observaciones)
        public IngDevMov(Int64 idBeneficiario, Int16 codPrestacion, DateTime fecha_Movimiento, string tipoMovimiento, string tipoIngreso, string destino, string estado, string sector)
        {
            this._codPrestacion = codPrestacion;
            this._Destino = destino;
            this._Estado = estado;
            this._fecha_Movimiento = fecha_Movimiento;
            this._idBeneficiario = idBeneficiario;
            this._Sector = sector;
            this._TipoIngreso = tipoIngreso;
            this._TipoMovimiento = tipoMovimiento;

        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Solicitud Denegada
    [Serializable()]
    public class SolicitudDenegada : Disposed
    {

        #region Dispose

        ~SolicitudDenegada()
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
        private Int64 _idBeneficiario;

        public Int64 IdBeneficiario
        {
            get { return _idBeneficiario; }
            set { _idBeneficiario = value; }
        }

        private Int16 _codPrestacion;

        public Int16 Cod_Prestacion
        {
            get { return _codPrestacion; }
            set { _codPrestacion = value; }
        }

        private DateTime _fechaDenegatoria;

        public DateTime FechaDenegatoria
        {
            get { return _fechaDenegatoria; }
            set { _fechaDenegatoria = value; }
        }

        private DateTime?  _fechaBajaDenegatoria;

        public DateTime? FechaBajaDenegatoria
        {
            get { return _fechaBajaDenegatoria; }
            set { _fechaBajaDenegatoria = value; }
        }

        private Int16 _codMotivo;

        public Int16 CodMotivo
        {
            get { return _codMotivo; }
            set { _codMotivo = value; }
        }

        private string _DescMotivo;

        public string DescMotivo
        {
            get { return _DescMotivo; }
            set { _DescMotivo = value; }
        }

        private string _observaciones;

        public string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }


        
        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public SolicitudDenegada()
        {
        }
        //public Movimiento_Solicitud(Int64 id_beneficiario, Prestacion Prestacion, DateTime fecha_Movimiento, Estado estado, Sector sector, string observaciones, string observaciones)
        public SolicitudDenegada(Int64 idBeneficiario, Int16 codPrestacion, DateTime fechaALtaDenegatoria, DateTime? fechaBajaDenegatoria, Int16 codMotivo, string descMotivo, string observacion)
        {
            this._codPrestacion = codPrestacion;
            this._idBeneficiario = idBeneficiario;
            this._fechaDenegatoria = fechaALtaDenegatoria;
            this._fechaBajaDenegatoria = fechaBajaDenegatoria;
            this._codMotivo = codMotivo;
            this._DescMotivo = descMotivo;
            this._observaciones = observacion;
        }

        #endregion Constructores
    }
    #endregion Clase Base
    
    #region Clase Base Solicitud Pprovisoria
    [Serializable()]
    public class SolicitudProvisoria : Disposed
    {

        #region Dispose

        ~SolicitudProvisoria()
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
        //    get { return _idBeneficiario; }
        //    set { _idBeneficiario = value; }
        //}

        private string _apellildoynombreDeclarado;

        public string ApellildoynombreDeclarado
        {
            get { return _apellildoynombreDeclarado; }
            set { _apellildoynombreDeclarado = value; }
        }

        private string _documentoDeclarado;

        public string DocumentoDeclarado
        {
            get { return _documentoDeclarado; }
            set { _documentoDeclarado = value; }
        }

        private Int16? _tipoDocumentoDeclarado;

        public Int16? TipoDocumentoDeclarado
        {
            get { return _tipoDocumentoDeclarado; }
            set { _tipoDocumentoDeclarado = value; }
        }


        //private Int16 _codPrestacion;

        //public Int16 Cod_Prestacion
        //{
        //    get { return _codPrestacion; }
        //    set { _codPrestacion = value; }
        //}

        private Prestacion _PrestacionSolicitada;

        public Prestacion PrestacionSolicitada
        {
            get { return _PrestacionSolicitada; }
            set { _PrestacionSolicitada = value; }
        }

        private DateTime _fAltaProvisoria;

        public DateTime FAltaProvisoria
        {
            get { return _fAltaProvisoria; }
            set { _fAltaProvisoria = value; }
        }

        private DateTime? _fBajaProvisoria;

        public DateTime? FBajaProvisoria
        {
            get { return _fBajaProvisoria; }
            set { _fBajaProvisoria = value; }
        }

        
        private string _referencia_Provisoria;

        public string Referencia_Provisoria
        {
            get { return _referencia_Provisoria; }
            set { _referencia_Provisoria = value; }
        }


        private string _nro_SolicitudProvisoria;

        public string Nro_SolicitudProvisoria
        {
            get { return _nro_SolicitudProvisoria; }
            set { _nro_SolicitudProvisoria = value; }
        }

        private TipoIngresoProvisorio _tIngresoProvisorio;

        public TipoIngresoProvisorio TIngresoProvisorio
        {
            get { return _tIngresoProvisorio; }
            set { _tIngresoProvisorio = value; }
        }

        private Sector _sectorderiva;

        public Sector Sectorderiva
        {
            get { return _sectorderiva; }
            set { _sectorderiva = value; }
        }

        private List<SolicitudProvisoriaMovimiento> _lMovimientos;

        public List<SolicitudProvisoriaMovimiento> LMovimientos
        {
            get { return _lMovimientos; }
            set { _lMovimientos = value; }
        }

        private Pais _paisConvenio;

        public Pais PaisConvenio
        {
            get { return _paisConvenio; }
            set { _paisConvenio = value; }
        }

        private Int32? _diasCaducidad;

        public Int32? DiasCaducidad
        {
            get { return _diasCaducidad; }
            set { _diasCaducidad = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public SolicitudProvisoria()
        {
        }
        
        public SolicitudProvisoria(String nroSolicitudProvisoria, String nombreyApellidoDeclarado, String documentoDeclarado, Int16? tipoDocumentoDeclarado, DateTime fechaALtaProvisoria, DateTime? fechaBajaProvisoria, string ReferenciaProvisoria, TipoIngresoProvisorio TipoIngresoProvisorio, Sector sectorDerivacion, Prestacion prestacionSolicitada, List<SolicitudProvisoriaMovimiento> lMovimientos, Pais paisConvenio)
        {
            this._apellildoynombreDeclarado = nombreyApellidoDeclarado;
            this._documentoDeclarado = documentoDeclarado;
            this._tipoDocumentoDeclarado = tipoDocumentoDeclarado;
            this._fAltaProvisoria = fechaALtaProvisoria;
            this._fBajaProvisoria = fechaBajaProvisoria;
            this._referencia_Provisoria = ReferenciaProvisoria;
            this._nro_SolicitudProvisoria = nroSolicitudProvisoria;
            this._tIngresoProvisorio = TipoIngresoProvisorio;
            this._sectorderiva = sectorDerivacion;
            this._PrestacionSolicitada = prestacionSolicitada;
            this._lMovimientos = lMovimientos;
            this._paisConvenio = paisConvenio;
         
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Solicitud Pprovisoria
    [Serializable()]
    public class SolicitudProvisoriaExtendida : SolicitudProvisoria
    {

        #region Dispose

        ~SolicitudProvisoriaExtendida()
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
        
        private Int32? _diasCaducidad;

        public Int32? DiasCaducidad
        {
            get { return this._diasCaducidad; }
            set {  this._diasCaducidad = value; }
        }

        #endregion Variables de la instancia
                
        /**
         * Constructores
         * */
        
        #region Constructores
        public SolicitudProvisoriaExtendida()
        {
        }

        public SolicitudProvisoriaExtendida(String nroSolicitudProvisoria, String nombreyApellidoDeclarado, String documentoDeclarado, Int16? tipoDocumentoDeclarado, DateTime fechaALtaProvisoria, DateTime? fechaBajaProvisoria, string ReferenciaProvisoria, TipoIngresoProvisorio TipoIngresoProvisorio, Sector sectorDerivacion, Prestacion prestacionSolicitada, List<SolicitudProvisoriaMovimiento> lMovimientos, Pais paisConvenio, Int32? diasCaducidad)
        {
            base.ApellildoynombreDeclarado = nombreyApellidoDeclarado;
            base.DocumentoDeclarado = documentoDeclarado;
            base.TipoDocumentoDeclarado = tipoDocumentoDeclarado;
            base.FAltaProvisoria = fechaALtaProvisoria;
            base.FBajaProvisoria = fechaBajaProvisoria;
            base.Referencia_Provisoria = ReferenciaProvisoria;
            base.Nro_SolicitudProvisoria = nroSolicitudProvisoria;
            base.TIngresoProvisorio = TipoIngresoProvisorio;
            base.Sectorderiva = sectorDerivacion;
            base.PrestacionSolicitada = prestacionSolicitada;
            base.LMovimientos = lMovimientos;
            base.PaisConvenio = paisConvenio;
            this._diasCaducidad = diasCaducidad;
        }

        
        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Solicitud Pprovisoria
    [Serializable()]
    public class Beneficiario_SolicitudProvisoria : Disposed
    {

        #region Dispose

        ~Beneficiario_SolicitudProvisoria()
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
        private Int64 _idBeneficiario;

        public Int64 IdBeneficiario
        {
            get { return _idBeneficiario; }
            set { _idBeneficiario = value; }
        }

        private SolicitudProvisoria _solicitudProovisoria;

        public SolicitudProvisoria SolicitudProovisoria
        {
          get { return _solicitudProovisoria; }
          set { _solicitudProovisoria = value; }
        }
        
        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Beneficiario_SolicitudProvisoria()
        {
        }

        public Beneficiario_SolicitudProvisoria(Int64 id_Beneficiario, SolicitudProvisoria solicitud_Provisoria)
        {
            this._idBeneficiario = id_Beneficiario;
            this._solicitudProovisoria = solicitud_Provisoria;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Solicitud Pprovisoria movimientos
    [Serializable()]
    public class SolicitudProvisoriaMovimiento : Disposed
    {

        #region Dispose

        ~SolicitudProvisoriaMovimiento()
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
        
        private string _nro_SolicitudProvisoria;

        public string Nro_SolicitudProvisoria
        {
            get { return _nro_SolicitudProvisoria; }
            set { _nro_SolicitudProvisoria = value; }
        }

        private TipoDocumentacion _tipoDocumentacion;

        public TipoDocumentacion TipoDocumentacion
        {
          get { return _tipoDocumentacion; }
          set { _tipoDocumentacion = value; }
        }

        private Boolean _digitalizado;

        public Boolean Digitalizado
        {
            get { return _digitalizado; }
            set { _digitalizado = value; }
        }

        private Int16 _secuenciaDocumento;

        public Int16 SecuenciaDocumento
        {
            get { return _secuenciaDocumento; }
            set { _secuenciaDocumento = value; }
        }

        private String _descripcionBreve;

        public String DescripcionBreve
        {
            get { return _descripcionBreve; }
            set { _descripcionBreve = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public SolicitudProvisoriaMovimiento()
        {
        }

        public SolicitudProvisoriaMovimiento( String nroSolicitudProvisoria, TipoDocumentacion tipoDocumentacion, Boolean digitalizado, Int16 secuenciaDocumento, String descripcionBreve)
        {
            this._nro_SolicitudProvisoria = nroSolicitudProvisoria;
            this._tipoDocumentacion = tipoDocumentacion;
            this._digitalizado = digitalizado;
            this._descripcionBreve = descripcionBreve;
            this._secuenciaDocumento = secuenciaDocumento;
        }

        #endregion Constructores
    }
    #endregion Clase Base

}
