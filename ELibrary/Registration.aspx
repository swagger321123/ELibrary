<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="ELibrary.Registration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration - E-Library</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <style>
        body {
            background-color: #f8f9fa;
        }
        .registration-container {
            max-width: 600px;
            margin: 50px auto;
            padding: 20px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .registration-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            font-weight: bold;
        }
        .btn-register {
            width: 100%;
            background-color: #007bff;
            border: none;
        }
        .btn-register:hover {
            background-color: #0056b3;
        }
        .error-message {
            color: red;
            text-align: center;
            margin-top: 10px;
        }
        .validator-error {
            color: red;
            font-size: 0.875em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="registration-container">
            <h2>Register</h2>
            <div class="form-group">
                <label for="txtUsername">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required." CssClass="validator-error" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtFullname">Full Name</label>
                <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control" placeholder="Enter full name"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFullname" ErrorMessage="Full name is required." CssClass="validator-error" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="ddlGender">Gender</label>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                    <asp:ListItem Value="M">Male</asp:ListItem>
                    <asp:ListItem Value="F">Female</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="txtPhoneNo">Phone Number</label>
                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" placeholder="Enter phone number"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhoneNo" ErrorMessage="Phone number is required." CssClass="validator-error" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." CssClass="validator-error" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." CssClass="validator-error" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtConfirmPassword">Confirm Password</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirm password"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm password is required." CssClass="validator-error" Display="Dynamic" />
                <asp:CompareValidator runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" ErrorMessage="Passwords do not match." CssClass="validator-error" Display="Dynamic" />
            </div>
            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary btn-register" OnClick="btnRegister_Click" />
            <div class="error-message">
                <asp:Label ID="lblErrorMessage" runat="server" Text="" Visible="false"></asp:Label>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS and dependencies -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>