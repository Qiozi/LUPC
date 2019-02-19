using LUComputers.DBProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace LUComputers.Watch
{
    public class OtherIncInfo
    {
      
        public OtherIncInfo() { }

        public void UpdateToRemate()
        {
            Helper.ProcessHelper ph = new LUComputers.Helper.ProcessHelper();
            string filename = string.Format("{0}_{1}.csv", Config.soft_download_path, "other_inc_info");
            ph.LoadFromLocal(filename, @"select id, other_inc_name, other_inc_type, tag, inc_record,inc_record_valid, inc_record_match, bigger_than_lu, less_than_lu,equal_than_lu,last_run_date from tb_other_inc ");
            //
            // update
            //
            Config.RemoteExecuteNonQuery(string.Format(@"delete from tb_other_inc;"));
            Config.UpdateLoadToRemoteByCSVTab(filename.Replace("\\", "\\\\"), "tb_other_inc", "( id, other_inc_name, other_inc_type, tag, inc_record,inc_record_valid, inc_record_match, bigger_than_lu, less_than_lu,equal_than_lu,last_run_date )");


            filename = string.Format("{0}_{1}.csv", Config.soft_download_path, "other_inc_match_lu_sku");
            ph.LoadFromLocal(filename, @"select id, lu_sku, other_inc_sku, other_inc_type, tmp, is_have_info from tb_other_inc_match_lu_sku ");

            Config.RemoteExecuteNonQuery(string.Format(@"delete from tb_other_inc_match_lu_sku;"));
            Config.UpdateLoadToRemoteByCSVTab(filename.Replace("\\", "\\\\"), "tb_other_inc_match_lu_sku", "( id, lu_sku, other_inc_sku, other_inc_type, tmp, is_have_info)");


        }
    }
}
