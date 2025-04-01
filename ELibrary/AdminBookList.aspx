<%@ Page Title="Admin Book List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminBookList.aspx.cs" Inherits="ELibrary.AdminBookList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/adminbooklist.css" rel="stylesheet" />
    <div class="container mt-5">
        <h2 class="text-center mb-4">Admin Book List</h2>

        <!-- Search Bar (ASP Controls) -->
        <div class="row mb-4">
            <div class="col-md-6 offset-md-3">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by Title or Author..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>

        <!-- Book Cards Container -->
        <div class="row" id="book-list-container">
            <asp:Repeater ID="rptBooks" runat="server" OnItemCommand="rptBooks_ItemCommand">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card h-100">
                            <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' 
                                alt='<%# Eval("Title") %>' 
                                class="card-img-top" 
                                onerror="this.onerror=null; this.src='<%# ResolveUrl("~/Images/default.jpg") %>';" />
                            <div class="card-body d-flex flex-column">
                                <h5 class="card-title"><%# Eval("Title") %></h5>
                                <p class="card-text">By <%# Eval("Author") %></p>
                                <p class="card-text text-success fw-bold">$<%# Eval("Price", "{0:F2}") %></p>
                                <div class="card-description">
                                    <p><%# Eval("Description") %></p>
                                </div>
                                <div class="mt-auto btn-actions">
                                    <asp:Button ID="btnToggleDisplay" runat="server" 
                                        Text='<%# Convert.ToBoolean(Eval("IsOnDefaultPage")) ? "Remove from Homepage" : "Display on Homepage" %>'
                                        CssClass="btn btn-sm btn-primary me-2" 
                                        CommandName="ToggleDisplay" 
                                        CommandArgument='<%# Eval("BookID") %>' />
                                    <asp:Button ID="btnUpdate" runat="server" 
                                        Text="Update" 
                                        CssClass="btn btn-sm btn-warning me-2" 
                                        PostBackUrl='<%# "~/UpdateBooks.aspx?BookID=" + Eval("BookID") %>' />
                                    <asp:Button ID="btnDelete" runat="server" 
                                        Text="Delete" 
                                        CssClass="btn btn-sm btn-danger" 
                                        CommandName="DeleteBook" 
                                        CommandArgument='<%# Eval("BookID") %>' />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <!-- No Results Message -->
            <asp:Label ID="lblNoResults" runat="server" Text="No results found." CssClass="alert alert-info col-md-12 mt-3" Visible="false" />
        </div>
    </div>
</asp:Content>