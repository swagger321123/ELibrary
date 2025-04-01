<%@ Page Title="Return Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminReturnBooks.aspx.cs" Inherits="ELibrary.AdminReturnBooks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/adminreturnbooks.css" rel="stylesheet" />
    <div class="container mt-5">
        <h2 class="text-center mb-4">Return Books</h2>
        <div class="row">
            <div class="col-md-12">
                <asp:Repeater ID="rptIssuedBooks" runat="server" OnItemCommand="rptIssuedBooks_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>User</th>
                                    <th>Book Title</th>
                                    <th>Issue Date</th>
                                    <th>Due Date</th>
                                    <th>Status</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='<%# (DateTime.Parse(Eval("ReturnDate").ToString()) < DateTime.Today && !Convert.ToBoolean(Eval("IsReturned"))) ? "expired" : "" %>'>
                            <td><%# Eval("Username") %> (<%# Eval("Fullname") %>)</td>
                            <td><%# Eval("Title") %></td>
                            <td><%# Eval("IssueDate", "{0:MMM dd, yyyy}") %></td>
                            <td><%# Eval("ReturnDate", "{0:MMM dd, yyyy}") %></td>
                            <td>
                                <%# Convert.ToBoolean(Eval("IsReturned")) ? 
                                    "<span class='badge bg-success'>Returned</span>" : 
                                    "<span class='badge bg-danger'>Not Returned</span>" %>
                            </td>
                            <td>
                                <asp:Button ID="btnReturn" runat="server" 
                                    Text='<%# Convert.ToBoolean(Eval("IsReturned")) ? "Returned" : "Return Book" %>'
                                    CssClass='<%# Convert.ToBoolean(Eval("IsReturned")) ? "btn btn-secondary disabled" : "btn btn-danger" %>'
                                    CommandName="ReturnBook" 
                                    CommandArgument='<%# Eval("IssueID") %>' 
                                    Enabled='<%# !Convert.ToBoolean(Eval("IsReturned")) %>' />
                            </td>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>