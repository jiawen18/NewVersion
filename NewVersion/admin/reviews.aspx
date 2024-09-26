<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="reviews.aspx.cs" Inherits="NewVersion.admin.reviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:SqlDataSource
    ID="SqlDataSource1"
    runat="server"
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    SelectCommand="SELECT ReviewID, ReviewDate, ReviewRating, ReviewImage, ReviewDescription FROM Review">
    </asp:SqlDataSource>

    <div class="col-md-12">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <table
                        id="add-row"
                        class="display table table-striped table-hover">
                        <thead>
                            <tr>
                                <th>Review ID</th>
                                <th>Review Date</th>
                                <th>Review Rating</th>
                                <th>Review Image</th>
                                <th>Review Description</th>
                            </tr>
                        </thead>
                         <tbody>
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                 <asp:Repeater ID="ReviewRepeater" runat="server" OnItemCommand="ReviewRepeater_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("ReviewID") %></td>
                                            <td><%# Eval("ReviewDate", "{0:yyyy-MM-dd}") %></td>
                                            <td><%# Eval("ReviewRating") %></td>
                                            <td>
                                                <img src='<%# Eval("ReviewImage") %>' alt="Review Image" style="width:50px;height:50px;" />
                                            </td>
                                            <td><%# Eval("ReviewDescription") %></td>
                                            <td>
                                                <div class="form-button-action">
                                                    <asp:LinkButton runat="server" CssClass="btn btn-link btn-danger" 
                                                        CommandName="DeleteReview" 
                                                        CommandArgument='<%# Eval("ReviewID") %>' 
                                                        ToolTip="Remove" >
                                                        <i class="fa fa-times"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
