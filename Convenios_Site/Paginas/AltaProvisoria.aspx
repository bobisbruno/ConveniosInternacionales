<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="AltaProvisoria.aspx.cs" Inherits="AltaProvisoria" Title="Ingreso provisorio de documentación."   Culture="Auto" UICulture="Auto"%>
<%@ Register Src="~/Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/MValidacion.ascx" TagName="MensajeCons" TagPrefix="uc" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/barraNav.ascx" TagName="BNav" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


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
        <asp:Label ID="LblTituloPagina" runat="server" Text=""></asp:Label>
        </h1>
    <div class="FondoClaro" style="text-align:center; width:98%; padding-top:5px;padding-bottom:5px">
            <div style="text-align:left; width:95%; padding-top:5px; margin-bottom:5px; padding-left:30px">
                <h2>Datos del solicitante</h2>
                <table>
                <tr>
                <td style=" width:30%; text-align:left; vertical-align:top; color:gray">Apellido y nombre</td>  
                <td style="padding-left:5px"><asp:TextBox ID="txtApeNomB" runat="server"   Width="200px" CssClass="CajaTexto" MaxLength="100" TabIndex="1"/>&nbsp;<small style=" color:Red">(*)</small>
                <asp:RequiredFieldValidator ID="rfvtxtApeNomB" runat="server" ControlToValidate="txtApeNomB" Display="None" ErrorMessage="Debe ingresar nombre y apellido" ValidationGroup="vgdatos"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="vcerfvtxtApeNomB" runat="server" TargetControlID="rfvtxtApeNomB"></cc1:ValidatorCalloutExtender>                                                                 
                </td>
                </tr>
                    <tr><td style="text-align:left; vertical-align:top; color:gray">Documento</td>
                <td style="padding-left:5px">
                    <asp:TextBox ID="txtDocB" runat="server" MaxLength="8" onkeyup="this.value=this.value.toUpperCase()" Width="80px" CssClass="CajaTexto" TabIndex="2"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revtxtDocB" runat="server" ControlToValidate="txtDocB" Display="None" ValidationExpression="^[0-9]{8,8}?$" ErrorMessage="Solo ingresar 8 dígitos enteros" ValidationGroup="vgdatos"></asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vcerevtxtDocB" runat="server" TargetControlID="revtxtDocB"></cc1:ValidatorCalloutExtender>
                    <asp:TextBox ID="txtValiDoc" runat="server" BackColor="Transparent" Enabled="false" BorderColor="Transparent" Text="" Width="0px" Height="0px"></asp:TextBox>
                    <asp:CustomValidator ID="cvtxtDocB" ControlToValidate="txtDocB" runat="server" OnServerValidate="documentoExiste" Display="None" ErrorMessage = "Ya existe trámite ingresado con anterioridad para este número de documento.</br>Ingresar por <strong>TRÁMITES CON CUIL</STRONG> para actualizar la documentación." ValidationGroup="vgdatos"  SetFocusOnError="true"  ValidateEmptyText="true" ></asp:CustomValidator>
                    <cc1:ValidatorCalloutExtender id="vcecvtxtDocB" runat="server" TargetControlID="cvtxtDocB" Width="400px"></cc1:ValidatorCalloutExtender>
                    &nbsp;<asp:DropDownList ID="ddltDocB" runat="server"  DataTextField="Descripcion" DataValueField="CodigoDocumento" Width="300px" TabIndex="3">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                <td style="text-align:left; vertical-align:top; color:gray">Fecha
                </td>
                <td style="padding-left:5px">
                    <asp:TextBox ID="txtFechaIngSolicitud" runat="server"   width="100px" CssClass="CajaTexto" TabIndex="4" MaxLength="12" ></asp:TextBox>&nbsp;<small style=" color:Red">(DD/MM/AAAA)</small>&nbsp;<small style=" color:Red">(*)</small>
                        <asp:RequiredFieldValidator ID="rfvtxtFechaIngSolicitud" ControlToValidate="txtFechaIngSolicitud" ErrorMessage="Ingresar fecha de trámite" Display="None" runat="server" ValidationGroup="vgdatos"/>
                        <cc1:ValidatorCalloutExtender ID="vcerfvtxtFechaIngSolicitud" runat="server" TargetControlID="rfvtxtFechaIngSolicitud"></cc1:ValidatorCalloutExtender>
                        <asp:RangeValidator ID="rvtxtFechaIngSolicitud" runat="server" Type="Date" ControlToValidate="txtFechaIngSolicitud" ErrorMessage="Fecha no válida. (ingresar desde un mes a la fecha actual)" Display="None" ValidationGroup="vgdatos"/>
                        <cc1:ValidatorCalloutExtender ID="vcervtxtFechaIngSolicitud" runat="server" TargetControlID="rvtxtFechaIngSolicitud"></cc1:ValidatorCalloutExtender>
                </td>
                </tr>
                <tr>
                <td style="text-align:left; vertical-align:top; color:gray">Datos de referencia
                </td>
                <td style="padding-left:5px; text-align:left">
                    <asp:TextBox ID="txtDatosReferencia" runat="server"  TextMode="MultiLine" Height="25px" Width="750px" CssClass="CajaTexto" TabIndex="5" MaxLength="500" ></asp:TextBox>&nbsp;<small style=" color:Red">(*)</small>
                    <asp:RequiredFieldValidator ID="rfvtxtDatosReferencia" runat="server" ControlToValidate="txtDatosReferencia" Display="None" ErrorMessage="Se requiere el ingreso de datos de referencia" ValidationGroup="vgdatos"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcerfvtxtDatosReferencia" runat="server" TargetControlID="rfvtxtDatosReferencia"></cc1:ValidatorCalloutExtender>                                                                 
                </td>
                </tr>
                    <tr>
                <td style="text-align:left; vertical-align:top; color:gray">Se deriva a sector
                </td>
                <td style="padding-left:5px">
                    <asp:DropDownList ID="ddlSector" runat="server" TabIndex="6" Width="300px" ></asp:DropDownList>
                </td></tr>
                    <tr>
                <td style="text-align:left; vertical-align:top; color:gray">País
                </td>
                <td style="padding-left:5px">
                    <asp:DropDownList ID="ddlPaisConvenio" runat="server" TabIndex="7" Width="300px" DataTextField="Descripcion" DataValueField="Pais_PK"></asp:DropDownList><%--&nbsp;<small style=" color:Red">(*)</small>--%>
                </td></tr>
                    <tr>
                        <td style="text-align:left; vertical-align:top; color:gray">Tramita para</td>
                <td style=" padding-left:5px"><asp:DropDownList ID="ddlPrestacionesS" runat="server" AutoPostBack="True" TabIndex="8" DataTextField="Descripcion"  DataValueField="Cod_Prestacion" Width="300px" OnSelectedIndexChanged="ddlPrestacionesS_SelectedIndexChanged"></asp:DropDownList>&nbsp;<small style=" color:Red">(*)</small>
                    <asp:RequiredFieldValidator id="rfvddlPrestacionesS" InitialValue="0" ControlToValidate="ddlPrestacionesS" ErrorMessage="Seleccione prestación." Display="None" runat="server" ValidationGroup="vgdatos"/>
                    <cc1:ValidatorCalloutExtender ID="vcerfvddlPrestacionesS" runat="server" TargetControlID="rfvddlPrestacionesS"></cc1:ValidatorCalloutExtender>
                </td>
                </tr>
                </table>
                </div>
                <div id="dvMovimientos" runat="server" style="text-align:left; width:95%; padding-top:5px; margin-bottom:5px; margin-top:20px; padding-left:30px">
                <table width="98%">
                    <tr>
                        <td style="text-align:left; vertical-align:top; color:gray">
                            Movimiento&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlTipoMovimiento" runat="server" TabIndex="9" Width="300px">
                            <asp:ListItem Value="0" Text="[Seleccionar tipo de Movimiento]" Selected="True"> </asp:ListItem>
                            <asp:ListItem Value="1" Text="Ingreso de documentacion"> </asp:ListItem>
                            <asp:ListItem Value="2" Text="Devolucion de documentacion"> </asp:ListItem>
                        </asp:DropDownList>&nbsp;<small style=" color:Red">(*)</small>
                    <asp:RequiredFieldValidator id="rfvddlTipoMovimiento" InitialValue="0" ControlToValidate="ddlTipoMovimiento" ErrorMessage="Seleccionar tipo de movimiento." Display="None" runat="server" ValidationGroup="vgdatos"/>
                    <cc1:ValidatorCalloutExtender ID="vcerfvddlTipoMovimiento" runat="server" TargetControlID="rfvddlTipoMovimiento"></cc1:ValidatorCalloutExtender>
                        </td></tr>
                    <tr>
                        <td style="text-align:left; vertical-align:top; color:gray; margin-top:15px">
                            <hr style="color:gray" />
                            </td></tr>
                    <tr>
                        <td style="text-align:left; vertical-align:top; color:gray; margin-top:5px">
                            1.Selección de documentación&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlTipoDocumentacion" runat="server" TabIndex="10" Width="300px" DataValueField="CodTipoDocumentacion" DataTextField="Descripcion">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator id="rfvddlTipoDocumentacion" InitialValue="0" ControlToValidate="ddlTipoDocumentacion" ErrorMessage="Seleccionar documentación de ingreso o egreso." Display="None" runat="server" ValidationGroup="valAddDocumento"/>
                            <cc1:ValidatorCalloutExtender ID="vcerfvddlTipoDocumentacion" runat="server" TargetControlID="rfvddlTipoDocumentacion"></cc1:ValidatorCalloutExtender>&nbsp;<small style=" color:Red">(*)</small>
                            </td></tr>
                    <tr>
                        <td style="text-align:left; vertical-align:top; color:gray">
                            2.Observaciónes&nbsp;&nbsp;
                            <asp:TextBox ID="txtDatosDocumentacion" runat="server"  TextMode="SingleLine" Width="350px" CssClass="CajaTexto" MaxLength="20" TabIndex="11"></asp:TextBox>
                            </td></tr>
                    <tr>
                        <td style="text-align:left; vertical-align:top; color:gray">
                            3.Archivo/s&nbsp;&nbsp;
                        <asp:FileUpload ID="IpFile" runat="server"  AllowMultiple="true"  TabIndex="12"/>
                            <b style=" color:gray;font-size:smaller">Solo son válidos los archivos PDF - GIF - JPEG - BMP - TIFF - PNG</b>
                                    </td></tr>
                    <tr>
                        <td style="text-align:left; vertical-align:top; color:gray">
		                    <asp:Button ID="btnAdd"  runat="server" Text="Agregar selección" Width="220px" OnClick="btnAdd_Click" CssClass="Botones" Height="18px" TabIndex="13" CausesValidation ="true" ValidationGroup="valAddDocumento" />

                            </td></tr>
                    </table>
                    <table width="98%">
                    <tr>
                        <td style="text-align:center; vertical-align:top; margin-top:10px">
                            <div style=" overflow:auto; max-height:150px; width:98%; text-align:center">
                                <asp:TextBox ID="txtValidaDxocumentacion" runat="server" BackColor="Transparent" Enabled="false" BorderColor="Transparent" Text="" Width="0px" Height="0px"></asp:TextBox>
                                        <asp:CustomValidator ID="cvValidaDocumentacion" ControlToValidate="txtValidaDxocumentacion" runat="server" OnServerValidate="validaDocumentacion" Display="None" ErrorMessage = "De debe registrar documentación de ingreso para el trámite." ValidationGroup="vgdatos"  SetFocusOnError="true"  ValidateEmptyText="true" ></asp:CustomValidator>
                                        <cc1:ValidatorCalloutExtender id="vcecvValidaDocumentacion" runat="server" TargetControlID="cvValidaDocumentacion" Width="400px"></cc1:ValidatorCalloutExtender>
                            <asp:Gridview ID="dgArchivos" runat="server" AutoGenerateColumns="False" EmptyDataText="No se registra documentación para este trámite."  EmptyDataRowStyle-CssClass="GrillaAternateItem"
                            Width="98%" Height="50px" Visible="true" CssClass="GrillaAternateItem" HorizontalAlign="Center" OnRowCommand="RowCommand" TabIndex="14"  >
                                                <Columns>
                                                    <asp:BoundField  HeaderText="Documentación" DataField="TipoDocumentacion" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField HeaderText="Archivo-s" DataField="ArchivosDigitalizados" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField HeaderText="Tamaño" DataField="Tamano" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField HeaderText="Descripción" DataField="Comentario" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="Quitar documentación" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnQuitar" runat="server"  Height="15px" Width="15px" CommandName="QuitarDoc"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" AlternateText="Quitar documentacion de la lista" ImageUrl="~/App_Themes/Imagenes/Borrar.gif" TabIndex="15"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                    </asp:Gridview>   
                                </div>
                    <div style="margin-top:5px; margin-bottom:5px">
                        <asp:Button runat="server" id="btnClear" style="width: 80px" CssClass="Botones" Text="Quitar todos" Height="18px" OnClick="btnClear_Click" TabIndex="16"  />
                        </div>
                            </td></tr></table>
	            </div>  
                
                </div>
    </div>
    </div>
    <!-- Botones -->
    <div style="margin-top:5px; width:95%">
    <table width="100%">
    <tr>
    <td align="center">
    <asp:Button   ID="btnGuardar" Text="Guardar" runat="server" CssClass="Botones" Width="100px" Height="21px" onclick="btnGuardar_Click" TabIndex="17" CausesValidation="true" ValidationGroup="vgdatos"/>&nbsp;
    <asp:Button  ID="btnAnular" Text="Anular" runat="server" CssClass="Botones" Width="100px" Height="21px" onclick="btnAnular_Click" TabIndex="18" />&nbsp;
    <asp:Button   ID="btnRegresar" Text="Regresar" runat="server" CssClass="Botones" Width="100px" Height="21px" onclick="btnRegresar_Click" TabIndex="19"/>&nbsp;
    </td>
    </tr>
    </table>
    </div>
    <!-- FIN Botones-->
    <uc1:Mensaje ID="mensaje" runat="server" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>