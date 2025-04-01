<%@ Page Title="Update User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateUser.aspx.cs" Inherits="ELibrary.UpdateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/updateuser.css" rel="stylesheet" />
    <div class="update-user-container">
        <h2>Update User Details</h2>
        <div class="form-group">
            <label for="txtUserID">User ID</label>
            <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtUsername">Username</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username" required></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtFullname">Full Name</label>
            <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control" placeholder="Enter full name" required></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="ddlGender">Gender</label>
            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                <asp:ListItem Value="M">Male</asp:ListItem>
                <asp:ListItem Value="F">Female</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="txtPhoneNo">Phone Number</label>
            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" placeholder="Enter phone number" required></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email" required></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="ddlUserType">User Type</label>
            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control">
                <asp:ListItem Value="Admin">Admin</asp:ListItem>
                <asp:ListItem Value="User">User</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:CheckBox ID="chkIsActive" runat="server" CssClass="form-check-input" />
            <label for="chkIsActive">Is Active</label>
        </div>
        <div class="form-group">
            <asp:Button ID="btnUpdate" runat="server" Text="Update User" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
        </div>
    </div>
    <script src="../JS/updateuser.js"></script>
</asp:Content>