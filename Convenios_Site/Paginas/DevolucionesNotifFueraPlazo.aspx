<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="DevolucionesNotifFueraPlazo.aspx.cs" Inherits="Paginas_DevolucionesNotifFueraPlazo" Title="Devoluciones notificadas fuera de plazo" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Pragma" content="no-clase">
    <meta http-equiv="Expires" content="-1">
 </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <uc3:MError2 ID="MError" runat="server" />
    <div style="vertical-align:top; text-align:center; line-height: 25px; padding-left:10px; margin-top:10px" class="TituloBold">
         Devoluciones notificadas fuera de plazo
    </div>
                    <div id="divListadoNoNotificados" runat="server" class="FondoClaro" style="margin-top: 5px; padding-top: 5px; padding-bottom: 10px; margin-bottom:10px; width:99%">
                    <!-- Botonera de navegacion -->
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div style="float: left;vertical-align: middle; padding-top:5px; padding-bottom:5px; padding-left:10px">
                                <table align="center" border="0">
                                    <tr>
                                        <td valign="top">
                                            <asp:ImageButton ID="PageSizeImb" runat="server" ImageAlign="AbsBottom"  OnClick="PageSizeImb_Click" ImageUrl="../App_Themes/imagenes/PageSize.gif" ToolTip="Set page size" Enabled="false" ></asp:ImageButton>&nbsp;&nbsp;
                                            <asp:TextBox ID="txtPageTotalRowsNum" runat="server" Columns="2" Text="50" Font-Bold="True" ReadOnly="true"></asp:TextBox>&nbsp;
                                            <asp:TextBox ID="txtPageSize" runat="server" Visible="false" Columns="2" Text="300" Font-Bold="True" ReadOnly="true"></asp:TextBox>&nbsp;
                                            <asp:ImageButton ID="FirstPage" OnCommand="NavigationLink_Click"  runat="server" ImageAlign="AbsBottom" ImageUrl="../App_Themes/imagenes/first.gif" ToolTip="First" CommandName="First"></asp:ImageButton>
                                            <asp:ImageButton ID="PreviousPage" runat="server" ImageAlign="AbsBottom" OnCommand="NavigationLink_Click"  ImageUrl="../App_Themes/imagenes/previous.gif" ToolTip="Previous" CommandName="Prev"></asp:ImageButton>&nbsp;&nbsp;
                                            <asp:ImageButton ID="GoToPageImb" runat="server"  OnClick="GoToPageImb_Click"  ImageAlign="AbsBottom" ImageUrl="../App_Themes/imagenes/GoToPage.gif" ToolTip="Go to page number"></asp:ImageButton>&nbsp;
                                            <asp:TextBox ID="txtGoToPage" runat="server" Width="30px" Text="1" Font-Bold="True" ReadOnly="true"></asp:TextBox>&nbsp;
                                            <asp:ImageButton ID="NextPage" runat="server" OnCommand="NavigationLink_Click"  ImageAlign="AbsBottom" ImageUrl="../App_Themes/imagenes/next.gif" ToolTip="Next" CommandName="Next" ></asp:ImageButton>
                                            <asp:ImageButton ID="LastPage" runat="server" ImageAlign="AbsBottom" OnCommand="NavigationLink_Click"  ImageUrl="../App_Themes/imagenes/last.gif" ToolTip="Last" CommandName="Last" ></asp:ImageButton>&nbsp;&nbsp;
                                            <asp:Label ID="lblCurrentPage" runat="server" Text="1" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="SepLbl" runat="server" Font-Size="X-Small" CssClass="pageLinks" Enabled="False">/</asp:Label>
                                            &nbsp;&nbsp;<asp:Label ID="lblTotalPages" runat="server" Font-Bold="True">1</asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                        <!-- Fin Botonera de navegacion -->
                    <div style="float: right;vertical-align: middle; padding-top:5px; padding-bottom:5px; padding-right:10px">
                                <asp:ImageButton ID="btnToExcell" runat="server" Visible="false"  ImageUrl="~/App_Themes/Imagenes/excel.gif" AlternateText="Exportar a Excell" onclick="btnToExcell_Click" />
                            </div>
                    <!-- Encabezado Grid -->
                    <div style=" width:98%; text-align:left">
                    <table width="98%" style=" height:30px; font-family:Arial, tahoma, helvetica; color:#ffffff;font-size: 8pt;font-weight:bold;background-color:#95B0DF;text-align:center">
                    <tr>
                        <td style=" width:30%">Apellido - Nombre</td>
                        <td style=" width:10%">F. devolución</td>
                        <td style=" width:10%">F. notificación</td>
                        <td style=" width:20%">Prestación</td>
                        <td style=" width:15%">Certificado</td>
                        <td style=" width:15%">Destino</td>
                    </tr>
                    </table>
                    </div>    
                    <!-- Fin Encabezado -->
                    <div style="overflow: auto; height: 360px; width:98%; text-align:left">
                    <asp:GridView runat = "server" ID="gridListadoDevolucionesNotificadasVencidas" GridLines="None" Height="5px"
                        Width="98%" Visible="true" AutoGenerateColumns="False" ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField="NomApe" HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" />
                            <asp:BoundField DataField="FechaMovimiento" HeaderText=""  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%"/>
                            <asp:BoundField DataField="FechaNotificacion" HeaderText=""  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%"/>
                            <asp:BoundField DataField="DescripcionPrestacion" HeaderText=""  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%"/>
                            <asp:BoundField DataField="Certificado" HeaderText=""  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%"/>
                            <asp:BoundField DataField="Destino" HeaderText=""  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%"/>
                        </Columns></asp:GridView>
                        </div>
                    </ContentTemplate></asp:UpdatePanel>
                    </div>
                    <div id="dvNoDevNotifVencidas" runat="server" style="vertical-align: middle; text-align:center; line-height: 25px" class="TituloBold">
                         No existen devoluciones notificadas fuera de plazo
                    </div>
                    <div align="right" style="margin-top: 10px; padding-right:10px">
            <asp:Button   ID="btnRegresar" runat="server" CssClass="Botones" Text="Regresar" Width="110px" TabIndex="1"
                Height="21" OnClick="btnRegresar_Click" />
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

