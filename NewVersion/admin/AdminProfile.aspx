<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminProfile.aspx.cs" Inherits="NewVersion.admin.AdminProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <!-- Script to upload file -->
     <script type="text/javascript">
        function triggerFileUpload() {
            document.getElementById('<%= fileUpload.ClientID %>').click();
        }      
    </script>

     <style>
        /* Hide the actual file input */
        input[type="file"] {
            display: none;
        }
     </style>


        <div class="container light-style flex-grow-1 container-p-y">
    <h4 class="font-weight-bold py-3 mb-4">
        Account Settings
    </h4>
   
    <div class="card overflow-hidden">
        <div class="row no-gutters row-bordered row-border-light">
            <div class="col-md-3 pt-0">
                <div class="list-group list-group-flush account-settings-links">
                    <a class="list-group-item list-group-item-action active" data-toggle="list"
                        href="#account-Account"><span style="padding-left:30px">Account</span></a>
                    <a class="list-group-item list-group-item-action" data-toggle="list"
                        href="#account-change-password"> <span style="padding-left:30px">Change password</span></a>
                    <a class="list-group-item list-group-item-action" data-toggle="list"
                        href="#account-info"><span style="padding-left:30px">Info</span></a>                                                      
                </div>
            </div>
          
            <div class="col-md-9">
                <div class="tab-content">
                    <div class="tab-pane fade active show" id="account-Account">
                        <div class="card-body media align-items-center">                            
                       <asp:Image ID="imgProfile" runat="server" 
                       ImageUrl="assets/img/profile.jpg" 
                       Width="150px" Height="150px" 
                       Style="cursor:pointer;"
                       OnClick="triggerFileUpload();" /> 
                       <asp:FileUpload ID="fileUpload" runat="server" OnChange="previewImage(this);" />
                       <div class="media-body ml-4">
                       <label class="btn btn-outline-primary">
                         Upload new photo
                         <input type="file" class="account-settings-fileinput">
                       </label>                                                     
  </div>
                            
                        </div>
                        <hr class="border-light m-0">

                        <!-- Admin Account -->
                        <div class="card-body">

                            <div class="form-group">
                            <asp:Label ID="lbl_adm_id" runat="server" Text="Admin ID" class="form-label"></asp:Label>
                            <asp:TextBox ID="txt_adm_id" runat="server" class="form-control mb-1" value="009283838" disabled="disabled"></asp:TextBox>                            
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_adm_username" runat="server" Text="Username" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_adm_username" runat="server" class="form-control mb-1" value="Hizrian"></asp:TextBox>                            
                            </div> 
                            
                             <div class="form-group">
                                 <asp:Label ID="lbl_adm_position" runat="server" Text="Position" class="form-label"></asp:Label>
                                 <asp:TextBox ID="txt_adm_position" runat="server" class="form-control mb-1" ></asp:TextBox>                            
                             </div>   
                        
                             <div class="form-group">
                                 <asp:Label ID="lbl_adm_office" runat="server" Text="Office" class="form-label"></asp:Label>
                                 <asp:TextBox ID="txt_adm_office" runat="server" class="form-control mb-1"></asp:TextBox>                            
                             </div>   

                            <div class="form-group">
                                <asp:Label ID="lbl_adm_email" runat="server" Text="Email" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_adm_email" runat="server" class="form-control mb-1" value="hello@example.com"></asp:TextBox>                                
                            </div>     
                            
                             <div class="form-group">
                                <asp:Label ID="lbl_adm_role" runat="server" Text="Role" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_adm_role" runat="server" class="form-control mb-1" value="Administrator" disabled="disabled"></asp:TextBox>                                
                            </div>   
                        </div>
                    </div>

                    <!-- Change Password -->
                    <div class="tab-pane fade" id="account-change-password">
                        <div class="card-body pb-2">

                            <div class="form-group">
                                <asp:Label ID="lbl_adm_crPassword" runat="server" Text="Current Password" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_adm_crPassword" runat="server" type="password" class="form-control"></asp:TextBox>                      
                            </div>

                            <div class="form-group"> 
                                <asp:Label ID="lbl_adm_newPassword" runat="server" Text="New Password" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_adm_newPassword" runat="server" type="password" class="form-control"></asp:TextBox>                                        
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_adm_rpNewPassword" runat="server" Text="Repeat New Password" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_adm_rpNewPassword" runat="server" type="password" class="form-control"></asp:TextBox>                                  
                            </div>
                        </div>
                    </div>

                    <!-- Admin Info -->
                    <div class="tab-pane fade" id="account-info">
                        <div class="card-body pb-2">
                           
                            <div class="form-group">
                                <asp:Label ID="lbl_adm_birthday" runat="server" Text="Birthday" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_adm_birthday" runat="server" value="May 3, 1995" class="form-control"></asp:TextBox>                            
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_adm_country" runat="server" Text="Country" class="form-label"></asp:Label>
                                <asp:DropDownList ID="dll_adm_country" runat="server" class="custom-select">
                                     <asp:ListItem Value="USA">United States</asp:ListItem>
                                     <asp:ListItem Value="CND">Canada</asp:ListItem>
                                     <asp:ListItem Value="UK">United Kingdom</asp:ListItem>
                                     <asp:ListItem Value="GM">Germany</asp:ListItem>
                                     <asp:ListItem Value="FR">France</asp:ListItem>
                                     <asp:ListItem Value="MLS">Malaysia</asp:ListItem>
                                </asp:DropDownList>                               
                            </div>
                        </div>

                        <hr class="border-light m-0">
                        <div class="card-body pb-2">
                            <h6 class="mb-4">Contacts</h6>

                            <div class="form-group">
                                <asp:Label ID="lbl_adm_phone" runat="server" Text="Phone" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_adm_phonr" runat="server" value="+0 (123) 456 7891" class="form-control"></asp:TextBox>                          
                            </div>
                     
                        </div>
                    </div>
                      
                </div>
            </div>
        </div>
    </div>
    <div class="text-right mt-3">
        <asp:Button ID="btn_acc_svChanges" runat="server" Text="Save Changes" class="btn btn-primary" OnClick="btn_acc_svChanges_Click" />
        <asp:Button ID="btn_acc_cancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btn_acc_cancel_Click"   />       
    </div>
 </div>


</asp:Content>
