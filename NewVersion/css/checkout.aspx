<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="NewVersion.css.checkout"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Start Hero Section -->
    <div class="hero">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-5">
                    <div class="intro-excerpt">
                        <h1>Checkout</h1>
                    </div>
                </div>
                <div class="col-lg-7">
                </div>
            </div>
        </div>
    </div>
    <!-- End Hero Section -->

    <div class="untree_co-section">
        <div class="container">
            <div class="deliveryDetails">
                <h2>
                    <asp:Label ID="lblTitle" runat="server" Text="Delivery Details">
                <strong>Delivery Details</strong>
                    </asp:Label>
                </h2>
                <br />
            </div>
            <div>
                <p>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                        <asp:Label ID="lblNameAndPhone" runat="server">Tang Yan Chun | 014 899 5230</asp:Label>
                <br />
                        <asp:Label ID="lblCurrentAddress" runat="server">77, Lorong Lembah Permai 3,
                <br />
                        11200 Tanjung Bungah, Pulau Pinang</asp:Label>
                    </asp:PlaceHolder>
                </p>
            </div>
            <br />
            <div>
                <!-- Button to trigger modal -->
                <asp:Button ID="btnShowModal" runat="server" Text="Edit >" CssClass="btn btn-primary" OnClientClick="$('#myModal').modal('show'); return false;" />
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" >
        <div class="modal-dialog" role="document" style="max-width: 1000px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Delivery Details</h5>
                    <asp:Button ID="btnClose" class="close" data-dismiss="modal" aria-label="Close" aria-hidden="true" runat="server" Text="&times;" CausesValidation="false" OnClientCloseClick="$('#myModal').modal('hide'); return false;" />
                </div>

                <div class="modal-body" style="position: center; transform: translateX(25%);">
                    <!-- Content inside modal -->
                    <div class="row">
                        <div class="col-md-6 mb-5 mb-md-0">
                            <div class="py-2">
                                
                                <div class="form-group row">
                                    <div class="col-md-6">
                                        <asp:Label ID="lblFirstName" class="text-black" runat="server">
                                             First Name <span class="text-danger">*</span>
                                        </asp:Label>
                                        <asp:TextBox ID="c_diff_fname" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName" runat="server" ErrorMessage="Please enter [First Name]." ControlToValidate="c_diff_fname" CssClass="error" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-md-6">
                                        <asp:Label ID="lblLastName" class="text-black" runat="server">
                                              Last Name <span class="text-danger">*</span>
                                        </asp:Label>
                                        <asp:TextBox ID="c_diff_lname" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName" runat="server" ErrorMessage="Please enter [Last Name]." ControlToValidate="c_diff_lname" CssClass="error" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group row  mb-3">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblAddress" class="text-black" runat="server">
                                                Address <span class="text-danger">*</span>
                                        </asp:Label>
                                        <asp:TextBox ID="c_diff_address" class="form-control" placeholder="Street address" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress" runat="server" ErrorMessage="Please enter [Address]." ControlToValidate="c_diff_address" CssClass="error" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddress" runat="server" ControlToValidate="c_diff_address" CssClass="error" Display="Dynamic" 
                                         ForeColor="Red" ErrorMessage="Invalid [Address] format. Only accept (, . - /) symbol, letters and digits. Please enter a valid [Address]." ValidationExpression="^[a-zA-Z0-9\s,.-/]+$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:TextBox ID="txtUnit" class="form-control" runat="server" placeholder="Apartment, suite, unit etc. (optional)"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblCountry" class="text-black" runat="server">
                                        Country 
                                    </asp:Label>
                                    <asp:DropDownList ID="c_diff_country" class="form-control" runat="server" >
                                        <asp:ListItem Text="-- Select a country --" Value="1" Selected="True" Disabled="True"></asp:ListItem>
                                        <asp:ListItem Text="Malaysia" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="China" Value="3"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                                <div class="form-group row">
                                    <asp:Label ID="lblCity" class="text-black" runat="server">
                                        City
                                    </asp:Label>
                                    <asp:DropDownList ID="c_diff_city" class="form-control" runat="server" >
                                        <asp:ListItem Text="-- Select a city --" Value="1" Selected="True" Disabled="True" ></asp:ListItem>
                                        <asp:ListItem Text="Penang" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Kuala Lumpur" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="BeiJing" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Shanghai" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group row mb-5">
                                    <asp:Label ID="lblPhone" class="text-black" runat="server">
                                        Phone <span class="text-danger">*</span>
                                    </asp:Label>
                                    <asp:TextBox ID="c_diff_phone" class="form-control" placeholder="For example: 012-3456-7890" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server" ErrorMessage="Please enter [Phone Number]." ControlToValidate="c_diff_phone" CssClass="error" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone" runat="server" ControlToValidate="c_diff_phone" CssClass="error" Display="Dynamic" 
                                        ForeColor="Red" ErrorMessage="Invalid [Phone Number] format. Please enter a valid [Phone Number]. For example: 0123567890." ValidationExpression="^\d{10,11}$"></asp:RegularExpressionValidator>
                                </div>

                                <div class="form-group">
                                    <label for="c_order_notes" class="text-black">Order Notes</label>
                                    <textarea name="c_order_notes" id="c_order_notes" cols="30" rows="5" class="form-control" placeholder="Write your notes here..."></textarea>
                                </div>

                                <div class="modal-footer">
                                    <asp:Button ID="btnCloseDialog" CssClass="btn btn-secondary" data-dismiss="modal" runat="server" Text="Close" OnClick="btnCloseDialog_Click" CausesValidation="false"/>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" CausesValidation="true"/>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>



    <div class="col-md-6">

        <div class="row mb-5">
            <div class="col-md-12" style="transform: translate(80%,-45%)">
                <h2 class="h3 mb-3 text-black">Your Order</h2>
                <div class="p-3 p-lg-5 border bg-white">
                    <table class="table site-block-order-table mb-5">
                        <thead>
                            <tr>
                            <th>Product</th>
                            <th>Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="phCartItems" runat="server">
                            
                            </asp:PlaceHolder>

                            <tr>
                                <td class="text-black font-weight-bold"><strong>Cart Subtotal</strong></td>
                                <td class="text-black"><asp:Label ID="lblCartSubTotal" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="text-black font-weight-bold"><strong>Delivery Fee</strong></td>
                                <td class="text-black"><asp:Label ID="lblDeliveryFee" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="text-black font-weight-bold"><strong>Order Total</strong></td>
                                <td class="text-black font-weight-bold">
                                    <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>

                    <div class="group-paymentmethod">
                         <div class="form-group">
                            <asp:Button class="btn btn-black btn-lg py-3 btn-block" ID="btnPay" runat="server" Text="Place Order" OnClick="btnPay_Click1" CausesValidation="false"/>
                        </div>
                    </div>
            </div>
        </div>
     </div>
