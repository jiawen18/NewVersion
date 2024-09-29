<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Smartphones.aspx.cs" Inherits="NewVersion.css.Smartphones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:SqlDataSource 
    ID="SqlDataSource1" 
    runat="server" 
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    ProviderName="System.Data.SqlClient"
    SelectCommand="
        SELECT 
            p.ProductID, 
            p.ProductName, 
            p.ProductImageURL, 
            p.Price, 
            AVG(r.ReviewRating) AS AverageRating 
        FROM 
            Product p 
        LEFT JOIN 
            Review r ON p.ProductID = r.ProductID 
        GROUP BY 
            p.ProductID, 
            p.ProductName, 
            p.ProductImageURL, 
            p.Price">
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
    <h2>Galaxy A55 | A35 5G Series</h2>
</div>
<!-- End Section Title -->
    <asp:HiddenField ID="hiddenProductId" runat="server" />

<!-- Start Products Row -->
<div class="products-row row">
    <!-- Dynamic product listing using Repeater -->
    <asp:Repeater ID="rptProducts" runat="server">
    <ItemTemplate>
        <div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">
            <img src='<%# Eval("ProductImageURL") %>' class="img-fluid product-thumbnail" alt='<%# Eval("ProductName") %>' />
            <h3 class="product-title"><%# Eval("ProductName") %></h3>
            <strong class="product-price">RM <%# Eval("Price", "{0:F2}") %></strong>

           <div class="product-rating">
            <%# GetRatingStars(Eval("AverageRating")) %>
        </div>   

            <div class="buyNow">
                <asp:Button ID="btnBuyNow" runat="server" Text="Buy Now" CssClass="buyNow-btn" CommandArgument='<%# Eval("ProductID") %>' OnClick="btnBuyNow_Click" />
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
</div>
<!-- End Products Row -->



</asp:Content>
