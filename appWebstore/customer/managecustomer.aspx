<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="managecustomer.aspx.cs" Inherits="appWebstore.customer.managecustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <div class="space16"></div>
    <div class="dialogbox" style="width: 840px;">
       <div class="dialogtitlebar">
            <div id="divTitle" runat="server" class="h1white">MANAGE CUSTOMERS</div>
        </div>
        <div class="inputpanel">

            <table style="width: 100%">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td class="labelright">Name:</td>
                                <td>
                                    <asp:TextBox ID="txtName" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright">Address:</td>
                                <td>
                                    <asp:TextBox ID="txtAddress" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td class="labelright">City:</td>
                                <td>
                                    <asp:DropDownList ID="cboCity" CssClass="combobox" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright">Country:</td>
                                <td>
                                    <asp:TextBox ID="txtCountry" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <div style="padding-top: 16px; text-align: right;">
                <asp:Button ID="btnSave" CssClass="button" runat="server" Width="120px" Text="Save" OnClick="btnSave_Click" />
            </div>

        </div>

    </div>

    <div class="space16"></div>

    <div class="listbox" style="width: 840px;">
        <div class="dialogpanel">
            <div style="text-align:right; padding-bottom: 8px;">
                <asp:DropDownList ID="cboFilter" CssClass="combobox" AutoPostBack="true" 
                    runat="server" OnSelectedIndexChanged="cboFilter_SelectedIndexChanged"></asp:DropDownList>

            </div>

            <asp:GridView ID="grdCustomer" runat="server" AutoGenerateColumns="false" CellPadding="4"
                GridLines="None" CssClass="grid" Width="100%" OnRowCommand="grdCustomer_RowCommand" Height="238px">
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

                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkremove" Width="70px" Height="26px" ToolTip="Remove Customer"
                                runat="server" CssClass="removeimage" CausesValidation="False" CommandName="RemoveRow"
                                CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridheadercenter" />
                        <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkcustomer" Width="70px" Height="26px" ToolTip="Edit Customer"
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


    <asp:HiddenField ID="hdfCustomerId" Visible="false" runat="server" />

</asp:Content>
