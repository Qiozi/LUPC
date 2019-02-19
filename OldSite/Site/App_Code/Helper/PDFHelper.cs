using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using GDI = System.Drawing;
using LU.Data;

/// <summary>
/// Summary description for PDFHelper
/// </summary>
public class PDFHelper
{
    LU.Data.nicklu2Entities _context;

    public PDFHelper(nicklu2Entities context)
    {
        //
        // TODO: Add constructor logic here
        //
        _context = context;
    }
    /// <summary>
    /// delete old pdf file.
    /// </summary>
    private void DeleteOldPDFFile(System.Web.UI.Page Page, string order_code)
    {
        DirectoryInfo dir = new DirectoryInfo(Page.Server.MapPath(Config.path_pdf_order));
        System.IO.FileInfo[] fis = dir.GetFiles();
        for (int i = 0; i < fis.Length; i++)
        {
            if (fis[i].FullName.IndexOf(order_code) == -1 && Path.GetExtension(fis[i].FullName).ToLower() == "pdf")
            {
                if (fis[i].LastAccessTime.DayOfYear < DateTime.Now.DayOfYear)
                    File.Delete(fis[i].FullName);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderCode"></param>
    /// <param name="pdf_path_file"></param>
    /// <param name="error"></param>
    /// <param name="Page"></param>
    public void CreatePDF(int orderCode, ref string pdf_path_file, ref string error, System.Web.UI.Page Page)
    {
        //
        // if file exist then delete it.
        DeleteOldPDFFile(Page, orderCode.ToString());

        //
        //
        if (orderCode.ToString().Length == 6
            || orderCode.ToString().Length == 5)
        {
            var oh = new OrderHelper(_context);
            var ohms = OrderHelperModel.GetModelsByOrderCode(_context, orderCode);
            var csm = CustomerStoreModel.FindModelsByOrderCode(_context, orderCode.ToString());

            if (ohms.Length != 1)
                return;
            if (csm.Length != 1)
                return;

            string invoice_code = "";
            string invoice_title = "";
            string invoice_title_big = "";
            if (ohms[0].is_download_invoice.HasValue && ohms[0].is_download_invoice.Value
                && (ohms[0].order_invoice.ToString().Length == Config.OrderInvoiceLength || ohms[0].order_invoice.ToString().Length == 5))
            {
                invoice_title = "Invoice NO.";
                invoice_code = ohms[0].order_invoice.ToString();
                invoice_title_big = "INVOICE  ";
            }
            else
            {
                invoice_title = "Order Code";
                invoice_code = ohms[0].order_code.ToString();
                invoice_title_big = "ORDER FORM";
            }

            Font font16B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, Font.BOLD);
            Font font12B = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
            Font font24B = FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 24, Font.BOLD);
            Font font11 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9);
            Font font11B = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font font8 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8);
            Font font7 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7);
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.LETTER, 5f, 5f, 10f, 10f);               //(PageSize., 80, 50, 30, 65);GetFamilyIndex("Tahoma")
            string filename = Page.Server.MapPath(Config.path_pdf_order + orderCode.ToString() + ".pdf");

            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
            if (!doc.IsOpen())
                doc.Open();
            try
            {
                iTextSharp.text.Table tHeader = new iTextSharp.text.Table(2);
                tHeader.Offset = 20f;
                tHeader.BorderWidth = 0f;
                tHeader.DefaultCellBorderWidth = 0f;



                iTextSharp.text.Table tLogo = new iTextSharp.text.Table(2);
                tLogo.BorderWidth = 0;
                tLogo.DefaultCellBorderWidth = 0f;
                tLogo.SetWidths(new int[] { 25, 200 });

                Phrase p1 = new Phrase("LU COMPUTERS", font16B);

                Cell c1 = new Cell(p1);

                c1.BorderWidth = 0;

                Cell c2 = new Cell(new Phrase("www.lucomputers.com", font12B));
                c2.BorderWidth = 0;
                //Cell c3 = new Cell(new Phrase("1875 Leslie Street, Unit 24", font11));
                //c3.BorderWidth = 0;
                //Cell c4 = new Cell(new Phrase("Toronto, Ontario, M3B2M5", font11));
                //c4.BorderWidth = 0;
                Cell c5 = new Cell(new Phrase(new Chunk("Tel:(866)999-7828  (416)446-7743", font11).SetTextRise(22)));
                c5.BorderWidth = 0;

                tLogo.AddCell(" ");
                tLogo.AddCell(c1);
                tLogo.AddCell(" ");
                tLogo.AddCell(c2);
                tLogo.AddCell(" ");
                tLogo.AddCell(" ");
                tLogo.AddCell(" ");
                tLogo.AddCell(" ");
                tLogo.AddCell(" ");
                tLogo.AddCell(c5);


                // 右上角
                iTextSharp.text.Table tRightTop = new iTextSharp.text.Table(1);
                tRightTop.BorderWidth = 0;

                iTextSharp.text.Table tRightTop1 = new iTextSharp.text.Table(3, 2);
                tRightTop1.Padding = 5f;
                tRightTop1.DefaultHorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                tRightTop1.DefaultVerticalAlignment = iTextSharp.text.Table.ALIGN_MIDDLE;
                tRightTop1.Cellspacing = 5f;


                iTextSharp.text.Table tRightTop2 = new iTextSharp.text.Table(1);
                tRightTop2.Spacing = 0;
                tRightTop2.SpaceBetweenCells = 0;
                tRightTop2.Padding = 0;





                Cell c11 = new Cell(new Phrase(new Chunk("Customer NO.", font11B).SetTextRise(4)));
                Cell c12 = new Cell(new Phrase(new Chunk("Date.", font11B).SetTextRise(4)));
                //Cell c13 = new Cell(new Phrase(new Chunk("Invoice NO.", font11B).SetTextRise(4)));invoice_title
                Cell c13 = new Cell(new Phrase(new Chunk(invoice_title, font11B).SetTextRise(4)));
                Cell c14 = new Cell(new Phrase(new Chunk(Code.FilterCustomerCode(csm[0].customer_serial_no.ToString()), font11).SetTextRise(4)));
                Cell c15 = new Cell(new Phrase(new Chunk((ohms[0].order_date.HasValue ? ohms[0].order_date.Value.ToShortDateString() : ""), font11).SetTextRise(4)));  // order_date
                // Cell c16 = new Cell(new Phrase(new Chunk(oh.FilterOrderCode(ohms[0].order_helper_serial_no.ToString()), font11).SetTextRise(4)));   // order_code
                Cell c16 = new Cell(new Phrase(new Chunk(invoice_code, font11).SetTextRise(4)));
                Cell c17 = new Cell("");
                c17.BorderWidth = 0;

                //Cell c18 = new Cell(new Phrase("INVOICE  ", font24B));
                Cell c18 = new Cell(new Phrase(invoice_title_big, font24B));
                c18.BorderWidth = 0;
                c18.HorizontalAlignment = iTextSharp.text.Table.ALIGN_RIGHT;


                tRightTop1.AddCell(c11);

                tRightTop1.AddCell(c12);
                tRightTop1.AddCell(c13);
                tRightTop1.AddCell(c14);
                tRightTop1.AddCell(c15);
                tRightTop1.AddCell(c16);

                tRightTop.AddCell(c17);

                tRightTop2.AddCell(c18);

                tRightTop.InsertTable(tRightTop1);
                tRightTop.InsertTable(tRightTop2);

                tHeader.InsertTable(tLogo, new GDI.Point(0, 0));
                tHeader.InsertTable(tRightTop, new GDI.Point(0, 1));

                //  联系地址
                iTextSharp.text.Table tAddress = new iTextSharp.text.Table(3, 5);
                tAddress.BorderWidth = 0f;
                tAddress.DefaultCellBorderWidth = 0f;
                tAddress.AutoFillEmptyCells = true;
                tAddress.DefaultVerticalAlignment = iTextSharp.text.Table.ALIGN_MIDDLE;
                tAddress.Offset = 10f;
                tAddress.SetWidths(new int[] { 28, 200, 300 });



                tAddress.AddCell(new Phrase(" ", font11B));
                tAddress.AddCell(new Phrase("Sale to", font11B));
                tAddress.AddCell(new Phrase("Ship to", font11B));
                tAddress.AddCell(new Phrase(" ", font11B));

                tAddress.AddCell(new Phrase(((csm[0].customer_company ?? "") == "" ? "" : csm[0].customer_company + "\r\n") + csm[0].customer_first_name + " " + csm[0].customer_last_name, font11)); // customer name
                tAddress.AddCell(new Phrase(((csm[0].customer_company ?? "") == "" ? "" : csm[0].customer_company + "\r\n") + csm[0].customer_shipping_first_name + " " + csm[0].customer_shipping_last_name, font11));  // ship name

                tAddress.AddCell(new Phrase(" ", font11B));
                if (orderCode == 309914)
                {
                    tAddress.AddCell(new Phrase(csm[0].customer_shipping_address, font11));  // sale: address
                }
                else
                    tAddress.AddCell(new Phrase(csm[0].customer_address1, font11));  // sale: address
                tAddress.AddCell(new Phrase((csm[0].customer_shipping_address ?? "").Trim().Length > 5 ? csm[0].customer_shipping_address : "", font11));  // ship: address

                tAddress.AddCell(new Phrase(" ", font11B));
                tAddress.AddCell(new Phrase((csm[0].customer_address1 ?? "").Trim().Length < 5 ? "" : (csm[0].customer_city + " " + StateShippingModel.GetStateShippingModel(_context, csm[0].customer_card_state.Value).state_short_name + " " + csm[0].zip_code), font11));     // sale: city, state, zipcode
                tAddress.AddCell(new Phrase((csm[0].customer_shipping_address ?? "").Trim().Length > 5 ? (csm[0].customer_shipping_city + " " + csm[0].shipping_state_code + " " + csm[0].customer_shipping_zip_code) : "", font11));     // ship: city, state, zipcode
                tAddress.AddCell(new Phrase(" ", font11B));

                tAddress.AddCell(new Phrase(csm[0].phone_d, font11));      // sale: tel
                tAddress.AddCell(new Phrase(csm[0].phone_d, font11));      // ship: tel


                iTextSharp.text.Table tGST = new iTextSharp.text.Table(1, 3);
                tGST.BorderWidth = 0f;
                tGST.DefaultVerticalAlignment = iTextSharp.text.Table.ALIGN_MIDDLE;
                tGST.DefaultCellBorderWidth = 0f;
                tGST.Cellspacing = 3f;
                tGST.Offset = 18f;

                Cell c21 = new Cell(new Phrase("GST# 855961975RT0001", font11));
                c21.BorderWidthBottom = 0.5f;
                tGST.AddCell(c21);

                iTextSharp.text.Table tListHeader = new iTextSharp.text.Table(1, 2);
                tListHeader.BorderWidth = 0;
                tListHeader.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;// iTextSharp.text.Table.ALIGN_MIDDLE;
                tListHeader.DefaultCellBorderWidth = 0;
                tListHeader.Cellspacing = 0;

                Cell c31 = new Cell(new Phrase(new Chunk(" P.O.No: " + csm[0].my_purchase_order + "               Tax Exemption No:  " + csm[0].tax_execmtion + "             Email address: " + csm[0].customer_email1, font11).SetTextRise(4)));
                c31.BorderWidthBottom = 0.5f;

                Cell c32 = new Cell(new Phrase(new Chunk(" Qnt      Item#              Description                                                                                                 Unit Price                   Extension   ", font11).SetTextRise(4)));

                // 呂總交待，不要單個價格。
                if (orderCode == 309914)
                {
                    c32 = new Cell(new Phrase(new Chunk(" Qnt                          Description                                                                                                                              Extension   ", font11).SetTextRise(4)));
                }

                c32.BorderWidthBottom = 0.5f;

                tListHeader.AddCell(c31);
                tListHeader.AddCell(c32);

                // 产品列表
                DataTable dt = this.FintProductList(orderCode.ToString());

                if (orderCode == 309914)
                {
                    //                    1x SERVER(Supermicro 5039A - IL MT S1151 H4, 16G, 1T x2, Xeon E3 - 1270v6, Windows Server 2016) 
                    //10x STATION(i5-7400 8G 240G SSD Win10) 
                    //10X LG 24" LED (BLK),5ms,DVI 24M38D-B 
                    //11x MS DESKTOP 600 KEYBOARD MOUSE
                    //1x 24PORT GIGABIT POE SWITCH
                    //1x TRI-BAND AC3200 GB ROUTER USB 3.0 GB LAN
                    //1x Back UPS PRO BR 1500VA 10 Out
                    //13x Back UPS NS 450VA 120V CAN
                    //4x Asus ACCESSPOINGT
                    //6x Ge6-outlet SRUGE PROTECTOR
                    //4x BELKIN SURGEMASTER OFFICE 8 OUTLETS
                    //2x Iphone charger 6x Iphone power cords
                    //1x Office cord cover
                    //1x SYSTEM INSTALLATION AND SETUP

                    dt = new DataTable();
                    dt.Columns.Add("qnt");
                    dt.Columns.Add("sku");
                    dt.Columns.Add("name");
                    dt.Columns.Add("unit_price");
                    dt.Columns.Add("extension");
                    dt.Columns.Add("is_non");
                    dt.Columns.Add("part_quantity");

                    DataRow dr1 = dt.NewRow();
                    dr1["qnt"] = 1;
                    dr1["name"] = "SERVER(Supermicro 5039A - IL MT S1151 H4, 16G, 1T x2, Xeon E3 - 1270v6, Windows Server 2016) ";
                    dt.Rows.Add(dr1);

                    DataRow dr2 = dt.NewRow();
                    dr2["qnt"] = 10;
                    dr2["name"] = "STATION(i5-7400 8G 240G SSD Win10) ";
                    dt.Rows.Add(dr2);

                    DataRow dr3 = dt.NewRow();
                    dr3["qnt"] = 10;
                    dr3["name"] = "LG 24\" LED(BLK),5ms,DVI 24M38D - B ";
                    dt.Rows.Add(dr3);

                    DataRow dr4 = dt.NewRow();
                    dr4["qnt"] = 11;
                    dr4["name"] = "MS DESKTOP 600 KEYBOARD MOUSE";
                    dt.Rows.Add(dr4);

                    DataRow dr5 = dt.NewRow();
                    dr5["qnt"] = 1;
                    dr5["name"] = "24PORT GIGABIT POE SWITCH";
                    dt.Rows.Add(dr5);

                    DataRow dr6 = dt.NewRow();
                    dr6["qnt"] = 1;
                    dr6["name"] = "TRI-BAND AC3200 GB ROUTER USB 3.0 GB LAN";
                    dt.Rows.Add(dr6);

                    DataRow dr7 = dt.NewRow();
                    dr7["qnt"] = 1;
                    dr7["name"] = "Back UPS PRO BR 1500VA 10 Out";
                    dt.Rows.Add(dr7);

                    DataRow dr8 = dt.NewRow();
                    dr8["qnt"] = 13;
                    dr8["name"] = "Back UPS NS 450VA 120V CAN";
                    dt.Rows.Add(dr8);

                    DataRow dr9 = dt.NewRow();
                    dr9["qnt"] = 4;
                    dr9["name"] = "Asus ACCESSPOINGT";
                    dt.Rows.Add(dr9);

                    DataRow dr10 = dt.NewRow();
                    dr10["qnt"] = 6;
                    dr10["name"] = "Ge6-outlet SRUGE PROTECTOR";
                    dt.Rows.Add(dr10);

                    DataRow dr11 = dt.NewRow();
                    dr11["qnt"] = 4;
                    dr11["name"] = "BELKIN SURGEMASTER OFFICE 8 OUTLETS";
                    dt.Rows.Add(dr11);

                    DataRow dr12 = dt.NewRow();
                    dr12["qnt"] = 2;
                    dr12["name"] = "Iphone charger 6x Iphone power cords";
                    dt.Rows.Add(dr12);

                    DataRow dr13 = dt.NewRow();
                    dr13["qnt"] = 1;
                    dr13["name"] = "Office cord cover";
                    dt.Rows.Add(dr13);

                    DataRow dr14 = dt.NewRow();
                    dr14["qnt"] = 1;
                    dr14["name"] = "SYSTEM INSTALLATION AND SETUP";
                    dt.Rows.Add(dr14);
                }

                iTextSharp.text.Table tProductList = new iTextSharp.text.Table(6, dt.Rows.Count);
                tProductList.SetWidths(new int[] { 30, 63, 400, 80, 80, 40 });
                tProductList.DefaultCellBorderWidth = 0f;
                tProductList.BorderWidth = 0f;
                tProductList.Offset = 0f;

                float offSet;
                float bs = dt.Rows.Count * 10f;
                if (bs > 180f)
                    offSet = 10f;
                else
                    offSet = 180f - bs;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    //if (dr["is_non"].ToString() == "0" || dr["name"].ToString().ToLower().IndexOf("onboard") != -1)
                    if (dr["name"].ToString().ToLower().IndexOf("onboard") == -1
                        && dr["name"].ToString().ToLower().IndexOf("none selected") == -1)
                    {
                        Cell c50 = new Cell(new Phrase(dr[0].ToString(), font7));
                        c50.HorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                        c50.BorderWidth = 0f;

                        Cell c51 = new Cell(new Phrase(dr[1].ToString(), font7));
                        c51.BorderWidth = 0f;


                        Cell c52 = new Cell(new Phrase(dr[2].ToString(), font7));
                        if (dr["part_quantity"].ToString() != "0")
                            c52 = new Cell(new Phrase((dr[6].ToString() == "1" ? "     " : dr[6].ToString() + "x ") + dr[2].ToString(), font7));

                        c52.BorderWidth = 0f;

                        Cell c53 = new Cell(new Phrase(dr[3].ToString(), font7));
                        c53.BorderWidth = 0f;
                        c53.HorizontalAlignment = iTextSharp.text.Table.ALIGN_RIGHT;

                        Cell c54 = new Cell(new Phrase(dr[4].ToString(), font7));

                        c54.BorderWidth = 0f;
                        c54.HorizontalAlignment = iTextSharp.text.Table.ALIGN_RIGHT;

                        Cell c55 = new Cell("     ");

                        c55.BorderWidth = 0f;
                        c55.HorizontalAlignment = iTextSharp.text.Table.ALIGN_RIGHT;

                        tProductList.AddCell(c50);
                        tProductList.AddCell(c51);
                        tProductList.AddCell(c52);
                        tProductList.AddCell(c53);
                        tProductList.AddCell(c54);
                        tProductList.AddCell(c55);
                    }
                }

                // 订单价格
                iTextSharp.text.Table tPrice = new iTextSharp.text.Table(3);
                tPrice.SetWidths(new int[] { 500, 70, 30 });
                tPrice.DefaultCellBorderWidth = 0f;
                tPrice.BorderWidth = 0f;
                tPrice.Offset = offSet;
                tPrice.DefaultHorizontalAlignment = iTextSharp.text.Table.ALIGN_RIGHT;

                Cell c61 = new Cell(new Phrase("Sub-Total", font11));
                c61.BorderWidth = 0f;
                tPrice.AddCell(c61);



                Cell c62 = new Cell(new Phrase(ohms[0].sub_total.ToString(), font11));
                c62.BorderWidth = 0f;
                tPrice.AddCell(c62);

                Cell c63 = new Cell("  ");
                c63.BorderWidth = 0f;
                tPrice.AddCell(c63);


                if (ohms[0].input_order_discount > 0)
                {
                    Cell c61_1 = new Cell(new Phrase("Special Cash Discount", font11));
                    c61_1.BorderWidth = 0f;
                    tPrice.AddCell(c61_1);



                    Cell c61_2 = new Cell(new Phrase(ohms[0].input_order_discount.ToString(), font11));
                    c61_2.BorderWidth = 0f;
                    tPrice.AddCell(c61_2);

                    Cell c61_3 = new Cell("  ");
                    c61_3.BorderWidth = 0f;
                    tPrice.AddCell(c61_3);
                }


                Cell c64 = new Cell(new Phrase("Shipping & Handling", font11));
                c64.BorderWidth = 0f;
                tPrice.AddCell(c64);

                Cell c65 = new Cell(new Phrase(ohms[0].shipping_charge.ToString(), font11));
                c65.BorderWidth = 0f;
                tPrice.AddCell(c65);

                Cell c66 = new Cell("  ");
                c66.BorderWidth = 0f;
                tPrice.AddCell(c66);


                // 处理价格中的Tax
                //if (ohms[0].tax_charge != 0)
                {

                    if (ohms[0].gst_rate > 0M)
                    {
                        Cell c77_1 = new Cell(new Phrase("GST(" + ohms[0].gst_rate.Value.ToString("##") + "%)", font11));
                        c77_1.BorderWidth = 0f;
                        tPrice.AddCell(c77_1);

                        Cell c78_1 = new Cell(new Phrase(Config.ConvertPrice2(ohms[0].gst.Value), font11));
                        c78_1.BorderWidth = 0f;
                        tPrice.AddCell(c78_1);

                        Cell c79_1 = new Cell("  ");
                        c79_1.BorderWidth = 0f;
                        tPrice.AddCell(c79_1);
                    }
                    if (ohms[0].pst_rate > 0M)
                    {
                        Cell c77_2 = new Cell(new Phrase("PST(" + ohms[0].pst_rate.Value.ToString("##") + "%)", font11));
                        c77_2.BorderWidth = 0f;
                        tPrice.AddCell(c77_2);

                        Cell c78_2 = new Cell(new Phrase(Config.ConvertPrice2(ohms[0].pst.Value), font11));
                        c78_2.BorderWidth = 0f;
                        tPrice.AddCell(c78_2);

                        Cell c79_2 = new Cell("  ");
                        c79_2.BorderWidth = 0f;
                        tPrice.AddCell(c79_2);
                    }

                    if (ohms[0].hst_rate > 0M)
                    {
                        Cell c77_3 = new Cell(new Phrase("HST(" + ohms[0].hst_rate.Value.ToString("##") + "%)", font11));
                        c77_3.BorderWidth = 0f;
                        tPrice.AddCell(c77_3);

                        Cell c78_3 = new Cell(new Phrase(Config.ConvertPrice2(ohms[0].hst.Value), font11));
                        c78_3.BorderWidth = 0f;
                        tPrice.AddCell(c78_3);

                        Cell c79_3 = new Cell("  ");
                        c79_3.BorderWidth = 0f;
                        tPrice.AddCell(c79_3);
                    }

                    if (ohms[0].weee_charge > 0M)
                    {
                        Cell c77_4 = new Cell(new Phrase("WEEE", font11));
                        c77_4.BorderWidth = 0f;
                        tPrice.AddCell(c77_4);

                        Cell c78_4 = new Cell(new Phrase(Config.ConvertPrice2(ohms[0].weee_charge.Value), font11));
                        c78_4.BorderWidth = 0f;
                        tPrice.AddCell(c78_4);

                        Cell c79_4 = new Cell("  ");
                        c79_4.BorderWidth = 0f;
                        tPrice.AddCell(c79_4);
                    }

                    //Cell c78 = new Cell(new Phrase(Config.ConvertPrice2(ohms[0].tax_charge), font11));
                    //c78.BorderWidth = 0f;
                    //tPrice.AddCell(c78);

                    //Cell c79 = new Cell(" ");
                    //c79.BorderWidth = 0f;
                    //tPrice.AddCell(c79);
                }

                Cell c80 = new Cell(new Phrase("Total(" + ohms[0].price_unit + ")", font11));
                c80.BorderWidth = 0f;
                tPrice.AddCell(c80);

                Cell c81 = new Cell(new Phrase(ohms[0].grand_total.ToString(), font11));
                c81.BorderWidth = 0f;
                tPrice.AddCell(c81);

                Cell c82 = new Cell("  ");
                c82.BorderWidth = 0f;
                tPrice.AddCell(c82);

                // sign name area
                iTextSharp.text.Table tSign = new iTextSharp.text.Table(5);
                tSign.SetWidths(new int[] { 104, 140, 35, 140, 200 });
                tSign.DefaultCellBorderWidth = 0f;
                tSign.BorderWidth = 0f;
                tSign.Offset = 10f;

                Cell c90 = new Cell(new Phrase("Customer Signature", font11));
                c90.BorderWidth = 0f;
                Cell c91 = new Cell("   ");
                c91.BorderWidth = 0f;
                c91.BorderWidthBottom = 0.5f;
                Cell c92 = new Cell(new Phrase("Date:", font11));
                c92.HorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                c92.BorderWidth = 0f;
                Cell c93 = new Cell("      ");
                c93.BorderWidth = 0f;
                c93.BorderWidthBottom = 0.5f;
                c93.BorderWidth = 0f;
                Cell c94 = new Cell("  ");
                c94.BorderWidth = 0f;

                tSign.AddCell(c90);
                tSign.AddCell(c91);
                tSign.AddCell(c92);
                tSign.AddCell(c93);
                tSign.AddCell(c94);

                // 说明
                iTextSharp.text.Table tNote = new iTextSharp.text.Table(1);
                tNote.DefaultCellBorderWidth = 0f;
                tNote.BorderWidth = 0f;
                tNote.Offset = 10f;
                Cell c100 = new Cell(new Phrase(@"All sales are subject to LU Computers' terms and policies. No credit for any item that can be replaced. Any returned product must be complete and
unused. All returns must be in their original packing material, and be in re-saleable condition. Credit will not be issued unless the above conditions
are met. All returns are subject to a 15% restocking charge. Notebooks, software and consumable items cannot be returned for credit. Returned
check subject to $20 charge. Late payment shall result in interest charge of two percent for any calendar month or part thereof for which payment or
partial payment remains due. All responsible costs and expenses suffered by LU in collecting monies due including but not limited to attorney's fees
and collection agency fees shall be paid by the purchaser. Warranty claimed items must be shipped/carried in at customer's cost. Returned shipment
without a LU issued RMA (Return Merchandise Authorization) number will be rejected. Warranty does not cover services completed by an
unauthorized third party.", font8));
                c100.BorderWidth = 0f;

                tNote.AddCell(c100);


                doc.Add(tHeader);
                doc.Add(tAddress);
                doc.Add(tGST);
                doc.Add(tListHeader);
                doc.Add(tProductList);
                doc.Add(tPrice);
                doc.Add(tSign);
                doc.Add(tNote);
                if (doc.IsOpen())
                {
                    doc.Close();
                }

                pdf_path_file = filename;
                //File.Delete(filename);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

        }
    }

    /// <summary>
    /// 取得产品列表
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public DataTable FintProductList(string order_code)
    {
        if (order_code.Length != 6
            && order_code.Length != 5)
            return null;
        DataTable dt = new DataTable();

        dt.Columns.Add("qnt");
        dt.Columns.Add("sku");
        dt.Columns.Add("name");
        dt.Columns.Add("unit_price");
        dt.Columns.Add("extension");
        dt.Columns.Add("is_non");
        dt.Columns.Add("part_quantity");

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        var opms = OrderProductModel.GetModelsByOrderCode(_context, order_code);
        //sb.Append(@"<table style=""width: 96%"">");
        for (int i = 0; i < opms.Length; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = opms[i].order_product_sum.ToString();
            dr[1] = order_code.Length == 5 ? ((opms[i].ebayItemID == null || opms[i].ebayItemID.Length < 7) ? opms[i].product_serial_no.ToString() : opms[i].ebayItemID) : opms[i].product_serial_no.ToString();
            dr[2] = opms[i].product_name;
            dr[3] = Config.ConvertPrice2(opms[i].order_product_sold.Value);
            dr[4] = Config.ConvertPrice2(opms[i].order_product_sold.Value * opms[i].order_product_sum.Value);
            dr[5] = "0";
            dr[6] = "0";
            dt.Rows.Add(dr);

            if (opms[i].product_serial_no.ToString().Trim().Length == 8)
            {
                DataTable ms = OrderProductSysDetailModel.GetModelsBySysCode(opms[i].product_serial_no.ToString());
                for (int j = 0; j < ms.Rows.Count; j++)
                {
                    DataRow sdr = ms.Rows[j];
                    DataRow dr2 = dt.NewRow();
                    dr2[0] = "";
                    dr2[1] = "";
                    dr2[2] = "" + sdr["product_name"].ToString();
                    dr2[3] = "";
                    dr2[4] = "";// "x";// +sdr["part_quantity"].ToString();
                    dr2[5] = 0;// sdr["is_non"].ToString();
                    dr2[6] = sdr["part_quantity"].ToString();
                    dt.Rows.Add(dr2);
                }
            }
        }

        //if (dt.Rows.Count < 12)
        //{
        //    int addCount = 12 - dt.Rows.Count;
        //    for (int i = 0; i < addCount; i++)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dr[0] = "  ";
        //        dr[1] = "  ";
        //        dr[2] = "  ";
        //        dr[3] = "  ";
        //        dr[4] = "  ";
        //        dr[5] = "0";
        //        dr[6] = "0";
        //        dt.Rows.Add(dr);
        //    }
        //}
        return dt;
    }
}
