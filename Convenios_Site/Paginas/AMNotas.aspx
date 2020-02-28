<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="AMNotas.aspx.cs" Inherits="Paginas_AMNotas" Title="Ingreso de notas"   Culture="Auto" UICulture="Auto"%>
<%@ Register Src="../Controles/Busquedabeneficiario.ascx" TagName="BBeneficiario" TagPrefix="uc2" %>
<%@ Register Src="~/Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/barraNav.ascx" TagName="BNav" TagPrefix="uc4" %>
<%@ Register Src="~/Controles/RtBoxCC.ascx" TagName="RtBox" TagPrefix="uc5" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Pragma" content="no-clase">
    <meta http-equiv="Expires" content="-1">
 </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <uc4:BNav id="BNav" runat="Server"/>
    <uc3:MError2 ID="MError" runat="server" />
    <asp:HiddenField ID="HFIdBeneficiario" runat="server" Value="" />
    <div class="FondoBlanco" align="center">
    <h1>
        <asp:Label ID="LblTituloPagina" runat="server" Text="Titulo Pagina"></asp:Label>
        </h1>
    <div class="FondoClaro" style="width:98%; padding-top:5px; margin-top:10px; padding-bottom:10px; padding-top:10px" align="center">        
        
    <table width="100%">
    <tr><td style=" width:85%; text-align:center">
    <!--Control busqueda beneficiario-->
    <uc2:BBeneficiario ID="busben" runat="server" /></td>
    <td style=" width:15%; text-align:right"><asp:Button ID="btnBuscar" runat="server" CssClass="Botones" Text="Buscar" Width="110" Height="21" onclick="btnBuscar_Click"  TabIndex="9" CausesValidation="true" AlternateText="Buscar solicitante"/></td>
    </tr></table></div>

        <!-- Nueva Nota -->
    <div id="dvAddNota" runat="server">
    <h3>
    <asp:Label ID="lbldatosBenef" runat="server"></asp:Label></h3>
    <div class="FondoClaro" style="width:98%; padding-top:5px; margin-top:10px; padding-bottom:10px; padding-top:10px" align="center">        
        <asp:UpdatePanel ID="udpNota" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table width="98%" align="center">
                                <tr>
                                    <td style="vertical-align:top; text-align:left">
                                        <font size="1">Fecha de Recepción</font></td>
                                    <td style="vertical-align:top; text-align:left">
                                        <asp:TextBox ID="txtFechaRec" runat="server" onkeyup="this.value=this.value.toUpperCase()"  Width="100px" CssClass="CajaTexto" TabIndex="1" MaxLength="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtFechaRec" Display="None" ValidationGroup="datosNota" ErrorMessage="Debe ingresar una fecha de recepción" ControlToValidate="txtFechaRec" runat="server"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vcerfvtxtFechaRec" runat="server" TargetControlID="rfvtxtFechaRec"></cc1:ValidatorCalloutExtender>
                                        <asp:RangeValidator ID="rvtxtFechaRec" runat="server" Type="Date" ControlToValidate="txtFechaRec" ErrorMessage="Fecha de no valida. Debe se no inferior a un año, hasta hoy." Display="None" ValidationGroup="datosNota"/>
                                        <cc1:ValidatorCalloutExtender ID="vcervtxtFechaRec" runat="server" TargetControlID="rvtxtFechaRec"></cc1:ValidatorCalloutExtender> 
                                        <cc1:TextBoxWatermarkExtender ID="twetxtFechaRec" runat="server" TargetControlID="txtFechaRec" WatermarkText="Fecha de recepción" WatermarkCssClass="watermarked" />
                                        <cc1:FilteredTextBoxExtender ID="ftetxtFechaRec" runat="server" FilterType="Numbers, Custom" TargetControlID="txtFechaRec" ValidChars="/" />
                                    </td>
                                    <td style="vertical-align:top; text-align:left">
                                    <font size="1">Número nota</font></td>
                                    <td style="vertical-align:top; text-align:left">
                                    <asp:TextBox ID="txtNumNota" runat="server" onkeyup="this.value=this.value.toUpperCase()"  Width="50px" CssClass="CajaTexto" TabIndex="12" MaxLength="5"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtNumNota" Display="None" ValidationGroup="datosNota" ErrorMessage="Debe ingresar un número de nota" ControlToValidate="txtNumNota" runat="server"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vcerfvtxtNumNota" runat="server" TargetControlID="rfvtxtNumNota"></cc1:ValidatorCalloutExtender>
                                    <cc1:TextBoxWatermarkExtender ID="twetxtNumNota" runat="server" TargetControlID="txtNumNota" WatermarkText="Nota nro." WatermarkCssClass="watermarked" />
                                    <cc1:FilteredTextBoxExtender ID="ftetxtNumNota" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txtNumNota"/>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td style="vertical-align:top; text-align:left">
                                    <font size="1">Asunto</font></td>
                                    <td colspan="3" style="vertical-align:top; text-align:left">
                                        <asp:TextBox ID="txtAsunto" runat="server" Width="700px" CssClass="CajaTexto" TabIndex="13" MaxLength="100"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="twetxtAsunto" runat="server" TargetControlID="txtAsunto" WatermarkText="Asunto" WatermarkCssClass="watermarked" />
                                        <cc1:FilteredTextBoxExtender ID="ftetxtAsunto" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,-,_" TargetControlID="txtAsunto"/>
                                    </td></tr>
                                    <tr>
                                        <td colspan="4" style="vertical-align:top">
                                            <uc5:RtBox ID="txtNota" runat="server" Visible="true" /></td>
                                    </tr>
                                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnBuscar"/>
            </Triggers>
        </asp:UpdatePanel>
        </div>
    </div>
    
        <!-- Notas guardadas -->
    <div id="dvShowNotasOld" runat="server">
    <h5 >Notas ingresadas</h5>
    <div class="FondoClaro" style="width:98%; padding-top:5px; margin-top:10px; padding-bottom:10px; padding-top:10px; height:150px; overflow:auto" align="center">        
    <asp:repeater id="rptNotas" runat="server"  OnItemDataBound="rptNotas_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="95%">
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
                                        <tr><td colspan="3" style=" overflow:scroll">
                                        <asp:Label runat="server" ID="lbDescripcion" Width="600px"/>
                                        </td></tr>
                                        <tr>
                                        <td colspan="3"><hr /></td>
                                        </tr>
                                    </itemtemplate>
                                    <footertemplate>
                                      </table>
                                    </footertemplate>
                                  </asp:repeater>
    </div></div>
    
        <!-- Botones -->
    <div align="right" style="margin-top: 10px;padding-right:10px; margin-bottom:10px">
        <asp:Button   ID="btnGuardar" Text="Guardar nota" runat="server" CssClass="Botones" Enabled="false" Width="150px" Height="21px"  OnClick="btnGuardar_Click" CausesValidation="true" ValidationGroup="datosNota" TabIndex="20"/>&nbsp;
        <asp:Button   ID="btnLimpiar" Text="Limpiar" runat="server" CssClass="Botones" Width="150px" Height="21px"  OnClick="btnLimpiar_Click" TabIndex="21"/>&nbsp;
        <asp:Button   ID="btnRegresar" Text="Regresar" runat="server" CssClass="Botones" Width="130px" Height="21px" onclick="btnRegresar_Click" TabIndex="22"/>
     </div>
     </div>
    <!-- Pop up solicitantes -->
     <cc1:ModalPopupExtender ID="mpeSolicitantes" runat="server" PopupDragHandleControlID="divDetalleTabla" DropShadow="true" BackgroundCssClass="modalBackground" TargetControlID="btnShowPopupSelSolicitante"
                        PopupControlID="divSelSolicitante" CancelControlID="imgCerrarDatosSelSolicitante">
                    </cc1:ModalPopupExtender>
     <asp:Button ID="btnShowPopupSelSolicitante" runat="server" Style="display: none" />
     <div style="position: relative">
                        <div style="position: fixed; top: 0px; left: 0px;">
                            <div id="divSelSolicitante" class="FondoOscuro" style="width: 800px; display:none" align="center">
                                <div class="FondoOscuro" style="float: left; padding: 5px 0px 5px 0px; text-align: left;"title="titulo">
                                    <span class="TextoBlancoBold" style="float: left; margin-left: 10px">Selección de solicitante</span>
                                    <img id="imgCerrarDatosSelSolicitante" alt="Cerrar ventana" src="../App_Themes/Imagenes/Error_chico.gif"
                                        style="cursor: hand; border: none; float: right; vertical-align: middle; margin-right: 10px; display:none" />
                                </div>
                                <asp:UpdatePanel runat="server" UpdateMode="Always" ID="udpSolicitante">
                                                    <ContentTemplate>
                                <table border="0" style="text-align: left; margin: 10px auto 0px auto;" cellpadding="1"
                                    cellspacing="2" width="96%">
                                    <tr>
                                        <td align="center">
                                            <div class="fdo_constancia" style="width: 98%; margin-top: 5px">
                                                <b class="a1"></b><b class="a2"></b><b class="a3"></b><b class="a4"></b>
                                                <div class="acontent">
                                                    <div class="fdo_constancia" style="width: 98%">
                                                        <!-- Datos -->
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
                                                                                            <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField = "Fecha_nac" HeaderText = "Fecha Nac." >
                                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField HeaderText="Ingresar nota" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                            <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="imgAddNota" runat="server" CommandName="AddNota" AlternateText="Ingresar una nota" CommandArgument='<%#Eval("Id_Beneficiario")+ ";" + Eval("apeNom")  + ";" + Eval("Documento") + ";" + Eval("ExpedienteExt")%>'   ImageUrl="~/App_Themes/Imagenes/Agregar.gif"/>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                     </Columns>
                                                                                </asp:GridView>
                                                            <asp:HiddenField ID="HFIdApoderado" runat="server" />
                                                            <asp:HiddenField ID="HFtipoTxApod" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <b class="a4"></b><b class="a3"></b><b class="a2"></b><b class="a1"></b>
                                            <div style="text-align: right; width: 98%; margin-top: 10px; margin-bottom: 5px">
                                                <asp:Button   ID="btnCancelar" Text="Cancelar" runat="server" CssClass="Botones" Width="100px" Height="20px" onclick="btnCancelarSel_Click" TabIndex="120"/></div>
                                        </td>
                                    </tr>
                                </table>
                                </ContentTemplate></asp:UpdatePanel>
                            </div>
                        </div>
         </div>
     <!--Fin Pop up solicitantes -->
    <uc1:Mensaje ID="mensaje" runat="server" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>



                                                            