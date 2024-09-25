<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="inventory.aspx.cs" Inherits="NewVersion.admin.inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <script type="text/javascript">
        function confirmDelete() {
            return confirm("Are you sure you want to remove this inventory item?");
        }

        function populateModal(name, supplier, quantity, inventoryID) {
            document.getElementById('<%= addInventoryName.ClientID %>').value = name;
            document.getElementById('<%= addInventorySupplier.ClientID %>').value = supplier;
            document.getElementById('<%= addInventoryQuantity.ClientID %>').value = quantity;
            document.getElementById('<%= HiddenInventoryID.ClientID %>').value = inventoryID;

            $('#addRowModal').modal('show');
        }
    </script>

    <div class="card">
        <div class="card-header">
            <div class="d-flex align-items-center">
                <h4 class="card-title">List of Inventory Items</h4>
            </div>
            <asp:Label ID="FeedbackLabel" runat="server"></asp:Label>
        </div>
        <div class="card-body">

            <asp:HiddenField ID="HiddenInventoryID" runat="server" />

            <!-- Add Row Modal -->
            <div class="modal fade" id="addRowModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header border-0">
                            <h5 class="modal-title">
                                <span class="fw-mediumbold">Edit</span>
                                <span class="fw-light">Inventory Item</span>
                            </h5>
                        </div>
                        <div class="modal-body">
                            <p class="small">Edit the inventory item information</p>
                            <div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group form-group-default">
                                            <label>Name</label>
                                            <asp:TextBox ID="addInventoryName" runat="server" CssClass="form-control" placeholder="fill item name" />
                                            <asp:RegularExpressionValidator
                                                ID="NameValidator"
                                                runat="server"
                                                ControlToValidate="addInventoryName"
                                                ErrorMessage="Name cannot exceed 100 characters"
                                                ValidationExpression="^.{0,100}$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorName"
                                                runat="server"
                                                ControlToValidate="addInventoryName"
                                                ErrorMessage="Name is required"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="col-md-6 pe-0">
                                        <div class="form-group form-group-default">
                                            <label>Supplier</label>
                                            <asp:TextBox ID="addInventorySupplier" runat="server" CssClass="form-control" placeholder="fill supplier name" />
                                            <asp:RegularExpressionValidator
                                                ID="SupplierValidator"
                                                runat="server"
                                                ControlToValidate="addInventorySupplier"
                                                ErrorMessage="Supplier cannot exceed 100 characters"
                                                ValidationExpression="^.{0,100}$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorSupplier"
                                                runat="server"
                                                ControlToValidate="addInventorySupplier"
                                                ErrorMessage="Supplier is required"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-group-default">
                                            <label>Quantity</label>
                                            <asp:TextBox ID="addInventoryQuantity" runat="server" CssClass="form-control" placeholder="fill quantity" />
                                            <asp:RegularExpressionValidator
                                                ID="QuantityValidator"
                                                runat="server"
                                                ControlToValidate="addInventoryQuantity"
                                                ErrorMessage="Quantity must be a positive number"
                                                ValidationExpression="^\d+$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorQuantity"
                                                runat="server"
                                                ControlToValidate="addInventoryQuantity"
                                                ErrorMessage="Quantity is required"
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
                    AllowPaging="True"
                    PageSize="7"
                    CssClass="display table table-striped table-hover"
                    DataKeyNames="inventoryID"
                    OnSorting="GridView1_Sorting"
                    OnPageIndexChanging="GridView1_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="inventoryID" HeaderText="ID" SortExpression="inventoryID" Visible="false" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="inventoryName" HeaderText="Name" SortExpression="inventoryName" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="inventorySupplier" HeaderText="Supplier" SortExpression="inventorySupplier" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="inventoryQuantity" HeaderText="Quantity" SortExpression="inventoryQuantity" HeaderStyle-ForeColor="Black" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button
                                    ID="EditTaskButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-primary"
                                    OnClientClick='<%# "populateModal(\"" + Eval("inventoryName") + "\", \"" + Eval("inventorySupplier") + "\", \"" + Eval("inventoryQuantity") + "\", \"" + Eval("inventoryID") + "\"); return false;" %>'
                                    Text="Edit" />
                                <asp:Button
                                    ID="RemoveItemButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to remove this inventory item?');"
                                    OnClick="RemoveInventoryButton_Click"
                                    CommandArgument='<%# Eval("inventoryID") %>'
                                    Text="Remove" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
