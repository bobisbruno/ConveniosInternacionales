<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNOMnu.master" AutoEventWireup="true" CodeFile="ProvisoriasAVencer.aspx.cs" Inherits="Paginas_ProvisoriasAVencer" Title="Trámites provisorios a vencer"   Culture="Auto" UICulture="Auto"%>
<%@ Register Src="~/Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPageNOMnu.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Pragma" content="no-clase">
    <meta http-equiv="Expires" content="-1">
 </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div style="text-align:center; width:98%; padding-top:5px; padding-bottom:5px; margin-bottom:5px; margin-top:5px">
        <h2>
            Trámites provisorios próximos a vencer</h2></div>
    <div class="FondoClaro" style="text-align:center; width:98%; padding-top:10px; padding-bottom:5px; padding-left:10px">
        <div class = "TituloBold" style="vertical-align: middle; text-align:left; padding-right:10px; padding-left: 10px; padding-bottom:1px; font-size: 8pt">
                <asp:Label ID = "lbElementosEncontrados" runat = "server" Text = "" />&nbsp;elementos encontrados
            </div>
            <table><tr><td style="width:99%; text-align:center">
            <div style="padding: 10px 0 0px 0; width: auto; height: auto; text-align:center ">
                <div id="divListado" style="overflow: auto; height: 400px; width:99%; text-align:center">
                    <asp:GridView runat = "server" ID="gridListadoSolicitudes" GridLines="None" OnRowCommand="RowCommand"
                        Width="98%" Visible="true" AutoGenerateColumns="False" ShowHeader="true" Height="5px"
                        AllowSorting="True" OnSorted="gv_Grilla_Sorted" OnSorting="gv_Grilla_Sorting" >
                        <HeaderStyle Height="30px" HorizontalAlign="Center" />
                        <RowStyle Height="25px" />
                        <Columns>
                            <asp:BoundField DataField="ApellidoyNombre" HeaderText = "Apellido y nombre"  SortExpression="ApellidoyNombre">
                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "DocumentoyTipo"  HeaderText = "Documento" >
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Desc_Prestacion" HeaderText = "Trámite" SortExpression="Desc_Prestacion">
                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Desc_Pais" HeaderText = "País"   SortExpression="Desc_Pais">
                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Nro_SolicitudProvisoria" HeaderText = "Nro trámite"  SortExpression="Nro_SolicitudProvisoria">
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundField>
                                <asp:BoundField DataField = "FAltaProvisoria" HeaderText = "Alta" >
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundField>
                                <asp:BoundField DataField = "Referencia_Provisoria"  HeaderText = "Referencia" >
                                <ItemStyle HorizontalAlign="Left" Width="28%"></ItemStyle>
                            </asp:BoundField>
                                <asp:BoundField DataField = "Sectorderiva" HeaderText = "Derivado" SortExpression="Sectorderiva">
                                <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "IngresaDevuelve" HeaderText = "Ingresa / Devuelve" >
                                <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "DocumentosIngresados"  HeaderText = "Documentos" >
                                <ItemStyle HorizontalAlign="Center" Width="6%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "DiasCaducidad"  HeaderText = "Días caducidad"   SortExpression="DiasCaducidad">
                                <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                            </asp:BoundField>
                         </Columns>
                    </asp:GridView>
                </div>
                </div>
                </td></tr></table>
        </div>
        
    <!-- Botones -->
    <div style="text-align: center; width:98%; padding-top:5px; margin-bottom:5px; padding-left:10px; padding-right:10px">
        <asp:Button   ID="btnCerrar" Text="Cerrar" runat="server" CssClass="Botones" Width="120px" Height="21px" onclick="btnCerrar_Click" TabIndex="24"/>
    </div>
     <uc1:Mensaje ID="mensaje" runat="server" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>