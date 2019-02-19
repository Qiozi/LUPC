// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-12-12 1:46:05
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;


[ActiveRecord("tb_update_product_store")]
public class UpdateProductStoreModel : ActiveRecordBase<UpdateProductStoreModel>
{
    int _update_store_id;
    string _update_store_regdate;
    int _update_store_staff;
    string _a;
    string _b;
    string _c;
    string _d;
    string _e;
    string _f;
    string _g;
    string _h;
    string _i;
    string _j;
    string _k;
    string _l;
    string _m;
    string _n;
    string _o;
    string _p;
    string _q;

    public UpdateProductStoreModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int update_store_id
    {
        get { return _update_store_id; }
        set { _update_store_id = value; }
    }
    public static UpdateProductStoreModel GetUpdateProductStoreModel(int _update_store_id)
    {
        UpdateProductStoreModel[] models = UpdateProductStoreModel.FindAllByProperty("update_store_id", _update_store_id);
        if (models.Length == 1)
            return models[0];
        else
            return new UpdateProductStoreModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string update_store_regdate
    {
        get { return _update_store_regdate; }
        set { _update_store_regdate = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int update_store_staff
    {
        get { return _update_store_staff; }
        set { _update_store_staff = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string a
    {
        get { return _a; }
        set { _a = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string b
    {
        get { return _b; }
        set { _b = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string c
    {
        get { return _c; }
        set { _c = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string d
    {
        get { return _d; }
        set { _d = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string e
    {
        get { return _e; }
        set { _e = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string f
    {
        get { return _f; }
        set { _f = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string g
    {
        get { return _g; }
        set { _g = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string h
    {
        get { return _h; }
        set { _h = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string i
    {
        get { return _i; }
        set { _i = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string j
    {
        get { return _j; }
        set { _j = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string k
    {
        get { return _k; }
        set { _k = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string l
    {
        get { return _l; }
        set { _l = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string m
    {
        get { return _m; }
        set { _m = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string n
    {
        get { return _n; }
        set { _n = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string o
    {
        get { return _o; }
        set { _o = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string p
    {
        get { return _p; }
        set { _p = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string q
    {
        get { return _q; }
        set { _q = value; }
    }


}
