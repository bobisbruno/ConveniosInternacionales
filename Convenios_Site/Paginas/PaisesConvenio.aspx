<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="PaisesConvenio.aspx.cs" Inherits="Paginas_PaisesConvenio" Title="Modificacion de Convenio Pais"   Culture="Auto" UICulture="Auto"%>
<%@ Register Src="~/Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/barraNav.ascx" TagName="BNav" TagPrefix="uc4" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Pragma" content="no-clase">
    <meta http-equiv="Expires" content="-1">
 </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <uc4:BNav id="BNav" runat="Server"/>
    <uc3:MError2 ID="MError" runat="server" />
        <div class="FondoBlanco">
    <div style="margin: 10px">
    <h1>
        <asp:Label ID="LblTituloPagina" runat="server" Text="Titulo Pagina"></asp:Label>
        </h1>
        <div class="FondoClaro" style="text-align:center; width:98%; padding-top:5px; margin-top:10px; height:400px; padding-bottom:5px; padding-top:10px">        
            <table width="99%">
                    <tr>
                        <td style="width: 100%; vertical-align:top; text-align:center">
                            <div style=" width:98%; text-align:left">
                            <table width="98%" style=" height:30px; font-family:Arial, tahoma, helvetica; color:#ffffff;font-size: 8pt;font-weight:bold;background-color:#95B0DF;text-align:center">
                            <tr>
                                <td style=" width:15%">Con convenio</td>
                                <td style=" width:85%">Pais</td>
                            </tr>
                            </table>
                            </div>    
                            <div id="divListado" style="overflow: auto; height: 340px; width:98%; text-align:left">
                            <asp:GridView runat = "server" ID="gvPaises" DataKeyNames="Pais_PK" GridLines="None" UseAccessibleHeader="true"
                            Width="98%" Visible="true"  AutoGenerateColumns="False" OnRowDataBound="gvPaises_RowDataBound" ShowHeader="false">
                            <Columns>
                                <asp:BoundField DataField="Pais_PK" Visible="false"/>
                                <asp:TemplateField HeaderText="Con convenio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkConConvenio" runat="server" OnCheckedChanged="chkConConvenio_CheckedChanged" AutoPostBack="true"/>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                <asp:BoundField DataField="ConConvenio" Visible="false"/>
                                <asp:BoundField DataField="Descripcion" HeaderText = "Pais" >
                                    <ItemStyle HorizontalAlign="Left" Width="85%"></ItemStyle>
                                </asp:BoundField>
                             </Columns>
                        </asp:GridView>
                        </div>
                        </td></tr></table></div>
    
    <div align="right" style="margin-top: 10px;padding-right:10px ">
        <asp:Button   ID="btnGuardar" Text="Guardar Cambios" runat="server" CssClass="Botones" Width="130px" Height="21px"  ValidationGroup="datosInstrumento" OnClick="btnGrabar_Click"/> &nbsp;
        <asp:Button   ID="btnRegresar" Text="Regresar" runat="server" CssClass="Botones" 
            Width="130px" Height="21px" onclick="btnRegresar_Click"/> 
     </div></div></div>
    <uc1:Mensaje ID="mensaje" runat="server" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

