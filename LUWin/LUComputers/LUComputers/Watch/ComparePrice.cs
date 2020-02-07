using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LUComputers.DBProvider;

namespace LUComputers.Watch
{
    public class ComparePrice
    {
        public ComparePrice() { }



        public string GETHTMLResult(Ltd ltd)
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(ltd);

            string html = "";
            DataTable dt = Config.ExecuteDateTable("select * from tb_other_inc_log where ltd_id='" + ltd_id.ToString() + "' order by id desc limit 0,1");
             if (dt.Rows.Count == 1)
             {
                 DataRow dr = dt.Rows[0];
                 html += string.Format(@"<h3>{0} </h3>", LH.FilterText(ltd.ToString()));
                 html += string.Format(@"<table>");
                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      Product summery:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["totality"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");

                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      New items:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["new_quantity"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");

                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      Deleted items:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["deleted_quantity"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");

                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      Price increased items:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["hike_quantity"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");

                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      Price decreased items:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["depreciate_quantity"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");

                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      Supplier linked items:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["match_totality"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");

                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      New supplier linked items:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["match_new_quantity"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");

                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      Deleted supplier linked items:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["match_delete_quantity"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");

                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      Price increased supplier linked items:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["match_hike_quantity"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");

                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      Price decreased supplier linked items:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["match_depreciate_quantity"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");

                 html += string.Format(@"      <tr>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      Database summery table:");
                 html += string.Format(@"              </td>");
                 html += string.Format(@"              <td>");
                 html += string.Format(@"                      {0}", dr["compare_table_name"].ToString());
                 html += string.Format(@"              </td>");
                 html += string.Format(@"      </tr>");
                 html += string.Format(@"</table>");
                 if (ltd == Ltd.lu)
                     html += GetLUPartDetail(ltd,ltd_id);
                 
                 return html;
             }
             else
             {
                 return "No Match Data.";
             }
        }

        public string GetLUPartDetail(Ltd ltd, int ltd_id)
        {
            string html = "";
            LtdHelper LH = new LtdHelper();

            DataTable tnDT = Config.ExecuteDateTable(string.Format(@"select db_table_name from tb_other_inc_run_date  where other_inc_id='{0}' order by id desc limit 0,2", ltd_id));
            if (tnDT.Rows.Count == 2)
            {
                string table_name1 = tnDT.Rows[0][0].ToString();
                string table_name2 = tnDT.Rows[1][0].ToString();


                string index_fields_name = "";
                string cost_fields_name = "";
                string quantity_fields_name = "";
                CompareDBFields(ltd, ref index_fields_name, ref cost_fields_name, ref quantity_fields_name);

                if (index_fields_name == "" || cost_fields_name == "")
                {
                    throw new Exception("Ltd is not exist.");
                }
                DataTable DDT = Config.ExecuteDateTable(string.Format(@"
select 'New items:', luc_sku, manufacturer_part_number, '' part_name, cost,price from {0} where luc_sku >0 and {4} not in (select {4} from {1})
union all
select 'Deleted items:', luc_sku, manufacturer_part_number, '' part_name, cost,price from {1} where luc_sku >0 and {4} not in (select {4} from {0})
union all
select 'Price increased items:', t1.luc_sku, t1.manufacturer_part_number, '' part_name, t1.cost,t1.price from {1} t1 inner join {0} t2 on t1.{4}=t2.{4} where t2.{5}>t1.{5} and t2.luc_sku >0
union all
select 'Price decreased items:', t1.luc_sku, t1.manufacturer_part_number, '' part_name, t1.cost,t1.price from {1} t1 inner join {0} t2 on t1.{4}=t2.{4} where t2.{5}<t1.{5} and t2.luc_sku >0
", table_name1, table_name2, ltd_id, LH.FilterText(ltd.ToString()), index_fields_name, cost_fields_name));
                html += string.Format(@"<h3>{0} Detail</h3>", LH.FilterText(ltd.ToString()));
                html += string.Format(@"<table>");

                html += string.Format(@"      <tr>");
                html += string.Format(@"              <td>");
                html += string.Format(@"                      {0}", "");
                html += string.Format(@"              </td>");
                html += string.Format(@"              <td>");
                html += string.Format(@"                      <b>{0}</b>", "SKU");
                html += string.Format(@"              </td>");
                html += string.Format(@"              <td>");
                html += string.Format(@"                      <b>{0}</b>", "MFP#");
                html += string.Format(@"              </td>");
                html += string.Format(@"              <td>");
                html += string.Format(@"                      <b>{0}</b>", "Name");
                html += string.Format(@"              </td>");
                html += string.Format(@"              <td >");
                html += string.Format(@"                      <b>{0}</b>", "Cost");
                html += string.Format(@"              </td>");
                html += string.Format(@"              <td >");
                html += string.Format(@"                      <b>{0}</b>", "Price");
                html += string.Format(@"              </td>");
                html += string.Format(@"      </tr>");

                string skus = "0";
                for (int i = 0; i < DDT.Rows.Count; i++)
                {
                    DataRow DDR = DDT.Rows[i];
                    skus += "," + DDR["luc_sku"].ToString();
                }

                DataTable nameDT = new DataTable();
                if (skus.Length > 3)
                {
                    nameDT = Config.RemoteExecuteDateTable("Select product_serial_no,product_name from tb_product where product_serial_no in (" + skus + ")");

                }


                for (int i = 0; i < DDT.Rows.Count; i++)
                {
                    DataRow DDR = DDT.Rows[i];

                    string name = "";

                    for (int j = 0; j < nameDT.Rows.Count; j++)
                    {
                        if (nameDT.Rows[j]["product_serial_no"].ToString() == DDR["luc_sku"].ToString())
                        {
                            name = nameDT.Rows[j]["product_name"].ToString();
                            break;
                        }
                    }

                    html += string.Format(@"      <tr>");
                    html += string.Format(@"              <td>");
                    html += string.Format(@"                      {0}", DDR[0].ToString());
                    html += string.Format(@"              </td>");
                    html += string.Format(@"              <td>");
                    html += string.Format(@"                      {0}", DDR["luc_sku"].ToString());
                    html += string.Format(@"              </td>");
                    html += string.Format(@"              <td>");
                    html += string.Format(@"                      [{0}]", DDR["manufacturer_part_number"].ToString());
                    html += string.Format(@"              </td>");
                    html += string.Format(@"              <td>");
                    html += string.Format(@"                      {0}", name);
                    html += string.Format(@"              </td>");
                    html += string.Format(@"              <td style='text-align:right;'>");
                    html += string.Format(@"                      {0}", DDR["cost"].ToString());
                    html += string.Format(@"              </td>");
                    html += string.Format(@"              <td style='text-align:right;'>");
                    html += string.Format(@"                      {0}", DDR["price"].ToString());
                    html += string.Format(@"              </td>");
                    html += string.Format(@"      </tr>");
                }

                html += string.Format(@"</table><br><hr size=1>");

            } return html;

        }

        public void CompareDBFields(Ltd ltd, ref string index_fields_name, ref string cost_fields_name, ref string quantity_fields_name)
        {
            //
            // 字段名称前面不能有空格。
            //
            switch (ltd)
            {
                case Ltd.Rival_ETC:
                    index_fields_name = "etc_part_model";
                    cost_fields_name = "etc_part_price";
                    quantity_fields_name = "etc_quantity";
                    break;

                case Ltd.wholesaler_supercom:
                    index_fields_name = "part_sku";
                    cost_fields_name = "part_cost";
                    quantity_fields_name = "store_quantity";
                    break;

                case Ltd.lu:
                    index_fields_name = "luc_sku";
                    cost_fields_name = "cost";
                    quantity_fields_name = "ltd_stock";
                    break;
             
                case Ltd.wholesaler_OCZ:
                case Ltd.wholesaler_DAIWA:
                case Ltd.wholesaler_BellMicroproducts:
                case Ltd.wholesaler_ALC:
                case Ltd.wholesaler_Smartvision_Direct:
              
                    index_fields_name = "part_sku";
                    cost_fields_name = "part_cost";
                    quantity_fields_name = "store_quantity ";
                    break;
                case Ltd.wholesaler_CanadaComputers:
                case Ltd.Rival_DirectDial:
                case Ltd.Rival_Ncix:
                case Ltd.Rival_TigerDirect:
                case Ltd.Rival_NewEgg:
                case Ltd.wholesaler_dandh:
                case Ltd.wholesaler_asi:
                case Ltd.wholesaler_Synnex:
                    index_fields_name = "sku";
                    cost_fields_name = "price";
                    quantity_fields_name = "quantity ";
                    break;
                case Ltd.wholesaler_d2a:
                    index_fields_name = "part_sku";
                    cost_fields_name = "part_cost";
                    quantity_fields_name = "store_quantity ";
                    break;
                case Ltd.wholesaler_MMAX:
                    index_fields_name = "part_sku";
                    cost_fields_name = "part_cost";
                    quantity_fields_name = "store_quantity ";
                    break;
                case Ltd.wholesaler_COMTRONIX:
                    index_fields_name = "part_sku";
                    cost_fields_name = "part_cost";
                    quantity_fields_name = "store_quantity ";
                    break;
                case Ltd.wholesaler_MINIMICRO:
                    index_fields_name = "part_sku";
                    cost_fields_name = "part_cost";
                    quantity_fields_name = "store_quantity ";
                    break;

            }
        }

        public bool Execute(Ltd ltd)
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(ltd);

            string index_fields_name = "";
            string cost_fields_name = "";
            string quantity_fields_name = "";
            CompareDBFields(ltd, ref index_fields_name, ref cost_fields_name, ref quantity_fields_name);

            if (index_fields_name == "" || cost_fields_name == "")
            {
                throw new Exception("Ltd is not exist.");
            }

            string tableNameNoDate = DBProvider.TableName.GetPriceTableNamePart(new LtdHelper().FilterText(ltd.ToString()));

            DataTable tnDT = Config.ExecuteDateTable(string.Format(@"
select table_name from information_schema.tables where table_schema='ltd_info' and table_name like '{0}%' order by table_name desc limit 0,2", tableNameNoDate));
            if (tnDT.Rows.Count == 2)
            {
                string table_name1 = tnDT.Rows[0][0].ToString();
                string table_name2 = tnDT.Rows[1][0].ToString();

                if (ltd == Ltd.wholesaler_Synnex
                    || ltd == Ltd.wholesaler_CanadaComputers)
                {
                    Config.ExecuteNonQuery("delete from " + table_name1 + " where luc_sku=0");
                }
                Config.ExecuteNonQuery(string.Format(@"
delete from tb_other_inc_log where compare_table_name='{1}|{0}';

insert into tb_other_inc_log 
	( ltd_id, ltd_name, regdate, totality, new_quantity, deleted_quantity, 
	hike_quantity, 
	depreciate_quantity, 
	match_totality, 
	match_new_quantity, 
	match_delete_quantity, 
	match_hike_quantity, 
	match_depreciate_quantity, 
	compare_table_name
	)

select '{2}', '{3}', now()
, (select count(*) from {0}) today
, (select count(*) from {0} where {4} not in (select {4} from {1})) new_quantity
, (select count(*) from {1} where {4} not in (select {4} from {0})) deleted_quantity
, (select count(*) from {1} t1 inner join {0} t2 on t1.{4}=t2.{4} where t2.{5}>t1.{5}) hike_quantity
, (select count(*) from {1} t1 inner join {0} t2 on t1.{4}=t2.{4} where t2.{5}<t1.{5}) depreciate_quantity
 
, (select count(*) from {0} where luc_sku >0 ) match_totality
, (select count(*) from {0} where luc_sku >0 and {4} not in (select {4} from {1})) match_new_quantity
, (select count(*) from {1} where luc_sku >0 and {4} not in (select {4} from {0})) match_delete_quantity
, (select count(*) from {1} t1 inner join {0} t2 on t1.{4}=t2.{4} where t2.{5}>t1.{5} and t2.luc_sku >0) match_hike_quantity
, (select count(*) from {1} t1 inner join {0} t2 on t1.{4}=t2.{4} where t2.{5}<t1.{5} and t2.luc_sku >0) match_depreciate_quantity
, '{1}|{0}' compare_table_name", table_name1, table_name2, ltd_id, LH.FilterText(ltd.ToString()), index_fields_name, cost_fields_name));
                return true;
            }
            return false;
        }

    }
}
