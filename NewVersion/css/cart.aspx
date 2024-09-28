<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="NewVersion.css.cart" %>
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

        
        /* Optional styling for the table and scrolling effect */
        table {
            width: 100%;
            border-collapse: collapse;
        }

        tbody {
            display: block; /* 将 tbody 设为块元素 */
            max-height: 400px; /* 设置最大高度 */
            overflow-y: auto; /* 启用垂直滚动条 */
            width: 100%;
        }

        tr {
            display: table; /* 每行作为表格行显示 */
            table-layout: fixed; /* 确保列宽度固定 */
            width: 100%; /* 确保行宽为100% */
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
                <div style="position: relative; margin-bottom: 50px;">
                     <span id="trash-icon"  class="trash-icon" style="position: absolute; right: 20px; top: 0px; display: none;" onclick="deleteSelectedItems();">
                         <img src="images/delete.png" alt="Delete" style="max-width :30px;max-height :30px;" />                 
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

                 <tbody style="display:block; max-height:400px; overflow-y:auto;">  <!-- tbody can scroll -->
                         <asp:Repeater ID="rptProduct" runat="server" >
                             <ItemTemplate>
                                <tr>
                                    <td class="select-item">
                                        <asp:CheckBox ID="chkSelect" runat="server" OnClick="toggleTrashIcon();" AutoPostBack="false" />
                                    </td>

                                    <td class="productImg" id="productImg"><img src='<%# Eval("ProductImage") %>' alt='<%# Eval("ProductName") %>' style="max-width: 200px; max-height:175px;"/></td>
                                    <td class="productName" id="productName">
                                        <%# Eval("ProductName") %><br />
                                        <%# Eval("StorageOption") %><br />
                                        <%# Eval("ColorOption") %>
                                    </td>
                                    <td class="productPrice" id="productPrice"><%# Eval("Price", "{0:N2}") %></td>
                                    
                                    <td class="quantity-container">
                                         <div class="quantity-controls">
                                             <asp:Button class="btn btn-outline-black decrease" ID="btnDecrease" runat="server" Text="-" CommandArgument='<%# Eval("ProductID") + "|" + Eval("StorageOption") + "|" + Eval("ColorOption") %>'  OnClick="btnDecrease_Click"/>
        
                                             <asp:TextBox class="form-control text-center quantity-amount" ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" style="max-width: 120px; max-height:30px;"></asp:TextBox>
        
                                             <asp:Button class="btn btn-outline-black increase" ID="btnIncrease" runat="server" Text="+" CommandArgument='<%# Eval("ProductID") + "|" + Eval("StorageOption") + "|" + Eval("ColorOption") %>'  OnClick="btnIncrease_Click"/>
                                        </div>
                                    </td>
                                    
                                    <td>
                                    <asp:Label ID="lblTotalPrice" runat="server"><%# (Convert.ToDecimal(Eval("Price")) * Convert.ToDecimal(Eval("Quantity"))).ToString("N2") %></asp:Label>
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
                var checkboxes = document.querySelectorAll('input[id*="_chkSelect"]');
                var trashIcon = document.getElementById('trash-icon');
                var isAnyChecked = Array.from(checkboxes).some(checkbox => checkbox.checked);
                trashIcon.style.display = isAnyChecked ? 'inline' : 'none';
            }

            // Function to select or deselect all checkboxes
            function SelectAllCheckBoxes(selectAllCheckbox) {
                var checkboxes = document.querySelectorAll('input[id*="_chkSelect"]');
                checkboxes.forEach(function (checkbox) {
                    checkbox.checked = selectAllCheckbox.checked;
                });
                toggleTrashIcon();
            }

            // Function to trigger delete operation for selected items
            function deleteSelectedItems() {
                var confirmed = confirm("Are you sure you want to delete the selected items?");
                if (confirmed) {
                    __doPostBack('<%= btnDeleteSelected.UniqueID %>', ''); // Trigger server-side event
            }
            }

        </script>

    <!-- Hidden button to handle server-side delete -->
    <asp:Button ID="btnDeleteSelected" runat="server" Style="display:none;" OnClick="btnDeleteSelected_Click" />

</asp:Content>
