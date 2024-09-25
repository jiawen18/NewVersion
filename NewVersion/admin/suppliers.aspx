<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="suppliers.aspx.cs" Inherits="NewVersion.admin.suppliers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <div class="col-md-12">
        <div class="card">
            <div class="card-header">
                <div class="d-flex align-items-center">
                    <h4 class="card-title">Add Supplier</h4>
                    <button
                        class="btn btn-primary btn-round ms-auto"
                        data-bs-toggle="modal"
                        data-bs-target="#addRowModal">
                        <i class="fa fa-plus"></i>
                        Add Supplier
                    </button>
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
                                    <span class="fw-light">Supplier</span>
                                </h5>
                                <button
                                    type="button"
                                    class="close"
                                    data-dismiss="modal"
                                    aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p class="small">
                                    Create a new supplier using this form, make sure you fill them all.
                                </p>
                                <div>
                                    <div class="row">
                                        <div class="col-md-6 pe-0">
                                            <div class="form-group form-group-default">
                                                <label>Branch</label>
                                                <input
                                                    id="addBranch"
                                                    type="text"
                                                    class="form-control"
                                                    placeholder="Fill branch" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group form-group-default">
                                                <label>Email</label>
                                                <input
                                                    id="addEmail"
                                                    type="email"
                                                    class="form-control"
                                                    placeholder="Fill email" />
                                            </div>
                                        </div>
                                        <div class="col-md-6 pe-0">
                                            <div class="form-group form-group-default">
                                                <label>Phone Number</label>
                                                <input
                                                    id="addPhone"
                                                    type="text"
                                                    class="form-control"
                                                    placeholder="Fill phone number" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group form-group-default">
                                                <label>Address</label>
                                                <input
                                                    id="addAddress"
                                                    type="text"
                                                    class="form-control"
                                                    placeholder="Fill address" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer border-0">
                                <button
                                    type="button"
                                    id="addRowButton"
                                    class="btn btn-primary"
                                    onclick="addSupplier()">
                                    Add
                                </button>
                                <button
                                    type="button"
                                    class="btn btn-danger"
                                    data-dismiss="modal">
                                    Close
                                </button>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvSuppliers" runat="server" CssClass="display table table-striped table-hover" AutoGenerateColumns="False" OnRowCommand="gvSuppliers_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="SupplierBranch" HeaderText="Branch" />
                            <asp:BoundField DataField="SupplierEmail" HeaderText="Email" />
                            <asp:BoundField DataField="SupplierPhone" HeaderText="Phone Number" />
                            <asp:BoundField DataField="SupplierAddress" HeaderText="Address" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <div class="form-button-action">
                                        <button type="button" data-bs-toggle="tooltip" title="" class="btn btn-link btn-primary btn-lg" data-original-title="Edit Supplier" CommandName="Edit" CommandArgument='<%# Eval("SupplierID") %>'>
                                            <i class="fa fa-edit"></i>
                                        </button>
                                        <button type="button" data-bs-toggle="tooltip" title="" class="btn btn-link btn-danger" data-original-title="Remove" CommandName="Delete" CommandArgument='<%# Eval("SupplierID") %>'>
                                            <i class="fa fa-times"></i>
                                        </button>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
