<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="NewVersion.css.cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <asp:SqlDataSource ID="dsProduct" runat="server" ConnectionString="<%$ ConnectionStrings:productConnectionString %>" ProviderName="<%$ ConnectionStrings:productConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>

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

             <div style="position: relative; margin-bottom: 10px;">
                    <asp:Button ID="btnSelectAll" runat="server" Text="Select All" style="cursor:pointer; position: absolute; right: 80px; top: -25px;" />
                    <span id="trash-icon" style="display:none; cursor:pointer; position: absolute; right: 80px; top: 0px;" OnClick="deleteSelectedItems()">
                        <img src="images/delete.png" alt="Delete"/>
                    </span>
                 </div>

               <div class="site-blocks-table">
               <table class="table">
                 <thead style="position:sticky; top:0; background-color:white; z-index:1;">
                   <tr>
                     <th class="product-checkbox"></th>       
                     <th class="product-thumbnail">Image</th>
                     <th class="product-name">Product</th>
                     <th class="product-price">Price</th>
                     <th class="product-quantity">Quantity</th>
                     <th class="product-total">Total</th>
                   </tr>
                 </thead>

                 <tbody style="max-height:400px; overflow-y:auto;">  <!-- tbody can scroll -->
                         <asp:Repeater ID="rptProduct" runat="server" OnItemCommand="rptProduct_ItemCommand">
                             <ItemTemplate>
                                <tr>
                                <div class="product-item">
                                    <td class="select-item">
                                        <asp:CheckBox ID="chkSelectItem" runat="server" CssClass="select-checkbox" OnClick="toggleTrashIcon()" />
                                    </td>

                                    <td class="productImg" id="productImg"><img src='<%# Eval("ProductImageURL") %>' alt='<%# Eval("ProductName") %>' style="max-width: 200px; max-height:175px;"/></td>
                                    <td class="productName" id="productName">
                                        <%# Eval("ProductName") %><br />
                                        <%# Eval("ProductStorage") %><br />
                                        <%# Eval("ProductColor") %>
                                    </td>
                                    <td class="productPrice" id="productPrice"><%# Eval("Price", "{0:F2}") %></td>
                                    
                                    <td class="quantity-container" rowspan="3">
                                         <div class="quantity-controls">
                                             <asp:Button class="btn btn-outline-black decrease" ID="btnDecrease" runat="server" Text="-" />
        
                                             <asp:TextBox class="form-control text-center quantity-amount" ID="txtQuantity" runat="server" value="1" style="max-width: 120px; max-height:30px;"></asp:TextBox>
        
                                             <asp:Button class="btn btn-outline-black increase" ID="btnIncrease" runat="server" Text="+" />
                                        </div>
                                    </td>
                                    
                                    <td>
                                    <asp:Label ID="lblTotalPrice" runat="server"><%# Eval("TotalPrice","{0:F2}") %></asp:Label>
                                    </td>
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
                       <asp:Label class="text-black" ID="lblSubtotal" runat="server" Text="RM 1999.90"></asp:Label>
                     
                   </div>
                 </div>
                 <div class="row mb-5">
                   <div class="col-md-6">
                     <span class="text-black">Total</span>
                   </div>
                   <div class="col-md-6 text-right">
                     <asp:Label class="text-black" ID="lblTotal" runat="server" Text="RM 1999.90"></asp:Label>
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

        //show or hide Trash icon
        function toggleTrashIcon() {
            var isAnyChecked = false;

            var repeaters = document.querySelectorAll('input[type="checkbox"][id$="_chkSelectItem"]');

            console.log(checkbox.id + " checked: " + checkbox.checked);

            repeaters.forEach(function (checkbox) {
                if (checkbox.checked) {
                    isAnyChecked = true;
                }
            });

            var trashIcon = document.getElementById('trash-icon');

            // Show or hide trash icon based on checkbox status
            trashIcon.style.display = isAnyChecked ? 'inline' : 'none';

            // Debug information
            console.log("Is any checkbox checked: " + isAnyChecked);
        }

        //delete selected items
        function deleteSelectedItems() {
            var checkboxes = document.querySelectorAll('input[type="checkbox"][id$="_chkSelectItem"]');

            //traversal checkbox and delete the selected items
            checkboxes.forEach(checkbox => {
                if (checkbox.checked) {
                    var row = checkbox.closest('tr'); //find row of the item
                    if (row) {
                        console.log("Deleting row for: " + checkbox.id); // Debug information
                        row.parentNode.removeChild(row); // Delete that row
                    }
                }
            });

            //After deleted, hide the trush icon
            toggleTrashIcon();
        }

        document.getElementById('lblSelectAll').addEventListener('click', function () {
            var checkboxes = document.querySelectorAll('input[type="checkbox"][id$="_chkSelectItem"]');
            var isChecked = Array.from(checkboxes).every(checkbox => checkbox.checked);

            checkboxes.forEach(function (checkbox) {
                checkbox.checked = !isChecked; // 反转当前状态
            });

            toggleTrashIcon();
        });

    </script>

</asp:Content>
