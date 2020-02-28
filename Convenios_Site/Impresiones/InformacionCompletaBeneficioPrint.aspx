<%@ Page Title="Informe del beneficiario" Language="C#" AutoEventWireup="true" CodeFile="InformacionCompletaBeneficioPrint.aspx.cs"
    Inherits="InformacionCompletaBeneficioPrint" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Informe del beneficiario - Impresión</title>
    <link href="../App_Themes/estilos/AnsesPrint.css" media="all" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 700px; page-break-after: avoid">
            <thead style="display: table-header-group;">
                <tr>
                    <td>
                        <table id="CabeceraCont" class="Cabecera" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td align="left">
                                    <table width="100%" style="border-bottom: #000000 2px solid; padding-bottom:5px; font-size:10px; font-weight:bold">
                                        <tr>
                                            <td width="100" rowspan="2">
                                                <img src="../App_Themes/Imagenes/encabezadoImpresionANSES.JPG" alt="Logo ANSES" 
                                                    style="height: 42px; width: 168px;" /></td>
                                            <td align="right" valign="middle">
                                                <h3>Gestión de Convenios Internacionales</h3>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="bottom">
                                                <h6><%=DateTime.Now.Date.ToLongDateString().ToUpper()%></h6>
                                                </td>
                                        </tr>
                                    </table>
                                    <h5 style="width:100%; text-align:left" class="titulo">
                                        <asp:Label ID="lblNomApeBeneficiario" Font-Size="20px"  runat="server" ></asp:Label>    </h5>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td valign="top" >
                        <center>
                            <hr />
                            <div class="titulo" style=" margin-top:15px; margin-bottom:5px">
                            <font size="4">Datos del beneficiario</font>
                            </div>
                            <table cellpadding="2" cellspacing="3" width="98%"  class="grillabody" style="text-align:left"">
                                    <tr>
                                        <td>Código SIACI:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblCodSIACI" Font-Size="8pt"/></td>
                                        <td>Fecha de Nacimiento:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbFechaNacimiento" Font-Size="8pt"/></td>
                                    </tr>
                                    <tr>
                                        <td>Apellido materno:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbApellidoM" Font-Size="8pt"/></td>
                                        <td>Sexo:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblSexoBen" Font-Size="8pt"/></td>
                                    </tr>
                                    <tr>
                                        <td>Nacionalidad:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="LbPais" Font-Size="8pt"/></td>
                                        <td>CUIL/T&nbsp;<asp:Label runat="server" ID="lblCuipB"  /></td></tr>
                                    <tr>
                                        <td colspan="2">Dirección:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbDirCalleBen"/>
                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lbDirNumBen"/>
                                        &nbsp;&nbsp;Piso / depto:&nbsp;<asp:Label runat="server" ID="lbDirPisoBen"/>&nbsp;<asp:Label runat="server" ID="lbDirDeptoBen"/>
                                        </td></tr>
                                    <tr>
                                        <td colspan="2">Entre calles:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbEcalleB1"/>&nbsp;y&nbsp;
                                        <asp:Label runat="server" ID="lbEcalleB2"/>
                                        </td></tr>
                                    <tr>
                                    <td colspan="2">
                                        Localidad (Prov.)&nbsp;<asp:Label runat="server" ID="lbProvLocalidadBen"/></td></tr>
                                    <tr>
                                        <td colspan="2"><font size="1">Dirección extranjera:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbDirExtranjera"/>
                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lbCiudadExtranjera"/></font></td>
                                    </tr>
                                    <tr>
                                    <td valign="top" colspan="3">
                                    <table width="100">
                                    <tr><td align="left">Documentos</td></tr>
                                    <tr><td align="left">
                                        <asp:GridView  runat = "server" ID="gridListadoDocBeneficiarios" PageSize="15" GridLines="None" Visible="true" AutoGenerateColumns="False" ShowHeader="false" Width="200px">
                                        <Columns>
                                                    <asp:BoundField DataField="AbrevDocumento" HeaderText="Tipo"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15px"/>
                                                    <asp:BoundField DataField="numDoc" HeaderText="Número" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                                                    <asp:BoundField DataField="codAbrevPais" HeaderText="Pais origen"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="30px"/>
                                                    <asp:BoundField DataField="fechaAlta" HeaderText="F. alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="30px"/>
                                                </Columns>
                                        </asp:GridView></td></tr>
                                    </table>
                                    </td>
                                    </tr>
                                </table>
                            <hr />
                            <div id="dvConCausante" runat="server">
                            <div class="titulo" style=" padding-top:30px; margin-bottom:5px">
                            <font size="4">Datos del causante</font>
                            </div>
                            <div  style="padding-top:5px;text-align:center" align="center">
                            <table cellpadding="2" cellspacing="3" width="98%" class="grillabody" style=" text-align:left">
                                    <tr>
                                        <td>Fecha defunción:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblFechaDefuncionC" Font-Size="8pt"/></td>
                                        <td>Apellido y Nombre:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblApellidoC" Font-Size="8pt"/>
                                        &nbsp;<asp:Label runat="server" ID="lblNombreC" Font-Size="8pt"/></td>
                                        <td>Sexo:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblSexoCaus" Font-Size="8pt"/>
                                        </td>
                                    </tr>
                                    <tr><td>Fecha nacimiento:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblFechaNacC"  />
                                    </td><td>CUIL/T&nbsp;<asp:Label runat="server" ID="lblCuipC"  />
                                    </td></tr>
                                    <tr>
                                    <td valign="top" colspan="3">
                                    <table width="100">
                                    <tr><td align="left">Documentos</td></tr>
                                    <tr><td align="left">
                                    <asp:GridView runat = "server" ID="gridListadoDocCausantes" GridLines="None" Visible="true" AutoGenerateColumns="False" ShowHeader="false" Width="200px">
                                                <Columns>
                                                    <asp:BoundField DataField="AbrevDocumento" HeaderText="Tipo"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15px"/>
                                                    <asp:BoundField DataField="numDoc" HeaderText="Número" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                                                    <asp:BoundField DataField="codAbrevPais" HeaderText="Pais origen"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="30px"/>
                                                    <asp:BoundField DataField="fechaAlta" HeaderText="F. alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="30px"/>
                                                </Columns></asp:GridView></td></tr>
                                    </table>
                                    </td></tr>
                                </table>
                                </div>
                                </div>
                            <div class="titulo" style=" padding-top:50px; margin-bottom:5px">
                            <font size="4">Solicitudes prestacionales de Convenio</font>
                            </div>
                            <div id="dvConSolicitud" runat="server" style=" margin-top:5px; margin-bottom:5px; width:98%; text-align:left">
                                <asp:repeater id="rptSolicitudes" runat="server" OnItemDataBound="rptSolicitudes_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="100%" class="grillabody">
                                    </headertemplate>
                                    <itemtemplate>
                                        <tr>
                                        <td style="width:40%">Pais - prestación solicitada:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblPaisPrest" Font-Size="8pt"/></td>
                                        </tr>
                                        <tr>
                                        <td>Fecha movim - estado - sector:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblfmovestSect" Font-Size="8pt"/></td>
                                        </tr>
                                        <tr>
                                        <td>Mercosur:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblMercosur" Font-Size="8pt"/></td></tr>
                                        <tr>
                                        <td><hr /></td>
                                        </tr>
                                    </itemtemplate>
                                    <footertemplate>
                                      </table>
                                      </footertemplate>
                                </asp:repeater>
                            </div>
                            <div id="dvSinSolicitud" runat="server" style=" margin-top:5px; margin-bottom:5px; width:98%; text-align:center">
                                No registra solicitudes ingresadas.
                            </div>
                            <hr />
                            <div class="titulo" style=" padding-top:50px; margin-bottom:5px; padding-left:10px; page-break-before:always">
                            <font size="4">Apoderados</font>
                            </div>
                            <div id="dvConApoderados"  runat="server" style=" margin-top:5px; margin-bottom:5px; width:98%; text-align:left; page-break-inside:auto">
                                <asp:repeater id="rptApoderados" runat="server" OnItemDataBound="rptApoderados_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="100%" class="grillabody">
                                    </headertemplate>
                                    <itemtemplate>
                                        <tr>
                                        <td colspan="2">Apoderado:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblSTipoApod"/>
                                        &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblTipoApoderadoAp" Font-Size="8pt"/>&nbsp;(&nbsp;<asp:Label runat="server" ID="lbTipoPoder" Font-Size="8pt"/>&nbsp;)</td>
                                        </tr>
                                        <tr>
                                        <td>Apellido y Nombre:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblApeNomAp" Font-Size="8pt"/></td>
                                        <td>Teléfono:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblTelefonoAp" Font-Size="8pt"/></td>
                                        </tr>
                                        <tr>
                                        <tr>
                                        <td colspan="2">Fecha alta:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbFaltaAp"/>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td colspan="2">Dirección:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbDirCalleAp"/>
                                        &nbsp;<asp:Label runat="server" ID="lbDirNumAp"/>
                                        &nbsp;<asp:Label runat="server" ID="lbDirPisoAp"/>
                                        &nbsp;<asp:Label runat="server" ID="lbDirDeptoAp"/>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td colspan="2">E / calles:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbDirEC1Ap"/>
                                        &nbsp;y&nbsp;<asp:Label runat="server" ID="lbDirEC2Ap"/></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2">
                                        Localidad (Pcia)&nbsp;&nbsp;<asp:Label runat="server" ID="lbProvLocalidadAp"/>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td>Banco:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblBancoAp" Font-Size="8pt"/></td>
                                        <td>E Mail:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblEmailAp" Font-Size="8pt"/></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2">Observaciones:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblObservaciones" Font-Size="8pt" Width="90%"/></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2">Documento&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbcodAbrevTdocA" Font-Size="8pt"/>&nbsp;-&nbsp;<asp:Label runat="server" ID="lbDocA" Font-Size="8pt"/>
                                        </td>
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
                        </center>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
