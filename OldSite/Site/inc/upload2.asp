<%
'�������ɷ���������ϴ�(2.0)���޸Ķ�����
'OPTION EXPLICIT
Server.ScriptTimeOut=50000
%>
<!--#include FILE="UpLoadClass2.asp"-->
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

FileType_all="jpg/gif"    '�����ļ�����ĸ�������
SavePath_all="../pro_img/"        '���÷������ļ�����·��
MaxSize_all=30*1048576            '�����ļ�����ֽ���
'�����ϴ�����
set upload=new UpLoadClass
	upload.FileType=FileType_all  '�����ļ�����ĸ�������
	upload.SavePath=SavePath_all  '���÷������ļ�����·��
	upload.MaxSize=MaxSize_all    '�����ļ�����ֽ���
	'�򿪶���
	upload.open() 
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>LU Computers</title>
</head>

<body>
<table width="650" border="0" align="center" cellpadding="2" cellspacing="0" bgcolor="#FFFFFF" style="border:1px solid #999999; display: none;" >
  <tr> 
    <td><blockquote>
	<%
	'----�г������ϴ��˵��ļ���ʼ----
	intCount=0
    FileSize_all=0

	for intTemp=1 to Ubound(upload.FileItem)
		
	  '��ȡ����ļ��ؼ����ƣ�ע��FileItem�±��1��ʼ
	   formName=upload.FileItem(intTemp)

'**********************************************************

'	�ڴ˰�����д�����ݿ⣬ ����������ϴ��� ����ԭ���� δ�ģ�
'   ����Ա��	�����.Qiozi : Qiozi@msn.com

'**********************************************************

	call updateDatabase()

'-----------------------------------------------------------------------------------------------

	  if upload.Form(formName&"_Err")>=0 then
	
		'��ʾԴ�ļ�·�����ļ���
		response.write "<br>"&upload.form(formName&"_Path")&upload.form(formName&"_Name")

		'��ʾ�ļ���С���ֽ�����
		response.write "(" &File_Size_sum(upload.form(formName&"_Size"))& ")  => "



		'��ʾĿ���ļ�·�����ļ���
		if upload.Form(formName&"_Err")=0 then
			response.Write upload.SavePath & upload.form(formName)&" "
		end if
		'��ʾ�ļ�����״̬
		select case upload.form(formName&"_Err")
			case -1:
				response.write "û���ļ��ϴ�<br>"
			case 0:
				response.write "�ļ��ϴ��ɹ�<br>"
				intCount=intCount+1
				FileSize_all=FileSize_all + upload.form(formName&"_Size")
			case 1:
				response.write "�ļ�̫�󣬾ܾ��ϴ�<br>"
			case 2:
				response.write "�ļ���ʽ���ԣ��ܾ��ϴ�<br>"
			case 3:
				response.write "�ļ�̫���Ҹ�ʽ���ԣ��ܾ��ϴ�<br>"
		end select
'       if upload.Form(formName&"_Err") =1 then '����ļ��ϴ��ɹ�
'       ���������ݿ�����Ӽ�¼
'       ��ǰ�ļ��ͻ����ļ�����   upload.form(formName&"_Name")
'       ��ǰ�ļ����������ļ����� upload.form(formName)
'       ��ǰ�ļ���С��B����     upload.form(formName&"_Size")
'       end if
      end if
	next
	'----�г������ϴ��˵��ļ�����---- 
    FileSize_all_1=FileSize_all
    FileSize_all_2=FileSize_all
	times=fix((timer()-startime)*1000) '����ִ��ʱ�䣨���룩
	if intCount > 0 then
	'response.write "<br><hr>����"&intCount&"���ļ��ϴ��ɹ�! �ϴ����ļ���" &File_Size_sum(FileSize_all_1)& "��ƽ���ϴ��� �ʣ�" &File_Size_sum(FileSize_all_2*1000/times)& "<font color=red>/S</font>&nbsp;"
	else
	' response.write "û���ļ��ϴ��ɹ�����Ӧ����Ū���ˣ�&nbsp;"
	end if
	' response.write "[<a href=""javascript:history.back();"">�� ��</a>]"
	%>
	</blockquote></td>
  </tr>
</table>
</body>
</html>
<%
'�ͷ��ϴ�����
set upload=nothing
%>