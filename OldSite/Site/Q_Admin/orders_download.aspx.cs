using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;

public partial class Q_Admin_OrderDownload : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(ReqOrderCode))
            {
                string[] columns = new string[] { "Order", "Date", "SystemSKU", "SKU", "Title", "MFP#", "Cost", "D2A", "ASI", "DH", "EPR", "Syn" };

                IWorkbook hssfworkbook = new HSSFWorkbook();
                ISheet sheet = hssfworkbook.CreateSheet("Sheet1");
                hssfworkbook.CreateSheet("Sheet2");
                hssfworkbook.CreateSheet("Sheet3");
                sheet.SetColumnWidth(0, 10 * 256); // 第二列宽度
                sheet.SetColumnWidth(1, 10 * 256); // 
                sheet.SetColumnWidth(2, 15 * 256);
                sheet.SetColumnWidth(3, 15 * 256);
                sheet.SetColumnWidth(4, 80 * 256);
                sheet.SetColumnWidth(5, 15 * 256);
                sheet.SetColumnWidth(6, 15 * 256);
                sheet.SetColumnWidth(7, 15 * 256);
                sheet.SetColumnWidth(8, 15 * 256);
                sheet.SetColumnWidth(9, 15 * 256);
                sheet.SetColumnWidth(10, 15 * 256);
                sheet.SetColumnWidth(11, 15 * 256);

                //IRow row = sheet.CreateRow(0);
                //row.Height = 30 * 20;
                //ICell cell = row.CreateCell(0);
                //cell.SetCellValue("Order");
                //ICellStyle style = hssfworkbook.CreateCellStyle();


                // 表格 头部

                int index = 0;

                IRow row1 = sheet.CreateRow(index);
                for (int i = 0; i < columns.Length; i++)
                {
                    ICell t1 = row1.CreateCell(i);
                    t1.SetCellValue(columns[i]);

                    ICellStyle style3 = hssfworkbook.CreateCellStyle();
                    IFont font2 = hssfworkbook.CreateFont();
                    font2.Boldweight = (short)FontBoldWeight.BOLD;
                    style3.SetFont(font2);

                    style3.Alignment = HorizontalAlignment.CENTER; // 1 left, 2 center.
                    t1.CellStyle = style3;
                }
                index++;


                string[] codes = ReqOrderCode.Split(new char[] { '|' });
                for (int i = 0; i < codes.Length; i++)
                {
                    var code = codes[i];
                    int cid;
                    int.TryParse(code, out cid);
                    if (cid > 0)
                    {
                        var pdt = Config.ExecuteDataTable("select op.product_serial_no,date_format(oh.create_datetime, '%Y-%m-%d') create_datetime from tb_order_product op inner join tb_order_helper oh on oh.order_code=op.order_code where oh.order_code ='" + cid + "'");
                        foreach (DataRow dr in pdt.Rows)
                        {
                            if (dr[0].ToString().Length == 8)
                            {
                                var sysParts = Config.ExecuteDataTable("Select product_serial_no from tb_order_product_sys_detail where sys_tmp_code='" + dr[0].ToString() + "' and product_serial_no<>16684 and product_serial_no<>6775 ");
                                foreach (DataRow pdr in sysParts.Rows)
                                {
                                    GerenatePartRow( cid, int.Parse(pdr[0].ToString()), index, sheet, hssfworkbook, dr["create_datetime"].ToString(), int.Parse(dr[0].ToString()));
                                    index++;
                                }
                            }
                            else
                            {
                                GerenatePartRow(cid, int.Parse(dr[0].ToString()), index, sheet, hssfworkbook, dr["create_datetime"].ToString(), 0);
                                index++;
                            }
                        }
                    }
                }

                //IRow row22 = sheet.CreateRow(index);
                //for (int i = 0; i < ColumnNames2.Count; i++)
                //{
                //    ICell t1 = row22.CreateCell(i);
                //    t1.SetCellValue(ColumnNames2[i]);

                //    ICellStyle style3 = hssfworkbook.CreateCellStyle();
                //    IFont font2 = hssfworkbook.CreateFont();
                //    font2.Boldweight = (short)FontBoldWeight.BOLD;
                //    style3.SetFont(font2);

                //    style3.Alignment = HorizontalAlignment.CENTER; // 1 left, 2 center.
                //    t1.CellStyle = style3;
                //}



                //index++;

                index++;
                //保存   
                using (MemoryStream ms = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(Server.MapPath("/export/order.xls"), FileMode.Create, FileAccess.Write))
                    {
                        hssfworkbook.Write(fs);
                    }
                }
                string fileName = "orders.xls";//客户端保存的文件名
                string filePath = Server.MapPath("/export/order.xls");//路径

                FileInfo fileInfo = new FileInfo(filePath);
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.AddHeader("Content-Transfer-Encoding", "binary");
                Response.ContentType = "application/octet-stream";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.WriteFile(fileInfo.FullName);
                Response.Flush();

                File.Delete(filePath);
                Response.End();
            }

        }
    }

    void GerenatePartRow(int orderCode, int sku, int index, ISheet sheet, IWorkbook hssfworkbook, string regDate, int sysSku)
    {
        var pm = ProductModel.GetProductModel(DBContext, sku);
        var asi = GetLtdCost((int)Ltd.wholesaler_asi, sku);
        var d2a = GetLtdCost((int)Ltd.wholesaler_d2a, sku);
        var dh = GetLtdCost((int)Ltd.wholesaler_dandh, sku);
        var epr = GetLtdCost((int)Ltd.wholesaler_EPROM, sku);
        var syn = GetLtdCost((int)Ltd.wholesaler_Synnex, sku);
        IRow row22 = sheet.CreateRow(index);
        CreatedRow(row22
            , hssfworkbook
            , orderCode         
            , regDate
            , pm.manufacturer_part_number
            , sysSku
            , sku
            , string.IsNullOrEmpty(pm.product_ebay_name) ? pm.product_name : pm.product_ebay_name
            , (double)pm.product_current_cost
            , asi
            , d2a
            , dh
            , epr
            , syn);
    }

    double GetLtdCost(int ltdId, int sku)
    {
        var dt = Config.ExecuteDataTable("Select other_inc_price from tb_other_inc_part_info where other_inc_store_sum>0 and luc_sku='" + sku + "' and other_inc_id='" + ltdId + "' order by id desc limit 1");
        if (dt.Rows.Count == 1)
        {
            return double.Parse(dt.Rows[0]["other_inc_price"].ToString());
        }
        return 0;
    }

    public void CreatedRow(IRow row, IWorkbook hssfworkbook, int order, string date, string mfp, int sysSku, int sku, string title, double cost
        , double asi, double d2a, double dh, double epr, double syn)
    {
        if (order > 0)
        {
            ICell cell = row.CreateCell(0);
            cell.SetCellValue(order);
        }
        ICell cell1 = row.CreateCell(1);
        cell1.SetCellValue(date);

        if (sysSku > 0)
        {
            ICell cell2 = row.CreateCell(2);
            cell2.SetCellValue(sysSku);
        }
        if (sku > 0)
        {
            ICell cell3 = row.CreateCell(3);
            cell3.SetCellValue(sku);
        }
        ICell cell4 = row.CreateCell(4);
        cell4.SetCellValue(title);
        ICell cell5 = row.CreateCell(5);
        cell5.SetCellValue(mfp);
        if (cost > 0d)
        {


            ICell cell6 = row.CreateCell(6);
            cell6.SetCellValue(cost);
        }
        if (d2a > 0d)
        {
            ICell cell7 = row.CreateCell(7);
            cell7.SetCellValue(d2a);
        }
        if (asi > 0d)
        {
            ICell cell8 = row.CreateCell(8);
            cell8.SetCellValue(asi);
        }
        if (dh > 0d)
        {
            ICell cell9 = row.CreateCell(9);
            cell9.SetCellValue(dh);

        }
        if (epr > 0d)
        {

            ICell cell10 = row.CreateCell(10);
            cell10.SetCellValue(epr);
        }
        if (syn > 0d)
        {

            ICell cell11 = row.CreateCell(11);
            cell11.SetCellValue(syn);
        }

        //ICellStyle style3 = hssfworkbook.CreateCellStyle();
        //IFont font2 = hssfworkbook.CreateFont();
        //font2.Boldweight = (short)FontBoldWeight.BOLD;
        //style3.SetFont(font2);

        //style3.Alignment = HorizontalAlignment.CENTER; // 1 left, 2 center.
        //t1.CellStyle = style3;
    }

    //public MemoryStream Download()
    //{
    //    InitData(context, userInfo);

    //    var ms = new MemoryStream();

    //    IWorkbook workbook = new HSSFWorkbook();

    //    ISheet sheet = workbook.CreateSheet();

    //    IRow headerRow = sheet.CreateRow(0);

    //    // handling header.
    //    headerRow.CreateCell(0).SetCellValue("序号");
    //    sheet.SetColumnWidth(0, 20 * 256);
    //    headerRow.CreateCell(1).SetCellValue("医院名称");
    //    sheet.SetColumnWidth(1, 40 * 256);
    //    headerRow.CreateCell(2).SetCellValue("处方数");
    //    sheet.SetColumnWidth(2, 10 * 256);

    //    // handling value.
    //    int rowIndex = 1;

    //    foreach (var item in this.Items)
    //    {
    //        IRow dataRow = sheet.CreateRow(rowIndex);
    //        dataRow.CreateCell(0).SetCellValue(item.Index);
    //        dataRow.CreateCell(1).SetCellValue(item.HospName);
    //        dataRow.CreateCell(2).SetCellValue(item.RecipeCount);
    //        rowIndex++;
    //    }

    //    workbook.Write(ms);
    //    ms.Flush();
    //    ms.Position = 0;

    //    return ms;
    //}
    string ReqOrderCode
    {
        get { return Util.GetStringSafeFromQueryString(Page, "codes"); }
    }
}