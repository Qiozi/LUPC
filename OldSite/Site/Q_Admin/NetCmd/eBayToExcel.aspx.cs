using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;

public partial class Q_Admin_NetCmd_eBayToExcel : PageBase
{
    List<TempPartGroupInfo> _partGroupInfo = new List<TempPartGroupInfo>();
    string FileFullName
    {
        get
        {
            object obj = ViewState["FileFullName"];
            if (obj != null)
                return obj.ToString();
            return "";
        }
        set { ViewState["FileFullName"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("../indexadmin.aspx", true);
            FileFullName = "/export/ebaySys.xls";
     
        }
    }
    

    void ExportExcel(string fileFullname)
    {
        IWorkbook hssfworkbook = new HSSFWorkbook();
        CreateEBaySys(hssfworkbook, true);
        CreateEBaySys(hssfworkbook, false);
        CreatePartGroup(hssfworkbook);

        //保存   
        using (MemoryStream ms = new MemoryStream())
        {
            using (FileStream fs = new FileStream(fileFullname, FileMode.Create, FileAccess.Write))
            {
                hssfworkbook.Write(fs);
            }
        }
    }

    /// <summary>
    /// 存comment , partGroupid
    /// </summary>
    /// <param name="comment"></param>
    /// <param name="partGroupID"></param>
    void SetPartGroupInfo(string comment, int partGroupID, int priority)
    {
        bool exist = false;
        bool existComment = false;
        foreach (var cp in _partGroupInfo)
        {
            if (comment.Trim() == cp.GroupComment)
            {
                existComment = true;

                foreach (var id in cp.PartGroupIds)
                {
                    if (partGroupID == id)
                    {
                        exist = true;
                        break;
                        
                    }
                }
            }
        }
        if (!exist)
        {
            if (existComment)
            {
                for (int i = 0; i < _partGroupInfo.Count; i++)
                {
                    if (comment.Trim() == _partGroupInfo[i].GroupComment)
                    {
                        _partGroupInfo[i].PartGroupIds.Add(partGroupID);
                    }
                }
            }
            else
            {
                _partGroupInfo.Add(new TempPartGroupInfo()
                {
                    GroupComment = comment,
                    PartGroupIds = new List<int>() { partGroupID },
                    Priority = priority
                });
            }
        }
    }

    void CreateEBaySys(IWorkbook hssfworkbook, bool haveSub)
    {
        string sheetname = haveSub ? "eBaySystemDetail" : "ebaySystem";
        ISheet sheet = hssfworkbook.CreateSheet(sheetname);

        DataTable dt = Config.ExecuteDataTable(@"
select es.id, e.BuyItNowPrice,es.category_id
 ,e.SKU cutom_label, es.ebay_system_price
 ,e.ItemID, es.adjustment, e.title
 ,es.is_barebone, es.is_shrink 
 ,es.large_pic_name 
 ,es.logo_filenames 
 ,es.sub_part_quantity 
 ,oeq.quantity sellqty
,es.ebay_fee
,es.shipping_fee
,es.profit
,es.cost
 from tb_ebay_system es 
 inner join tb_ebay_selling e on e.sys_sku=es.id 
 left join tb_order_ebay_quantity oeq on oeq.itemid=e.ItemID

 inner join (select category_id, category_name from tb_product_category_new where parent_category_id='10000' and showit=1 ) cate on cate.category_id=es.category_id
 where es.is_from_ebay=0 and es.showit=1 order by e.sku asc
");

        int index = 0;
        IRow row;
        if (!haveSub)
        {
            row = sheet.CreateRow(index);
            index++;
            row.CreateCell(0).SetCellValue("SKU");
            row.CreateCell(1).SetCellValue("ItemId");
            row.CreateCell(2).SetCellValue("Cost");
            row.CreateCell(3).SetCellValue("eBay Fee");
            row.CreateCell(4).SetCellValue("Base Shipping Fee");
            row.CreateCell(5).SetCellValue("Profit");
            row.CreateCell(6).SetCellValue("BuyItNowPrice");
            row.CreateCell(7).SetCellValue("Cutom_label");
            row.CreateCell(8).SetCellValue("Title");
            row.CreateCell(9).SetCellValue("SellQuantity");
            row.CreateCell(10).SetCellValue("IsBarebone");
            row.CreateCell(11).SetCellValue("Logo Pricture");
        }

        row = sheet.CreateRow(index);
        index++;
        ICell cell = row.CreateCell(0);
        cell.SetCellValue("");

        ICellStyle style1 = hssfworkbook.CreateCellStyle();
        style1.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.RED.index;
        IFont font = hssfworkbook.CreateFont();
        font.FontHeightInPoints = 13;
        style1.SetFont(font);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            
            DataRow dr = dt.Rows[i];  
            row = sheet.CreateRow(index);            
            index++;

           
            cell = row.CreateCell(0);
            cell.CellStyle = style1;
            cell.SetCellValue(int.Parse(dr["ID"].ToString()));
            cell.SetCellType(CellType.NUMERIC);
            cell.CellStyle = style1;

            cell = row.CreateCell(1);
            cell.SetCellValue(dr["ItemID"].ToString());
            cell = row.CreateCell(2);
            cell.SetCellValue(double.Parse(dr["cost"].ToString() ?? "0"));
            cell.SetCellType(CellType.NUMERIC);
            //cell.CellStyle = style1;
            cell = row.CreateCell(3);
            cell.SetCellValue(double.Parse(dr["ebay_fee"].ToString() ?? "0"));
            cell.SetCellType(CellType.NUMERIC);

            cell = row.CreateCell(4);
            cell.SetCellValue(double.Parse(dr["shipping_fee"].ToString() ?? "0"));
            cell.SetCellType(CellType.NUMERIC);
            //cell.CellStyle = style1;

            cell = row.CreateCell(5);
            cell.SetCellValue(double.Parse(dr["profit"].ToString()??"0"));
            cell.SetCellType(CellType.NUMERIC);
            cell= row.CreateCell(6);
            cell.SetCellValue(double.Parse(dr["BuyItNowPrice"].ToString() ?? "0"));
            cell.SetCellType(CellType.NUMERIC);

            row.CreateCell(7).SetCellValue(dr["cutom_label"].ToString());
            row.CreateCell(8).SetCellValue(dr["title"].ToString());
            cell = row.CreateCell(9);
            cell.SetCellValue(dr["sellqty"].ToString());
            cell.SetCellType(CellType.STRING);
            cell = row.CreateCell(10);
            cell.SetCellValue(dr["is_barebone"].ToString());
            cell.SetCellType(CellType.STRING);
            row.CreateCell(11).SetCellValue("http://www.lucomputers.com/ebay/"+ dr["logo_filenames"].ToString() +".jpg");

            // 明细
            DataTable sysDetailDT = Config.ExecuteDataTable(string.Format(@"select c.id, c.comment,category_ids, ps.luc_sku,
p.product_ebay_name, ps.part_quantity, ps.max_quantity
,p.product_current_price-p.product_current_discount sell, p.product_current_cost
,ifnull(ps.part_group_id, -1) part_group_id 
,ps.is_label_of_flash
,ps.is_belong_price
,c.is_mb
,c.is_cpu
,c.priority
, pg.part_group_comment
from tb_ebay_system_part_comment c inner join tb_ebay_system_parts ps on ps.comment_id=c.id
 left join tb_product p on p.product_serial_no=ps.luc_sku 
 inner join tb_part_group pg on pg.part_group_id=ps.part_group_id
where c.showit=1 and ps.system_sku = '{0}' order by c.priority asc", dr["ID"].ToString()));

            sheet.SetColumnWidth(1, 16 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 20 * 256);
            sheet.SetColumnWidth(8, 100 * 256);

            if (!haveSub)
            {
                continue;
            }
                for (int j = 0; j < sysDetailDT.Rows.Count; j++)
                {

                    DataRow sdr = sysDetailDT.Rows[j];
                    SetPartGroupInfo(sdr["comment"].ToString(), int.Parse(sdr["part_group_id"].ToString()), int.Parse(sdr["priority"].ToString()));

                    row = sheet.CreateRow(index);
                    index++;

                    cell = row.CreateCell(0);
                    cell.SetCellValue(int.Parse(dr["ID"].ToString()));
                    cell.SetCellType(CellType.NUMERIC);
                   

                    row.CreateCell(1).SetCellValue("");
                    cell = row.CreateCell(2);
                    cell.SetCellType(CellType.NUMERIC);
                    cell.SetCellValue(int.Parse(sdr["id"].ToString()));

                    row.CreateCell(3).SetCellValue(sdr["comment"].ToString());
                    row.CreateCell(4).SetCellValue("");

                    cell = row.CreateCell(5);
                    cell.SetCellType(CellType.NUMERIC);
                    cell.SetCellValue(int.Parse(sdr["part_group_id"].ToString()));
                    row.CreateCell(6).SetCellValue(sdr["part_group_comment"].ToString());

                    cell = row.CreateCell(7);
                    cell.SetCellType(CellType.NUMERIC);
                    cell.SetCellValue(int.Parse(sdr["luc_sku"].ToString()));
                    cell = row.CreateCell(8);
                    cell.SetCellValue(sdr["product_ebay_name"].ToString());

                    cell = row.CreateCell(9);
                    cell.SetCellValue(double.Parse(sdr["product_current_cost"].ToString()));
                    cell.SetCellType(CellType.NUMERIC);
                }
            

            row = sheet.CreateRow(index);
            index++;
        }
    }

    void CreatePartGroup(IWorkbook hssfworkbook)
    {
        _partGroupInfo.Sort((x,y)=>x.Priority - y.Priority);
   
        foreach(var g in _partGroupInfo)
        {
            ISheet sheet = hssfworkbook.CreateSheet(g.GroupComment.Replace("/", "&"));

            int index = 0;

            IRow row = sheet.CreateRow(index);
            index++;
            ICell cell = row.CreateCell(0);
            cell.SetCellValue("");

            ICellStyle style1 = hssfworkbook.CreateCellStyle();
            style1.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.RED.index;
            IFont font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 12;
            style1.SetFont(font);


            foreach(var id in g.PartGroupIds)
            {

                var pgm = PartGroupModel.GetPartGroupModel(DBContext, id);

                row = sheet.CreateRow(index);
                index++;


                cell = row.CreateCell(0);
                cell.CellStyle = style1;
                cell.SetCellValue(pgm.part_group_id);
                cell.SetCellType(CellType.NUMERIC);
                cell.CellStyle = style1;

                row.CreateCell(1).SetCellValue(pgm.part_group_comment);
                cell = row.CreateCell(2);
                cell.SetCellValue("");
                cell.SetCellType(CellType.STRING);
                cell.CellStyle = style1;
                row.CreateCell(3).SetCellValue("");

              
                // 明细
               DataTable detailDT = Config.ExecuteDataTable(string.Format(@"Select p.product_current_cost, p.product_serial_no, 
p.ltd_stock, 
 case when length(p.product_ebay_name)>4 then p.product_ebay_name 
   when length(p.product_name_long_en )>4 then p.product_name_long_en 
  when length(p.product_name)>4 then p.product_name 
	   else p.product_short_name end as product_name, p.product_current_price price, pd.part_group_id 
from tb_part_group_detail pd inner join tb_product p on p.product_serial_no=pd.product_serial_no 
where part_group_id='{0}' and 
p.split_line=0 and p.tag=1  
order by p.product_ebay_name asc, p.product_current_cost asc", id));

                 //sheet.SetColumnWidth( 5, 15 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(5, 100 * 256);
               for (int j = 0; j < detailDT.Rows.Count; j++)
                {
                    DataRow sdr = detailDT.Rows[j];

                    row = sheet.CreateRow(index);
                    index++;

                    row.CreateCell(0).SetCellValue("");
                    row.CreateCell(1).SetCellValue("");
                    cell = row.CreateCell(2);
                    cell.SetCellType(CellType.NUMERIC);
                    cell.SetCellValue(int.Parse(sdr["product_serial_no"].ToString()));

                    cell = row.CreateCell(3);
                    cell.SetCellValue(double.Parse( sdr["product_current_cost"].ToString()));
                    cell.SetCellType(CellType.NUMERIC);
                    row.CreateCell(4).SetCellValue("");

                    cell = row.CreateCell(5);
                    cell.SetCellValue(sdr["product_name"].ToString());
                    row.CreateCell(6).SetCellValue("");

                    //cell = row.CreateCell(7);
                    //cell.SetCellType(CellType.NUMERIC);
                    //cell.SetCellValue(int.Parse(sdr["luc_sku"].ToString()));
                    //cell = row.CreateCell(8);
                    //cell.SetCellValue(sdr["product_ebay_name"].ToString());

                    //cell = row.CreateCell(9);
                    //cell.SetCellValue(double.Parse(sdr["product_current_cost"].ToString()));
                    //cell.SetCellType(CellType.NUMERIC);
                }

            }
        }
       

       
    }

    void CreateeBayList(IWorkbook hssfworkbook)
    {

    }


    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (File.Exists(Server.MapPath(FileFullName)))
        {
            File.Delete(Server.MapPath(FileFullName));
            Label1.Text = "File deleted";
        }
    }
    protected void Button_generate_Click(object sender, EventArgs e)
    {
        ExportExcel(Server.MapPath(FileFullName));
        Label1.Text = "";
        Response.Write("<script> window.location.href='" + FileFullName + "';</script>");
    }
}
