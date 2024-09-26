<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="support.aspx.cs" Inherits="NewVersion.admin.support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <script type="text/javascript">
        function confirmMarkAsRead() {
            return confirm("Are you sure you want to mark this message as read?");
        }

        function openMailClient(email, firstName) {
            var subject = encodeURIComponent("Reply to your support request");
            var body = encodeURIComponent("Dear " + firstName + ",\n\n");
            var mailtoLink = "mailto:" + email + "?subject=" + subject + "&body=" + body;

            window.open(mailtoLink);

            return false;
        }

    </script>

    <div class="card">
        <div class="card-header d-flex justify-content-between align-items-start">
            <div>
                <h4 class="card-title mb-0">Support Messages</h4>
                <asp:Label ID="FeedbackLabel" runat="server" CssClass="text-success mb-3"></asp:Label>
            </div>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView
                    ID="GridView1"
                    runat="server"
                    AutoGenerateColumns="False"
                    AllowSorting="True"
                    AllowPaging="True"
                    PageSize="7"
                    CssClass="display table table-striped table-hover"
                    DataKeyNames="SupportID"
                    OnSorting="GridView1_Sorting"
                    OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="SupportID" HeaderText="Support ID" SortExpression="SupportID" Visible="false" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" HeaderStyle-ForeColor="Black" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button
                                    ID="MarkAsReadButton"
                                    runat="server"
                                    CssClass="btn btn-link"
                                    OnClientClick="return confirmMarkAsRead();"
                                    CommandArgument='<%# Eval("SupportID") %>'
                                    Text="Mark as Read"
                                    OnClick="MarkAsReadButton_Click" />
                                <asp:Button
                                    ID="ReplyButton"
                                    runat="server"
                                    CssClass="btn btn-link"
                                    CommandArgument='<%# Eval("SupportID") %>'
                                    Text="Reply"
                                    OnClick="ReplyButton_Click"
                                    OnClientClick='<%# "openMailClient(\"" + Eval("Email") + "\", \"" + Eval("FirstName") + "\")" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
