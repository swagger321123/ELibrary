<%@ Page Title="All Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllUsers.aspx.cs" Inherits="ELibrary.AllUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/allusers.css" rel="stylesheet" />
    <div class="container mt-5">
        <h2 class="text-center mb-4">All Users</h2>

        <!-- Search Bar -->
        <div class="row mb-4">
            <div class="col-md-6 offset-md-3">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by Username or Fullname..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>

        <!-- User Cards Container -->
        <div class="row" id="user-list-container">
            <asp:Repeater ID="rptUsers" runat="server" OnItemCommand="rptUsers_ItemCommand">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card h-100">
                            <div class="card-body d-flex flex-column">
                                <h5 class="card-title"><%# Eval("Username") %></h5>
                                <p class="card-text">Name: <%# Eval("Fullname") %></p>
                                <p class="card-text">Gender: <%# Eval("Gender") %></p>
                                <p class="card-text">Email: <%# Eval("Email") %></p>
                                <p class="card-text">
                                    User Type: 
                                    <span class="badge <%# Eval("UserType").ToString() == "Admin" ? "bg-danger" : "bg-primary" %>">
                                        <%# Eval("UserType") %>
                                    </span>
                                </p>
                                <p class="card-text">Status: 
                                    <span class="badge <%# Convert.ToBoolean(Eval("IsActive")) ? "bg-success" : "bg-secondary" %>">
                                        <%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Inactive" %>
                                    </span>
                                </p>
                                <p class="card-text">Joined: <%# Eval("CreatedAt", "{0:MMM dd, yyyy}") %></p>
                                <div class="mt-auto btn-actions">
                                    <asp:Button ID="btnViewActivity" runat="server" 
                                        Text="View Activity" 
                                        CssClass="btn btn-info me-2" 
                                        PostBackUrl='<%# "~/UserActivity.aspx?UserID=" + Eval("UserID") %>' />
                                    <asp:Button ID="btnUpdate" runat="server" 
                                        Text="Update" 
                                        CssClass="btn btn-warning me-2" 
                                        PostBackUrl='<%# "~/UpdateUser.aspx?UserID=" + Eval("UserID") %>' />
                                    <asp:Button ID="btnDelete" runat="server" 
                                        Text="Delete" 
                                        CssClass="btn btn-danger" 
                                        CommandName="DeleteUser" 
                                        CommandArgument='<%# Eval("UserID") %>' />
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