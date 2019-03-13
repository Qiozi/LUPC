using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace KKWStore.Helper
{
    public class NPOIExcel
    {
        /// <summary>
        /// 导入excel
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string filename, int headRowIndex = 0)
        {
            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(sr);

            //获取excel的第一个sheet
            NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0);

            DataTable table = new DataTable();
            //获取sheet的首行
            NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(headRowIndex);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue.Trim());
                table.Columns.Add(column);
            }
            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;

            for (int i = (headRowIndex + 1); i <= sheet.LastRowNum; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        if (row.GetCell(j).CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(row.GetCell(j)))
                        {
                            dataRow[j] = row.GetCell(j).DateCellValue.ToString();
                        }
                        else
                            dataRow[j] = row.GetCell(j).ToString();
                    }
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

            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
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
