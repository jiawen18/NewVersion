<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="orderList.aspx.cs" Inherits="NewVersion.admin.orderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    
    <asp:SqlDataSource 
    ID="SqlDataSource1" 
    runat="server" 
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    ProviderName="System.Data.SqlClient"
    SelectCommand="SELECT OrderID, OrderDetails, PaymentStatus, DeliveryStatus ,OrderDate FROM Order">
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
                                <th>Order ID</th>
                                <th>Order Details</th>
                                <th>Payment Status</th>
                                <th>Delivery Status</th>
                                <th>Order Date</th>
                            </tr>
                        </thead>

                        <tbody id="orderTableBody">
                            <asp:Repeater ID="OrderRepeater" runat="server" >
                                <ItemTemplate>
                            <tr>
                                <td><%# Eval("OrderID") %></td>
                                <td><%# Eval("OrderDetails") %></td>
                                <td><%# Eval("PaymentStatus") %></td>
                                <td><%# Eval("DeliveryStatus") %></td>
                                <td><%# Eval("OrderDate") %></td>
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
