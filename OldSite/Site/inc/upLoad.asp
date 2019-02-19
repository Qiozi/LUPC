<%
'本程序由风声无组件上传(2.0)版修改而来。
'OPTION EXPLICIT
Server.ScriptTimeOut=50000
%>
<!--#include FILE="UpLoadClass.asp"-->
<%
dim upload,formName,intCount,FileSize_all,intTemp,FileSize,mm,startime,times,FileSize_all_1,FileSize_all_2,FileType_all,SavePath_all,MaxSize_all
startime=timer()

Function File_Size_sum(FileSize)
	     if FileSize >=1099511627776 then 
          FileSize=round(FileSize/1099511627776, 2)
		  mm="TB"
	     elseif FileSize >=1073741824 then 
          FileSize=round(FileSize/1073741824, 2)
		  mm="GB"
	     elseif FileSize >=1048576 then 
          FileSize=round(FileSize/1048576, 2)
		  mm="MB"
	     elseif FileSize >=1024 then 
          FileSize=round(FileSize/1024, 2)
		  mm="KB"
	     elseif FileSize >=0 then 
          FileSize=round(FileSize, 2)
		  mm="&nbsp;B"
         end if
         File_Size_sum="<font color=blue>" &FileSize& "</font><font color=red>" &mm& "</font>"
End Function

FileType_all="wmv/jpg/rar/zip/rar/doc/xls/sql/gif/pdf/rm/mp3/html/htm/wav/mid/midi/ra/avi/mpg/mpeg/asf/asx/mov/exe"    '设置文件允许的附件类型
SavePath_all="../UploadFilename/"        '设置服务器文件保存路径
MaxSize_all=30*1048576            '设置文件最大字节数
'建立上传对象
set upload=new UpLoadClass
	upload.FileType=FileType_all  '设置文件允许的附件类型
	upload.SavePath=SavePath_all  '设置服务器文件保存路径
	upload.MaxSize=MaxSize_all    '设置文件最大字节数
	'打开对象
	upload.open() 
%>
<html>
<head>
<title>LU Computers</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<link href="basic.css" rel="stylesheet" type="text/css">
</head>

<body>
<table width="650" border="0" align="center" cellpadding="2" cellspacing="0" bgcolor="#FFFFFF" style="border:1px solid #999999; display: none;" >
  <tr> 
    <td><blockquote>
	<%
	'----列出所有上传了的文件开始----
	intCount=0
    FileSize_all=0

	for intTemp=1 to Ubound(upload.FileItem)
		
	  '获取表单文件控件名称，注意FileItem下标从1开始
	   formName=upload.FileItem(intTemp)

'**********************************************************

'	在此把数据写入数据库， 整个无组件上传， 还是原样， 未改；
'   程序员：	吴添辉.Qiozi : Qiozi@msn.com

'**********************************************************

	call updateDatabase()

'-----------------------------------------------------------------------------------------------

	  if upload.Form(formName&"_Err")>=0 then
	
		'显示源文件路径与文件名
		response.write "<br>"&upload.form(formName&"_Path")&upload.form(formName&"_Name")

		'显示文件大小（字节数）
		response.write "(" &File_Size_sum(upload.form(formName&"_Size"))& ")  => "



		'显示目标文件路径与文件名
		if upload.Form(formName&"_Err")=0 then
			response.Write upload.SavePath & upload.form(formName)&" "
		end if
		'显示文件保存状态
		select case upload.form(formName&"_Err")
			case -1:
				response.write "没有文件上传<br>"
			case 0:
				response.write "文件上传成功<br>"
				intCount=intCount+1
				FileSize_all=FileSize_all + upload.form(formName&"_Size")
			case 1:
				response.write "文件太大，拒绝上传<br>"
			case 2:
				response.write "文件格式不对，拒绝上传<br>"
			case 3:
				response.write "文件太大且格式不对，拒绝上传<br>"
		end select
'       if upload.Form(formName&"_Err") =1 then '如果文件上传成功
'       可以向数据库里添加记录
'       当前文件客户端文件名：   upload.form(formName&"_Name")
'       当前文件服务器上文件名： upload.form(formName)
'       当前文件大小（B）：     upload.form(formName&"_Size")
'       end if
      end if
	next
	'----列出所有上传了的文件结束---- 
    FileSize_all_1=FileSize_all
    FileSize_all_2=FileSize_all
	times=fix((timer()-startime)*1000) '程序执行时间（毫秒）
	if intCount > 0 then
	'response.write "<br><hr>共有"&intCount&"个文件上传成功! 上传的文件共" &File_Size_sum(FileSize_all_1)& "，平均上传速 率：" &File_Size_sum(FileSize_all_2*1000/times)& "<font color=red>/S</font>&nbsp;"
	else
	' response.write "没有文件上传成功！你应该是弄错了？&nbsp;"
	end if
	' response.write "[<a href=""javascript:history.back();"">返 回</a>]"
	%>
	</blockquote></td>
  </tr>
</table>
</body>
</html>
<%
'释放上传对象
set upload=nothing
%>