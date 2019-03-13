using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace KKWStore.db
{
    public class SerialNoAndPCodeModel_p
    {
        /// <summary>
        /// 判断是否已被使用。
        /// </summary>
        /// <param name="SerialNO"></param>
        /// <returns></returns>
        public static bool ExistSN(qstoreEntities DB, string serialNO)
        {
            //SerialNoAndPCodeModel[] pms = FindAllByProperty("SerialNO", SerialNO);
            //if (pms.Length > 0)
            //    return true;
            //return false;
            return DB.tb_serial_no_and_p_code.SingleOrDefault(p => p.SerialNO.Equals(serialNO)) != null;
        }

        /// <summary>
        /// 判断是否已被使用。
        /// </summary>
        /// <param name="DB"></param>
        /// <param name="serialNO"></param>
        /// <returns></returns>
        public static tb_serial_no_and_p_code GetModelBySerialNO(qstoreEntities DB, string serialNO)
        {
            //SerialNoAndPCodeModel[] pms = FindAllByProperty("SerialNO", SerialNO);
            //if (pms.Length > 0)
            //    return pms[0];
            //return null;
            return DB.tb_serial_no_and_p_code.SingleOrDefault(p => p.SerialNO.Equals(serialNO));
        }

        /// <summary>
        /// 纪录出库时间, 并删除库存
        /// </summary>
        /// <param name="SerialNO"></param>
        /// <returns></returns>
        public static bool OutStore(qstoreEntities DB, string SerialNO, DateTime date)
        {
            //SerialNoAndPCodeModel[] pms = FindAllByProperty("SerialNO", SerialNO);
            //if (pms.Length > 0)
            //{
            //    pms[0].out_regdate = DateTime.Now;                
            //    pms[0].Update();

            //    db.ProductModel_p.ChangeQuantity(pms[0].p_code);
            //}
            //return false;
            var query = DB.tb_serial_no_and_p_code.SingleOrDefault(p => p.SerialNO.Equals(SerialNO));
            if (query != null)
            {
                query.out_regdate = date;
                DB.SaveChanges();
                ProductModel_p.ChangeQuantity(DB, query.p_code);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 取得所有的库存纪录
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllStoreProduct()
        {
            return db.SqlExec.ExecuteDataTable(@"select s.SerialNo, p.p_code, p.p_name, s.in_cost cost from tb_serial_no_and_p_code s left join tb_product p on p.p_code=s.p_code 
where date_format(out_regdate, '%Y')<2000 and date_format(in_regdate, '%Y')>2000 and IsReturnWholesaler =0");
        }
        /// <summary>
        /// 取得单个产品的所有当前库存SN
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static DataTable GetValidListByProdCode(string p_code)
        {
            return db.SqlExec.ExecuteDataTable(@"select s.SerialNo, s.p_code,in_cost cost from tb_serial_no_and_p_code s 
where date_format(out_regdate, '%Y')<2000 and date_format(in_regdate, '%Y')>2000 and s.IsReturnWholesaler=0 and p_code='" + p_code + "' order by s.SerialNo asc");
        }

        public static void ChangeSingleProdCost(string p_code, decimal cost)
        {
            db.SqlExec.ExecuteNonQuery(@" update tb_serial_no_and_p_code set in_cost ='" + cost + @"' 
where date_format(out_regdate, '%Y')<2000 and date_format(in_regdate, '%Y')>2000 and IsReturnWholesaler=0 and p_code='" + p_code + @"';");
        }

        /// <summary>
        /// 取得单个产品的所有(显神仓库: SN = 100000000)库存
        /// 或其他仓库sn
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static DataTable GetValidListByProdCodeNoSN(int p_id, enums.WarehouseType warehouseType)
        {
            if (warehouseType == enums.WarehouseType.显神仓库)
                return db.SqlExec.ExecuteDataTable(@"select s.id, s.SerialNo, s.p_code,in_cost cost from tb_serial_no_and_p_code s 
where s.id>0 and s.SerialNo = '1000000000' and p_id='" + p_id + "' order by id asc ");
            else
            {
                return db.SqlExec.ExecuteDataTable(@"select s.id, s.SerialNo, s.p_code,in_cost cost from tb_serial_no_and_p_code s 
where s.id>0 and s.curr_warehouse_id = '" + ((int)warehouseType) + "' and p_id='" + p_id + "' and s.out_regdate like '00%' order by id asc ");
            }

        }
        /// <summary>
        /// 取得单个产品的所有(显神仓库: SN = 100000000)库存
        /// </summary>
        /// <param name="p_id"></param>
        /// <returns></returns>
        public static DataTable GetValidListByProdCodeNoSN(int p_id)
        {
            return GetValidListByProdCodeNoSN(p_id, enums.WarehouseType.显神仓库);
        }
        /// <summary>
        /// 取得单个产品的所有当前库存SN
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static DataTable GetValidQuantityByProdCode(string p_code, int? warehouseID)
        {
            return db.SqlExec.ExecuteDataTable(@"select s.id, s.SerialNo, s.in_cost, date_format(s.in_regdate,'%Y-%m-%d') in_regdate from tb_serial_no_and_p_code s 
where date_format(out_regdate, '%Y')<2000 and date_format(in_regdate, '%Y')>2000 and s.IsReturnWholesaler = 0 and s.curr_warehouse_id='" + warehouseID + "' and p_code='" + p_code + "' order by s.id asc");
        }

        /// <summary>
        /// 取得单个产品的所有当前库存SN , 按云仓代码
        /// </summary>
        /// <param name="yun_code"></param>
        /// <param name="warehouseID"></param>
        /// <returns></returns>
        public static DataTable GetValidQuantityByYunCode(string yun_code, int? warehouseID)
        {
            return db.SqlExec.ExecuteDataTable(@"select s.id, s.SerialNo, s.in_cost, date_format(s.in_regdate,'%Y-%m-%d') in_regdate from tb_serial_no_and_p_code s inner join tb_product p on s.p_id = p.id 
where date_format(s.out_regdate, '%Y')<2000 and date_format(s.in_regdate, '%Y')>2000 and s.IsReturnWholesaler = 0 and s.curr_warehouse_id='" + warehouseID + "' and p.yun_code='" + yun_code + "' order by s.id asc");
        }

        /// <summary>
        /// 取得当月出库数量
        /// </summary>
        /// <param name="yun_code"></param>
        /// <param name="warehouseID"></param>
        /// <returns></returns>
        public static DataTable GetMonthOutQuantityByYunCode(string yun_code, int warehouseID, DateTime date)
        {
            return db.SqlExec.ExecuteDataTable(@"select s.id, s.SerialNo, s.in_cost, date_format(s.in_regdate,'%Y-%m-%d') in_regdate from tb_serial_no_and_p_code s inner join tb_product p on s.p_id = p.id 
where date_format(s.out_regdate, '%Y%m')='" + date.ToString("yyyyMM") + "' and date_format(s.in_regdate, '%Y')>2000 and s.IsReturnWholesaler = 0 and s.curr_warehouse_id='" + warehouseID + "' and p.yun_code='" + yun_code + "' order by s.id asc");
        }

        public static DataTable GetValidSNByProdId(int p_id, int warehouseID)
        {
            return db.SqlExec.ExecuteDataTable(@"select s.id, s.SerialNo, s.in_cost, date_format(s.in_regdate,'%Y-%m-%d') in_regdate from tb_serial_no_and_p_code s 
where date_format(out_regdate, '%Y')<2000 and date_format(in_regdate, '%Y')>2000 and s.IsReturnWholesaler = 0 and s.curr_warehouse_id='" + warehouseID + "' and p_id='" + p_id + "'");
        }

        /// <summary>
        /// 取得单个产品的所有当前库存SN数量
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static int GetValidQuantityByProdCode(string p_code)
        {
            return int.Parse(db.SqlExec.ExecuteDataTable(@"select count(s.id) from tb_serial_no_and_p_code s 
where date_format(out_regdate, '%Y')<2000 and date_format(in_regdate, '%Y')>2000 and s.IsReturnWholesaler = 0 and p_code='" + p_code + "'").Rows[0][0].ToString());
        }

        /// <summary>
        /// 按批次， 取得某个产品的所有条码
        /// </summary>
        /// <param name="p_code"></param>
        /// <param name="inInvoiceCode"></param>
        /// <returns></returns>
        public static DataTable GetAllSNByProdInInvoice(string p_code, string inInvoiceCode)
        {
            return db.SqlExec.ExecuteDataTable(@"select id, SerialNo, in_cost, date_format(in_regdate, '%Y-%m-%d') regdate from tb_serial_no_and_p_code s 
where s.id>0 and p_code='" + p_code + "' and in_invoice_code='" + inInvoiceCode + "'");
        }
        /// <summary>
        /// 按仓库， 取得某个产品的所有未出库条码
        /// </summary>
        /// <param name="p_code"></param>
        /// <param name="WareID"></param>
        /// <returns></returns>
        public static DataTable GetAllInStoreSNByWare(string p_code, int WareID)
        {
            DataTable dt = db.SqlExec.ExecuteDataTable(@"select id, SerialNo, curr_warehouse_date,'' code_and_in_date,  in_cost, date_format(in_regdate, '%Y-%m-%d') regdate from tb_serial_no_and_p_code s 
where s.id>0 and p_code='" + p_code + "' and date_format(out_regdate, '%Y')<2000 and s.IsReturnWholesaler=0 and curr_warehouse_id='" + WareID + "'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code_and_in_date = string.Format("{0} {1}"
                    , dt.Rows[i]["SerialNo"].ToString()
                    , string.IsNullOrEmpty(dt.Rows[i]["curr_warehouse_date"].ToString()) ? "没日期" : DateTime.Parse(dt.Rows[i]["curr_warehouse_date"].ToString()).ToString("yyyy-MM-dd HH:mm"));
                dt.Rows[i]["code_and_in_date"] = code_and_in_date;
            }
            return dt;
        }
        /// <summary>
        /// 已售
        /// 取得单个产品的所有当前已售SN
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static DataTable GetInValidListByProdCode(string p_code)
        {
            return db.SqlExec.ExecuteDataTable(@"select s.SerialNo, s.p_code,in_cost cost, s.out_regdate from tb_serial_no_and_p_code s 
where date_format(out_regdate, '%Y')>2000 and p_code='" + p_code + "' and s.IsReturnWholesaler=0 order by out_regdate desc");
        }

        /// <summary>
        /// 取得产品Code
        /// </summary>
        /// <param name="SN"></param>
        /// <returns></returns>
        public static string GetProductCode(qstoreEntities context, string SN)
        {
            //SerialNoAndPCodeModel model = GetModelBySerialNO(SN);
            //return model == null ? "" : model.p_code;
            var model = GetModelBySerialNO(context, SN);
            return model == null ? string.Empty : model.p_code;
        }

        public static int GetProductId(qstoreEntities context, string SN)
        {
            //SerialNoAndPCodeModel model = GetModelBySerialNO(SN);
            //return model == null ? 0 : model.p_id;

            var model = GetModelBySerialNO(context, SN);
            return model == null ? 0 : model.p_id.Value;
        }


        /// <summary>
        /// 有判断SN集合里，是不是一个产品。
        /// </summary>
        /// <param name="sns"></param>
        /// <returns></returns>
        public static bool IsSingleProduct(db.qstoreEntities context, string[] sns)
        {

            string partCode = "";
            foreach (var sn in sns)
            {
                if (string.IsNullOrEmpty(sn.Trim()))
                    continue;
                string pCode = GetProductCode(context, sn);
                if (pCode != ""
                    && partCode != pCode
                    && partCode != "")
                    return false;
                partCode = pCode;

            }
            return true;
        }


        /// <summary>
        /// 盘点后， 人工出库产品。
        /// </summary>
        /// <param name="SerialNo"></param>
        /// <param name="adminName"></param>
        /// <returns></returns>
        public static bool AutoOut2(db.qstoreEntities context, string SerialNo, string comment = " 手工出库 ")
        {
            //SerialNoAndPCodeModel model = GetModelBySerialNO(SerialNo);
            var model = GetModelBySerialNO(context, SerialNo);
            if (model != null)
            {

                model.out_regdate = DateTime.Now;
                model.Comment = comment;
                //model.Update();
                context.SaveChanges();
                db.ProductModel_p.ChangeQuantity(context, model.p_code);

                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="Brand"></param>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static DataTable GetOutList(DateTime beginDate, DateTime endDate, string Brand, string p_code)
        {
            string sql = string.Format(@"
select distinct p.p_code '编号', p.WholesalerCode '批发商编号', p.p_name '商品名称', concat('{0}', ' 至', '{1}') '出库时间'
, s.in_cost '成本'
, count(s.id) '数量'
from tb_serial_no_and_p_code s inner join tb_product p on p.id=s.p_id     
    where date_format(s.out_regdate, '%Y%j') >= date_format('{0}', '%Y%j')
    and date_format(s.out_regdate, '%Y%j')<= date_format('{1}','%Y%j')
    and s.IsReturnWholesaler = 0
	{2} {3} 
group by p.p_code 
                       

                                               "
                , beginDate.ToString("yyyy-MM-dd")
                , endDate.ToString("yyyy-MM-dd")
                , !string.IsNullOrEmpty(Brand) ? " and p.brand='" + Brand + "'" : ""
                , !string.IsNullOrEmpty(p_code) ? " and p.p_code='" + p_code + "'" : "");

            return db.SqlExec.ExecuteDataTable(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="Brand"></param>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static DataTable GetOutListSN(DateTime beginDate, DateTime endDate, string Brand, string p_code)
        {
            string sql = string.Format(@"
select  s.SerialNO, date_format(s.out_regdate, '%Y-%m-%d') d
from tb_serial_no_and_p_code s inner join tb_product p on p.id=s.p_id     
    where date_format(s.out_regdate, '%Y%j') >= date_format('{0}', '%Y%j')
    and date_format(s.out_regdate, '%Y%j')<= date_format('{1}','%Y%j')
    and s.IsReturnWholesaler = 0
	{2} {3} order by s.in_regdate asc 

                       

                                               "
                , beginDate.ToString("yyyy-MM-dd")
                , endDate.ToString("yyyy-MM-dd")
                , !string.IsNullOrEmpty(Brand) ? " and p.brand='" + Brand + "'" : ""
                , !string.IsNullOrEmpty(p_code) ? " and p.p_code='" + p_code + "'" : "");

            return db.SqlExec.ExecuteDataTable(sql);
        }


        /// <summary>
        /// 退到经销商，
        /// 把日期改为0。
        /// 
        /// </summary>
        /// <param name="sn"></param>
        public static void ProductReturnWholesaler(long sn)
        {
            db.SqlExec.ExecuteNonQuery("Update tb_serial_no_and_p_code set IsReturnWholesaler=1, IsReturnWholesaler_regdate=now() where SerialNO='" + sn + "'");
        }

        /// <summary>
        /// 公司仓库扫描入库
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newSN"></param>
        /// <param name="warehouseID"></param>
        public static void ChangeSN(int sid, string newSN, int warehouseID, int p_id, string p_code, int oldWarehouseId)
        {
            db.SqlExec.ExecuteNonQuery("Update tb_serial_no_and_p_code set is_return=0, SerialNo='" + newSN + "', curr_warehouse_id='" + warehouseID + "', curr_warehouse_date=now() where id='" + sid + "'");

            SaveChangeSNRecord(sid, p_id, p_code, oldWarehouseId, Helper.Config.TmpSNCode, newSN, warehouseID);

        }

        /// <summary>
        /// 修改条码（SN） 所在的仓库（转库）
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="p_id"></param>
        /// <param name="p_code"></param>
        /// <param name="oldWarehouseId"></param>
        /// <param name="newSN"></param>
        /// <param name="newWarehouseId"></param>
        public static void ChangeSNOnWarehouse(int sid, int p_id, string p_code, int oldWarehouseId, string newSN, int newWarehouseId)
        {
            db.SqlExec.ExecuteNonQuery("Update tb_serial_no_and_p_code set curr_warehouse_id='" + newWarehouseId + "', curr_warehouse_date=now() where id='" + sid + "'");

            SaveChangeSNRecord(sid, p_id, p_code, oldWarehouseId, newSN, newSN, newWarehouseId);
        }

        /// <summary>
        /// 转库纪录
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="p_id"></param>
        /// <param name="p_code"></param>
        /// <param name="oldWT"></param>
        /// <param name="tmpSN"></param>
        /// <param name="newWT"></param>
        /// <param name="newSN"></param>
        public static void SaveChangeSNRecord(int sid, int p_id, string p_code, int oldWarehouseId, string tmpSN
            , string newSN
            , int newWarehouseI)
        {
            db.SqlExec.ExecuteNonQuery(string.Format(@"insert into tb_serial_no_change_storage(serialNo_id, p_id, p_code,old_warehouse_ID,old_warehouse_code,new_warehouse_ID,new_warehouse_code,CreateName,regdate)
                                                values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', now())"
                           , sid
                           , p_id
                           , p_code
                           , oldWarehouseId
                           , tmpSN
                           , newWarehouseI
                           , newSN
                           , Helper.Config.CurrentUser.user_name));
        }


        /// <summary>
        /// 取得仓库的库存金额
        /// </summary>
        /// <param name="context"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public static decimal GetStoreTotal(db.qstoreEntities context, int warehouseId, ref int count)
        {
            if (warehouseId == 0)
            {
                count = context.tb_serial_no_and_p_code.Count(p => p.is_return.HasValue && p.is_return.Value.Equals(false) &&
                                   p.out_regdate.HasValue && p.out_regdate.Value.Year < 2000);

                return context.tb_serial_no_and_p_code.Where(p => p.is_return.HasValue && p.is_return.Value.Equals(false) &&
                                   p.out_regdate.HasValue && p.out_regdate.Value.Year < 2000).Sum(p => p.in_cost.Value);
            }
            else
            {
                count = context.tb_serial_no_and_p_code.Count(p => p.is_return.HasValue &&
                                     p.is_return.Value.Equals(false) &&
                                     p.out_regdate.HasValue && p.out_regdate.Value.Year < 2000 &&
                                     p.curr_warehouse_id.HasValue &&
                                     p.curr_warehouse_id.Value.Equals(warehouseId));

                return context.tb_serial_no_and_p_code.Where(p => p.is_return.HasValue &&
                                    p.is_return.Value.Equals(false) &&
                                    p.out_regdate.HasValue && p.out_regdate.Value.Year < 2000 &&
                                    p.curr_warehouse_id.HasValue &&
                                    p.curr_warehouse_id.Value.Equals(warehouseId)).Sum(p => (decimal?)p.in_cost.Value).GetValueOrDefault();
            }
        }
    }
}
