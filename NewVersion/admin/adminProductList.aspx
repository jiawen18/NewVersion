<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="adminProductList.aspx.cs" Inherits="NewVersion.admin.admin_pages.adminProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" SelectMethod="GridView1_GetData">

        </asp:GridView>
    </form>
</asp:Content>
