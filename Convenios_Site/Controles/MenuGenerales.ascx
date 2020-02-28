<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuGenerales.ascx.cs"
    Inherits="comun_controles_MenuGenerales" %>
<!--Menu con iconos-->
<div id="encab_Menu"  >
    <div style="float: right; padding-right:5px ; padding-top:5px">
        <asp:ImageButton ID="btnFAQ" runat="server" title="Preguntas Frecuentes" ImageUrl="~/App_Themes/Imagenes/bt_help.png"  onclick="btnFAQ_Click" />
    </div>
    <div style="float: right; padding-right:5px ; padding-top:5px">
        <asp:ImageButton ID="btnHome" runat="server" title="Inicio"
            ImageUrl="~/App_Themes/Imagenes/bt_home.png" onclick="btnHome_Click" />
    </div>
    <div style="float: right; padding-right:5px ; padding-top:5px">
        <asp:ImageButton ID="btnExit" runat="server" title="Salir"
            ImageUrl="~/App_Themes/Imagenes/bt_exit.png" OnClick="btnExit_Click"/>
    </div>
    <div id="identificacion2" style="float: right; padding-top:10px; margin-right:15px">
        <asp:Label ID="lblPerfil" runat="server" Font-Size="XX-Small" ForeColor="DarkGray"></asp:Label>            
        </div>
    <div id="identificacion" style="float:left">
        <label>
            <asp:Label ID="lblNombre" runat="server" ></asp:Label>
            <asp:Label ID="lblCuip" runat="server" ></asp:Label>
            <asp:Label ID="lblIdentificador" runat="server" ></asp:Label>            
        </label>
    </div>
</div>
