<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="NewVersion.admin.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <div class="card">
        <div class="card-header">
            <div class="d-flex align-items-center">
                <h4 class="card-title">Add Row</h4>
                <asp:LinkButton
                    ID="AddRowButton"
                    runat="server"
                    CssClass="btn btn-black btn-border btn-round ms-auto"
                    OnClientClick="return false;"
                    data-bs-toggle="modal"
                    data-bs-target="#addRowModal">
                    <i class="fa fa-plus" style="padding-right: 8px;"></i>
                    Add Row
                </asp:LinkButton>
            </div>
        </div>
        <div class="card-body">
            <!-- Add Row Modal -->
            <div class="modal fade" id="addRowModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header border-0">
                            <h5 class="modal-title">
                                <span class="fw-mediumbold">New</span>
                                <span class="fw-light">Supplier</span>
                            </h5>
                            <asp:LinkButton
                                ID="CloseModalButton"
                                runat="server"
                                CssClass="close"
                                OnClientClick="$('#addRowModal').modal('hide'); return false;"
                                aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <p class="small">Create a new supplier using this form, make sure you fill them all</p>
                            <div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group form-group-default">
                                            <label>Branch</label>
                                            <asp:TextBox ID="addBranch" runat="server" CssClass="form-control" placeholder="fill branch" />
                                            <asp:RegularExpressionValidator
                                                ID="BranchValidator"
                                                runat="server"
                                                ControlToValidate="addBranch"
                                                ErrorMessage="Branch cannot exceed 100 characters"
                                                ValidationExpression="^.{0,100}$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorBranch"
                                                runat="server"
                                                ControlToValidate="addBranch"
                                                ErrorMessage="Branch is required"
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
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-black btn-border" OnClientClick="CloseModal(); return false;">Close</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Edit Row Modal -->
            <%--<div class="modal fade" id="editRowModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header border-0">
                            <h5 class="modal-title">
                                <span class="fw-mediumbold">Edit</span>
                                <span class="fw-light">Supplier</span>
                            </h5>
                            <asp:LinkButton
                                ID="CloseEditModalButton"
                                runat="server"
                                CssClass="close"
                                OnClientClick="$('#editRowModal').modal('hide'); return false;"
                                aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <p class="small">Edit supplier details using this form</p>
                            <asp:HiddenField ID="editSupplierID" runat="server" />
                            <div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group form-group-default">
                                            <label>Branch</label>
                                            <asp:TextBox ID="editBranch" runat="server" CssClass="form-control" placeholder="fill branch" />
                                            <asp:RegularExpressionValidator
                                                ID="EditBranchValidator"
                                                runat="server"
                                                ControlToValidate="editBranch"
                                                ErrorMessage="Branch cannot exceed 100 characters"
                                                ValidationExpression="^.{0,100}$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorEditBranch"
                                                runat="server"
                                                ControlToValidate="editBranch"
                                                ErrorMessage="Branch is required"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="col-md-6 pe-0">
                                        <div class="form-group form-group-default">
                                            <label>Email</label>
                                            <asp:TextBox ID="editEmail" runat="server" CssClass="form-control" placeholder="fill email" />
                                            <asp:RegularExpressionValidator
                                                ID="EditEmailValidator"
                                                runat="server"
                                                ControlToValidate="editEmail"
                                                ErrorMessage="Invalid email format"
                                                ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorEditEmail"
                                                runat="server"
                                                ControlToValidate="editEmail"
                                                ErrorMessage="Email is required"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-group-default">
                                            <label>Phone</label>
                                            <asp:TextBox ID="editPhone" runat="server" CssClass="form-control" placeholder="fill phone" />
                                            <asp:RegularExpressionValidator
                                                ID="EditPhoneValidator"
                                                runat="server"
                                                ControlToValidate="editPhone"
                                                ErrorMessage="Phone number must be numeric and max 20 characters"
                                                ValidationExpression="^\d{1,20}$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorEditPhone"
                                                runat="server"
                                                ControlToValidate="editPhone"
                                                ErrorMessage="Phone number is required"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group form-group-default">
                                            <label>Address</label>
                                            <asp:TextBox ID="editAddress" runat="server" CssClass="form-control" placeholder="fill address" />
                                            <asp:RegularExpressionValidator
                                                ID="EditAddressValidator"
                                                runat="server"
                                                ControlToValidate="editAddress"
                                                ErrorMessage="Address cannot exceed 255 characters"
                                                ValidationExpression="^.{0,255}$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorEditAddress"
                                                runat="server"
                                                ControlToValidate="editAddress"
                                                ErrorMessage="Address is required"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer border-0">
                            <asp:Button ID="ButtonEdit" runat="server" CssClass="btn btn-primary" Text="Save Changes" OnClick="ButtonEdit_Click" />
                            <asp:LinkButton ID="LinkButtonCloseEdit" runat="server" CssClass="btn btn-black btn-border" OnClientClick="$('#editRowModal').modal('hide'); return false;">Close</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>--%>

            <div class="table-responsive">
                <asp:GridView
                    ID="GridView1"
                    runat="server"
                    AutoGenerateColumns="False"
                    AllowSorting="True"
                    CssClass="display table table-striped table-hover"
                    OnSorting="GridView1_Sorting">
                    <Columns>
                        <asp:BoundField DataField="supplierID" HeaderText="ID" SortExpression="supplierID" />
                        <asp:BoundField DataField="supplierBranch" HeaderText="Branch" SortExpression="supplierBranch" />
                        <asp:BoundField DataField="supplierEmail" HeaderText="Email" SortExpression="supplierEmail" />
                        <asp:BoundField DataField="supplierPhone" HeaderText="Phone" SortExpression="supplierPhone" />
                        <asp:BoundField DataField="supplierAddress" HeaderText="Address" SortExpression="supplierAddress" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="CopyItemButton"
                                    runat="server"
                                    CommandName="Copy"
                                    CssClass="btn btn-link btn-black"
                                    data-bs-toggle="tooltip"
                                    title="Copy"
                                    data-original-title="Copy Item">
                                    <i class="fa fa-copy"></i>
                                </asp:LinkButton>
                                <asp:LinkButton
                                    ID="EditTaskButton"
                                    runat="server"
                                    CommandName="Edit"
                                    CssClass="btn btn-link btn-primary"
                                    data-bs-toggle="tooltip"
                                    title="Edit"
                                    data-original-title="Edit Task"
                                    OnClientClick="return ShowEditModal(this);">
                                    <i class="fa fa-edit"></i>
                                </asp:LinkButton>
                                <asp:LinkButton
                                    ID="RemoveItemButton"
                                    runat="server"
                                    CommandName="Delete"
                                    CssClass="btn btn-link btn-danger"
                                    data-bs-toggle="tooltip"
                                    title="Remove"
                                    data-original-title="Remove">
                                    <i class="fa fa-trash"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <%--<script type="text/javascript">
        function ShowEditModal(linkButton) {

            var row = $(linkButton).closest('tr');

            var supplierID = row.find('td:eq(0)').text();
            var branch = row.find('td:eq(1)').text();
            var email = row.find('td:eq(2)').text();
            var phone = row.find('td:eq(3)').text();
            var address = row.find('td:eq(4)').text();

            $('#<%= editSupplierID.ClientID %>').val(supplierID);
            $('#<%= editBranch.ClientID %>').val(branch);
            $('#<%= editEmail.ClientID %>').val(email);
            $('#<%= editPhone.ClientID %>').val(phone);
            $('#<%= editAddress.ClientID %>').val(address);

            $('#editRowModal').modal('show');

            return false;
        }

        function CloseModal() {
            $('#addRowModal').modal('hide');
            $('.modal-backdrop').remove();
        }

    </script>--%>
</asp:Content>
