<%@ Page Title="Información peticiónes"  Language="C#" MasterPageFile="~/MasterPage/MasterPageNOMnu.master" AutoEventWireup="true" CodeFile="InfoCompletaPeticion.aspx.cs" Inherits="Paginas_InformacionCompletaPeticion" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Src="../Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPageNOMnu.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <uc3:MError2 ID="MError" runat="server" />
    <div class="tblMasterPage" style="width: 99%; margin-bottom:10px; text-align:center">
    <div class="TituloBold" style="text-align: left; width:98%; padding-top:5px; padding-bottom:5px">
            <asp:Label ID="lbEncabezadoBeneficiario" runat="server"></asp:Label></div>
        <div class="FondoClaro" style="margin-bottom:10px;  overflow:auto; height:500px; width:100%">
            <div id="principal" style="padding: 5px 0 5px 0; margin-left:10px; margin-right:10px; clear: both; color: Black; width:95%">
                <div id="divdatosSolicitud" runat="server">
                            <%--Datos del Beneficio--%>
                            <div class="accordionHeaderSelected">
                                Solicitudes de convenio&nbsp;&nbsp;<asp:Label ID="lbdescPrestacion" runat="server"></asp:Label>
                                &nbsp;&nbsp;<asp:Label ID="lbDescPais" runat="server"></asp:Label>
                            </div>
                            <%--Datos de Solicitudes--%>
                            <div style="padding-top:5px; padding-bottom:5px; margin-top:5px" align="center" class="TextoNegroCENTER">
                            Estado y Sector actual
                            </div>
                            <div style="padding-top:5px; padding-bottom:5px; margin-top:5px" align="center">
                            <table width="80%" class="TextoNegroBold"><tr><td><font size="1">Fecha última novedad:</font>&nbsp;<asp:Label ID="lbFechaUltMov" runat="server"></asp:Label></td>
                            <td><font size="1">Observación:</font>&nbsp;<asp:Label ID="lbUltMovObserv" runat="server"></asp:Label></td></tr>
                            <tr><td><font size="1">Estado:</font>&nbsp;<asp:Label ID="lbUltMovEstado" runat="server"></asp:Label></td>
                            <td><font size="1">Sector:</font>&nbsp;<asp:Label ID="lbUltMovSector" runat="server"></asp:Label></td></tr></table></div>
                            <div style="padding-top:5px; padding-bottom:5px; margin-top:5px" align="center" class="TextoNegroCENTER">
                            Peticiónes de solicitud ingresadas
                            </div>
                                <asp:repeater id="rptSolicitudes" runat="server" OnItemDataBound="rptSolicitudes_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="98%">
                                    </headertemplate>
                                    <itemtemplate>
                                        <tr>
                                        <td style=" width:50%"><font size="1">País convenio:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblPais"/></font>
                                        </td>
                                        <td><font size="1">Prestación solicitada:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbPrestacion"/>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"></td>
                                        </tr><td>
                                        <font size="1">Fecha de ingreso:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblFechaSolicitud"/></font></td>
                                        <tr>
                                        <td colspan="2"><font size="1">Observación:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbObservacion"/></font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"><font size="1">Ref. exterior:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbRefExterior"/></font>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td><font size="1">Ubic. Doc
                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lbUbicdoc"/>
                                        </font></td>
                                        <td><font size="1"><small style=" color:Red">
                                        <asp:Label runat="server" ID="lbDenegada"/></small>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"><hr /></td>
                                        </tr>
                                    </itemtemplate>
                                    <footertemplate>
                                      </table>
                                    </footertemplate>
                                  </asp:repeater>
                            <div style="padding-top:5px; padding-bottom:5px" align="center" class="TextoNegroCENTER">
                            Beneficios registrados
                            </div>
                            <asp:GridView runat = "server" ID="gridListadoBeneficio" PageSize="15" GridLines="None" Width="99%" Visible="false" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="numBeneficio" HeaderText="Beneficio" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                                                    <asp:BoundField DataField="fAltabeneficio" HeaderText="Fecha Alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
                                                    <asp:BoundField DataField="DTipoTrDerivado" HeaderText="Tipo"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
                                                    <asp:BoundField DataField="observacion" HeaderText="Observaciones"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="55%"/>
                                                </Columns></asp:GridView>
                            <div style="padding-top:5px; padding-bottom:5px; margin-top:5px" align="center" class="TextoNegroCENTER">
                            Expedientes registrados
                            </div>
                            <asp:GridView runat = "server" ID="gridListadoexpedienteBen" PageSize="15" GridLines="None" Width="99%" Visible="false" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="numExpte" HeaderText="Expediente" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                                                    <asp:BoundField DataField="fAltaexpediente" HeaderText="Fecha Alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
                                                    <asp:BoundField DataField="Caratula" HeaderText="Caratula"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="65%"/>
                                                    <asp:BoundField DataField="Observacion" Visible="false" HeaderText="Comentarios"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="35%"/>
                                                </Columns></asp:GridView>
                            <div style="padding-top:5px; padding-bottom:5px; margin-top:5px" align="center" class="TextoNegroCENTER">
                            Resúmen de operaciónes
                            </div>
                            <asp:GridView runat = "server" ID="gridMovimientosSol" PageSize="15" GridLines="None" Width="99%" Visible="false" AutoGenerateColumns="False" >
                                                <Columns>
                                                    <asp:BoundField DataField="Fecha_Movimiento" HeaderText="Fecha" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%"/>
                                                    <asp:BoundField DataField="TipoMovimiento" HeaderText="Acción" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%"/>
                                                    <asp:BoundField DataField="TipoIngreso" HeaderText="Ingreso" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                                                    <asp:BoundField DataField="Destino" HeaderText="Destino" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                                                    <asp:BoundField DataField="Sector" HeaderText="Sector" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                                                </Columns></asp:GridView>
                            <div style="padding-top:5px; padding-bottom:5px; margin-top:5px" align="center" class="TextoNegroCENTER">
                            Documentación recepcionada
                            </div>
                            <asp:repeater id="rptIngresos" runat="server" OnItemDataBound="rptIngresos_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="100%">
                                    </headertemplate>
                                    <itemtemplate>
                                        <tr>
                                        <td style=" width:50%"><font size="1">Fecha de ingreso:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbIngFIng"/></font>
                                        </td>
                                        <td><font size="1">Vía ingreso:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbIngVia"/>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2">
                                        <font size="1">Observaciónes:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbComentarioIng"/></font>
                                        </td>
                                        </tr><td>
                                        <font size="1"><strong>Documentación recibida:</strong></font></td>
                                        <tr>
                                        <td colspan="2"><font size="1">
                                        <asp:GridView runat = "server" ID="gridDocRecibida" GridLines="None" Width="50%" Visible="false" AutoGenerateColumns="False" ShowHeader="false" SkinID="Simple">
                                                <Columns>
                                                    <asp:BoundField DataField="Descripcion" ItemStyle-HorizontalAlign="Left"/>
                                                </Columns></asp:GridView>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"><hr /></td>
                                        </tr>
                                    </itemtemplate>
                                    <footertemplate>
                                      </table>
                                    </footertemplate>
                                  </asp:repeater>
                            <div style="padding-top:5px; padding-bottom:5px; margin-top:5px" align="center" class="TextoNegroBoldCENTER">
                            Documentación faltante de ingresar
                            </div>
                            <asp:GridView runat = "server" ID="gridDocFaltante" GridLines="None" Width="99%" Visible="false" AutoGenerateColumns="False" SkinID="Simple">
                                                <Columns>
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Documentación faltante" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"/>
                                                </Columns></asp:GridView>
                            
                            <%--FIN Datos de solicitudes--%>
                </div>
            </div>
        </div>
        <div align="center" style="margin-top: 10px">
            <asp:Button  ID="btnCerrar"   Text="Cerrar" runat="server" CssClass="Botones" Width="130px" Height="21px" OnClick="btnCerrar_Click" />&nbsp;
            <asp:Button ID="btnImprimir"  Text="Imprimir" runat="server" CssClass="Botones" Width="130px" Height="21px" OnClick= "btnImprimir_Click" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>

