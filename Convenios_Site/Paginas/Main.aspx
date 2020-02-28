<%@ Page Title = "Convenios internacionales - Búsqueda" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Paginas_Main"%>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Src="../Controles/Busquedabeneficiario.ascx" TagName="BBeneficiario" TagPrefix="uc2" %>
<%@ Register Src="~/Controles/barraNav.ascx" TagName="BNav" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <uc4:BNav id="BNav" runat="Server"/>
    <uc3:MError2 ID="MError" runat="server" />
    <div class="FondoBlanco">
    <div style="margin: 10px">
    <h1>
        <asp:Label ID="LblTituloPagina" runat="server" Text="Titulo Pagina"></asp:Label>
        </h1>
        
    <div align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 20px; padding-bottom: 10px; width:99%">
        
            <!--Control busqueda beneficiario-->
        <table><tr><td style=" width:75%; text-align:right">
            <uc2:BBeneficiario ID="busben" runat="server" />
                   </td><td style=" width:25%; text-align:left">
        <asp:Button style=" cursor:hand"  ID="btnBuscar" runat="server" CssClass="Botones" Text="Buscar" Width="110" Height="20"  onclick="btnBuscar_Click"  TabIndex="9"/>
        <asp:Button  ID="btnNueva" runat="server" CssClass="Botones"  Text="Nuevo solicitante" Width="130" Height="21" onclick="btnAltaBeneficio_Click"  TabIndex="10"/>
        
    </td></tr></table>
    </div>
    <div id="divNoConsulta" runat="server" style="padding-top:5px; margin-top:10px; margin-bottom:10px" runat="server" align="center" class="TituloBold">
         No existen resultados para la consulta efectuada
    </div>
        <!-- Solicitudes a vencer-->
        <div  id="dvSolicitudesProvisorias" style="margin-top:10px" runat="server">
            <h2>Trámites provisorios</h2>
    <div align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 10px; padding-bottom: 10px; width:99%">
        <table width="99%">
            <tr>
                <td style="padding-left:20px;width:70%">
                    <h4>Trámites provisorios ingresados en el período actual:&nbsp;<asp:Label ID="lbCantidadTrProvisoriosActual" runat="server"></asp:Label></h4>
                </td>
                <td style="text-align:right;padding-right:10px">
                    <asp:Button style=" cursor:hand"  ID="btnverProvisorios" runat="server" CssClass="Botones" Text="Consultar" Width="80" Height="20"  onclick="verProvisorios_Click"/>
                </td>
            </tr>
            <tr>
                <td style="padding-left:20px;width:70%">
                    <h4>Hay &nbsp;<asp:Label ID="lbCantidadTrProvisoriosAVencer" runat="server"></asp:Label>&nbsp;trámites provisorios próximos a vencer.</h4>
                </td>
                <td style="text-align:right;padding-right:10px">
                                        <asp:Button style=" cursor:hand"  ID="btnVerProvisoriosaVencer" runat="server" CssClass="Botones" Text="Ver" Width="80" Height="20"  onclick="btnVerProvisoriosaVencer_Click"/>
                </td>
            </tr>
        </table>
        
        
            </div>
            </div>
        <!-- FIN Solicitudes a vencer-->
    </div></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

