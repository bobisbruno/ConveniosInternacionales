<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Busquedabeneficiario.ascx.cs" Inherits="Controles_Busquedabeneficiario" %>
<meta http-equiv="X-UA-Compatible" content="IE=8;FF=3;OtherUA=4" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table style="width: 650px" cellpadding="0" cellspacing="0" class="TituloBold">
                    <tr valign="middle">
                        <td style="padding-left:10px; text-align:left">
                        <asp:DropDownList runat="server" ID="listaBusqueda" RepeatDirection="Vertical"
                            Font-Size="10" ForeColor="Gray" Width="160px"  CssClass="CajaTexto" TabIndex="0">
                            <asp:ListItem Text="Código SIACI" Value="2"/>
                            <asp:ListItem Text="Documento" Value="1" Selected="True"/>
                            <asp:ListItem Text="Nombre o Apellido" Value="0" />
                            <asp:ListItem Text="Expediente" Value="3"/>
                            <asp:ListItem Text="Número de beneficio" Value="4"/>
                            <asp:ListItem Text="Cuil" Value="5"/>
                            <asp:ListItem Text="Trámite provisorio" Value="6"/>
                        </asp:DropDownList>
                        </td>
                        <td style="padding-left:10px; text-align:left">
                            <div id="divDocumento" onpaste="separaDatosOnPaste(event,this);return false;" style="white-space: nowrap; width: 130px; vertical-align:text-top; text-align: left; display:inherit">
                            <asp:TextBox runat="server" ID="txtDocumento" CssClass="CajaTexto" Width="70"   onkeypress="return validarNumeroControl(event)" onkeyup="this.value=this.value.toUpperCase()" MaxLength="8"  TabIndex="1"/>
                            &nbsp;&nbsp;&nbsp;<asp:DropDownList AutoPostBack="false" ID="ddlTipoDocumento"  DataTextField="Descripcion" DataValueField="CodigoDocumento" Width="160px" runat="server" CssClass="CajaTexto" TabIndex="2" />
                            <cc1:TextBoxWatermarkExtender ID="twetxtDocumento" WatermarkText="Documento" runat="server" TargetControlID="txtDocumento" WatermarkCssClass="watermarked" ></cc1:TextBoxWatermarkExtender>
                            <cc1:FilteredTextBoxExtender ID="ftetxtDocumento" runat="server" FilterType="Numbers" TargetControlID="txtDocumento" />
                            </div>
                            <div id="divNombreApell" onpaste="separaDatosOnPaste(event,this);return false;"  style="white-space: nowrap; width: 130px; vertical-align:text-top; text-align: left; display:none">
                            <asp:TextBox runat="server" ID="txtNomApe" CssClass="CajaTexto" Width="150"    onkeypress="return validarLetraAlfabetoControl(event)"  onkeyup="this.value=this.value.toUpperCase()" MaxLength="50"  TabIndex="1"/>
                            <cc1:TextBoxWatermarkExtender ID="twetxtNomApe" WatermarkText="Nombre y Apellido" runat="server" TargetControlID="txtNomApe" WatermarkCssClass="watermarked" ></cc1:TextBoxWatermarkExtender>
                            <cc1:FilteredTextBoxExtender ID="ftetxtNomApe" runat="server" FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="txtNomApe" />
                            </div>
                            <div id="divCiaci" onpaste="separaDatosOnPaste(event,this);return false;" style="white-space: nowrap; width: 130px; vertical-align:text-top; text-align: left; display:none">
                            <asp:TextBox runat="server" ID="txtCodCiaci" CssClass="CajaTexto" Width="100"   onkeyup="this.value=this.value.toUpperCase()" MaxLength="50"  TabIndex="1"/>
                            <cc1:TextBoxWatermarkExtender ID="twetxtCodCiaci" WatermarkText="Código CIACI" runat="server" TargetControlID="txtCodCiaci" WatermarkCssClass="watermarked" ></cc1:TextBoxWatermarkExtender>
                            <cc1:FilteredTextBoxExtender ID="ftetxtCodCiaci" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Numbers" TargetControlID="txtCodCiaci" />
                            </div>
                            <div id="divExpediente" onpaste="separaDatosOnPaste(event,this);return false;" style="white-space: nowrap; width: 130px; vertical-align:text-top; text-align: left; display:none">
                            <asp:TextBox ID="txtorg" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="3" Style="text-align: center; width: 30px"    TabIndex="3"></asp:TextBox>
                            -<asp:TextBox ID="txtpre" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="2" Style="text-align: center" Width="18px"    TabIndex="4"></asp:TextBox>
                            -<asp:TextBox ID="txtddoc" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="8" Style="text-align: center" Width="60px"    TabIndex="5"></asp:TextBox>
                            -<asp:TextBox ID="txtdig" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="1" Style="text-align: center" Width="5px"    TabIndex="6"></asp:TextBox>
                            -<asp:TextBox ID="txttram" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="3" Style="text-align: center" Width="30px"    TabIndex="7"></asp:TextBox>
                            -<asp:TextBox ID="txtsec" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="5" Style="text-align: center" Width="45px"    TabIndex="8"></asp:TextBox>
                            </div> 
                            <div id="divBeneficio" onpaste="separaDatosOnPaste(event,this);return false;" style="white-space: nowrap; width: 130px; vertical-align:text-top; text-align: left; display:none">
                            <asp:TextBox ID="txtBenExCaja" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="2" Style="text-align: center; width: 20px"    TabIndex="3"></asp:TextBox>
                            -<asp:TextBox ID="txtBenTipo" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="1" Style="text-align: center" Width="5px"    TabIndex="4"></asp:TextBox>
                            -<asp:TextBox ID="txtBenNumero" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="7" Style="text-align: center" Width="55px"    TabIndex="5"></asp:TextBox>
                            -<asp:TextBox ID="txtBenCopart" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="1" Style="text-align: center" Width="5px"    TabIndex="6"></asp:TextBox>
                            -<asp:TextBox ID="txtBenDigVerif" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="1" Style="text-align: center" Width="5px"    TabIndex="7"></asp:TextBox>
                            </div> 
                            <div id="divCuip" onpaste="separaDatosOnPaste(event,this);return false;" style="white-space: nowrap; width: 130px; vertical-align:text-top; text-align: left; display:none">
                            <asp:TextBox ID="txtPrecuip" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="2" Style="text-align: center; width: 18px"    TabIndex="3"></asp:TextBox>
                            -<asp:TextBox ID="txtDocCuip" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="8" Style="text-align: center" Width="60px"    TabIndex="4"></asp:TextBox>
                            -<asp:TextBox ID="txtDigCuip" runat="server" onkeypress="return validarNumeroControl(event)" onkeyup="return autoTabControl(this, event);" MaxLength="1" Style="text-align: center" Width="5px"    TabIndex="5"></asp:TextBox>
                            </div>
                            <div id="divTramite" onpaste="separaDatosOnPaste(event,this);return false;" style="white-space: nowrap; width: 130px; vertical-align:text-top; text-align: left; display:none">
                            <asp:TextBox runat="server" ID="txtNroTramite" CssClass="CajaTexto" Width="100"   onkeyup="this.value=this.value.toUpperCase()" MaxLength="8"  TabIndex="1"/>
                            <cc1:TextBoxWatermarkExtender ID="twetxtNroTramite" WatermarkText="Trámite nro." runat="server" TargetControlID="txtNroTramite" WatermarkCssClass="watermarked" ></cc1:TextBoxWatermarkExtender>
                            <cc1:FilteredTextBoxExtender ID="ftetxtNroTramite" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Numbers" TargetControlID="txtNroTramite"/>
                            </div>
                        </td>
                    </tr>
                </table>
                <script type="text/javascript" language="javascript">
                    function Seleccionar(combo) {
                        var valor = combo.options[combo.selectedIndex].value;
                        //var texto = combo.options[combo.selectedIndex].text;

                        divDocumento.style.display = eval(valor == 1) ? '' : 'none';
                        divNombreApell.style.display = eval(valor == 0) ? '' : 'none';
                        divCiaci.style.display = eval(valor == 2) ? '' : 'none';
                        divExpediente.style.display = eval(valor == 3) ? '' : 'none';
                        divBeneficio.style.display = eval(valor == 4) ? '' : 'none';
                        divCuip.style.display = eval(valor == 5) ? '' : 'none';
                        divTramite.style.display = eval(valor == 6) ? '' : 'none';
                    }
            </script>