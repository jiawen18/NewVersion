<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="FailurePage.aspx.cs" Inherits="NewVersion.css.FailurePage" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container">
    <br />
        <h1>Payment Failed</h1>
        <p class="message">Unfortunately, your payment could not be processed.</p>
        <div class="actions">
            <asp:Button ID="btnRetryPayment" runat="server" Text="Retry Payment" CssClass="btn btn-primary" OnClick="btnRetryPayment_Click" />
            &nbsp&nbsp&nbsp&nbsp
            <asp:Button ID="btnReturnHome" runat="server" Text="Return to Home" CssClass="btn btn-primary" OnClick="btnReturnHome_Click" />
        </div>

    </div>

</asp:Content>
