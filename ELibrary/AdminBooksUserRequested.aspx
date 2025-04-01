<%@ Page Title="User Book Requests" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminBooksUserRequested.aspx.cs" Inherits="ELibrary.AdminBooksUserRequested" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/adminbooksuserrequested.css" rel="stylesheet" />
    <div class="container mt-5">
        <h2 class="text-center mb-4">User Book Requests</h2>

        <!-- Search Bar -->
        <div class="row mb-4">
            <div class="col-md-6 offset-md-3">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by User or Book Title..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>

        <!-- Book Requests Table -->
        <div class="card">
            <div class="card-body">
                <asp:Repeater ID="rptRequests" runat="server" OnItemCommand="rptRequests_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>User</th>
                                    <th>Book Title</th>
                                    <th>Requested On</th>
                                    <th>Status</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("Username") %> (<%# Eval("Fullname") %>)<br />
                                <small>Email: <%# Eval("Email") %></small>
                            </td>
                            <td>
                                <%# Eval("Title") %><br />
                                <small>Author: <%# Eval("Author") %></small>
                            </td>
                            <td><%# Eval("RequestedAt", "{0:MMM dd, yyyy}") %></td>
                            <td>
                                <span class="badge <%# Convert.ToBoolean(Eval("IsIssued")) ? "bg-success" : "bg-warning" %>">
                                    <%# Convert.ToBoolean(Eval("IsIssued")) ? "Issued" : "Pending" %>
                                </span>
                            </td>
                            <td>
                                <asp:Button ID="btnIssue" runat="server" 
                                    Text='<%# Convert.ToBoolean(Eval("IsIssued")) ? "Issued" : "Issue Book" %>'
                                    CssClass='<%# Convert.ToBoolean(Eval("IsIssued")) ? "btn btn-secondary disabled" : "btn btn-success" %>'
                                    CommandName="IssueBook" 
                                    CommandArgument='<%# Eval("RequestID") %>'
                                    Enabled='<%# !Convert.ToBoolean(Eval("IsIssued")) %>' />
                            </td>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

                <!-- No Results Message -->
                <asp:Label ID="lblNoResults" runat="server" Text="No results found." CssClass="alert alert-info mt-3" Visible="false" />
            </div>
        </div>
    </div>
</asp:Content>