using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActoresWS;
//using System.Collections;
//using System.Collections.Generic;
//using EnviosWS;
using System.Data;
using ConsultasWS;
using System.ComponentModel;
using ObtenerDatosxDocumento;
using ObtenerDatosxApeyNom;


/// <summary>
/// Summary description for ToDatatable
/// </summary>
public class ToDatatable
{
	public ToDatatable()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region ToDatatable

    //public static DataTable toDataTable(ADPWS.DatosPw02[] iParam)
    //{
    //    DataTable _dt = new DataTable();
    //    _dt.Columns.Add("ape_nom", typeof(String));
    //    _dt.Columns.Add("cuil", typeof(String));
    //    _dt.Columns.Add("doc_c_tipo", typeof(short));
    //    _dt.Columns.Add("doc_da_tipo", typeof(String));
    //    _dt.Columns.Add("doc_nro", typeof(String));
    //    _dt.Columns.Add("estado", typeof(String));
    //    _dt.Columns.Add("f_naci", typeof(String));
    //    _dt.Columns.Add("sexo", typeof(String));
        

    //    foreach (ADPWS.DatosPw02 oDat in iParam)
    //    {
    //        DataRow _drTemp;
    //        _drTemp = _dt.NewRow();
    //        _drTemp["ape_nom"] = oDat.ape_nom;
    //        _drTemp["cuil"] = oDat.cuil;
    //        _drTemp["doc_c_tipo"] = oDat.doc_c_tipo;
    //        _drTemp["doc_da_tipo"] = oDat.doc_da_tipo.ToUpper();
    //        _drTemp["doc_nro"] = oDat.doc_nro;
    //        _drTemp["estado"] = oDat.estado.ToUpper();
    //        if(oDat.f_naci == "" || oDat.f_naci.Length != 8)
    //            _drTemp["f_naci"] = "";
    //        else
    //        {
    //            if (Util.esFechaValida(oDat.f_naci.Substring(0, 2) + "/" + oDat.f_naci.Substring(2, 2) + "/" + oDat.f_naci.Substring(4, 4)))
    //                _drTemp["f_naci"] = oDat.f_naci.Substring(0, 2) + "/" + oDat.f_naci.Substring(2, 2) + "/" + oDat.f_naci.Substring(4, 4);
    //            else
    //                _drTemp["f_naci"] = "";

    //        }
            
    //        _drTemp["sexo"] = oDat.sexo.ToUpper();

    //        _dt.Rows.Add(_drTemp);
    //    }
    //    return _dt;
    //}

