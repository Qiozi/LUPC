<%
'//	//	//	//	//	//	//	//	//	//	//	//	//	//	//	//	
'
'	author: 	qiozi@msn.com
'	date:		15/10/2006 17:25:30
'	descation:	product_detailClass
'
'
'//	//	//	//	//	//	//	//	//	//	//	//	//	//	//	//

class code_helperClass

	public function getCustomerCode(s)
		getCustomerCode = "U"&right("000000" & s, 6)		
	end function 
	
	public function getProductCode(s)
		getProductCode = right("000000" & s, 6)
	end function
	
	public function getVendorCode(s)
		getVendorCode = right("00000000" & s, 8)
	end function

	public function getSystemCode(s)
		getSystemCode = right("000000" & s, 6)
	end function
	
	public function getOrderCode(s)
		getOrderCode = right("000000" & s, 6)
	end function
	
	public function getCompatibilityTemplete(s)
		getCompatibilityTemplete = right("000000" & s, 6)
	end function 
	
	public function getRND2(length)
		dim num1
		dim rndnum
		Randomize
		
		getRND2 = Int((999999-100000+1)*rnd + 100000)
		'Do While Len(rndnum)<length
'		num1=CStr(Chr((57-49)*rnd+49))
'		rndnum=rndnum&num1
'		loop
'
'		if rndnum < "1" & getZone(5) then getRND(length)
'		getRND = rndnum
   	end function 
	
		public function getRND(length)
		dim num1
		dim rndnum
		Randomize
		
		getRND = Int((99999999-10000000+1)*rnd + 10000000)
		'Do While Len(rndnum)<length
'		num1=CStr(Chr((57-49)*rnd+49))
'		rndnum=rndnum&num1
'		loop
'
'		if rndnum < "1" & getZone(5) then getRND(length)
'		getRND = rndnum
   	end function 
	
	function getZone(i)
		getZone = ""
		for n=1 to i
			getZone = getZone & "0"
		next
		
	end function
	
	public function getOrderNewCode()
		getOrderNewCode =	getRND2(6)
	end function 

end class


%>