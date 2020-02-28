<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNOMnu.master" AutoEventWireup="true" CodeFile="ANotificacion.aspx.cs" Inherits="ANotificacion" Title="Notificar devoluciónes"   Culture="Auto" UICulture="Auto"%>
<%@ Register Src="~/Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPageNOMnu.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Pragma" content="no-clase">
    <meta http-equiv="Expires" content="-1">
 </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <uc3:MError2 ID="MError" runat="server" />
    <div class="TituloBold" style="text-align: left; width:98%; padding-top:5px; padding-bottom:5px">
            <asp:Label ID="lbEncabezadoBeneficiario" runat="server"></asp:Label></div>
    <div class="FondoClaro" style="text-align:center; width:98%; padding-top:5px">
            <div class="TituloBold" style="text-align: left; width:98%; padding-top:5px; margin-bottom:5px; float:left">
                Devoluciónes efectuadas sobre&nbsp;&nbsp;<asp:Label ID="lbdescPrestacion" runat="server"></asp:Label>
                &nbsp;&nbsp;<asp:Label ID="lbDescPaisS" runat="server"></asp:Label>
            </div>
            <asp:GridView runat = "server" ID="gridListadoDevoluciones" PageSize="15" GridLines="None" Width="98%" Visible="false" AutoGenerateColumns="False" UseAccessibleHeader="true" AllowSorting="false">
                <Columns>
                    <asp:BoundField DataField="FechaMovimiento" HeaderText="Fecha devolución"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                    <asp:BoundField DataField="Destino" HeaderText="Destino"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="35%"/>
                    <asp:BoundField DataField="Certificado" HeaderText="Certificado"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50%"/>
                </Columns>
                </asp:GridView>
                <div id="dvNoDevolucion" runat="server" class="TextoNegroBoldCENTER">No existen devoluciónes a notificar</div>
                <div id="dvNotificacion" style=" margin-top:10px; padding-left:10px; margin-left:10px; padding-bottom:10px" runat="server" visible="true">
                    <table cellpadding="1" cellspacing="1" width="98%">
                    <tr>
                    <td style=" width:30%"><font size="1">Fecha de notificación:</font></td>
                    <td><asp:TextBox ID="txtFechaNotificacion" runat="server" onkeyup="this.value=this.value.toUpperCase()"  Width="100px" MaxLength="10" CssClass="CajaTexto" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtFechaNotificacion" runat="server" ControlToValidate="txtFechaNotificacion" Display="None" ErrorMessage="Debe ingresar fecha de notificación" ValidationGroup="datosNotifica"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcerfvtxtFechaNotificacion" runat="server" TargetControlID="rfvtxtFechaNotificacion"></cc1:ValidatorCalloutExtender>                                                                 
                    <asp:RangeValidator ID="rvtxtFechaNotificacion" runat="server" Type="Date" ControlToValidate="txtFechaNotificacion" ErrorMessage="Fecha no válida. (ingresar desde 2 años a la fecha)" Display="None" ValidationGroup="datosNotifica"/>
                    <cc1:ValidatorCalloutExtender ID="vcervtxtFechaNotificacion" runat="server" TargetControlID="rvtxtFechaNotificacion"></cc1:ValidatorCalloutExtender> 
                    </td>
                    </tr></table></div>
        </div>
    <!-- Botones -->
    <div style="text-align: center; width:98%; padding-top:5px; margin-bottom:5px; padding-left:10px; padding-right:10px">
    <asp:Button   ID="btnGuardar" Text="Notificar" runat="server" CssClass="Botones" Width="110px" Height="21px" 
    onclick="btnGuardar_Click" TabIndex="19" CausesValidation="true" ValidationGroup="datosNotifica"/>&nbsp;
    <asp:Button   ID="btnCerrar" Text="Cerrar" runat="server" CssClass="Botones" Width="120px" Height="21px" onclick="btnCerrar_Click" TabIndex="21"/>
    </div>
    
    <!-- end Botones -->
    <uc1:Mensaje ID="mensaje" runat="server" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>