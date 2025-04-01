<%@ Page Title="Add Balance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBalance.aspx.cs" Inherits="ELibrary.AddBalance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/addbalance.css" rel="stylesheet" />
    <div class="container mt-5">
        <div class="add-balance-container card">
            <div class="card-body">
                <h2 class="card-title text-center mb-4">Add Balance to User</h2>
                <div class="form-group">
                    <label for="ddlUsers">Select User</label>
                    <asp:DropDownList ID="ddlUsers" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- Select User --" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUsers" ErrorMessage="Please select a user." CssClass="text-danger" Display="Dynamic" />
                </div>
                <div class="form-group">
                    <label for="txtAmount">Amount ($)</label>
                    <asp:TextBox ID="txtAmount" min="0" runat="server" CssClass="form-control" placeholder="Enter amount" TextMode="Number" step="0.01" required></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAmount" ErrorMessage="Amount is required." CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtAmount" 
                        ValidationExpression="^\d+(\.\d{1,2})?$" 
                        ErrorMessage="Invalid amount. Use format: 10 or 10.50" 
                        CssClass="text-danger" 
                        Display="Dynamic" />
                </div>
                <div class="form-group text-center">
                    <asp:Button ID="btnAddBalance" runat="server" Text="Add Balance" CssClass="btn btn-primary me-2" OnClick="btnAddBalance_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 text-center d-block" />
            </div>
        </div>
    </div>
</asp:Content>