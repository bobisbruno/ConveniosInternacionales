using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActoresWS;

using System.Configuration;

/// <summary>
/// Descripción breve de DictamenTotal
/// </summary>
public partial class DocumentacioIE : System.Web.UI.Page
{
    private TipoDocumentacion _TipoDocumentacion;

    public TipoDocumentacion TipoDocumentacion
    {
      get { return _TipoDocumentacion; }
      set { _TipoDocumentacion = value; }
    }

    private String _ComentarioIngreso;

    public String ComentarioIngreso
    {
      get { return _ComentarioIngreso; }
      set { _ComentarioIngreso = value; }
    }

    private List<HttpPostedFile> _lArchivo;

    public List<HttpPostedFile> LArchivo
    {
      get { return _lArchivo; }
      set { _lArchivo = value; }
    }


    public DocumentacioIE()
    {
        this._TipoDocumentacion = null;
        this._ComentarioIngreso = null;
        this._lArchivo = null;
    }

    public DocumentacioIE(TipoDocumentacion iTipoDocumentacion, String comentario, List<HttpPostedFile> lArchivos)
    {
        this._TipoDocumentacion = iTipoDocumentacion;
        this._ComentarioIngreso = comentario;
        this._lArchivo = lArchivos;
    }
}
