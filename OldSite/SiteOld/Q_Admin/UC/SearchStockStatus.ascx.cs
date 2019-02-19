using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_UC_SearchStockStatus : CtrlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WebLoad();
        }
    }

    private void WebLoad()
    {

    }

    private string StoreCost(int store, decimal cost)
    {
        return string.Format("<span style='color:green;'>{0}</span><b>|</b><span style='color:#ff9900;'>{1}</span>", store, cost.ToString("$###,###.##"));
    }

    private void SetSKUText(int sku)
    {
        ProductModel pm = new ProductModel();

        this.lbl_part_stock.Text = string.Format("<b>{0}</b>{1}", sku, pm.FindStockByLuSku(sku, -1));
    }

    private void BindControlValue()
    {
        LtdHelper lh = new LtdHelper();
        int sku;
        int.TryParse(this.txt_lu_sku.Text, out sku);
        if (sku > 0)
        {
            //throw new Exception(sku.ToString());
            ProductStoreSumModel pss = new ProductStoreSumModel();
            DataTable dt = pss.FindStoreStatusByLuSku(sku);
            SetSKUText(sku);

            if (dt.Rows.Count < 1)
            {
                throw new Exception("No Match Record");
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                int ltd_id;
                int.TryParse(dr["ltd_id"].ToString(), out ltd_id);

                string last_regdate = dr["last_regdate"].ToString();
                int store ;
                int.TryParse(dr["s"].ToString(), out store);

                decimal cost;
                decimal .TryParse(dr["c"].ToString(),out cost);

                if(ltd_id == lh.LtdHelperValue(Ltd.lu))
                {
                    this.lbl_store_lu.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_lu.Text = last_regdate;
                }
                else if(ltd_id == lh.LtdHelperValue(Ltd.wholesaler_ALC))
                {
                    this.lbl_store_alc.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_alc.Text = last_regdate;
                }
                else if (ltd_id == lh.LtdHelperValue(Ltd.wholesaler_asi))
                {
                    this.lbl_store_asi.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_asi.Text = last_regdate;
                }
                else if (ltd_id == lh.LtdHelperValue(Ltd.wholesaler_COMTRONIX))
                {
                    this.lbl_store_comtronix.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_comtuonix.Text = last_regdate;
                }
                else if (ltd_id == lh.LtdHelperValue(Ltd.wholesaler_DAIWA))
                {
                    this.lbl_store_daiwa.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_daiwa.Text = last_regdate;
                }
                else if (ltd_id == lh.LtdHelperValue(Ltd.wholesaler_EPROM))
                {
                    this.lbl_store_eprom.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_eprom.Text = last_regdate;
                }
                else if(ltd_id == lh.LtdHelperValue(Ltd.wholesaler_MINIMICRO))
                {
                    this.lbl_store_minimicro.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_minimicro.Text = last_regdate;
                }
                else if (ltd_id == lh.LtdHelperValue(Ltd.wholesaler_OCZ))
                {
                    this.lbl_store_mmax.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_mmax.Text = last_regdate;
                }
                else if (ltd_id == lh.LtdHelperValue(Ltd.wholesaler_MUTUAL))
                {
                    this.lbl_store_mutual.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_mutual.Text = last_regdate;
                }
                else if (ltd_id == lh.LtdHelperValue(Ltd .wholesaler_SAMTACH))
                {
                    this.lbl_store_samtach.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_samtach.Text = last_regdate;
                }
                else if (ltd_id == lh.LtdHelperValue(Ltd.wholesaler_SINOTECH))
                {
                    this.lbl_store_sinotech.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_sinotech.Text = last_regdate;
                }
                else if (ltd_id == lh.LtdHelperValue(Ltd.wholesaler_supercom))
                {
                    this.lbl_store_supercom.Text = StoreCost(store, cost);
                    this.lbl_last_regdate_supercom.Text = last_regdate;
                }                
            }
        }
    }

    protected void btn_go_Click(object sender, EventArgs e)
    {
        try
        {

            this.lbl_store_lu.Text = string.Empty;
            this.lbl_last_regdate_lu.Text = string.Empty;

            this.lbl_store_alc.Text = string.Empty;
            this.lbl_last_regdate_alc.Text = string.Empty;

            this.lbl_store_asi.Text = string.Empty;
            this.lbl_last_regdate_asi.Text = string.Empty;

            this.lbl_store_comtronix.Text = string.Empty;
            this.lbl_last_regdate_comtuonix.Text = string.Empty;

            this.lbl_store_daiwa.Text = string.Empty;
            this.lbl_last_regdate_daiwa.Text = string.Empty;

            this.lbl_store_eprom.Text = string.Empty;
            this.lbl_last_regdate_eprom.Text = string.Empty;

            this.lbl_store_minimicro.Text = string.Empty;
            this.lbl_last_regdate_minimicro.Text = string.Empty;

            this.lbl_store_mmax.Text = string.Empty;
            this.lbl_last_regdate_mmax.Text = string.Empty;

            this.lbl_store_mutual.Text = string.Empty;
            this.lbl_last_regdate_mutual.Text = string.Empty;

            this.lbl_store_samtach.Text = string.Empty;
            this.lbl_last_regdate_samtach.Text = string.Empty;

            this.lbl_store_sinotech.Text = string.Empty;
            this.lbl_last_regdate_sinotech.Text = string.Empty;

            this.lbl_store_supercom.Text = string.Empty;
            this.lbl_last_regdate_supercom.Text = string.Empty;
            BindControlValue();

        }
        catch (Exception ex)
        {
            Page.CH.Alert(ex.Message, this.btn_go);
        }
    }
}
