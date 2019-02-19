using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

/// <summary>
/// Summary description for GetEbayCategoryIDs
/// </summary>
public class GetEbayCategoryIDs
{
	public GetEbayCategoryIDs()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// 从ebay.ca上加载数据
    /// </summary>
    void LoadStoreCategory()
    {
        EbayGetXmlHelper eh = new EbayGetXmlHelper();
        eh.GetStoreRequest();
    }
    /// <summary>
    /// 取得类型列表
    /// </summary>
    /// <param name="FullFilename"></param>
    /// <returns></returns>
    public List<eBayCategory> GetStoreCategory(string FullFilename)
    {
        List<eBayCategory> myHashtable = new List<eBayCategory>();

        string filename = FullFilename;// Server.MapPath("~/soft_img/eBayXml/GetStoreResponse.xml");
        if (!File.Exists(filename))
        {
            LoadStoreCategory();
        }
        FileInfo fi = new FileInfo(filename);
        if (fi.LastWriteTime.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
        {
            LoadStoreCategory();
        }
        XmlDocument doc = new XmlDocument();
        doc.Load(filename);
        XmlNodeList list = doc["GetStoreResponse"]["Store"]["CustomCategories"].ChildNodes;
        for (int i = 0; i < list.Count; i++)
        {
            string name_1 = "";
            if (list[i]["ChildCategory"] != null)
            {
                name_1 = list[i]["Name"].InnerText;

                for (int j = 0; j < list[i].ChildNodes.Count; j++)
                {
                    XmlNode childXN = list[i].ChildNodes[j];
                    //Response.Write(childXN.Name);
                    if (childXN.Name == "ChildCategory")
                    {
                        if (childXN["ChildCategory"] != null)
                        {
                            string name_2 = name_1 + ":::" + childXN["Name"].InnerText;

                            for (int x = 0; x < childXN.ChildNodes.Count; x++)
                            {
                                XmlNode subXN = childXN.ChildNodes[x];

                                if (subXN.Name == "ChildCategory")
                                {
                                    myHashtable.Add(new eBayCategory()
                                    {
                                        Id = subXN["CategoryID"].InnerText,
                                        Name = name_2 + ":::" + subXN["Name"].InnerText
                                    });
                                }
                            }
                        }
                        else
                        {
                            myHashtable.Add(new eBayCategory()
                            {
                                Id = childXN["CategoryID"].InnerText,
                                Name = name_1 + ":::" + childXN["Name"].InnerText
                            });
                        }
                    }
                }
            }
            else
            {
                myHashtable.Add(new eBayCategory()
                {
                    Id = list[i]["CategoryID"].InnerText,
                    Name = list[i]["Name"].InnerText
                });
            }
        }

        return myHashtable;
    }
}

[Serializable]
public class eBayCategory
{
    public string Id { get; set; }
    public string Name { get; set; }

    public eBayCategory() { }
}