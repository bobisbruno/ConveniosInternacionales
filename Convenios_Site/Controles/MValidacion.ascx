<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MValidacion.ascx.cs" Inherits="Comun_Controles_MValidacion" %>
<div align="center" id="divMValidacion" runat="server" style="width: 100%; margin-top: 5px"
    visible="false">
    <table>
        <tr>
            <td >
                <asp:Image ID="imgMValidacion" runat="server" src="../App_Themes/Imagenes/atencion_gde_ani.gif"
                    alt="" Height="20px" Width="24px" Style="vertical-align: middle" />
            </td>
            <td align="left">
                <asp:Label ID="lblMValidacion" runat="server" Style="text-align: left; font-family: tahoma, Arial, helvetica;
                    color: #ff0000; font-size: 12px; vertical-align: middle">Error en la validación.</asp:Label>
            </td>
        </tr>
    </table>
</div>
