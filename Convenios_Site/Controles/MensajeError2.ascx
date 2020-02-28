<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MensajeError2.ascx.cs" Inherits="Comun_Controles_Merror2" %>
<asp:Panel ID="pnlMasterErrores" runat="server" Width="100%" Style="display: none">
    <div style="margin-top:2px; margin-bottom:2px; width:100%">
                        <table cellpadding='3' width="100%" cellspacing='0' border='0' class='tableCabeceraPanelMessages'>
                            <tr>
                                <td class='cabeceraPanelMensajes'>
                                    &nbsp;&nbsp;&nbsp;&nbsp;Se han encontrado los siguientes errores - Verifique
                                </td>
                                <td class='labelOcultarMostrarPanel' align='right'>
                                    <label id='label01_mostrarPanelMensajes' onclick='ocultarMostrarPanelMensajes()'>
                                        Mostrar Errores</label>
                                </td>
                                <td align='center' style="width: 34px">
                                    <img id='img01_mostrarPanelMensajes' class='imageOcultarMostrar' src="../App_Themes/Imagenes/Flecha_Abajo.gif"
                                        onclick='ocultarMostrarPanelMensajes()' alt='Ocultar Panel' />
                                </td>
                            </tr>
                        </table>
                        <div id='div01_panelMensajes' style="display: none">
                            <table cellpadding='2' cellspacing='0' border='0' class='tableMessages'>
                                <tr>
                                    <td class='cajaIconMessages'>
                                    </td>
                                    <td valign='top' class='cajaIconMessages'>
                                        <asp:Label CssClass="textError" ID="lblDetalleErrores" runat="server"></asp:Label>
                                        <asp:ValidationSummary CssClass="textError" ID="ValidationSummary1" runat="server"
                                            DisplayMode="List" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    </asp:Panel>
<script language="javascript" type="text/javascript">


    function ocultarMostrarPanelMensajes() {
        divElement = document.getElementById("div01_panelMensajes");
        imgElement = document.getElementById("img01_mostrarPanelMensajes");
        labelElement = document.getElementById("label01_mostrarPanelMensajes");

        if (divElement.style.display == 'none') {
            divElement.style.display = '';
            labelElement.innerHTML = "Ocultar Errores";
            imgElement.src = "../App_Themes/Imagenes/Flecha_UP.gif";
            imgElement.alt = "Ocultar Errores";
        } else {
            divElement.style.display = 'none';
            imgElement.src = "../App_Themes/Imagenes/Flecha_Abajo.gif";
            labelElement.innerHTML = "Mostrar Errores";
            imgElement.alt = "Mostrar Errores";
        }
    }
        
</script>
