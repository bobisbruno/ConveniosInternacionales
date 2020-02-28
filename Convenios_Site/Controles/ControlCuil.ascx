<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlCuil.ascx.cs" Inherits="ControlCuil" %>
<div id="divCuil" onpaste="separaDatosOnPaste(event,this);return false;" runat="server"
    style="white-space: nowrap; width: 130px; vertical-align: text-top; text-align: left">
    <asp:TextBox ID="txtCodigo" runat="server" onkeypress="return validarNumeroControl(event)"
        onkeyup="return autoTabControl(this, event);" MaxLength="2" Style="text-align: center;
        width: 18px">
    </asp:TextBox>-<asp:TextBox ID="txtNumero" runat="server" onkeypress="return validarNumeroControl(event)"
        onkeyup="return autoTabControl(this, event);" MaxLength="8" Style="text-align: center"
        Width="60px">
    </asp:TextBox>-<asp:TextBox ID="txtDigito" runat="server" onkeypress="return validarNumeroControl(event)"
        onkeyup="return autoTabControl(this, event);" MaxLength="1" Style="text-align: center"
        Width="12px">
    </asp:TextBox>
    <asp:Label ID="lbl_Obligatorio" CssClass="CajaTextoError" runat="server" Text="*"
        Style="margin-left: 0px" Visible="false">
    </asp:Label>
</div>
<asp:Label ID="lbl_Error" CssClass="CajaTextoError" runat="server" Style="position: absolute"
    Visible="false"></asp:Label>

