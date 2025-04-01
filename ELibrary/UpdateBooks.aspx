<%@ Page Title="Update Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateBooks.aspx.cs" Inherits="ELibrary.UpdateBooks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/updatebooks.css" rel="stylesheet" />
    <div class="update-books-container">
        <h2>Update Book Details</h2>
        <div class="form-group">
            <label for="txtBookID">Book ID</label>
            <asp:TextBox ID="txtBookID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtTitle">Title</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Enter book title" required></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtAuthor">Author</label>
            <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" placeholder="Enter author name" required></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtDescription">Description</label>
            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Enter book description"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="fileImage">Book Image</label>
            <asp:FileUpload ID="fileImage" runat="server" CssClass="form-control" accept=".jpg,.jpeg,.png" />
            <asp:Label ID="lblCurrentImage" runat="server" CssClass="current-image-label"></asp:Label>
        </div>
        <div class="form-group">
            <label for="txtPrice">Price</label>
            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter price" TextMode="Number" step="0.01" required></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:CheckBox ID="chkIsFree" runat="server" CssClass="form-check-input" />
            <label for="chkIsFree">Is Free</label>
        </div>
        <div class="form-group">
            <asp:Button ID="btnUpdate" runat="server" Text="Update Book" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
        </div>
    </div>
    <script src="../JS/updatebooks.js"></script>
</asp:Content>