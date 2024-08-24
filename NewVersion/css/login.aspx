<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="NewVersion.css.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
  <div class="vh-100">
   <div class="container py-5 h-100">
    <div class="row d-flex align-items-center justify-content-center h-100">
      <div class="col-md-7 col-lg-5 col-xl-5 offset-xl-1">
          <div class="inner_form">
          <!-- Form title -->
          <h2>Sign In</h2>
              
          <div class="input_wrapper">
          
          <!-- Email input -->
          <div class="form-outline mb-4">
              <asp:Label ID="lbl_email" runat="server" Text="Email Address" class="form-label"></asp:Label>
            <asp:TextBox ID="txt_email" runat="server" class="form-control form-control-lg"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_email" Display="Dynamic" ErrorMessage="*Please Enter Email Address" ForeColor="Red"></asp:RequiredFieldValidator>
          </div>

          <!-- Password input -->
          <div class="form-outline mb-4">
              <asp:Label ID="lbl_password" runat="server" Text="Password" class="form-label"></asp:Label>
              <asp:TextBox ID="txt_password" runat="server" class="form-control form-control-lg"></asp:TextBox> 
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_password" Display="Dynamic" ErrorMessage="*Please Enter Password" ForeColor="Red"></asp:RequiredFieldValidator>
          </div>
          </div>

          <div class="d-flex justify-content-around align-items-center mb-4">
            <!-- Checkbox -->
            <div class="form-check">
            <asp:CheckBox ID="ckb_remember" runat="server" />
            <asp:Label ID="lbl_remember" runat="server" Text="Remember Me" class="form-check-label"></asp:Label>
            </div>
            <a href="forgotpw.aspx">Forgot password?</a>
          </div>

          <!-- Submit button -->
          <div class="button_wrapper">
          <asp:Button ID="btn_sigin" runat="server" Text="Sign In" class="btn btn-primary btn-lg btn-block" OnClick="btn_sigin_Click"/>
          </div>
          
          <p style="text-align:center; margin:10px">Dont have an account? <a href="signup.aspx">Sign Up</a></p>

          <div class="divider d-flex align-items-center my-4">
          <p class="text-center fw-bold mx-3 mb-0 text-muted"><span style="color:#fff">OR</span></p>
          </div>
             
          <!-- Social Icon -->
              <div class="icon_wrapper">
          <asp:LinkButton ID="login_google" runat="server" class="btn btn-primary btn-lg btn-block" style="background-color: #5D5D5D; border:2px solid #5D5D5D; border-radius:50px;" OnClick="login_google_Click">
              <i class="fab fa-google me-2"></i>Continue with Google
          </asp:LinkButton>
    
            
          
          <a class="btn btn-primary btn-lg btn-block" style="background-color: #5D5D5D ;border:2px solid #5D5D5D; border-radius:50px; margin-top:15px" href="#!"
            role="button">
            <i class="fab fa-twitter me-2"></i>Continue with Twitter</a>
              </div>
      </div>
    </div>
   </div>
  </div>
 </div>
</asp:Content>
