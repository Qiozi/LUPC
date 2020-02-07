using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LUComputers.DBProvider;

namespace LUComputers.Watch
{
    public class UpdateStock
    {
        public event Events.UrlEventHandler WatchE;

        protected void InvokeWatchE(Events.UrlEventArgs e)
        {
            Events.UrlEventHandler watch = WatchE;
            if (watch != null) watch(this, e);
        }

        public void SetStatus(string url, string comment, string result)
        {
            LUComputers.Events.UrlEventArgs ua = new LUComputers.Events.UrlEventArgs(new Events.UrlEventModel()
            {
                comment = comment,
                url = url,
                ltd = Ltd.lu,
                result = result
            });
            InvokeWatchE(ua);
        }

        public void SetStatus(string url, string comment)
        {
            SetStatus(url, comment, null);
        }

        public void SetStatus(string result)
        {
            SetStatus(null, null, result);
        }



        public UpdateStock() { }

        /// <summary>
        /// 统计每个零件的库存情况。
        /// </summary>
        public void StatStock()
        {
            LtdHelper LH = new LtdHelper();

            string[] ltds = LH.GetLtdStoreDBTableNames(new Ltd[]{Ltd.wholesaler_asi                
                , Ltd.wholesaler_dandh
                , Ltd.wholesaler_Synnex
                //, Ltd.wholesaler_Smartvision_Direct
                , Ltd.wholesaler_d2a
                //,Ltd.wholesaler_MMAX
            });

            Watch.LU lu = new LUComputers.Watch.LU();

            DataTable dt = Config.ExecuteDateTable("Select distinct lu_sku from tb_other_inc_valid_lu_sku where menu_child_serial_no<>358");
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                int luc_sku;
                int.TryParse(dt.Rows[i][0].ToString(), out luc_sku);
                SetStatus(null, string.Format("exec {0} ({1} of {2})", luc_sku, i, count));
                lu.ExecStock(ltds, luc_sku);
                SetStatus(null, string.Format("exec {0} end.", luc_sku));
            }
        }
        /// <summary>
        /// 把stock 上传到服务器
        /// </summary>
        public void UpdateRemote()
        {
            DataTable dt = Config.ExecuteDateTable("select db_table_name from tb_other_inc_run_date where other_inc_id=1 order by id desc limit 0,1 ");
            if (dt.Rows.Count == 1)
            {
                string tableName = dt.Rows[0][0].ToString();

                dt = Config.ExecuteDateTable(@"
select t1.lu_sku,t1.ltd_stock from tb_other_inc_valid_lu_sku t1 inner join " + tableName + @" t2 on t1.lu_sku=t2.luc_sku
where t1.menu_child_serial_no<>358");

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(string.Format(@"Delete from tb_product_temp_stock;
insert into  tb_product_temp_stock(luc_sku, ltd_quantity) values "));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    if (i == dt.Rows.Count - 1)
                    {
                        sb.Append(string.Format(@" ('{0}', '{1}');"
                                                        , dr["lu_sku"].ToString()
                                                        , dr["ltd_stock"].ToString()));
                    }
                    else
                    {
                        sb.Append(string.Format(@" ('{0}', '{1}'),"
                                                        , dr["lu_sku"].ToString()
                                                        , dr["ltd_stock"].ToString()));
                    }
                }

                if (dt.Rows.Count > 0)
                    Config.RemoteExecuteNonQuery(sb.ToString());
                else
                    SetStatus(null,  "Warn........................");
                SetStatus(null, " Stock SKU Update End:(" + dt.Rows.Count.ToString() + ")");

                SetStatus(null, " Stock remote change is begin");
                Config.RemoteExecuteNonQuery(@"delete from tb_other_inc_part_info where other_inc_id ='' or other_inc_sku =''; 
update tb_product set ltd_stock = 0;
update tb_product p,tb_product_temp_stock ts set p.ltd_stock=ts.ltd_quantity where p.product_serial_no=ts.luc_sku");
                SetStatus(null, " Cmd is End");
            }
        }
    }
}
