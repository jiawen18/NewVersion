<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="transactions.aspx.cs" Inherits="NewVersion.admin.transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
            <asp:SqlDataSource 
    ID="SqlDataSource1" 
    runat="server" 
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    ProviderName="System.Data.SqlClient"
    SelectCommand="SELECT TransactionID, OrderID, InvoiceNumber, InvoiceDate FROM Transaction">
</asp:SqlDataSource>

    <div class="col-md-12">
        <div class="card">
            <div class="card-header">
                <div class="d-flex align-items-center">
                    <h4 class="card-title"></h4>
                        </div>
            </div>
            <div class="card-body">
                <!-- Modal -->
                <div class="modal fade" id="addRowModal" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header border-0">
                
            </div>
            <div class="modal-footer border-0">
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
                                <th>Invoice ID</th>
                                <th>Invoice Date</th>
                            </tr>
                        </thead>

                        <tbody id="orderTableBody">
                            <asp:Repeater ID="TransactionRepeater" runat="server" >
                                <ItemTemplate>
                            <tr>
                                <td><%# Eval("TransactionID") %></td>
                                <td><%# Eval("OrderID") %></td>
                                <td><%# Eval("InvoiceID") %></td>
                                <td><%# Eval("InvoiceDate") %></td>
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
