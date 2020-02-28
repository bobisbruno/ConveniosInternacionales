<%@ Page Title="Comprobante de trámite" Language="C#" AutoEventWireup="true" CodeFile="ComprobanteTramite.aspx.cs" Inherits="ComprobanteTramite" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Comprobante Trámite</title>
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
                                                <h3>Convenios Internacionales</h3>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="bottom">
                                                <h7><%=DateTime.Now.Date.ToLongDateString().ToUpper()%></h7>
                                                </td>
                                        </tr>
                                    </table>
                                    <h5 style="width:100%; text-align:center" class="titulo">
                                        <asp:Label ID="lbTramiteNro" Font-Size="25px"  runat="server" ></asp:Label>
                                        </h5>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td valign="top" >
                            <br />
                            <div align="center" class="titulo">
                            <asp:Label ID="lbTramite" runat="server" Font-Size="20px"></asp:Label>
                            </div>
                        <div style="padding-top:5px; padding-bottom:5px" align="left">
                        <table width="90%" class="TextoNegroBold" style=" text-align:left">
                            <tr><td>
                            <font size="1">Nombre y apellido del solicitante:</font>&nbsp;<b style="text-align:left; font-size:xx-small; font-style:normal"><asp:Label ID="lbSolicitanteNomApe" runat="server"></asp:Label></b></td>
                            </tr>
                            <tr><td>
                            <font size="1">Documento:</font>&nbsp;<b style="text-align:left; font-size:xx-small; font-style:normal"><asp:Label ID="lbSolicitanteDoc" runat="server"></asp:Label></b></td>
                            </tr>
                            </table>
                            </div>
                            <div style="padding-top:5px; padding-bottom:5px" align="left">
                            <table width="90%" class="TextoNegroBold" style=" text-align:left">
                            <tr><td>
                            <font size="1">Fecha de ingreso / recepción de trámite: </font>&nbsp;<b style="text-align:left; font-size:xx-small; font-style:normal"><asp:Label ID="lbFechaIR" runat="server"></asp:Label></b></td>
                            </tr>
                                <tr><td>
                            <font size="1">Observaciones: </font>&nbsp;<asp:Label ID="lbDatosReferencia" runat="server"></asp:Label></td>
                            </tr>
                                <tr><td>
                            <b style="text-align:left; font-size:xx-small; font-style:normal"><asp:Label ID="lbTipodeTramite" runat="server"></asp:Label></b></td>
                            </tr>
                                <tr><td>
                            <font size="1">Sector:&nbsp;<b style="text-align:left; font-size:xx-small; font-style:normal"><asp:Label ID="lbSectorDeriva" runat="server"></asp:Label></b></font></td>
                            </tr>
                            <tr>
                                <td>
                            <b style="text-align:left; font-size:xx-small; font-style:normal"><asp:Label ID="lbCantidadDoc" runat="server" Width="99%"></asp:Label></b></td></tr>
                            </table></div>
                        <asp:repeater id="rptDocumentacionIR" runat="server" OnItemDataBound="rptDocumemntacionIR_ItemDataBound">
                                    <headertemplate>
                                        <table cellpadding="1" cellspacing="2" width="100%" class="grillabody">
                                    </headertemplate>
                                    <itemtemplate>
                                        <tr>
                                        <td style="width:98%; text-align:left"><asp:Label runat="server" ID="lblDocumentacion" Font-Size="8pt"/></td>
                                        </tr>
                                    </itemtemplate>
                                    <footertemplate>
                                      </table>
                                      </footertemplate>
                                </asp:repeater>
                    </td>
                </tr>
                <tr><td>
                            <asp:Label ID="lbError" runat="server" Text ="" Width="99%"></asp:Label></td></tr>
                <tr><td>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <b style="text-align:left; font-size:xx-small; font-style:italic">Conservar este comprobante.</b>
                    </td></tr>
                <tr><td>
                    <hr />
                    </td></tr>
                <tr><td>
                    <b style="text-align:left; font-size:x-small; font-style:normal; color:darkred">ATENCIÓN: El presente documento tiene una validez de 60 días a partir de la fecha informada. Pasado el plazo, se deberá reingresar la documentación para el inicio de trámite por convenio formalmente.</b>
                    </td></tr>
            </tbody>
        </table>
    </form>
</body>
</html>
