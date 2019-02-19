// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-30 23:49:38
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;

[Serializable]
public class CustomerModel 
{

    public CustomerModel()
    {

    }

    public static tb_customer GetCustomerModel(nicklu2Entities context, int _customer_serial_no)
    {
        //var 
        //CustomerModel[] models = CustomerModel.FindAllByProperty("customer_serial_no", _customer_serial_no);
        ////throw new Exception(models.Length.ToString());

        //if (models.Length > 0)
        //    return models[0];

        var query = context.tb_customer.FirstOrDefault(me => me.customer_serial_no.Value.Equals(_customer_serial_no));
        if (query!=null)
        {
            return query;
        }
        else
        {
            if(Config.ExecuteScalarInt32(string.Format(@"Select count(serial_no) c from tb_customer_store where customer_serial_no = '{0}'", _customer_serial_no)) >0
                && Config.ExecuteScalarInt32(string.Format(@"Select count(id) c from tb_store_customer_code where customerCode = '{0}'", _customer_serial_no)) ==0)
            {
                var c = new tb_customer();
                c.customer_serial_no = _customer_serial_no;
                c.create_datetime = DateTime.Now;
                context.tb_customer.Add(c);
                context.SaveChanges();
                return c;
            }
            return null;
        }
    }

    /// <summary>
    /// 按customer number,email,phone, zip_code,etc... 查询
    /// 
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public static DataTable GetModelsBySearch(string keyword)
    {
        string sql = "Select customer_first_name, customer_last_name, phone_d,phone_n, customer_serial_no,customer_login_name,customer_card_state,customer_password,customer_card_zip_code, customer_card_city, state_code from tb_customer where 1=1 ";
        if (keyword != "")
            sql += " and ( customer_first_name like '%" + keyword + "%' or customer_login_name='" + keyword + "' or customer_last_name like '%" + keyword + "%'  or phone_d like '%" + keyword + "%' or zip_code like '%" + keyword + "%' or customer_serial_no='" + keyword + "' ) ";
        sql += " order by customer_serial_no desc";
        return Config.ExecuteDataTable(sql);
    }

    /// <summary>
    /// 按customer number,login username 查询
    /// 只返回一条纪录
    /// 
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public static DataTable GetModelsByKeyword(string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
            return null;
        string sql = "Select * from tb_customer where 1=1 ";
        sql += " and customer_serial_no='" + keyword + "' or customer_login_name='" + keyword + "' ";
        return Config.ExecuteDataTable(sql);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="first_name"></param>
    /// <param name="last_name"></param>
    /// <returns></returns>
    public static DataTable GetModelsByFirstLastname(string first_name, string last_name)
    {
        string sql = "Select customer_first_name, customer_login_name,customer_last_name,customer_card_state, phone_d, phone_n,customer_serial_no, customer_password,customer_card_zip_code,customer_card_city from tb_customer where 1=1 ";
        
        if (first_name != "" && last_name == "")
        {
            sql += " and  customer_first_name='" + first_name + "'";

        }
        if (last_name != "" && first_name == "")
        {
            sql += " and customer_last_name='" + last_name + "'";
        }
        if (last_name != "" && first_name != "")
        {
            sql += " and customer_last_name='" + last_name + "' and customer_first_name='" + first_name + "'";
        }
        sql += " order by customer_serial_no desc";

        return Config.ExecuteDataTable(sql);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="full_login"></param>
    /// <returns></returns>
    public static DataTable GetModelsByLoginName(string full_login)
    {
        string sql = "Select customer_first_name,customer_login_name,customer_password, customer_last_name,customer_card_state,customer_card_zip_code, phone_d,phone_n, customer_serial_no, customer_card_city from tb_customer where 1=1 ";
        sql += " and  customer_login_name='" + full_login + "'";
        sql += " order by customer_serial_no desc";

        return Config.ExecuteDataTable(sql);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="customer_id"></param>
    /// <returns></returns>
    public static DataTable GetModelsByCustomerID(int customer_id)
    {
        return Config.ExecuteDataTable("select * from tb_customer where customer_serial_no=" + customer_id);
    }

   
}
