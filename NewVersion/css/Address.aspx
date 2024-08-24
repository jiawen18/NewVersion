<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Address.aspx.cs" Inherits="NewVersion.css.Address" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="hero">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-5">
                    <div class="intro-excerpt">
                        <h1>Delivery Address</h1>
                        <%--<p class="mb-4">Discover the latest gadgets designed to elevate your digital experience. Shop now for cutting-edge devices that keep you ahead of the curve.</p>
                        <p>
                            <a href="Smartphones.aspx" class="btn btn-secondary me-2">Shop Now</a>
                            <a href="AboutUs.aspx" class="btn btn-white-outline">Explore</a>
                        </p>--%>
                    </div>
                </div>
                <%--<div class="col-lg-7">
                    <div class="hero-img-wrap">
                        <img src="images/slide_image1.png" class="images">
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
    
        <div class="wrapper-address-content">
        <!-- Grid items will be added here -->
        <div class="address-content">
            <a href="#" data-toggle="modal" data-target="#addressModal"><span style="align-items:center">Add New Address ></span></a>          
        </div>

        <div class="address-content">
            <div class="address-name"><strong>Kelvin</strong></div>
            <div class="address-phone">+ 60183938494</div>
            <div class="address-address">A-39, Taman Padi Emas Kedah 03403</div>
            <div class="address-edit">
                <a href="#" data-toggle="modal" data-target="#addressModal">Edit</a>
                <a href="#">Delete</a>
            </div>
        </div>

       
        <!-- Add more grid items as needed -->
    </div>


  <div class="container">
  
        <!-- Address Modal -->
        <div class="modal fade" id="addressModal" tabindex="-1" role="dialog" aria-labelledby="addressModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="addressModalLabel">Address Form</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Panel ID="pnlAddressForm" runat="server">
                            <asp:TextBox ID="txt_addr_name" runat="server" CssClass="form-control" placeholder="Name*"></asp:TextBox><br />
                            <asp:TextBox ID="txt_addr_street" runat="server" CssClass="form-control" placeholder="State*"></asp:TextBox><br />
                            <asp:TextBox ID="txt_addr_city" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox><br />
                            <asp:TextBox ID="txt_addr_postcode" runat="server" CssClass="form-control" placeholder="Post Code"></asp:TextBox><br />
                            <asp:TextBox ID="txt_addr_address" runat="server" CssClass="form-control" placeholder="Address*"></asp:TextBox><br />
                            <asp:TextBox ID="txt_addr_email" runat="server" CssClass="form-control" placeholder="Email Address*"></asp:TextBox><br />
                            <asp:HiddenField ID="hfAddressID" runat="server" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"/>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
