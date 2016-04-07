<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="salesorder.aspx.cs" Inherits="appWebStore.sales.salesorder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function(){
        $(".datepicker").datepicker({dateFormat: 'dd M yy'});
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <div class="space16"></div>
    <div class="listbox" style="width: 840px;">
        
        <div class="dialogpanel" style="background: #ffffff;">
            <div class="h1black" style="padding: 8px;" >SALES ORDER</div>

            <div class="hrlinelight"></div>
            <div class="space16"></div>

            <table style="width: 100%">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td class="labelright">Sale Date</td>
                                <td>
                                    <asp:TextBox ID="txtSaleDate" CssClass="textbox datepicker" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright">Customer</td>
                                <td>
                                    <asp:TextBox ID="txtCustomer" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright">Address</td>
                                <td>
                                    <asp:TextBox ID="txtAddress" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td class="labelright">City</td>
                                <td>
                                    <asp:TextBox ID="txtCity" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright">Country</td>
                                <td>
                                    <asp:TextBox ID="txtCountry" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>

            <div class="space16"></div>
            <asp:GridView ID="grdSalesOrderItem" runat="server" AutoGenerateColumns="false" CellPadding="4"
                GridLines="None" CssClass="grid" Width="100%" OnRowCommand="grdSalesOrderItem_RowCommand">
                <RowStyle BackColor="#ffffff" HorizontalAlign="Left" Height="26px" />
                <SelectedRowStyle BackColor="#DCE6F4" Font-Bold="True" ForeColor="#585858" />
                <HeaderStyle CssClass="gridheader" />
                <AlternatingRowStyle BackColor="#EAF5FE" />
                <Columns>
                    <asp:BoundField DataField="SaleItemId">
                        <HeaderStyle CssClass="griddisplaynone" />
                        <ItemStyle CssClass="griddisplaynone" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductName" HeaderText="Product">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                        <HeaderStyle CssClass="gridheaderright" />
                        <ItemStyle CssClass="gridrowright" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ProductPrice" HeaderText="Price" DataFormatString="{0:c}">
                        <HeaderStyle CssClass="gridheaderright" />
                        <ItemStyle CssClass="gridrowright" />
                    </asp:BoundField>

                    <asp:BoundField DataField="LineTotal" HeaderText="Total" DataFormatString="{0:c}">
                        <HeaderStyle CssClass="gridheaderright" />
                        <ItemStyle CssClass="gridrowright" />
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

                </Columns>
            </asp:GridView>

        </div>
    </div>

    <div class="space16"></div>
    <div class="listbox" style="width: 840px;">
        <div class="dialogpanel">

            <table style="width: 100%">
                <tr>
                    <td class="h1black">PRODUCT LIST</td>
                    <td style="width: 184px; text-align: right;">
                        <asp:TextBox ID="txtQuantity"  CssClass="textbox" Width="180px" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 124px; text-align: right;">
                        <asp:Button ID="btnAdd" CssClass="button" Width="120px" runat="server" Text="Add Product" OnClick="btnAdd_Click" />
                    </td>
                </tr>
            </table>


            <div class="space12"></div>

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
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Description" HeaderText="Description">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:c}">
                        <HeaderStyle CssClass="gridheaderright" />
                        <ItemStyle CssClass="gridrowright" />
                    </asp:BoundField>

                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Select">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkProduct" Width="70px" Height="26px" ToolTip="Select Product"
                                runat="server" CssClass="selectimage" CausesValidation="False" CommandName="SelectRow"
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
    <asp:HiddenField ID="hdfCustomerId" Visible="false" runat="server" />
    <asp:HiddenField ID="hdfSalesOrderId" Visible="false" runat="server" />

</asp:Content>
