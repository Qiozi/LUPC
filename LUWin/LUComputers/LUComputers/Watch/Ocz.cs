using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Windows.Forms;
using LUComputers.DBProvider;

namespace LUComputers.Watch
{
    public class Ocz
    {
        public Ocz() { }

        public void Run(Label lbl_ltd_name, Label lbl_part_mfp)
        {
            Helper.ProcessHelper.SubControlMaxSum = 4;

            LtdHelper LH= new LtdHelper();
                int ltd_id = LH.LtdHelperValue(Ltd.wholesaler_OCZ);


                WebClient webclieng = new WebClient();
                string new_file = string.Format("{0}ocz\\ocz_store_{1}.xls", Config.soft_download_path, DateTime.Now.ToString("yyyyMMddhhmmss"));
                webclieng.DownloadFile("http://customer.ocztechnology.com/csv/pricelist.csv", new_file);

                Helper.ProcessHelper.SubControlCurrentValue = 1;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string[] allLines = System.IO.File.ReadAllLines(new_file);
                string[] newLines = new string[allLines.Length -1];
                for (int i = 1; i < allLines.Length; i++)
                {
                    newLines[i - 1] = allLines[i].Replace("$ ", "");                    
                }
                System.IO.File.WriteAllLines(new_file, newLines);
                Config.ExecuteNonQuery("delete from  tb_other_inc_ocz");
                Helper.ProcessHelper.SubControlCurrentValue = 2;
                Config.ExecuteUpdateLoadByCSV(new_file.Replace("\\", "\\\\"), "tb_other_inc_ocz", "");

                Helper.ProcessHelper.SubControlCurrentValue = 3;
                Config.ExecuteNonQuery(string.Format(@"delete from  tb_other_inc_part_info where other_inc_id='{0}';
insert into tb_other_inc_part_info 
	( other_inc_id, other_inc_sku, manufacture_part_number, other_inc_price, other_inc_store_sum, 
	tag, 
	regdate, 
	last_regdate)
select {0}, item_code, item_code, base_price, on_hand, 1, now(), now() from tb_other_inc_ocz;
", ltd_id));


                //Helper.SaveNewMatch hsn = new Helper.SaveNewMatch();
                //hsn.FilterSave(Ltd.wholesaler_OCZ);
                //hsn.UpdateToRemote(Ltd.wholesaler_OCZ);
                //Config.ExecuteNonQuery(string.Format("update tb_other_inc set last_run_date=now() where id='{0}'", ltd_id));
                //Helper.ProcessHelper.SubControlCurrentValue = 4;
        }
    }
}
