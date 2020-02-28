<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="comun_controles_Menu" %>

 <script type="text/javascript">
        arrowimages.down[1] = '<%= ResolveUrl("~/App_Themes/PortalAnses/Imagenes/arrow-down.gif") %>'; //"../App_Themes/PortalAnses/Imagenes/arrow-down.gif";
        arrowimages.right[1] = '<%= ResolveUrl("~/App_Themes/PortalAnses/Imagenes/arrow-right.gif") %>'; //"../App_Themes/PortalAnses/Imagenes/arrow-right.gif";
    </script>
<div align="left" id="menuDinamico" runat="server"></div>