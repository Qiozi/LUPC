<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>LU Computers</title>
</head>

<body>
<%
dim fid 
fid = request("fid")

select case fid
	case "1":
		response.Redirect("http://www.lucomputers.com/adv_page/asus_201107/asus_201107.asp")
	case "2":
		response.Redirect("http://www.lucomputers.com/adv_page/asus_201107/list3.asp")
	case "3":
		response.Redirect("http://www.lucomputers.com/adv_page/asus_201107/list2.asp")
	case "4":
		response.Redirect("http://www.lucomputers.com/adv_page/asus_201107/list1.asp")
    case "5":
		response.Redirect("http://www.lucomputers.com/site/product_list.asp?page_category=1&class=2&cid=22&keywords=Intel")
    case "6":
		response.Redirect("http://www.lucomputers.com/adv_page/adv_index.asp?file_name=adv_9.html")
end select
%>
</body>
</html>
