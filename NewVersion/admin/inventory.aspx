<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="inventory.aspx.cs" Inherits="NewVersion.admin.inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <script type="text/javascript">
        function confirmDelete() {
            return confirm("Are you sure you want to remove this product?");
        }

        function populateModal(productName, supplierBranch, quantity, productID) {
            document.getElementById('<%= addInventoryName.ClientID %>').value = productName;
            document.getElementById('<%= currentInventoryQuantity.ClientID %>').value = quantity;
            document.getElementById('<%= HiddenProductID.ClientID %>').value = productID;
            document.getElementById('<%= adjustInventoryQuantity.ClientID %>').value = "0";

            var dropdown = document.getElementById('<%= addInventorySupplier.ClientID %>');
            if (!supplierBranch) {
                dropdown.selectedIndex = 0;
            } else {
                for (var i = 0; i < dropdown.options.length; i++) {
                    if (dropdown.options[i].text === supplierBranch) {
                        dropdown.selectedIndex = i;
                        break;
                    }
                }
            }

            $('#addRowModal').modal('show');
        }
    </script>

    <div class="card">
        <div class="card-header">
            <div class="d-flex align-items-center">
                <h4 class="card-title">Inventory</h4>
            </div>
            <asp:Label ID="FeedbackLabel" runat="server"></asp:Label>
        </div>
        <div class="card-body">

            <asp:HiddenField ID="HiddenProductID" runat="server" />

            <!-- Add Row Modal -->
            <div class="modal fade" id="addRowModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header border-0">
                            <h5 class="modal-title">
                                <span class="fw-mediumbold">Edit</span>
                                <span class="fw-light">Product</span>
                            </h5>
                        </div>
                        <div class="modal-body">
                            <p class="small">Edit the product information</p>
                            <div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group form-group-default">
                                            <label>Name</label>
                                            <asp:TextBox ID="addInventoryName" runat="server" CssClass="form-control" placeholder="Product name" ReadOnly="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-6 pe-0">
                                        <div class="form-group form-group-default">
                                            <label>Supplier</label>
                                            <asp:DropDownList ID="addInventorySupplier" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1"
                                                runat="server"
                                                ControlToValidate="addInventorySupplier"
                                                ErrorMessage="Supplier is required."
                                                CssClass="text-danger"
                                                Display="Dynamic"
                                                InitialValue="">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-group-default">
                                            <label>Current Quantity</label>
                                            <asp:TextBox ID="currentInventoryQuantity" runat="server" CssClass="form-control" ReadOnly="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-group-default">
                                            <label>Add/Subtract Quantity</label>
                                            <asp:TextBox ID="adjustInventoryQuantity" runat="server" CssClass="form-control" placeholder="Enter adjustment" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredQuantityValidator"
                                                runat="server"
                                                ControlToValidate="adjustInventoryQuantity"
                                                ErrorMessage="Adjustment is required."
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:RegularExpressionValidator
                                                ID="QuantityValidator"
                                                runat="server"
                                                ControlToValidate="adjustInventoryQuantity"
                                                ErrorMessage="Adjustment must be a number (positive or negative)"
                                                ValidationExpression="^-?\d+$"
                                                CssClass="text-danger"
                                                Display="Dynamic" />
                                            <asp:CustomValidator
                                                ID="CustomQuantityValidator"
                                                runat="server"
                                                ErrorMessage="Adjustment must be less than the current quantity."
                                                CssClass="text-danger"
                                                Display="Dynamic"
                                                OnServerValidate="ValidateQuantity"
                                                ClientValidationFunction="ValidateQuantityClient" />
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
                    DataKeyNames="ProductID"
                    OnSorting="GridView1_Sorting"
                    OnPageIndexChanging="GridView1_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ProductID" HeaderText="ID" SortExpression="ProductID" Visible="false" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="ProductName" HeaderText="Name" SortExpression="ProductName" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier" SortExpression="SupplierName" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" HeaderStyle-ForeColor="Black" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button
                                    ID="EditTaskButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-primary"
                                    OnClientClick='<%# "populateModal(\"" + Eval("ProductName") + "\", \"" + Eval("SupplierName") + "\", \"" + Eval("Quantity") + "\", \"" + Eval("ProductID") + "\"); return false;" %>'
                                    Text="Edit" />
                                <asp:Button
                                    ID="RemoveItemButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to remove this inventory item?');"
                                    OnClick="RemoveProductButton_Click"
                                    CommandArgument='<%# Eval("ProductID") %>'
                                    Text="Remove" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>

</asp:Content>
