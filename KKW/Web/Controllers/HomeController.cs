using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.DB;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new DB.qstoreEntities();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult UploadFileTemp(Guid gid)
        {
            ViewBag.Gid = gid;
            return View("");
        }
        [HttpPost]
        public ActionResult UploadFileTemp(HttpPostedFileBase fileData, Guid gid)
        {
            try
            {
                if (fileData != null && fileData.ContentLength > 0)
                {
                    var context = new DB.qstoreEntities();
                    var query = context.tbtokens.SingleOrDefault(me => me.Gid.Equals(gid) && me.Showit.Equals(true));
                    if (query == null)
                    {
                        return Content("上传失败, Gid is falid.");
                    }
                    else
                    {
                        query.Showit = false;
                        context.SaveChanges();
                    }

                    string fileSave = Server.MapPath("~/Files/");
                    //获取文件的扩展名
                    string extName = Path.GetExtension(fileData.FileName);
                    //得到一个新的文件名称
                    string newName = Guid.NewGuid().ToString() + extName;
                    var fullname = Path.Combine(fileSave, newName);
                    fileData.SaveAs(fullname);

                    ReadExcel(context, fullname, query.UserId, query.UserName);
                    return Content(query.UserName + " 任务完成，");
                }
            }
            catch (Exception ex) { return Content(ex.Message); }
            return Content("。。。");
        }

        private List<MatchItem> MatchItems { get; set; }


        int GetQty(qstoreEntities db, string code)
        {
            return db.tb_serial_no_and_p_code.Count(me => me.p_code.Equals(code) &&
                                                          me.curr_warehouse_id.HasValue &&
                                                          me.curr_warehouse_id.Value.Equals(Config.YunWarehouseId) &&
                                                          me.IsReturnWholesaler.HasValue &&
                                                          !me.IsReturnWholesaler.Value &&
                                                          (!me.out_regdate.HasValue || me.out_regdate.Value.Year < 2000));
        }
        Item FindCode(qstoreEntities context, string yunCode)
        {
            if (string.IsNullOrEmpty(yunCode))
            {
                return null;
            }

            try
            {
                var query = context.tb_product.SingleOrDefault(me => me.yun_code.Equals(yunCode));
                if (query != null)
                {
                    return new Item
                    {
                        Code = query.p_code,
                        Qty = GetQty(context, query.p_code)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("重复编号： " + yunCode);
            }

            return null;
        }
        private void ReadExcel(qstoreEntities context, string filename, int userId, string userName)
        {
            if (string.IsNullOrEmpty(filename))
                throw new Exception("文件不存在");

            var dt = new Util.ExcelHelper(filename).ExcelToDataTable("Sheet0", true);

            MatchItems = new List<MatchItem>();

            // 保存文件数据
            var fileInfo = new DB.tb_yun_stock_main
            {
                FileName = Path.GetFileName(filename),
                regdate = DateTime.Now,
                StaffId = userId,
                StaffName = userName
            };
            context.tb_yun_stock_main.Add(fileInfo);
            context.SaveChanges();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var yunCode = dr["商品编码"].ToString();
                int yunQty = 0;
                int.TryParse(dr["可用量"].ToString(), out yunQty);
                var item = FindCode(context, yunCode.Trim());

                if (item == null) // 不存在，创建新的。
                {
                    GenerateNew(context, userName);
                    item = FindCode(context, yunCode.Trim());
                }

                MatchItems.Add(new MatchItem
                {
                    LocalItem = item,
                    YunCode = yunCode,
                    YunQty = yunQty,
                    YunProdName = (string.IsNullOrEmpty(dr["商品规格"].ToString()) ? "" : "[" + dr["商品规格"].ToString() + "]") + dr["商品名称"].ToString()
                });

                var fileContextInfo = new tb_yun_stock_child
                {
                    ParentId = fileInfo.Id,
                    ProdCode = yunCode,
                    ProdName = dr["商品名称"].ToString(),
                    ProdQtyAvailability = yunQty,
                    ProdQtyTotal = int.Parse(dr["总量"].ToString()),
                    ProdQtyUsedOrder = int.Parse(dr["订单占用量"].ToString()),
                    ProdQtyWarn = int.Parse(dr["警戒量"].ToString()),
                    ProdSN = dr["商品条码"].ToString(),
                    ProdStandard = dr["商品规格"].ToString(),
                    Regdate = DateTime.Now
                };
                context.tb_yun_stock_child.Add(fileContextInfo);

            }
            context.SaveChanges();
            Done(context, userId, userName);
        }
        /// <summary>
        /// 取得新的单据编码
        /// </summary>
        /// <returns></returns>
        public static string NewInvoiceCode()
        {
            if (SqlExec.ExecuteScalarInt("Select count(*) from tb_in_invoice") > 0)
            {
                string serial = "00000" + (SqlExec.ExecuteScalarInt("Select max(id) from tb_in_invoice") + 1).ToString();
                return DateTime.Now.ToString("yyMMdd") + serial.Substring(serial.Length - 6);
            }
            else
                return DateTime.Now.ToString("yyMMdd") + "000001";
        }
        /// <summary>
        /// 取个未用的serialno
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetSerialNo(qstoreEntities context)
        {
            var query = context.tb_serial_no.FirstOrDefault(me => me.is_print.HasValue && me.is_print.Value.Equals(false));
            if (query == null)
            {
                throw new Exception("serial no store is null.");
            }
            query.is_print = true;
            context.SaveChanges();
            return query.SerialNo;
        }
        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="context"></param>
        /// <param name="qty"></param>
        /// <param name="item"></param>
        public void InStore(qstoreEntities context, int qty, MatchItem item, DateTime inDate, int userId, string userName)
        {
            var snArray = new List<string>();
            var prod = context.tb_product.Single(me => me.p_code.Equals(item.LocalItem.Code));
            string invoiceCode = NewInvoiceCode();

            string supplier = "未知，系统自动处理";
            var total = 0M;
            var yunCost = 0M;
            if (!string.IsNullOrEmpty(prod.yun_code))
            {
                var yunInfo = context.tb_yun_stock_price.FirstOrDefault(me => me.yun_code.Equals(prod.yun_code));
                if (yunInfo != null)
                {
                    yunCost = yunInfo.yun_cost;
                }
            }

            var cost = yunCost > 0 ? yunCost : prod.p_cost.Value;
            prod.p_cost = cost; // 变化为新的cost
            total = qty * cost;

            var iim = new tb_in_invoice
            {
                curr_warehouse_id = Config.YunWarehouseId,
                input_regdate = inDate,
                invoice_code = invoiceCode,
                pay_method = "现金",
                note = "云仓库存同步，入库",
                pay_total = total,
                staff = userName,
                regdate = inDate,
                summary = string.Empty,
                Supplier = supplier

            };

            context.tb_in_invoice.Add(iim);

            context.SaveChanges();

            var iipm = new tb_in_invoice_product
            {
                cost = cost,
                in_invoice_code = iim.invoice_code,
                in_invoice_id = iim.id,
                p_code = prod.p_code,
                p_id = prod.id,
                quantity = qty,
                regdate = inDate
            };

            context.tb_in_invoice_product.Add(iipm);
            context.SaveChanges();

            for (int i = 0; i < qty; i++)
            {
                var sn = GetSerialNo(context);
                snArray.Add(sn);
                var snpm = new tb_serial_no_and_p_code
                {
                    IsFree = false,
                    curr_warehouse_date = inDate,
                    curr_warehouse_id = Config.YunWarehouseId,
                    Comment = string.Empty,
                    in_cost = cost,
                    in_invoice_code = iim.invoice_code,
                    in_regdate = inDate,
                    is_order_code = false,
                    is_return = false,
                    IsReturnWholesaler = false,
                    IsReturnWholesaler_regdate = DateTime.MinValue,
                    out_regdate = DateTime.MinValue,
                    p_code = prod.p_code,
                    SerialNO = sn,
                    p_id = prod.id,
                    regdate = inDate,
                    return_regdate = DateTime.MinValue
                };

                context.tb_serial_no_and_p_code.Add(snpm);

            }
            context.SaveChanges();
            SaveHistory(context, snArray, prod, item, userId, userName, "in");
            ChangeQuantity(context, iipm.p_code, cost);  // 变化库存数量
        }
        /// <summary>
        /// 保存 sn 操作记录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="snArray"></param>
        /// <param name="pm"></param>
        /// <param name="item"></param>
        /// <param name="type"></param>
        void SaveHistory(qstoreEntities context, List<string> snArray, tb_product pm, MatchItem item, int userId, string userName, string type = "out")
        {
            var asyncRecord = new tb_yun_stock_async
            {
                p_code = pm.p_code,
                p_id = pm.id,
                p_qty = item.LocalItem.Qty,
                Regdate = DateTime.Now,
                StaffId = userId,
                StaffName = userName,
                yun_code = item.YunCode,
                yun_qty = item.YunQty
            };
            context.tb_yun_stock_async.Add(asyncRecord);
            context.SaveChanges();

            foreach (var sn in snArray)
            {
                if (!string.IsNullOrEmpty(sn.Trim()))
                {
                    // sn 记录
                    var outRecord = new tb_yun_stock_async_code
                    {
                        cmd = type,
                        p_code = item.LocalItem.Code,
                        p_id = pm.id,
                        regdate = DateTime.Now,
                        serial_no = sn,
                        staff_id = userId,
                        staff_name = userName,
                        yun_stock_async_id = asyncRecord.Id
                    };
                    context.tb_yun_stock_async_code.Add(outRecord);
                }
            }
            context.SaveChanges();
        }

        public tb_product GetModelByCode(qstoreEntities DB, string p_code)
        {
            var query = DB.tb_product.SingleOrDefault(p => p.p_code.Equals(p_code));
            return query;
        }
        /// <summary>
        /// 变更产品数量
        /// </summary>
        /// <param name="p_code"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool ChangeQuantity(qstoreEntities DB, string p_code, decimal? Cost)
        {

            var query = GetModelByCode(DB, p_code);
            if (query != null)
            {
                if (Cost != null)
                {
                    query.p_cost = Cost.Value;
                    query.p_quantity = GetValidQuantityByProdCode(p_code);
                    DB.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 取得单个产品的所有当前库存SN数量
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static int GetValidQuantityByProdCode(string p_code)
        {
            return int.Parse(SqlExec.ExecuteDataTable(@"select count(s.id) from tb_serial_no_and_p_code s 
where date_format(out_regdate, '%Y')<2000 and date_format(in_regdate, '%Y')>2000 and s.IsReturnWholesaler = 0 and p_code='" + p_code + "'").Rows[0][0].ToString());
        }
        /// <summary>
        /// 取得单个产品的所有当前库存SN
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        DataTable GetValidQuantityByProdCode(string p_code, int? warehouseID)
        {
            return SqlExec.ExecuteDataTable(@"select s.id, s.SerialNo, s.in_cost, date_format(s.in_regdate,'%Y-%m-%d') in_regdate from tb_serial_no_and_p_code s 
where date_format(out_regdate, '%Y')<2000 and date_format(in_regdate, '%Y')>2000 and s.IsReturnWholesaler = 0 and s.curr_warehouse_id='" + warehouseID + "' and p_code='" + p_code + "' order by s.id asc");
        }

        void Done(qstoreEntities context, int userId, string userName)
        {
            foreach (var item in MatchItems)
            {
                if (item.LocalItem != null)
                {
                    var diffQty = item.YunQty - item.LocalItem.Qty;
                    if (diffQty > 0)
                    {
                        InStore(context, diffQty, item, DateTime.Now, userId, userName);
                    }
                    if (diffQty < 0)
                    {
                        OutStore(context, diffQty, item, DateTime.Now, userId, userName);
                    }
                }
            }
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="context"></param>
        /// <param name="qty"></param>
        /// <param name="item"></param>
        public void OutStore(qstoreEntities context, int qty, MatchItem item, DateTime outDate, int userId, string userName)
        {
            qty = 0 - qty;
            var snArray = new List<string>();
            DataTable singelDT = GetValidQuantityByProdCode2(item.LocalItem.Code, Config.YunWarehouseId);
            if (singelDT.Rows.Count < qty)
            {
                throw new Exception(item.LocalItem.Code + " 库存不足");
            }
            for (var i = singelDT.Rows.Count - 1; i >= 0; i--)
            {
                if (i >= qty)
                {
                    singelDT.Rows.RemoveAt(i);
                }
            }

            var _total = 0M;
            foreach (DataRow dr in singelDT.Rows)
            {
                decimal cost = 0M;
                decimal.TryParse(dr["in_cost"].ToString(), out cost);
                _total += cost;
            }
            var iim = new tb_out_invoice();
            iim.input_regdate = DateTime.Now;
            iim.invoice_code = DateTime.Now.ToString("yyMMddhhmmss");
            iim.pay_total = _total;
            iim.Price = _total.ToString();
            iim.staff = userName;
            iim.regdate = DateTime.Now;
            context.tb_out_invoice.Add(iim);
            context.SaveChanges();

            int count = 0;

            var pm = GetModelByCode(context, item.LocalItem.Code);

            foreach (DataRow dr in singelDT.Rows)
            {
                string sn = dr["SerialNo"].ToString();
                if (!string.IsNullOrEmpty(sn))
                {
                    snArray.Add(sn);
                    var iipm = new tb_out_invoice_product();

                    iipm.SerialNO = sn;
                    iipm.out_invoice_code = iim.invoice_code;
                    iipm.out_invoice_id = iim.id;
                    iipm.p_id = pm == null ? 0 : pm.id;
                    iipm.regdate = DateTime.Now;

                    OutStore(context, iipm.SerialNO, outDate);
                    context.tb_out_invoice_product.Add(iipm);
                    count += 1;
                }
            }
            SaveHistory(context, snArray, pm, item, userId, userName, "out");
            iim.SN_Quantity = count;
            context.SaveChanges();

        }

        /// <summary>
        /// 取得单个产品的所有当前库存SN
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public DataTable GetValidQuantityByProdCode2(string p_code, int? warehouseID)
        {
            return SqlExec.ExecuteDataTable(@"select s.id, s.SerialNo, s.in_cost, date_format(s.in_regdate,'%Y-%m-%d') in_regdate from tb_serial_no_and_p_code s 
where date_format(out_regdate, '%Y')<2000 and date_format(in_regdate, '%Y')>2000 and s.IsReturnWholesaler = 0 and s.curr_warehouse_id='" + warehouseID + "' and p_code='" + p_code + "' order by s.id asc");
        }
        /// <summary>
        /// 变更产品数量
        /// </summary>
        /// <param name="p_code"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool ChangeQuantity(qstoreEntities DB, string p_code)
        {
            return ChangeQuantity(DB, p_code, null);
        }

        /// <summary>
        /// 纪录出库时间, 并删除库存
        /// </summary>
        /// <param name="SerialNO"></param>
        /// <returns></returns>
        public bool OutStore(qstoreEntities DB, string SerialNO, DateTime date)
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
                ChangeQuantity(DB, query.p_code);
                return true;
            }
            return false;
        }
        private void GenerateNew(qstoreEntities context, string userName)
        {
            var count = 0;
            var inrecord = new tb_in_invoice
            {
                regdate = DateTime.Now,
                note = "云仓库存同步，自动添加。操作者：" + userName,
                staff = userName,
                input_regdate = DateTime.Now,
                curr_warehouse_id = Config.YunWarehouseId
            };
            context.tb_in_invoice.Add(inrecord);
            context.SaveChanges();
            foreach (var item in MatchItems.Where(me => me.LocalItem == null))
            {
                count++;
                var pmNew = new tb_product();
                pmNew.p_name = item.YunProdName.Trim();
                pmNew.p_code = item.YunCode.Trim();
                pmNew.p_taobao_url = string.Empty;
                pmNew.p_cost = 0M;
                pmNew.p_price = 0m;
                pmNew.p_quantity = 0;
                pmNew.WholesalerCode = string.Empty;
                pmNew.WholesalerUrl = string.Empty;
                pmNew.WarnQty = 5;
                pmNew.brand = string.Empty;
                pmNew.yun_code = item.YunCode.Trim();
                pmNew.showit = true;
                pmNew.regdate = DateTime.Now;
                context.tb_product.Add(pmNew);
                context.SaveChanges();
                var inProd = new tb_in_invoice_product
                {
                    cost = 0M,
                    in_invoice_code = inrecord.invoice_code,
                    in_invoice_id = inrecord.id,
                    p_code = pmNew.p_code,
                    p_id = pmNew.id,
                    quantity = 0,
                    regdate = DateTime.Now
                };
                context.tb_in_invoice_product.Add(inProd);
                context.SaveChanges();
            }

        }
        public class Item
        {
            public string Code { get; set; }

            public int Qty { get; set; }
        }
        public class MatchItem
        {
            public string YunCode { get; set; }


            public int YunQty { get; set; }

            public string YunProdName { get; set; }

            public Item LocalItem { get; set; }

        }
    }
}