    public static DataTable toDataTable(List<ActoresWS.TipoDocumentacion> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("CodTipoDocumentacion", typeof(Int32));
        _dt.Columns.Add("Descripcion", typeof(String));
        
        foreach (ActoresWS.TipoDocumentacion oTipo in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["CodTipoDocumentacion"] = oTipo.CodTipoDocumentacion;
            _drTemp["Descripcion"] = oTipo.Descripcion;
            
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    public static DataTable toDataTable(List<AuxiliaresWS.TipoDocumentacion> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("CodTipoDocumentacion", typeof(Int32));
        _dt.Columns.Add("Descripcion", typeof(String));
        
        foreach (AuxiliaresWS.TipoDocumentacion oTdoc in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["CodTipoDocumentacion"] = oTdoc.CodTipoDocumentacion;
            _drTemp["Descripcion"] = oTdoc.Descripcion;
            
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    public static DataTable toDataTable(List<Documento_Beneficiario> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("CodAbrevPais", typeof(String));
        _dt.Columns.Add("CodigoDocumento", typeof(Int16));
        _dt.Columns.Add("AbrevDocumento", typeof(String));
        _dt.Columns.Add("FechaAlta", typeof(String));
        _dt.Columns.Add("FechaBaja", typeof(String));
        _dt.Columns.Add("NumDoc", typeof(String));


        foreach (Documento_Beneficiario oDoc in iParam)
        {
            if (oDoc.FechaBaja == null)
            {
                DataRow _drTemp;
                _drTemp = _dt.NewRow();
                _drTemp["CodAbrevPais"] = oDoc.CodAbrevPais;
                _drTemp["CodigoDocumento"] = oDoc.CodigoDocumento;
                _drTemp["AbrevDocumento"] = oDoc.AbrevDocumento;
                _drTemp["FechaAlta"] = oDoc.FechaAlta.Date.ToShortDateString();
                //_drTemp["FechaBaja"] = oDoc.FechaBaja == null ? "" : oDoc.FechaBaja.Value.ToShortDateString();
                _drTemp["FechaBaja"] = "";
                _drTemp["NumDoc"] = oDoc.NumDoc;

                _dt.Rows.Add(_drTemp);
            }
        }
        return _dt;
    }

    public static DataTable toDataTable(List<Devolucion> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("Certificado", typeof(String));
        _dt.Columns.Add("Destino", typeof(String));
        _dt.Columns.Add("FechaMovimiento", typeof(String));
        
        foreach (Devolucion oDev in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Certificado"] = oDev.Certificado;
            _drTemp["Destino"] = oDev.Destino;
            _drTemp["FechaMovimiento"] = oDev.FechaMovimiento.ToShortDateString();
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }


    public static DataTable toDataTable(List<BeneficiarioNotas> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("Asunto", typeof(String));
        _dt.Columns.Add("Descripcion", typeof(String));
        _dt.Columns.Add("FRecepcion", typeof(String));
        _dt.Columns.Add("Id_Beneficiario", typeof(Int64));
        _dt.Columns.Add("NroNota", typeof(String));


        foreach (BeneficiarioNotas oNota in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Asunto"] = oNota.Asunto;
            _drTemp["Descripcion"] = oNota.Descripcion;
            _drTemp["FRecepcion"] = oNota.FRecepcion.ToShortDateString();
            _drTemp["Id_Beneficiario"] = oNota.Id_Beneficiario;
            _drTemp["NroNota"] = oNota.NroNota;

            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    
    public static DataTable toDataTable(List<Documento_Causante> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("CodAbrevPais", typeof(String));
        _dt.Columns.Add("CodigoDocumento", typeof(Int16));
        _dt.Columns.Add("AbrevDocumento", typeof(String));
        _dt.Columns.Add("FechaAlta", typeof(String));
        _dt.Columns.Add("FechaBaja", typeof(String));
        _dt.Columns.Add("NumDoc", typeof(String));


        foreach (Documento_Causante oDoc in iParam)
        {
            if (oDoc.FechaBaja == null)
            {
                DataRow _drTemp;
                _drTemp = _dt.NewRow();
                _drTemp["CodAbrevPais"] = oDoc.CodAbrevPais;
                _drTemp["CodigoDocumento"] = oDoc.CodigoDocumento;
                _drTemp["AbrevDocumento"] = oDoc.AbrevDocumento;
                _drTemp["FechaAlta"] = oDoc.FechaAlta.Date.ToShortDateString();
                //_drTemp["FechaBaja"] = oDoc.FechaBaja == null ? "" : oDoc.FechaBaja.Value.ToShortDateString();
                _drTemp["FechaBaja"] = "";
                _drTemp["NumDoc"] = oDoc.NumDoc;

                _dt.Rows.Add(_drTemp);
            }
        }
        return _dt;
    }

    public static DataTable toDataTable(List<BancoWS.Banco> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("Id_Banco", typeof(Int16));
        _dt.Columns.Add("Descripcion", typeof(String));
        _dt.Columns.Add("Frecuente", typeof(Boolean));
        _dt.Columns.Add("FrecuenteStr", typeof(String));
        _dt.Columns.Add("WebSite", typeof(String));

        foreach (BancoWS.Banco oBan in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Id_Banco"] = oBan.Id_Banco;
            _drTemp["Descripcion"] = oBan.Descripcion;
            _drTemp["Frecuente"] = oBan.Frecuente;
            _drTemp["FrecuenteStr"] = oBan.Frecuente ? "S":"N";
            _drTemp["WebSite"] = oBan.WebSite;

            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    
    public static DataTable toDataTable(List<AuxiliaresWS.TipoDocumentacion_Prestacion> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("CodTipoDocumentacion", typeof(Int32));
        _dt.Columns.Add("DescripcionTdoc", typeof(String));
        _dt.Columns.Add("CodPrestacion", typeof(Int32));
        _dt.Columns.Add("DescripcionPrestacion", typeof(String));
        _dt.Columns.Add("Comentario", typeof(String));
        _dt.Columns.Add("RequeridoInicioTramite", typeof(String));
        
        foreach (AuxiliaresWS.TipoDocumentacion_Prestacion oTipo in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["CodTipoDocumentacion"] = oTipo.TDocumentacion.CodTipoDocumentacion;
            _drTemp["DescripcionTdoc"] = oTipo.TDocumentacion.Descripcion;
            _drTemp["CodPrestacion"] = oTipo.Prestacion.Cod_Prestacion;
            _drTemp["DescripcionPrestacion"] = oTipo.Prestacion.Descripcion;
            _drTemp["Comentario"] = oTipo.Comentario;
            _drTemp["RequeridoInicioTramite"] = oTipo.RequeridoInicioTramite ? "S":"N";
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    public static DataTable toDataTable(List<ActoresWS.TipoDocumentacion_Prestacion> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("CodTipoDocumentacion", typeof(Int32));
        _dt.Columns.Add("Descripcion", typeof(String));
        _dt.Columns.Add("Comentario", typeof(String));
        _dt.Columns.Add("RequeridoInicioTramite", typeof(String));

        foreach (ActoresWS.TipoDocumentacion_Prestacion oTipo in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["CodTipoDocumentacion"] = oTipo.TDocumentacion.CodTipoDocumentacion;
            _drTemp["Descripcion"] = oTipo.TDocumentacion.Descripcion;
            _drTemp["Comentario"] = oTipo.Comentario;
            _drTemp["RequeridoInicioTramite"] = oTipo.RequeridoInicioTramite ? "Si" : "No";


            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    public static DataTable toDataTable(List<NotificacionesVencidas> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("Certificado", typeof(String));
        _dt.Columns.Add("CodPrestacion", typeof(Int16));
        _dt.Columns.Add("DescripcionPrestacion", typeof(String));
        _dt.Columns.Add("Destino", typeof(String));
        _dt.Columns.Add("FechaMovimiento", typeof(String));
        _dt.Columns.Add("FechaNotificacion", typeof(String));
        _dt.Columns.Add("Id_Beneficiario", typeof(Int64));
        _dt.Columns.Add("NomApe", typeof(String));
        _dt.Columns.Add("Observaciones", typeof(String));


        foreach (NotificacionesVencidas oNoti in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Certificado"] = oNoti.Certificado;
            _drTemp["CodPrestacion"] = oNoti.CodPrestacion;
            _drTemp["DescripcionPrestacion"] = oNoti.DescripcionPrestacion;
            _drTemp["Destino"] = oNoti.Destino;
            _drTemp["FechaMovimiento"] = oNoti.FechaMovimiento.ToShortDateString();
            _drTemp["FechaNotificacion"] = oNoti.FechaNotificacion.HasValue ? oNoti.FechaNotificacion.Value.ToShortDateString():"";
            _drTemp["Id_Beneficiario"] = oNoti.Id_Beneficiario;
            _drTemp["NomApe"] = oNoti.NomApe;
            _drTemp["Observaciones"] = oNoti.Observaciones;

            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }


    public static DataTable toDataTable(List<Apoderado> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("numDoc", typeof(String));
        _dt.Columns.Add("codDocumento", typeof(Int16));
        _dt.Columns.Add("AbrevDoc", typeof(string));
        _dt.Columns.Add("docCompleto", typeof(String));
        _dt.Columns.Add("ApeNom", typeof(String));
        _dt.Columns.Add("fAlta", typeof(String));
        _dt.Columns.Add("fBaja", typeof(String));
        _dt.Columns.Add("TipoApoderado", typeof(String));
        _dt.Columns.Add("TipoPoder", typeof(String));
        

        foreach (Apoderado oApo in iParam)
        {
            string tPoder = oApo.TipoApoderado.PoderTramitar ? "Tramitar" : "";
            tPoder += oApo.TipoApoderado.PoderPercibir ? " Percibir" : "";
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["numDoc"] = oApo.NumDoc;
            _drTemp["codDocumento"] = oApo.CodigoDocumento;
            _drTemp["AbrevDoc"] = oApo.AbrevDoc;
            _drTemp["docCompleto"] = oApo.NumDoc+"-"+oApo.AbrevDoc;
            _drTemp["ApeNom"] = oApo.ApeNom;
            _drTemp["fAlta"] = oApo.FAlta.ToShortDateString();
            _drTemp["fBaja"] = oApo.Fbaja.HasValue ? oApo.Fbaja.Value.ToShortDateString() : "";
            _drTemp["TipoApoderado"] = oApo.StipoApoderado.Descripcion + "-" + oApo.TipoApoderado.Descripcion;
            _drTemp["TipoPoder"] = tPoder;
            
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    public static DataTable toDataTable(List<PaisWS.Pais> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("ConConvenio", typeof(Boolean));
        _dt.Columns.Add("Descripcion", typeof(String));
        _dt.Columns.Add("Pais_PK", typeof(Int32));
        


        foreach (PaisWS.Pais oPais in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["ConConvenio"] = oPais.ConConvenio;
            _drTemp["Descripcion"] = oPais.Descripcion;
            _drTemp["Pais_PK"] = oPais.Pais_PK;
            
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

   

    public static DataTable toDataTable(List<ActoresWS.Expediente_Solicitud> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("numExpte", typeof(String));
        _dt.Columns.Add("fAltaexpediente", typeof(String));
        _dt.Columns.Add("caratula", typeof(String));
        _dt.Columns.Add("observaciones", typeof(String));

        foreach (ActoresWS.Expediente_Solicitud oExpte in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["numExpte"] = oExpte.Expediente_org + "-" + oExpte.Expediente_precu + "-" + oExpte.Expediente_doccu + "-" + oExpte.Expediente_digcu + "-" + oExpte.Expediente_ctipo + "-" + oExpte.Expediente_sec;
            _drTemp["fAltaexpediente"] = oExpte.FAltaexpediente.Date.ToShortDateString();
            _drTemp["caratula"] = oExpte.Caratula;
            _drTemp["observaciones"] = oExpte.Observacion;
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }


    public static DataTable toDataTable(List<ExpedienteWS.ExpedienteDTO> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("numExpte", typeof(String));
        _dt.Columns.Add("nBeneficio", typeof(String));
        _dt.Columns.Add("fAltaexpediente", typeof(String));
        _dt.Columns.Add("caratula", typeof(String));
        _dt.Columns.Add("estado", typeof(String));
        _dt.Columns.Add("estadoSentencia", typeof(String));
        _dt.Columns.Add("fechaAltaAFJP", typeof(String));
        _dt.Columns.Add("fechaProceso", typeof(String));
        _dt.Columns.Add("fechaVencimiento", typeof(String));
        _dt.Columns.Add("codigoOficinaAlta", typeof(String));
        _dt.Columns.Add("ultimaOficina", typeof(String));
        
        foreach (ExpedienteWS.ExpedienteDTO oExpte in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["numExpte"] = oExpte.organismo + "-" + oExpte.preCuil + "-" + oExpte.numeroDocumento + "-" + oExpte.digCuil + "-" + oExpte.tipoTramite + "-" + oExpte.secuencia;
            _drTemp["nBeneficio"] = oExpte.beneficioExCaja + "-" + oExpte.beneficioNumero + "-" + oExpte.beneficioTipo + "-" + oExpte.beneficioCoparticipe;
            _drTemp["fAltaexpediente"] = oExpte.fechaAlta.HasValue ? oExpte.fechaAlta.Value.ToShortDateString():"";
            _drTemp["caratula"] = oExpte.caratula;
            _drTemp["estado"] = oExpte.estado;
            _drTemp["estadoSentencia"] = oExpte.estadoSentencia;
            _drTemp["fechaAltaAFJP"] = oExpte.fechaAltaAFJP.HasValue ? oExpte.fechaAltaAFJP.Value.ToShortDateString() : "";
            _drTemp["fechaProceso"] = oExpte.fechaProceso.ToShortDateString();
            _drTemp["fechaVencimiento"] = oExpte.fechaVencimiento.ToShortDateString();
            _drTemp["codigoOficinaAlta"] = oExpte.codigoOficinaAlta;
            _drTemp["ultimaOficina"] = oExpte.ultimaOficina;
            
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    public static DataTable toDataTable(List<ActoresWS.Beneficio_Solicitud> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("numBeneficio", typeof(String));
        _dt.Columns.Add("fAltabeneficio", typeof(String));
        _dt.Columns.Add("observacion", typeof(String));
        _dt.Columns.Add("DTipoTrDerivado", typeof(String));

        
        foreach (ActoresWS.Beneficio_Solicitud oBenef in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["numBeneficio"] = oBenef.BenExCaja + "-" + oBenef.BenTipo + "-" + oBenef.BenNumero + "-" + oBenef.BenCopart + "-" + oBenef.BenDigVerif;
            _drTemp["fAltabeneficio"] = oBenef.FAltaBeneficio.ToShortDateString();
            _drTemp["observacion"] = oBenef.Observacion;
            _drTemp["DTipoTrDerivado"] = oBenef.DTipoTrDerivado;
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }


    //public static DataTable toDataTable(List<ActoresWS.SolicitudDenegada> iParam)
    //{
    //    DataTable _dt = new DataTable();
    //    _dt.Columns.Add("DescMotivo", typeof(String));
    //    _dt.Columns.Add("FechaDenegatoria", typeof(String));
    //    _dt.Columns.Add("Observaciones", typeof(String));


    //    foreach (ActoresWS.SolicitudDenegada oSol in iParam)
    //    {
    //        if (!oSol.FechaBajaDenegatoria.HasValue)
    //        {
    //            DataRow _drTemp;
    //            _drTemp = _dt.NewRow();
    //            _drTemp["DescMotivo"] = oSol.DescMotivo;
    //            _drTemp["FechaDenegatoria"] = oSol.FechaDenegatoria.ToShortDateString();
    //            _drTemp["Observaciones"] = oSol.Observaciones;
    //            _dt.Rows.Add(_drTemp);
    //        }   
    //    }
    //    return _dt;
    //}

    public static DataTable toDataTable(List<PrestacionBeneficiario> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("IdBeneficiario", typeof(String));
        _dt.Columns.Add("Mercosur", typeof(Boolean));
        _dt.Columns.Add("Pais", typeof(String));
        _dt.Columns.Add("Prestacion", typeof(String));
        _dt.Columns.Add("codPrestacion", typeof(Int16));
        _dt.Columns.Add("codPais", typeof(Int16));
        _dt.Columns.Add("col_Pais_Prestacion", typeof(String));
        _dt.Columns.Add("col_FMov_Estado_Sector", typeof(String));
        
        ConsultasWS.Movimiento_Solicitud oMov = null;
        foreach (PrestacionBeneficiario oPresBen in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["IdBeneficiario"] = oPresBen.IdBeneficiario.ToString();
            _drTemp["Mercosur"] = oPresBen.Mercosur;
            _drTemp["Pais"] = oPresBen.PaisDescCompleto;
            _drTemp["Prestacion"] = oPresBen.DescripcionPrestacion;
            _drTemp["codPrestacion"] = oPresBen.CodPrestacion;
            _drTemp["codPais"] = oPresBen.CodigoPais;
            _drTemp["col_Pais_Prestacion"] = oPresBen.PaisDescCompleto + " - " + oPresBen.DescripcionPrestacion;
            string merror = "";
            try
            {
                oMov = InvocaWsDao.TraeUltimoMovimientoSolicitud(oPresBen.IdBeneficiario, oPresBen.CodPrestacion, out merror);
                if (oMov != null && merror == "")
                    _drTemp["col_FMov_Estado_Sector"] = oMov.Fecha_Movimiento.ToShortDateString() + " - " + oMov.Estado.Descripcion + " - " + oMov.Sector.Descripcion;
                else
                    _drTemp["col_FMov_Estado_Sector"] = "";
            }
            catch (Exception)
            {
                _drTemp["col_FMov_Estado_Sector"] = "";
            }
            
            
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    public static DataTable toDataTable(List<Solicitud> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("IdBeneficiario", typeof(String));
        _dt.Columns.Add("fAMSolicitud", typeof(String));
        _dt.Columns.Add("Mercosur", typeof(Boolean));
        _dt.Columns.Add("FechaIngreso", typeof(String));
        _dt.Columns.Add("Pais", typeof(String));
        _dt.Columns.Add("Prestacion", typeof(String));
        _dt.Columns.Add("codPrestacion", typeof(Int16));
        _dt.Columns.Add("Referencia_exterior", typeof(String));
        _dt.Columns.Add("Ubicacion_Fisica", typeof(String));
        _dt.Columns.Add("Observaciones", typeof(String));
        _dt.Columns.Add("Denegada", typeof(String));
        
        foreach (Solicitud oSol in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["IdBeneficiario"] = oSol.IdBeneficiario.ToString();
            _drTemp["fAMSolicitud"] = oSol.FAMSolicitud.ToShortDateString(); 
            _drTemp["Mercosur"] = oSol.Mercosur;
            _drTemp["Pais"] = oSol.PaisDescCompleto;
            _drTemp["Prestacion"] = oSol.DescripcionPrestacion;
            _drTemp["codPrestacion"] = oSol.CodPrestacion;
            _drTemp["Referencia_exterior"] = oSol.Referencia_exterior;
            _drTemp["Ubicacion_Fisica"] = oSol.Ubicacion_Fisica;
            _drTemp["Denegada"] = oSol.DescripcionMotivo;
            _drTemp["Observaciones"] = oSol.Observaciones;
            _drTemp["FechaIngreso"] = oSol.FechaIngreso.HasValue ? oSol.FechaIngreso.Value.ToShortDateString() : "";
            
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    public static DataTable toDataTable(List<ActoresWS.IngDevMov> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("Fecha_Movimiento", typeof(String));
        _dt.Columns.Add("TipoMovimiento", typeof(String));
        _dt.Columns.Add("TipoIngreso", typeof(String));
        _dt.Columns.Add("Destino", typeof(String));
        _dt.Columns.Add("Estado", typeof(String));
        _dt.Columns.Add("Sector", typeof(String));
        _dt.Columns.Add("codPrestacion", typeof(Int16));


        foreach (ActoresWS.IngDevMov oMov in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Fecha_Movimiento"] = oMov.Fecha_Movimiento.ToShortDateString();
            _drTemp["TipoMovimiento"] = oMov.TipoMovimiento;
            _drTemp["TipoIngreso"] = oMov.TipoIngreso;
            _drTemp["Destino"] = oMov.Destino;
            _drTemp["Estado"] = oMov.Estado;
            _drTemp["Sector"] = oMov.Sector;
            _drTemp["codPrestacion"] = oMov.Cod_Prestacion;
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    public static DataTable toDataTable(List<ActoresWS.SolicitudProvisoriaExtendida> iParam)
    {
        DataTable _dt = new DataTable();
        //_dt.Columns.Add("IdBeneficiario", typeof(Int64));
        _dt.Columns.Add("ApellidoyNombre", typeof(String));
        _dt.Columns.Add("DocumentoyTipo", typeof(String));
        _dt.Columns.Add("Nro_SolicitudProvisoria", typeof(String));
        _dt.Columns.Add("FAltaProvisoria", typeof(String));
        _dt.Columns.Add("Referencia_Provisoria", typeof(String));
        _dt.Columns.Add("Sectorderiva", typeof(String));
        _dt.Columns.Add("Desc_Prestacion", typeof(String));
        _dt.Columns.Add("Desc_Pais", typeof(String));
        _dt.Columns.Add("IngresaDevuelve", typeof(String));
        _dt.Columns.Add("DocumentosIngresados", typeof(String));
        _dt.Columns.Add("DiasCaducidad", typeof(String));
        


        List<AuxiliaresWS.Prestacion> lp = InvocaWsDao.TraerPrestaciones();
        AuxiliaresWS.Prestacion p = null;


        foreach (ActoresWS.SolicitudProvisoria oSolPrev in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Nro_SolicitudProvisoria"] = oSolPrev.Nro_SolicitudProvisoria;
            _drTemp["ApellidoyNombre"] = oSolPrev.ApellildoynombreDeclarado;
            _drTemp["DocumentoyTipo"] = oSolPrev.DocumentoDeclarado.Equals(string.Empty) ? "" : oSolPrev.DocumentoDeclarado;
            _drTemp["FAltaProvisoria"] = oSolPrev.FAltaProvisoria.ToShortDateString();
            _drTemp["Referencia_Provisoria"] = oSolPrev.Referencia_Provisoria;
            _drTemp["Sectorderiva"] = oSolPrev.Sectorderiva == null ? "No derivado" : oSolPrev.Sectorderiva.Descripcion;
            _drTemp["IngresaDevuelve"] = oSolPrev.TIngresoProvisorio == TipoIngresoProvisorio.Ingreso ? "Ingreso" : "Devolución";

        
            _drTemp["Desc_Prestacion"] = oSolPrev.PrestacionSolicitada == null ? "No solicita" : oSolPrev.PrestacionSolicitada.Descripcion;
            _drTemp["Desc_Pais"] = oSolPrev.PaisConvenio == null ? "No define" : oSolPrev.PaisConvenio.Descripcion;
            _drTemp["DocumentosIngresados"] = oSolPrev.LMovimientos == null ? "No registra" : oSolPrev.LMovimientos.Count().ToString();

            _drTemp["DiasCaducidad"] = oSolPrev.DiasCaducidad.HasValue ? oSolPrev.DiasCaducidad.Value.ToString() : "";

            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }


    public static DataTable toDataTable(List<ActoresWS.SolicitudProvisoria> iParam)
    {
        DataTable _dt = new DataTable();
        //_dt.Columns.Add("IdBeneficiario", typeof(Int64));
        _dt.Columns.Add("ApellidoyNombre", typeof(String));
        _dt.Columns.Add("DocumentoyTipo", typeof(String));
        _dt.Columns.Add("Nro_SolicitudProvisoria", typeof(String));
        _dt.Columns.Add("FAltaProvisoria", typeof(String));
        _dt.Columns.Add("EsProvisoria", typeof(String));
        _dt.Columns.Add("Referencia_Provisoria", typeof(String));
        _dt.Columns.Add("Sectorderiva", typeof(String));
        _dt.Columns.Add("Desc_Prestacion", typeof(String));
        _dt.Columns.Add("Desc_Pais", typeof(String));
        _dt.Columns.Add("IngresaDevuelve", typeof(String));
        _dt.Columns.Add("DocumentosIngresados", typeof(String));


        List<AuxiliaresWS.Prestacion> lp = InvocaWsDao.TraerPrestaciones();
        AuxiliaresWS.Prestacion p = null;


        foreach (ActoresWS.SolicitudProvisoria oSolPrev in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            //_drTemp["IdBeneficiario"] = oSolPrev.IdBeneficiario;
            _drTemp["Nro_SolicitudProvisoria"] = oSolPrev.Nro_SolicitudProvisoria;
            _drTemp["ApellidoyNombre"] = oSolPrev.ApellildoynombreDeclarado;
            _drTemp["DocumentoyTipo"] = oSolPrev.DocumentoDeclarado.Equals(string.Empty) ? "" : oSolPrev.DocumentoDeclarado;
            _drTemp["FAltaProvisoria"] = oSolPrev.FAltaProvisoria.ToShortDateString();
            _drTemp["EsProvisoria"] =  oSolPrev.FBajaProvisoria.HasValue ? "NO":"SI";
            _drTemp["Referencia_Provisoria"] = oSolPrev.Referencia_Provisoria;
            _drTemp["Sectorderiva"] = oSolPrev.Sectorderiva == null ? "No derivado" : oSolPrev.Sectorderiva.Descripcion;
            _drTemp["IngresaDevuelve"] = oSolPrev.TIngresoProvisorio == TipoIngresoProvisorio.Ingreso ? "Ingreso" : "Devolución";
        
            //p = lp.Find(delegate(AuxiliaresWS.Prestacion prestacion)
            //        {
            //            return prestacion.Cod_Prestacion == oSolPrev.Cod_Prestacion;;
            //        }
            //        );

            //_drTemp["Desc_Prestacion"] = p.Descripcion;

            _drTemp["Desc_Prestacion"] = oSolPrev.PrestacionSolicitada == null ? "No solicita" : oSolPrev.PrestacionSolicitada.Descripcion;
            _drTemp["Desc_Pais"] = oSolPrev.PaisConvenio == null ? "No define" : oSolPrev.PaisConvenio.Descripcion;
            _drTemp["DocumentosIngresados"] = oSolPrev.LMovimientos == null ? "No registra" : oSolPrev.LMovimientos.Count().ToString();

            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }

    public static DataTable toDataTable(List<ActoresWS.SolicitudProvisoriaMovimiento> iParam)
    {
        DataTable _dt = new DataTable();
        //_dt.Columns.Add("IdBeneficiario", typeof(Int64));
        _dt.Columns.Add("Nro_SolicitudProvisoria", typeof(String));
        _dt.Columns.Add("DescripcionBreve", typeof(String));
        _dt.Columns.Add("Digitalizado", typeof(String));
        _dt.Columns.Add("TipoDocumentacion", typeof(String));
        _dt.Columns.Add("SecuenciaDocumento", typeof(String));
        
        foreach (ActoresWS.SolicitudProvisoriaMovimiento oSolPrev in iParam)
        {
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Nro_SolicitudProvisoria"] = oSolPrev.Nro_SolicitudProvisoria;
            _drTemp["DescripcionBreve"] = oSolPrev.DescripcionBreve;
            _drTemp["Digitalizado"] = oSolPrev.Digitalizado ? "Si" : "No";
            _drTemp["TipoDocumentacion"] = oSolPrev.TipoDocumentacion.Descripcion;
            _drTemp["SecuenciaDocumento"] = oSolPrev.SecuenciaDocumento.ToString();
            
            _dt.Rows.Add(_drTemp);
        }
        return _dt;
    }


    #region ADP
    public static DataTable toDataTable(List<DatosPw02> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("Apellidoynombre", typeof(String));
        _dt.Columns.Add("CUIP", typeof(String));
        _dt.Columns.Add("Documento", typeof(String));
        _dt.Columns.Add("FNacimiento", typeof(String));
        _dt.Columns.Add("Estado", typeof(String));

        
        string textoFnac;

        foreach (DatosPw02 odato in iParam)
        {
            //textoFnac = (DateTime.TryParse(odato.f_naci, out fechaConvert)) ? fechaConvert.ToShortDateString() : "";
            textoFnac = odato.f_naci == "" ? "No informa" : Util.FormateoFecha(odato.f_naci, true);
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Apellidoynombre"] = odato.ape_nom;
            _drTemp["CUIP"] = odato.cuil;
            _drTemp["Documento"] = odato.doc_nro + "-" + odato.doc_da_tipo;
            _drTemp["FNacimiento"] = textoFnac;
            _drTemp["Estado"] = odato.estado;

            _dt.Rows.Add(_drTemp);
        }
        return _dt;
 
    }

    public static DataTable toDataTable(List<DatosPw03> iParam)
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("Apellidoynombre", typeof(String));
        _dt.Columns.Add("CUIP", typeof(String));
        _dt.Columns.Add("Documento", typeof(String));
        _dt.Columns.Add("FNacimiento", typeof(String));
        _dt.Columns.Add("Estado", typeof(String));

        
        string textoFnac;

        foreach (DatosPw03 odato in iParam)
        {
            //textoFnac = (DateTime.TryParse(odato.f_naci, out fechaConvert)) ? fechaConvert.ToShortDateString() : "";
            textoFnac = odato.f_naci == "" ? "No informa" : Util.FormateoFecha(odato.f_naci, true);
            DataRow _drTemp;
            _drTemp = _dt.NewRow();
            _drTemp["Apellidoynombre"] = odato.ape_nom;
            _drTemp["CUIP"] = odato.cuil;
            _drTemp["Documento"] = odato.doc_nro + "-" + odato.doc_da_tipo;
            _drTemp["FNacimiento"] = textoFnac;
            _drTemp["Estado"] = odato.estado;

            _dt.Rows.Add(_drTemp);
        }
        return _dt;

    }


    #endregion ADP

    //generic
    public static DataTable toDataTable<T>(IList<T> data)// T is any generic type
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

        DataTable table = new DataTable();
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];

            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }
        object[] values = new object[props.Count];
        foreach (T item in data)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = props[i].GetValue(item);
            }
            table.Rows.Add(values);
        }
        return table;
    }

    #endregion ToDatatable
}