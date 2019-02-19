using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for XmlStore
/// </summary>
public class XmlStore
{
    XmlHelper XH = new XmlHelper();
    const string CA_STATE_FILE="ca_state.xml";
    const string US_STATE_FILE="us_state.xml";
    const string PAY_METHODS_FILE = "pay_method.xml";
    const string SHIPPING_COMPANY_FILE = "shipping_company.xml";
    const string ASSDINGED_TO_COMMENT_FILE = "assdinged_to_comment.xml";
    const string CATEGORY_NAME_FILE = "PartValidCategoryName.xml";
    const string ORDER_PAY_STATUS_FILE = "order_pay_status.xml";
    const string PART_GROUP_COMMENT_FILE = "partGroupComment.xml";
    const string PRE_STATUS_FILE = "pre_status.xml";
    const string FACTURE_STATE_FILE = "facture_state.xml";

	public XmlStore()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region state 
    public DataTable FindStateByCountry(CountryCategory cc)
    {
        switch (cc)
        {
            case CountryCategory.CA:
               return XH.GetForXmlFileStore(CA_STATE_FILE);
            case CountryCategory.US:
                return XH.GetForXmlFileStore(US_STATE_FILE);
            //case CountryCategory.other :
            //    return new DataTable();
        }
        return null;
    }

    //public DataTable FindStateByCountry(int cid)
    //{
    //    return FindStateByCountry(CountryCategoryHelper.GetCountryCategoryByValue(cid));
    //}

    #endregion

    #region Pay methods
    public DataTable FindPayMethods()
    {
        return XH.GetForXmlFileStore(PAY_METHODS_FILE);
    }
    #endregion

    #region Shipping Company

    public DataTable FindShippingCompany()
    {
        return XH.GetForXmlFileStore(SHIPPING_COMPANY_FILE);
    }

    public string FindShippingCompanyName(int id)
    {
        DataTable dt = FindShippingCompany();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (id.ToString() == dt.Rows[i]["shipping_company_id"].ToString())
                return dt.Rows[i]["shipping_company_name"].ToString();
        }
        return "None";
    }
    #endregion

    #region assdinged to comment
    public DataTable FindAssdingedToComment()
    {
        return FindAssdingedToComment(true);
    }

    public DataTable FindAssdingedToComment(bool have_null)
    {
        DataTable dt = XH.GetForXmlFileStore(ASSDINGED_TO_COMMENT_FILE);
        if (have_null)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "";
            dt.Rows.InsertAt(dr, 0);
        }
        return dt;
    }
    #endregion

    #region order pay status
    public DataTable FindOrderPayStatus()
    {
        return XH.GetForXmlFileStore(ORDER_PAY_STATUS_FILE);
    }
    #endregion

    #region category name
    public DataTable FindPartValidCategoryNameMethods()
    {
        return XH.GetForXmlFileStore(CATEGORY_NAME_FILE);
    }
    #endregion

    #region Part Group
    public DataTable FindPartGroupComment()
    {
        return XH.GetForXmlFileStore(PART_GROUP_COMMENT_FILE);
    }
    #endregion

    #region pre status
    public DataTable FindPreStatus()
    {
        DataTable dt = XH.GetForXmlFileStore(PRE_STATUS_FILE);
        List<int> remoteIndexs = new List<int>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["showit"].ToString() == "0")
                remoteIndexs.Insert(0,i);
        }
        foreach (int i in remoteIndexs)
            dt.Rows.RemoveAt(i);
        return dt;
    }
    #endregion

    #region facture state
    public DataTable FindFactureState()
    {
        DataTable dt = XH.GetForXmlFileStore(FACTURE_STATE_FILE);
        List<int> remoteIndexs = new List<int>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["showit"].ToString() == "0")
                remoteIndexs.Insert(0, i);
        }
        foreach (int i in remoteIndexs)
            dt.Rows.RemoveAt(i);
        return dt;
    }
    #endregion

}
