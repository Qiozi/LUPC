
<%

'dim begin_timer_sc
'begin_timer_sc = timer()

dim current_tmp_order_code

if len(current_tmp_order_code) <> 6 then 
	current_tmp_order_code	= GetCookiesOrderCode()
end if

%>
<!--#include file="validate_order_code.asp"-->
<!--#include file="../change_http.asp"-->
<!--#include file="string_helper.asp"-->
<!--#include file="../inc/conn.asp"-->
<!--#include file="code_helperClass.asp"-->
<!--#include file="client_helper.asp"-->
<!--#include file="getCode.asp"-->
<!--#include file="customer_send_msg.asp"-->
<%

'---------------------------------------------------
'	系统名称
'---------------------------------------------------
Application("system_name") = "LU Computer"

'---------------------------------------------------
'	validate helper class
'---------------------------------------------------
	set encode = new string_helper
	set code_helper = new code_helperClass
	set client = new client_helper
	set getCode = new getCodeClass
	
	function closeClass
		set encode = nothing
		set code_helper = nothing
		set client = nothing
		set getCode  = nothing
	end function
	
'---------------------------------------------------
'	var 
'---------------------------------------------------
dim qiozi_null
qiozi_null = "qiozi_null"
	
'----------------------------------------------------
'	page file name
'----------------------------------------------------
dim page_name
page_name = request.ServerVariables("script_name")


dim sql_system_category, current_system_category
current_system_category = Session("current_system_category")
sql_system_category = " system_category_serial_no="&current_system_category
if current_system_category = "" then 
	current_system_category = 1
end if




'----------------------------------------------------
'	money unit name
'----------------------------------------------------
dim punit,price_unit, price_unit_2
punit ="$"
if current_system_category = 2 then 
	price_unit = "US"
	price_unit_2 = "USD"
else
	price_unit = "CA"
	price_unit_2 = "CAD"
end if

function getSysSKU()
	
end function

'----------------------------------------------------
'	email
'----------------------------------------------------

dim temp_MailServerUserName, temp_MailServerPassWord, temp_send, temp_fromname,temp_appendtext,temp_bak_email
temp_MailServerUserName="xiaowu021@126.com"
temp_MailServerPassWord = "9867054"
temp_send="smtp.126.com"
temp_fromname="LU COMPUTERS"
temp_appendtext = "This is a mail of HTML type"
temp_bak_email = "xiaowu021@126.com"
	'response.write temp_bak_email

dim email_user, email_password
email_user = "quote@lucomputers.com"
email_password = "2cold2swim"
'----------------------------------------------------
'	图片路径
'----------------------------------------------------
dim desktop_image_folder, laptop_image_folder, part_image_folder
desktop_image_folder = "/pro_img/SYSTEMS/"
laptop_image_folder = "/pro_img/LAPTOPSS/"
part_image_folder = "/pro_img/COMPONENTS/"


'----------------------------------------------------
' 费用计算
'----------------------------------------------------
dim charge_path
charge_path = "AccountCharge.aspx?SavePrice=1&tmp_code="

'----------------------------------------------------
' 产品类型
'----------------------------------------------------
dim product_type_part, product_type_noebook , product_type_system
product_type_part=1
product_type_noebook = 3
product_type_system = 2

'----------------------------------------------------
' 要变化的支付方式
'----------------------------------------------------
dim pay_method_card
pay_method_card = 14

dim paypal_method_card
paypal_method_card = 15

dim rate_pay_methods
rate_pay_methods = "[14][15][20][25]"
'----------------------------------------------------
' 如果是美国，email , cash 不能用
'----------------------------------------------------
dim no_suppert_us
no_suppert_us = "[16][18]"

'----------------------------------------------------
' 订单前台完成状态
'----------------------------------------------------
dim order_status_finished
order_status_finished = 7

'----------------------------------------------------
' 订单前台默认状态
'----------------------------------------------------
dim order_pre_status
order_pre_status = 8
'----------------------------------------------------
' 订单后台默认状态
'----------------------------------------------------
dim order_back_status
order_back_status = 12
'----------------------------------------------------
' paypal支付方式
'----------------------------------------------------
dim pay_pal_value
pay_pal_value = 15

const    HELPER_PAYPAL_SUCCESS = 2
const    HELPER_PAYPAL_FAILURE = 4
const    HELPER_PAYPAL_NO_PAED = 1
const    HELPER_PAY_RECORD_METHOD_PAYPAL= 15      '付款纪录类型
'----------------------------------------------------
' paypal支付方式 22
'----------------------------------------------------
const PAYPAL_PAYMENTS  = "[14][25]"
'----------------------------------------------------
' pay pick up支付方式
'----------------------------------------------------
dim pay_pickup_value
pay_pickup_value = 21
'----------------------------------------------------


