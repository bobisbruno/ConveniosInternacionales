using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class TiposEnumerados
{
    public TiposEnumerados()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public enum TipoActor
    {
        Beneficiario = 1,
        Causante = 2,
        Apoderado = 3,
    }

    public enum TipoTx
    {
        Alta = 1,
        Modificacion = 2,
        Eliminacion = 3,
    }

    public enum TipoGrafico
    {
        TORTA = 0,
        COLUMNA = 1,
        LINEA = 2,
        AREA = 3,
        BARRA = 4,
        FUNEL = 5,
        SEGMENTO = 6
    }

    

    public enum criteriotemporal
    {
        Anual = 0,
        Semestral = 1,
        Trimestral = 2,
        Mensual = 3
    }

    public enum paramTemporalSemestre
    {
        PrimerSemestre = 1,
        SegundoSemestre = 2
    }

    public enum paramTemporalTrimestre
    {
        PrimerTrimestre = 1,
        SegundoTrimestre = 2,
        TercerTrimestre = 3,
        CuartoTrimestre = 4
    }


    public enum tipoMovimiento
    {
        Ingreso = 1,
        Devolucion = 2,
        DerivaCambioEstado = 3,
        Notifica = 4
    }
    

    
    
}


 public class ParseoDatosDB
 {
    public ParseoDatosDB()
    {
        //
        // TODO: Add constructor logic here
        //
    }


   
    public static String codigoRegionToTexto(Int16 codRegion)
    {
        string ss;
        switch (codRegion)
        {
            case 1:
                ss= "Cap Fed y GBA";
                break;

            case 8:
                ss = "Litoral";
                break;

            case 3:
                ss = "Patagonia";
                break;

            case 4:
                ss = "Noroeste";
                break;

            case 5:
                ss = "Cuyo";
                break;

            case 6:
                ss = "Centro";
                break;

            case 7:
                ss = "Noreste";
                break;
 
            default:
                ss = "No informa";
                break;
                
        }
        return ss;
 
    }
}