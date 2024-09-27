<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="transactions.aspx.cs" Inherits="NewVersion.admin.transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
            <asp:SqlDataSource 
    ID="SqlDataSource1" 
    runat="server" 
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    ProviderName="System.Data.SqlClient"
    SelectCommand="SELECT TransactionID, OrderID, InvoiceNumber, InvoiceDate,TransactionStatus FROM Transaction">
</asp:SqlDataSource>

    <div class="col-md-12">
            <div class="card">
<div class="card-header">
    <div class="d-flex align-items-center">
        <h4 class="card-title">List of Transaction</h4>
    </div>
    
</div>
<div class="card-body">
    
    <asp:Label ID="lblMessage" runat="server"></asp:Label>

             <!-- Edit Row Modal -->
         <div class="modal fade" id="addRowModal" tabindex="-1" role="dialog" aria-hidden="true">
             <div class="modal-dialog" role="document">
                 <div class="modal-content">
                     <div class="modal-header border-0">
                         <h5 class="modal-title">
                             <span class="fw-mediumbold">Edit</span>
                             <span class="fw-light">Transaction</span>
                         </h5>
                            <asp:Button ID="Button3" runat="server" CssClass="close" 
                            OnClientClick="return cancelFunction();" data-bs-dismiss="modal" />
                        <span aria-hidden="true" style="cursor: pointer;" onclick="cancelFunction(); $('#addRowModal').modal('hide');">&times;</span>
                     </div>
                     <div class="modal-body">
                         <p class="small">Edit the Transaction information</p>
                         <div>
                             <div class="row">
                                 <div class="col-sm-12">
                                     <div class="form-group form-group-default">
                                         <label>Transaction ID</label>
                                         <asp:HiddenField ID="txtTransactionID" runat="server"/>
                                     </div>
                                 </div>
                                 <div class="col-md-6 pe-0">
                                     <div class="form-group form-group-default">
                                         <label>Order ID</label>
                                        <asp:TextBox ID="txtOrderID" runat="server" CssClass="form-control" ReadOnly="true" />
                                     </div>
                                 </div>
                                 
                                 <div class="col-md-6">
                                <div class="form-group form-group-default">
                                    <label>Transaction Status</label>
                                    <asp:DropDownList ID="ddlTransactionStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Success">Success</asp:ListItem>
                                        <asp:ListItem Value="Cancel">Cancel</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                 <div class="col-md-6">
                                     <div class="form-group form-group-default">
                                         <label>Invoice ID</label>
                                         <asp:TextBox ID="txtInvoiceID" runat="server" CssClass="form-control" ReadOnly="true" />
                                     </div>
                                 </div>
                                 <div class="col-md-6">
                                <div class="form-group form-group-default">
                                    <label>Invoice Date</label>
                                    <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                            </div>
                                                     </div>
                </div>
            </div>
            <div class="modal-footer border-0">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdateTransaction_Click" />
                <asp:Button ID="Button2" runat="server" Text="Close" class="btn btn-danger" data-dismiss="modal" />
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
                                <th>Transaction ID</th>
                                <th>Order ID</th>
                                <th>Transaction Status</th>
                                <th>Invoice ID</th>
                                <th>Invoice Date</th>
                                <th>Action</th>
                            </tr>
                        </thead>

                        <tbody id="transactionTableBody">
                            <asp:Repeater ID="TransactionRepeater" runat="server" OnItemCommand="TransactionRepeater_ItemCommand">
                                <ItemTemplate>
                            <tr>
                                <td><%# Eval("TransactionID") %></td>
                                <td><%# Eval("OrderID") %></td>
                                <td><%# Eval("TransactionStatus") %></td>
                                <td><%# Eval("InvoiceID") %></td>
                                <td><%# Eval("InvoiceDate") %></td>
                                <td><asp:Button
                                    ID="EditTaskButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-primary"
                                    OnClientClick='<%# "populateModal(\"" + Eval("TransactionID") + "\", \"" + Eval("OrderID") + "\", \"" + Eval("TransactionStatus") + "\", \"" + Eval("InvoiceID") + "\", \"" + Eval("InvoiceDate") + "\"); return false;" %>'
                                    Text="Edit" />
                                <asp:Button
                                    ID="RemoveItemButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to remove this transaction ?');"
                                    OnClick="RemoveTransactionButton_Click"
                                    CommandArgument='<%# Eval("TransactionID") %>'
                                    Text="Remove" /></td>
                            </tr>
                          </ItemTemplate>
                          </asp:Repeater> 
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>

<script type="text/javascript">
    function populateModal(orderID, transactionID, invoiceID, invoiceDate, transactionStatus) {
        
        document.getElementById('<%= txtOrderID.ClientID %>').value = orderID;
        document.getElementById('<%= txtTransactionID.ClientID %>').value = transactionID;
        document.getElementById('<%= txtInvoiceID.ClientID %>').value = invoiceID;
        document.getElementById('<%= txtInvoiceDate.ClientID %>').value = invoiceDate;

        var ddlTransactionStatus = document.getElementById('<%= ddlTransactionStatus.ClientID %>');
        ddlTransactionStatus.value = transactionStatus;
        
        ShowEditModal();
    }

    function ShowEditModal() {
        $('#addRowModal').modal('show');
    }
</script>

    </asp:Content>
