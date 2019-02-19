using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

public partial class temp : System.Web.UI.Page
{
    static DataTable datatable = new DataTable("Hellp Test PDF");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataRow dr;
            datatable.Columns.Add("编号");
            datatable.Columns.Add("用户名");

            for (int i = 0; i < 5; i++)
            {
                dr = datatable.NewRow();
                dr[0] = System.Convert.ToString(i);
                dr[1] = "Hello Qiozi" + System.Convert.ToString(i);
                datatable.Rows.Add(dr);

            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath("test.pdf"), FileMode.Create));
            document.Open();

            BaseFont bfChinese = BaseFont.CreateFont("C:\\windows\\fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font fontChinese = new Font(bfChinese, 12, Font.NORMAL, new Color(0, 0, 0));
            document.Add(new Paragraph(this.TextBox1.Text.ToString(), fontChinese));
            
            iTextSharp.text.Image Jpeg = iTextSharp.text.Image.GetInstance(Server.MapPath("images/hot.gif"));
            document.Add(Jpeg);

            PdfPTable table = new PdfPTable(datatable.Columns.Count);
          
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                for (int j = 0; j < datatable.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(datatable.Rows[i][j].ToString(), fontChinese));
                }
            }
            document.Add(table);

            document.Close();
        }
        catch (DocumentException de)
        {
            Response.Write(de.ToString());
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder db = new System.Text.StringBuilder();
        db.Append("<html>");
        db.Append("<head>");
        //db.Append("<title>LU Computers</title>");
        db.Append("</head>");
        db.Append("<body>");
        db.Append("<h1> Qiozi </h1>");
        db.Append("<table><tr><td>Q</td><td style='font-size: 38pt;'>iozi</td></tr></table>");
        db.Append("</body>");
        db.Append("</html>");

        Document doc = new Document(PageSize.A4, 80, 50, 30, 65);
        PdfWriter pdfwriter = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("qiozi_test2.pdf"), FileMode.Create));
        doc.Open();
         //Font fontChinese = new Font(bfChinese, 12, Font.NORMAL, new Color(0, 0, 0));
        iTextSharp.text.html.simpleparser.HTMLWorker htmlworker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
        StringReader sr = new StringReader(db.ToString());
        ArrayList p = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(sr, null);
        Paragraph pg = new Paragraph();
        
        for (int i = 0; i < p.Count; i++)
        {
            pg.Add((IElement)p[i]);
        }
        doc.Add(pg);
        doc.Close();
        //iTextSharp.text.pdf.PdfContentByte cb = pdfwriter.DirectContent;
        //iTextSharp.text.pdf.ColumnText ct = new ColumnText(cb);
        //ct.SetSimpleColumn(doc.Left, 0, doc.Right, doc.Top);
        //ct.YLine = doc.Top;
        //iTextSharp.text.html.HtmlParser.Parse(doc, Server.MapPath("email_templete.htm"));
        //doc.Close();
    }
}
