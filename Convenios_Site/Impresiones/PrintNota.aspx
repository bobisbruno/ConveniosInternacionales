<%@ Page Title="Impresión de nota" Language="C#" AutoEventWireup="true" CodeFile="PrintNota.aspx.cs" Inherits="InformacionNota" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Nota - Impresión</title>
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
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td valign="top" >
                            <hr />
                            <%--Datos de la Nota--%>
                            <br />
                            <div style="padding-top:5px; padding-bottom:5px" align="left" class="titulo">
                            Asunto:&nbsp;<asp:Label ID="lbAsunto" runat="server"></asp:Label>
                            </div>
                            <div style="padding-top:5px; padding-bottom:5px" align="left">
                            <table width="80%" class="TextoNegroBold" style=" text-align:left">
                            <tr><td>
                            <font size="1">Fecha de recepción:</font>&nbsp;<asp:Label ID="lbFechaRec" runat="server"></asp:Label></td>
                            <td align="right"><font size="1">Nota nro.:</font><asp:Label ID="lbNumNota" runat="server"></asp:Label></td></tr>
                            <tr><td colspan="2">
                            <hr /></td></tr>
                            <tr><td colspan="2">
                            <asp:Label ID="lbDescripcion" runat="server" Width="99%"></asp:Label></td></tr>
                            </table></div>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
