<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="refunds.aspx.cs" Inherits="NewVersion.admin.refunds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <script type="text/javascript">
        function confirmAction(action) {
            return confirm("Are you sure you want to " + action + " this refund request?");
        }
    </script>

    <div class="card">
        <div class="card-header">
            <div class="d-flex align-items-center">
                <h4 class="card-title">List of Refund Requests</h4>
            </div>
            <asp:Label ID="FeedbackLabel" runat="server"></asp:Label>
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
                    DataKeyNames="refundID"
                    OnSorting="GridView1_Sorting"
                    OnPageIndexChanging="GridView1_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="refundID" HeaderText="Refund ID" SortExpression="refundID" Visible="false" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="OrderID" HeaderText="Order ID" SortExpression="OrderID" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="RefundRequestDate" HeaderText="Request Date" SortExpression="RefundRequestDate" HeaderStyle-ForeColor="Black" />
                        <asp:BoundField DataField="RefundStatus" HeaderText="Status" SortExpression="RefundStatus" HeaderStyle-ForeColor="Black" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button
                                    ID="ApproveRefundButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-primary"
                                    OnClientClick="return confirmAction('approve');"
                                    CommandArgument='<%# Eval("refundID") %>'
                                    Text="Approve"
                                    OnClick="ApproveRefundButton_Click" />
                                <asp:Button
                                    ID="RejectRefundButton"
                                    runat="server"
                                    CssClass="btn btn-link btn-danger"
                                    OnClientClick="return confirmAction('reject');"
                                    CommandArgument='<%# Eval("refundID") %>'
                                    Text="Reject"
                                    OnClick="RejectRefundButton_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
