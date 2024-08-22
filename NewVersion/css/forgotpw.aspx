<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="forgotpw.aspx.cs" Inherits="NewVersion.css.forgotpw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div class="vh-100">
  <div class="container py-5 h-100">
   <div class="row d-flex align-items-center justify-content-center h-100">
     <div class="col-md-7 col-lg-5 col-xl-5 offset-xl-1">
         <div class="inner_form">
         <!-- Form title -->
         <div class="resetpw_wrapper">

         <h2>Reset Password</h2>

         <p class="resetpw_text">Enter your email address and we'll send you an email with instructions to reset your password.</p>
             
         <div class="email_input">
         <asp:TextBox ID="txt_emailreset" runat="server"></asp:TextBox>
         </div>

         <div class="btn_resetpw_wrapper">
         <asp:Button ID="btn_resetpw" runat="server" Text="Reset Password" class="btn_resetpw"/>
         </div>

         </div>
      

   

         
      
            
      
           
     </div>
   </div>
  </div>
 </div>
</div>

</asp:Content>
