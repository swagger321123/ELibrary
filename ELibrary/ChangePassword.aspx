<%@ Page Title="Change Password" Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ELibrary.ChangePassword" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../CSS/changepassword.css" rel="stylesheet" />
    <style>
        /* Internal CSS */
        .form-container {
            max-width: 400px;
            margin: 50px auto;
            padding: 20px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .form-group label {
            font-weight: bold;
        }

        .btn-primary {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2 class="text-center mb-4">Change Password</h2>
            <div class="form-group">
                <label for="txtUsername">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtOldPassword">Old Password</label>
                <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your old password" required />
            </div>
            <div class="form-group">
                <label for="txtNewPassword">New Password</label>
                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your new password" required />
            </div>
            <div class="form-group">
                <label for="txtConfirmPassword">Confirm New Password</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm your new password" required />
            </div>
            <div class="form-group text-center mt-4">
                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-primary" OnClick="btnChangePassword_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary ms-2" OnClick="btnCancel_Click" />
            </div>
            <div class="text-danger mt-3">
                <asp:Label ID="lblErrorMessage" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>