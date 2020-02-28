using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Contrato
{
    #region Clase Base Beneficiario
    [Serializable()]
    public class Beneficiario : Disposed
    {

        #region Dispose

        ~Beneficiario()
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
        private Int64? _idBeneficio;

        public Int64? IdBeneficio
        {
            get { return _idBeneficio; }
            set { _idBeneficio = value; }
        }

        private string _expedienteExterno;

        public string ExpedienteExterno
        {
            get { return _expedienteExterno; }
            set { _expedienteExterno = value; }
        }

        private string _apeNom;

        public string ApeNom
        {
            get { return _apeNom; }
            set { _apeNom = value; }
        }

        private string _apellMaterno;

        public string ApellMaterno
        {
            get { return _apellMaterno; }
            set { _apellMaterno = value; }
        }


        private string _cuip;

        public string Cuip
        {
            get { return _cuip; }
            set { _cuip = value; }
        }
        private string _sexo;

        public string Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }

        private string _dirCalle;

        public string DirCalle
        {
            get { return _dirCalle; }
            set { _dirCalle = value; }
        }

        private string _dirNum;

        public string DirNum
        {
            get { return _dirNum; }
            set { _dirNum = value; }
        }

        private string _eCalle1;

        public string ECalle1
        {
            get { return _eCalle1; }
            set { _eCalle1 = value; }
        }

        private string _eCalle2;

        public string ECalle2
        {
            get { return _eCalle2; }
            set { _eCalle2 = value; }
        }

        private string _piso;

        public string Piso
        {
            get { return _piso; }
            set { _piso = value; }
        }

        private string _departamento;

        public string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }

        private string _codPostal;

        public string CodPostal
        {
            get { return _codPostal; }
            set { _codPostal = value; }
        }

        private string _ciudad;

        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }



        private DirUbicacion _ubicacion;

        public DirUbicacion Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        private oDireccionExtranjera _odirExtranjera;

        public oDireccionExtranjera OdirExtranjera
        {
            get { return _odirExtranjera; }
            set { _odirExtranjera = value; }
        }

               
        private DateTime? _fecha_nac;

        public DateTime? Fecha_nac
        {
          get { return _fecha_nac; }
          set { _fecha_nac = value; }
        }
        
        private Pais _pais_Nacionalidad;

        public Pais Pais_Nacionalidad
        {
            get { return _pais_Nacionalidad; }
            set { _pais_Nacionalidad = value; }
        }

        private Causante _causante;

        public Causante Causante
        {
            get { return _causante; }
            set { _causante = value; }
        }

        private List<Apoderado> _lApoderado;

        public List<Apoderado> LApoderado
        {
            get { return _lApoderado; }
            set { _lApoderado = value; }
        }

        private List<Documento_Beneficiario> _LDocumentosBeneficiario;

        public List<Documento_Beneficiario> LDocumentosBeneficiario
        {
            get { return _LDocumentosBeneficiario; }
            set { _LDocumentosBeneficiario = value; }
        }

        private List<PrestacionBeneficiario> _LPrestacionBeneficiario;

        public List<PrestacionBeneficiario> LPrestacionBeneficiario
        {
            get { return _LPrestacionBeneficiario; }
            set { _LPrestacionBeneficiario = value; }
        }

        private List<Beneficiario_SolicitudProvisoria> _LSolicitudProvisoria;

        public List<Beneficiario_SolicitudProvisoria> LSolicitudProvisoria
        {
            get { return _LSolicitudProvisoria; }
            set { _LSolicitudProvisoria = value; }
        }
        
        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Beneficiario()
        {
        }

        /**
         * Este constructor completo para traer todos los datos del beneficiario
         * */
        public Beneficiario(Int64? idBeneficio
            , string ExpedienteExterno
            , string apeNom
            , string apell_materno
            , DateTime ? fechaNacimiento
            , string cuip
            , string sexo
            , string dirCalle
            , string dirNum
            , string eCalle1
            , string eCalle2
            , string piso
            , string departamento
            , string codPostal
            , string ciudad
            , DirUbicacion ubicacion
            , oDireccionExtranjera dirExtranjera
            , Pais pais_nacionalidad
            , List<PrestacionBeneficiario> iLPrestacionBenef
            , List<Documento_Beneficiario> iLDocumentos
            , Causante causante
            , List<Apoderado> iLApoderados
            , List<Beneficiario_SolicitudProvisoria> iLSolicitudesProvisorias

            )
        {
            _idBeneficio = idBeneficio;
            _expedienteExterno = ExpedienteExterno;
            _apeNom = apeNom;
            _apellMaterno = apell_materno;
            _fecha_nac = fechaNacimiento;
            _cuip = cuip;
            _sexo = sexo;
            _dirCalle = dirCalle;
            _dirNum = dirNum;
            _eCalle1 = eCalle1;
            _eCalle2 = eCalle2;
            _piso = piso;
            _departamento = departamento;
            _codPostal = codPostal;
            _ciudad = ciudad;
            _ubicacion = ubicacion;
            _odirExtranjera = dirExtranjera;
            _pais_Nacionalidad = pais_nacionalidad;
            _LPrestacionBeneficiario = iLPrestacionBenef;
            _LDocumentosBeneficiario = iLDocumentos;
            _causante = causante;
            _lApoderado = iLApoderados;
            _LSolicitudProvisoria = iLSolicitudesProvisorias;
        }

        /**
         * Este constructor se utilizara para obtener listado de Beneficiarios
         * */
        //public Beneficiario(Int64 idBeneficio
        //    , string ExpedienteExterno
        //    , string apeNom
        //    , string apell_materno
        //    , string cuip
        //    , string sexo
        //    , string dirCalle
        //    , string dirNum
        //    , DirUbicacion ubicacion
        //    , DateTime? fecha_nac
            
        //    )
        //{
        //    this._idBeneficio = idBeneficio;
        //    this._apell_materno = apell_materno;
        //    this._apeNom = apeNom;
        //    this._cuip = cuip;
        //    this._expedienteExterno = ExpedienteExterno;
        //    this._fecha_nac = fecha_nac;
        //    this._pais_Nacionalidad = pais_nacionalidad;
        //    this.Sexo = sexo;
        //    this.DirCalle = dirCalle;
        //    this.DirNum = dirNum;
        //    this.Ubicacion = ubicacion;
        //}

        /**
         * Este constructor se utilizara para obtener todos los datos de un beneficiario
         * */
        //public Beneficiario(Int64? idBeneficio
        //    , string ExpedienteExterno
        //    , string apell_paterno
        //    , string apell_materno
        //    , string nombre
        //    , string sexo
        //    , string dirCalle
        //    , string dirNum
        //    , DirUbicacion ubicacion
        //    , DateTime? fecha_nac
        //    , Pais pais_nacionalidad
        //    ,List<Solicitud> iLSolicitudes
        //    ,List<Documento_Beneficiario>iLDocumentos
        //    ,Causante causante
        //    ,List<Apoderado> iLApoderados)
        //{
        //    this._idBeneficio = idBeneficio;
        //    this._apell_materno = apell_materno;
        //    this.Apellido = apell_paterno;
        //    this.Nombre = nombre;
        //    this._expedienteExterno = ExpedienteExterno;
        //    this._fecha_nac = fecha_nac;
        //    this._pais_Nacionalidad = pais_nacionalidad;
        //    this.Sexo = sexo;
        //    this.DirCalle = dirCalle;
        //    this.DirNum = dirNum;
        //    this.Ubicacion = ubicacion;
        //    this._causante = causante;
        //    this._lApoderado = iLApoderados;
        //    this._LDocumentosBeneficiario = iLDocumentos;
        //    this._Lsolicitudes = iLSolicitudes;
        //}

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Apoderado
    [Serializable()]
    public class Apoderado : Disposed
    {

        #region Dispose

        ~Apoderado()
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

        private string _abrevDoc;

        public string AbrevDoc
        {
            get { return _abrevDoc; }
            set { _abrevDoc = value; }
        }

        private string _descDoc;

        public string DescDoc
        {
            get { return _descDoc; }
            set { _descDoc = value; }
        }

        private string _apeNom;

        public string ApeNom
        {
            get { return _apeNom; }
            set { _apeNom = value; }
        }

        private string _DirCalle;

        public string DirCalle
        {
            get { return _DirCalle; }
            set { _DirCalle = value; }
        }

        private string _DirNum;

        public string DirNum
        {
            get { return _DirNum; }
            set { _DirNum = value; }
        }

        private string _entreCalle1;

        public string EntreCalle1
        {
            get { return _entreCalle1; }
            set { _entreCalle1 = value; }
        }

        private string _entreCalle2;

        public string EntreCalle2
        {
            get { return _entreCalle2; }
            set { _entreCalle2 = value; }
        }

        private string _piso;

        public string Piso
        {
            get { return _piso; }
            set { _piso = value; }
        }

        private string _departamento;

        public string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }

        private Int32? _codLocalidad;

        public Int32? CodLocalidad
        {
            get { return _codLocalidad; }
            set { _codLocalidad = value; }
        }

        private string _cod_postal;

        public string Cod_postal
        {
            get { return _cod_postal; }
            set { _cod_postal = value; }
        }

        private string _ciudad;

        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }

        private DirUbicacion _dirUbicacion;

        public DirUbicacion DirUbicacion
        {
            get { return _dirUbicacion; }
            set { _dirUbicacion = value; }
        }

        private string _Sexo;

        public string Sexo
        {
            get { return _Sexo; }
            set { _Sexo = value; }
        }

        private string _Telefono;

        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }
        private string _EMail;

        public string EMail
        {
            get { return _EMail; }
            set { _EMail = value; }
        }

        private TipoApoderado _tipoApoderado;

        public TipoApoderado TipoApoderado
        {
            get { return _tipoApoderado; }
            set { _tipoApoderado = value; }
        }

        private SubTipoApoderado _stipoApoderado;

        public SubTipoApoderado StipoApoderado
        {
            get { return _stipoApoderado; }
            set { _stipoApoderado = value; }
        }

        private string _comentario;

        public string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        private Banco _banco;

        public Banco Banco
        {
            get { return _banco; }
            set { _banco = value; }
        }

        private DateTime _fAlta;

        public DateTime FAlta
        {
            get { return _fAlta; }
            set { _fAlta = value; }
        } 

        private DateTime? _fbaja;

        public DateTime? Fbaja
        {
            get { return _fbaja; }
            set { _fbaja = value; }
        }

        
        #endregion Variables de la instancia



        /**
         * Constructores
         * */
        #region Constructores
        public Apoderado()
        {
        }
        public Apoderado(string numDoc
            , Int16 codDocumento
            , string abrevDoc
            , string descripcionDoc
            , string apeNom
            , string DirCalle
            , string DirNum
            , string entreCalle1
            , string entreCalle2
            , string piso
            , string departamento
            , Int32 ? codLocalidad
            , string codPostal
            , string ciudad
            , string sexo
            , string telefono
            , string email
            , DirUbicacion ubicacion)
        {
            _numDoc = numDoc;
            _codigoDocumento = codDocumento;
            _abrevDoc = abrevDoc;
            _descDoc = descripcionDoc;
            _apeNom = apeNom;
            _DirCalle = DirCalle;
            _DirNum = DirNum;
            _entreCalle1 = entreCalle1;
            _entreCalle2 = entreCalle2;
            _piso = piso;
            _departamento = departamento;
            _codLocalidad = codLocalidad;
            _cod_postal = codPostal;
            _ciudad = ciudad;
            _Sexo = sexo;
            _Telefono = telefono;
            _EMail = email;
            _dirUbicacion = ubicacion;
        }

        public Apoderado(string numDoc
            , Int16 codDocumento
            , string abrevDoc
            , string descripcionDoc
            , string apeNom
            , string DirCalle
            , string DirNum
            , string entreCalle1
            , string entreCalle2
            , string piso
            , string departamento
            , Int32? codLocalidad
            , string codPostal
            , string ciudad
            , string sexo
            , string telefono
            , string email
            , DirUbicacion ubicacion
            , TipoApoderado tipoApoderado
            , SubTipoApoderado stipoApoderado
            , String comentario
            , Banco banco
            , DateTime fAlta
            , DateTime? fbaja
        )
        {
            _numDoc = numDoc;
            _codigoDocumento = codDocumento;
            _abrevDoc = abrevDoc;
            _descDoc = descripcionDoc;
            _apeNom = apeNom;
            _DirCalle = DirCalle;
            _DirNum = DirNum;
            _entreCalle1 = entreCalle1;
            _entreCalle2 = entreCalle2;
            _piso = piso;
            _departamento = departamento;
            _codLocalidad = codLocalidad;
            _cod_postal = codPostal;
            _ciudad = ciudad;
            _Sexo = sexo;
            _Telefono = telefono;
            _EMail = email;
            _dirUbicacion = ubicacion;
            _banco = banco;
            _comentario = comentario;
            _tipoApoderado = tipoApoderado;
            _stipoApoderado = stipoApoderado;
            _fAlta = fAlta;
            _fbaja = fbaja;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Causante
    [Serializable()]
    public class Causante : Disposed
    {

        #region Dispose

        ~Causante()
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
        private DateTime _Fecha_Def;

        public DateTime Fecha_Def
        {
            get { return _Fecha_Def; }
            set { _Fecha_Def = value; }
        }

        private DateTime? _Fecha_Nacimiento;

        public DateTime? Fecha_Nacimiento
        {
            get { return _Fecha_Nacimiento; }
            set { _Fecha_Nacimiento = value; }
        }

        private string _cuip;

        public string Cuip
        {
            get { return _cuip; }
            set { _cuip = value; }
        }

        private string _apeNom;

        public string ApeNom
        {
            get { return _apeNom; }
            set { _apeNom = value; }
        }

        private string _Sexo;

        public string Sexo
        {
            get { return _Sexo; }
            set { _Sexo = value; }
        }

        private List<Documento_Causante> _LDocCausante;

        public List<Documento_Causante> LDocCausante
        {
            get { return _LDocCausante; }
            set { _LDocCausante = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Causante()
        {
        }
        public Causante(Int64 id_causante
            , string apeNom
            , string cuip
            , string Sexo
            , DateTime fechaDef
            , DateTime? fechaNaciomiento
            , List<Documento_Causante> iLDocCausante)
        {
            this._id_causante = id_causante;
            this._Fecha_Def = fechaDef;
            this._Fecha_Nacimiento = fechaNaciomiento;
            this._apeNom = apeNom;
            this._cuip = cuip;
            this._Sexo = Sexo;
            this._LDocCausante = iLDocCausante;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base LsBeneficiario
    [Serializable()]
    public class LsBeneficiario : Disposed
    {

        #region Dispose

        ~LsBeneficiario()
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

        private String _apeNom;

        public String apeNom
        {
          get { return _apeNom; }
          set { _apeNom = value; }
        }

        private string _ApellidoMaterno;

        public string ApellidoMaterno
        {
            get { return _ApellidoMaterno; }
            set { _ApellidoMaterno = value; }
        }



        private String _expedienteExt;

        public String ExpedienteExt
        {
          get { return _expedienteExt; }
          set { _expedienteExt = value; }
        }

        private string _documento;

        public string Documento
        {
            get { return _documento; }
            set { _documento = value; }
        }

        
        private String _sexo;

        public String Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }

        private DateTime? _fecha_nac;

        public DateTime? Fecha_nac
        {
            get { return _fecha_nac; }
            set { _fecha_nac = value; }
        }

        #endregion Variables de la instancia

        #region Constructores
        public LsBeneficiario()
        {
        }
        public LsBeneficiario(Int64 id_beneficiario, string apeNom, string apellidoMaterno
            , string documento, string ExpedienteExt, string sexo, DateTime? fecha_nac)
        {
            this._id_Beneficiario = id_beneficiario;
            this._apeNom = apeNom;
            this._expedienteExt = ExpedienteExt;
            this._sexo = sexo;
            this._fecha_nac = fecha_nac;
            this._ApellidoMaterno = apellidoMaterno;
            this._documento = documento;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base BeneficiarioNotas
    [Serializable()]
    public class BeneficiarioNotas : Disposed
    {

        #region Dispose

        ~BeneficiarioNotas()
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

        private DateTime _fRecepcion;

        public DateTime FRecepcion
        {
          get { return _fRecepcion; }
          set { _fRecepcion = value; }
        }

        private String _Asunto;

        public String Asunto
        {
            get { return _Asunto; }
            set { _Asunto = value; }
        }

        private String _nroNota;

        

        public String NroNota
        {
          get { return _nroNota; }
          set { _nroNota = value; }
        }
        private String _Descripcion;

        public String Descripcion
        {
          get { return _Descripcion; }
          set { _Descripcion = value; }
        }
        

        #endregion Variables de la instancia

        #region Constructores
        public BeneficiarioNotas()
        {
        }
        public BeneficiarioNotas(Int64 id_beneficiario,  DateTime fRecepcion, string nroNota, string asunto, string descripcion)
        {
            this._id_Beneficiario = id_beneficiario;
            this._fRecepcion = fRecepcion;
            this._Asunto = asunto;
            this._Descripcion = descripcion;
            this._nroNota = nroNota;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base DireccionExtranjeraBeneficiario
    [Serializable()]
    public class iDireccionExtranjera : Disposed
    {

        #region Dispose

        ~iDireccionExtranjera()
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

        private string _estado;

        public string Estado
        {
          get { return _estado; }
          set { _estado = value; }
        }

        private string _CountryCode;

        public string CountryCode
        {
          get { return _CountryCode; }
          set { _CountryCode = value; }
        }

        private int? _idCiudad;

        public int? IdCiudad
        {
          get { return _idCiudad; }
          set { _idCiudad = value; }
        }

        private string _ciudad;

        public string Ciudad
        {
          get { return _ciudad; }
          set { _ciudad = value; }
        }

        private string _dircalle;

        public string Dircalle
        {
          get { return _dircalle; }
          set { _dircalle = value; }
        }

        private string _dirnum;

        public string Dirnum
        {
          get { return _dirnum; }
          set { _dirnum = value; }
        }

        private string _ecalle1;

        public string Ecalle1
        {
          get { return _ecalle1; }
          set { _ecalle1 = value; }
        }

        private string _ecalle2;

        public string Ecalle2
        {
          get { return _ecalle2; }
          set { _ecalle2 = value; }
        }

        private string _piso;

        public string Piso
        {
          get { return _piso; }
          set { _piso = value; }
        }

        private string _depto;

        public string Depto
        {
          get { return _depto; }
          set { _depto = value; }
        }

        private string _codPostal;

        public string CodPostal
        {
          get { return _codPostal; }
          set { _codPostal = value; }
        }

        #endregion Variables de la instancia

        #region Constructores
        public iDireccionExtranjera()
        {
        }
        public iDireccionExtranjera(Int64 id_beneficiario, string estado, string codPais3C, int? codCiudad, string ciudad, string dircalle, string dirnum, string ecalle1, string ecalle2, string piso, string depto, string codpostal)
        {
            this._id_Beneficiario = id_beneficiario;
            this._ciudad = ciudad;
            this._codPostal = codpostal;
            this._CountryCode = codPais3C;
            this._depto = depto;
            this._dircalle = dircalle;
            this._dirnum = dirnum;
            this._ecalle1 = ecalle1;
            this._ecalle2 = ecalle2;
            this._estado = estado;
            this._idCiudad = codCiudad;
            this._piso = piso;
        }

        #endregion Constructores
    }

    [Serializable()]
    public class oDireccionExtranjera : iDireccionExtranjera
    {

        #region Dispose

        ~oDireccionExtranjera()
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
        private string  _nomPais;

        public string NomPais
        {
          get { return _nomPais; }
          set { _nomPais = value; }
        }

        private string _distrito;

        public string Distrito
        {
          get { return _distrito; }
          set { _distrito = value; }
        }

        private string _nomCiudad;

        public string NomCiudad
        {
          get { return _nomCiudad; }
          set { _nomCiudad = value; }
        }


        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public oDireccionExtranjera()
        {
        }
        public oDireccionExtranjera(Int64 id_beneficiario
            , string estado
            , string codPais3C
            , int? codCiudad
            , string ciudad
            , string dircalle
            , string dirnum
            , string ecalle1
            , string ecalle2
            , string piso
            , string depto
            , string codpostal
            , string nomPais
            , string nomCiudad
            , string distrito)
        {
            base.Id_Beneficiario = id_beneficiario;
            base.Ciudad = ciudad;
            base.CodPostal = codpostal;
            base.CountryCode = codPais3C;
            base.Depto = depto;
            base.Dircalle = dircalle;
            base.Dirnum = dirnum;
            base.Ecalle1 = ecalle1;
            base.Ecalle2 = ecalle2;
            base.Estado = estado;
            base.IdCiudad = codCiudad;
            base.Piso = piso;
            this._distrito = distrito;
            this._nomCiudad = nomCiudad;
            this._nomPais = nomPais;
        }

        #endregion Constructores
    }
    #endregion Clase Base
}
