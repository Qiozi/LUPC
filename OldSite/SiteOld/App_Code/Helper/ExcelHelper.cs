using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Globalization;

/// <summary>
/// Summary description for ExcelHelper
/// </summary>
public class ExcelHelper
{   
    string _fileName;
    DataTable _dataSource;
    string[] _titles = null;
    string[] _fields = null;
    int _maxRecords = 1000;



    #region Properties

    /// <summary>
    /// 限制输出到 Excel 的最大记录数。超出则抛出异常
    /// </summary>
    public int MaxRecords
    {
        set { _maxRecords = value; }
        get { return _maxRecords; }
    }

    /// <summary>
    /// 输出到浏览器的 Excel 文件名
    /// </summary>
    public string FileName
    {
        set { _fileName = value; }
        get { return _fileName; }
    }

    #endregion

    #region .ctor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="titles">要输出到 Excel 的列标题的数组</param>
    /// <param name="fields">要输出到 Excel 的字段名称数组</param>
    /// <param name="dataSource">数据源</param>
    public ExcelHelper(string[] titles, string[] fields, DataTable dataSource)
        : this(titles, dataSource)
    {
        if (fields == null || fields.Length == 0)
            throw new ArgumentNullException("fields");

        if (titles.Length != fields.Length)
            throw new ArgumentException("titles.Length != fields.Length", "fields");

        _fields = fields;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="titles">要输出到 Excel 的列标题的数组</param>
    /// <param name="dataSource">数据源</param>
    public ExcelHelper(string[] titles, DataTable dataSource)
        : this(dataSource)
    {
        if (titles == null || titles.Length == 0)
            throw new ArgumentNullException("titles");
        //if (titles.Length != dataSource.Columns.Count)
        //	throw new ArgumentException("titles.Length != dataSource.Columns.Count", "dataSource");

        _titles = titles;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dataSource">数据源</param>
    public ExcelHelper(DataTable dataSource)
    {
        if (dataSource == null)
            throw new ArgumentNullException("dataSource");
        // maybe more checks needed here... (IEnumerable, IList, IListSource, ...) ???
        // 很难判断，先简单的使用 DataTable

        _dataSource = dataSource;
    }

    public ExcelHelper() { }

    #endregion

    #region public Methods

    /// <summary>
    /// 导出到 Excel 并提示下载
    /// </summary>
    /// <param name="dg">DataGrid</param>
    public void Export(DataGrid dg)
    {
        if (dg == null)
            throw new ArgumentNullException("dg");
        if (dg.AllowPaging || dg.PageCount > 1)
            throw new ArgumentException("paged DataGrid can't be exported.", "dg");

        // 添加标题样式
        dg.HeaderStyle.Font.Bold = true;
        dg.HeaderStyle.BackColor = System.Drawing.Color.LightGray;

        RenderExcel(dg);
    }
    
    /// <summary>
    /// 导出到 Excel 并提示下载
    /// </summary>
    /// <param name="xgrid">ASPxGrid</param>
    //public void Export(DevExpress.Web.ASPxGrid.ASPxGrid xgrid)
    //{
    //    if (xgrid == null)
    //        throw new ArgumentNullException("xgrid");
    //    if (xgrid.PageCount > 1)
    //        throw new ArgumentException("paged xgird not can't be exported.", "xgrid");

    //    // 添加标题样式
    //    xgrid.HeaderStyle.Font.Bold = true;
    //    xgrid.HeaderStyle.BackColor = System.Drawing.Color.LightGray;

    //    RenderExcel(xgrid);
    //}

    /// <summary>
    /// 导出到 Excel 并提示下载
    /// </summary>
    public void Export()
    {
        if (_dataSource == null)
            throw new Exception("数据源尚未初始化");

        if (_fields == null && _titles != null && _titles.Length != _dataSource.Columns.Count)
            throw new Exception("_titles.Length != _dataSource.Columns.Count");

        if (_dataSource.Rows.Count > _maxRecords)
            throw new Exception("导出数据条数超过限制。请设置 MaxRecords 属性以定义导出的最多记录数。");

        //DataGrid dg = new DataGrid();
        //dg.DataSource = _dataSource;

        GridView dg = new GridView();
        dg.DataSource = _dataSource;

        if (_titles == null)
        {
            dg.AutoGenerateColumns = true;
            
        }
        else
        {
            dg.AutoGenerateColumns = false;
            int cnt = _titles.Length;

            //System.Web.UI.WebControls.BoundColumn col;
            System.Web.UI.WebControls.DataControlField col ;
            

            if (_fields == null)
            {
                for (int i = 0; i < cnt; i++)
                {
                    col = new BoundField();
                    ((BoundField)col).HeaderText = _titles[i];
                    ((BoundField)col).DataField = _dataSource.Columns[i].ColumnName;
                    //dg.Columns.Add(col);
                    dg.Columns.Add(col);
                }
            }
            else
            {
                for (int i = 0; i < cnt; i++)
                {
                    //col = new System.Web.UI.WebControls.BoundColumn();
                    col = new BoundField();
                    ((BoundField)col).HeaderText = _titles[i];
                    ((BoundField)col).DataField = _fields[i];
                    dg.Columns.Add(col);
                }
            }
        }

        // 添加标题样式
        dg.HeaderStyle.Font.Bold = true;
        dg.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
        //dg.ItemDataBound += new DataGridItemEventHandler(DataGridItemDataBound);
        dg.RowDataBound += new GridViewRowEventHandler(dg_RowDataBound);
        dg.DataBind();
        RenderExcel(dg);
    }

    void dg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow )
        {
           e.Row.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            //e.Item.Cells[3].Attributes.Add("style", "vnd.ms-excel.numberformat:￥#,###.00");
        }
    }

