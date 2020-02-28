<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BusquedaGrilla.ascx.cs"
    Inherits="comun_controles_BusquedaGrilla" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="busquedaABM">
    <asp:Panel ID="PnlBusquedaGrilla" runat="server" DefaultButton="imgabtnBuscar">
        <asp:DropDownList ID="ddlCamposBusqueda" runat="server" Width="200px" Height="20px">
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:TextBox ID="txtFiltro" runat="server" CssClass="CajaTexto" Width="300px"></asp:TextBox>
        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderFI" runat="server"
            TargetControlID="txtFiltro" WatermarkText="Ingrese Búsqueda" WatermarkCssClass="watermarked" />
        &nbsp;&nbsp;
        <asp:ImageButton ID="imgabtnBuscar" runat="server" ForeColor="#ABD3FC" ImageUrl="~/App_Themes/Imagenes/search.png" OnClick="imgabtnBuscar_Click" />
    </asp:Panel>
</div>
