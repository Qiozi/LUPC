using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace KKWStore.db
{
    public class ProductModel_p
    {
        /// <summary>
        /// 用产品编号查找
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static tb_product GetModelByCode(qstoreEntities DB, string p_code)
        {
            //ProductModel[] pms = FindAllByProperty("p_code", p_code);
            //if (pms.Length > 0)
            //    return pms[0];
            //return null;

            var query = DB.tb_product.SingleOrDefault(p => p.p_code.Equals(p_code));
            return query;
        }

        /// <summary>
        /// 用产品编号查找
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static tb_product GetModelBySN(qstoreEntities DB, string SerialNO, ref bool invalid)
        {
            //SerialNoAndPCodeModel[] pms = SerialNoAndPCodeModel.FindAllByProperty("SerialNO", SerialNO);
            //if (pms.Length > 0)
            //{
            //    if (pms[0].out_regdate.Year > 2000)
            //    {
            //        invalid = true;
            //    }
            //    return GetModelByCode(pms[0].p_code);
            //}
            //return null;

            var query = DB.tb_serial_no_and_p_code.SingleOrDefault(p => p.SerialNO.Equals(SerialNO));
            if (query != null)
            {
                if (query.out_regdate.HasValue)
                {
                    if (query.out_regdate.Value.Year > 2000)
                    {
                        invalid = true;
                    }
                }
                return GetModelByCode(DB, query.p_code);
            }
            return null;
        }
        /// <summary>
        /// 变更产品数量
        /// </summary>
        /// <param name="p_code"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static bool ChangeQuantity(qstoreEntities DB, string p_code, decimal? Cost)
        {
            //ProductModel pm = GetModelByCode(p_code);
            //if (pm != null)
            //{
            //    if (Cost != null)
            //        pm.p_cost = decimal.Parse(Cost.ToString());
            //    //pm.p_quantity = db.SerialNoAndPCodeModel_p.GetValidListByProdCode(p_code).Rows.Count;
            //    pm.p_quantity = db.SerialNoAndPCodeModel_p.GetValidQuantityByProdCode(p_code);
            //    pm.Update();
            //    return true;
            //}
            //else
            //    return false;

            var query = GetModelByCode(DB, p_code);
            if (query != null)
            {
                if (Cost != null)
                {
                    query.p_cost = Cost.Value;
                    query.p_quantity = SerialNoAndPCodeModel_p.GetValidQuantityByProdCode(p_code);
                    DB.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 变更产品数量
        /// </summary>
        /// <param name="p_code"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static bool ChangeQuantity(qstoreEntities DB, string p_code)
        {
            return ChangeQuantity(DB, p_code, null);
        }
        /// <summary>
        /// 查询取得列表示
        /// 2014-09-03  全部改为，显神，公司，已打包
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static DataTable GetModelsByKeyword(string keyword, enums.stock st, bool IsWarn, string brand, int warehouseID)
        {
            string sql = "";
            string paramsSql = " ";
            if (st == enums.stock.noStock)
            {
                paramsSql = " and p.p_quantity=0 ";
            }
            if (st == enums.stock.stock)
            {
                paramsSql = " and p.p_quantity>0 ";
            }

            if (IsWarn)
            {
                paramsSql += " and p.WarnQty > p.p_quantity ";
            }

            if (brand != "全部" && brand != null)
            {
                paramsSql += " and p.Brand = '" + brand + "' ";
            }

            DataTable dt = db.SqlExec.ExecuteDataTable("select p_code from tb_serial_no_and_p_code where serialno='" + keyword + "'");
            if (dt.Rows.Count == 1)
            {
                keyword = dt.Rows[0][0].ToString();
            }

            var pidAndStoreDT = db.SqlExec.ExecuteDataTable(@"
select distinct p_id, count(p_id) qty, curr_warehouse_id
        from tb_serial_no_and_p_code s inner
            join tb_warehouse w on w.id = s.curr_warehouse_id
        where out_regdate like '000%'
        group by curr_warehouse_id, p_id");



            sql = string.Format(@"
select p.id,
    p.p_code,
    p.p_name,
    WholesalerCode WholesalerCode,
    WarnQty WarnQty, 
    WholesalerUrl WholesalerUrl, 
    p_taobao_url p_taobao_url, 
    p_cost p_cost, 
    '0' costALL, 
    '0' c, 
    p.yun_code,
    '0' companyQty,
    '0' badQty,
    '0' yunQty
from tb_product p 
where 
    p.id>0 and  
    p.showit=1 " + paramsSql + " and " +
@"(p.p_code like '%{0}%' or p.p_name like '%{0}%' or p.WholesalerCode like '%{0}%' or p.yun_code like '%{0}%') group by p.p_code, p.p_name order by p.p_name asc", keyword);
            var queryDT = db.SqlExec.ExecuteDataTable(sql);
            for (var i = 0; i < queryDT.Rows.Count; i++)
            {
                int companyQty = 0, badQty = 0, yunQty = 0, tempQty = 0;
                DataRow dr = queryDT.Rows[i];
                foreach (DataRow qtyDR in pidAndStoreDT.Rows)
                {
                    if (qtyDR["p_id"].ToString() == dr["id"].ToString())
                    {
                        if (qtyDR["curr_warehouse_id"].ToString() == "9") // 云仓
                        {
                            int.TryParse(qtyDR["qty"].ToString(), out yunQty);
                        }
                        if (qtyDR["curr_warehouse_id"].ToString() == "1")// 公司
                        {
                            int.TryParse(qtyDR["qty"].ToString(), out companyQty);
                        }
                        if (qtyDR["curr_warehouse_id"].ToString() == "5")// 瑕次
                        {
                            int.TryParse(qtyDR["qty"].ToString(), out badQty);
                        }
                        if (qtyDR["curr_warehouse_id"].ToString() == "8")// 中转
                        {
                            int.TryParse(qtyDR["qty"].ToString(), out tempQty);
                        }

                    }
                }
                queryDT.Rows[i]["c"] = (companyQty + badQty + yunQty + tempQty).ToString();
                queryDT.Rows[i]["companyQty"] = companyQty;
                queryDT.Rows[i]["yunQty"] = yunQty;
                queryDT.Rows[i]["badQty"] = badQty;
            }


            //string subSQL = " (select count(id) from tb_serial_no_and_p_code where p.id=p_id and date_format(out_regdate, '%Y')<2000  " + (warehouseID == 0 ? "" : " and curr_warehouse_id=" + warehouseID + "") + ") sub_qty, (select sum(in_cost) from tb_serial_no_and_p_code where p.id=p_id and date_format(out_regdate, '%Y')<2000  " + (warehouseID == 0 ? "" : " and curr_warehouse_id=" + warehouseID + "") + ") sub_total ";

            //if (warehouseID == 0)
            //{
            //    if (keyword.Length == 0)
            //    {
            //        sql = "select p.id,p.p_code,p.p_name,max(WholesalerCode) WholesalerCode,max(WarnQty) WarnQty, max(WholesalerUrl) WholesalerUrl, max(p_taobao_url) p_taobao_url, max(p_cost) p_cost, ifnull(sum(s.in_cost),0) costALL, ifnull(count(s.id),0) c, p.yun_code from tb_product p left join (select ss.id, ss.in_cost,ss.p_id from tb_serial_no_and_p_code ss inner join tb_warehouse_all_info wi on ss.curr_warehouse_id=wi.id and ss.id>0 and date_format(ss.out_regdate, '%Y')<2000 and ss.IsReturnWholesaler=0) s on p.id=s.p_id  where p.id>0 and p.showit=1 " + paramsSql + " group by p.p_code, p.p_name order by p.p_name asc";
            //    }
            //    else
            //        sql = string.Format("select p.id,p.p_code,p.p_name,max(WholesalerCode) WholesalerCode,max(WarnQty) WarnQty, max(WholesalerUrl) WholesalerUrl, max(p_taobao_url) p_taobao_url, max(p_cost) p_cost, ifnull(sum(s.in_cost),0) costALL, ifnull(count(s.id),0) c, p.yun_code  from tb_product p left join (select ss.id, ss.in_cost,ss.p_id from tb_serial_no_and_p_code ss inner join tb_warehouse_all_info wi on ss.curr_warehouse_id=wi.id and ss.id>0 and date_format(ss.out_regdate, '%Y')<2000 and ss.IsReturnWholesaler=0) s on p.id=s.p_id where p.id>0 and p.showit=1 " + paramsSql + " and (p.p_code like '%{0}%' or p.p_name like '%{0}%' or p.WholesalerCode like '%{0}%' or p.yun_code like '%{0}%')  group by p.p_code, p.p_name order by p.p_name asc", keyword);
            //}
            //else
            //{
            //    // 公司可以显示为0，其他仓库没货不显示。
            //    //
            //    if (warehouseID != (int)enums.WarehouseType.公司仓库)
            //    {
            //        if (keyword.Length == 0)
            //        {
            //            sql = "select p.id,p.p_code,p.p_name,max(WholesalerCode) WholesalerCode,max(WarnQty) WarnQty, max(WholesalerUrl) WholesalerUrl, max(p_taobao_url) p_taobao_url, max(p_cost) p_cost, ifnull(sum(s.in_cost),0) costALL, ifnull(count(s.id),0) c, p.yun_code from tb_product p inner join tb_serial_no_and_p_code s on p.id=s.p_id  and s.id>0 and date_format(s.out_regdate, '%Y')<2000 and s.IsReturnWholesaler=0 and s.curr_warehouse_id=" + warehouseID + " where p.id>0 and p.showit=1 " + paramsSql + " group by p.p_code, p.p_name order by p.p_name asc";
            //        }
            //        else
            //        {
            //            sql = string.Format("select p.id,p.p_code,p.p_name,max(WholesalerCode) WholesalerCode,max(WarnQty) WarnQty, max(WholesalerUrl) WholesalerUrl, max(p_taobao_url) p_taobao_url, max(p_cost) p_cost, ifnull(sum(s.in_cost),0) costALL, ifnull(count(s.id),0) c, p.yun_code  from tb_product p inner join tb_serial_no_and_p_code s on p.id=s.p_id and s.id>0 and date_format(s.out_regdate, '%Y')<2000 and s.IsReturnWholesaler=0 and s.curr_warehouse_id=" + warehouseID + "  where p.id>0 and p.showit=1 " + paramsSql + " and (p.p_code like '%{0}%' or p.p_name like '%{0}%' or p.WholesalerCode like '%{0}%' or p.yun_code like '%{0}%' or p.yun_code like '%{0}%')  group by p.p_code, p.p_name order by p.p_name asc", keyword);
            //        }
            //    }
            //    else
            //    {
            //        if (keyword.Length == 0)
            //        {
            //            sql = "select p.id,p.p_code,p.p_name,max(WholesalerCode) WholesalerCode,max(WarnQty) WarnQty, max(WholesalerUrl) WholesalerUrl, max(p_taobao_url) p_taobao_url, max(p_cost) p_cost, ifnull(sum(s.in_cost),0) costALL, ifnull(count(s.id),0) c, p.yun_code from tb_product p left join tb_serial_no_and_p_code s on p.id=s.p_id  and s.id>0 and date_format(s.out_regdate, '%Y')<2000 and s.IsReturnWholesaler=0 and s.curr_warehouse_id=" + warehouseID + " where p.id>0 and p.showit=1 " + paramsSql + " group by p.p_code, p.p_name order by p.p_name asc";
            //        }
            //        else
            //        {
            //            sql = string.Format("select p.id,p.p_code,p.p_name,max(WholesalerCode) WholesalerCode,max(WarnQty) WarnQty, max(WholesalerUrl) WholesalerUrl, max(p_taobao_url) p_taobao_url, max(p_cost) p_cost, ifnull(sum(s.in_cost),0) costALL, ifnull(count(s.id),0) c, p.yun_code  from tb_product p left join tb_serial_no_and_p_code s on p.id=s.p_id and s.id>0 and date_format(s.out_regdate, '%Y')<2000 and s.IsReturnWholesaler=0 and s.curr_warehouse_id=" + warehouseID + "  where p.id>0 and p.showit=1 " + paramsSql + " and (p.p_code like '%{0}%' or p.p_name like '%{0}%' or p.WholesalerCode like '%{0}%' or p.yun_code like '%{0}%')  group by p.p_code, p.p_name order by p.p_name asc", keyword);
            //        }
            //    }
            //}

            return queryDT;// db.SqlExec.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 取得显神仓库，没有条码的产品
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static DataTable GetModelsTmpSNByKeyword(string keyword)
        {
            return db.SqlExec.ExecuteDataTable(string.Format(@"select p.id, p.p_name, p.p_code, sum(s.in_cost) total, count(s.id) c from 
tb_product p inner join tb_serial_no_and_p_code s on s.p_id=p.id
where serialno='1000000000' and p.id>0 and s.id>0 and p.p_code like '%{0}%' and p.p_name like '%{0}%' group by p.p_name, p.id, p.p_code", keyword));

        }

        /// <summary>
        /// 取得中转仓库所有产品。
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static DataTable GetModelsTransferSNByKeyword(string keyword)
        {
            return db.SqlExec.ExecuteDataTable(string.Format(@"select p.id, p.p_name, p.p_code, sum(s.in_cost) total, count(s.id) c from 
tb_product p inner join tb_serial_no_and_p_code s on s.p_id=p.id
where s.curr_warehouse_id ='" + ((int)enums.WarehouseType.中转_未分配) + "' and p.id>0 and s.id>0 and s.out_regdate like '00%' and (p.p_code like '%{0}%' or p.p_name like '%{0}%') group by p.p_name, p.id, p.p_code", keyword));

        }
        /// <summary>
        /// 获取某个仓库的所有产品
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="wt"></param>
        /// <returns></returns>
        public static DataTable GetModelsByKeyword(string keyword, enums.WarehouseType wt)
        {
            return db.SqlExec.ExecuteDataTable(string.Format(@"
                    select p.id, p.p_name, p.p_code, sum(s.in_cost) total, count(s.id) c ,
                            (SELECT COUNT(id) FROM tb_serial_no_and_p_code WHERE p_id=s.p_id AND out_regdate LIKE '{2}%') outCount

                    from 
                    tb_product p inner join tb_serial_no_and_p_code s on s.p_id=p.id
                    where s.curr_warehouse_id='{1}' and date_format(s.out_regdate,'%Y')<2000 and s.IsReturnWholesaler=0 and p.id>0 and s.id>0 and (p.p_code like '%{0}%' or p.yun_code like '%{0}%' or p.p_name like '%{0}%') group by p.p_name, p.id, p.p_code order by p.p_code asc"
                        , keyword
                        , (int)wt
                        , DateTime.Now.ToString("yyyy-MM-dd")));

        }
        /// <summary>
        /// 修改产品编码
        /// </summary>
        /// <param name="p_id"></param>
        /// <param name="new_p_code"></param>
        public static void ChangeProductCode(int p_id, string new_p_code)
        {
            db.SqlExec.ExecuteNonQuery(string.Format(@"
update tb_serial_no_and_p_code set p_code='{1}' where p_id='{0}';
update tb_return_history set p_code='{1}' where p_id='{0}';
update tb_in_invoice_product set p_code='{1}' where p_id='{0}';
update tb_check_store_detail set p_code='{1}' where p_id='{0}';", p_id, new_p_code));

        }

        /// <summary>
        /// 删除产品， 假
        /// </summary>
        /// <param name="p_id"></param>
        public static void DeleteById(int p_id)
        {
            if (db.SqlExec.ExecuteScalarInt("select count(id) from tb_serial_no_and_p_code where p_id='" + p_id + "'") > 0)
                db.SqlExec.ExecuteNonQuery(string.Format(@"
update tb_product set showit='0' where id='{0}'", p_id));
            else
                db.SqlExec.ExecuteNonQuery("delete from tb_product where id='" + p_id + "'");
        }

        /// <summary>
        /// 取得所有品牌
        /// </summary>
        /// <returns></returns>
        public static List<string> GetBrand()
        {

            GenerateBrand();


            List<string> list = new List<string>();

            DataTable dt = db.SqlExec.ExecuteDataTable("select distinct brand from tb_product order by brand asc");
            for (int i = 0; i < dt.Rows.Count; i++)
                list.Add(dt.Rows[i][0].ToString());
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetProdCodes()
        {
            List<string> list = new List<string>();

            DataTable dt = db.SqlExec.ExecuteDataTable("select distinct p_code from tb_product order by p_code asc");
            for (int i = 0; i < dt.Rows.Count; i++)
                list.Add(dt.Rows[i][0].ToString());
            return list;
        }

        /// <summary>
        /// 对已有的数据生成brand
        /// 从产品编号的头部取得-号前
        /// </summary>
        public static void GenerateBrand()
        {
            DataTable dt = db.SqlExec.ExecuteDataTable("select distinct p_code from tb_product where showit=1 and (brand = '' or brand is null)");

            foreach (DataRow dr in dt.Rows)
            {
                string p_code = dr[0].ToString();
                if (p_code.IndexOf("-") > -1)
                {
                    string brand = p_code.Split(new char[] { '-' })[0];
                    db.SqlExec.ExecuteNonQuery("update tb_product set brand = '" + brand + "' where p_code='" + p_code + "'");

                }
            }
        }
    }
}
