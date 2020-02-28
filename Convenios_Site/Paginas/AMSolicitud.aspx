<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNOMnu.master" AutoEventWireup="true" CodeFile="AMSolicitud.aspx.cs" Inherits="AMSolicitud" Title="Alta y modificacion de solicitud"   Culture="Auto" UICulture="Auto"%>
<%@ Register Src="~/Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/MValidacion.ascx" TagName="MensajeCons" TagPrefix="uc" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPageNOMnu.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Pragma" content="no-clase">
    <meta http-equiv="Expires" content="-1">
 </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <uc3:MError2 ID="MError" runat="server" />
    <div class="TituloBold" style="text-align: left; width:98%; padding-top:5px; padding-bottom:5px">
            <asp:Label ID="lbEncabezadoBeneficiario" runat="server"></asp:Label></div>
    <div class="FondoClaro" style="text-align:center; width:98%; padding-top:5px; padding-bottom:5px">
        <div id="dvSelNewPrestacionPais" runat="server" style="text-align: left; width:98%; padding-top:5px; margin-bottom:5px">
            <div class="TituloBold" style="text-align: center; width:98%">
                            Seleccionar prestación y convenio / estado a ingresar.
                            </div>
            <table width="98%">
                <tr><td style=" width:15%" ><font size="1"><strong>Prestación:&nbsp;</strong></font></td>
                <td style=" width:35%" ><asp:DropDownList ID="ddlPrestacionesS" runat="server" AutoPostBack="True" TabIndex="1" DataTextField="Descripcion"  DataValueField="Cod_Prestacion" Width="200px" OnSelectedIndexChanged="ddlPrestacionesS_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td style=" width:15%" ><font size="1"><strong>Convenio país:</strong></font></td>
                <td style=" width:35%" ><asp:DropDownList ID="ddlPaisS" runat="server" AutoPostBack="True" TabIndex="2" DataTextField="Descripcion"  DataValueField="Pais_PK" Width="150px" OnSelectedIndexChanged="ddlPaisS_SelectedIndexChanged">
                </asp:DropDownList></td>
                </tr>
                </table>
            </div>
        <asp:Panel id="pnlAllDato" runat="server" Width="98%">
            <div class="TituloBold" style="text-align: center; width:98%; padding-top:5px; margin-bottom:5px; float:left">
                Solicitudes de convenio&nbsp;&nbsp;<asp:Label ID="lbdescPrestacion" runat="server"></asp:Label>
                &nbsp;&nbsp;<asp:Label ID="lbDescPaisS" runat="server"></asp:Label>
            </div>
            <asp:GridView runat = "server" ID="gridListadoSolicitudes" PageSize="15" GridLines="None" Width="98%" Visible="false" AutoGenerateColumns="False" UseAccessibleHeader="true" AllowSorting="false" OnRowCommand="RowCommand">
                <Columns>
                    <asp:BoundField DataField="IdBeneficiario" Visible="false"/>
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha ingreso"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
                    <asp:BoundField DataField="Pais" HeaderText="Pais convenio"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                    <asp:BoundField DataField="codPrestacion" Visible="false"/>
                    <asp:BoundField DataField="Prestacion" HeaderText="Prestación"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                    <asp:BoundField DataField="Ubicacion_Fisica" HeaderText="Ubic. Doc."  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
                    <asp:BoundField DataField="Referencia_exterior" HeaderText="Ref. exterior"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="23%"/>
                    <asp:BoundField DataField="Denegada" HeaderText="Denegada / Motivo"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%"/>
                    <asp:TemplateField HeaderText="Borrar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnBorrar" runat="server" CommandName="BorrarS" CommandArgument='<%#Eval("FechaIngreso")%>'  ImageUrl="~/App_Themes/Imagenes/Borrar.gif"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                </Columns>
                </asp:GridView>
            <div class="FondoBlanco"style=" width:98%; margin-top:10px; margin-bottom:5px" >
            <div style="text-align:center; padding-top:5px; margin-bottom:5px; padding-left:10px; width:98%">
            <table width="98%">
            <tr><td colspan="4"><font size="1"><strong><asp:CheckBox ID="chkMercosurS" runat="server" AutoPostBack="false" Checked="false" TabIndex="3" Text="Mercosur" TextAlign="Left"/></strong></font></td></tr>
            <tr><td><font size="1"><strong>Ubicación física</strong></font></td>
            <td colspan="3">                    
                <asp:TextBox ID="txtUbicFisicaS" runat="server" onkeyup="this.value=this.value.toUpperCase()"  Width="200px" CssClass="CajaTexto" TabIndex="4" MaxLength="50" ></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td>                    
            <font size="1"><strong>Fecha de ingreso petición</strong></font>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtFechaIngSolicitud" runat="server"   width="100px" CssClass="CajaTexto" TabIndex="5" MaxLength="10" ></asp:TextBox>
                &nbsp;<small style=" color:Red">(DD/MM/AAAA)</small>
            </td>
            </tr>
            <tr>
            <td>                    
            <font size="1"><strong>Referencia exterior</strong></font>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRefExteriorS" runat="server"  TextMode="MultiLine" Height="25px" Width="98%" CssClass="CajaTexto" TabIndex="6" MaxLength="500" ></asp:TextBox>
            </td>
            </tr>
            <tr><td><font size="1"><strong>Denegar solicitud</strong></font></td></tr>
            <tr><td>
            <font size="1"><strong>Motivo</strong></font></td>
            <td colspan="3"><asp:DropDownList ID="ddlMotivoDeniega" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMotivoDeniega_SelectedIndexChanged"  DataTextField="Descripcion"  DataValueField="codMotivo" Width="200px" Enabled="True" TabIndex="7"></asp:DropDownList><font size="1"><strong> (seleccionar para denegar)</strong></font></td></tr>
            <tr><td>
            <font size="1"><strong>Observaciónes</strong></font></td>
            <td colspan="3">
            <asp:TextBox ID="txtObservacionS" runat="server" TextMode="MultiLine" Height="25px" Width="98%" CssClass="CajaTexto" TabIndex="8" MaxLength="500"></asp:TextBox></td></tr>
            </table>
            <asp:Button   ID="btnGuardarSolicitud" Text="Ingresar" runat="server" CssClass="Botones" Width="120px" Height="20px" onclick="btnGuardarSolicitud_Click" TabIndex="9" CausesValidation="true"/>
            </div>
            <asp:HiddenField ID="HFtipoTxSolicitud" runat="server" />
            </div>
            <div class="TituloBold" style="text-align: center; width:98%; padding-top:5px; margin-bottom:5px; float:left">
                            Movimientos pendientes
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
            <div class="FondoBlanco"style=" width:98%; margin-top:10px; margin-bottom:5px" >
            <div style="width:98%; text-align:left; padding-left:10px; padding-top:5px; padding-bottom:5px">
            <font size="1"><strong>Seleccione el tipo de accion a realizar</strong></font>&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlTipoMovimiento" runat="server" TabIndex="10" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoMovSelIndexCh">
                            <asp:ListItem Selected="True" Value="0" Text="Seleccione"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Recepción de documentación"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Generar devolución"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Derivar a Sector / Cambio de estado"></asp:ListItem>
                            </asp:DropDownList>
                            </div>
            <div id="dvDatosMovimientos"  runat="server" style="text-align:center; width:95%">
            <div id="dvIngreso" style=" margin-top:10px; margin-left:10px" runat="server">
            <table cellpadding="1" cellspacing="1" width="98%" style=">
            <tr><td style=" width:25%"><font size="1"><strong>Tipo de Ingreso:</strong></font></td>
            <td><asp:DropDownList ID="ddlTipoIngreso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoIngreso_SelectedIndexChanged" TabIndex="11" Width="150px"></asp:DropDownList></td>
            </tr>
            <tr>
            <td style=" width:25%"><font size="1"><strong>Observaciónes:</strong></font></td>
            <td><asp:TextBox ID="txtObservacionIngreso" runat="server" TextMode="MultiLine" Height="25px" Width="98%" CssClass="CajaTexto" TabIndex="12" MaxLength="500"></asp:TextBox>
            </td>
            </tr>
            <tr><td><font size="1"><strong>Documentacion ingresada:</strong></font></td>
            </tr>
            </table></div>
            <div id="dvDevolucion" style=" margin-top:10px; padding-left:10px; margin-left:10px" runat="server">
            <table cellpadding="1" cellspacing="1" width="98%">
            <tr><td style=" width:30%"><font size="1"><strong>Destino:</strong></font></td>
            <td><asp:TextBox ID="txtDestino" MaxLength="100"  runat="server"   Width="150px" CssClass="CajaTexto" TabIndex="11"/></td>
            </tr>
            <tr><td><font size="1"><strong>Certificado:</strong></font></td>
            <td><asp:TextBox ID="txtCertificado" MaxLength="50"  runat="server"   Width="150px" CssClass="CajaTexto" TabIndex="12"/></td>
            </tr>
            <tr style=" height:5px">
            <td><font size="1"><strong>Observaciones:</strong></font></td>
            <td><asp:TextBox ID="txtObservacionesD" MaxLength="500"  TextMode="MultiLine" Height="40px" runat="server" Width="95%" CssClass="CajaTexto" TabIndex="13"/>
            </td>
            </tr>
            <tr><td><font size="1"><strong>Documentacion solicitada:</strong></font></td>
            </tr>
            </table>
            </div>
            <asp:GridView runat = "server" ID="gridDocPrestacion" PageSize="50" GridLines="None" Width="50%" Visible="true" AutoGenerateColumns="False" AllowSorting="false" DataKeyNames="CodTipoDocumentacion, Descripcion" Height="5px" TabIndex="14">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Selección" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelDoc" runat="server" Checked="false" TabIndex="15" />
                                                            </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CodTipoDocumentacion"  Visible="false"/>
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Documentación faltante"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"/>
                                                </Columns>
                                                </asp:GridView>
            <div id="dvMovimiento" style=" margin-top:10px; padding-left:10px; margin-left:10px; padding-bottom:10px" runat="server">
            <!--Datos Movimiento-->
            <asp:HiddenField ID="HFcodEstadoAct" runat="server" Value="" />
            <asp:HiddenField ID="HFcodSectorAct" runat="server" Value="" />
            <table cellpadding="1" cellspacing="1" width="98%">
            <tr><td><font size="1"><strong>Estado actual&nbsp;<strong><asp:Label ID="lbEstadoActual" runat="server" Text=""></asp:Label></strong></strong></font></td>
            <td><font size="1"><strong>Sector actual&nbsp;<strong><asp:Label ID="lbSectorActual" runat="server" Text=""/></strong></strong></font></td>
            </tr>
            <tr>
            <td colspan="2" valign="top" style="padding-top:5px"><hr /></td></tr>
            <tr>
            <td style="padding-top:5px"><font size="1"><strong>Estado:</strong></font>&nbsp;&nbsp;<asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="true" TabIndex="11" Width="200px" OnSelectedIndexChanged="ddlEstadoSelectedIndexCh"></asp:DropDownList>
            </td>
            <td style="padding-top:5px"><font size="1"><strong>Sector:</strong></font>&nbsp;&nbsp;<asp:DropDownList ID="ddlSector" runat="server" AutoPostBack="true" TabIndex="12" Width="200px" OnSelectedIndexChanged="ddlSectorSelectedIndexCh"></asp:DropDownList>            
            </td>
            </tr>
            <tr>
            <td colspan="2" valign="top" style="padding-top:5px">
            <font size="1"><strong>Observaciónes:</strong></font></td></tr>
            <tr>
            <td colspan="2"><asp:TextBox ID="txtObservacionesM" MaxLength="500" TextMode="MultiLine" Height="40px" runat="server" Width="98%" CssClass="CajaTexto" TabIndex="13"/>
            </td>
            </tr>
            </table>
            <!--FIN Datos Movimiento-->
            </div>
            <div  style="text-align: center; width:98%; padding-top:5px; margin-bottom:5px; padding-left:10px; padding-right:10px">
            <asp:Button    ID="btnGurdar" Text="Ingresar" runat="server" CssClass="Botones" Width="120px" Height="21px"  TabIndex="19" OnClick="btnGuardarMov_Click"/>
            </div>
            </div>
            </div>
            <asp:HiddenField ID="HFtipoTxBeneficio" runat="server" />
            <asp:HiddenField ID="HFFaltaBeneficio" runat="server" />
            
            <asp:HiddenField ID="HFtipoTxExpe" runat="server" />
            <asp:HiddenField ID="HFFaltaExpe" runat="server" />
            <div style="text-align:left; padding-top:5px; margin-bottom:5px; padding-left:10px">
            <asp:Button   ID="btntraeExptes" Text="Seleccionar expedientes" runat="server" CssClass="Botones"  CommandName="expediente" Width="145px" Height="20px"  onclick="btnSelExpediente_Click" TabIndex="21"/>
            </div>
            <asp:GridView runat = "server" ID="gridListadoexpedienteBen" PageSize="15" GridLines="None" Width="98%" Visible="false" AutoGenerateColumns="False"  OnRowCommand="RowCommand" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Mofificar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnModificar" runat="server" CommandName="ModificarE" CommandArgument='<%#Eval("fAltaexpediente")%>'  ImageUrl="~/App_Themes/Imagenes/Edicion.gif" TabIndex="22" ></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="numExpte" HeaderText="Expediente" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%"/>
                                                <asp:BoundField DataField="fAltaexpediente" HeaderText="Fecha Alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
                                                <asp:BoundField DataField="Caratula" HeaderText="Caratula"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="32%"/>
                                                <asp:BoundField DataField="observaciones" HeaderText="Observaciónes"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="31%"/>
                                            </Columns></asp:GridView>
            <asp:GridView runat = "server" ID="gridListadoBeneficio" PageSize="15" GridLines="None" Width="98%" Visible="false" AutoGenerateColumns="False"  OnRowCommand="RowCommand" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Mofificar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnModificar" runat="server" CommandName="ModificarBen" CommandArgument='<%#Eval("fAltabeneficio")%>'  ImageUrl="~/App_Themes/Imagenes/Edicion.gif"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="numBeneficio" HeaderText="Beneficio" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                                                <asp:BoundField DataField="fAltabeneficio" HeaderText="Fecha Alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
                                                <asp:BoundField DataField="DTipoTrDerivado" HeaderText="Tipo"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
                                                <asp:BoundField DataField="observacion" HeaderText="Observaciones"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="55%"/>
                                            </Columns></asp:GridView>
        </asp:Panel>
        </div>
        
    <!-- Botones -->
    <div style="text-align: center; width:98%; padding-top:5px; margin-bottom:5px; padding-left:10px; padding-right:10px">
    <asp:Button   ID="btnGuardar" Text="Guardar cambios" runat="server" CssClass="Botones" Width="110px" Height="21px" 
    onclick="btnGuardar_Click" TabIndex="23" CausesValidation="true" ValidationGroup="datosBeneficiario"/>&nbsp;
    <asp:Button   ID="btnCerrar" Text="Cerrar" runat="server" CssClass="Botones" Width="120px" Height="21px" onclick="btnCerrar_Click" TabIndex="24"/>&nbsp;
    <asp:Button   ID="btnImprimir" Text="Imprimir" runat="server" CssClass="Botones" Width="120px" Height="21px"  TabIndex="25" OnClick= "btnImprimir_Click"  />
    </div>
    <!-- end Botones -->
     <!-- Pop up Expediente -->
     <cc1:ModalPopupExtender ID="mpeIngExpediente" runat="server" PopupDragHandleControlID="divDetalleTabla" DropShadow="true" BackgroundCssClass="modalBackground" TargetControlID="btnShowPopupSelExpediente"
                        PopupControlID="divSelexpediente" CancelControlID="imgCerrarDatosSelExpediente">
                    </cc1:ModalPopupExtender>
     <asp:Button ID="btnShowPopupSelExpediente" runat="server" Style="display: none" />
     <div style="position: relative">
                        <div style="position: fixed; top: 0px; left: 0px;">
                            <div id="divSelexpediente" class="FondoOscuro" style="width: 1000px; display:none" align="center">
                                <div class="FondoOscuro" style="float: left; padding: 5px 0px 5px 0px; text-align: left;"title="titulo">
                                    <span class="TextoBlancoBold" style="float: left; margin-left: 10px">Selección de expediente/s</span>
                                    <img id="imgCerrarDatosSelExpediente" alt="Cerrar ventana" src="../App_Themes/Imagenes/Error_chico.gif"
                                        style="cursor: hand; border: none; float: right; vertical-align: middle; margin-right: 10px; display:none" />
                                </div>
                                <table border="0" style="text-align: left; margin: 10px auto 0px auto;" cellpadding="1"
                                    cellspacing="2" width="99%">
                                    <tr>
                                        <td align="center">
                                            <div class="fdo_constancia" style="width: 99%; margin-top: 5px">
                                                <b class="a1"></b><b class="a2"></b><b class="a3"></b><b class="a4"></b>
                                                <div class="acontent">
                                                    <div class="fdo_constancia" style="width: 99%; height:100px; overflow:auto">
                                                        <asp:GridView runat = "server" ID="gvexpedtesANME" PageSize="15" GridLines="None" Width="99%" AutoGenerateColumns="False"
                                                        DataKeyNames="numExpte,fAltaexpediente,nBeneficio,caratula,estado,estadoSentencia,fechaAltaAFJP,fechaProceso,fechaVencimiento,codigoOficinaAlta,ultimaOficina" TabIndex="1">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Selección" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="GrillaHead"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <HeaderTemplate>
                                                                        Todo&nbsp;
                                                                        <asp:CheckBox ID="chbSeleccionTodo" runat="server" ToolTip="Seleccionar" AutoPostBack="true" OnCheckedChanged="chbSeleccionTodo_CheckedChanged" Checked="true" TabIndex="500" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                          <asp:CheckBox ID="chbSeleccion" runat="server" ToolTip="Seleccionar" Checked="true" TabIndex="501" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="numExpte" HeaderText="Expediente" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25%"/>
                                                                <asp:BoundField DataField="fAltaexpediente" HeaderText="Fecha Alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="7%"/>
                                                                <asp:BoundField DataField="nBeneficio" HeaderText="Beneficio"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
                                                                <asp:BoundField DataField="caratula" HeaderText="Caratula"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="28%"/>
                                                                <asp:BoundField DataField="estado" HeaderText="Estado"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="6%"/>
                                                                <asp:BoundField DataField="estadoSentencia" Visible="false" HeaderText="Estado Sent"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%"/>
                                                                <asp:BoundField DataField="fechaAltaAFJP"  Visible="false" HeaderText="Alta AFJP"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%"/>
                                                                <asp:BoundField DataField="fechaProceso"  Visible="false" HeaderText="F. proc."  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%"/>
                                                                <asp:BoundField DataField="fechaVencimiento"  Visible="false" HeaderText="F. Venc"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%"/>
                                                                <asp:BoundField DataField="codigoOficinaAlta" HeaderText="Of. Alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="6%"/>
                                                                <asp:BoundField DataField="ultimaOficina" HeaderText="Ult. of"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="6%"/>
                                                            </Columns></asp:GridView>
                                                            <br />
                                                            <asp:TextBox ID="txtObservacion" TextMode="MultiLine" Height="30px" MaxLength="150"  runat="server" Width="95%" CssClass="CajaTexto" TabIndex="502"/>
                                                        </div>
                                                    </div>
                                                </div>
                                                <b class="a4"></b><b class="a3"></b><b class="a2"></b><b class="a1"></b>
                                            <div style="text-align: right; width: 99%; margin-top: 10px; margin-bottom: 5px">
                                                <asp:Button   ID="btn_agregarPopexpe" runat="server" Text="Ingresar" Width="80px"  OnClick="btn_AgregarPopExpe_Click" TabIndex="503" CausesValidation="true"/>
                                                <asp:Button   ID="btn_cerrarPopexpe" runat="server" Text="Anular" Width="80px"  OnClick="btn_cerrarPopExpe_Click" TabIndex="504"/></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
         </div>
     <!--Fin Pop up expediente -->
     <uc1:Mensaje ID="mensaje" runat="server" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>