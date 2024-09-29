<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="orderList.aspx.cs" Inherits="NewVersion.admin.orderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    
    <asp:SqlDataSource 
    ID="SqlDataSource1" 
    runat="server" 
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    ProviderName="System.Data.SqlClient"
    SelectCommand="SELECT OrderID, ProductID,OrderDetails, PaymentStatus, DeliveryStatus ,OrderDate FROM Order">
</asp:SqlDataSource>

         <div class="card">
     <div class="card-header">
         <div class="d-flex align-items-center">
             <h4 class="card-title">List of Order</h4>
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
                             <span class="fw-light">Order</span>
                         </h5>
                            <asp:Button ID="Button3" runat="server" CssClass="close" 
                            OnClientClick="return cancelFunction();" data-bs-dismiss="modal" />
                        <span aria-hidden="true" style="cursor: pointer;" onclick="cancelFunction(); $('#addRowModal').modal('hide');">&times;</span>
                     </div>
                     <div class="modal-body">
                         <p class="small">Edit the Order information</p>
                         <div>
                             <div class="row">
                                 <div class="col-sm-12">
                                     <div class="form-group form-group-default">
                                         <label>Order ID</label>
                                         <asp:HiddenField ID="txtOrderID" runat="server"/>
                                     </div>
                                 </div>
                                  <div class="col-md-6 pe-0">
                                    <div class="form-group form-group-default">
                                        <label>User Details</label>
                                        <asp:TextBox ID="txtUserDetails" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>
                                 <div class="col-md-6 pe-0">
                                    <div class="form-group form-group-default">
                                        <label>Delivery Fee</label>
                                        <asp:TextBox ID="txtDeliveryFee" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>
                                 <div class="col-md-6 pe-0">
                                     <div class="form-group form-group-default">
                                         <label>Total Price</label>
                                         <asp:TextBox ID="txtTotalPrice" runat="server" CssClass="form-control" ReadOnly="true" />
                                     </div>
                                 </div>
                                 <div class="col-md-6">
                                     <div class="form-group form-group-default">
                                         <label>Transaction Status</label>
                                         <asp:TextBox ID="txtTransactionStatus" runat="server" CssClass="form-control" ReadOnly="true" />
                                     </div>
                                 </div>
                                 <div class="col-md-6">
                                <div class="form-group form-group-default">
                                    <label>Delivery Status</label>
                                    <asp:DropDownList ID="ddlDeliveryStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                        <asp:ListItem Value="Shipping">Shipping</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                 <div class="col-md-6">
                                <div class="form-group form-group-default">
                                    <label>Order Date</label>
                                    <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                            </div>
                                                     </div>
                </div>
            </div>
            <div class="modal-footer border-0">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdateOrder_Click" />
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
                                <th>Order ID</th>
                                <th>User Details</th>
                                <th>Delivery Fee</th>
                                <th>Total Price</th>
                                <th>Transaction Status</th>
                                <th>Delivery Status</th>
                                <th>Order Date</th>
                                <th>Actions</th>
                            </tr>
                        </thead>

                        <tbody id="orderTableBody">
                            <asp:Repeater ID="OrderRepeater" runat="server" OnItemCommand="OrderRepeater_ItemCommand" >
                                <ItemTemplate>
                            <tr>
                                <td><%# Eval("OrderID") %></td>
                                <td><%# Eval("UserDetails") %></td>
                                <td><%# Eval("DeliveryFee") %></td>
                                <td><%# Eval("TotalPrice") %></td>
                                <td><%# Eval("TransactionStatus") %></td>
                                <td><%# Eval("DeliveryStatus") %></td>
                                <td><%# Eval("OrderDate") %></td>
                                <td><asp:Button
                                    ID="EditTaskButton"
                                    runat="server"
                                    CommandName="EditOrder"
                                    CommandArgument='<%# Eval("OrderID") %>'
                                    CssClass="btn btn-link btn-primary"
                                    OnClientClick='<%# "populateModal(\"" + Eval("OrderID") + "\",\"" + Eval("UserDetails") + "\",\"" + Eval("DeliveryFee") + "\", \"" + Eval("TotalPrice") + "\", \"" + Eval("TransactionStatus") + "\", \"" + Eval("DeliveryStatus") + "\", \"" + Eval("OrderDate") + "\"); return false;" %>'
                                    Text="Edit" />
                                <asp:Button
                                    ID="RemoveItemButton"
                                    runat="server"
                                    CommandName="DeleteOrder"
                                    CssClass="btn btn-link btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to remove this order ?');"
                                    OnClick="RemoveOrderButton_Click"
                                    CommandArgument='<%# Eval("OrderID") %>'
                                    Text="Remove" /></td>
                            </tr>
                          </ItemTemplate>
                          </asp:Repeater> 
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>

    <script type="text/javascript">
        function populateModal(orderID, deliveryFee, transactionStatus, totalPrice, deliveryStatus, orderDate, serDetails) {
            
            document.getElementById('<%= txtOrderID.ClientID %>').value = orderID;
            document.getElementById('<%= txtUserDetails.ClientID %>').value = userDetails;
            document.getElementById('<%= txtTotalPrice.ClientID %>').value = totalPrice;
            document.getElementById('<%= txtTransactionStatus.ClientID %>').value = transactionStatus;
            document.getElementById('<%= txtOrderDate.ClientID %>').value = orderDate;
            document.getElementById('<%= txtDeliveryFee.ClientID %>').value = deliveryFee;


            var ddlDeliveryStatus = document.getElementById('<%= ddlDeliveryStatus.ClientID %>');
            ddlDeliveryStatus.value = deliveryStatus;
            
            ShowEditModal();
        }

        function ShowEditModal() {
            $('#addRowModal').modal('show');
        }
    </script>

    
</asp:Content>
