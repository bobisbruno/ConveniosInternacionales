<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BotonesGraficos.ascx.cs" Inherits="Comun_Controles_BotonesGraficos" %>
    <div style=" text-align:center; width:300px; height:45px; background-color:transparent">
        <asp:ImageButton ID="btnTorta" AlternateText="Torta" 
            ImageUrl="~/App_Themes/ImagenesBotones/pie-chart-149726_1280.png" Height="42px" Width="42px" 
            runat="server" onclick="btnTorta_Click" TabIndex="100" />
        <asp:ImageButton ID="btnLinea" AlternateText="Linea" 
            ImageUrl="~/App_Themes/ImagenesBotones/stock-149421_1280.png" Height="42px" Width="42px" 
            runat="server" onclick="btnLinea_Click"  TabIndex="101" />
        <asp:ImageButton ID="btnCol" AlternateText="Columna" 
            ImageUrl="~/App_Themes/ImagenesBotones/bar-charts-152544_1280.png" Height="42px" Width="42px" 
            runat="server" onclick="btnColumna_Click"  TabIndex="102" />
        <asp:ImageButton ID="btnFunnel" AlternateText="Funnel" 
            ImageUrl="~/App_Themes/ImagenesBotones/Funnel.jpg"  Height="42px" Width="42px" 
            runat="server" onclick="btnFunnel_Click"  TabIndex="103" />
        <asp:ImageButton ID="btnBarra" AlternateText="Barra" 
            ImageUrl="~/App_Themes/ImagenesBotones/bar-charts-152544_1280H.png" Height="42px" Width="42px" 
            runat="server" onclick="btnBarra_Click"  TabIndex="104" />
        <asp:ImageButton ID="btnSegmento" AlternateText="Circular" 
            ImageUrl="~/App_Themes/ImagenesBotones/pie-chart-154411_1280.png" Height="42px" Width="42px" 
            runat="server" onclick="btnSecmento_Click"  TabIndex="105" />
    </div>
    