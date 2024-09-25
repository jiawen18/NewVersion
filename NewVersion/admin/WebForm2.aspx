<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="NewVersion.admin.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <script type="text/javascript">
        function confirmDelete() {
            return confirm("Are you sure you want to remove this supplier?");
        }

        function populateModal(branch, email, phone, address, supplierID) {
            document.getElementById('<%= addBranch.ClientID %>').value = branch;
            document.getElementById('<%= addEmail.ClientID %>').value = email;
            document.getElementById('<%= addPhone.ClientID %>').value = phone;
            document.getElementById('<%= addAddress.ClientID %>').value = address;
            document.getElementById('<%= HiddenSupplierID.ClientID %>').value = supplierID;

            $('#addRowModal').modal('show');
        }
    </script>

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
            <asp:Label ID="FeedbackLabel" runat="server"></asp:Label>
        </div>
        <div class="card-body">

            <asp:HiddenField ID="HiddenSupplierID" runat="server" />

            <!-- Add Row Modal -->
            <div class="modal fade" id="addRowModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header border-0">
                            <h5 class="modal-title">
                                <span class="fw-mediumbold">Edit</span>
                                <span class="fw-light">Supplier</span>
                            </h5>
                        </div>
                        <div class="modal-body">
                            <p class="small">Edit the supplier information</p>
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
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="UpdateRowButton_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView
                    ID="GridView1"
                    runat="server"
                    AutoGenerateColumns="False"
                    AllowSorting="True"
                    CssClass="display table table-striped table-hover"
                    DataKeyNames="supplierID"
                    OnSorting="GridView1_Sorting">
                    <Columns>
                        <asp:BoundField DataField="supplierID" HeaderText="ID" SortExpression="supplierID" Visible="false" />
                        <asp:BoundField DataField="supplierBranch" HeaderText="Branch" SortExpression="supplierBranch" />
                        <asp:BoundField DataField="supplierEmail" HeaderText="Email" SortExpression="supplierEmail" />
                        <asp:BoundField DataField="supplierPhone" HeaderText="Phone" SortExpression="supplierPhone" />
                        <asp:BoundField DataField="supplierAddress" HeaderText="Address" SortExpression="supplierAddress" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button
                                    ID="EditTaskButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-primary"
                                    OnClientClick='<%# "populateModal(\"" + Eval("supplierBranch") + "\", \"" + Eval("supplierEmail") + "\", \"" + Eval("supplierPhone") + "\", \"" + Eval("supplierAddress") + "\", \"" + Eval("supplierID") + "\"); return false;" %>'
                                    Text="Edit" />
                                <asp:Button
                                    ID="RemoveItemButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to remove this supplier?');"
                                    OnClick="RemoveSupplierButton_Click"
                                    CommandArgument='<%# Eval("supplierID") %>'
                                    Text="Remove" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
