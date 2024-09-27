<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="newSuperAdmin.aspx.cs" Inherits="NewVersion.admin.newSuperAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <div class="card">
        <div class="card-header">
            <div class="d-flex align-items-center">
                <h4 class="card-title">Add New Admin</h4>
                <a href ="#"
                    class="btn btn-primary btn-round ms-auto"
                    data-toggle="modal"
                    data-target="#addRowModal">
                    <i class="fa fa-plus"></i>
                    Create Super Admin
                </a>
            </div>     
        </div>
                    <div class="card-body">
    <div class="table-responsive">
        <table
            id="add-row"
            class="display table table-striped table-hover">
            <thead>
                <tr>
                    <th>Super Admin ID</th>
                    <th>Email</th>
                    <th>Username</th>
                    <th>Position</th>
                    <th>Office</th>
                </tr>
            </thead>
              <tbody>
       <!-- Repeater control to bind super admin data -->
        <asp:Repeater ID="RepeaterSuperAdminList" runat="server">
            <ItemTemplate>
                <tr>                        
                    <td>
                        <asp:Label ID="lbl_superAdminID" runat="server" Text='<%# Eval("SuperAdminID") %>'></asp:Label>
         
                    </td>
                    <td>
                        <asp:Label ID="lbl_email" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                        <asp:TextBox ID="txt_email" runat="server" Text='<%# Eval("Email") %>' CssClass="form-control" Visible="false"></asp:TextBox>
                    </td>
                     <td>
                        <asp:Label ID="lbl_username" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                        <asp:TextBox ID="txt_username" runat="server" Text='<%# Eval("Username") %>' CssClass="form-control" Visible="false"></asp:TextBox>
                    </td>
                     <td>
                        <asp:Label ID="lbl_position" runat="server" Text='<%# Eval("Position") %>'></asp:Label>
                        <asp:TextBox ID="txt_position" runat="server" Text='<%# Eval("Position") %>' CssClass="form-control" Visible="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_office" runat="server" Text='<%# Eval("Office") %>'></asp:Label>
                        <asp:TextBox ID="txt_office" runat="server" Text='<%# Eval("Office") %>' CssClass="form-control" Visible="false"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
        </table>
    </div>
</div>
       
    </div>
     <div class="card-body">
     <!-- Modal -->
     <div
         class="modal fade"
         id="addRowModal"
         tabindex="-1"
         role="dialog"
         aria-hidden="true">
         <div class="modal-dialog" role="document">
             <div class="modal-content">
                 <div class="modal-header border-0">
                     <h5 class="modal-title">
                         <span class="fw-mediumbold">New</span>
                         <span class="fw-light">Super Admin </span>
                     </h5>
                     <button
                         type="button"
                         class="close"
                         data-dismiss="modal"
                         aria-label="Close">
                         <span aria-hidden="true">&times;</span>
                     </button>
                 </div>
                 <div class="modal-body">
                     <p class="small">
                         Please fill up the registration form to create a super admin account.
                     </p>
                     <div>
                         <div class="row">
                             <div class="col-sm-12">                                       
                                 <div class="form-group form-group-default">
                                    <asp:Label ID="lbl_sp_adm_username" runat="server" Text="Username" class="form-label"></asp:Label>
                                    <asp:TextBox ID="txt_sp_adm_username" runat="server" class="form-control"></asp:TextBox>
                                 </div>
                                  <asp:RequiredFieldValidator ID="rqvld_username" runat="server" ControlToValidate="txt_sp_adm_username" ErrorMessage="*Please Enter Username" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                             </div>


                               <div class="col-sm-12"> 
                                 <div class="form-group form-group-default">                                        
                                     <asp:Label ID="lbl_sp_adm_email" runat="server" Text="Email" class="form-label"></asp:Label>
                                     <asp:TextBox ID="txt_sp_adm_email" runat="server" class="form-control"></asp:TextBox>
             
                                  </div> 
                                     <asp:RequiredFieldValidator ID="rqvld_email" runat="server" ControlToValidate="txt_sp_adm_email" Display="Dynamic" ErrorMessage="*Please Enter Email Address" ForeColor="Red"></asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator ID="revld_email" runat="server" ControlToValidate="txt_sp_adm_email" Display="Dynamic" ErrorMessage="*Invalid Email Addrerss" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                     <asp:CustomValidator ID="cvExisted" runat="server" ControlToValidate="txt_sp_adm_email" Display="Dynamic" ErrorMessage="CustomValidator" ForeColor="Red">*Email Address Already Exist</asp:CustomValidator>
                             </div>

                                  <div class="col-md-6 pe-0">
                              <div class="form-group form-group-default">                                        
                                  <asp:Label ID="lbl_sp_adm_position" runat="server" Text="Position" class="form-label"></asp:Label>
                                  <asp:TextBox ID="txt_sp_adm_position" runat="server" class="form-control"></asp:TextBox>                                                   
                               </div>                                 
                          </div>

                               <div class="col-md-6">
                             <div class="form-group form-group-default">                                        
                                 <asp:Label ID="lbl_sp_adm_office" runat="server" Text="Office" class="form-label"></asp:Label>
                                 <asp:TextBox ID="txt_sp_adm_office" runat="server" class="form-control"></asp:TextBox>                                                   
                              </div>                                 
                         </div>
                          
                           
                               <div class="col-sm-12"> 
                                 <div class="form-group form-group-default">
                                  
                                <asp:Label ID="lbl_sp_adm_password" runat="server" Text="Password" class="form-label"></asp:Label>
                                <asp:TextBox ID="txt_sp_adm_password" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                
                             </div>
                                <p style="font-size:12px;font-weight:lighter">*Password must contains at least eight characters, including at least one number,one uppercase letter and at least one special characters.</p>                       
                                <asp:RequiredFieldValidator ID="rqvld_password" runat="server" ControlToValidate="txt_sp_adm_password" ErrorMessage="*Please Enter Password" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revld_password" runat="server" ControlToValidate="txt_sp_adm_password" Display="Dynamic" ErrorMessage="*Invalid Password." ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&amp;])[A-Za-z\d@$!%*#?&amp;]{8,}$" ForeColor="Red"></asp:RegularExpressionValidator>
                                
                         </div>

                               <div class="col-sm-12"> 
                               <div class="form-group form-group-default">

                                   <asp:Label ID="lbl_sp_adm_passwordconfirm" runat="server" Text="Confirm Password" class="form-label"></asp:Label>
                                   <asp:TextBox ID="txt_sp_adm_passwordconfirm" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                 
                                </div>
                                 <asp:CompareValidator ID="cpvld_password" runat="server" ErrorMessage="*Password Not Match" ControlToValidate="txt_sp_adm_passwordconfirm" Display="Dynamic" ForeColor="Red" ControlToCompare="txt_sp_adm_password"></asp:CompareValidator>
                                 <asp:RequiredFieldValidator ID="rqcld_passwordconfirm" runat="server" ControlToValidate="txt_sp_adm_passwordconfirm" Display="Dynamic" ErrorMessage="*Password Not Match" ForeColor="Red"></asp:RequiredFieldValidator>
                             
                         </div>
                       </div>
                     </div>
                 </div>
                 <div class="modal-footer border-0">
                      <asp:Button ID="btn_add_sp_admin" runat="server" Text="Create" class="btn btn-primary" OnClick=" btn_add_sp_admin_Click"/>

                      <a href="#" class="btn btn-danger" data-dismiss="modal">Close</a>
            
                 </div>
             </div>
         </div>
     </div>
 </div>
</asp:Content>
