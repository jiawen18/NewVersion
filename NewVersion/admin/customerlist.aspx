<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="customerlist.aspx.cs" Inherits="NewVersion.customerlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
        <div class="col-md-12">
        <div class="card">
          
            <div class="card-body">
                <div class="table-responsive">
                    <table
                        id="add-row"
                        class="display table table-striped table-hover">
                        <thead>
                            <tr>
                                <th>Customer ID</th>
                                <th>Email</th>
                                <th>Username</th>
                                <th style="width: 10%">Action</th>
                            </tr>
                        </thead>
                          <tbody>
                   <!-- Repeater control to bind customer data -->
                    <asp:Repeater ID="RepeaterCustomerList" runat="server">
                        <ItemTemplate>
                            <tr>                        
                                <td>
                                    <asp:Label ID="lbl_customerID" runat="server" Text='<%# Eval("MemberID") %>'></asp:Label>
                                    <asp:TextBox ID="txt_customerID" runat="server" Text='<%# Eval("MemberID") %>' CssClass="form-control" Visible="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_email" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                    <asp:TextBox ID="txt_email" runat="server" Text='<%# Eval("Email") %>' CssClass="form-control" Visible="false"></asp:TextBox>
                                </td>
                                 <td>
                                    <asp:Label ID="lbl_username" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                    <asp:TextBox ID="txt_username" runat="server" Text='<%# Eval("Username") %>' CssClass="form-control" Visible="false"></asp:TextBox>
                                </td>
                                <td>
                                    <div class="form-button-action">
                                        <!-- Edit button -->
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-link btn-primary btn-lg"
                                            CommandName="EditCustomer" CommandArgument='<%# Eval("Username") %>' OnClick="EditCustomer_Click" />

                                        <!-- Save button, initially hidden -->
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-link btn-success btn-lg"
                                            CommandName="SaveCustomer" CommandArgument='<%# Eval("Username") %>' OnClick="SaveCustomer_Click" Visible="false" />

                                        <!-- Delete button with confirmation -->
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-link btn-danger btn-lg"
                                            CommandName="DeleteCustomer" CommandArgument='<%# Eval("Username") %>' OnClick="DeleteCustomer_Click"
                                            OnClientClick="return confirm('Are you sure you want to delete this customer?');" />
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

   
</asp:Content>