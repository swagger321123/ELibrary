<%@ Page Title="User Activity" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserActivity.aspx.cs" Inherits="ELibrary.UserActivity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/useractivity.css" rel="stylesheet" />
    <div class="container mt-5">
        <h2 class="text-center mb-4">Activity for User: <%# Eval("Username") %></h2>

        <!-- Borrowed Books Section -->
        <div class="card mb-4">
            <div class="card-header bg-primary text-white">
                Borrowed Books
            </div>
            <div class="card-body">
                <asp:Repeater ID="rptBorrowedBooks" runat="server">
                    <ItemTemplate>
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' 
                                    alt='<%# Eval("Title") %>' 
                                    class="img-thumbnail" 
                                    onerror="this.onerror=null; this.src='<%# ResolveUrl("~/Images/default.jpg") %>';" />
                            </div>
                            <div class="col-md-8">
                                <h5><%# Eval("Title") %></h5>
                                <p>By <%# Eval("Author") %></p>
                                <p>Issued On: <%# Eval("IssueDate", "{0:MMM dd, yyyy}") %></p>
                                <p>Return Date: <%# Eval("ReturnDate", "{0:MMM dd, yyyy}") %></p>
                                <p>Status: 
                                    <span class="badge <%# Convert.ToBoolean(Eval("IsReturned")) ? "bg-success" : "bg-warning" %>">
                                        <%# Convert.ToBoolean(Eval("IsReturned")) ? "Returned" : "Borrowed" %>
                                    </span>
                                </p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="lblNoBorrowed" runat="server" Text="No borrowed books." CssClass="alert alert-info" Visible="false" />
            </div>
        </div>

        <!-- Pending Requests Section -->
        <div class="card mb-4">
            <div class="card-header bg-warning text-white">
                Pending Requests
            </div>
            <div class="card-body">
                <asp:Repeater ID="rptPendingRequests" runat="server">
                    <ItemTemplate>
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' 
                                    alt='<%# Eval("Title") %>' 
                                    class="img-thumbnail" 
                                    onerror="this.onerror=null; this.src='<%# ResolveUrl("~/Images/default.jpg") %>';" />
                            </div>
                            <div class="col-md-8">
                                <h5><%# Eval("Title") %></h5>
                                <p>By <%# Eval("Author") %></p>
                                <p>Requested On: <%# Eval("RequestedAt", "{0:MMM dd, yyyy}") %></p>
                                <p>Status: 
                                    <span class="badge bg-warning">
                                        Pending
                                    </span>
                                </p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="lblNoPending" runat="server" Text="No pending requests." CssClass="alert alert-info" Visible="false" />
            </div>
        </div>
    </div>
</asp:Content>