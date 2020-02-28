<%@ Page Title="Listado de trámites provisorios" Language="C#" AutoEventWireup="true" CodeFile="ListadoSolicitudesProvisorias.aspx.cs"
    Inherits="ListadosolicitudesProvisorias" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listado de trámites provisorios</title>
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
                            <div class="titulo" style=" margin-top:15px; margin-bottom:5px">
                            <asp:Label ID="lblencabezado" runat="server"></asp:Label>
                            </div>
                            <div style=" width:100%; margin-top:20px; margin-bottom:10px; text-align:center">
                             <asp:GridView runat = "server" ID="gridListadoSolicitudes" GridLines="None"
                                Width="100%" Visible="true" AutoGenerateColumns="False" ShowHeader="false" Height="5px" >
                            <%--    <HeaderStyle CssClass="grillahead" />
                                <RowStyle CssClass="grillaitem" />--%>
                                <Columns>
                                    <asp:BoundField DataField="ApellidoyNombre" HeaderText = "Apellido y nombre" >
                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "DocumentoyTipo" HeaderText = "Documento" >
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Desc_Prestacion" HeaderText = "Trámite" >
                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "Nro_SolicitudProvisoria" HeaderText = "Nro. trámite">
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundField>
                                <asp:BoundField DataField = "FAltaProvisoria" HeaderText = "Fecha" >
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundField>
                                <asp:BoundField DataField = "Referencia_Provisoria"  HeaderText = "Observaciónes" >
                                <ItemStyle HorizontalAlign="Left" Width="40%"></ItemStyle>
                            </asp:BoundField>
                                <asp:BoundField DataField = "Sectorderiva" HeaderText="Sector derivado">
                                <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField = "IngresaDevuelve" HeaderText="Ingresa / Devuelve">
                                <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
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
