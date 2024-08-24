<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="NewVersion.css.Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="hero">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-5">
                    <div class="intro-excerpt">
                        <h1>Account Settings</h1>
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
    
    <div class="container light-style flex-grow-1 container-p-y" style="margin-top: 3rem">
        <%--<h4 class="font-weight-bold py-3 mb-4">Account settings
        </h4>--%>
  
    <div class="card overflow-hidden">
        <div class="row no-gutters row-bordered row-border-light">
            <div class="col-md-3 pt-0">
                <div class="list-group list-group-flush account-settings-links">
                    <a class="list-group-item list-group-item-action active" data-toggle="list"
                        href="#account-Account">Account</a>
                    <a class="list-group-item list-group-item-action" data-toggle="list"
                        href="#account-change-password">Change password</a>
                    <a class="list-group-item list-group-item-action" data-toggle="list"
                        href="#account-info">Info</a>                                       
                     <a class="list-group-item list-group-item-action" href="Home.aspx">Log Out</a>
                </div>
            </div>
          
            <div class="col-md-9">
                <div class="tab-content">
                    <div class="tab-pane fade active show" id="account-Account">
                        <div class="card-body media align-items-center">
                            <img src="https://i.imgur.com/8RKXAIV.jpg"
                                class="d-block ui-w-80">
                            <div class="media-body ml-4">
                                <label class="btn btn-outline-primary">
                                    Upload new photo
                                    <input type="file" class="account-settings-fileinput">
                                </label>                                                     
                            </div>
                        </div>
                        <hr class="border-light m-0">

                        <!-- User Account -->
                        <div class="card-body">
                            <div class="form-group">
                                <asp:Label ID="lbl_acc_username" runat="server" Text="Username" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_username" runat="server" class="form-control mb-1" value="shen"></asp:TextBox>                            
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_acc_name" runat="server" Text="Name" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_name" runat="server" class="form-control mb-1" value="Chong Khai Shen"></asp:TextBox>                  
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_acc_email" runat="server" Text="Username" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_acc_email" runat="server" class="form-control mb-1" value="kelvinchong0457@gmail.com"></asp:TextBox>                                
                            </div>                           
                        </div>
                    </div>

                    <!-- Change Password -->
                    <div class="tab-pane fade" id="account-change-password">
                        <div class="card-body pb-2">

                            <div class="form-group">
                                <asp:Label ID="lbl_acc_crPassword" runat="server" Text="Current Password" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_acc_crPassword" runat="server" type="password" class="form-control"></asp:TextBox>                      
                            </div>

                            <div class="form-group"> 
                                <asp:Label ID="lbl_acc_newPassword" runat="server" Text="New Password" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_acc_newPassword" runat="server" type="password" class="form-control"></asp:TextBox>                                        
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_acc_rpNewPassword" runat="server" Text="Repeat New Password" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_acc_rpNewPassword" runat="server" type="password" class="form-control"></asp:TextBox>                                  
                            </div>
                        </div>
                    </div>

                    <!-- User Info -->
                    <div class="tab-pane fade" id="account-info">
                        <div class="card-body pb-2">
                           
                            <div class="form-group">
                                <asp:Label ID="lbl_acc_birthday" runat="server" Text="Birthday" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_acc_birthday" runat="server" value="May 3, 1995" class="form-control"></asp:TextBox>                            
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_acc_country" runat="server" Text="Country" class="form-label"></asp:Label>
                                <asp:DropDownList ID="dll_acc_country" runat="server" class="custom-select">
                                     <asp:ListItem Value="USA">United States</asp:ListItem>
                                     <asp:ListItem Value="CND">Canada</asp:ListItem>
                                     <asp:ListItem Value="UK">United Kingdom</asp:ListItem>
                                     <asp:ListItem Value="GM">Germany</asp:ListItem>
                                     <asp:ListItem Value="FR">France</asp:ListItem>
                                     <asp:ListItem Value="MLs">Malaysia</asp:ListItem>
                                </asp:DropDownList>                               
                            </div>
                        </div>

                        <hr class="border-light m-0">
                        <div class="card-body pb-2">
                            <h6 class="mb-4">Contacts</h6>

                            <div class="form-group">
                                <asp:Label ID="lbl_acc_phone" runat="server" Text="Phone" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_acc_phonr" runat="server" value="+0 (123) 456 7891" class="form-control"></asp:TextBox>                          
                            </div>
                     
                        </div>
                    </div>
              
              
          
                </div>
            </div>
        </div>
    </div>
    <div class="text-right mt-3">
        <asp:Button ID="btn_acc_svChanges" runat="server" Text="Save Changes" class="btn btn-primary" />
        <asp:Button ID="btn_acc_cancel" runat="server" Text="Cancel" class="btn btn-default" OnClick="btn_acc_cancel_Click" />       
    </div>
</div>
</asp:Content>
