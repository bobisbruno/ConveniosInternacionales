<%@ Page Title="Totales por sectores" Language="C#" AutoEventWireup="true" CodeFile="PrintConIndicadoresXSector.aspx.cs"
    Inherits="PrintConIndicadoresXSector" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Totales por sector</title>
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
                            <asp:Label ID="lblCaption" runat="server"></asp:Label>
                            </div>
                            <div style=" width:100%; margin-top:20px; margin-bottom:10px; text-align:center">
                                <asp:GridView runat = "server" ID="gridListado" GridLines="None" Width="98%" Visible="true" AutoGenerateColumns="False" Height="5px">
                                <HeaderStyle CssClass="grillahead" />
                                <RowStyle CssClass="grillaitem" />
                                <Columns>
                                    <asp:BoundField DataField="Descripcion"  HeaderText = "Sector" >
                                        <ItemStyle HorizontalAlign="Left" Width="50%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField = "Total"  HeaderText = "Total" >
                                        <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField = "Porcentual"  HeaderText = "%" >
                                        <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                                    </asp:BoundField>
                                 </Columns>
                                 </asp:GridView>
                                <div style=" width:100%; text-align:center; margin-bottom:10px">
                            <table width="98%" style=" height:20px; font-family:Arial, tahoma, helvetica; font-size: 8pt;font-weight:bold;text-align:center" class="" >
                            <tr>
                                <td style=" width:50%" align="left">Totales</td>
                                <td style=" width:25%"><asp:Label ID="lblTotal" runat="server"></asp:Label></td>
                                <td style=" width:25%">100 %</td>
                            </tr>
                            </table>
                            </div>
                                <div style=" width:90%; text-align:center; margin-bottom:10px; margin-top:10px">
                                <asp:Literal ID="litGrafico" runat="server"></asp:Literal>
                                    </div>
                             </div>
                             <div  id="dvEvolutivo" runat="server" style=" width:100%; margin-top:30px; margin-bottom:10px; text-align:center">
                                 <hr />
                                 <font size="2"><strong>
                                 <asp:Label ID="lbCaption2" runat="server"></asp:Label></strong></font>
                                 <br />
                                 <br />
                                 <asp:GridView runat = "server" ID="gridEvolutivo" GridLines="None" Width="99%" Visible="true" AutoGenerateColumns="False" >
                                <Columns>
                                </Columns>
                                </asp:GridView>
                                <br />
                                <asp:Literal ID="litGraficoLinea" runat="server"></asp:Literal>
                             </div>
                        </center>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
