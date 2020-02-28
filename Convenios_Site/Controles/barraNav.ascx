<%@ Control Language="C#" AutoEventWireup="true" CodeFile="barraNav.ascx.cs" Inherits="comun_controles_barraNav" %>
<style >
.textOpcion
{	
	font-family:tahoma, verdana, arial;
	color: #666666;
	font-size:11px;
	padding: 2px 0px 2px 10px;
	text-align:left;
}
</style>
<div id="dvBNav"  style="height: 19px; padding-left:5px; text-align:left; width:100%; background-color:#f9fcf0; height:19px; margin-top:0px; margin-bottom:0px" runat="server">
    <asp:LinkButton ID="lnkInicio" runat="server" OnClick="lnkInicio_Click" Text="Inicio" CssClass="textOpcion"></asp:LinkButton>
    <asp:Label ID="lblTexto" runat="server"  CssClass="textOpcion" Font-Size="11px"></asp:Label>
</div>
