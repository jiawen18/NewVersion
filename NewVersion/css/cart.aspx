<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="NewVersion.css.cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:SqlDataSource ID="dsProduct" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>

<!-- Start Hero Section -->
<div class="hero">
	<div class="container">
        <div class="row justify-content-between">
	        <div class="col-lg-5">
		        <div class="intro-excerpt">
			        <h1>Cart s</h1>
		        </div>
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
               <table class="table">
                 <tbody>
                         <asp:Repeater ID="rptProduct" runat="server">
                             <HeaderTemplate>
                                 <tr style="border-bottom: 1px solid black;">
                                     <th class="product-thumbnail">Image</th>
                                     <th class="product-name">Product</th>
                                     <th class="product-price">Price</th>
                                     <th class="product-quantity">Quantity</th>
                                     <th class="product-total">Total</th>
                                 </tr>
                             </HeaderTemplate>
                             <ItemTemplate>
                                <tr>
                                <div class="product-item">
                                    <td class="image1" id="image1"><img src='<%# Eval("ProductImageURL") %>' alt='<%# Eval("Name") %>' style="max-width: 200px; max-height:175px;"/></td>
                                    <td class="productName" id="productName"><%# Eval("Name") %></td>
                                    <td class="productPrice" id="productPrice"><%# Eval("Price", "{0:F2}") %></td>
                                    <td class="quantity-container">
                                        <asp:Button class="btn btn-outline-black decrease" ID="btnDecrease" runat="server" Text="-" />

                                        <asp:TextBox class="form-control text-center quantity-amount" ID="txtQuantity" runat="server" value="1" style="max-width: 120px; max-height:30px;"></asp:TextBox>

                                        <asp:Button class="btn btn-outline-black increase" ID="btnIncrease" runat="server" Text="+" />
                                    </td>
                                    <td class="TotalPrice" id="TotalPrice"> <%# Eval("TotalPrice","{0:F2}") %></td>
                                </div>
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
                   <asp:Button class="btn btn-outline-black btn-sm btn-block" ID="btnContinue" runat="server" Text="Continue Shopping" OnClick="btnContinue_Click" />
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
                     <strong class="text-black">$230.00</strong>
                   </div>
                 </div>
                 <div class="row mb-5">
                   <div class="col-md-6">
                     <span class="text-black">Total</span>
                   </div>
                   <div class="col-md-6 text-right">
                     <strong class="text-black">$230.00</strong>
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
</asp:Content>