' 网站路径
'----------------------------------------------------
dim tureurl
tureurl="http://www.lucomputers.com/"
'tureurl="http://localhost/"

'----------------------------------------------------
' 电话例实
'----------------------------------------------------
dim phone_format
phone_format="Format：555-777-8888"


'----------------------------------------------------
' 信用卡价格比率
'----------------------------------------------------
dim card_rate, new_card_rate
card_rate = 1.022

'----------------------------------------------------
' LU 公司所在洲
'----------------------------------------------------
dim Ontario_id
Ontario_id = 8

'----------------------------------------------------
' 最低运费默认公司
'----------------------------------------------------
dim shipping_company_low_price_id
shipping_company_low_price_id = 1

'----------------------------------------------------
' cpu logo图片所在路径
'----------------------------------------------------
dim cpu_logo_path
cpu_logo_path = "/pro_img/logo/"

'----------------------------------------------------
' cpu logo图片所在路径
'----------------------------------------------------
dim right_manager
right_manager = "STEVENYAO@LUCOMPUTERS.COM"

dim pcie_ati, pcie_nvidia

pcie_ati = "234"
pcie_nvidia = "233"

'----------------------------------------------------
' 当前网页文件名
'----------------------------------------------------
dim current_page_name
set ch = new client_helper
current_page_name =  lcase( ch.onlyPageName )
set ch  = nothing
'----------------------------------------------------
' change https to http
'----------------------------------------------------
'call redirectHTTP(current_page_name, httpPage)
 
'----------------------------------------------------
' 当前网页自定义默认值
'----------------------------------------------------
dim pageLeftDefaultContent, PageMainDefaultContent, PageRightDefaultContent
pageLeftDefaultContent = ""
PageMainDefaultContent = ""
PageRightDefaultContent= ""

set rs = conn.execute("select * from tb_right where right_page='default'")
if not rs.eof then 
	pageLeftDefaultContent = rs("left_content")
	PageMainDefaultContent = rs("main_content")
	PageRightDefaultContent= rs("right_content")
end if
rs.close : set rs = nothing
'----------------------------------------------------
' 把当前http改为https
'----------------------------------------------------
dim https_path_var, https_path_var_URL
https_path_var_URL= lcase(Request.ServerVariables("SERVER_NAME"))
if https_path_var_URL="www.lucomputers.com" or https_path_var_URL="lucomputers" then
	https_path_var = "https://www.lucomputers.com/"
else
	https_path_var = ""
end if
'----------------------------------------------------
' 目录排除的类，（不显示）
'----------------------------------------------------
dim none_display_product_category
none_display_product_category = "201"

'----------------------------------------------------
' paypal Return Path
'----------------------------------------------------
dim paypal_return_path_file
paypal_return_path_file = "http://www.lucomputers.com/shopping_cheOut_paypal.asp"


'----------------------------------------------------
' paypal Return Path
'----------------------------------------------------
	dim EMAIL_HOST			:	EMAIL_HOST			= "mail.lucomputers.com" '"207.182.248.46"
	dim EMAIL_SMTP_LOGIN	:	EMAIL_SMTP_LOGIN	= "sales@lucomputers.com"
	dim EMAIL_SMTP_PWD		:	EMAIL_SMTP_PWD		= "5calls2day"
	
'----------------------------------------------------
' warrary , win os 
'----------------------------------------------------	
dim warrary_group_id 	: 	warrary_group_id 	=	"62"
dim warrary_product_id 	: 	warrary_product_id	=	"6755"
dim window_system_group_ids 	: 	window_system_group_ids = "[95][153]"
const WARRARY_CATEGORY_ID ="201"
'----------------------------------------------------
' images path
'----------------------------------------------------
const HOST_APP_IMG	=	"/images/"

const HOST_URL	= 	"/"
const PAGE_LEFT_FILE_NAME	= 	"/left_menu_tpl.html"

const NONE_SELECTED_IDS = "4,1049,2036,6760,6765,8221"
const NONE_SELECTED_ID = 1049
const NONE_SELECTED_TITLE	=	"None Selected"

const EBAY_VIEW_PAGE = "http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item="


'/////////////////////////////////////////////////////////////////////////////////
'
dim sql_not_issue
sql_not_issue = ""
if session("user") = right_manager  and  right_manager <> "" then
    sql_not_issue = " or issue =0 "
end if
%>