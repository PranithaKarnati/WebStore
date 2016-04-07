<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="selectcustomer.aspx.cs" Inherits="appStore.customer.selectcustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <div class="space16"></div>
    <div class="listbox" style="width: 840px;">
        <div class="dialogpanel">

            <div class="h1black" style="padding: 8px;" >SELECT CUSTOMER</div>

            <div class="space12"></div>
            <asp:GridView ID="grdCustomer" runat="server" AutoGenerateColumns="false" CellPadding="4"
                GridLines="None" CssClass="grid" Width="100%" OnRowCommand="grdCustomer_RowCommand">
                <RowStyle BackColor="#ffffff" HorizontalAlign="Left" Height="26px" />
                <SelectedRowStyle BackColor="#DCE6F4" Font-Bold="True" ForeColor="#585858" />
                <HeaderStyle CssClass="gridheader" />
                <AlternatingRowStyle BackColor="#EAF5FE" />
                <Columns>
                    <asp:BoundField DataField="CustomerId">
                        <HeaderStyle CssClass="griddisplaynone" />
                        <ItemStyle CssClass="griddisplaynone" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="Customer">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Address" HeaderText="Address">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:BoundField DataField="City" HeaderText="City">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Country" HeaderText="Country">
                        <HeaderStyle CssClass="gridheader" />
                        <ItemStyle CssClass="gridrow" />
                    </asp:BoundField>


                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Select">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkcustomer" Width="70px" Height="26px" ToolTip="Select Customer"
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
