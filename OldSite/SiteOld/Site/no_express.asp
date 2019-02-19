<%
Response.Buffer   =   True 
Response.ExpiresAbsolute = Now() - 2 
Response.Expires = -1 
Response.CacheControl = "no-cache" 
Response.AddHeader "Pragma", "No-Cache"
%>