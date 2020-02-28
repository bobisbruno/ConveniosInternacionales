<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultaTProvisorios.aspx.cs" Inherits="Paginas_ConsultaTProvisorios" Title="Trámites provisorios" %>
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
        <div align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 10px; padding-bottom: 10px; width:99%; margin-bottom:10px">
                <table width="70%"><tr><td>
                    <font size="1">Año</font>&nbsp;&nbsp;
        <asp:TextBox ID="txtAnio" runat="server" onkeyup="this.value=this.value.toUpperCase()"  Width="50px" CssClass="CajaTexto" TabIndex="1"></asp:TextBox>
        <asp:RequiredFieldValidator id="rfvtxtAnio" ControlToValidate="txtAnio" ErrorMessage="Ingresar año." Display="None" runat="server" ValidationGroup="paramConsulta"/>
        <cc1:ValidatorCalloutExtender ID="vcerfvtxtAnio" runat="server" TargetControlID="rfvtxtAnio"></cc1:ValidatorCalloutExtender> 
        <cc1:FilteredTextBoxExtender ID="fttxtAnio" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtAnio" runat="server"></cc1:FilteredTextBoxExtender>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size="1">Mes</font>&nbsp;&nbsp;
            <asp:DropDownList ID="ddlMeses" runat="server" AutoPostBack="False" TabIndex="2" Width="110px">
                            <asp:ListItem Selected="True" Value="0" Text="Todos"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Enero"></asp:ListItem>
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
                            </asp:DropDownList>
                &nbsp;&nbsp;<asp:CheckBox ID="chksoloProvisorios" runat="server" AutoPostBack="false" Checked="true" TabIndex="3" Text="Sin verificar" TextAlign="Left" Font-Size="Smaller" Font-Bold="true"/>
                    &nbsp;&nbsp;<font size="1">Pais convenio</font>&nbsp;&nbsp;<asp:DropDownList ID="ddlPaisConvenio" runat="server" TabIndex="4" Width="160px" DataTextField="Descripcion" DataValueField="Pais_PK"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <font size="1">Prestación</font>&nbsp;&nbsp;<asp:DropDownList ID="ddlPrestacionesS" runat="server" AutoPostBack="False" TabIndex="5" DataTextField="Descripcion"  DataValueField="Cod_Prestacion" Width="190px"></asp:DropDownList>
                        </td></tr>
                </table>
            </div>
        <h2><asp:Label runat="server" ID="lbTituloConsulta"/></h2>
        <div id="dvDatosConsulta" runat="server" align="center" class="FondoClaro" style="margin-top: 5px; padding-top: 10px; padding-bottom: 10px; width:99%">
            <asp:HiddenField ID="hfEncabezado" runat="server" Value="" />
            <div class = "TituloBold" style="vertical-align: middle; text-align:right; padding-right:10px; padding-left: 10px; padding-bottom:1px; font-size: 8pt">
                <asp:Label ID = "lbElementosEncontrados" runat = "server" Text = "" />&nbsp;elementos encontrados
            </div>
            <table><tr><td style="width:100%; text-align:center">
            <div style="padding: 10px 0 0px 0; width: auto; height: auto; text-align:center ">
                <div id="divListado" style="overflow: auto; height: 400px; width:99%; text-align:center">
                    <asp:GridView runat = "server" ID="gridListadoSolicitudes" GridLines="None" OnRowCommand="RowCommand"
                        Width="98%" Visible="true" AutoGenerateColumns="False" ShowHeader="true" Height="5px"
                        AllowSorting="True" OnSorted="gv_Grilla_Sorted" OnSorting="gv_Grilla_Sorting" >
                        <HeaderStyle Height="30px" HorizontalAlign="Center" />
                        <RowStyle Height="25px" />
                        <Columns>
                            <asp:BoundField DataField="ApellidoyNombre" HeaderText = "Apellido y nombre"  SortExpression="ApellidoyNombre">
                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
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
                            <asp:BoundField DataField = "EsProvisoria" HeaderText = "Provisoria" >
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
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%"  HeaderText = "Acciones" >
                            <ItemTemplate>
                                <asp:ImageButton ID="btnImprimeComprobante" TabIndex="8" runat="server" CommandName="ImprimirComprobante"  CommandArgument='<%#Eval("Nro_SolicitudProvisoria")%>'
                                     AlternateText="Ver comprobante"  Width="14px" Height="14px" ImageUrl="~/App_Themes/Imagenes/Print.jpg"></asp:ImageButton>&nbsp;
                                <asp:ImageButton ID="btnVerDocs" TabIndex="7" runat="server" CommandName="Verdocumentos"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                     AlternateText="Ver documentos"  Width="14px" Height="14px" ImageUrl="~/App_Themes/Imagenes/seleccion.gif"></asp:ImageButton>
                            </ItemTemplate>
                            </asp:TemplateField>
                            
                         </Columns>
                    </asp:GridView>
                </div>
                </div>
                </td></tr></table>
        </div>
        <div id="dvNODatosConsulta" style="padding-top:5px; margin-top:10px; margin-bottom:10px" runat="server" align="center" class="TextoNegroBoldCENTER">
                                No existen datos para la consulta solicitada.
                            </div>
        <div align="center" style="margin-top: 5px">
                <asp:Button   ID="btnConsultar" runat="server" CssClass="Botones" Text="Consultar" Width="110px" TabIndex="7"
                Height="21" OnClick="btnConsultar_Click" ValidationGroup="paramConsulta" />&nbsp;
            <asp:Button   ID="btnRegresar" runat="server" CssClass="Botones" Text="Regresar" Width="110px" TabIndex="8"
                Height="21" OnClick="btnRegresar_Click" />&nbsp;
            <asp:Button   ID="btnImprimir" runat="server" CssClass="Botones" Text="Imprimir listado" Width="110px" Height="21" TabIndex="9"
                        OnClientClick="abrirArchivo();return false;"/>&nbsp;
            <asp:Button   ID="btnToExcell"  runat="server" CssClass="Botones" Text="Exportar a Excell"  Width="115px" Height="21" TabIndex="10" 
                        onclick="btnToExcell_Click"/>
                </div>
        </div></div>
        <script language="javascript" type="text/javascript">
            function abrirArchivo() {
                var labelID = '<%=hfEncabezado.ClientID%>';
                var txtEncabezado = document.getElementById(labelID).value;
                window.open('../Impresiones/ListadoSolicitudesProvisorias.aspx?ec=' + txtEncabezado);
                return false;
            }
        </script>
    <cc1:ModalPopupExtender ID="mpeShowDocumentos" runat="server" PopupDragHandleControlID="divDetalleTabla" DropShadow="true" BackgroundCssClass="modalBackground" TargetControlID="btnShowPopupDocumentos"
                        PopupControlID="divViewDocumentos" CancelControlID="imgCerrarDatosViewDocumentos">
                    </cc1:ModalPopupExtender>
     <asp:Button ID="btnShowPopupDocumentos" runat="server" Style="display: none" />
     <div style="position:relative">
                        <div style="position: fixed; top: 0px; left: 0px;">
                            <div id="divViewDocumentos" class="FondoClaro" style="width: 800px; display:none" align="center">
                                <div class="FondoOscuro" style="float: left; padding: 5px 0px 5px 0px; text-align: left; width:100%"title="titulo">
                                    <span class="TextoBlancoBold" style="float: left; margin-left: 10px">Documentos registrados</span>
                                    <img id="imgCerrarDatosViewDocumentos" alt="Cerrar ventana" src="../App_Themes/Imagenes/Error_chico.gif"
                                        style="cursor: hand; border: none; float: right; vertical-align: middle; margin-right: 10px; display:none"/>
                                </div>
                                <asp:UpdatePanel runat="server" UpdateMode="Always" ID="udpDocumentos">
                                <ContentTemplate>
                                <table border="0" style="text-align: left; margin: 10px auto 0px auto;" cellpadding="1" cellspacing="2" width="99%">
                                    <tr>
                                        <td align="center">
                                            <div class="fdo_constancia" style="width: 99%; margin-top: 5px">
                                                <div class="FondoBlanco" style="width: 99%; padding-bottom:5px">
                                                        <h4>Documentación registrada en trámite provisorio nro.&nbsp;<asp:Label ID="lbTrProvisorioEncab" runat="server"></asp:Label></h4>
                                                        <br />
                                                        <div style="width: 99%; text-align:left; padding-bottom:5px; margin-left:10px">
                                                            <div style="overflow: auto; height: 200px; width:99%; text-align:center">
                    <asp:GridView runat = "server" ID="gvMovimientos" GridLines="None" OnRowCommand="RowCommand"
                        Width="98%" Visible="true" AutoGenerateColumns="False" ShowHeader="True" Height="5px" EmptyDataText="No registra documentación"  OnRowDataBound="dgMovimientos_RowDataBound">
                        <HeaderStyle HorizontalAlign="Center" Height="30px" />
                        <Columns>
                            <asp:BoundField DataField="Nro_SolicitudProvisoria" HeaderText = "Solicitud" >
                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DescripcionBreve" HeaderText = "Observación" >
                                <ItemStyle HorizontalAlign="Left" Width="35%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Digitalizado"  HeaderText = "Digitalizado" >
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "TipoDocumentacion" HeaderText = "Documentación" >
                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "SecuenciaDocumento" HeaderText = "Secuencia" >
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderText="Ver">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnVerDocs" TabIndex="7" runat="server" CommandName="VerDigitalizado"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                     AlternateText="Ver documento"  Width="20px" Height="20px" ImageUrl="~/App_Themes/Imagenes/seleccion.gif"></asp:ImageButton>
                            </ItemTemplate>
                            </asp:TemplateField>
                            
                         </Columns>
                    </asp:GridView>
                </div>

                                                        
                                                                </div>
                                                        
                                                    </div>
                                                    </div>
                                                </div>
                                            <div style="text-align:center; width: 98%; margin-top: 10px; margin-bottom: 5px">
                                                &nbsp;<asp:Button   ID="btnCerrar" Text="Cerrar" runat="server" CssClass="Botones" Width="150px" Height="20px" onclick="btnCerrarMovimientos_Click" TabIndex="109"/></div>
                                        </td>
                                    </tr>
                                </table>
                                    
                                </ContentTemplate></asp:UpdatePanel>
                            </div>
                        </div>
         </div>
    <!--FIN modal-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

