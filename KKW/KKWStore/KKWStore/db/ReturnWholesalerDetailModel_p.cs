using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;

namespace KKWStore.db
{
    public class ReturnWholesalerDetailModel_p 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="brand"></param>
        /// <returns></returns>
        public static DataTable GetReturnHistoryList(DateTime beginDate, DateTime endDate, string brand, string p_code)
        {
            string sql = string.Format(@"select rw.ReturnCode '批次',rwd.sn '条型码', rwd.p_code '编号'
, rwd.p_name '商品名称'
, s.in_cost '成本'
, date_format(s.in_regdate,'%Y-%m-%d') '进货日期'
, date_format(rw.regdate,'%Y-%m-%d') '退货日期'
, rwd.Brand '品牌' 
, case when rw.IsSure = 0 then '退货中' else '已退货' end '状态'
from tb_return_wholesaler rw inner join tb_return_wholesaler_detail rwd
                                        on rw.returncode=rwd.returncode 
                                        inner join tb_serial_no_and_p_code s on s.serialno=rwd.sn
                                        where date_format(rw.regdate, '%Y%j') >= date_format('{0}', '%Y%j')
                                            and date_format(rw.regdate, '%Y%j')<= date_format('{1}','%Y%j')
                                            {2} {3}"
                                                , beginDate.ToString("yyyy-MM-dd")
                                                , endDate.ToString("yyyy-MM-dd")
                                                , !string.IsNullOrEmpty(brand) ? " and rwd.brand='" + brand + "'" : ""
                                                , !string.IsNullOrEmpty(p_code) ? " and rwd.p_code='"+ p_code +"'" : ""
                                                );
            return db.SqlExec.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 获取 Brand
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> GetBrandName()
        {
            DataTable dt = db.SqlExec.ExecuteDataTable("Select distinct Brand from tb_return_wholesaler_detail order by Brand asc");
            List<Supplier> list = new List<Supplier>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(new Supplier()
                {
                    Cost = "1000000",
                    Name = dr[0].ToString()
                });
            }
            return list;
        }
        /// <summary>
        /// 获取 SN 
        /// </summary>
        /// <param name="returnCode"></param>
        /// <returns></returns>
        public static List<string> GetReturnSN(string returnCode)
        {
            List<string> list = new List<string>();
            DataTable dt = db.SqlExec.ExecuteDataTable("Select SN from tb_return_wholesaler_detail where ReturnCode='" + returnCode + "'");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr[0].ToString());
            }
            return list;
        }
    }
}
