<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="NewVersion.css.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="vh-100">
  <div class="container py-5 h-100">
   <div class="row d-flex align-items-center justify-content-center h-100">
     <div class="col-md-7 col-lg-5 col-xl-5 offset-xl-1">
         <div class="inner_form">

         <!-- Form title -->
         <h2>Sign Up</h2>

         <div class="input_wrapper">
         
         <!-- Email input -->
         <div class="form-outline mb-4">
         <asp:Label ID="lbl_email" runat="server" Text="Email Address" class="form-label"></asp:Label>
         <asp:TextBox ID="txt_emailsignup" runat="server" class="form-control form-control-lg"></asp:TextBox>
         <asp:RequiredFieldValidator ID="rqvld_email" runat="server" ControlToValidate="txt_emailsignup" Display="Dynamic" ErrorMessage="*Please Enter Email Address" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:RegularExpressionValidator ID="revld_email" runat="server" ControlToValidate="txt_emailsignup" Display="Dynamic" ErrorMessage="*Invalid Email Addrerss" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
         <asp:CustomValidator ID="cvExisted" runat="server" ControlToValidate="txt_emailsignup" Display="Dynamic" ErrorMessage="CustomValidator" ForeColor="Red">*Email Address Already Exist</asp:CustomValidator>
         </div>

         <!-- Usernamme -->
         <div class="form-outline mb-4">
         <asp:Label ID="lbl_username" runat="server" Text="Username" class="form-label"></asp:Label>
         <asp:TextBox ID="txt_username" runat="server" class="form-control form-control-lg"></asp:TextBox>
         <asp:RequiredFieldValidator ID="revld_username" runat="server" ControlToValidate="txt_username" Display="Dynamic" ErrorMessage="*Please Enter Username" ForeColor="Red"></asp:RequiredFieldValidator>       
         </div>

         <!-- Password input -->
         <div class="form-outline mb-4">
         <asp:Label ID="lbl_password" runat="server" Text="Password" class="form-label"></asp:Label>
         <asp:TextBox ID="txt_passwordsingup" runat="server"  TextMode="Password" class="form-control form-control-lg"></asp:TextBox>
         <asp:RequiredFieldValidator ID="rqvld_password" runat="server" ControlToValidate="txt_passwordsingup" ErrorMessage="*Please Enter Password" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
         <asp:RegularExpressionValidator ID="revld_password" runat="server" ControlToValidate="txt_passwordsingup" Display="Dynamic" ErrorMessage="*Invalid Password." ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&amp;])[A-Za-z\d@$!%*#?&amp;]{8,}$" ForeColor="Red"></asp:RegularExpressionValidator>
         <p style="font-size:12px;font-weight:lighter">*Password must contains at least eight characters, including at least one number,one uppercase letter and at least one special characters.</p>
         </div>


         <!-- Password Confirmation input -->
         <div class="form-outline mb-4">
         <asp:Label ID="lbl_passwordconfirm" runat="server" Text="Confirm Password" class="form-label"></asp:Label>
         <asp:TextBox ID="txt_passwordconfirm" runat="server" TextMode="Password" class="form-control form-control-lg"></asp:TextBox>
         <asp:CompareValidator ID="cpvld_password" runat="server" ErrorMessage="*Password Not Match" ControlToValidate="txt_passwordconfirm" Display="Dynamic" ForeColor="Red" ControlToCompare="txt_passwordsingup"></asp:CompareValidator>
         <asp:RequiredFieldValidator ID="rqcld_passwordconfirm" runat="server" ControlToValidate="txt_passwordconfirm" Display="Dynamic" ErrorMessage="*Password Not Match" ForeColor="Red"></asp:RequiredFieldValidator>
         </div>
         </div>

         <!-- Submit button -->
         <div class="button_wrapper">
         <asp:Button ID="btn_sigup" runat="server" Text="Sign Up" class="btn btn-primary btn-lg btn-block" OnClick="btn_sigup_Click"/>
         </div>      
         <p style="text-align:center;padding-top:10px;"><a href="login.aspx"> Already have an account? Sign In.</a></p>
        
     </div>
    </div>
   </div>
  </div>
 </div>
</asp:Content>
