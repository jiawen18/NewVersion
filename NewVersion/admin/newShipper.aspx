<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="newShipper.aspx.cs" Inherits="NewVersion.admin.newShipper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

<<<<<<< HEAD
=======

>>>>>>> 86cf3bc8bcf3431942c093f919979914f331d4a2
    <div class="card">
        <div class="card-header">
            <div class="d-flex align-items-center">
                <h4 class="card-title">New Shipper</h4>
                <asp:LinkButton
                    ID="AddRowButton"
                    runat="server"
                    CssClass="btn btn-black btn-border btn-round ms-auto"
                    OnClientClick="return false;"
                    data-bs-toggle="modal"
                    data-bs-target="#addRowModal">
                    <i class="fa fa-plus" style="padding-right: 8px;"></i>
                    Add
                </asp:LinkButton>
            </div>
            <asp:Label ID="FeedbackLabel" runat="server"></asp:Label>
        </div>
        <div class="card-body">

            <asp:HiddenField ID="HiddenShipperID" runat="server" />

            <!-- Add Row Modal -->
            <div class="modal fade" id="addRowModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header border-0">
                            <h5 class="modal-title">
                                <span class="fw-mediumbold">New</span>
                                <span class="fw-light">Shipper</span>
                            </h5>
                        </div>
                        <div class="modal-body">
                            <p class="small">Create a new shipper using this form, make sure you fill them all</p>
                            <div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group form-group-default">
                                            <label>Name</label>
                                            <asp:TextBox ID="addName" runat="server" CssClass="form-control" placeholder="fill name" />
                                            <asp:RegularExpressionValidator
                                                ID="NameValidator"
                                                runat="server"
                                                ControlToValidate="addName"
                                                ErrorMessage="Name cannot exceed 100 characters"
                                                ValidationExpression="^.{0,100}$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorName"
                                                runat="server"
                                                ControlToValidate="addName"
                                                ErrorMessage="Name is required"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="col-md-6 pe-0">
                                        <div class="form-group form-group-default">
                                            <label>Email</label>
                                            <asp:TextBox ID="addEmail" runat="server" CssClass="form-control" placeholder="fill email" />
                                            <asp:RegularExpressionValidator
                                                ID="EmailValidator"
                                                runat="server"
                                                ControlToValidate="addEmail"
                                                ErrorMessage="Invalid email format"
                                                ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorEmail"
                                                runat="server"
                                                ControlToValidate="addEmail"
                                                ErrorMessage="Email is required"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-group-default">
                                            <label>Phone</label>
                                            <asp:TextBox ID="addPhone" runat="server" CssClass="form-control" placeholder="fill phone" />
                                            <asp:RegularExpressionValidator
                                                ID="PhoneValidator"
                                                runat="server"
                                                ControlToValidate="addPhone"
                                                ErrorMessage="Phone number must be numeric and max 20 characters"
                                                ValidationExpression="^\d{1,20}$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorPhone"
                                                runat="server"
                                                ControlToValidate="addPhone"
                                                ErrorMessage="Phone number is required"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group form-group-default">
                                            <label>Address</label>
                                            <asp:TextBox ID="addAddress" runat="server" CssClass="form-control" placeholder="fill address" />
                                            <asp:RegularExpressionValidator
                                                ID="AddressValidator"
                                                runat="server"
                                                ControlToValidate="addAddress"
                                                ErrorMessage="Address cannot exceed 255 characters"
                                                ValidationExpression="^.{0,255}$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorAddress"
                                                runat="server"
                                                ControlToValidate="addAddress"
                                                ErrorMessage="Address is required"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer border-0">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="AddRowButton_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView
                    ID="GridView1"
                    runat="server"
                    AutoGenerateColumns="False"
                    AllowSorting="False"
                    CssClass="display table table-striped table-hover"
                    DataKeyNames="shipperID">
                    <Columns>
                        <asp:BoundField DataField="shipperID" HeaderText="ID" SortExpression="shipperID" Visible="false" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="shipperName" HeaderText="Name" SortExpression="shipperName" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="shipperEmail" HeaderText="Email" SortExpression="shipperEmail" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="shipperPhone" HeaderText="Phone" SortExpression="shipperPhone" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="shipperAddress" HeaderText="Address" SortExpression="shipperAddress" HeaderStyle-ForeColor="Black" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

<<<<<<< HEAD
=======

>>>>>>> 86cf3bc8bcf3431942c093f919979914f331d4a2
</asp:Content>

