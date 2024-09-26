<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="orderList.aspx.cs" Inherits="NewVersion.admin.orderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <div class="col-md-12">
        <div class="card">
            <div class="card-header">
                <div class="d-flex align-items-center">
                    <h4 class="card-title">Add Row</h4>
                    <button
                        class="btn btn-primary btn-round ms-auto"
                        data-bs-toggle="modal"
                        data-bs-target="#addRowModal">
                        <i class="fa fa-plus"></i>
                        Add Row
                    </button>
                </div>
            </div>
            <div class="card-body">
                <!-- Modal -->
                <div class="modal fade" id="addRowModal" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header border-0">
                <h5 class="modal-title">
                    <span class="fw-mediumbold">New</span>
                    <span class="fw-light">Row </span>
                </h5>
                <asp:Button ID="Button2" runat="server" CssClass="close" 
                    OnClientClick="return cancelFunction();" data-bs-dismiss="modal" />
                <span aria-hidden="true" style="cursor: pointer;" onclick="cancelFunction(); $('#addRowModal').modal('hide');">&times;</span>
            </div>
            <div class="modal-body">
                <p class="small">
                    Create a new product using this form. Make sure to fill in all the required fields.
                </p>
                <div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group form-group-default">
                                <label for="txtProductID">Product ID:</label>
                                <asp:HiddenField ID="txtProductID" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group form-group-default">
                                    <label for="txtProductName">Product Name:</label>
                                    <asp:TextBox ID="txtProductName" runat="server" class="form-control" placeholder="Enter product name" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorProductName" runat="server" ErrorMessage="Please enter a product name." Display="Dynamic" CssClass="error" ControlToValidate="txtProductName" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group form-group-default">
                                    <label for="txtProductImageURL">Product Image URL:</label>
                                    <asp:TextBox ID="txtProductImageURL" runat="server" class="form-control" placeholder="Enter image URL" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter an image URL." Display="Dynamic" CssClass="error" ControlToValidate="txtProductImageURL" />
                                </div>
                            </div>
                            <div class="col-sm-6 pe-0">
                                <div class="form-group form-group-default">
                                    <label for="txtPrice">Price:</label>
                                    <asp:TextBox ID="txtPrice" runat="server" class="form-control" placeholder="Enter price" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter a price." Display="Dynamic" CssClass="error" ControlToValidate="txtPrice" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group form-group-default">
                                    <label for="txtQuantity">Quantity:</label>
                                    <asp:TextBox ID="txtQuantity" runat="server" class="form-control" placeholder="Enter quantity" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter a quantity." Display="Dynamic" CssClass="error" ControlToValidate="txtQuantity" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group form-group-default">
                                    <label for="chkIsVisible">Is Visible:</label>
                                    <asp:CheckBox ID="chkIsVisible" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer border-0">
                <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" CssClass="btn btn-primary" OnClick="btnAddProduct_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-danger" data-dismiss="modal" OnClientClick="return cancelFunction();"/>
            </div>
        </div>
    </div>
</div>
                <asp:Label ID="lblMessage" runat="server" />

        <!-- Edit Row Modal -->
        <div class="modal fade" id="editRowModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header border-0">
                        <h5 class="modal-title">
                            <span class="fw-mediumbold">Edit</span>
                            <span class="fw-light">Product </span>
                        </h5>
                       <asp:Button ID="Button3" runat="server" CssClass="close" 
                        OnClientClick="return cancelFunction();" data-bs-dismiss="modal" />
                    <span aria-hidden="true" style="cursor: pointer;" onclick="cancelFunction(); $('#addRowModal').modal('hide');">&times;</span>
                    </div>
                    <div class="modal-body">
                        <div class="form-group form-group-default">
                            <label for="txtProductName1">Product Name:</label>
                            <asp:TextBox ID="txtProductName1" runat="server" class="form-control" placeholder="Enter product name" />
                        </div>
                        <div class="form-group form-group-default">
                            <label for="txtProductImageURL1">Product Image URL:</label>
                            <asp:TextBox ID="txtProductImageURL1" runat="server" class="form-control" placeholder="Enter image URL" />
                        </div>
                        <div class="form-group form-group-default">
                            <label for="txtPrice1">Price:</label>
                            <asp:TextBox ID="txtPrice1" runat="server" class="form-control" placeholder="Enter price" />
                        </div>
                        <div class="form-group form-group-default">
                            <label for="txtQuantity1">Quantity:</label>
                            <asp:TextBox ID="txtQuantity1" runat="server" class="form-control" placeholder="Enter quantity" />
                        </div>
                        <div class="form-group form-group-default">
                            <label for="chkIsVisible1">Is Visible:</label>
                            <asp:CheckBox ID="chkIsVisible1" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer border-0">
                        <asp:Button ID="btnUpdateProduct" runat="server" Text="Update Product" CssClass="btn btn-primary" OnClick="btnUpdateProduct_Click" />
                        <asp:Button ID="Button1" runat="server" Text="Close" class="btn btn-danger" data-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>

                <div class="table-responsive">
                    <table
                        id="add-row"
                        class="display table table-striped table-hover">
                        <thead>
                            <tr>
                                <th>OrderID</th>
                                <th>Order Details</th>
                                <th>Payment Status</th>
                                <th>Delievery Status</th>
                                <th style="width: 10%">Action</th>
                            </tr>
                        </thead>
                        <%--<tfoot>
                            <tr>
                                <th>Name</th>
                                <th>Position</th>
                                <th>Office</th>
                                <th>Action</th>
                            </tr>
                        </tfoot>--%>
                        <tbody>
                            <asp:Repeater ID="OrderRepeater" runat="server">
                                <ItemTemplate>
                            <tr>
                                <td><%# Eval("OrderID") %></td>
                                <td><%# Eval("OrderDetails") %></td>
                                <td><%# Eval("PaymentStatus") %></td>
                                <td><%# Eval("DeliveryStatus") %></td>
                                <td>
                                    <div class="form-button-action">
                                        <button
                                            type="button"
                                            data-bs-toggle="tooltip"
                                            title=""
                                            class="btn btn-link btn-primary btn-lg"
                                            data-original-title="Edit Task">
                                            <i class="fa fa-edit"></i>
                                        </button>
                                        <button
                                            type="button"
                                            data-bs-toggle="tooltip"
                                            title=""
                                            class="btn btn-link btn-danger"
                                            data-original-title="Remove">
                                            <i class="fa fa-times"></i>
                                        </button>
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
