<%@ Page Title="User Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="ELibrary.UserDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/userdashboard.css" rel="stylesheet" />
    <div class="container mt-5">
        <!-- Logout Button -->
        <div class="row mb-4">
            <div class="col-md-12 text-end">
                <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="btnLogout_Click" />
            </div>
        </div>

        <!-- User Stats Section -->
        <div class="row mb-4">
            <div class="col-md-12">
                <div class="card text-center bg-light">
                    <div class="card-body">
                        <h3 class="card-title">Welcome, <%# GetUsername() %>!</h3>
                        <div class="row mt-3">
                            <div class="col-md-3">
                                <p class="card-text fw-bold">Balance</p>
                                <p class="card-text text-success">$<%# GetBalance() %></p>
                            </div>
                            <div class="col-md-3">
                                <p class="card-text fw-bold">Active Requests</p>
                                <p class="card-text"><%# GetActiveRequestsCount() %></p>
                            </div>
                            <div class="col-md-3">
                                <p class="card-text fw-bold">Borrowed Books</p>
                                <p class="card-text"><%# GetBorrowedBooksCount() %></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Card Buttons -->
        <div class="row">
            <!-- Browse Books Card -->
            <div class="col-md-4 mb-4">
                <div class="card-button">
                    <div>
                        <h5 class="card-title">Browse Books</h5>
                        <p class="card-text">Explore available books in the library.</p>
                    </div>
                    <div class="btn-actions">
                        <asp:Button ID="btnBrowseBooks" runat="server" Text="Go to Browse Books" CssClass="btn btn-primary" OnClick="btnBrowseBooks_Click" />
                    </div>
                </div>
            </div>

            <!-- Change Password Card -->
            <div class="col-md-4 mb-4">
                <div class="card-button">
                    <div>
                        <h5 class="card-title">Change Password</h5>
                        <p class="card-text">Update your account password securely.</p>
                    </div>
                    <div class="btn-actions">
                        <asp:Button ID="btnChangePassword" runat="server" Text="Go to Change Password" CssClass="btn btn-primary" OnClick="btnChangePassword_Click" />
                    </div>
                </div>
            </div>

            <!-- Borrowed Books Card -->
            <div class="col-md-4 mb-4">
                <div class="card-button">
                    <div>
                        <h5 class="card-title">View Borrowed Books</h5>
                        <p class="card-text">Manage your borrowed books and return them.</p>
                    </div>
                    <div class="btn-actions">
                        <asp:Button ID="btnBorrowedBooks" runat="server" Text="Go to Borrowed Books" CssClass="btn btn-primary" OnClick="btnBorrowedBooks_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>