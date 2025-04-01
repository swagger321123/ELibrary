<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="ELibrary.Help" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex; justify-content: center; align-items: center; height: 100vh; background-color: #f8f9fa;">
        <div style="width: 600px; background: #fff; border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); padding: 20px; text-align: center;">
            <h2 style="margin-bottom: 20px; font-size: 1.5rem; color: #333;">Frequently Asked Questions</h2>
            
            <!-- Question 1 -->
            <div style="margin-bottom: 15px; background: #f1f1f1; border-radius: 4px; padding: 10px; cursor: pointer;" onclick="toggleAnswer('answer1')">
                <p style="font-weight: bold; margin: 0;">How do I register for an account?</p>
            </div>
            <div id="answer1" style="display: none; background: #e9ecef; padding: 10px; border-radius: 4px; margin-bottom: 15px;">
                <p style="margin: 0;">To register, email your details at evisionnepal@gmail.com</p>
            </div>

            <!-- Question 2 -->
            <div style="margin-bottom: 15px; background: #f1f1f1; border-radius: 4px; padding: 10px; cursor: pointer;" onclick="toggleAnswer('answer2')">
                <p style="font-weight: bold; margin: 0;">How can I borrow a book?</p>
            </div>
            <div id="answer2" style="display: none; background: #e9ecef; padding: 10px; border-radius: 4px; margin-bottom: 15px;">
                <p style="margin: 0;">You can browse the available books and click the "Borrow" button next to the book you want.</p>
            </div>

            <!-- Question 3 -->
            <div style="margin-bottom: 15px; background: #f1f1f1; border-radius: 4px; padding: 10px; cursor: pointer;" onclick="toggleAnswer('answer3')">
                <p style="font-weight: bold; margin: 0;">How do I add my balance?</p>
            </div>
            <div id="answer3" style="display: none; background: #e9ecef; padding: 10px; border-radius: 4px; margin-bottom: 15px;">
                <p style="margin: 0;">Send your details at evisionnepal@gmail.com</p>
            </div>

            <!-- Question 4 -->
            <div style="margin-bottom: 15px; background: #f1f1f1; border-radius: 4px; padding: 10px; cursor: pointer;" onclick="toggleAnswer('answer4')">
                <p style="font-weight: bold; margin: 0;">What if I forget my password?</p>
            </div>
            <div id="answer4" style="display: none; background: #e9ecef; padding: 10px; border-radius: 4px; margin-bottom: 15px;">
                <p style="margin: 0;">To reset your password, send your details at evisionnepal@gmail.com</p>
            </div>

            <!-- Question 5 -->
            <div style="margin-bottom: 15px; background: #f1f1f1; border-radius: 4px; padding: 10px; cursor: pointer;" onclick="toggleAnswer('answer5')">
                <p style="font-weight: bold; margin: 0;">How can I contact support?</p>
            </div>
            <div id="answer5" style="display: none; background: #e9ecef; padding: 10px; border-radius: 4px; margin-bottom: 15px;">
                <p style="margin: 0;">You can contact support by visiting the "Contact Us" page and use information there to contact.</p>
            </div>
        </div>
    </div>

    <script>
        function toggleAnswer(answerId) {
            var answer = document.getElementById(answerId);
            if (answer.style.display === "none") {
                answer.style.display = "block";
            } else {
                answer.style.display = "none";
            }
        }
    </script>
</asp:Content>