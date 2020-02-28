<%@ Page Title="Información del Solicitante"  Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="InformacionCompletaBeneficio.aspx.cs" Inherits="Paginas_InformacionCompleta" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Src="../Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc2" %>
<%@ Register Src="~/Controles/barraNav.ascx" TagName="BNav" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <uc4:BNav id="BNav" runat="Server"/>
    <uc3:MError2 ID="MError" runat="server" />
    <div class="tblMasterPage" style="width: 99%; margin-bottom:10px; text-align:center">
    <div class="FondoBlanco" style="margin-bottom:10px;  overflow:auto; height:500px; width:100%">
            <div id="divSolicitanteEncab" class="Titulo2" style="vertical-align: middle; padding: 10px 5px 10px 5px; width:98%">
                    <asp:HiddenField id="HFidBeneficiario" runat="server" Value="" />
                    <div style="height: 100%; padding: 4px 7px 0 7px; font-family: Verdana; font-size: 16px; font-weight: bold">
                        <div style="float: left">
                            Solicitante:&nbsp;
                            <asp:Label runat="server" Font-Size="Medium" CssClass="TextoNegroBold" ID="lbApeNom" />
                            </div>
                    </div>
            </div>
            <div id="principal" style="padding: 5px 0 5px 0; margin-left:10px; margin-right:10px; clear: both; color: Black; width:98%">
                <div id="divdatosBeneficiario" runat="server">
                            <table width="100%">
                            <tr>
                            <td  style=" width:75%; padding-left:15px">
                            <table cellpadding="2" cellspacing="|3" width="98%" >
                                    <tr>
                                        <td colspan="2"><font size="1">Código SIACI:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblCodSIACI"  /></font></td>
                                    </tr>
                                    <tr>
                                        <td style=" width:60%"><font size="1">Fecha de Nacimiento:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbFechaNacimiento"  /></font></td>
                                        <td><font size="1">Sexo:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="LbSexo"  /></font></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"><font size="1">Apellido materno:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbApellidoM"  />
                                        &nbsp;&nbsp;&nbsp;&nbsp;CUIL/T&nbsp;<asp:Label runat="server" ID="lblCuipB"  /></font></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"><font size="1">Nacionalidad:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="LbPais"   /></font></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"><font size="1">Dirección:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbDirCalleBen"/>
                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lbDirNumBen"/>
                                        &nbsp;&nbsp;Piso / depto:&nbsp;<asp:Label runat="server" ID="lbDirPisoBen"/>&nbsp;<asp:Label runat="server" ID="lbDirDeptoBen"/>
                                        </font></td>
                                        </tr>
                                    <tr>
                                        <td colspan="2"><font size="1">Entre calles:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbEcalleB1"/>&nbsp;y&nbsp;
                                        <asp:Label runat="server" ID="lbEcalleB2"/>
                                        </font></td></tr>
                                    <tr>
                                        <td colspan="2"><font size="1">
                                        Localidad (Prov.)&nbsp;<asp:Label runat="server" ID="lbProvLocalidadBen"/>
                                        </font></td></tr>
                                    <tr>
                                        <td colspan="2"><font size="1">Dirección extranjera:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbDirExtranjera"/>
                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lbCiudadExtranjera"/></font></td>
                                    </tr>
                                </table></td>
                            <td  style=" width:25%; padding-left:15px; vertical-align:top">
                            <div id="dvconDocumentos" style="padding-top:5px;text-align:left; vertical-align:text-top" runat="server">
                                            <font size="1">Documentos</font>
                                            <asp:GridView runat = "server" ID="gridListadoDocBeneficiarios" PageSize="15" GridLines="None" Visible="true" AutoGenerateColumns="False" ShowHeader="false" Width="200px">
                                                <Columns>
                                                    <asp:BoundField DataField="AbrevDocumento" HeaderText="Tipo"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15px" />
                                                    <asp:BoundField DataField="numDoc" HeaderText="Número" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                                                    <asp:BoundField DataField="codAbrevPais" HeaderText="Pais origen"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15px"/>
                                                    <asp:BoundField DataField="fechaAlta" HeaderText="F. alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="30px"/>
                                                    <asp:BoundField DataField="CodigoDocumento" Visible="false"/>
                                                    </Columns></asp:GridView>
                                                </div>
                            </td></tr>
                            </table>
                    <%--Datos Solicitudes provisorias--%>
                            <h2>Presentaciones provisorias</h2>
                            <div class="FondoClaro" style="text-align:center; width:98%; padding-top:5px; padding-left:10px; padding-right:10px">
                                <asp:repeater id="rptProvisorias" runat="server" OnItemDataBound="rptProvisorias_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="98%">
                                    </headertemplate>
                                    <itemtemplate>
                                        <tr>
                                        <td style=" width:50%"><font size="1">
                                            <asp:HiddenField ID="hfIdCodPrestacion" runat="server" Value="" />
                                            Nro de Solicitud:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblnroSolicitud"/>
                                        </font></td>
                                        <td style="width:25%"><font size="1">Trámite:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblTramite"/></font></td>
                                            <td align="right"><font size="1"><asp:Label runat="server" ID="lbFalta"/>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                        <td colspan="3"><font size="1"><asp:Label runat="server" ID="lbDatosReferencia"/>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="3"><font size="1"><asp:Label runat="server" ID="lblSectsoDerivacion"/></font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="3" style="text-align:center">
                                            <asp:ImageButton ID="btnVerDocumentosAdjuntos" runat="server" CommandName="VerDocumentosAdjuntos"  CommandArgument='<%#Eval("Nro_SolicitudProvisoria")%>' AlternateText="Ver documentación adjunta"  ImageUrl="~/App_Themes/Imagenes/icon_lupe2.gif"></asp:ImageButton>
                                            &nbsp;&nbsp;<asp:ImageButton ID="btnImprimirComprobante" runat="server"  CommandName="ImprimirComprobante"  CommandArgument='<%#Eval("Nro_SolicitudProvisoria")%>' AlternateText="Imprimir comprobante" ImageUrl="~/App_Themes/Imagenes/Print.gif"></asp:ImageButton>
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
                            <%--FIN Datos de solicitudes provisorias--%>

                            <%--Datos de Solicitudes--%>
                            <h2>Solicitudes prestacionales de Convenio</h2>
                            <div class="FondoClaro" style="text-align:center; width:98%; padding-top:5px; padding-left:10px; padding-right:10px">
                            <table width="100%">
                            <tr>
                            <td  style=" width:100%" colspan="2">
                            <div id="dvConSolicitudes" style="padding-top:5px;text-align:left" runat="server" align="center">
                            <asp:UpdatePanel ID="udp1" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                            <asp:GridView runat = "server" ID="gvPrestacionesBeneficiario" PageSize="15" GridLines="None" Width="100%" Visible="true" AutoGenerateColumns="False" UseAccessibleHeader="true" 
                                            AllowSorting="false"  OnRowCommand="RowCommand" OnRowDataBound="gvPrestacionesBeneficiario_RowDataBound" DataKeyNames="codPrestacion,Prestacion,IdBeneficiario">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acciones" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                        <ItemTemplate>
                                                              <asp:ImageButton ID="btnVerDatosPrestacion" runat="server" CommandName="VerDatos"  CommandArgument='<%#Eval("codPrestacion")+ ";" + Eval("Prestacion")  + ";" + Eval("Pais") + ";" + Eval("codPais")%>' AlternateText="Ver datos de la petición"  ImageUrl="~/App_Themes/Imagenes/icon_lupe2.gif"></asp:ImageButton>
                                                              &nbsp;&nbsp;
                                                              <asp:ImageButton ID="btnModificarDatosPrestacion" runat="server"  CommandName="ModificarDatos"  CommandArgument='<%#Eval("codPrestacion")+ ";" + Eval("Prestacion") + ";" + Eval("Pais") + ";" + Eval("codPais")%>' AlternateText="Modificar datos de la petición" ImageUrl="~/App_Themes/Imagenes/Edicion.gif"></asp:ImageButton>
                                                              &nbsp;&nbsp;
                                                              <asp:ImageButton ID="btnNotificar" runat="server"  CommandName="Notificar"  CommandArgument='<%#Eval("codPrestacion")+ ";" + Eval("Prestacion") + ";" + Eval("Pais") + ";" + Eval("codPais")%>' AlternateText="Notificar" ImageUrl="~/App_Themes/Imagenes/seleccion.gif"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="IdBeneficiario" Visible="false"/>
                                                    <asp:BoundField DataField="col_Pais_Prestacion" HeaderText="País - Prestación"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="35%"/>
                                                    <asp:BoundField DataField="codPrestacion" Visible="false"/>
                                                    <asp:BoundField DataField="col_FMov_Estado_Sector" HeaderText="Fecha - Estado - Sector"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="35%"/>
                                                    <asp:BoundField DataField="Mercosur" Visible="false"/>
                                                    <asp:TemplateField HeaderText="Mercosur" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgMercoS" runat="server" ImageUrl="~/App_Themes/Imagenes/ok.png"/>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="codPais" Visible="false"/>
                                                    <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                        <ItemTemplate>
                                                              <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar"  CommandArgument='<%#Eval("codPrestacion")+ ";" + Eval("Prestacion")  + ";" + Eval("Pais") + ";" + Eval("codPais")%>' AlternateText="Eliminar solicitud"  ImageUrl="~/App_Themes/Imagenes/Borrar.gif"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                            </ContentTemplate></asp:UpdatePanel>
                            </div>
                            <div id="dvSinSolicitudes" style="padding-top:5px" runat="server" align="center" class="TextoNegroBoldCENTER">
                            No registra.
                            </div>
                            </td>
                            </tr>
                            <tr>
                            <td  align="left" valign="middle" style=" width:50%">
                            <asp:Button  ID="btnAgregarSolicitud"   Text="Nueva solicitud" runat="server" CssClass="Botones" OnClick="btnAgregarSolicitud_Click" Width="130px" Height="21px"/>
                            <%--<asp:Button  ID="btnActualizarSolicitudes"   Text="Actualizar" runat="server" CssClass="Botones" OnClick="btnActualizarSolicitud_Click" Width="130px" Height="21px"/>--%>
                            </td>
                                <td  align="right" valign="middle" style=" width:50%">
                                    <asp:ImageButton   ID="btnActualizarSolicitudes"  AlternateText="Actualizar" runat="server" OnClick="btnActualizarSolicitud_Click" ImageUrl="~/App_Themes/Imagenes/refresh.png" Height="30px" Width="30px"/>
                                    </td>
                            </tr>
                            </table>
                                </div>
                            <%--FIN Datos de solicitudes--%>
                            <%--Datos del Causante--%>
                            <div id="dvConCausante" runat="server">
                            <h2>Datos del Causante</h2>
                            <div class="FondoClaro" style="text-align:center; width:98%; padding-top:5px; padding-left:10px; padding-right:10px">
                                <table cellpadding="1" cellspacing="2" width="98%" style=" width:70%; padding-left:15px">
                                        <tr>
                                            <td><font size="1">Fecha defunción:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblFechaDefuncionC"  /></font></td>
                                            <td rowspan="4" valign="top"><div id="dvCconDocumentos" style="padding-top:5px;text-align:left" runat="server" align="center">
                                            <font size="1">Documentos</font>
                                            <asp:GridView runat = "server" ID="gridListadoDocCausantes" PageSize="15" GridLines="None" Visible="true" AutoGenerateColumns="False" ShowHeader="false" Width="200px">
                                                <Columns>
                                                    <asp:BoundField DataField="AbrevDocumento" HeaderText="Tipo"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15px" />
                                                    <asp:BoundField DataField="numDoc" HeaderText="Número" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                                                    <asp:BoundField DataField="codAbrevPais" HeaderText="Pais origen"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15px"/>
                                                    <asp:BoundField DataField="fechaAlta" HeaderText="F. alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="30px"/>
                                                    <asp:BoundField DataField="CodigoDocumento" Visible="false"/>
                                            </Columns></asp:GridView>
                                                </div></td>
                                        </tr>
                                        <tr>
                                        <td><font size="1">Apellido y Nombre:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblApeNomC"  />
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td><font size="1">Fecha nacimiento:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblFechaNacC"  />
                                        &nbsp;&nbsp;&nbsp;&nbsp;CUIL/T&nbsp;<asp:Label runat="server" ID="lblCuipC"  />
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td><font size="1">
                                        Sexo:&nbsp;<asp:Label runat="server" ID="lblsexoC"  />
                                        </font></td>
                                        </tr>
                                </table>
                                </div>
                                </div>
                            <%--Datos de Apoderados--%>
                            <div id="dvConApoderados" runat="server">
                            <h2>Apoderados</h2>
                                <div class="FondoClaro" style="text-align:center; width:98%; padding-top:5px; padding-left:10px; padding-right:10px">
                                <asp:repeater id="rptApoderados" runat="server" OnItemDataBound="rptApoderados_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="98%">
                                    </headertemplate>
                                    <itemtemplate>
                                        <tr>
                                        <td style=" width:50%"><font size="1">Apoderado:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblSTipoApod"/>
                                        &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblTipoApoderadoAp"/>
                                        &nbsp;(<asp:Label runat="server" ID="lblTipoPoderAp"  />)</font></td>
                                        <td><font size="1">Ape y Nom / Razón Social:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblApeNomAp"/></font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"><font size="1">Fecha alta:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbFalta"/>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"><font size="1">Dirección:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbDirCalleAp"/>
                                        &nbsp;<asp:Label runat="server" ID="lbDirNumAp"/>
                                        &nbsp;<asp:Label runat="server" ID="lbDirPisoAp"/>
                                        &nbsp;<asp:Label runat="server" ID="lbDirDeptoAp"/>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"><font size="1">E / calles:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbDirEC1Ap"/>
                                        &nbsp;y&nbsp;<asp:Label runat="server" ID="lbDirEC2Ap"/></font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"><font size="1">
                                        Localidad (Pcia)&nbsp;&nbsp;<asp:Label runat="server" ID="lbProvLocalidadAp"/>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td><font size="1">Documento:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbCodAbrevTdocA"  />&nbsp;-&nbsp;<asp:Label runat="server" ID="lbNumDocA"/></font></td>
                                        <td><font size="1">Banco:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblBancoAp"/></font></td>
                                        </tr>
                                        <tr>
                                        <td><font size="1">E Mail:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblEmailAp"  /></font></td>
                                        <td><font size="1">Telefono:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblTelefonoAp"  /></font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"><font size="1">Observaciones:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblObservaciones"  /></font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"><hr /></td>
                                        </tr>
                                    </itemtemplate>
                                    <footertemplate>
                                      </table>
                                    </footertemplate>
                                  </asp:repeater>
                            </div>
                            </div>
                            <%--Fin Datos de Apoderados--%>
                            
                </div>
                <div id="divBeneficiarioErr" runat="server" style="vertical-align: middle; text-align:center; line-height: 25px" class="TextoNegroBoldCENTER">
                            Beneficiario inexistente
                </div>
            </div>
        </div>
        <div align="right" style="margin-top: 10px">
            <asp:Button  ID="btnModificarBeneficiario"   Text="Modificar" runat="server" CssClass="Botones" Width="130px" Height="21px" OnClick="btnModificarBeneficiario_Click"/>&nbsp;
            <asp:Button  ID="btnRegresar"   Text="Regresar" runat="server" CssClass="Botones" Width="130px" Height="21px" OnClick="btnRegresar_Click" />&nbsp;
            <asp:Button ID="btnImprimir"  Text="Imprimir" runat="server" CssClass="Botones" Width="130px" Height="21px" OnClientClick="abrirArchivo();return false;" />
        </div>
    </div>
    <uc2:Mensaje ID="mensaje" runat="server" /> 
    <script language="javascript" type="text/javascript">
    function abrirArchivo() {
            window.open('../Impresiones/InformacionCompletaBeneficioPrint.aspx');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>

