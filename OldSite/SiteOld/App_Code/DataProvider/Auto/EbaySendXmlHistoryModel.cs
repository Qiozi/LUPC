
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	3/27/2010 1:35:52 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_send_xml_history")]
[Serializable]
public class EbaySendXmlHistoryModel : ActiveRecordBase<EbaySendXmlHistoryModel>
{

    public EbaySendXmlHistoryModel()
    {

    }

    public static EbaySendXmlHistoryModel GetEbaySendXmlHistoryModel(int _id)
    {
        EbaySendXmlHistoryModel[] models = EbaySendXmlHistoryModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbaySendXmlHistoryModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _SKU;

    [Property]
    public int SKU
    {
        get { return _SKU; }
        set { _SKU = value; }
    }

    bool _is_sys;

    [Property]
    public bool is_sys
    {
        get { return _is_sys; }
        set { _is_sys = value; }
    }

    string _Content;

    [Property]
    public string Content
    {
        get { return _Content; }
        set { _Content = value; }
    }

    bool _is_modify;
    [Property]
    public bool is_modify
    {
        get { return _is_modify; }
        set { _is_modify = value; }
    }

    string _comm;
    [Property]
    public string comm
    {
        get { return _comm; }
        set { _comm = value; }
    }

}

