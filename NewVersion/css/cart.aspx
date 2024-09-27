﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="NewVersion.css.cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <asp:SqlDataSource ID="dsProduct" runat="server" ConnectionString="<%$ ConnectionStrings:productConnectionString %>" ProviderName="<%$ ConnectionStrings:productConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>

    <style>
        .quantity-controls {
            display: flex;
            align-items: center;
            justify-content: space-between;
        }

        .quantity-amount {
            text-align: center;
            width: 40px;
        }

        .toggle-table {
            display: none; /* Initially hide */
        }
    </style>

<!-- Start Hero Section -->
<div class="hero">
	<div class="container">
        <div class="row justify-content-between">
	        <div class="col-lg-5">
		        <div class="intro-excerpt">
			        <h1>Cart</h1>
		        </div>
	        </div>
            <div class="col-lg-7">
				
            </div>
        </div>
    	</div>
    </div>
<!-- End Hero Section -->


<div class="untree_co-section before-footer-section">
   <div class="container">
     <div class="row mb-5">
           <div class="col-md-12" method="post">

               <div class="site-blocks-table">
                <div style="position: relative; margin-bottom: 10px;">
                     <span id="trash-icon" style="display:none; cursor:pointer; position: absolute; right: 80px; top: 0px;" OnClick="btnDeleteSelected_Click">
                        <img src="images/delete.png" alt="Delete" />
                    </span>

                 </div>
                   
                   <table class="table">
                 <thead style="position:sticky; top:0; background-color:white; z-index:1;">
                   <tr>
                     <th class="product-checkbox"><asp:CheckBox ID="chkSelectAll" runat="server" alt="Select All" OnClick="SelectAllCheckBoxes(this);" /></th>       
                     <th class="product-thumbnail">Image</th>
                     <th class="product-name">Product</th>
                     <th class="product-price">Price</th>
                     <th class="product-quantity">Quantity</th>
                     <th class="product-total">Total Price</th>
                   </tr>
                 </thead>

                 <tbody style="max-height:400px; overflow-y:auto;">  <!-- tbody can scroll -->
                         <asp:Repeater ID="rptProduct" runat="server"  >
                             <ItemTemplate>
                                <tr>
                                    <td class="select-item">
                                        <asp:CheckBox ID="chkSelect" runat="server" OnClick="toggleTrashIcon();"/>
                                    </td>

                                    <td class="productImg" id="productImg"><img src='<%# Eval("ProductImage") %>' alt='<%# Eval("ProductName") %>' style="max-width: 200px; max-height:175px;"/></td>
                                    <td class="productName" id="productName">
                                        <%# Eval("ProductName") %><br />
                                        <%# Eval("StorageOption") %><br />
                                        <%# Eval("ColorOption") %>
                                    </td>
                                    <td class="productPrice" id="productPrice"><%# Eval("Price", "{0:F2}") %></td>
                                    
                                    <td class="quantity-container">
                                         <div class="quantity-controls">
                                             <asp:Button class="btn btn-outline-black decrease" ID="btnDecrease" runat="server" Text="-" CommandArgument='<%# Eval("ProductID") %>' OnClick="btnDecrease_Click"/>
        
                                             <asp:TextBox class="form-control text-center quantity-amount" ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" style="max-width: 120px; max-height:30px;"></asp:TextBox>
        
                                             <asp:Button class="btn btn-outline-black increase" ID="btnIncrease" runat="server" Text="+" CommandArgument='<%# Eval("ProductID") %>' OnClick="btnIncrease_Click"/>
                                        </div>
                                    </td>
                                    
                                    <td>
                                    <asp:Label ID="lblTotalPrice" runat="server"><%# "RM " + (Convert.ToDecimal(Eval("Price")) * Convert.ToDecimal(Eval("Quantity"))).ToString("N2") %></asp:Label>
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
    </div>

                       
   
<div class="row">
           <div class="col-md-6">
             <div class="row mb-5">
               <div class="continueShopping">
                   <asp:Button class="btn btn-outline-black btn-sm btn-block" ID="btnContinue" runat="server" Text="Continue Shopping" OnClick="btnContinue_Click"  />
               </div>
             </div>
           </div>
           <div class="col-md-6 pl-5">
             <div class="row justify-content-end">
               <div class="checkOut">
                 <div class="row">
                   <div class="col-md-12 text-right border-bottom mb-5">
                     <h3 class="text-black h4 text-uppercase">Cart Totals</h3>
                   </div>
                 </div>
                 <div class="row mb-3">
                   <div class="col-md-6">
                     <span class="text-black">Subtotal</span>
                   </div>
                   <div class="col-md-6 text-right">
                       RM <asp:Label class="text-black" ID="lblSubtotal" runat="server" ></asp:Label>
                     
                   </div>
                 </div>
                 <div class="row mb-5">
                   <div class="col-md-6">
                     <span class="text-black">Total</span>
                   </div>
                   <div class="col-md-6 text-right">
                     RM <asp:Label class="text-black" ID="lblTotal" runat="server" ></asp:Label>
                   </div>
                 </div>
   
                 <div class="row">
                   <div class="col-md-12">
                       <asp:Button class="btn btn-black btn-lg py-3 btn-block" ID="btnCheckOut" runat="server" Text="Proceed To Checkout" OnClick="btnCheckOut_Click" />
                   </div>
                 </div>
               </div>
             </div>
           </div>
         </div>

    
        <script type="text/javascript">
        // Show or hide trash icon
            function toggleTrashIcon() {
            var checkboxes = document.querySelectorAll('input[type="checkbox"][id$="_chkSelect"]');
            var trashIcon = document.getElementById('trash-icon');
            var isAnyChecked = Array.from(checkboxes).some(checkbox => checkbox.checked);
            trashIcon.style.display = isAnyChecked ? 'inline' : 'none';
        }

        // Handle checkbox change
        document.querySelectorAll('input[type="checkbox"][id$="_chkSelect"]').forEach(checkbox => {
                checkbox.addEventListener('change', toggleTrashIcon);
        });
    </script>

</asp:Content>
