﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="productList.aspx.cs" Inherits="NewVersion.admin.productList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
<asp:SqlDataSource 
    ID="SqlDataSource1" 
    runat="server" 
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    ProviderName="System.Data.SqlClient"
    SelectCommand="SELECT ProductID, ProductName, ProductImageURL, Price, Quantity, 
    ProductStorage, ProductColor, IsVisible FROM Product">
</asp:SqlDataSource>

<div class="col-md-12">
    <div class="card">
        <div class="card-header">
            <div class="d-flex align-items-center">
                <h4 class="card-title">Add Row</h4>
                <asp:Button ID="btnAddRow" runat="server" Text="Add Row"  class="btn btn-primary btn-round ms-auto" data-bs-toggle="modal" data-bs-target="#addRowModal" OnClientClick="showModal(); return false;" />
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorProductName" runat="server" ErrorMessage="Please enter a product name." Display="Dynamic" CssClass="text-danger" ControlToValidate="txtProductName" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="form-group form-group-default">
                                                <label for="txtProductImageURL">Product Image URL:</label>
                                                <asp:TextBox ID="txtProductImageURL" runat="server" class="form-control" placeholder="Enter image URL" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter an image URL." Display="Dynamic" CssClass="text-danger" ControlToValidate="txtProductImageURL" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pe-0">
                                            <div class="form-group form-group-default">
                                                <label for="txtPrice">Price:</label>
                                                <asp:TextBox ID="txtPrice" runat="server" class="form-control" placeholder="Enter price" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter a price." Display="Dynamic" CssClass="text-danger" ControlToValidate="txtPrice" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group form-group-default">
                                                <label for="txtQuantity">Quantity:</label>
                                                <asp:TextBox ID="txtQuantity" runat="server" class="form-control" placeholder="Enter quantity" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter a quantity." Display="Dynamic" CssClass="text-danger" ControlToValidate="txtQuantity" />
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
            <!-- Product Table -->
            <div class="table-responsive">
                <table id="add-row" class="display table table-striped table-hover">
                    <thead>
                        <tr>
                            <th>Product ID</th>
                            <th>Product Name</th>
                            <th>Product Image</th>
                            <th>Price</th>
                            <th style="width: 10%">Quantity</th>
                        </tr>
                    </thead>
                    <tbody id="productTableBody">
                        <%-- Dynamically generated rows go here --%>
                        <asp:Repeater ID="ProductRepeater" runat="server" OnItemCommand="ProductRepeater_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("ProductID") %></td>
                                    <td><%# Eval("ProductName") %></td>
                                    <td><img src='<%# Eval("ProductImageURL") %>' alt="Product Image" style="width:50px;height:50px;" /></td>
                                    <td><%# Eval("Price") %></td>
                                    <td><%# Eval("Quantity") %></td>
                                    <td>
                                <div class="form-button-action">
                                    <asp:LinkButton runat="server" CssClass="btn btn-link btn-primary btn-lg" 
                                        CommandName="EditProduct" 
                                        CommandArgument='<%# Eval("ProductID") %>' 
                                        ToolTip="Edit Task" 
                                        OnClientClick='<%# "openEditModal(" + Eval("ProductID") + ", \"" + Eval("ProductName") + "\", \"" + Eval("Price") + "\", \"" + Eval("Quantity") + "\", \"" + Eval("ProductImageURL") + "\"); return false;" %>'>
                                        <i class="fa fa-edit"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-link btn-danger" 
                                        CommandName="DeleteProduct" 
                                        CommandArgument='<%# Eval("ProductID") %>' 
                                        ToolTip="Remove">
                                        <i class="fa fa-times"></i>
                                    </asp:LinkButton>
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

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha384-k6RqeWeci5ZR/Lv4MR0sA0FfDOMjWSh2D8Oj/AnjU5OamAftPZyC6M+qFcO1yLq" crossorigin="anonymous">
<script>
    function showModal() {
        var myModal = new bootstrap.Modal(document.getElementById('addRowModal'));
        myModal.show();
    }

    function cancelFunction() {
        var myModal = new bootstrap.Modal(document.getElementById('addRowModal'));
        myModal.hide();
    }

    function openEditModal(productID, productName, price, quantity, imageURL) {
        // Set the values in the edit modal
        document.getElementById('<%= txtProductID.ClientID %>').value = productID;
        document.getElementById('<%= txtProductName.ClientID %>').value = productName;
        document.getElementById('<%= txtPrice.ClientID %>').value = price;
        document.getElementById('<%= txtQuantity.ClientID %>').value = quantity;
        document.getElementById('<%= txtProductImageURL.ClientID %>').value = imageURL;

        // Show the edit modal
        var editModal = new bootstrap.Modal(document.getElementById('editRowModal'));
        editModal.show();
    }
</script>

</asp:Content>
