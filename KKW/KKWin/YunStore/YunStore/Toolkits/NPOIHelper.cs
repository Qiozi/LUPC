using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunStore.Toolkits
{
    public class NPOIHelper
    {
        public class Data
        {
            public List<string> Columns { get; set; }

            public List<List<string>> RowValues { get; set; }
        }

        public static MemoryStream RenderToExcel(Data data, string sheetName = "sheet1")
        {
            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < data.Columns.Count; i++)
            {
                headerRow.CreateCell(i).SetCellValue(data.Columns[i]);
            }
            int rowIndex = 1;
            for (int i = 0; i < data.RowValues.Count; i++)
            {
                var item = data.RowValues[i];
                IRow newRow = sheet.CreateRow(i + rowIndex);
                for (int j = 0; j < item.Count; j++)
                {
                    var cellValue = item[j];
                    newRow.CreateCell(j).SetCellValue(cellValue);
                }
            }
            workbook.Write(ms);

            ms.Flush();
            ms.Position = 0;
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
