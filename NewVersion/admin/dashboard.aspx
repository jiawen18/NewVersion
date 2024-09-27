<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="NewVersion.admin.dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="d-flex align-items-left align-items-md-center flex-column flex-md-row pt-2 pb-4">
        <div>
            <h3 class="fw-bold mb-3">Dashboard</h3>
        </div>
    </div>

    <!-- totals section -->
    <div class="row">
        <div class="col-sm-6 col-md-3">
            <div class="card card-stats card-round">
                <div class="card-body">
                    <div class="row align-items-center">
                        <div class="col-icon">
                            <div class="icon-big text-center icon-primary bubble-shadow-small">
                                <i class="fas fa-users"></i>
                            </div>
                        </div>
                        <div class="col col-stats ms-3 ms-sm-0">
                            <div class="numbers">
                                <p class="card-category">Members</p>
                                <asp:Label runat="server" ID="MembersCountLabel" CssClass="card-title">0</asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6 col-md-3">
            <div class="card card-stats card-round">
                <div class="card-body">
                    <div class="row align-items-center">
                        <div class="col-icon">
                            <div class="icon-big text-center icon-info bubble-shadow-small">
                                <i class="fas fa-user-check"></i>
                            </div>
                        </div>
                        <div class="col col-stats ms-3 ms-sm-0">
                            <div class="numbers">
                                <p class="card-category">Admins</p>
                                <asp:Label runat="server" ID="AdminsCountLabel" CssClass="card-title">0</asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6 col-md-3">
            <div class="card card-stats card-round">
                <div class="card-body">
                    <div class="row align-items-center">
                        <div class="col-icon">
                            <div class="icon-big text-center icon-success bubble-shadow-small">
                                <i class="fas fa-luggage-cart"></i>
                            </div>
                        </div>
                        <div class="col col-stats ms-3 ms-sm-0">
                            <div class="numbers">
                                <p class="card-category">Sales</p>
                                <asp:Label runat="server" ID="SalesAmountLabel" CssClass="card-title">RM 0.00</asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6 col-md-3">
            <div class="card card-stats card-round">
                <div class="card-body">
                    <div class="row align-items-center">
                        <div class="col-icon">
                            <div class="icon-big text-center icon-secondary bubble-shadow-small">
                                <i class="far fa-check-circle"></i>
                            </div>
                        </div>
                        <div class="col col-stats ms-3 ms-sm-0">
                            <div class="numbers">
                                <p class="card-category">Orders</p>
                                <asp:Label runat="server" ID="OrdersCountLabel" CssClass="card-title">0</asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- user insights section -->
    <div class="row">
        <div class="col-md-12">
            <div class="card card-round">
                <div class="card-header">
                    <div class="card-head-row card-tools-still-right">
                        <h4 class="card-title">User Insights</h4>
                    </div>
                    <p class="card-category">
                        Data sourced from Google Analytics
                    </p>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div style="width: 100%; height: 600px; overflow: hidden;">
                            <iframe
                                src="https://lookerstudio.google.com/embed/reporting/ef6051ef-f90c-4261-9412-15e395f0bdea/page/8pEDE"
                                frameborder="0"
                                style="border: 0; width: 100%; height: 100%;"
                                allowfullscreen
                                sandbox="allow-storage-access-by-user-activation allow-scripts allow-same-origin allow-popups allow-popups-to-escape-sandbox"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <!-- pending refund section -->
        <div class="col-md-4">
            <div class="card card-round">
                <div class="card-header">
                    <div class="card-head-row card-tools-still-right">
                        <div class="card-title">Pending Refunds</div>
                    </div>
                </div>
                <div class="card-body p-0">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <asp:GridView ID="PendingRefundsGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table align-items-center mb-0" AllowPaging="True" PageSize="7" 
                                    OnPageIndexChanging="PendingRefundsGridView_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                                        <asp:BoundField DataField="TotalAmount" HeaderText="Amount" />
                                        <asp:TemplateField HeaderText="Time">
                                            <ItemTemplate>
                                                <%# GetTimeAgo(Convert.ToDateTime(Eval("RefundRequestDate"))) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <!-- inventory levels section -->
        <div class="col-md-8">
            <div class="card card-round">
                <div class="card-header">
                    <div class="card-head-row card-tools-still-right">
                        <div class="card-title">Inventory Levels</div>
                    </div>
                </div>
                <div class="card-body p-0">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <asp:GridView ID="InventoryGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table align-items-center mb-0" AllowPaging="True" PageSize="7"
                                    OnPageIndexChanging="InventoryGridView_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                        <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <span class='<%# Convert.ToString(Eval("Status")) == "Low" ? "badge badge-warning" : Convert.ToString(Eval("Status")) == "Out of Stock" ? "badge badge-danger" : Convert.ToString(Eval("Status")) == "Sufficient" ? "badge badge-success" : "badge badge-secondary" %>'>
                                                    <%# Convert.ToString(Eval("Status")) %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
