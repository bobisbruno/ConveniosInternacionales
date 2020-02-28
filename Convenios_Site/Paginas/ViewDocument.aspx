<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDocument.aspx.cs" Inherits="Paginas_VerDocumento" %>
<meta http-equiv="Content-Security-Policy" content="plugin-types application/pdf; child-src 'none'">


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Documento</title>
</head>
<body>
   
    <form id="form1" enctype="multipart/form-data"  runat="server">
        <div id="dvError" runat="server" style="text-align:center">
            <h2>El archivo no está disponible</h2>
        </div>
        
    </form>

    
</body>
    
</html>
