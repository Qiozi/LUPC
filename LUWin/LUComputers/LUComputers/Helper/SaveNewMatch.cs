using LUComputers.DBProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LUComputers.Helper
{
    public class SaveNewMatch
    {

        public event Events.UrlEventHandler WatchE;

        protected void InvokeWatchE(Events.UrlEventArgs e)
        {
            Events.UrlEventHandler watch = WatchE;
            if (watch != null) watch(this, e);
        }

        public void SetStatus(string url, string comment)
        {
            LUComputers.Events.UrlEventArgs ua = new LUComputers.Events.UrlEventArgs(new Events.UrlEventModel()
            {
                comment = comment,
                url = url,
                ltd = Ltd.ALLPublic
            });
            InvokeWatchE(ua);
        }

        public SaveNewMatch() { }

        public void FilterSave(Ltd ltd)
        {
        }

        public bool UpdatePartInfo(Ltd ltd, string late_table_name_group, int page_size)
        {

            Config.RemoteExecuteNonQuery("Delete from tb_other_inc_part_info where other_inc_id='' or other_inc_sku=''");
            //
            // split table name.
            string last_table = "";
            string old_table = "";

            DBProvider.Find.LastTwoTableName(new LtdHelper().FilterText(ltd.ToString()), ref old_table, ref last_table);

            if (last_table == "" || old_table == "")
            {
                throw new Exception("Ltd table is less.");
            }

            var LH = new LtdHelper();
            var ltd_id = LH.LtdHelperValue(ltd);
            string part_sql = "";
            switch (ltd)
            {
                case Ltd.wholesaler_d2a:
                    part_sql = "part_sku other_inc_sku, part_cost other_inc_price, store_quantity other_inc_store_sum, '1' tag, regdate, regdate last_regdate, luc_sku, mfp manufacture_part_number";
                    break;
                case Ltd.wholesaler_asi:
                case Ltd.wholesaler_dandh:
                case Ltd.wholesaler_Synnex:
                    part_sql = "sku other_inc_sku, price other_inc_price, quantity other_inc_store_sum, '1' tag, regdate, regdate last_regdate, luc_sku, mfp manufacture_part_number";

                    break;
            }

            string cost_field_name = "";
            string stock_field_name = "";
            string index_field_name = "";

            var CP = new LUComputers.Watch.ComparePrice();
            CP.CompareDBFields(ltd, ref index_field_name, ref cost_field_name, ref stock_field_name);

            var sb_sku = new System.Text.StringBuilder();

            var sql = string.Format(@"
 delete from nicklu2.tb_other_inc_part_info where other_inc_id='{0}';
 insert into nicklu2.tb_other_inc_part_info(other_inc_id, other_inc_sku, other_inc_price, other_inc_store_sum, tag, regdate,last_regdate, luc_sku, manufacture_part_number) 
 select {0},{2} from ltd_info.{1}  
", ltd_id.ToString(), last_table, part_sql);

            Logs.WriteLog(sql);
            // delete all remote
            int delCount = Config.RemoteExecuteNonQuery(sql);

            SetStatus(null, ltd.ToString() + " Update page: end. "+ Config.RemoteExecuteDateTable("select count(*) from ltd_info." + last_table).Rows.Count.ToString());

            return true;
        }

        public bool UpdateMatchLUSKu(Ltd ltd, int page_size)
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(ltd);

            DataTable oldNameDT = Config.ExecuteDateTable("show tables where tables_in_ltd_info like '%match_lu_sku_%' ");
            string table_name = "";
            if (oldNameDT.Rows.Count > 0)
            {
                table_name = oldNameDT.Rows[oldNameDT.Rows.Count - 1][0].ToString();
            }
            //MessageBox.Show(table_name);
            if (table_name == "")
                throw new Exception("Old Match Table isn't exist");

            DataTable upDt = Config.ExecuteDateTable(string.Format(@"

select 
t1.other_inc_sku,
t1.lu_sku , 
t1.other_inc_type 

from (select distinct other_inc_sku, lu_sku , other_inc_type from tb_other_inc_match_lu_sku where other_inc_type={1} ) t1 left join 
(select distinct other_inc_sku, lu_sku , other_inc_type from {0} where other_inc_type={1} ) t2 on t1.lu_sku=t2.lu_sku and t1.other_inc_type=t2.other_inc_type
where t2.lu_sku is null or t1.other_inc_sku <> t2.other_inc_sku", table_name, ltd_id));


            var sql = string.Format(@"
delete from nicklu2.tb_other_inc_match_lu_sku where other_inc_type='{0}' and lu_sku in ({1});

insert into nicklu2.tb_other_inc_match_lu_sku(other_inc_type, other_inc_sku, lu_sku)
{2}


",
                            ltd_id,
                            string.Format(@"
select 
t1.lu_sku 
from (select distinct other_inc_sku, lu_sku , other_inc_type from ltd_info.tb_other_inc_match_lu_sku where other_inc_type={1} ) t1 left join 
(select distinct other_inc_sku, lu_sku , other_inc_type from ltd_info.{0} where other_inc_type={1} ) t2 on t1.lu_sku=t2.lu_sku and t1.other_inc_type=t2.other_inc_type
where t2.lu_sku is null or t1.other_inc_sku <> t2.other_inc_sku", table_name, ltd_id),
                            string.Format(@"

select 
t1.other_inc_type, 
t1.other_inc_sku,
t1.lu_sku

from (select distinct other_inc_sku, lu_sku , other_inc_type from ltd_info.tb_other_inc_match_lu_sku where other_inc_type={1}) t1 left join 
(select distinct other_inc_sku, lu_sku , other_inc_type from ltd_info.{0} where other_inc_type={1}) t2 on t1.lu_sku=t2.lu_sku and t1.other_inc_type=t2.other_inc_type
where t2.lu_sku is null or t1.other_inc_sku <> t2.other_inc_sku", table_name, ltd_id));

            SetStatus(null, ltd.ToString() + " Match SKU Update End:(" + upDt.Rows.Count.ToString() + ")");
            
            return true;
        }

        public void UpdateToRemote(Ltd ltd, string table_name)
        {
            if (table_name.Length < 2)
                throw new Exception("The TableName is error.");

            var ph = new ProcessHelper();
            LtdHelper LH = new LtdHelper();
            // string filename = "";
            int ltd_id = LH.LtdHelperValue(ltd);
            int page_size = 100;
            switch (ltd)
            {
                case Ltd.wholesaler_asi:
                case Ltd.wholesaler_dandh:
                    //
                    // update part info 
                    //
                    if (UpdatePartInfo(ltd, table_name, page_size))
                    {
                        //_form1.SetListBox(_listBox, ltd.ToString() + " part update end.");
                        SetStatus(null, ltd.ToString() + " part update end.");
                    }
                    //
                    // update match luc_sku
                    //
                    if (UpdateMatchLUSKu(ltd, page_size))
                    {
                        //_form1.SetListBox(_listBox, ltd.ToString() + " part Match SKU update end.");
                        SetStatus(null, ltd.ToString() + " part Match SKU update end.");
                    }

                    break;
     
                case Ltd.wholesaler_d2a:
                    //
                    // update part info 
                    //
                    if (UpdatePartInfo(ltd, table_name, page_size))
                    {
                        //_form1.SetListBox(_listBox, ltd.ToString() + " part update end.");
                        SetStatus(null, ltd.ToString() + " part update end.");
                    }
                    //
                    // update match luc_sku
                    //
                    if (UpdateMatchLUSKu(ltd, page_size))
                    {
                        //_form1.SetListBox(_listBox, ltd.ToString() + " part Match SKU update end.");
                        SetStatus(null, ltd.ToString() + " part Match SKU update end.");
                    }
                    break;
                case Ltd.wholesaler_Synnex:
                    //
                    // update part info 
                    //
                    if (UpdatePartInfo(ltd, table_name, page_size))
                    {
                        SetStatus(null, ltd.ToString() + " part update end.");
                    }
                    //
                    // update match luc_sku
                    //
                    if (UpdateMatchLUSKu(ltd, page_size))
                    {
                        SetStatus(null, ltd.ToString() + " part Match SKU update end.");
                    }
                    break;
            }
        }

    }
}
