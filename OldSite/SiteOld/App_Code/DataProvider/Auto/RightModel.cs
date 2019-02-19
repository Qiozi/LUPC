// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-10 19:50:14
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[Serializable, ActiveRecord("tb_right")]
public class RightModel : ActiveRecordBase<RightModel>
{
    // Fields
    private DateTime _create_datetime;
    private string _left_content;
    private string _main_content;
    private string _part_product_category;
    private string _right_content;
    private int _right_id;
    private string _right_page;
    bool _exist_top;

    // Methods
    public static void DeleteNull(string page_name, string part_product_category)
    {
        try
        {
            int part_id = int.Parse(part_product_category);
            //if (part_id != -1)
            //{
            Config.ExecuteNonQuery("delete from tb_right where  part_product_category='" + part_product_category + "' and right_page='" + page_name + "' ");
           // }
        }
        catch {
            TrackModel.InsertInfo("delete from tb_right where  part_product_category='" + part_product_category + "' and right_page='" + page_name + "' ", 1000);
        }
    }

    public static RightModel FindModelByRightPage(string page_name, string part_product_category)
    {
        RightModel[] rm = ActiveRecordBase<RightModel>.FindAllByProperty("right_page", page_name);
        for (int i = 0; i < rm.Length; i++)
        {
            if (rm[i].part_product_category == part_product_category)
            {
                return rm[i];
            }
        }
        return null;
    }

    public static RightModel GetAccountModel(int _right_id)
    {
        RightModel[] models = ActiveRecordBase<RightModel>.FindAllByProperty("right_id", _right_id);
        if (models.Length == 1)
        {
            return models[0];
        }
        return new RightModel();
    }

    // Properties
    [Property]
    public DateTime create_datetime
    {
        get
        {
            return this._create_datetime;
        }
        set
        {
            this._create_datetime = value;
        }
    }

    [Property]
    public string left_content
    {
        get
        {
            return this._left_content;
        }
        set
        {
            this._left_content = value;
        }
    }

    [Property]
    public string main_content
    {
        get
        {
            return this._main_content;
        }
        set
        {
            this._main_content = value;
        }
    }

    [Property]
    public string part_product_category
    {
        get
        {
            return this._part_product_category;
        }
        set
        {
            this._part_product_category = value;
        }
    }

    [Property]
    public string right_content
    {
        get
        {
            return this._right_content;
        }
        set
        {
            this._right_content = value;
        }
    }

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int right_id
    {
        get
        {
            return this._right_id;
        }
        set
        {
            this._right_id = value;
        }
    }

    [Property]
    public string right_page
    {
        get
        {
            return this._right_page;
        }
        set
        {
            this._right_page = value;
        }
    }
    [Property]
    public bool exist_top
    {
        get
        {
            return this._exist_top;
        }
        set
        {
            this._exist_top = value;
        }
    }
}
