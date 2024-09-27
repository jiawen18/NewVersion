<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="customerlist.aspx.cs" Inherits="NewVersion.admin.customerlist" %>
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
                                <th>ID</th>
                                <th>Email</th>
                                <th>Username</th>
                                <th style="width: 10%">Action</th>
                            </tr>
                        </thead>
                          <tbody>
                   <!-- Repeater control to bind admin data -->
                    <asp:Repeater ID="RepeaterAdminList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Username") %></td>
                                <td>
                                    <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>'></asp:Label>
                                    <asp:TextBox ID="txtPosition" runat="server" Text='<%# Eval("Position") %>' CssClass="form-control" Visible="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblOffice" runat="server" Text='<%# Eval("Office") %>'></asp:Label>
                                    <asp:TextBox ID="txtOffice" runat="server" Text='<%# Eval("Office") %>' CssClass="form-control" Visible="false"></asp:TextBox>
                                </td>
                                <td>
                                    <div class="form-button-action">
                                        <!-- Edit button -->
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-link btn-primary btn-lg"
                                            CommandName="EditAdmin" CommandArgument='<%# Eval("Username") %>' OnClick="EditAdmin_Click" />

                                        <!-- Save button, initially hidden -->
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-link btn-success btn-lg"
                                            CommandName="SaveAdmin" CommandArgument='<%# Eval("Username") %>' OnClick="SaveAdmin_Click" Visible="false" />

                                        <!-- Delete button with confirmation -->
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-link btn-danger btn-lg"
                                            CommandName="DeleteAdmin" CommandArgument='<%# Eval("Username") %>' OnClick="DeleteAdmin_Click"
                                            OnClientClick="return confirm('Are you sure you want to delete this admin?');" />
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
