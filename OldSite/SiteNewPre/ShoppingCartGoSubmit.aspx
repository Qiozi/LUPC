<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShoppingCartGoSubmit.aspx.cs" Inherits="ShoppingCartGoSubmit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="container" style="background:#74C774;padding:0px;">
       

               <div  style="padding-bottom:0px;background:#74C774; border:0px;padding:15px;">      
                 <ul class="list-inline" style="margin-left:2em;">
                    <li>
                        <div >
                            <a href="default.aspx" style="color:White;font-size:20px;"><span class="glyphicon glyphicon-home"></span> Home</a>                                 
                            <span class="glyphicon glyphicon-menu-right"></span>                                 
                        </div>                            
                    </li>
                   
                    <li>
                        <div style="font-size:20px; color:White;">ORDER SUBMIT</div>
                    </li>
                </ul>
            </div>
   
            <div style="background:white;margin: -1em 3em 3em 3em ;padding: 2em; min-height: 600px;">
            <strong>CONGRATULATIONS!</strong> 
            <p></p>

<p>You have successfully submitted your order!</p>
<p>A copy of your order was sent to: <strong><%= email %></strong></p>
<p>If you have paid by PayPal, you will receive your payment receipt via your PayPal associated email address.
</p>
<p>Thank you for shopping with lucomputers.com !</p>

 

<p>WHAT'S NEXT?</p>

<p>If you selected Bank Trasfer, Email Transfer, Money Order, Check as your pay method, please go ahead to make your payment.  We are waiting for your payment.  As soon as your payment arrives we will send you an email confirmation and process your order.  Please always contact us to inform about your payment status.
</p>
<p>Once your computer has been shipped we will email you tracking number.  You can also log in to <strong><a href="/">www.lucomputers.com</a></strong> and find your tracking number in your account.
</p>
<p>If you wish to inquire about status of your order, please log in <strong><a href="/">www.lucomputers.com</a></strong> and select Your Account to see details. </p>
            </div>
    </div>

    <%--<iframe src="http://www.lucomputers.com/q_admin/email_simple_invoice.aspx?order_code=<%= ORDER_CODE %>&email=<%= email %>&issend=true" name="iframe1" id="iframe1" style="width: 0px; height:0px;" frameborder="0" ></iframe>
--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>

