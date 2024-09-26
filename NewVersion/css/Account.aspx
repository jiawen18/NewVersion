<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="NewVersion.css.Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <script type="text/javascript">
         function triggerFileUpload() {
             document.getElementById('<%= fileUpload.ClientID %>').click();
         }

         function previewImage(input) {
             if (input.files && input.files[0]) {
                 var reader = new FileReader();
                 reader.onload = function (e) {
                     document.getElementById('<%= imgProfile.ClientID %>').src = e.target.result;
                 };
                 reader.readAsDataURL(input.files[0]);
             }
         }
     </script>


 <style>
    /* Hide the actual file input */
    input[type="file"] {
        display: none;
    }
 </style>
        <div class="hero">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-5">
                    <div class="intro-excerpt">
                        <h1>Account Settings</h1>             
                    </div>
                </div>
              
            </div>
        </div>
    </div>
    
    <div class="container light-style flex-grow-1 container-p-y" style="margin-top: 3rem">
       
  
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
                  
                </div>
            </div>
          
            <div class="col-md-9">
                <div class="tab-content">
                    <div class="tab-pane fade active show" id="account-Account">
                        <div class="card-body media align-items-center">
                              <asp:Image ID="imgProfile" runat="server"                      
                                  Width="150px" Height="150px" 
                                  Style="cursor:pointer;"
                                 OnClick="triggerFileUpload();"/> 
                               <asp:FileUpload ID="fileUpload" runat="server" OnChange="previewImage(this);" style="display:none;" />
                                  <div class="media-body ml-4">
                                <label class="btn btn-outline-primary" onclick="triggerFileUpload()">
                                    Upload new photo
                                </label>     
                                <asp:FileUpload ID="fileUpload1" runat="server" OnChange="previewImage(this);" style="display:none;" />                                                    
                            </div>
                        </div>
                        <hr class="border-light m-0">

                        <!-- User Account -->
                        <div class="card-body">

                             <div class="form-group">
                            <asp:Label ID="lbl_mb_id" runat="server" Text="Hansumg Account ID" class="form-label"></asp:Label>
                            <asp:TextBox ID="txt_mb_id" runat="server" class="form-control mb-1"  disabled="disabled"></asp:TextBox>                            
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="lbl_acc_username" runat="server" Text="Username" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_username" runat="server" class="form-control mb-1"></asp:TextBox>                            
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_acc_email" runat="server" Text="Email" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_acc_email" runat="server" class="form-control mb-1"></asp:TextBox>                                
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
                                <asp:TextBox ID="txt_acc_birthday" runat="server" class="form-control"></asp:TextBox>                            
                            </div>
                         
                        </div>

                        <hr class="border-light m-0">
                        <div class="card-body pb-2">
                            <h6 class="mb-4">Contacts</h6>

                            <div class="form-group">
                                <asp:Label ID="lbl_acc_phone" runat="server" Text="Phone" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_acc_phonr" runat="server" class="form-control"></asp:TextBox>                          
                            </div>
                     
                        </div>
                    </div>
              
              
          
                </div>
            </div>
        </div>
    </div>
    <div class="text-right mt-3">
        <asp:Button ID="btn_acc_svChanges" runat="server" Text="Save Changes" class="btn btn-primary"/>
        <asp:Button ID="btn_acc_cancel" runat="server" Text="Cancel" class="btn btn-default" OnClick="btn_acc_cancel_Click" />       
    </div>
</div>
</asp:Content>
