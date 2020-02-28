<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ConIndicadoresXEstado.aspx.cs" Inherits="Paginas_ConIndicadoresXEstado" Title="Indicador de estados a la fecha" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Pragma" content="no-clase">
    <meta http-equiv="Expires" content="-1">
 </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <uc3:MError2 ID="MError" runat="server" />
        <div class="TituloBold" style="text-align: left; width:98%; padding-top:5px; padding-bottom:5px">
            Totales por estados a la fecha</div>
        <div align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 10px; padding-bottom: 10px; width:98%">
        <font size="1">Buscar totalizaciónes por estado a fecha.. (dd/mm/AAAA)</font>&nbsp;&nbsp;
        <asp:TextBox ID="txtFecha" runat="server" onkeyup="this.value=this.value.toUpperCase()"  Width="70px" CssClass="CajaTexto" TabIndex="1" MaxLength="10"></asp:TextBox>
        <asp:RequiredFieldValidator id="rfvtxtFecha" ControlToValidate="txtFecha" ErrorMessage="Ingresar Fecha." Display="None" runat="server" ValidationGroup="paramConsulta"/>
        <cc1:ValidatorCalloutExtender ID="vcerfvtxtFecha" runat="server" TargetControlID="rfvtxtFecha"></cc1:ValidatorCalloutExtender> 
        <asp:RangeValidator ID="rvtxtFecha" runat="server" Type="Date" ControlToValidate="txtFecha" ErrorMessage="Fecha no debe ser mayor a la fecha actual." Display="None" ValidationGroup="paramConsulta"/>
        <cc1:ValidatorCalloutExtender ID="vcervtxtFecha" runat="server" TargetControlID="rvtxtFecha"></cc1:ValidatorCalloutExtender> 
        <div id="dvDatosConsulta" runat="server" style="text-align:center; width:98%">
        <div style="text-align:Center; vertical-align:top; margin-top:20px; margin-bottom:5px">
                <asp:Button style=" cursor: hand"  ID="btnConsultar" runat="server" CssClass="Botones" Text="Consultar" Width="110px" TabIndex="2"
                OnClick="btnConsultar_Click" ValidationGroup="paramConsulta" />&nbsp;
                    <asp:Button style=" cursor: hand"  ID="btnImprimir" runat="server" CssClass="Botones" Text="Imprimir" Width="90px" TabIndex="3"
                        OnClientClick="abrirArchivo();return false;"/>&nbsp;
                    <asp:Button style=" cursor: hand"  ID="btnToExcell"  runat="server" CssClass="Botones" Text="Exportar a Excell"  Width="100px" TabIndex="4"
                        onclick="btnToExcell_Click"/>
                </div>
            <div style="padding: 10px 0 0px 0; width: auto; height: auto; text-align:center; width:98% ">
            <div style=" width:70%; text-align:left">
                            <table width="98%" style=" height:30px; font-family:Arial, tahoma, helvetica; color:#ffffff;font-size: 8pt;font-weight:bold;background-color:#95B0DF;text-align:center">
                            <tr>
                                <td style=" width:50%">Estado</td>
                                <td style=" width:25%">Total</td>
                                <td style=" width:25%">%</td>
                            </tr>
                            </table>
                            </div>    
            <div id="divListado" style="overflow: auto; width:70%; text-align:left; margin-bottom:10px">
                    <asp:GridView runat = "server" ID="gridListadoSolicitudes" GridLines="None" Width="98%" Visible="true" AutoGenerateColumns="False" ShowHeader="false" Height="5px">
                        <Columns>
                            <asp:BoundField DataField="Destado">
                                <ItemStyle HorizontalAlign="Left" Width="50%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "TotalEstado">
                                <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "PorcentualEstado">
                                <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                            </asp:BoundField>
                         </Columns>
                    </asp:GridView>
                </div>
                <br />
            <asp:Literal ID="litGraficoBarras" runat="server"></asp:Literal>
            <br />
            <asp:Literal ID="litGraficoTorta" runat="server"></asp:Literal>
            <br />
            <font size="2"><asp:Label runat="server" ID="lbTituloLineal" /></font>
            <asp:GridView runat = "server" ID="gridEvolutivo" GridLines="None"
                            Width="99%" Visible="true" AutoGenerateColumns="False" >
                            <Columns>
                             </Columns>
                        </asp:GridView>
                        <br />
            <div style=" width:99%">
            <asp:Literal ID="litGraficoLinea" runat="server"></asp:Literal></div>
            </div>
        </div>
        <div id="dvNODatosConsulta" style="padding-top:5px" runat="server" align="center" class="TextoNegroCENTER">
                                No existen datos para la consulta solicitada.
                            </div>
                            </div>
        <div align="right" style="margin-top: 10px; padding-right:10px">
            <asp:Button style=" cursor: hand"  ID="btnRegresar" runat="server" CssClass="Botones" Text="Regresar" Width="110px" TabIndex="5"
                Height="21" OnClick="btnRegresar_Click" />
        </div>
        <asp:HiddenField ID="hfcaption" runat="server" Value="" />
        <asp:HiddenField ID="hfcaption2" runat="server" Value="" />
        <asp:HiddenField ID="hffechaing" runat="server" Value="" />
            <script language="javascript" type="text/javascript">
                function abrirArchivo() {
                    var labelcap = '<%=hfcaption.ClientID%>';
                    var labelcap2 = '<%=hfcaption2.ClientID%>';
                    var labelfechaing = '<%=hffechaing.ClientID%>';
                    var txtcap = document.getElementById(labelcap).value;
                    var txtcap2 = document.getElementById(labelcap2).value;
                    var txtfechaing = document.getElementById(labelfechaing).value;
                    window.open('../Impresiones/PrintConIndicadoresXEstado.aspx?caption=' + txtcap + '&caption2=' + txtcap2 + '&fechaIng=' + txtfechaing);
                    return false;
                }
        </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

