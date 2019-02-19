<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="bContactUs.aspx.cs" Inherits="bContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<style>
    h4{ background: #467417;  padding: 10px; color: #FFFFA2;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div class="container" style="background:#74C774;padding:0px; color: #996633">       
        <ol class="breadcrumb" style="margin-top:0em;background:#74C774;margin-left: 2em;">
          <li><a href="default.aspx" style="color:White;"><h3>Home</h3></a></li>
          <li class="active" style="font-size:24px; color:White;">Contact Us</li>
        </ol>
        <div  style="background-color:White; margin:-1em 3em 3em 3em;padding:50px;">

            <h4>Welcome to contact us! </h4>

            <div class="media">
             
              <div class="media-body">
    
                   	<p>
                    For questions, comments or suggestions, please feel free to contact us by telephone or email.</p>
      	                    <p>To inquire about products, check prices or for assistance placing an order, our friendly and knowledgeable sales representatives are always standing by.</p>
      	                    <p>Need technical support, trace packages, claim shipping damages, etc., please call our toll free support line or send us emails.</p>

      	                    <p>Please remember that you can always get a hold of us in any case any time through our email addresses. </p>
 
                   
                </div>
                <div class="media-right">
   
                  <img class="" src="https://lucomputers.com/soft_img/app/canada_map.gif" alt="...">
                  <img class="" src="https://lucomputers.com/soft_img/app/USA_map.gif" alt="...">
                </div>
            </div>


            <h4>General Information</h4>

            <div class="media">
             
               <div class="media-body">
                    <address>
                      <strong>LU Computers</strong><br>
                      1875 Leslie Street, Unit 24<br>
                      Toronto, Ontario M3B 2M5 <br><br>
                      <abbr title="Phone">P:</abbr> (Toll free) 1866.999.7828 <br>
                      <abbr title="Phone">P:</abbr> (Local) 416.446.7743 <br>
                    </address>

                    <address>
                      <a href="mailto:sales@lucomputers.com">sales@lucomputers.com </a>
                    </address>      
                </div>
                <div class="media-right">   
                  <img class="" src="https://lucomputers.com/soft_img/app/map2.gif" alt="...">
                </div>
            </div>

             <h4>Business Hours</h4>
             <div>
                 <p>
              	    Monday - Friday: <strong>10.00 AM - 7.00 PM Eastern Time</strong>
                 </p>
                <%-- <p>
                    Saturday:<strong> 11.00 AM - 4.00 PM Eastern Time </strong>
                 </p>--%>
             </div>
             <h4>Directions</h4>
             <div>
                <p>
                    <strong>From 404 (Don Valley Parkway):</strong> Please change to 401 West and take first exit to Leslie Street south. Please pass the second light after you turn onto Leslie Street. About 150M further a yellow sign of 1875 is on your left hand side. 1875 Leslie Street, Unit 24 is LU Computers. </p>
	
  	 <p><strong>From 401 West:</strong> Please take exit Leslie Street south. Leslie Street exit is next to Bayview Ave exit. Please pass the second light after you turn onto Leslie Street. About 150M further a yellow sign of 1875 is on your left hand side. 1875 Leslie Street, Unit 24 is LU Computers. </p>
	
  	 <p><strong>From 401 East:</strong> Please take exit Leslie Street south. Leslie Street exit is next to exit 404 (Don Valley Parkway). Please pass the second light after you turn onto Leslie Street. About 150M further a yellow sign of 1875 is on your left hand side. 1875 Leslie Street, Unit 24 is LU Computers. </p>
	
  	 <p>By Sheppard Subway:</strong> Please get off at Leslie Street. Please take TTC 51 south to York Mills Road and walk back (north) about 150M. A yellow sign of 1875 is next to Esso on your right hand side. 1875 Leslie Street, Unit 24 is LU Computers. </p>
	
  	 <p><strong>By Younge Subway:</strong> Please get off at York Mills Road. Please take TTC east bound to Leslie Street and turn left at Leslie Street. Please go north about 150M. A yellow sign of 1875 is next to Esso on your right hand side. 1875 Leslie Street, Unit 24 is LU Computers. </p>
	
  	 <p><strong>Local driving or by bus:</strong> LU Computers is located at northeast intersection of York Mills Road and Leslie Street. About 150M away from York Mills Road is a yellow address sign of 1875 next to Esso. 1875 Leslie Street, Unit 24 is LU Computers.

                </p>
             </div>

             <h4>Sales Department</h4>
             <div>
                <p>By Telephone: Toll free (866) 999-7828 GTA local (416) 446-7743<br />By Email: <a href="mailto:sales@lucomputers.com">sales@lucomputers.com </a> </p>
                <p> If you like to place an order for a computer system it is best to obtain a system SKU number or Quote Number before you make a sales call. Submitting your order online is safe and secure; this will make your order being processed promptly and accurately.                 </p>
             </div>

             <h4>Technical Support</h4>
             <div>
                <p>By Telephone: Toll free (866) 999-7828 GTA local (416) 446-7743<br />By Email: <a href="mailto:support@lucomputers.com">support@lucomputers.com</a> </p>
                <p>Most of your questions may have already been answered before on our website, please check FAQ and Help Desk; if you cannot find the answer on FAQ or Help Desk, please send us an email or make a phone call to us. </p>
             </div>

             <h4>Check Order Status</h4>
             <div>
                <p>Most Customers will find all necessary Order Management features available directly from the secured "My Account" area of our website.</p>
                <p>Check your email for shipping confirmation;<br />
                    Track your order with your order number;<br />
                    By Telephone: Toll free (866) 999-7828 GTA local (416) 446-7743<br />
                    By Email: <a href="mailto:sales@lucomputers.com">sales@lucomputers.com</a>
                </p>                
             </div>

             <h4>Customer Service</h4>
             <div>
                <p>
                 	By Telephone: Toll free (866) 999-7828 GTA local (416) 446-7743<br />
                    By Email: <a href="mailto:support@lucomputers.com">support@lucomputers.com</a>
	            </p>
                <p>
  	                Your must fill up one of the following forms:<br />
  	                Return Merchandise Authorization (RMA) Form
                </p>
             </div>

             <h4>Corporate Sales / Reseller Department</h4>
             <div>
                <p>
                    By Telephone: Toll free (866) 999-7828 GTA local (416) 446-7743<br />
                    By Email: <a href="mailto:sales@lucomputers.com">sales@lucomputers.com</a>
                </p>
             </div>

             <h4>Native Indian Customers</h4>
             <div>
                <p>
                    By Telephone: Toll free (866) 999-7828 GTA local (416) 446-7743<br />
                    By Email: <a href="mailto:sales@lucomputers.com">sales@lucomputers.com</a>
                </p>
                <p> Your Indian Status Card / Form is acceptable</p>
             </div>

             <h4>Government and Education Purchases</h4>
             <div>
                <p>
                    By Telephone: Toll free (866) 999-7828 GTA local (416) 446-7743<br />
                    By Email: <a href="mailto:sales@lucomputers.com">sales@lucomputers.com</a>
                </p>
             </div>

              <h4>Customer Feedback</h4>
             <div>
                <p>
                    LU Computers regards all Customer Feedback as vitally important information for improving our business practices. We encourage all of our Customers to drop us a line, so that we can address specific customer issues
             </div>
        </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server"></asp:Content>