<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Smartphones.aspx.cs" Inherits="NewVersion.css.Smartphones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:SqlDataSource 
    ID="SqlDataSource1" 
    runat="server" 
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    ProviderName="System.Data.SqlClient"
    SelectCommand="SELECT P.ProductID, P.ProductName, P.ProductImageURL, P.Price, P.Quantity, P.IsVisible, 
                          R.ReviewID, R.ReviewDate, R.ReviewRating, R.ReviewDescription
                   FROM Product P
                   LEFT JOIN Review R ON P.ProductID = R.ProductID
                   WHERE P.IsVisible = 1">
</asp:SqlDataSource>

    <!-- Start Hero Section -->
<div class="hero">
	<div class="container">
		<div class="row justify-content-between">
				<div class="col-lg-5">
					<div class="intro-excerpt">
						<h1>Smartphones</h1>
					</div>
				</div>
				<div class="col-lg-7">
					
				</div>
		</div>
	</div>
</div>
<!-- End Hero Section -->

<br />
<!-- Start Section Title -->
<div class="section-title-container">
    <h2>Galaxy Z flip Series</h2>
</div>
<!-- End Section Title -->


<!-- Start Products Row -->
<div class="products-row row">
    <!-- Dynamic product listing using Repeater -->
    <asp:Repeater ID="rptProducts" runat="server" >
    <ItemTemplate>
        <div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">
            <img src='<%# Eval("ProductImageURL") %>' class="img-fluid product-thumbnail" alt='<%# Eval("ProductName") %>' />
            <h3 class="product-title"><%# Eval("ProductName") %></h3>
            <strong class="product-price">RM <%# Eval("Price", "{0:F2}") %></strong>

            <!-- Review Section -->
            <asp:Repeater ID="rptReviews" runat="server">
            <ItemTemplate>
                <div class="review-section">
                    <h5>Review by Customer</h5>
                    <p><%# Eval("ReviewDescription") %></p>
                    <strong>Rating: <%# Eval("ReviewRating") %> / 5</strong>
                    <br />
                    <em>Reviewed on: <%# Eval("ReviewDate", "{0:dd MMM yyyy}") %></em>
                </div>
            </ItemTemplate>
            </asp:Repeater>
            <!-- End Review Section -->

            <div class="buyNow">
                <asp:Button ID="btnBuyNow" runat="server" Text="Buy Now" CssClass="buyNow-btn" CommandArgument='<%# Eval("ProductID") %>' OnClick="btnBuyNow_Click" />
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
</div>
<!-- End Products Row -->

<br />
<!-- Start Section Title -->
<div class="section-title-container">
    <h2>Galaxy S Series</h2>
</div>
<!-- End Section Title -->

<!-- Start Products Row -->
<div class="products-row row">
<!-- Start Grey Container -->
<!-- Start Products Row -->
<div class="products-row row">
    <!-- Dynamic product listing using Repeater -->
    <asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">
            <img src='<%# Eval("ProductImageURL") %>' class="img-fluid product-thumbnail" alt='<%# Eval("ProductName") %>' />
            <h3 class="product-title"><%# Eval("ProductName") %></h3>
            <strong class="product-price">RM <%# Eval("Price", "{0:F2}") %></strong>

           <!-- Review Section -->
            <asp:Repeater ID="rptReviews" runat="server">
            <ItemTemplate>
                <div class="review-section">
                    <h5>Review by Customer</h5>
                    <p><%# Eval("ReviewDescription") %></p>
                    <strong>Rating: <%# Eval("ReviewRating") %> / 5</strong>
                    <br />
                    <em>Reviewed on: <%# Eval("ReviewDate", "{0:dd MMM yyyy}") %></em>
                </div>
            </ItemTemplate>
            </asp:Repeater>
            <!-- End Review Section -->

            <div class="buyNow">
                <asp:Button ID="btnBuyNow" runat="server" Text="Buy Now" CssClass="buyNow-btn" CommandArgument='<%# Eval("ProductID") %>' OnClick="btnBuyNow_Click" />
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
</div>
<!-- End Products Row -->


</div>
<!-- End Products Row -->


<br />
<!-- Start Section Title -->
<div class="section-title-container">
    <h2>Galaxy A55 | A35 5G Series</h2>
</div>
<!-- End Section Title -->

<!-- Start Products Row -->
<div class="products-row row">
    <!-- Dynamic product listing using Repeater -->
    <asp:Repeater ID="Repeater3" runat="server">
    <ItemTemplate>
        <div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">
            <img src='<%# Eval("ProductImageURL") %>' class="img-fluid product-thumbnail" alt='<%# Eval("ProductName") %>' />
            <h3 class="product-title"><%# Eval("ProductName") %></h3>
            <strong class="product-price">RM <%# Eval("Price", "{0:F2}") %></strong>

            <!-- Review Section -->
            <asp:Repeater ID="rptReviews" runat="server">
            <ItemTemplate>
                <div class="review-section">
                    <h5>Review by Customer</h5>
                    <p><%# Eval("ReviewDescription") %></p>
                    <strong>Rating: <%# Eval("ReviewRating") %> / 5</strong>
                    <br />
                    <em>Reviewed on: <%# Eval("ReviewDate", "{0:dd MMM yyyy}") %></em>
                </div>
            </ItemTemplate>
            </asp:Repeater>
            <!-- End Review Section -->

            <div class="buyNow">
                <asp:Button ID="btnBuyNow" runat="server" Text="Buy Now" CssClass="buyNow-btn" CommandArgument='<%# Eval("ProductID") %>' OnClick="btnBuyNow_Click" />
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
</div>
<!-- End Products Row -->

</asp:Content>
