<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="orderhistory.aspx.cs" Inherits="appWebStore.sales.orderhistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <div class="space16"></div>
    <div class="listbox" style="width: 840px;">
        <div class="dialogpanel">

            <div class="h1black" style="padding: 8px;">SALES ORDERS</div>

            <div class="space12"></div>
            <asp:GridView ID="grdSales" runat="server" AutoGenerateColumns="false" CellPadding="4"
                GridLines="None" CssClass="grid" Width="100%" OnRowCommand="grdSales_RowCommand">
                <RowStyle BackColor="#ffffff" HorizontalAlign="Left" Height="26px" />
                <SelectedRowStyle BackColor="#DCE6F4" Font-Bold="True" ForeColor="#585858" />
                <HeaderStyle CssClass="gridheader" />
                <AlternatingRowStyle BackColor="#EAF5FE" />
                <Columns>
                    <asp:BoundField DataField="SaleId">
                        <HeaderStyle CssClass="griddisplaynone" />
                        <ItemStyle CssClass="griddisplaynone" />
                    </asp:BoundField>

                    <asp:BoundField DataField="SaleDate" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:BoundField DataField="CustomerName" HeaderText="Customer">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Description" HeaderText="Description">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="{0:c}">
                        <HeaderStyle CssClass="gridheaderright" />
                        <ItemStyle CssClass="gridrowright" />
                    </asp:BoundField>

                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkremove" Width="70px" Height="26px" ToolTip="Delete Sale"
                                runat="server" CssClass="removeimage" CausesValidation="False" CommandName="RemoveRow"
                                CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridheadercenter" />
                        <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Select">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkselect" Width="70px" Height="26px" ToolTip="Select Sale"
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

    <asp:HiddenField ID="hdfCustomerId" Visible="false" runat="server" />



</asp:Content>
