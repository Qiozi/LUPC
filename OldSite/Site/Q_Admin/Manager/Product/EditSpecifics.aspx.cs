using LU.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Q_Admin_Manager_Product_EditSpecifics : PageBase
{
    string _path = "/soft_img/eBayXml/ItemSpecifics/";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var filename = Server.MapPath(string.Concat(_path, ReqeBayCateId, ".xml"));
            if (File.Exists(filename))
            {

                var xmlStr = File.ReadAllText(filename);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlStr);
                foreach (XmlNode xml in doc.ChildNodes[1].ChildNodes)
                {
                    if ("ack" == xml.Name.ToLower() && xml.InnerText.ToLower() != "success")
                    {
                        break;
                    }

                    if (xml.Name == "Recommendations")
                    {
                        ReadXml(xml);
                    }
                }
            }
            else
            {
                Response.Write("Specifices file is not exist.");
            }
        }
    }

    void ReadXml(XmlNode recommentNode)
    {
        var list = new List<ProductSpecifices>();


        foreach (XmlNode node in recommentNode.ChildNodes)
        {
            if (node.Name == "NameRecommendation")
            {
                var valueRecommend = new List<string>();

                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "ValueRecommendation")
                    {
                        foreach (XmlNode subnode in childnode.ChildNodes)
                        {
                           if (!string.IsNullOrEmpty(subnode.InnerText))
                            {
                                valueRecommend.Add(
                                     subnode.InnerText
                                );
                                //Response.Write(subnode.InnerText + "<br>");
                            }
                        }
                    }
                    else if (childnode.Name == "ValidationRules")
                    {
                        // TODO
                    }
                }

                list.Add(new ProductSpecifices
                {
                    Name = node.FirstChild.InnerText,
                    ValueRecommendation = valueRecommend,
                    Text = string.Empty
                });
            }
        }
        list.Add(new ProductSpecifices
        {
            Name = "UPC",
            Text = "Does Not Apply",
            ValueRecommendation = new List<string>()

        });
        SetOldValue(list);

        this.rptList.DataSource = list;
        this.rptList.DataBind();
    }

    void SetOldValue(List<ProductSpecifices> list)
    {

        var query = DBContext.tb_ebay_system_item_specifics.Where(p => p.system_sku.Equals(ReqSku)).ToList();

        if (query.Count(p => !string.IsNullOrEmpty(p.ItemSpecificsValue)) == 0)
        {
            var prod = DBContext.tb_product.FirstOrDefault(p => p.product_serial_no == ReqSku);
            if (prod != null)
            {
                query.Add(new tb_ebay_system_item_specifics
                {
                    ItemSpecificsName = "Brand",
                    ItemSpecificsValue = prod.producter_serial_no
                });
                query.Add(new tb_ebay_system_item_specifics
                {
                    ItemSpecificsName = "MPN",
                    ItemSpecificsValue = prod.manufacturer_part_number
                });
                query.Add(new tb_ebay_system_item_specifics
                {
                    ItemSpecificsValue = prod.UPC,
                    ItemSpecificsName = "UPC"
                });

                if (prod.menu_child_serial_no.HasValue && prod.menu_child_serial_no.Value.Equals(350))
                {
                    query.Add(new tb_ebay_system_item_specifics
                    {
                        ItemSpecificsValue = prod.screen_size.HasValue ? string.Concat(prod.screen_size.Value.ToString("0.0"), "in.") : "",
                        ItemSpecificsName = "Screen Size"
                    });

                }
            }


            var ebaySys = DBContext.tb_ebay_system.FirstOrDefault(p => p.id.Equals(ReqSku));
            var title = ReqSku.ToString().Length == 5 ? prod.product_ebay_name : (ebaySys != null ? ebaySys.system_title1 : "");

            if (!string.IsNullOrEmpty(title))
            {
                var keywords = DBContext.tb_ebay_system_item_specifics_keyword.ToList();
                foreach (var key in keywords)
                {

                    if (title.ToLower().IndexOf(key.Keyword.ToLower()) > -1)
                    {
                        query.Add(new tb_ebay_system_item_specifics
                        {
                            ItemSpecificsValue = key.SpecificsValue,
                            ItemSpecificsName = key.SpecificsName
                        });
                    }
                }
            }

        }
        foreach (var item in query)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (item.ItemSpecificsName.Equals(list[i].Name))
                {
                    list[i].Text = item.ItemSpecificsValue;
                }
            }
        }
    }

    public string ReqeBayCateId
    {
        get
        {
            return Util.GetStringSafeFromQueryString(Page, "cid");
        }
    }

    public int ReqSku
    {
        get
        {
            return Util.GetInt32SafeFromQueryString(Page, "sku", 0);
        }
    }

    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            var data = e.Item.DataItem as ProductSpecifices;
            var ddl = e.Item.FindControl("_ddlRemm") as DropDownList;

            ddl.Items.Clear();
            foreach (var obj in data.ValueRecommendation)
            {
                ddl.Items.Add(obj.ToString());
            }
        }
    }

    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Set":
                var ddl = e.Item.FindControl("_ddlRemm") as DropDownList;
                var txt = e.Item.FindControl("_txtValue") as TextBox;
                txt.Text = ddl.SelectedItem.Text;
                break;
        }
    }

    protected void submitTo_Click(object sender, EventArgs e)
    {
        if (ReqSku > 0)
        {
            if (rptList.Items.Count > 0)
            {
                var query = (from c in DBContext.tb_ebay_system_item_specifics
                             where c.system_sku == ReqSku
                             select c).ToList();

                foreach (var item in query)
                {
                    DBContext.tb_ebay_system_item_specifics.Remove(item);//.DeleteObject(item);
                }
                DBContext.SaveChanges();
            }

            foreach (RepeaterItem item in rptList.Items)
            {
                if (item.ItemType != ListItemType.Header && item.ItemType != ListItemType.Footer)
                {
                    var name = item.FindControl("_txtName") as TextBox;
                    var value = item.FindControl("_txtValue") as TextBox;
                    var specificesModel = new tb_ebay_system_item_specifics
                    {
                        ItemSpecificsName = name.Text,
                        ItemSpecificsValue = value.Text,
                        system_sku = ReqSku
                    };
                    DBContext.tb_ebay_system_item_specifics.Add(specificesModel);
                }
            }
            DBContext.SaveChanges();

            var prod = DBContext.tb_product.First(p => p.product_serial_no.Equals(ReqSku));
            prod.SpecificsCount = rptList.Items.Count;
            DBContext.SaveChanges();
            Response.Write("OK");
        }
        else
        {
            Response.Write("SKU is null.");
        }
    }
}

public class ProductSpecifices
{

    public int Id { get; set; }

    public string Name { get; set; }

    public string Text { get; set; }

    public List<string> ValueRecommendation { get; set; }
}