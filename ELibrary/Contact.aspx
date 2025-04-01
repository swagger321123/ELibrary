<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ELibrary.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" style="padding: 20px;">
        <!-- Contact Information Card -->
        <div class="container" style="margin-top: 50px;">
            <div class="card" style="max-width: 600px; margin: 0 auto; padding: 20px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); border-radius: 8px;">
                <h2 style="text-align: center; color: #007bff; margin-bottom: 20px;">Contact Us</h2>
                <p style="font-size: 1rem; line-height: 1.6; text-align: center;">
                    For any inquiries or feedback, feel free to reach out to us via email or social media.
                </p>
                <div style="text-align: center; margin-top: 20px;">
                    <p style="font-weight: bold; font-size: 1.1rem; color: #333;">Email:</p>
                    <p style="font-size: 1rem; color: #007bff;">evisionnepal@gmail.com</p>
                </div>
                <div style="text-align: center; margin-top: 20px;">
                    <p style="font-weight: bold; font-size: 1.1rem; color: #333;">Phone:</p>
                    <p style="font-size: 1rem; color: #007bff;">+977 9812345678</p>
                </div>
                <div style="text-align: center; margin-top: 20px;">
                    <p style="font-weight: bold; font-size: 1.1rem; color: #333;">Follow Us:</p>
                    <div style="display: flex; justify-content: center; gap: 15px; margin-top: 10px;">
                        <!-- Facebook Icon -->
                        <a href="https://www.facebook.com/evisionnepal" target="_blank" style="color: #1877F2; text-decoration: none;">
                            <i class="fab fa-facebook fa-2x"></i>
                        </a>
                        <!-- X (Twitter) Icon -->
                        <a href="https://twitter.com/evisionnepal" target="_blank" style="color: #1DA1F2; text-decoration: none;">
                            <i class="fab fa-twitter fa-2x"></i>
                        </a>
                        <!-- WhatsApp Icon -->
                        <a href="https://wa.me/+977-9878123456" target="_blank" style="color: #25D366; text-decoration: none;">
                            <i class="fab fa-whatsapp fa-2x"></i>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>