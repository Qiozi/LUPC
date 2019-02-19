using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NPOI.HSSF.UserModel;
using System.IO;


namespace Chinese2Pinyin
{
 
    public class HSSFExcel
    {
        public static DataTable ToDataTable(string filename)
        {
            return ToDataTable(filename, 0);
        }

        public static DataTable ToDataTable(string filename, int sheetIndex)
        {
            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(sr);

            //获取excel的第一个sheet
            HSSFSheet sheet = workbook.GetSheetAt(sheetIndex);
           
            DataTable table = new DataTable();
            //获取sheet的首行
            HSSFRow headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue??"");
                table.Columns.Add(column);
            }
            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                if (row == null) continue;
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            workbook = null;
            sheet = null;

            sr.Close();
            sr.Dispose();
            return table;
        }
        /// <summary>
        /// 卓越外语导入使用
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string filename, int sheetIndex,ref string sheetName)
        {
            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(sr);
            sheetName = workbook.GetSheetName(sheetIndex);
            //获取excel的第一个sheet
            HSSFSheet sheet = workbook.GetSheetAt(sheetIndex);
           
            DataTable table = new DataTable();
            //获取sheet的首行
            HSSFRow headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                if (row == null) continue;
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            workbook = null;
            sheet = null;

            sr.Close();
            sr.Dispose();
            return table;
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

            HSSFRow row = sheet.CreateRow(0);
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