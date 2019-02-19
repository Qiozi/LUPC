<%

    dim ONLINE				:	ONLINE = TRUE	'	a global flag to indicate if all systems are a go
	dim LUC_LUC_EMAIL_LIVE_SEND		:	LUC_EMAIL_LIVE_SEND		= True 'actually send emails 
	dim LUC_EMAIL_ORDER_ADMIN	:	LUC_EMAIL_ORDER_ADMIN	= "sales@lucomputers.com"
	dim LUC_EMAIL_HELO			:	LUC_EMAIL_HELO			= "mail.lucomputers.com"
	dim LUC_EMAIL_HOST			:	LUC_EMAIL_HOST			= "mail.lucomputers.com" '"207.182.248.46"
	dim LUC_EMAIL_SMTP_LOGIN	:	LUC_EMAIL_SMTP_LOGIN	= "sales@lucomputers.com"
	dim LUC_EMAIL_SMTP_PWD		:	LUC_EMAIL_SMTP_PWD		= "5calls2day"
	
' ---------------------------------------------------------------------------------
Function LUWebSendEmail(sTo1, sTo2, sFrom, sFromName, sReplyTo, sBcc, sSubject, sIsHtml, sBody)
' ---------------------------------------------------------------------------------
	dim strTo1		:	strTo1		= sTo1
	dim strTo2		:	strTo2		= sTo2
	dim strFrom		:	strFrom		= sFrom
	dim strFromName	:	strFromName	= sFromName
	dim strReplyTo	:	strReplyTo	= sReplyTo
	dim strBcc		:	strBcc		= sBcc
	dim strSubject	:	strSubject	= sSubject
	dim IsHtml		:	IsHtml		= sIsHtml
	dim strBody		:	strBody		= sBody

	dim allOK : allOK = true
	if isEmpty(strTo1) or isNull(strTo1)			then 	allOK = false			end if
	if isEmpty(strSubject) or isNull(strSubject)	then	allOK = false			end if
	if isEmpty(IsHtml) or isNull(isHtml)			then	isHtml = false			end if
	if isEmpty(strBody) or isNull(strBody)			then	allOK = false			end if

	
	if LUC_EMAIL_LIVE_SEND then
		if allOK then
			dim objMail
			On Error Resume Next
			set objMail = Server.CreateObject("Persits.MailSender")
			if isObject(objMail) then
				''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
				objMail.Helo		= LUC_EMAIL_HELO
				objMail.Host		= LUC_EMAIL_HOST
				objMail.Username	= LUC_EMAIL_SMTP_LOGIN
				objMail.Password	= LUC_EMAIL_SMTP_PWD
				objMail.From		= strFrom						' Specify sender's address
				objMail.FromName	= strFromName				' Specify sender's name
				objMail.AddAddress	strTo1
				if (not isEmpty(strTo2)) and (not isNull(strTo2)) and (strTo2 <> "") then
					objMail.AddAddress	strTo2
				end if
				objMail.AddReplyTo	strReplyTo
				if (not isEmpty(strBcc)) and (not isNull(strBcc)) and (strBcc <> "") then
					objMail.AddBcc		strBcc
				end if
				objMail.isHTML		= IsHtml
				objMail.Subject		= strSubject
				objMail.Body		= Replace(strBody, vbLf, VbCrLf)
				objMail.Send
				''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
			end if 'isObject
			If Err <> 0 Then ' error occurred
 	           response.write  Err.Description
            end if
			On Error Goto 0
			set objMail = nothing
		end if
	end if
End Function


 %>