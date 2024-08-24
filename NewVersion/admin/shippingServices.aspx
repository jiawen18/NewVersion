<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="shippingServices.aspx.cs" Inherits="NewVersion.admin.shippingServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <div class="card">
        <div class="card-header">
            <div class="d-flex align-items-center">
                <h4 class="card-title">Add Row</h4>
                <asp:LinkButton
                    ID="AddRowButton"
                    runat="server"
                    CssClass="btn btn-primary btn-round ms-auto"
                    OnClientClick="return false;"
                    data-bs-toggle="modal"
                    data-bs-target="#addRowModal">
                    <i class="fa fa-plus"></i>
                    Add Row
                </asp:LinkButton>

            </div>
        </div>
        <div class="card-body">
            <!-- Modal -->
            <div
                class="modal fade"
                id="addRowModal"
                tabindex="-1"
                role="dialog"
                aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header border-0">
                            <h5 class="modal-title">
                                <span class="fw-mediumbold">New</span>
                                <span class="fw-light">Row </span>
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
                            <p class="small">
                                Create a new row using this form, make sure you fill them all
                            </p>
                            <div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group form-group-default">
                                            <label>Name</label>
                                            <input
                                                id="addName"
                                                type="text"
                                                class="form-control"
                                                placeholder="fill name" />
                                        </div>
                                    </div>
                                    <div class="col-md-6 pe-0">
                                        <div class="form-group form-group-default">
                                            <label>Position</label>
                                            <input
                                                id="addPosition"
                                                type="text"
                                                class="form-control"
                                                placeholder="fill position" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-group-default">
                                            <label>Office</label>
                                            <input
                                                id="addOffice"
                                                type="text"
                                                class="form-control"
                                                placeholder="fill office" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer border-0">
                            <asp:Button
                                ID="Button1"
                                runat="server"
                                CssClass="btn btn-primary"
                                Text="Add"
                                OnClick="AddRowButton_Click" />
                            <!-- Server-side event handler -->
                            <asp:LinkButton
                                ID="LinkButton1"
                                runat="server"
                                CssClass="btn btn-danger"
                                OnClientClick="$('#addRowModal').modal('hide'); return false;">
                                Close
                            </asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table class="display table table-striped table-hover">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Price</th>
                                    <th>Quantity</th>
                                    <th colspan="2" style="width: 10%; text-align: center;">Actions</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("Price") %></td>
                            <td><%# Eval("Quantity") %></td>
                            <td style="display: flex;">
                                <asp:LinkButton
                                    ID="CopyItemButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-black"
                                    Style="opacity: 0.5;"
                                    data-bs-toggle="tooltip"
                                    title="Copy"
                                    data-original-title="Copy Item"
                                    OnClick="CopyItemButton_Click">
                                    <i class="fa fa-copy"></i>
                                </asp:LinkButton>
                                <asp:LinkButton
                                    ID="EditTaskButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-primary"
                                    data-bs-toggle="tooltip"
                                    title="Edit"
                                    data-original-title="Edit Task"
                                    OnClick="EditTaskButton_Click">
                                    <i class="fa fa-edit"></i>
                                </asp:LinkButton>
                                <asp:LinkButton
                                    ID="RemoveItemButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-danger"
                                    data-bs-toggle="tooltip"
                                    title="Remove"
                                    data-original-title="Remove"
                                    OnClick="RemoveItemButton_Click">
                                    <i class="fa fa-trash"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>


</asp:Content>
