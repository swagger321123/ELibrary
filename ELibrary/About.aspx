<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ELibrary.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" style="padding: 20px;">
        <!-- Library Information Card -->
        <div class="container" style="margin-top: 30px;">
            <div class="card" style="max-width: 800px; margin: 0 auto; padding: 20px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); border-radius: 8px;">
                <h2 style="text-align: center; color: #007bff;">Welcome to E-Library</h2>
                <p style="font-size: 1rem; line-height: 1.6; text-align: justify;">
                    Our digital library provides access to thousands of books across various genres. Whether you're looking for fiction, non-fiction, academic resources, or entertainment, we've got you covered. Our mission is to make reading accessible to everyone, anytime, anywhere.
                </p>
                <p style="font-size: 1rem; line-height: 1.6; text-align: justify;">
                    Join our community today and explore the world of knowledge at your fingertips!
                </p>
            </div>
        </div>

        <!-- Testimonials Section -->
        <div class="container" style="margin-top: 50px;">
            <h2 style="text-align: center; color: #007bff; margin-bottom: 30px;">What Our Users Say</h2>
            <div class="row">
                <!-- Testimonial 1 -->
                <div class="col-md-4 mb-4">
                    <div class="card" style="box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); border-radius: 8px; padding: 15px;">
                        <p style="font-size: 0.9rem; line-height: 1.6;">This library has been a game-changer for me. I can find all the books I need without leaving my home!</p>
                        <p style="font-weight: bold; font-size: 0.9rem; margin-top: 10px;">- Dikesh Kumar Singh</p>
                    </div>
                </div>

                <!-- Testimonial 2 -->
                <div class="col-md-4 mb-4">
                    <div class="card" style="box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); border-radius: 8px; padding: 15px;">
                        <p style="font-size: 0.9rem; line-height: 1.6;">The variety of books here is amazing. I love how easy it is to borrow and return books.</p>
                        <p style="font-weight: bold; font-size: 0.9rem; margin-top: 10px;">- Sagar Khadka</p>
                    </div>
                </div>

                <!-- Testimonial 3 -->
                <div class="col-md-4 mb-4">
                    <div class="card" style="box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); border-radius: 8px; padding: 15px;">
                        <p style="font-size: 0.9rem; line-height: 1.6;">I highly recommend this platform to anyone who loves reading. It's user-friendly and efficient.</p>
                        <p style="font-weight: bold; font-size: 0.9rem; margin-top: 10px;">- Rimal Dangol</p>
                    </div>
                </div>
            </div>

            <div class="row">
                <!-- Testimonial 4 -->
                <div class="col-md-4 mb-4">
                    <div class="card" style="box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); border-radius: 8px; padding: 15px;">
                        <p style="font-size: 0.9rem; line-height: 1.6;">The customer support team is very helpful and responsive. Great experience overall!</p>
                        <p style="font-weight: bold; font-size: 0.9rem; margin-top: 10px;">- Basanti Pathak</p>
                    </div>
                </div>

                <!-- Testimonial 5 -->
                <div class="col-md-4 mb-4">
                    <div class="card" style="box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); border-radius: 8px; padding: 15px;">
                        <p style="font-size: 0.9rem; line-height: 1.6;">I found some rare books here that I couldn't find anywhere else. Thank you, E-Library!</p>
                        <p style="font-weight: bold; font-size: 0.9rem; margin-top: 10px;">- Bibek Magar</p>
                    </div>
                </div>

                <!-- Testimonial 6 -->
                <div class="col-md-4 mb-4">
                    <div class="card" style="box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); border-radius: 8px; padding: 15px;">
                        <p style="font-size: 0.9rem; line-height: 1.6;">The interface is clean and intuitive. I love how everything is organized.</p>
                        <p style="font-weight: bold; font-size: 0.9rem; margin-top: 10px;">- Kripesh Lamichhane</p>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>