<!--
<div id="msgPageRequestManagerHandler" style="display: none; text-align: center;
    height: 50px; width: 280px;">
    
    <table style="vertical-align: baseline; height: 100%; width: 100%; 
           border: solid 1px black" class="FondoClaro">
        <tr>
            <td style="width: 15%; text-align: center">
                <img src="<% = ResolveClientUrl("~/App_Themes/Imagenes/procesando.gif") %>" alt="#" style="vertical-align: baseline; height:15px;" />
            </td>
            <td style="text-align: left">
                
                <asp:Label id="lblMsg" runat="server" style="font-weight: bold; font-size: 8pt">
							Aguarde<br /> se esta procesando su solicitud...
				</asp:Label>
            </td>
        </tr>
    </table>
</div>
-->

<div id="msgPageRequestManagerHandler" class="FondoClaro">
    Aguarde<br /> se esta procesando su solicitud...
</div>

<script language="javascript" type="text/javascript" src="<% = ResolveClientUrl("~/App_Themes/AsyncPostBackHandler.js") %>"></script>

