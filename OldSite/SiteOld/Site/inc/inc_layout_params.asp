
<%
'
' test
'
'
	const LAYOUT_RELEASE		=	true

'
'
'   1, CA;  2, US;  
'
'
    if SQLescape(Request.cookies("system_transition")) = "" then 
        Session("system_transition") = "1"
        Response.cookies("system_transition") = "1"
		Response.cookies("system_transition").expires = dateadd("d",365,now())
    else
        Session("system_transition") = Request.cookies("system_transition")
    end if

    Dim Current_System  :   Current_System = Session("system_transition")
    Dim CCUN            :   CCUN = Current_Currency_Unit(Current_System)  
	  
    Dim CurrentIsEbay  
    
	If SQLescape(request.Cookies("CurrentIsEbay")) = "" Then
		Response.Cookies("CurrentIsEbay") = false
		CurrentIsEbay = false
	else
		CurrentIsEbay = request.Cookies("CurrentIsEbay")
    End if
	
    Const CSCA      =   "1"
    Const CSUS      =   "2"
    



    const LAYOUT_CONTENT_TYPE   = "<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />"
    
    CONST LAYOUT_SCRIPT_FILES   = "<script language=""javascript"" src=""/js_css/jquery_lab/jquery-1.9.1.js""></script><script type=""text/javascript"" src=""/js_css/jquery_lab/jquery.flash.js""></script></script><script src=""/js/urchin.js"" type=""text/javascript""></script><script src=""/js_css/advflash.js"" type=""text/javascript""></script><script src=""/js_css/jquery_helper.js?r=e"" type=""text/javascript""></script><script src=""/js_css/jquery_lab/jQuery.cookie.js"" type=""text/javascript""></script><script src=""/js_css/jquery_lab/jQuery.float.js"" type=""text/javascript""></script><script src=""/js_css/jquery-ui-1.10.2.custom.min.js"" type=""text/javascript""></script>"
                                
    CONST LAYOUT_CSS_FILES      = "<link href=""/js_css/pre_lu.css"" rel=""stylesheet"" type=""text/css"" /><link href=""/js_css/ui-lightness/jquery-ui-1.10.2.custom.min.css"" rel=""stylesheet"" type=""text/css"" />"
	
	CONST LAYOUT_CSS_FILES_BACK	= "<link href=""/js_css/b_lu.css"" rel=""stylesheet"" type=""text/css"" />"
    CONST LAYOUT_LINK_FILES     = "<link rel=""shortcut icon"" href=""/favicon.ico"" />"
    
    CONST LAYOUT_KEYWORDS       = "<meta name=""keywords"" content=""audio, video, battery, dvd-r, dvd+r, dvd, PC, GeForce, Radeon, parallel, scsi, motherboard, core, duo, Pentium, drives, storage, monitors, SIMM, DIMM, site development, processor upgrades, cables, adapters, surge protectors, UPC, PC Cards, mobile computing, disks, cartridges, Pentium, Athlon, SDRAM, DDR, DDR2, DDR3, AM2, LGA775, computers, hardware, software, notebook, laptop, networking, flash, LCD, monitor, cpu, pda, memory, ram, digital, camera, modems, printers, scanners, cd, palmtop, purchase, ethernet, hubs, routers, cdrom drives, Internet, server, buy, order, inkjet, laser, pci, USB, PCI-E, PCI Express, secure, microSD, HDMI, HDCP, Widescreen, projector, DVI, Firewall, VISA, Mastercard, Amex, Paypal, secure, Canada, 3Com, AMD, Acer, Antec, AOpen, Asus, ATI, Adobe, Abit, BFG, BenQ, Canon, CoolerMaster, Corsair, Cisco, Coolit, Creative, Labs, Crucial, D-Tek, Danger Den, DFI, Dlink, D-link, Diamond, Drobo, ECS, Enlight, Epson, EVGA, Galaxy, Gigabyte, Hewlett Packard, HIS, Hitachi, HP, IBM, Intel, Kingston, Koolance, Kef, LG, Lian-Li, Lian, Li, Logitech, Linksys, LiteOn, Lite, On, Lexmark, Micron, Microsoft, MSI, Mitsumi, NGear, NEC, nVidia, OCZ, Optoma, Onkyo, Palit, Panasonic, Philips, Pioneer, Plextor, Promise, Razer, RedHat, Samsung, Seagate, Shuttle, Sapphire, Silverstone, Sony, Swiftech, Symantec, Supermicro, Tekram, Toshiba, Thermaltake, Thermalright, Vantec, Viewsonic, Western Digital, WD, XFX, Zalman""><meta name=""rebots"" content=""all"">"
    
    CONST LAYOUT_DESCRIPTION    = "<meta name=""description"" content=""LU Computers is an established company providing computers and parts to home users and business in the US and Canada. We, having presences in Toronto and New York, aim to deliver fast and courteous service to our customers."">"

	CONST NO_DATA_MATCH 	=	"<div style='line-height:50px; text-align:center;'>No Data Match </div>"
    '
    ' photo
    ' 
    CONST HTTP_PART_GALLERY     				= "/pro_img/COMPONENTS/"'"http://www.lucomputers.com/pro_img/COMPONENTS/"
	CONST HTTP_PART_GALLERY_CPU_LOGO_PATH		= "http://www.lucomputers.com/pro_img/logo/"
    CONST LAYOUT_HOST_URL						=		"/site/" 	' "http://www.lucomputers.com/"
	
    CONST LAYOUT_FindSpecialCashPriceComment 	= "SPECIAL CASH PRICE is promotional offer, valid on pay methods of cash, Interac, bank transfer, money order, etc.  Cash price does not waive sales taxes if applicable."


    ' CLIENT HOST	
    DIM  LAYOUT_HOST_IP         :       LAYOUT_HOST_IP       =   Request.ServerVariables("REMOTE_ADDR")
    Dim  LAYOUT_HOST_HTTP       :       LAYOUT_HOST_HTTP     =   Request.ServerVariables("SCRIPT_NAME")
    
	'response.write request.Cookies("customer_serial_no")
    DIM  LAYOUT_CCID            		:       LAYOUT_CCID          			=	SQLescape(request.Cookies("customer_serial_no"))
	'IF	 LAYOUT_CCID	=	""	THEN	LAYOUT_CCID	=	0
    DIM  LAYOUT_EBAY_ORDER_CODE 		:       LAYOUT_EBAY_ORDER_CODE			=	SQLescape(request.Cookies("ebay_order_code"))
	DIM  LAYOUT_ORDER_CODE 				:       LAYOUT_ORDER_CODE				=	GetCurrentOrder()
	DIM  LAYOUT_SHIPPING_STATE_CODE		:		LAYOUT_SHIPPING_STATE_CODE		=	SQLescape(request.Cookies("shipping_state_code"))
	DIM  LAYOUT_SHIPPING_COUNTRY_CODE	:		LAYOUT_SHIPPING_COUNTRY_CODE	=	SQLescape(request.Cookies("shipping_country_code"))
	
	DIM	 LAYOUT_CURRENT_ORDER_TYPE		:		LAYOUT_CURRENT_ORDER_TYPE		=	SQLescape(request.Cookies("CurrentOrder"))
	'test
	
	
	
	'----------------------------------------------------
	' card rate
	'----------------------------------------------------
	DIM LAYOUT_CARD_RATE		:		LAYOUT_CARD_RATE	=	1.022
	
	
	'----------------------------------------------------
	' warrary , win os 
	'----------------------------------------------------	
	CONST LAYOUT_WARRARY_GROUP_ID				=	"62" ' warrary_group_id 	 		=	"62"
	CONST LAYOUT_WARRARY_PRODUCT_ID				=	"6755"'warrary_product_id 		=	"6755"
	CONST LAYOUT_WINDOW_SYSTEM_GROUP_IDS		=	"[95][153]"'window_system_group_ids 	= "[95][153]"
	const LAYOUT_WARRARY_CATEGORY_ID			=	"201"'WARRARY_CATEGORY_ID 		="201"

	const LAYOUT_ORDER_LENGTH					=	6		'	the order char length
	CONST LAYOUT_SYSTEM_CODE_LENGTH				=	8		'	sys char length
	CONST LAYOUT_EBAY_CODE_LENGTH				=	12		'	ebay product char length.
	
	const LAYOUT_ONTARIO_ID						=	8		'	Onitario ID , lu in	
	const LAYOUT_ONTARIO_Code					=	"ON"		'	ontario Char , lu in	
	'----------------------------------------------------
	' need change payment
	'----------------------------------------------------
	CONST LAYOUT_PAY_METHOD_CARD				=	14	' this is payment ID of the credit.
	CONST LAYOUT_PAYPAL_METHOD_CARD				=	15	'	paypal payment ID
	CONST LAYOUT_PAYPAL_METHOD_CREDIT_CARD		=	25	'	paypal (direct payment) payment
	CONST LAYOUT_RATE_PAY_METHODS				=	"[14][15][20][21][25][23][24]"	'account amt by card rate.
	
	'----------------------------------------------------
	' pay pick up PAYMENT
	'----------------------------------------------------
	CONST LAYOUT_PAY_PICKUP_VALUE				=	21				'	pick up payment ID
	CONST LAYOUT_PAY_PICKUP_VALUE_s				=	"[21][22][23][24]"			'	pick up payment IDs
	'----------------------------------------------------
	' if country is US , then email , cash , pick up payment is not used
	'----------------------------------------------------
	CONST LAYOUT_NO_SUPPERT_US					=	"[16][18][21][22][23][24]"
	
	'----------------------------------------------------
	' 	pay result
	'----------------------------------------------------
	const    LAYOUT_PAYPAL_SUCCESS 				= 	2
	const    LAYOUT_PAYPAL_FAILURE 				= 	4
	const    LAYOUT_PAYPAL_NO_PAED 				= 	1
	const    LAYOUT_PAY_RECORD_METHOD_PAYPAL	= 	15      'need store PAY RECORD 
	
	
	'----------------------------------------------------
	' order status is finished on the pre web
	'----------------------------------------------------
	CONST	LAYOUT_ORDER_STATUS_FINISHED		=	7
	
	'----------------------------------------------------
	' default order status use the pre web.
	'----------------------------------------------------
	CONST	LAYOUT_ORDER_PRE_STATUS				=	8
	'----------------------------------------------------
	' default order status use the back web.
	'----------------------------------------------------
	CONST 	LAYOUT_ORDER_BACK_STATUS			=	12
	'----------------------------------------------------
	
	'
	'	HST STATE ID
	'
	CONST LAYOUT_HST_STATE_IDS					=	"[NB][NF][NS][ON][BC]"
	
	'----------------------------------------------------
	' phont format
	'----------------------------------------------------
	CONST	LAYOUT_PHONE_FORMAT					=	"Format: 555-777-8888"
	
	CONST LAYOUT_MANAGER_NAME					= 	"STEVENYAO@LUCOMPUTERS.COM"
	
	'
	' SHIPPING company is pickup
	'
	
	CONST LAYOUT_SHIPPING_COMPANY_PICKUP_ID		=	11
	
	CONST LAYOUT_ADV_COMMENT					=	"/adv_comment/"
	
	
	CONST LAYOUT_SPLIT_CHAR						=	"QIOZI"
	
	
	'----------------------------------------------------
	' the menu tree no display Category ID
	'----------------------------------------------------
	CONST LAYOUT_NONE_DISPLAY_PRODUCT_CATEGORY	= "201"
	
	
	CONST LAYOUT_NONE_SELECTED_IDS				= "4,1049,2036,6760,6765,8221"
	
	'----------------------------------------------------
	' default shipping company is low charge.
	'----------------------------------------------------
	CONST LAYOUT_SHIPPING_COMPANY_LOW_PRICE_ID 	= 1
	
	
	
	CONST JS_COMMA_EXCODE						=	"[Q]"
	
	
	'----------------------------------------------------
	' if it is a https , then convert to http
	'----------------------------------------------------	
	Call redirectHTTP(getCurrentPageName())
	
	
	
	
	
	Response.write "<script>"&vblf
	Response.write "js_currency_convert		=	"&getCurrencyConvert()&";"&vblf
	Response.write "js_card_rate			=	"&LAYOUT_CARD_RATE&";"&vblf
	Response.write "js_current_system_code 	= 	'"& Current_system &"';"&vblf
	Response.write "</script>"&vblf
	
%>
