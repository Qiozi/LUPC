using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Xml;
using System.Data;

public partial class Q_Admin_ebayMaster_WM : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
    }

    [WebMethod(true)]
    public static object GetShippingServiceDetail()
    {
        try
        {
            XmlDocument doc = new XmlDocument();

            XmlNode xml = new EbayGetXmlHelper().GetShippingServiceXML();

            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            //throw new Exception(xml.ChildNodes[0].InnerText);
            for (int i = 0; i < xml.ChildNodes.Count; i++)
            {
                if (xml.ChildNodes[i].Name != "ShippingServiceDetails") continue;

                KeyValuePair<string, string> m = new KeyValuePair<string, string>(xml.ChildNodes[i]["ShippingService"].InnerText, xml.ChildNodes[i]["Description"].InnerText);
               
                list.Add(m);
            }
            // throw new Exception(xml.ChildNodes.Count.ToString());
            //englishschoolModel.englishschoolEntities db = new englishschoolModel.englishschoolEntities();
            //englishschoolModel.tb_student stu = Repository.GetStudent(id, db);
            //if (stu != null)
            //{
            //    List<englishschoolModel.tb_student> list = new List<englishschoolModel.tb_student>();
            //    list.Add(stu);
            //    return new { Result = "OK", Records = list };
            //}

            //else
           
                return new { Result = "OK", Records=list };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(true)]
    public static object GetShippingServiceValue(string categoryid)
    {
        try
        {

            EbayShippingSettingsModel[] list = EbayShippingSettingsModel.FindAllByProperty("CategoryID", categoryid.Trim());
          
            return new { Result = "OK", Records = list };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(true)]
    public static object NewShipping(string cid)
    {
        try
        {

            Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_shipping_settings(shippingFee, shippingCompany, IsFree, CategoryID, ShortCategoryName, regdate)
                                                    values ('{0}', '{1}','{2}','{3}','{4}',now())"
                , "0"
                , "CA_Pickup"
                , 1
                , cid + "-" + (1+Config.ExecuteScalarInt32("select count(*) c from (select distinct CategoryID from tb_ebay_shipping_settings where categoryId like '" + cid.Trim() + "-%' group by CategoryID) t")).ToString()
                , ""));

            return new { Result = "OK"};
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(true)]
    public static object SavePartShipping(string shipCate, string sku)
    {
        try
        {
            if (shipCate.Trim().Length < 11
                && sku.Trim().Length < 10)
            {
                Config.ExecuteNonQuery("delete from tb_part_and_shipping where sku='" + sku + "'");
                Config.ExecuteNonQuery("insert into tb_part_and_shipping(SKU, ShippingCategoryId) values ('" + sku + "', '" + shipCate + "')");
            }
            return new { Result = "OK" };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
}