    #endregion

    #region private Methods

    private void RenderExcel(Control c)
    {
        // 确保有一个合法的输出文件名
        if (_fileName == null || _fileName == string.Empty)
        {
            if (!(_fileName.ToLower().EndsWith(".xls")) && !(_fileName.ToLower().EndsWith(".csv")))
                _fileName = GetRandomFileName();
        }
        //throw new Exception(_fileName);
        HttpResponse response = HttpContext.Current.Response;

        response.Charset = "utf-8";
        response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        response.ContentType = "application/ms-excel/msword";
        response.AppendHeader("Content-Disposition", "attachment;filename=" +
            HttpUtility.UrlEncode(_fileName));

        CultureInfo cult = new CultureInfo("en-us", true);
        StringWriter sw = new StringWriter(cult);
        HtmlTextWriter writer = new HtmlTextWriter(sw);

        writer.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf8\">");

        //DataGrid dg = c as DataGrid;
        GridView gv = c as GridView;
        if (gv != null)
        {
            gv.RenderControl(writer);
            //dg.RenderControl(writer);
        }
        //else
        //{
        //    ASPxGrid xgrid = c as ASPxGrid;

        //    if (xgrid != null)
        //        xgrid.RenderControl(writer);
        //    else
        //        throw new ArgumentException("only supports DataGrid or ASPxGrid.", "c");
        //}
        c.Dispose();

        response.Write(sw.ToString());
        response.End();
    }


    /// <summary>
    /// 得到一个随意的文件名
    /// </summary>
    /// <returns></returns>
    private string GetRandomFileName()
    {
        Random rnd = new Random((int)(DateTime.Now.Ticks));
        string s = rnd.Next(Int32.MaxValue).ToString();
        return DateTime.Now.ToShortDateString() + "_" + s + ".xls";
    }

    private void DataGridItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            e.Item.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            //e.Item.Cells[3].Attributes.Add("style", "vnd.ms-excel.numberformat:￥#,###.00");
        }
    }

    #endregion
}
