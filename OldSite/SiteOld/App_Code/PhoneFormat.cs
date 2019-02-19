using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PhoneFormat
/// </summary>
public class PhoneFormat
{
	public PhoneFormat()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string Format(string phone)
    {
        if (string.IsNullOrEmpty(phone))
            return "";
        if (phone.Trim().IndexOf(" ") > -1)
            return phone.Trim().Replace(" ", "-");
        return phone;
    }
}