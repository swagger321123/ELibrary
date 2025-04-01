<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ELibrary._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../CSS/adminbooklist.css" rel="stylesheet" />
    <main>
       <%-- <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle" style="align-items: center">Welcome to Library</h1>
        </section>
        <div>
            <img src="library.jpg" style="width: 90vw; height: 100vh; object-fit: cover;" alt="Library Background" />
        </div>--%>

         <!-- Dark Overlay and Centered Text -->
        <div style="position: relative; width: 100%; height: 100vh; display: flex; justify-content: center; align-items: center;">
            <!-- Image with Dark Overlay -->
            <img src="library.jpg" style="width: 100%; height: 100vh; object-fit: cover; position: absolute; top: 0; left: 0; z-index: 1;" alt="Library Background" />
            <div style="position: absolute; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0, 0, 0, 0.5); z-index: 2;"></div>
            
            <!-- Centered Heading -->
            <h1 id="aspnetTitle" style="z-index: 3; color: white; font-size: 3rem; text-align: center; margin: 0;">Welcome to Library</h1>
        </div>

        <div class="row" id="defaultBookList" runat="server">
            <!-- Books added to the default page will be dynamically displayed here -->
        </div>
    </main>
</asp:Content>