</div>
    <!-- </form> -->

    <script src ="https://checkout.razorpay.com/v1/checkout.js"></script>
    <script>

        function OpenPaymentWindow(key, currency, amountInSubunits, descritpion, imageLogo, orderId, notes) {
            notes = $.parseJSON(notes);
            var options = {
                "key": key, // Enter the Key ID generated from the Dashboard
                "currency": currency,
                "amount": amountInSubunits,
                "description": descritpion,
                "image": imageLogo,
                "order_id": orderId, //This is a sample Order ID. Pass the id obtained in the response of Step 1
                "handler": function (response) {
                    var orderId = response.razorpay_order_id;
                    var transactionId = response.razorpay_payment_id;
                    
                    window.location.href = "SuccessPage.aspx?orderId=" + orderId + "&TransactionId=" + transactionId;
                    UpdateOrderStatus(orderId, "Shipping", "Success");
                    //alert(response.razorpay_payment_id);
                    //alert(response.razorpay_order_id);
                    //alert(response.razorpay_signature)
                },
                "notes": notes,
                "theme": {
                    "color": "#528FF0"
                }
            };
            var rzp1 = new Razorpay(options);
            rzp1.open();
            rzp1.on('payment.failed', function (response) {
                var transactionId = "Fail_" + new Date().getTime();
                window.location.href = "FailurePage.aspx?TransactionId=" + transactionId;
                UpdateOrderStatus("", "", "Failed");
            });
        }
    </script>

        
</asp:Content>
