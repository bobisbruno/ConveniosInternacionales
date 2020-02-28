<%@ Page Title="Listado de solicitudes" Language="C#" AutoEventWireup="true" CodeFile="Listadosolicitudes.aspx.cs"
    Inherits="Listadosolicitudes" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listado de solicitudes</title>
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
                            Listado de solicitudes ingresadas entre&nbsp&nbsp<asp:Label ID="lblfechas" runat="server"></asp:Label>
                            </div>
                            <div style=" width:100%; margin-top:20px; margin-bottom:10px; text-align:center">
                                 <asp:GridView runat = "server" ID="gridListadoSolicitudes" GridLines="None"
                                Width="100%" Visible="true" AutoGenerateColumns="False" ShowHeader="true" Height="5px" CssClass="saltopagina">
                                <HeaderStyle CssClass="grillahead" />
                                <RowStyle CssClass="grillaitem" />
                                <Columns>
                                    <asp:BoundField DataField="ApeNomCompleto" HeaderText = "Apellido y nombre" >
                                    <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField = "DescripcionPrestacion" HeaderText = "Prestación" >
                                        <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField = "PaisDescCompleto" HeaderText = "Pais convenio" >
                                        <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField = "fAMSolicitud" HeaderText = "Fecha solicitud">
                                        <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField = "Mercosur" HeaderText = "Mercosur">
                                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                 </Columns>
                                 </asp:GridView>
                         </div>
                        </center>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
