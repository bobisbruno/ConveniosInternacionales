<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ConNotas.aspx.cs" Inherits="Paginas_ConNotas" Title="Consulta de notas"   Culture="Auto" UICulture="Auto"%>
<%@ Register Src="../Controles/Busquedabeneficiario.ascx" TagName="BBeneficiario" TagPrefix="uc2" %>
<%@ Register Src="~/Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/barraNav.ascx" TagName="BNav" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <div align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 10px; padding-bottom: 10px; width:99%">
    <table width="100%">
    <tr><td style=" width:85%; text-align:right">
    <!--Control busqueda beneficiario-->
    <uc2:BBeneficiario ID="busben" runat="server" /></td>
    <td style=" width:15%; text-align:left"><asp:Button   ID="btnBuscar" runat="server" CssClass="Botones" Text="Buscar" Width="110" Height="21" onclick="btnBuscar_Click"  TabIndex="49"/></td>
    </tr></table>
    <div id="dvBenefConNota" runat="server" style="overflow:auto; height:150px; width:98%; margin-top:10px; margin-bottom:10px" visible="false">
    <asp:GridView runat = "server" ID="gridListadoBenefCNota" GridLines="None" Width="98%" AutoGenerateColumns="False" Height="5px" OnRowCommand="RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Id_Beneficiario" Visible="false"/>
                            <asp:BoundField DataField = "apeNom" HeaderText = "Apellido y Nombre" >
                                <ItemStyle HorizontalAlign="Left" Width="35%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Documento" HeaderText = "Documento" >
                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "ExpedienteExt" HeaderText = "C CIACI" >
                                <ItemStyle HorizontalAlign="Center" Width="30%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Fecha_nac" HeaderText = "Fecha Nac." >
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Ver nota" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgVer" AlternateText="Consultar notas" runat="server" CommandName="VerNotas"  CommandArgument='<%#Eval("Id_Beneficiario")+ ";" + Eval("apeNom")  + ";" + Eval("Documento") + ";" + Eval("ExpedienteExt")%>'   ImageUrl="~/App_Themes/Imagenes/Lupa.gif"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                         </Columns>
                    </asp:GridView></div>
    </div>
    <div id="dvDatosNotas" runat="server" class="FondoClaro" style="text-align:center; width:99%; padding-top:5px; margin-top:10px; padding-bottom:10px; padding-top:10px; height:auto; overflow-wrap:break-word">
    <asp:Label ID="lbldatosBenef" runat="server" width="95%"></asp:Label>
       <asp:repeater id="rptNotas" runat="server"  OnItemDataBound="rptNotas_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="99%">
                                    </headertemplate>
                                    <itemtemplate>
                                        <tr>
                                        <td style=" width:50%"><font size="1">Nro. Nota:</font>&nbsp;<asp:Label runat="server" ID="lblNNota"/>
                                        </td>
                                        <td><font size="1">Fecha:</font>&nbsp;<asp:Label runat="server" ID="lbFecha"/>
                                        </td>
                                        <td align="right"><asp:Button   ID="btnImprimirNota" Text="Imprimir nota" runat="server" CssClass="Botones" Width="100px" Height="21px" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Asunto") + ";" + DataBinder.Eval(Container.DataItem, "Descripcion")  + ";" + DataBinder.Eval(Container.DataItem, "FRecepcion") + ";" + DataBinder.Eval(Container.DataItem, "NroNota")%>' OnCommand="PrintCommand" CommandName = "Imprimir"/></td>
                                        </tr>
                                        <tr><td colspan="2">
                                        <font size="1">Asunto:</font>&nbsp;<asp:Label runat="server" ID="lblAsunto"/></td></tr>
                                        <tr>
                                        <td colspan="3"><font size="1">Descripción</font></td>
                                        </tr>
                                        <tr><td colspan="3" style=">
                                        <asp:Label runat="server" ID="lbDescripcion" Width="95%"/>
                                        </td></tr>
                                        <tr>
                                        <td colspan="3"><hr /></td>
                                        </tr>
                                    </itemtemplate>
                                    <footertemplate>
                                      </table>
                                    </footertemplate>
                                  </asp:repeater>
        </div>
    <div align="center" style="margin-top: 5px; margin-bottom:5px">
        <asp:Button   ID="btnRegresar" Text="Regresar" runat="server" CssClass="Botones" Width="130px" Height="21px" onclick="btnRegresar_Click" TabIndex="50"/>
    </div>
    </div></div>
    <uc1:Mensaje ID="mensaje" runat="server" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

