<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ConIndicadoresXSolPais.aspx.cs" Inherits="Paginas_ConIndicadoresXSolPais" Title="Indicador de solicitudes por paises" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/barraNav.ascx" TagName="BNav" TagPrefix="uc4" %>
<%@ Register Src="~/Controles/BotonesGraficos.ascx" TagName="BGraf" TagPrefix="uc4" %>
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
    
        <div align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 10px; padding-bottom: 10px; width:98%">
        <table width="70%">
        <tr>
        <td style=" width:250px"><font size="1">Tipo período</font>&nbsp;&nbsp;<asp:DropDownList ID="ddlTPeriodo" runat="server" AutoPostBack="False" TabIndex="1" Width="150px">
                            <asp:ListItem Selected="True" Value="0" Text="Anual"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Semestral"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Trimestral"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mensual"></asp:ListItem>
                            </asp:DropDownList></td>
        <td style=" width:90px"><font size="1">Año</font>&nbsp;&nbsp;
        <asp:TextBox ID="txtanio" runat="server" onkeyup="this.value=this.value.toUpperCase()"  Width="40px" CssClass="CajaTexto" TabIndex="2"></asp:TextBox>
        <asp:RequiredFieldValidator id="rfvtxtanio" ControlToValidate="txtanio" ErrorMessage="Ingresar año" Display="None" runat="server" ValidationGroup="paramConsulta"/>
        <cc1:ValidatorCalloutExtender ID="vcerfvtxtanio" runat="server" TargetControlID="rfvtxtanio"></cc1:ValidatorCalloutExtender> 
        <asp:RegularExpressionValidator ID="revtxtanio" runat="server" ControlToValidate="txtanio"  Display="None" ValidationExpression="^[0-9]{4,4}?$" ErrorMessage="Solo ingresar 4 dígitos enteros" ValidationGroup="paramConsulta"></asp:RegularExpressionValidator>
        <cc1:ValidatorCalloutExtender ID="vcerevtxtanio" runat="server" TargetControlID="revtxtanio"></cc1:ValidatorCalloutExtender> </td>
        <td style=" width:200px"><div id="dvsemestre" style="display:none; float:right; text-align:center"><asp:DropDownList ID="ddlSemestre" runat="server" AutoPostBack="False" TabIndex="3" Width="150px">
                            <asp:ListItem Selected="True" Value="1" Text="Primer semestre"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Segundo semestre"></asp:ListItem>
                            </asp:DropDownList></div>
        <div id="dvtrimestre"  style="white-space: nowrap; vertical-align:text-top; text-align: left; display:none"><asp:DropDownList ID="ddltrimestre" runat="server" AutoPostBack="False" TabIndex="3" Width="150px">
                            <asp:ListItem Selected="True" Value="1" Text="Primer trimestre"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Segundo trimestre"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Tercer trimestre"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Cuarto trimestre"></asp:ListItem>
                            </asp:DropDownList></div>
        <div id="dvMes"  style="white-space: nowrap; vertical-align:text-top; text-align: left; display:none"><asp:DropDownList ID="ddlMeses" runat="server" AutoPostBack="False" TabIndex="3" Width="150px">
                            <asp:ListItem Selected="True" Value="1" Text="Enero"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Febrero"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Marzo"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Abril"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Mayo"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Junio"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Julio"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Septiembre"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Octubre"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Noviembre"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Diciembre"></asp:ListItem>
                            </asp:DropDownList></div>
                            </td>
        </tr>
        </table>
        <div align="center" style="margin-top: 10px; padding-right:10px">
        <asp:Button   ID="btnConsultar" runat="server" CssClass="Botones" Text="Consultar" Width="110px" TabIndex="4" Height="20"
                OnClick="btnConsultar_Click" ValidationGroup="paramConsulta" />&nbsp;
            <asp:Button   ID="btnRegresar" runat="server" CssClass="Botones" Text="Regresar" Width="110px" TabIndex="5"
                Height="20" OnClick="btnRegresar_Click" />&nbsp;
            <asp:Button   ID="btnImprimir" runat="server" CssClass="Botones" Text="Imprimir" Width="90px" TabIndex="6" Height="20"
                        OnClientClick="abrirArchivo();return false;"/>&nbsp;
                    <asp:Button   ID="btnToExcell"  runat="server" CssClass="Botones" Text="Exportar a Excell"  Width="100px" TabIndex="7" Height="20"
                        onclick="btnToExcell_Click"/>
            </div></div>
        <div id="dvDatosConsulta" runat="server" align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 10px; padding-bottom: 10px; width:98%; margin-bottom:15px">
        <div  style="text-align:center; width:98%">
            <div style="padding: 10px 0 0px 0; height: auto; text-align:center; width:98% ">
                <div style=" width:70%; text-align:left">
                            <table width="98%" style=" height:25px; font-family:Arial, tahoma, helvetica; font-size: 8pt;font-weight:bold;text-align:center" class="GrillaHead">
                            <tr>
                                <td style=" width:50%">Pais</td>
                                <td style=" width:25%">Total</td>
                                <td style=" width:25%">%</td>
                            </tr>
                            </table>
                            </div>    
            <div id="divListado" style="overflow: auto; width:70%; text-align:left">
                    <asp:GridView runat = "server" ID="gridListadoSolicitudes" GridLines="None" Width="98%" Visible="true" AutoGenerateColumns="False" ShowHeader="false" Height="5px">
                        <Columns>
                            <asp:BoundField DataField="Descripcion">
                                <ItemStyle HorizontalAlign="Left" Width="50%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Total">
                                <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Porcentual">
                                <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                            </asp:BoundField>
                         </Columns>
                    </asp:GridView>
                </div>
            <div style=" width:70%; text-align:left; margin-bottom:10px">
                            <table width="98%" style=" height:20px; font-family:Arial, tahoma, helvetica; font-size: 8pt;font-weight:bold;text-align:center" class="GrillaHead">
                            <tr>
                                <td style=" width:50%" align="left">Totales</td>
                                <td style=" width:25%"><asp:Label ID="lblTotalPaises" runat="server"></asp:Label></td>
                                <td style=" width:25%">100 %</td>
                            </tr>
                            </table>
                            </div>    

                <div style=" width:90%; text-align:center; margin-bottom:10px; margin-top:10px">
                <asp:Literal ID="litGrafico" runat="server" ></asp:Literal>
                <div style="margin-top:10px"><uc4:BGraf ID="botonesGraficos" runat="server" />
                    </div>
                    
            </div>
        </div>
        </div>
            </div>
        <div id="dvNODatosConsulta" style="padding-top:5px" runat="server" align="center" class="TextoNegroCENTER">
                                No existen datos para la consulta solicitada.
                            </div>
        </div>
        <asp:HiddenField ID="hfcaption" runat="server" Value="" />
        <asp:HiddenField ID="hftgraf" runat="server" Value="" />
<script type="text/javascript" language="javascript">
    function Seleccionar(combo) {
        var valor = combo.options[combo.selectedIndex].value;
        //var texto = combo.options[combo.selectedIndex].text;

        dvsemestre.style.display = eval(valor == 1) ? '' : 'none';
        dvtrimestre.style.display = eval(valor == 2) ? '' : 'none';
        dvMes.style.display = eval(valor == 3) ? '' : 'none';
    }
            </script>
            <script language="javascript" type="text/javascript">
                function abrirArchivo() {
                    var labelcap = '<%=hfcaption.ClientID%>';
                    var labeltgr = '<%=hftgraf.ClientID%>';
                    var txtcap = document.getElementById(labelcap).value;
                    var txttgr = document.getElementById(labeltgr).value;
                    window.open('../Impresiones/PrintConIndicadoresXSolPaises.aspx?caption=' + txtcap + '&tGrafico=' + txttgr);
                    return false;
                }
        </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

