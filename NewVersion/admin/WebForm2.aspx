<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="NewVersion.admin.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Name</th>
                    <th>Price</th>
                    <th>Quantity</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("Name") %></td>
                <td><%# Eval("Price") %></td>
                <td><%# Eval("Quantity") %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
