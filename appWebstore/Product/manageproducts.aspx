<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="manageproducts.aspx.cs" Inherits="appStore.product.manageproducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <div class="space16"></div>
    <div class="dialogbox" style="width: 840px;">
        <div class="dialogtitlebar">
            <div class="h1white">MANAGE PRODUCTS</div>
        </div>
        <div class="inputpanel">
            <table style="width: 100%">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td class="labelright width100">Name</td>
                                <td class="ctrlalign">
                                    <asp:TextBox ID="txtName" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width100">Description</td>
                                <td class="ctrlalign">
                                    <asp:TextBox ID="txtDescription" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td class="labelright width100">Price</td>
                                <td class="ctrlalign">
                                    <asp:TextBox ID="txtPrice" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <div class="space8"></div>
            <div style="text-align: right;">
                <asp:Button ID="btnSave" CssClass="button" Width="120px" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnNew" CssClass="button" Width="120px" runat="server" Text="New" OnClick="btnNew_Click" />
            </div>
        </div>
    </div>

    <div class="space16"></div>
    
    <div class="listbox" style="width: 840px;">
        <div class="dialogpanel">

            <asp:GridView ID="grdProduct" runat="server" AutoGenerateColumns="false" CellPadding="4"
                GridLines="None" CssClass="grid" Width="100%" OnRowCommand="grdProduct_RowCommand">
                <RowStyle BackColor="#ffffff" HorizontalAlign="Left" Height="26px" />
                <SelectedRowStyle BackColor="#DCE6F4" Font-Bold="True" ForeColor="#585858" />
                <HeaderStyle CssClass="gridheader" />
                <AlternatingRowStyle BackColor="#EAF5FE" />
                <Columns>
                    <asp:BoundField DataField="ProductId">
                        <HeaderStyle CssClass="griddisplaynone" />
                        <ItemStyle CssClass="griddisplaynone" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Name" HeaderText="Product">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" Width="90px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Description" HeaderText="Description">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:c}">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkremove" Width="70px" Height="26px" ToolTip="Remove Product"
                                runat="server" CssClass="removeimage" CausesValidation="False" CommandName="RemoveRow"
                                CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridheadercenter" />
                        <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkProduct" Width="70px" Height="26px" ToolTip="Edit Product"
                                runat="server" CssClass="editimage" CausesValidation="False" CommandName="SelectRow"
                                CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridheadercenter" />
                        <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
    </div>

    <asp:HiddenField ID="hdfProductId" Visible="false" runat="server" />

</asp:Content>
