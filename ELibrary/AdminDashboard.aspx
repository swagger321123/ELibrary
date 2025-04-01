<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="ELibrary.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/admindashboard.css" rel="stylesheet" />
    <div class="container mt-5">
        <!-- Logout Button -->
        <div class="row mb-4">
            <div class="col-md-12 text-end">
                <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="btnLogout_Click" />
            </div>
        </div>

        <!-- Admin Dashboard Cards -->
        <div class="row">
            <!-- User Registration Card -->
            <div class="col-md-3 mb-4">
                <div class="card text-center h-100">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">User Registration</h5>
                        <p class="card-text">Register new users.</p>
                        <asp:Button ID="btnUserRegistration" runat="server" Text="Go to Registration" CssClass="btn btn-primary mt-auto" OnClick="btnUserRegistration_Click" />
                    </div>
                </div>
            </div>

            <!-- Add Books Card -->
            <div class="col-md-3 mb-4">
                <div class="card text-center h-100">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">Add Books</h5>
                        <p class="card-text">Add new books to the library.</p>
                        <asp:Button ID="btnAddBooks" runat="server" Text="Go to Add Books" CssClass="btn btn-primary mt-auto" OnClick="btnAddBooks_Click" />
                    </div>
                </div>
            </div>

            <!-- Admin Book List Card -->
            <div class="col-md-3 mb-4">
                <div class="card text-center h-100">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">Admin Books</h5>
                        <p class="card-text">View and manage all books.</p>
                        <asp:Button ID="btnAdminBookList" runat="server" Text="Go to Admin Books" CssClass="btn btn-primary mt-auto" OnClick="btnAdminBookList_Click" />
                    </div>
                </div>
            </div>

            <!-- All Users Card -->
            <div class="col-md-3 mb-4">
                <div class="card text-center h-100">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">All Users</h5>
                        <p class="card-text">View all user details.</p>
                        <asp:Button ID="btnAllUsers" runat="server" Text="Go to All Users" CssClass="btn btn-primary mt-auto" OnClick="btnAllUsers_Click" />
                    </div>
                </div>
            </div>

            <!-- Admin Return Books Card -->
            <div class="col-md-3 mb-4">
                <div class="card text-center h-100">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">Return Books</h5>
                        <p class="card-text">Manage returned books.</p>
                        <asp:Button ID="btnAdminReturnBooks" runat="server" Text="Go to Return Books" CssClass="btn btn-primary mt-auto" OnClick="btnAdminReturnBooks_Click" />
                    </div>
                </div>
            </div>

            <!-- Add Balance Card -->
            <div class="col-md-3 mb-4">
                <div class="card text-center h-100">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">Add Balance</h5>
                        <p class="card-text">Add balance to user accounts.</p>
                        <asp:Button ID="btnAddBalance" runat="server" Text="Go to Add Balance" CssClass="btn btn-primary mt-auto" OnClick="btnAddBalance_Click" />
                    </div>
                </div>
            </div>

            <!-- User Requested Books Card -->
            <div class="col-md-3 mb-4">
                <div class="card text-center h-100 position-relative">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">User Requests</h5>
                        <p class="card-text">View user book requests.</p>
                        <asp:Button ID="btnUserRequests" runat="server" Text="Go to Requests" CssClass="btn btn-primary mt-auto" OnClick="btnUserRequests_Click" />
                    </div>
                    <!-- Notification Badge -->
                    <span id="requestBadge" runat="server" class="badge bg-danger position-absolute top-0 end-0 translate-middle" style="margin-top: 10px; margin-right: 10px;"><%# GetRequestCount() %></span>
                </div>
            </div>
        </div>
    </div>
</asp:Content>