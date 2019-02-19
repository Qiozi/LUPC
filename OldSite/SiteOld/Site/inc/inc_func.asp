<%

'
'	ConvertDate(str)
' 	GetStrFromFile(FileName)
'	ReadTextFile(filePath,CharSet) 
'	IsEmail(str)
'
'
'
'
'
'
'
''
'


'
'	Convert Date 
'
function ConvertDate(str)
	ConvertDate = str
'	if str <> "" then
'		str =cdate(str)
'		dim d, m , y
'		d = day(str)
'		m = month(str)
'		y = year(str)
'	
'					
'		select case m
'			case 1
'				ConvertDate = "Jan"
'			case 2
'				ConvertDate = "Feb"
'			case 3
'				ConvertDate = "Mar"
'			case 4
'				ConvertDate = "Apr"
'			case 5
'				ConvertDate = "May"
'			case 6
'				ConvertDate = "Jun"
'			case 7
'				ConvertDate = "Jul"
'			case 8
'				ConvertDate = "Aug"
'			case 9
'				ConvertDate = "Sept"
'			case 10
'				ConvertDate = "Oct"
'			case 11
'				ConvertDate = "Nov"
'			case 12
'				ConvertDate = "Dec"
'			case else
'				ConvertDate = "December"
'		end select 
'		
'		ConvertDate = ConvertDate & " " & d & ", " & y
'	end if
end function



' ---------------------------------------------------------------------------------
Function GetStrFromFile(FileName)
' ---------------------------------------------------------------------------------
	dim out : out = ""
	GetStrFromFile = out
	if (not isEmpty(FileName)) and (not isNull(FileName)) and (Len(FileName) > 3) then 
		if Right(FileName, 5) = ".html" then
			FileName = Server.MapPath(FileName)

				dim fs : set fs=Server.CreateObject("Scripting.FileSystemObject")
				if fs.FileExists(FileName)=true then
					set fs = nothing
					'response.Write(FileName)
					GetStrFromFile = ReadTextFile(FileName, "utf-8")
				else
					set fs = nothing
				end if
				set fs = nothing
			'end if
		end if ' html file
	end if
	
	
End Function




' ---------------------------------------------------------------------------------
Function ReadTextFile(filePath,CharSet) 
' ---------------------------------------------------------------------------------
       dim stm 
       set stm=Server.CreateObject("adodb.stream")  
       stm.Type=1 'adTypeBinary，按二进制数据读入 
       stm.Mode=3 'adModeReadWrite ,这里只能用3用其他会出错 
       stm.Open  
       stm.LoadFromFile filePath 
       stm.Position=0 '把指针移回起点 
       stm.Type=2 '文本数据 
       stm.Charset=CharSet 
       ReadTextFile = stm.ReadText 
       stm.Close  
       set stm=nothing  
End Function 



' ---------------------------------------------------------------------------------
Function IsEmail(str)
' ---------------------------------------------------------------------------------
	Dim ieRegEx
	Dim blnResult : blnResult = False
	if str <> "" and Len(str) > 0 then
		Set ieRegEx = New RegExp
		ieRegEx.Pattern = "^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
		blnResult = ieRegEx.Test(str)
	end if
	IsEmail = blnResult
End Function
%>