<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Delivery.aspx.cs" Inherits="NewVersion.css.Delivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="hero">
	<div class="container">
        <div class="row justify-content-between">
            <div class="col-lg-5">
	            <div class="intro-excerpt">
	            <h1>Delivery Status</h1>
	            </div>
                </div>
            <div class="col-lg-7">
			
            </div>
        </div>
    </div>
</div>


            <div class="container">
             <article class="card">
                <header class="card-header"> My Orders / Tracking </header>
                    <div class="card-body">
                        <h6>Order ID: ORD12345</h6>
                    <article class="card">
                        <div class="card-body row">
                                    <div class="col"> <strong>Estimated Delivery time:</strong> <br>26 Aug 2024 </div>
                                    <div class="col"> <strong>Shipping BY:</strong> <br> SPX Express, | <i class="fa fa-phone"></i> +1598675986 </div>
                                    <div class="col"> <strong>Status:</strong> <br> Picked by the courier </div>
                                    <div class="col"> <strong>Tracking #:</strong> <br> BD045903594059 </div>
                          </div>
                    </article>
            <div class="track">
                <div class="step active"> <span class="icon"> <i class="fa fa-check"></i> </span> <span class="text">Order confirmed</span> </div>
                <div class="step active"> <span class="icon"> <i class="fa fa-user"></i> </span> <span class="text"> Picked by courier</span> </div>
                <div class="step"> <span class="icon"> <i class="fa fa-truck"></i> </span> <span class="text"> On the way </span> </div>
                <div class="step"> <span class="icon"> <i class="fa fa-box"></i> </span> <span class="text">Ready for pickup</span> </div>
            </div>
    <hr>
            <div>
                    <figure class="itemside mb-3">
                        <div class="aside"><img src="images/GalaxyA.png" class="img-sm "></div>
                            <figcaption class="info align-self-center">
    <div style="display: flex; justify-content: space-between; align-items: center;">
        <div>
            <p class="title" style="margin: 0;">Samsung Galaxy A55</p>
            <span class="text-muted">Color: Mix <br /> Capacity: 64GB</span>
            <p>x 1</p>
        </div>
        <div style="position: relative; left: 680px;">
            <p >RM 1999.90</p>
        </div>
    </div>
                        
                                
                </figcaption> 
             </figure>
             </div>   
    <hr>
        <div >
        <table>
            <tr>
                <td style="position: relative; left: 800px;">Total</td>
                <td style="position: relative; left: 915px;">RM 1999.90</td>
            </tr>
            <tr>
                <td style="position: relative; left: 800px;">Delivery Fee</td>
                <td style="position: relative; left: 922px;">RM 5.90</td>
            </tr>
            <tr>
                <td style="position: relative; left: 800px;">Grant Total</td>
                <td style="position: relative; left: 922px;"><strong>RM 4.90</strong></td>
</tr>
        </table>
    </div>
            <hr style="border: 0; border-top: 1px solid #000; margin: 10px 0;">
                <asp:Button class="btn btn-warning" data-abc="true" ID="btnBackOrder" runat="server" Text="< Back to orders" OnClick="btnBackOrder_Click" />
            </div>
    </article>
</div> 

    <%--css--%>
    <link href="css/delivery.css" rel="stylesheet" />
</asp:Content>
