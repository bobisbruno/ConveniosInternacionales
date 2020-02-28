<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="AMDocumentacionXPrestacion.aspx.cs" Inherits="Paginas_TiposDocumentacionXPrestacion" Title="Administrar documentación - prestaciónes"   Culture="Auto" UICulture="Auto"%>
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
            <asp:GridView ID="gv_Grilla" CssClass="grillaAdmin_MP" runat="server" CellPadding="4"
              ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gv_Grilla_RowCancelingEdit"
              OnRowEditing="gv_Grilla_RowEditing" OnRowUpdating="gv_Grilla_RowUpdating" AutoGenerateColumns="False"
              ShowFooter="True" OnRowCommand="gv_Grilla_RowCommand" OnRowDeleting="gv_Grilla_RowDeleting"
              AllowSorting="True" OnSorted="gv_Grilla_Sorted" OnSorting="gv_Grilla_Sorting" OnDataBound="gv_Grilla_DataBound"
              OnPageIndexChanging="gv_Grilla_PageIndexChanging" OnRowCreated="gv_Grilla_RowCreated" Width="98%">
              <HeaderStyle></HeaderStyle>
              <AlternatingRowStyle BackColor="#E5E5E5" ForeColor="#000" />
              <Columns>
                <asp:TemplateField HeaderText="Prestación" SortExpression="Prestacion">
                  <ItemTemplate>
                    <asp:Label ID="Prestacion" runat="server" Text='<%# Bind("DescripcionPrestacion") %>'></asp:Label>
                  </ItemTemplate>
                  <FooterTemplate>
                    <asp:DropDownList ID="Prestacion" runat="server">
                    </asp:DropDownList>
                  </FooterTemplate>
                  <FooterStyle HorizontalAlign="Center" Width="20%" />
                  <HeaderStyle HorizontalAlign="Center" Width="20%" />
                  <ItemStyle HorizontalAlign="Center" Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo documentación" SortExpression="DescripcionTD">
                  <ItemTemplate>
                    <asp:Label ID="Descripcion" runat="server" Text='<%# Bind("DescripcionTdoc") %>'></asp:Label>
                  </ItemTemplate>
                  <FooterTemplate>
                    <asp:DropDownList ID="Descripcion" runat="server">
                    </asp:DropDownList>
                  </FooterTemplate>
                  <FooterStyle HorizontalAlign="Center" Width="25%" />
                  <HeaderStyle HorizontalAlign="Center" Width="25%" />
                  <ItemStyle HorizontalAlign="Center" Width="25%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comentario" SortExpression="Comentario">
                  <ItemTemplate>
                    <asp:Label ID="Comentario" runat="server" Text='<%# Bind("Comentario") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox ID="Comentario" runat="server" ValidationGroup="UpdateVal"
                                            Width="280px" Text='<%# Bind("Comentario") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvComentario" ValidationGroup="UpdateVal"
                                            ControlToValidate="Comentario" SetFocusOnError="True" Display="Dynamic"
                                            ErrorMessage="<b>*</b>" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderCF" runat="server"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars="." TargetControlID="Comentario" />
                  </EditItemTemplate>
                  <FooterTemplate>
                    <asp:TextBox ID="Comentario" runat="server" ValidationGroup="InsertVal"
                                            Width="280px" Text=""></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvComentario" ValidationGroup="InsertVal"
                                            ControlToValidate="Comentario" SetFocusOnError="True" Display="Dynamic"
                                            ErrorMessage="<b>*</b>" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderCF" runat="server"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars="." TargetControlID="Comentario" />
                  </FooterTemplate>
                  <FooterStyle HorizontalAlign="Center" Width="35%" />
                  <HeaderStyle HorizontalAlign="Center" Width="35%" />
                  <ItemStyle HorizontalAlign="Center" Width="35%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Requerido" SortExpression="RequeridoInicioTramite">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="RequeridoInicioTramite" runat="server" Width="50px" SelectedValue='<%# Bind("RequeridoInicioTramite") %>'>
                                            <asp:ListItem Value="S">Si</asp:ListItem>
                                            <asp:ListItem Value="N">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="RequeridoInicioTramite" runat="server" Width="50px" Text='<%# Eval("RequeridoInicioTramite").ToString() == "S" ? "Si" : "No" %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="RequeridoInicioTramite" runat="server" Width="50px">
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
                    &nbsp;<asp:ImageButton ID="IMGBDelete" runat="server" CausesValidation="False" CommandName="Delete"
                      ImageUrl="~/App_Themes/Imagenes/Borrar.gif" Text="Eliminar" ToolTip="Eliminar" />
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
            <div align="right" style="margin-top: 10px;padding-right:10px; margin-bottom:5px">
                <asp:Button   ID="btnRegresar" Text="Regresar" runat="server" CssClass="Botones" Width="130px" Height="21px" onclick="btnRegresar_Click"/>
     </div>
          </div>
        </asp:Panel>
      </div>
    </div>
    <!-- margin: 10px -->
  </div>
  <!-- FondoBlanco -->
  <uc1:Mensaje ID="mensaje" runat="server" />
  <uc1:Mensaje ID="mensajeOk" runat="server" />
</asp:Content>
