<%@ Page Title="Informe de solicitud prestacional" Language="C#" AutoEventWireup="true" CodeFile="PrintInfoCompletaPeticion.aspx.cs" Inherits="InformacionCompletaPeticionPrint" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Informe de solicitud beneficiario - Impresión</title>
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
                                        <asp:Label ID="lbEncabezadoBeneficiario" Font-Size="18px"  runat="server" ></asp:Label>
                                        </h5>
                                        <h5 style="width:100%; text-align:left" class="titulo">
                                        <asp:Label ID="lbdescPrestacion" Font-Size="15px"  runat="server" ></asp:Label></h5>
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
                            <%--Datos de Solicitudes--%>
                            <br />
                            <div style="padding-top:5px; padding-bottom:5px" align="center" class="titulo">
                            Estado y Sector actual
                            </div>
                            <div style="padding-top:5px; padding-bottom:5px" align="center">
                            <table width="80%" class="TextoNegroBold"><tr><td><font size="1">Fecha última novedad:</font>&nbsp;<asp:Label ID="lbFechaUltMov" runat="server"></asp:Label></td>
                            <td><font size="1">Observación:</font>&nbsp;<asp:Label ID="lbUltMovObserv" runat="server"></asp:Label></td></tr>
                            <tr><td><font size="1">Estado:</font>&nbsp;<asp:Label ID="lbUltMovEstado" runat="server"></asp:Label></td>
                            <td><font size="1">Sector:</font>&nbsp;<asp:Label ID="lbUltMovSector" runat="server"></asp:Label></td></tr></table></div>
                            <br />
                            <div style="padding-top:5px; padding-bottom:5px" align="center" class="titulo">
                            Peticiones de solicitud ingresadas
                            </div>
                            <asp:repeater id="rptSolicitudes" runat="server" OnItemDataBound="rptSolicitudes_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="98%">
                                    </headertemplate>
                                    <itemtemplate>
                                        <tr>
                                        <td style=" width:50%" align="left"><font size="1">País convenio:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblPais"/></font>
                                        </td>
                                        <td align="left"><font size="1">Prestación solicitada:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbPrestacion"/>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2" align="left"></td>
                                        </tr><td align="left">
                                        <font size="1">Fecha de ingreso:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblFechaSolicitud"/></font></td>
                                        <tr>
                                        <td colspan="2" align="left"><font size="1">Observación:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbObservacion"/></font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2" align="left"><font size="1">Ref. exterior:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbRefExterior"/></font>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td align="left"><font size="1">Ubic. Doc
                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lbUbicdoc"/>
                                        </font></td>
                                        <td align="left"><font size="1"><small style=" color:Red">
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
                                <br />
                                <div style="padding-top:5px; padding-bottom:5px" align="center" class="titulo">
                            Beneficios
                            </div>
                            <asp:GridView runat = "server" ID="gridListadoBeneficio" PageSize="15" GridLines="None" Width="99%" Visible="false" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="numBeneficio" HeaderText="Beneficio" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="fAltabeneficio" HeaderText="Fecha Alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="DTipoTrDerivado" HeaderText="Tipo"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="observacion" HeaderText="Observaciones"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"/>
                                                </Columns></asp:GridView>
                                                <br />
                            <div style="padding-top:5px; padding-bottom:5px" align="center" class="titulo">
                            Expedientes
                            </div>
                            <asp:GridView runat = "server" ID="gridListadoexpedienteBen" PageSize="15" GridLines="None" Width="99%" Visible="false" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="numExpte" HeaderText="Expediente" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="fAltaexpediente" HeaderText="Fecha Alta"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="Caratula" HeaderText="Caratula"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="Observacion" Visible="false" HeaderText="Comentarios"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                </Columns></asp:GridView>
                                                <br />
                            <div style="padding-top:5px; padding-bottom:5px" align="center" class="titulo">
                            Movimientos
                            </div>
                            <asp:GridView runat = "server" ID="gridMovimientosSol" PageSize="15" GridLines="None" Width="99%" Visible="false" AutoGenerateColumns="False" >
                                                <Columns>
                                                    <asp:BoundField DataField="Fecha_Movimiento" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="TipoMovimiento" HeaderText="Acción" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="TipoIngreso" HeaderText="Ingreso" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="Destino" HeaderText="Destino" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="Sector" HeaderText="Sector" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"/>
                                                </Columns></asp:GridView>
                            <br />
                            <div style="padding-top:5px; padding-bottom:5px" align="center" class="titulo">
                            Documentación recepcionada
                            <hr />
                            </div>
                                <asp:repeater id="rptIngresos" runat="server" OnItemDataBound="rptIngresos_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="100%">
                                    </headertemplate>
                                    <itemtemplate>
                                        <tr>
                                        <td style=" width:50%" align="left"><font size="1">Fecha de ingreso:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbIngFIng"/></font>
                                        </td>
                                        <td align="left"><font size="1">Vía ingreso:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbIngVia"/>
                                        </font></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2" align="left">
                                        <font size="1">Observaciónes:&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbComentarioIng"/></font>
                                        </td>
                                        </tr><td align="left">
                                        <font size="1"><strong>Documentación recibida:</strong></font></td>
                                        <tr>
                                        <td colspan="2" align="left"><font size="1">
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
                                <br />
                            <div style="padding-top:5px; padding-bottom:5px" align="center" class="titulo">
                            Documentación faltante de ingresar
                            </div>
                            <asp:GridView runat = "server" ID="gridDocFaltante" PageSize="15" GridLines="None" Width="99%" Visible="false" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Documentación faltante" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"/>
                                                </Columns></asp:GridView>
                            
                            <%--FIN Datos de solicitudes--%>
                        </center>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
