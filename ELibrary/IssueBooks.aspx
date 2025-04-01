<%@ Page Title="Issue Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IssueBooks.aspx.cs" Inherits="ELibrary.IssueBooks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/issuebooks.css" rel="stylesheet" />
    <div class="issue-books-container">
        <h2>Issue Book to User</h2>
        <div class="form-group">
            <label for="ddlUser">Select User</label>
            <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control" DataTextField="Username" DataValueField="UserID"></asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="ddlBook">Select Book</label>
            <asp:DropDownList ID="ddlBook" runat="server" CssClass="form-control" DataTextField="Title" DataValueField="BookID"></asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="txtIssueDate">Issue Date</label>
            <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control" TextMode="Date" required></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtReturnDate">Return Date</label>
            <asp:TextBox ID="txtReturnDate" runat="server" CssClass="form-control" TextMode="Date" required></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="btnIssue" runat="server" Text="Issue Book" CssClass="btn btn-primary" OnClick="btnIssue_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
        </div>
    </div>
    <script src="../JS/issuebooks.js"></script>
</asp:Content>