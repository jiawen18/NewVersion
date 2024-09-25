<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="newInventory.aspx.cs" Inherits="NewVersion.admin.newInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <div class="card">
        <div class="card-header">
            <div class="d-flex align-items-center">
                <h4 class="card-title">New Inventory Item</h4>
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

            <asp:HiddenField ID="HiddenInventoryID" runat="server" />

            <!-- Add Row Modal -->
            <div class="modal fade" id="addRowModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header border-0">
                            <h5 class="modal-title">
                                <span class="fw-mediumbold">New</span>
                                <span class="fw-light">Inventory Item</span>
                            </h5>
                        </div>
                        <div class="modal-body">
                            <p class="small">Create a new inventory item using this form, make sure you fill them all</p>
                            <div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group form-group-default">
                                            <label>Name</label>
                                            <asp:DropDownList ID="addInventoryName" runat="server" CssClass="form-control" placeholder="Select item name"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 pe-0">
                                        <div class="form-group form-group-default">
                                            <label>Supplier</label>
                                            <asp:DropDownList ID="addInventorySupplier" runat="server" CssClass="form-control" placeholder="Select supplier"></asp:DropDownList>
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
                    DataKeyNames="inventoryID">
                    <Columns>
                        <asp:BoundField DataField="inventoryID" HeaderText="ID" SortExpression="inventoryID" Visible="false" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="inventoryName" HeaderText="Name" SortExpression="inventoryName" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="inventorySupplier" HeaderText="Supplier" SortExpression="inventorySupplier" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="inventoryQuantity" HeaderText="Quantity" SortExpression="inventoryQuantity" HeaderStyle-ForeColor="Black" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
