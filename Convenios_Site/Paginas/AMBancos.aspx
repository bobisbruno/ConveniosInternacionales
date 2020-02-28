<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="AMBancos.aspx.cs" Inherits="Paginas_AMBancos" Title="Administrar Bancos"   Culture="Auto" UICulture="Auto"%>
<%@ Register Src="~/Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<%@ Register Src="../Controles/MensajeError2.ascx" TagName="MError2" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/barraNav.ascx" TagName="BNav" TagPrefix="uc4" %>
<%@ MasterType VirtualPath="~/MasterPage/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Pragma" content="no-clase">
    <meta http-equiv="Expires" content="-1">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
  <uc4:BNav id="BNav" runat="Server"/>
  <div class="FondoBlanco">
    <div style="margin: 10px">
      <h1>
        <asp:Label ID="LblTituloPagina" runat="server" Text="Titulo Pagina"></asp:Label>
      </h1>
      <div class="panelgrilla">
        <asp:Panel ID="pnlGrilla" runat="server" CssClass="FondoClaro">
          <div class="grillaAbm">
              <div style="overflow:auto; width:99%; max-height:500px">
            <asp:GridView ID="gv_Grilla" CssClass="grillaAdmin_MP" runat="server" CellPadding="4"
              ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gv_Grilla_RowCancelingEdit" OnRowDataBound="gv_Grilla_RowDataBound"
              OnRowEditing="gv_Grilla_RowEditing" OnRowUpdating="gv_Grilla_RowUpdating" AutoGenerateColumns="False"
              ShowFooter="True" OnRowCommand="gv_Grilla_RowCommand"
              AllowSorting="True" OnSorted="gv_Grilla_Sorted" OnSorting="gv_Grilla_Sorting"
              OnPageIndexChanging="gv_Grilla_PageIndexChanging" OnRowCreated="gv_Grilla_RowCreated" Width="98%">
              <HeaderStyle></HeaderStyle>
              <AlternatingRowStyle BackColor="#E5E5E5" ForeColor="#000" />
              <Columns>
                <asp:TemplateField HeaderText="Descripción banco" SortExpression="Banco">
                  <ItemTemplate>
                    <asp:Label ID="Banco" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox ID="Descripcion" runat="server" ValidationGroup="UpdateVal"
                                            Width="280px" Text='<%# Bind("Descripcion") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvDescripcion" ValidationGroup="UpdateVal"
                                            ControlToValidate="Descripcion" SetFocusOnError="True" Display="Dynamic"
                                            ErrorMessage="<b>*</b>" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderD" runat="server"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars="." TargetControlID="Descripcion" />
                  </EditItemTemplate>
                  <FooterTemplate>
                    <asp:TextBox ID="Descripcion" runat="server" ValidationGroup="InsertVal"
                                            Width="280px"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvDescripcion" ValidationGroup="InsertVal"
                                            ControlToValidate="Descripcion" SetFocusOnError="True" Display="Dynamic"
                                            ErrorMessage="<b>*</b>" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderD" runat="server"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars="." TargetControlID="Descripcion" />
                  </FooterTemplate>
                  <FooterStyle HorizontalAlign="Center" Width="30%" />
                  <HeaderStyle HorizontalAlign="Center" Width="30%" />
                  <ItemStyle HorizontalAlign="Center" Width="30%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sitio web" SortExpression="Web">
                  <ItemTemplate>
                    <asp:Label ID="SitioWeb" runat="server" Text='<%# Bind("WebSite") %>'/>
                    <%--<asp:HyperLink ID="SitioWeb" runat="server" Text='<%# Bind("WebSite") %>' Target="_blank" NavigateUrl='<%# Bind("WebSite") %>'></asp:HyperLink>--%>
                    <%--<asp:HiddenField ID="hfws" runat="server" Value='<%# Bind("WebSite") %>'/>--%>
                    <%--<asp:ImageButton ID="WebSite" runat="server" ImageUrl="~/App_Themes/Imagenes/seleccion.gif"></asp:ImageButton>--%>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox ID="WebSite" runat="server" ValidationGroup="UpdateVal"
                                            Width="280px" Text='<%# Bind("WebSite") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvWebSite" ValidationGroup="UpdateVal"
                                            ControlToValidate="WebSite" SetFocusOnError="True" Display="Dynamic"
                                            ErrorMessage="<b>*</b>" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderW" runat="server"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars="." TargetControlID="WebSite" />
                  </EditItemTemplate>
                  <FooterTemplate>
                    <asp:TextBox ID="WebSite" runat="server" ValidationGroup="InsertVal"
                                            Width="280px"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvWebSite" ValidationGroup="InsertVal"
                                            ControlToValidate="WebSite" SetFocusOnError="True" Display="Dynamic"
                                            ErrorMessage="<b>*</b>" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderW" runat="server"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars="." TargetControlID="WebSite" />
                  </FooterTemplate>
                  <FooterStyle HorizontalAlign="Center" Width="40%" />
                  <HeaderStyle HorizontalAlign="Center" Width="40%" />
                  <ItemStyle HorizontalAlign="Center" Width="40%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Frecuente" SortExpression="Frecuente">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="Frecuente" runat="server" Width="50px" SelectedValue='<%# Bind("FrecuenteStr") %>'>
                                            <asp:ListItem Value="S">Si</asp:ListItem>
                                            <asp:ListItem Value="N">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Frecuente" runat="server" Width="50px" Text='<%# Eval("FrecuenteStr").ToString() == "S" ? "Si" : "No" %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="Frecuente" runat="server" Width="50px">
                                            <asp:ListItem Value="S">Si</asp:ListItem>
                                            <asp:ListItem Value="N">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Center" Width="10%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acciones" ShowHeader="False">
                  <EditItemTemplate>
                    <asp:ImageButton ID="IMGBUpdate" runat="server" CommandName="Update" ValidationGroup="UpdateVal"
                      ImageUrl="~/App_Themes/Imagenes/ok.png" Text="Actualizar" ToolTip="Actualizar" />
                    &nbsp;<asp:ImageButton ID="IMGBCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                      ImageUrl="~/App_Themes/Imagenes/Cancelar.gif" Text="Cancelar" ToolTip="Cancelar" />
                  </EditItemTemplate>
                  <ItemTemplate>
                    <asp:ImageButton ID="IMGBEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Imagenes/Edicion.gif"
                      Text="Editar" ToolTip="Editar" />
                    <%--&nbsp;<asp:ImageButton ID="IMGBDelete" runat="server" CausesValidation="False" CommandName="Delete"
                      ImageUrl="~/App_Themes/Imagenes/Borrar.gif" Text="Eliminar" ToolTip="Eliminar" />--%>
                  </ItemTemplate>
                  <FooterTemplate>
                    <asp:ImageButton ID="IMGBInsert" runat="server" CommandName="Insert" ValidationGroup="InsertVal"
                      ImageUrl="~/App_Themes/Imagenes/Agregar.gif" Text="Insertar" ToolTip="Insertar" />
                  </FooterTemplate>
                  <FooterStyle HorizontalAlign="Center" Width="10%" />
                  <HeaderStyle HorizontalAlign="Center" Width="10%" />
                  <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:TemplateField>
              </Columns>
              <EditRowStyle BackColor="#BBBBBB" />
              <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
              <HeaderStyle BackColor="#397bc4" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#EFF4FA" ForeColor="#333333" />
              <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000" />
            </asp:GridView>
                  </div>
          </div>
        </asp:Panel>

      </div>
        <div align="center" style="margin-top: 5px;margin-bottom:5px">
                <asp:Button   ID="btnRegresar" Text="Regresar" runat="server" CssClass="Botones" Width="130px" Height="21px" onclick="btnRegresar_Click"/>
     </div>
    </div>
    <!-- margin: 10px -->
  </div>
  <!-- FondoBlanco -->
  <uc1:Mensaje ID="mensaje" runat="server" />
  <uc1:Mensaje ID="mensajeOk" runat="server" />
</asp:Content>