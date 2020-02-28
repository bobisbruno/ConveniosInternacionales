<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultaSolicitudes.aspx.cs" Inherits="Paginas_ConsultaSolicitudes" Title="Lista de Beneficiarios" %>
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
        <div align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 10px; padding-bottom: 10px; width:99%">
        <font size="1">Fecha desde: (dd/mm/AAAA)</font>&nbsp;&nbsp;
        <asp:TextBox ID="txtFechaDesde" runat="server" onkeyup="this.value=this.value.toUpperCase()"  Width="70px" CssClass="CajaTexto" TabIndex="1"></asp:TextBox>
        <%--<cc1:CalendarExtender runat="server" ID="CEini" FirstDayOfWeek="Sunday" TargetControlID="txtFechaDesde" PopupButtonID="btnCalendarIni"></cc1:CalendarExtender>--%>
        <asp:RequiredFieldValidator id="rfvtxtFechaDesde" ControlToValidate="txtFechaDesde" ErrorMessage="Ingresar Fecha." Display="None" runat="server" ValidationGroup="paramConsulta"/>
        <cc1:ValidatorCalloutExtender ID="vcerfvtxtFechaDesde" runat="server" TargetControlID="rfvtxtFechaDesde"></cc1:ValidatorCalloutExtender> 
        <asp:RangeValidator ID="rvtxtFechaDesde" runat="server" Type="Date" ControlToValidate="txtFechaDesde" ErrorMessage="Fecha no debe ser mayor de 10 años, ni inferior de 50 años a la fecha actual." Display="None" ValidationGroup="paramConsulta"/>
        <cc1:ValidatorCalloutExtender ID="vcervtxtFechaDesde" runat="server" TargetControlID="rvtxtFechaDesde"></cc1:ValidatorCalloutExtender> 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size="1">Fecha hasta: (dd/mm/AAAA)</font>&nbsp;&nbsp;
        <asp:TextBox ID="txtFechaHasta" runat="server" onkeyup="this.value=this.value.toUpperCase()"  Width="70px" CssClass="CajaTexto" TabIndex="2"></asp:TextBox>
        <%--<cc1:CalendarExtender runat="server" ID="CEFin" FirstDayOfWeek="Sunday" TargetControlID="txtFechaHasta" Animated="true" PopupButtonID="btnCalendarFin"></cc1:CalendarExtender>--%>
        <asp:RequiredFieldValidator id="rfvtxtFechaHasta" ControlToValidate="txtFechaHasta" ErrorMessage="Ingresar Fecha." Display="None" runat="server" ValidationGroup="paramConsulta"/>
        <cc1:ValidatorCalloutExtender ID="vcerfvtxtFechaHasta" runat="server" TargetControlID="rfvtxtFechaHasta"></cc1:ValidatorCalloutExtender> 
        <asp:RangeValidator ID="rvtxtFechaHasta" runat="server" Type="Date" ControlToValidate="txtFechaHasta" ErrorMessage="Fecha no debe ser mayor de 10 años, ni inferior de 50 años a la fecha actual." Display="None" ValidationGroup="paramConsulta"/>
        <cc1:ValidatorCalloutExtender ID="vcervtxtFechaHasta" runat="server" TargetControlID="rvtxtFechaHasta"></cc1:ValidatorCalloutExtender> 
        <asp:CompareValidator ID="cvtxtFechaHasta" ControlToValidate="txtFechaHasta" ControlToCompare="txtFechaDesde"  Display="None" runat="server" ValidationGroup="paramConsulta" ErrorMessage="Fecha hasta debe ser igual o superior a Fecha desde." Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>
        <cc1:ValidatorCalloutExtender ID="vcecvtxtFechaHasta" runat="server" TargetControlID="cvtxtFechaHasta"></cc1:ValidatorCalloutExtender>
        <asp:CustomValidator ID="cvtxtFechaHastaDif" runat="server" OnServerValidate="FechValidateDays" ControlToValidate="txtFechaHasta" ErrorMessage="Fecha desde y hasta no debe superar los 120 días de diferencia." Display="None" ValidationGroup="paramConsulta"/>
        <cc1:ValidatorCalloutExtender ID="vcecvtxtFechaHastaDif" runat="server" TargetControlID="cvtxtFechaHastaDif"></cc1:ValidatorCalloutExtender>
            <div align="center" style="margin-top: 10px; padding-right:10px">
                <font size="1">Prestación:</font>&nbsp;&nbsp;
                <asp:DropDownList ID="ddlPrestacionesS" runat="server" TabIndex="3" DataTextField="Descripcion"  DataValueField="Cod_Prestacion" Width="200px"></asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;<font size="1">País convenio:</font>&nbsp;&nbsp;
                <asp:DropDownList ID="ddlPaisS" runat="server" TabIndex="4" DataTextField="Descripcion"  DataValueField="Pais_PK" Width="150px"/>
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkMercosurS" runat="server" AutoPostBack="false" Checked="false" TabIndex="5" Text="Mercosur" TextAlign="Left"/>
                &nbsp;&nbsp;&nbsp;&nbsp;<font size="1">Ordenar por:</font>&nbsp;&nbsp;<asp:DropDownList ID="ddlOrderBy" runat="server" TabIndex="6" Width="90px">
                    <asp:ListItem Selected="True" Value="0" Text="Fecha"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Prestación"></asp:ListItem>
                    <asp:ListItem Value="2" Text="País convenio"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Apellido y nombre"></asp:ListItem>
                    </asp:DropDownList>
            </div>
        </div>
        <div id="dvDatosConsulta" runat="server" align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 10px; padding-bottom: 10px; width:99%">
            <asp:HiddenField ID="hffd" runat="server" Value="" />
            <asp:HiddenField ID="hffh" runat="server" Value="" />
            <div class = "TituloBold" style="vertical-align: middle; padding-left: 10px; padding-bottom:10px; font-size: 7pt">
                <asp:Label ID = "lbElementosEncontrados" runat = "server" Text = "" />&nbsp;elementos encontrados
            </div>
            <table><tr><td style="width:100%; text-align:center">
            <div style="padding: 10px 0 0px 0; width: auto; height: auto; text-align:center ">
            <div style=" width:99%; text-align:left">
                            <table width="98%" style=" height:30px; font-family:Arial, tahoma, helvetica; color:#ffffff;font-size: 8pt;font-weight:bold;background-color:#95B0DF;text-align:center">
                            <tr>
                                <td style=" width:25%">Apellido y nombre</td>
                                <td style=" width:15%">Prestación</td>
                                <td style=" width:15%">Pais convenio</td>
                                <td style=" width:5%">Fecha ult modificación</td>
                                <td style=" width:5%">CUIL-T</td>
                                <td style=" width:15%">Ref. CIACI</td>
                                <td style=" width:12%">Ubicación</td>
                                <td style=" width:8%">Ver trámite</td>
                            </tr>
                            </table>
                            </div>    
                <div id="divListado" style="overflow: auto; height: 400px; width:99%; text-align:center">
                    <asp:GridView runat = "server" ID="gridListadoSolicitudes" GridLines="None" OnRowCommand="RowCommand"
                        Width="98%" Visible="true" AutoGenerateColumns="False" ShowHeader="false" Height="5px">
                        <Columns>
                            <asp:BoundField DataField="ApeNomCompleto" HeaderText = "Apellido y nombre">
                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "DescripcionPrestacion">
                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "PaisDescCompleto" HeaderText = "Pais convenio" >
                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "fAMSolicitud" HeaderText = "Fecha solicitud">
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundField>
                                <asp:BoundField DataField = "CUIP">
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundField>
                                <asp:BoundField DataField = "RefExt" >
                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                            </asp:BoundField>
                                <asp:BoundField DataField = "UbicFisica">
                                <ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnVerDatosPrestacion" runat="server" CommandName="VerDatos"  CommandArgument='<%#Eval("codPrestacion")+ ";" + Eval("DescripcionPrestacion")  + ";" + Eval("PaisDescCompleto") + ";" + Eval("pais_PK") + ";" + Eval("id_Beneficiario") + ";" + Eval("ApeNomCompleto") + ";" + Eval("fAMSolicitud") + ";" + Eval("CUIP") + ";" + Eval("RefExt") + ";" + Eval("UbicFisica")%>'
                                     AlternateText="Ver datos de la petición"  ImageUrl="~/App_Themes/Imagenes/icon_lupe2.gif"></asp:ImageButton>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "Id_Beneficiario" Visible="false"/>
                            <asp:BoundField DataField = "codPrestacion" Visible="false"/>
                            <asp:BoundField DataField = "pais_PK" Visible="false"/>
                            <asp:BoundField DataField = "Mercosur" Visible="false"/>
                         </Columns>
                    </asp:GridView>
                </div>
                </div>
                </td></tr></table>
        </div>
        <div id="dvNODatosConsulta" style="padding-top:5px" runat="server" align="center" class="TextoNegroCENTER">
                                No existen datos para la consulta solicitada.
                            </div>
                            </div>
        <div align="center" style="margin-top: 5px; margin-bottom:5px">
                <asp:Button   ID="btnConsultar" runat="server" CssClass="Botones" Text="Consultar" Width="110px" TabIndex="7"
                Height="21" OnClick="btnConsultar_Click" ValidationGroup="paramConsulta" />&nbsp;
            <asp:Button   ID="btnRegresar" runat="server" CssClass="Botones" Text="Regresar" Width="110px" TabIndex="8"
                Height="21" OnClick="btnRegresar_Click" />&nbsp;
            <asp:Button   ID="btnImprimir" runat="server" CssClass="Botones" Text="Imprimir" Width="90px" Height="21" TabIndex="9"
                        OnClientClick="abrirArchivo();return false;"/>&nbsp;
            <asp:Button   ID="btnToExcell"  runat="server" CssClass="Botones" Text="Exportar a Excell"  Width="100px" Height="21" TabIndex="10" 
                        onclick="btnToExcell_Click"/>
                </div>
        </div>
        <script language="javascript" type="text/javascript">
            function abrirArchivo() {
                var labelID = '<%=hffd.ClientID%>';
                var labelID2 = '<%=hffh.ClientID%>';
                var txtfd = document.getElementById(labelID).value;
                var txtfh = document.getElementById(labelID2).value;
                window.open('../Impresiones/Listadosolicitudes.aspx?fd=' + txtfd + '&fh=' + txtfh);
                return false;
        }
        </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

