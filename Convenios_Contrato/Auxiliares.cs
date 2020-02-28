using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Ar.Gov.Anses.Microinformatica.ConveniosX5.Contrato
{
    #region Clase Base TipoIngreso
    [Serializable()]
    public class TiposTramiteConvenios : Disposed
    {

        #region Dispose

        ~TiposTramiteConvenios()
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
        private String _tipoTram;

        public String TipoTram
        {
          get { return _tipoTram; }
          set { _tipoTram = value; }
        }
        private String _comentario;

        public String Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public TiposTramiteConvenios()
        {
        }
        public TiposTramiteConvenios(string tipoTram, string comentario)
        {
            this._tipoTram = tipoTram;
            this._comentario = comentario;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base TipoPoder
    [Serializable()]
    public class TipoPoder : Disposed
    {

        #region Dispose

        ~TipoPoder()
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
        private Byte _id_TipoPoder;

        public Byte Id_TipoPoder
        {
            get { return _id_TipoPoder; }
            set { _id_TipoPoder = value; }
        }
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        
        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public TipoPoder()
        {
        }
        public TipoPoder(byte idTipoPoder, string descripcion)
        {
            this._id_TipoPoder = idTipoPoder;
            this._descripcion = descripcion;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Tramites_derivados
    [Serializable()]
    public class Tramitesderivados : Disposed
    {

        #region Dispose

        ~Tramitesderivados()
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
        private Byte _idtipoTrDerivado;

        public Byte IdTipoTrDerivado
        {
            get { return _idtipoTrDerivado; }
            set { _idtipoTrDerivado = value; }
        }
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Tramitesderivados()
        {
        }
        public Tramitesderivados(byte idtipoTrDerivado, string descripcion)
        {
            this._idtipoTrDerivado = idtipoTrDerivado;
            this._descripcion = descripcion;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base TipoIngreso
    [Serializable()]
    public class TipoIngreso : Disposed
    {

        #region Dispose

        ~TipoIngreso()
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
        private Byte _idTipoIngreso;

        public Byte IdTipoIngreso
        {
          get { return _idTipoIngreso; }
          set { _idTipoIngreso = value; }
        }

        
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public TipoIngreso()
        {
        }
        public TipoIngreso(byte idTipoIngreso, string descripcion)
        {
            this._idTipoIngreso = idTipoIngreso;
            this._descripcion = descripcion;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base TipoDocumento
    [Serializable()]
    public class TipoDocumento : Disposed
    {

        #region Dispose

        ~TipoDocumento()
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
        private Int16 _codigoDocumento;

        public Int16 CodigoDocumento
        {
            get { return _codigoDocumento; }
            set { _codigoDocumento = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
          get { return _descripcion; }
          set { _descripcion = value; }
        }

        private string _abreviatura;

        public string Abreviatura
        {
            get { return _abreviatura; }
            set { _abreviatura = value; }
        }

        private Boolean _frecuente;

        public Boolean Frecuente
        {
          get { return _frecuente; }
          set { _frecuente = value; }
        }

        
        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public TipoDocumento()
        {
        }
        public TipoDocumento(Int16 codigoDocumento, string abreviatura, string descripcion, bool frecuente)
        {
            this._codigoDocumento = codigoDocumento;
            this._descripcion = descripcion;
            this._frecuente = frecuente;
            this._abreviatura = abreviatura;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base TipoDocumentacion
    [Serializable()]
    public class TipoDocumentacion : Disposed
    {

        #region Dispose

        ~TipoDocumentacion()
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
        private Int32 _codTipoDocumentacion;

        public Int32 CodTipoDocumentacion
        {
          get { return _codTipoDocumentacion; }
          set { _codTipoDocumentacion = value; }
        }
        
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public TipoDocumentacion()
        {
        }
        public TipoDocumentacion(Int32 codTipoDocumentacion, string descripcion)
        {
            this._codTipoDocumentacion = codTipoDocumentacion;
            this._descripcion = descripcion;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base TipoDocumentacion_Prestacion
    [Serializable()]
    public class TipoDocumentacion_Prestacion : Disposed
    {

        #region Dispose

        ~TipoDocumentacion_Prestacion()
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

        private Prestacion _Prestacion;

        public Prestacion Prestacion
        {
            get { return _Prestacion; }
            set { _Prestacion = value; }
        }

        private TipoDocumentacion _tDocumentacion;

        public TipoDocumentacion TDocumentacion
        {
            get { return _tDocumentacion; }
            set { _tDocumentacion = value; }
        }

        private Boolean _requeridoInicioTramite;

        public Boolean RequeridoInicioTramite
        {
            get { return _requeridoInicioTramite; }
            set { _requeridoInicioTramite = value; }
        }

        private string _comentario;

        public string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }
        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public TipoDocumentacion_Prestacion()
        {
        }
        public TipoDocumentacion_Prestacion(Prestacion Prestacion, TipoDocumentacion TDocumentacion , Boolean RequeridoInicioTramite, string comentario)
        {
            this._Prestacion = Prestacion;
            this._tDocumentacion = TDocumentacion;
            this._requeridoInicioTramite = RequeridoInicioTramite;
            this._comentario = comentario;            
        }

        #endregion Constructores
    }
    #endregion Clase Base


    #region Clase Base TipoApoderado
    [Serializable()]
    public class TipoApoderado : Disposed
    {

        #region Dispose

        ~TipoApoderado()
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
        private Byte _id_TipodeApoderado;

        public Byte Id_TipodeApoderado
        {
          get { return _id_TipodeApoderado; }
          set { _id_TipodeApoderado = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }


        private bool _poderTramitar;

        public bool PoderTramitar
        {
            get { return _poderTramitar; }
            set { _poderTramitar = value; }
        }

        private bool _poderPercibir;

        public bool PoderPercibir
        {
            get { return _poderPercibir; }
            set { _poderPercibir = value; }
        }



        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public TipoApoderado()
        {
        }
        public TipoApoderado(Byte idTipoApoderado, string descripcion, bool tramitar, bool percibir)
        {
            this._id_TipodeApoderado = idTipoApoderado;
            this._descripcion = descripcion;
            this._poderTramitar = tramitar;
            this._poderPercibir = percibir;
        }

        #endregion Constructores
    }

    [Serializable()]
    public class SubTipoApoderado : Disposed
    {

        #region Dispose

        ~SubTipoApoderado()
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
        private Byte _id_STipodeApoderado;

        public Byte Id_STipodeApoderado
        {
            get { return _id_STipodeApoderado; }
            set { _id_STipodeApoderado = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public SubTipoApoderado()
        {
        }
        public SubTipoApoderado(Byte idSTipoApoderado, string descripcion)
        {
            this._id_STipodeApoderado = idSTipoApoderado;
            this._descripcion = descripcion;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Sector
    [Serializable()]
    public class Sector : Disposed
    {

        #region Dispose

        ~Sector()
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
        private Int32 _cod_sector;

        public Int32 Cod_sector
        {
          get { return _cod_sector; }
          set { _cod_sector = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }



        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Sector()
        {
        }
        public Sector(Int32 codSectdor, string descripcion)
        {
            this._cod_sector = codSectdor;
            this._descripcion = descripcion;
        }

        #endregion Constructores
    }
    #endregion Clase Base


    #region Clase Base DirUbicacion
    [Serializable()]
    public class DirUbicacion : Disposed
    {

        #region Dispose

        ~DirUbicacion()
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
        private Int32 _codlocalidad;

        public Int32 Cod_Localidad
        {
            get { return _codlocalidad; }
            set { _codlocalidad = value; }
        }

        private string _descripcionLocalidad;

        public string DescripcionLocalidad
        {
            get { return _descripcionLocalidad; }
            set { _descripcionLocalidad = value; }
        }


        private Int16 _cod_Provincia;

        public Int16 Cod_Provincia
        {
          get { return _cod_Provincia; }
          set { _cod_Provincia = value; }
        }

        private string _descripcionProvincia;

        public string DescripcionProvincia
        {
          get { return _descripcionProvincia; }
          set { _descripcionProvincia = value; }
        }
        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public DirUbicacion()
        {
        }
        public DirUbicacion(Int32 codLocalidad, string descripcionLocalidad, Int16 codProvincia, string descripcionProvincia)
        {
            this._cod_Provincia = codProvincia;
            this._descripcionLocalidad = descripcionLocalidad;
            this._codlocalidad = codLocalidad;
            this._descripcionProvincia = descripcionProvincia;
        }

        #endregion Constructores
    }
    #endregion Clase Base


    #region Clase Base Motivo denegacion
    [Serializable()]
    public class MotivoDenegacion : Disposed
    {

        #region Dispose

        ~MotivoDenegacion()
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
        

        private Int16 _codMotivo;

        public Int16 CodMotivo
        {
            get { return _codMotivo; }
            set { _codMotivo = value; }
        }

        private String _descripcion;

        public String Descripcion
        {
          get { return _descripcion; }
          set { _descripcion = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public MotivoDenegacion()
        {
        }
        public MotivoDenegacion(Int16 codmotivo, string descripcion)
        {
            this._codMotivo = codmotivo;
            this._descripcion = descripcion;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Localidad
    [Serializable()]
    public class Localidad : Disposed
    {

        #region Dispose

        ~Localidad()
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
        private Int32 _codlocalidad;

        public Int32 Cod_Localidad
        {
            get { return _codlocalidad; }
            set { _codlocalidad = value; }
        }

        private string _descripcionLocalidad;

        public string DescripcionLocalidad
        {
            get { return _descripcionLocalidad; }
            set { _descripcionLocalidad = value; }
        }


        private string _codPostal;

        public string CodPostal
        {
            get { return _codPostal; }
            set { _codPostal = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Localidad()
        {
        }
        public Localidad(Int32 codLocalidad, string descripcionLocalidad, string codPostal)
        {
            this._descripcionLocalidad = descripcionLocalidad;
            this._codlocalidad = codLocalidad;
            this._codPostal = codPostal;
        }

        #endregion Constructores
    }
    #endregion Clase Base



    #region Clase Base Provincia
    [Serializable()]
    public class Provincia : Disposed
    {

        #region Dispose

        ~Provincia()
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
        private Int16 _codProvincia;

        public Int16 CodProvincia
        {
            get { return _codProvincia; }
            set { _codProvincia = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Provincia()
        {
        }
        public Provincia(Int16 codProvincia, string descripcion)
        {
            this._descripcion = descripcion;
            this._codProvincia = codProvincia;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Prestacion
    [Serializable()]
    public class Prestacion : Disposed
    {

        #region Dispose

        ~Prestacion()
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
        private Int16 _codPrestacion;

        public Int16 Cod_Prestacion
        {
            get { return _codPrestacion; }
            set { _codPrestacion = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        //private List<TipoDocumentacion_Prestacion> _lTipoDocumentacionPrestacion;

        //public List<TipoDocumentacion_Prestacion> LTipoDocumentacionPrestacion
        //{
        //    get { return _lTipoDocumentacionPrestacion; }
        //    set { _lTipoDocumentacionPrestacion = value; }
        //}

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Prestacion()
        {
        }
        public Prestacion(Int16 codPrestacion, string descripcion)
        {
            this._codPrestacion = codPrestacion;
            this._descripcion = descripcion;
        }

        //public Prestacion(Int16 codPrestacion, string descripcion, List<TipoDocumentacion_Prestacion> ilTipoDocumentacionPrestacion)
        //{
        //    this._codPrestacion = codPrestacion;
        //    this._descripcion = descripcion;
        //    this._lTipoDocumentacionPrestacion = ilTipoDocumentacionPrestacion;
        //}

        #endregion Constructores
    }
    #endregion Clase Base


    #region Clase Base Pais
    [Serializable()]
    public class Pais : Disposed
    {

        #region Dispose

        ~Pais()
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
        private Int16 _pais_PK;

        public Int16 Pais_PK
        {
          get { return _pais_PK; }
          set { _pais_PK = value; }
        }
        
        private string _descripcion;

        public string Descripcion
        {
          get { return _descripcion; }
          set { _descripcion = value; }
        }

        private string _gentilicio;

        public string Gentilicio
        {
            get { return _gentilicio; }
            set { _gentilicio = value; }
        }



        private Boolean _conConvenio;

        public Boolean ConConvenio
        {
          get { return _conConvenio; }
          set { _conConvenio = value; }
        }

        private Boolean _mercosur;

        public Boolean Mercosur
        {
            get { return _mercosur; }
            set { _mercosur = value; }
        }

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Pais()
        {
        }
        public Pais(Int16 pais_PK, string descripcion, string gentilicio, Boolean conConvenio, Boolean mercosur)
        {
            this._pais_PK = pais_PK;
            this._descripcion = descripcion;
            this._conConvenio = conConvenio;
            this._mercosur = mercosur;
            this._gentilicio = gentilicio;
        }

        #endregion Constructores
    }

    [Serializable()]
    public class Pais2 : Disposed
    {

        #region Dispose

        ~Pais2()
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
        private string _Code;

        public string Codigo3C
        {
          get { return _Code; }
          set { _Code = value; }
        }
        private string _Name;

        public string Nombre
        {
          get { return _Name; }
          set { _Name = value; }
        }
        private string _Continent;

        public string Continente
        {
          get { return _Continent; }
          set { _Continent = value; }
        }
        private string _Region;

        public string Region
        {
          get { return _Region; }
          set { _Region = value; }
        }
        private string _LocalName;

        public string LocalName
        {
          get { return _LocalName; }
          set { _LocalName = value; }
        }
        private string _GovernmentForm;

        public string GovernmentForm
        {
          get { return _GovernmentForm; }
          set { _GovernmentForm = value; }
        }
        private Int32? _Capital;

        public Int32? Capital
        {
          get { return _Capital; }
          set { _Capital = value; }
        }
        private string _Code2;

        public string Codigo2C
        {
          get { return _Code2; }
          set { _Code2 = value; }
        }
        

        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Pais2()
        {
        }
        public Pais2(string Codigo3c, string nombre, string continente, string region, string nombrelocal, string formaGobierno, Int32? capital, string codigo2C)
        {
            this._Capital = capital;
            this._Code = Codigo3c;
            this._Code2 = codigo2C;
            this._Continent = continente;
            this._GovernmentForm = formaGobierno;
            this._LocalName = nombrelocal;
            this._Name = nombre;
            this._Region = region;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Ciudad
    [Serializable()]
    public class Ciudad : Disposed
    {

        #region Dispose

        ~Ciudad()
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
        private int _id;

        public int Id
        {
          get { return _id; }
          set { _id = value; }
        }

        private string _nombre;

        public string Nombre
        {
          get { return _nombre; }
          set { _nombre = value; }
        }

        private string _codPais3C;

        public string CodPais3C
        {
          get { return _codPais3C; }
          set { _codPais3C = value; }
        }

        private string _District;

        public string District
        {
          get { return _District; }
          set { _District = value; }
        }

        private string _estadoCiudad;

        public string EstadoCiudad
        {
            get { return _estadoCiudad; }
            set { _estadoCiudad = value; }
        }



        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Ciudad()
        {
        }
        public Ciudad(int ID, string Codigo3c, string nombre, string distrito, string estadoCiudad)
        {
            this._codPais3C = Codigo3c;
            this._District = distrito;
            this._id = ID;
            this._nombre = nombre;
            this._estadoCiudad = estadoCiudad;
        }

        #endregion Constructores
    }

    #endregion Clase Base Ciudad

  
    #region Clase Base Estado
    [Serializable()]
    public class Estado : Disposed
    {

        #region Dispose

        ~Estado()
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
        private Int32 _cod_estado;

        public Int32 Cod_estado
        {
            get { return _cod_estado; }
            set { _cod_estado = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }



        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Estado()
        {
        }
        public Estado(Int32 codEstado, string descripcion)
        {
            this._cod_estado = codEstado;
            this._descripcion = descripcion;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    #region Clase Base Banco
    [Serializable()]
    public class Banco : Disposed
    {

        #region Dispose

        ~Banco()
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
        private Int16 _id_Banco;

        public Int16 Id_Banco
        {
            get { return _id_Banco; }
            set { _id_Banco = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private Boolean _frecuente;

        public Boolean Frecuente
        {
          get { return _frecuente; }
          set { _frecuente = value; }
        }


        private string _webSite;

        public string WebSite
        {
          get { return _webSite; }
          set { _webSite = value; }
        }



        #endregion Variables de la instancia


        /**
         * Constructores
         * */
        #region Constructores
        public Banco()
        {
        }
        public Banco(Int16 idBanco, string descripcion, bool frecuente, string webSite)
        {
            this._id_Banco = idBanco;
            this._descripcion = descripcion;
            this._frecuente = frecuente;
            this._webSite = webSite;
        }

        #endregion Constructores
    }
    #endregion Clase Base

    public enum TipoConsultaBeneficioario
    {
        NombreoApellidos = 0,
        Documento = 1,
        CodigoSIACI = 2,
        DocumentoYTipo = 3,
        Expediente = 4,
        Beneficio = 5,
        CUIP = 6,
        Tramite = 7
    }

    public enum TipoIngresoProvisorio
    {
        Ingreso = 0,
        Devolucion = 1
    }
    
}
