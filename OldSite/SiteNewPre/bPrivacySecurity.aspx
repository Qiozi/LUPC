<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="bPrivacySecurity.aspx.cs" Inherits="bPrivacySecurity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        h4
        {
            background: #467417;
            padding: 10px;
            color: #FFFFA2;
        }
        td
        {
            border: 1px solid #ccc;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background: #74C774; padding: 0px; color: #996633">
        <ol class="breadcrumb" style="margin-top: 0em; background: #74C774; margin-left: 2em;">
            <li><a href="default.aspx" style="color: White;">
                <h3>
                    Home</h3>
            </a></li>
            <li class="active" style="font-size: 24px; color: White;">Privacy Security</li>
        </ol>
        <div style="background-color: White; margin: -1em 3em 3em 3em; padding: 50px;">
            <h4>
                How private is my information?
            </h4>
            <div>
                LU Computerss understands our customers?wishes for privacy, so we handle all the
                information you provide to us accordingly. Our conduct with your information will
                in no way involve a third party organization. Also, as LU Computerss is independent
                with no links to any other companies, the information you provide will not be directed
                to a parent, sub, or partner company. Therefore, the conduct and organization of
                LU Computerss firmly guarantees that YOUR INFORMATION WILL NOT BE SOLD TO OR SHARED
                WITH ANOTHER ORGANIZATION. LU Computers retains your information in our database
                for the sole purpose of making the purchasing process faster. If you wish to remove
                your information from our database, you may notify us by email or telephone.
            </div>
            <h4>
                How secure is my information?
            </h4>
            <div>
                We have taken every possible measure to ensure that purchasing online at LU Computerss
                is safe. We are currently using the industry standard SSL software to protect our
                customers?billing and personal information. This technology encrypts the data representing
                your information as it travels through the Internet space before it is decrypted
                in our systems. Once in our systems, we transfer your information to a database
                that is not linked to the Internet so that it is protected from hackers. Finally,
                only our accountant and no one else will handle your billing information. For more
                detailed and technical information of our security system, please e-mail us at webmaster@lucomputers.com
                Further inquiries about the SSL software may be answered at http://www.verisign.com.
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server"></asp:Content>