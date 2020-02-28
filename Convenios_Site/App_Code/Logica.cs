using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Logica
{

    #region Contructores
    public Logica()
    {

    }
    #endregion Contructores

    #region Verifica Movimientos Traspaso
    public static String ValidaOpcionesMovimiento(
        Int32? sectorActual
        , Int32? estadoNuevo
        , Int32? sectorNuevo
        , Boolean conExpediente
        )
    {

        string mensaje = "";
        //setea lista de estados y sectores a mostrar segun estado - sector actual - expediente
        //String mensaje = string.Empty;
        //oestadosValidos = new List<AuxiliaresWS.Estado>();
        //osectoresValidos = new List<AuxiliaresWS.Sector>();
        //foreach (AuxiliaresWS.Estado oEstado in allEstados)
        //{
        //    if (oEstado.Cod_estado != estadoActual)
        //        oestadosValidos.Add(oEstado);
        //}
        //Estado Derivado debe cambiar de sector
        if(estadoNuevo.HasValue && sectorNuevo.HasValue && sectorActual.HasValue)
        {
            if (( estadoNuevo.Value == 7 ) && (sectorActual.Value == sectorNuevo.Value))
                mensaje = "Para derivaciónes se debe seleccionar un sector diferente al actual";
        }
        //comentado el 23/09/2014 - el sistema debe admitir cualquier moovimiento, asi no tenga sector asignado.
        //else if(!sectorActual.HasValue && !sectorNuevo.HasValue)
        //    mensaje = "El trámite no registra sector asignado, se debe derivar antes a un sector.";

        //comentado el 18/09/2014 - el sistema debe admitir la falta de expedientes para los cambios de 
            //sectdor mencionados
        //else if (sectorNuevo.HasValue)
        //{
 
        //    //Se requiere expediente para derivar a "Beneficiarios - Computos o Notificaciones"
        //    if (!conExpediente && (sectorNuevo == 2 || sectorNuevo == 3 || sectorNuevo == 5))
        //        mensaje = "Se requiere expediente ingresado para derivar trámite a sector Beneficiarios - Cómputos o Notificaciónes";
        //}

        else if (estadoNuevo.HasValue)
        {
            if (!conExpediente && (estadoNuevo == 10 || estadoNuevo == 9))
                mensaje = "Se requiere expediente ingresado para estados -Caja 33- o -Agregar a Expediente-";
        }
        
        return mensaje;       
    }
    #endregion

    //#region Trae ultimo movimiento de una lista de movimientos
    //public static ActoresWS.Movimiento_Solicitud MovimientosTraeULtimo(List<ActoresWS.Movimiento_Solicitud> iMovimientos)
    //{
    //    ActoresWS.Movimiento_Solicitud ultimoMovimiento = new ActoresWS.Movimiento_Solicitud();
    //    ultimoMovimiento.Fecha_Movimiento = DateTime.Parse("01/01/1900");
    //    foreach (ActoresWS.Movimiento_Solicitud movimiento in iMovimientos)
    //    {
    //        if (movimiento.Fecha_Movimiento > ultimoMovimiento.Fecha_Movimiento)
    //            ultimoMovimiento = movimiento;
    //    }
    //    //retorna el ultimo movimiento realizado
    //    return ultimoMovimiento;
    //}
    //#endregion
}

