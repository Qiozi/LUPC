using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunStore.Toolkits
{
    /// <summary>
    /// Excel帮助类
    /// </summary>
    public static class ExcelHelper
    {
        public static MemoryStream DataSetToExcel(string fileName, DataSet ds)
        {
            System.IO.MemoryStream stream = new MemoryStream();
            IWorkbook workbook = null;

            //文件名为空时则系统自动分配
            if (string.IsNullOrEmpty(fileName))
            {
                Random ro = new Random();
                string stro = ro.Next(100, 100000000).ToString();//产生一个随机数用于新命名
                fileName = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + stro;
                fileName = fileName + ".xlsx";
            }
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();
            else
            {
                throw new Exception("文件拓展名只支持xlsx和xls两种，请联系信息部修改导出的文件名");
            }
            if (workbook == null)
            {
                throw new Exception("创建Excel工作簿失败，请重新尝试");
            }
            ICellStyle cellstyle = workbook.CreateCellStyle();
            //设置单元格的样式：水平对齐居中
            cellstyle.Alignment = HorizontalAlignment.Center;
            ICellStyle numCellstyle = workbook.CreateCellStyle();
            numCellstyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
            int sheetNum = 1;
            foreach (DataTable dt in ds.Tables)
            {
                ISheet sheet = null;
                string sheetName = dt.TableName;
                if (dt.TableName.Length > 31)
                {
                    sheetName = dt.TableName.Substring(0, 31);
                    if (workbook.GetSheet(sheetName) != null)
                    {
                        sheetName = sheetName.Substring(0, sheetName.Length - 1) + sheetNum.ToString();
                        sheetNum++;
                    }
                }
                sheet = workbook.CreateSheet(sheetName);
                //合并单元格CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dt.Columns.Count - 1));
                IRow rowtitle = sheet.CreateRow(0);
                ICell cell = rowtitle.CreateCell(0);
                cell.SetCellValue(dt.Rows[0]["title"].ToString());
                cell.CellStyle = cellstyle;
                int count = 3;
                IRow row = sheet.CreateRow(count++);
                for (int j = 0; j < dt.Columns.Count - 1; ++j)
                {
                    row.CreateCell(j).SetCellValue(dt.Columns[j].ColumnName);

                }
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    row = sheet.CreateRow(count);
                    for (int j = 0; j < dt.Columns.Count - 1; ++j)
                    {
                        if (RegexHelper.IsDecimal(dt.Rows[i][j].ToString()))
                        {
                            row.CreateCell(j).SetCellValue(ConvertHelper.CustomConvert<double>(dt.Rows[i][j]));
                        }
                        else
                        {
                            row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                    ++count;
                }
            }
            workbook.Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public static MemoryStream DataSetToExcelReport(string fileName, DataSet ds, DataSet ds2)
        {
            System.IO.MemoryStream stream = new MemoryStream();
            IWorkbook workbook = null;

            //文件名为空时则系统自动分配
            if (string.IsNullOrEmpty(fileName))
            {
                Random ro = new Random();
                string stro = ro.Next(100, 100000000).ToString();//产生一个随机数用于新命名
                fileName = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + stro;
                fileName = fileName + ".xlsx";
            }
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();
            else
            {
                throw new Exception("文件拓展名只支持xlsx和xls两种，请联系信息部修改导出的文件名");
            }
            if (workbook == null)
            {
                throw new Exception("创建Excel工作簿失败，请重新尝试");
            }




            foreach (DataTable dt in ds.Tables)
            {
                ISheet sheet = null;
                string sheetName = dt.TableName;
                if (dt.Rows.Count > 0)
                {
                    sheetName = dt.Rows[0][0].ToString();
                }
                else
                {
                    continue;
                }

                sheet = workbook.CreateSheet(sheetName);

                int count = 0;
                IRow row = sheet.CreateRow(0);
                for (int j = 0; j < dt.Columns.Count; ++j)
                {
                    row.CreateCell(j).SetCellValue(dt.Columns[j].ColumnName);
                }
                count = 1;


                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    IRow row2 = sheet.CreateRow(count);
                    for (int j = 0; j < dt.Columns.Count; ++j)
                    {
                        row2.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                    ++count;
                }

            }

            foreach (DataTable dt in ds2.Tables)
            {
                ISheet sheet = null;
                string sheetName = dt.TableName;
                if (dt.Rows.Count > 0)
                {
                    sheetName = dt.Rows[0][0].ToString() + "(自采)";
                }
                else
                {
                    continue;
                }

                sheet = workbook.CreateSheet(sheetName);

                int count = 0;
                IRow row = sheet.CreateRow(0);
                for (int j = 0; j < dt.Columns.Count; ++j)
                {
                    row.CreateCell(j).SetCellValue(dt.Columns[j].ColumnName);
                }
                count = 1;


                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    IRow row2 = sheet.CreateRow(count);
                    for (int j = 0; j < dt.Columns.Count; ++j)
                    {
                        row2.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                    ++count;
                }

            }


            workbook.Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="fileName">文件名(没有则随机生成文件名)</param>
        /// <param name="data">要导入的数据</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="errMsg">错误消息</param>
        /// <returns>文件流</returns>
        public static MemoryStream DataTableToExcel(string fileName, DataTable data, string sheetName, bool isColumnWritten, out string errMsg)
        {
            errMsg = string.Empty;
            sheetName = sheetName.Replace("/", "");
            System.IO.MemoryStream stream = new MemoryStream();
            IWorkbook workbook = null;
            ISheet sheet = null;
            try
            {
                //文件名为空时则系统自动分配
                if (string.IsNullOrEmpty(fileName))
                {
                    Random ro = new Random();
                    string stro = ro.Next(100, 100000000).ToString();//产生一个随机数用于新命名
                    fileName = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + stro;
                    fileName = fileName + ".xlsx";
                }
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook();
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook();
                else
                {
                    errMsg = "文件拓展名只支持xlsx和xls两种，请联系信息部修改导出的文件名";
                    return stream;
                }
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    errMsg = "创建Excel工作簿失败，请重新尝试";
                    return stream;
                }
                int count = 0;
                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (int j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }

                for (int i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (int j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(stream);
                stream.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                errMsg = "导出Excel失败：" + ex.Message.ToString();
                return null;
            }
            return stream;
        }

        /// <summary>
        /// 图片导出到excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <param name="sheetName"></param>
        /// <param name="fileUploadHostUrl"></param>
        /// <param name="isColumnWritten"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static MemoryStream ExportLYReportToExcel(string fileName, DataTable data, string sheetName, bool isColumnWritten, Guid token, out string errMsg)
        {
            errMsg = string.Empty;
            sheetName = sheetName.Replace("/", "");
            System.IO.MemoryStream stream = new MemoryStream();
            IWorkbook workbook = null;
            ISheet sheet = null;
            try
            {
                //文件名为空时则系统自动分配
                if (string.IsNullOrEmpty(fileName))
                {
                    Random ro = new Random();
                    string stro = ro.Next(100, 100000000).ToString();//产生一个随机数用于新命名
                    fileName = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + stro;
                    fileName = fileName + ".xlsx";
                }
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook();
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook();
                else
                {
                    errMsg = "文件拓展名只支持xlsx和xls两种，请联系信息部修改导出的文件名";
                    return stream;
                }
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    errMsg = "创建Excel工作簿失败，请重新尝试";
                    return stream;
                }
                int count = 0;
                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (int j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }

                for (int i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    int columnCount = data.Columns.Count;
                    for (int j = 0; j < columnCount; ++j)
                    {
                        if ((j + 1) == columnCount)
                        {
                            HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                            if (!string.IsNullOrEmpty(data.Rows[i][j].ToString()))
                            {
                                InsertSignImage(workbook, patriarch, sheet, i + 1, j, (byte[])data.Rows[i][j], token);
                            }

                        }
                        else
                        {
                            row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                        }

                    }
                    ++count;
                }

                //每一张表只能有一个HSSFPatriarch对象，如果把它的创建放到了setPic方法中，那么一行只会出现一张图片，最后的图片会消掉之前的图片。也不能放到for循环里。
                //HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                //setPic(workbook, patriarch, sheet, 10, 11, fileUploadHostUrl, "201903060090554125673.png"); //201903060090554125673.png  060090554125673              
                workbook.Write(stream);
                stream.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                errMsg = "导出Excel失败：" + ex.Message.ToString();
                return null;
            }
            return stream;
        }

        //private static void setPic(IWorkbook workbook, HSSFPatriarch patriarch, ISheet sheet, int rowline, int col, string fileUploadHostUrl, string newfileName,Guid token)
        //{
        //   // 换一种方式下载测试
        //    using (WebClient client = new WebClient())
        //    {
        //        var res = client.DownloadData(newfileName + "&fileType=png&token=" + GuidExtension.GuidToBase64(token));
        //        client.Dispose();
        //        byte[] bytes = res;

        //        int pictureIdx = workbook.AddPicture(bytes, PictureType.PNG);//JPEG
        //                                                                     // 插图片的位置  HSSFClientAnchor（dx1,dy1,dx2,dy2,col1,row1,col2,row2)
        //        HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, col, rowline, col + 1, rowline + 1);
        //        //把图片插到相应的位置
        //        HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
        //    }

        //}

        private static void InsertSignImage(IWorkbook workbook, HSSFPatriarch patriarch, ISheet sheet, int rowline, int col, byte[] bytes, Guid token)
        {
            int pictureIdx = workbook.AddPicture(bytes, PictureType.PNG);//JPEG
                                                                         // 插图片的位置  HSSFClientAnchor（dx1,dy1,dx2,dy2,col1,row1,col2,row2)
            HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, col, rowline, col + 1, rowline + 1);
            //把图片插到相应的位置
            HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);


        }

        /// <summary>
        /// 将EXCEL文件导到DataSet中
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="sheetNames">需要取出数据的工作表名称，多个用;隔开</param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string fileName)
        {
            DataSet ds = new DataSet();
            IWorkbook workbook;
            string fileExt = Path.GetExtension(fileName).ToLower();
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx" || fileExt == ".xls")
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else
                {
                    throw new Exception("只可以选择Excel文件！");
                }
                int sheetNum = workbook.NumberOfSheets;
                for (int index = 0; index < sheetNum; index++)
                {
                    ISheet sheet = workbook.GetSheetAt(index);
                    DataTable dt = new DataTable(sheet.SheetName);
                    if (sheet.LastRowNum == 0)
                    {
                        continue;
                    }
                    //表头  
                    IRow header = sheet.GetRow(sheet.FirstRowNum);
                    List<int> columns = new List<int>();
                    for (int i = 0; i < header.LastCellNum; i++)
                    {
                        object obj = header.GetCell(i);
                        if (obj == null || obj.ToString() == string.Empty)
                        {
                            dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        }
                        else
                            dt.Columns.Add(new DataColumn(obj.ToString()));
                    }
                    //数据  
                    for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                    {
                        DataRow dr = dt.NewRow();
                        bool hasValue = false;
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            dr[j] = sheet.GetRow(i).GetCell(j);
                            if (dr[j] != null && dr[j].ToString() != string.Empty)
                            {
                                hasValue = true;
                            }
                        }
                        if (hasValue)
                        {
                            dt.Rows.Add(dr);
                        }
                    }
                    ds.Tables.Add(dt);
                }
            }
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static DataSet ExcelSteamToDataSet(Stream fs, bool useOldVersionOfExcel = false)
        {
            DataSet res = new DataSet();
            IWorkbook workbook;// = new XSSFWorkbook(fs);
            if (useOldVersionOfExcel)
            {
                workbook = new HSSFWorkbook(fs);
            }
            else
            {
                workbook = new XSSFWorkbook(fs);
            }
            int sheetNum = workbook.NumberOfSheets;
            for (int index = 0; index < sheetNum; index++)
            {
                ISheet sheet = workbook.GetSheetAt(index);
                DataTable dt = new DataTable(sheet.SheetName);
                if (sheet.LastRowNum == 0)
                {
                    continue;
                }
                //表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = header.GetCell(i);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                }
                //数据  
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        try
                        {
                            if (sheet.GetRow(i) == null)
                            {
                                hasValue = false;
                                break;
                            }

                            //判断当前单元格的内容是否为日期格式的数据，是则进行处理
                            ICell cell = sheet.GetRow(i).GetCell(j);
                            if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                            {
                                dr[j] = cell.DateCellValue.ToString();
                            }
                            else
                            {
                                dr[j] = cell.ToString();
                            }
                            //dr[j] = sheet.GetRow(i).GetCell(j);
                            if (dr[j] != null && dr[j].ToString() != string.Empty)
                            {
                                hasValue = true;
                            }
                        }
                        catch { }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
                res.Tables.Add(dt);
            }
            return res;
        }

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="fileName">文件名(没有则随机生成文件名)</param>
        /// <param name="modelName">模块名称</param>
        /// <param name="data">要导入的数据</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="errMsg">错误消息</param>
        /// <returns>文件流</returns>
        public static MemoryStream DataTableToExcel(string fileDirectory, string fileName, string modelName, DataTable data, string sheetName, bool isColumnWritten, out string errMsg)
        {
            errMsg = string.Empty;
            System.IO.MemoryStream stream = new MemoryStream();
            IWorkbook workbook = null;
            ISheet sheet = null;
            try
            {
                //文件名为空时则系统自动分配
                if (string.IsNullOrEmpty(fileName))
                {
                    Random ro = new Random();
                    string stro = ro.Next(100, 100000000).ToString();//产生一个随机数用于新命名
                    fileName = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + stro;
                    fileName = fileName + ".xlsx";
                }
                if (!System.IO.Directory.Exists(fileDirectory))
                {
                    //这个是根据路径新建一个目录  
                    System.IO.Directory.CreateDirectory(fileDirectory);
                }
                fileName = fileDirectory + fileName;
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook();
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook();
                else
                {
                    errMsg = "文件拓展名只支持xlsx和xls两种，请联系信息部修改导出的文件名";
                    return stream;
                }
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    errMsg = "创建Excel工作簿失败，请重新尝试";
                    return stream;
                }
                int count = 0;
                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (int j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }

                for (int i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (int j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(stream);
                stream.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                errMsg = "导出Excel失败：" + ex.Message.ToString();
                return null;
            }
            return stream;
        }

        public static string GetExcelConnectionString(string file)
        {
            string connectionString = string.Empty;
            string fileExtension = file.Substring(file.LastIndexOf(".") + 1);
            switch (fileExtension)
            {
                case "xls":
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file + ";Extended Properties='Excel 8.0 Xml;HDR=YES;IMEX=1;'";
                    break;
                case "xlsx":
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
                    break;
            }
            return connectionString;
        }

        /// <summary>
        /// 输excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="SheetName"></param>
        /// <param name="filename"></param>
        public static void ToExcel(DataTable dt, string SheetName, string filename)
        {
            //FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Write);
            HSSFWorkbook workbook = new HSSFWorkbook();

            var sheet = workbook.CreateSheet(SheetName);

            IRow row = sheet.CreateRow(0);
            foreach (DataColumn column in dt.Columns)
            {
                row.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //保存   
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
            }
        }

    }
}
