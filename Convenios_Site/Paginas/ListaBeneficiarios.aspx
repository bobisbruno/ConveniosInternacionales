<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ListaBeneficiarios.aspx.cs" Inherits="Paginas_ListaBeneficiarios" Title="Lista de Beneficiarios" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/barraNav.ascx" TagName="BNav" TagPrefix="uc4" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Pragma" content="no-clase">
    <meta http-equiv="Expires" content="-1">
 </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <uc4:BNav id="BNav" runat="Server"/>
    <uc3:MError2 ID="MError" runat="server" />
        <div class="TituloBold" style="text-align: left; width:98%; padding-top:5px; padding-bottom:5px">
            Resultado solicitantes por&nbsp;<asp:Label ID="lbTituloCriterio" runat="server"></asp:Label></div>
        <div align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 10px; padding-bottom: 10px; width:98%">
            <div id="Div1" class = "TituloBold" style="vertical-align: middle; padding-left: 10px; font-size: 7pt">
                <asp:Label ID = "lbElementosEncontrados" runat = "server" Text = "" />&nbsp;elementos encontrados
            </div>
            <div style="padding: 10px 0 0px 0; width: auto; height: auto; text-align:center ">
                <div style=" width:98%; text-align:left">
                            <table width="98%" style=" height:30px; font-family:Arial, tahoma, helvetica; color:#ffffff;font-size: 8pt;font-weight:bold;background-color:#95B0DF;text-align:center">
                            <tr>
                                <td style=" width:10%">Ver datos</td>
                                <td style=" width:35%">Apellido y nombre</td>
                                <td style=" width:20%">Documento</td>
                                <td style=" width:15%">Código SIACI</td>
                                <td style=" width:5%">Sexo</td>
                                <td style=" width:15%">Fecha nacimiento</td>
                            </tr>
                            </table>
                            </div>    
                <div id="divListado" style="overflow: auto; height: 340px; width:98%; text-align:left">
                    <asp:GridView runat = "server" ID="gridListadoBeneficiarios" GridLines="None"
                        Width="98%" Visible="true" AutoGenerateColumns="False" ShowHeader="false" Height="5px">
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkVerInstrumento" runat="server" Text="&lt;img src=&quot;../App_Themes/imagenes/icon_lupe2.gif&quot; style=&quot;border:none&quot; /&gt;" NavigateUrl='<%# String.Format("InformacionCompletaBeneficio.aspx?idBeneficiario={0}", Eval("Id_Beneficiario")) %>'></asp:HyperLink>
                                    <%--<asp:ImageButton ID="lnkDetalle" runat="server" ImageUrl="~/App_Themes/Imagenes/icon_lupe2.gif"></asp:ImageButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ApeNom" HeaderText = "Apellido" >
                                <ItemStyle HorizontalAlign="Left" Width="35%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Documento" HeaderText = "Nombre" >
                                <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "ExpedienteExt" HeaderText = "Códogo SIACI" >
                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "sexo" HeaderText = "Sexo" >
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundField>
                                    <asp:BoundField DataField = "fecha_nac" HeaderText = "Fecha Nacimiento" >
                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Id_Beneficiario" Visible="false"/>
                         </Columns>
                    </asp:GridView>
                </div>
                <div style="width: 98%; text-align:right; padding-top:3px">
                    <asp:ImageButton ID="btnToExcell" runat="server" Visible="false"  
                        ImageUrl="~/App_Themes/Imagenes/excel.gif" AlternateText="Exportar a Excell" 
                        onclick="btnToExcell_Click" />
                </div>
                </div>
            </div>
                <div align="right" style="margin-top: 10px">
            <asp:Button   ID="btnRegresar" runat="server" CssClass="Botones" Text="Regresar" Width="110px"
                Height="21" OnClick="btnRegresar_Click" />
        </div>
        <div class="tblMasterPage" style="height: 1px"></div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

