﻿<%@ Page Title=" About US - LU Computers" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="bAboutUs.aspx.cs" Inherits="bAboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="background-box1"></div>
    <div class="container lu-desc-page customPlane">
        <ul class="breadcrumb">
            <li>
                <a href="/default.aspx">
                    <span class="glyphicon glyphicon-home"></span>&nbsp;Home
                </a>
            </li>
            <li class="active">About Us</li>
        </ul>
        <div class="page-main">
            <div class="media">
                <div class="media-left">
                    <img class="media-object img-thumbnail" src="<%= string.Concat(setting.ImgHost, "soft_img/app/1.jpg")%>" alt="...">
                </div>
                <div class="media-body note">
                    <p>
                        LU Computers is an established company providing computers and parts to home users and business in the US and Canada. We, having presences in Toronto and New York, aim to deliver fast and courteous service to our customers.
                    </p>
                    <p>
                        With our hard working, dedication and efficient operation, our sales volume has been increasing tremendously year over year, which permits us to establish ties with manufacturers and to negotiate prices with vendors. This price benefit is passed right on to you, our customers.
                    </p>
                    <p>
                        Being a dedicated computer builder since 1993, our technical team is of professional enthusiasts who are on the cutting edge of computing, and bring you the latest technology and stay tuned to what is the hot. Our accumulated knowledge enables us to deliver effective and prompt technical support.
                    </p>
                    <p>
                        LU Computers only stocks brand new and top quality name brand products. When you receive your dream PC, high-end or just "bare-bone", it is fully assembled, tested and well packed in our manufactory facility with 100-feet state-of-art computer assembly line. We stock adequate items to ensure prompt shipment.
                    </p>
                    <p>
                        LU Computers provides toll free lifetime support in addition to our standard warranty. We stand behind the products we sell, and if a problem arises after delivery we will take the necessary action to ensure the matter is resolved. We will do this fairly and professionally.
                    </p>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>
