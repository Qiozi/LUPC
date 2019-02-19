using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_product_helper_match_sku_2 : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
            CH.CloseParentWatting(this.btn_save);
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        this.MenuChildName1.menu_child_serial_no = CategoryID;
        BindPartListDL();
    }

    private void BindPartListDL()
    {
        int count = 0;
        ProductModel pms = new ProductModel();
        this.ListView_part.DataSource = pms.FindPartNameSKUManufacture(CategoryID, ref count);
        this.ListView_part.DataBind();
    }


    #region perporties
    public int CategoryID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "categoryID", -1); }
    }
    #endregion
    protected void ListView_part_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            Label lu_sku_L = (Label)e.Item.FindControl("_lbl_lu_sku");
            LtdHelper lh = new LtdHelper();
            int lu_sku;
            int.TryParse(lu_sku_L.Text, out lu_sku);
            ProductStoreSumModel pssm = new ProductStoreSumModel();
            DataTable dt = Config.ExecuteDataTable(string.Format(@"select id, other_inc_sku,other_inc_type 	from 	tb_other_inc_match_lu_sku where lu_sku='{0}'", lu_sku));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                int ltd_id;
                int.TryParse(dr["other_inc_type"].ToString(), out ltd_id);

                string ltd_sku;
                //string ltd_part_name;
                //string ltd_last_datetime;
                string id;
                id = dr["id"].ToString();
                ltd_sku = dr["other_inc_sku"].ToString();
                //ltd_part_name = dr["product_name"].ToString();
                //ltd_last_datetime = dr["regdate"].ToString();
                if (ltd_id < 1)
                    break;
                switch (lh.LtdModelByValue(ltd_id))
                {
                    case Ltd.wholesaler_supercom:
                        ((TextBox)e.Item.FindControl("_txt_supercom_sku")).Text = ltd_sku;
                        ((HiddenField)e.Item.FindControl("_hf_supercom_id")).Value = id;

                        break;
                    case Ltd.wholesaler_ALC:
                        ((TextBox)e.Item.FindControl("_txt_alc_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_alc_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_alc_last_datetime")).Text = ltd_last_datetime;
                        ((HiddenField)e.Item.FindControl("_hf_alc_id")).Value = id;

                        break;
                    case Ltd.wholesaler_asi:
                        ((TextBox)e.Item.FindControl("_txt_asi_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_asi_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_asi_last_datetime")).Text = ltd_last_datetime;
                        ((HiddenField)e.Item.FindControl("_hf_asi_id")).Value = id;

                        break;
                    case Ltd.wholesaler_COMTRONIX:
                        ((TextBox)e.Item.FindControl("_txt_comtronix_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_comtronix_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_comtronix_last_datetime")).Text = ltd_last_datetime;
                        ((HiddenField)e.Item.FindControl("_hf_comtronix_id")).Value = id;

                        break;
                    case Ltd.wholesaler_DAIWA:
                        ((TextBox)e.Item.FindControl("_txt_daiwa_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_daiwa_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_daiwa_last_datetime")).Text = ltd_last_datetime;
                        ((HiddenField)e.Item.FindControl("_hf_daiwa_id")).Value = id;

                        break;
                    case Ltd.wholesaler_EPROM:
                        ((TextBox)e.Item.FindControl("_txt_eprom_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_eprom_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_eprom_last_datetime")).Text = ltd_last_datetime;
                        ((HiddenField)e.Item.FindControl("_hf_eprom_id")).Value = id;

                        break;
                    case Ltd.wholesaler_MINIMICRO:
                        ((TextBox)e.Item.FindControl("_txt_minimicro_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_minimicro_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_minimicro_last_datetime")).Text = ltd_last_datetime;
                        ((HiddenField)e.Item.FindControl("_hf_minimicro_id")).Value = id;

                        break;
                    case Ltd.wholesaler_MMAX:
                        ((TextBox)e.Item.FindControl("_txt_mmax_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_mmax_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_mmax_last_datetime")).Text = ltd_last_datetime;
                        //((HiddenField)e.Item.FindControl("_hf_mmax_id")).Value = id;

                        break;
                    case Ltd.wholesaler_OCZ:
                        ((TextBox)e.Item.FindControl("_txt_ocz_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_ocz_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_ocz_last_datetime")).Text = ltd_last_datetime;
                        ((HiddenField)e.Item.FindControl("_hf_ocz_id")).Value = id;

                        break;
                    case Ltd.wholesaler_MUTUAL:
                        ((TextBox)e.Item.FindControl("_txt_mutual_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_mutual_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_mutual_last_datetime")).Text = ltd_last_datetime;
                        ((HiddenField)e.Item.FindControl("_hf_mutual_id")).Value = id;

                        break;
                    case Ltd.wholesaler_SAMTACH:
                        ((TextBox)e.Item.FindControl("_txt_samtach_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_samtach_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_samtach_last_datetime")).Text = ltd_last_datetime;
                        ((HiddenField)e.Item.FindControl("_hf_samtach_id")).Value = id;

                        break;
                    case Ltd.wholesaler_SINOTECH:
                        ((TextBox)e.Item.FindControl("_txt_sinotech_sku")).Text = ltd_sku;
                        //((TextBox)e.Item.FindControl("_txt_sinotech_name")).Text = ltd_part_name;
                        //((Label)e.Item.FindControl("_lbl_sinotech_last_datetime")).Text = ltd_last_datetime;
                        ((HiddenField)e.Item.FindControl("_hf_sinotech_id")).Value = id;

                        break;
                    case Ltd.wholesaler_CanadaComputer:
                        ((TextBox)e.Item.FindControl("_txt_CanadaComputer_sku")).Text = ltd_sku;
                        ((HiddenField)e.Item.FindControl("_hf_CanadaComputer_id")).Value = id;

                        break;
                    case Ltd.Rival_DirectDial:
                        ((TextBox)e.Item.FindControl("_txt_directdial_sku")).Text = ltd_sku;
                        ((HiddenField)e.Item.FindControl("_hf_directdial_id")).Value = id;

                        break;
                    case Ltd.Rival_ETC:
                        ((TextBox)e.Item.FindControl("_txt_etc_sku")).Text = ltd_sku;
                        ((HiddenField)e.Item.FindControl("_hf_etc_id")).Value = id;

                        break;
                    case Ltd.Rival_Ncix:
                        ((TextBox)e.Item.FindControl("_txt_ncix_sku")).Text = ltd_sku;
                        ((HiddenField)e.Item.FindControl("_hf_ncix_id")).Value = id;

                        break;
                    case Ltd.Rival_NewEgg:
                        ((TextBox)e.Item.FindControl("_txt_newegg_sku")).Text = ltd_sku;
                        ((HiddenField)e.Item.FindControl("_hf_newegg_id")).Value = id;

                        break;
                    case Ltd.Rival_PcvOnline:
                        ((TextBox)e.Item.FindControl("_txt_pcvonline_sku")).Text = ltd_sku;
                        ((HiddenField)e.Item.FindControl("_hf_pcvonline_id")).Value = id;

                        break;
                    case Ltd.Rival_TigerDirect:
                        ((TextBox)e.Item.FindControl("_txt_tigerdirect_sku")).Text = ltd_sku;
                        ((HiddenField)e.Item.FindControl("_hf_tigerdirect_id")).Value = id;

                        break;
                }
            }
        }
        catch { }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListView_part_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        TextBox _txt_lu_manufacture = (TextBox)e.Item.FindControl("_txt_lu_manufacture");

        switch (e.CommandName)
        {
            case "supercom":
                TextBox _txt_supercom_sku = (TextBox)e.Item.FindControl("_txt_supercom_sku");
                TextBox _txt_supercom_name = (TextBox)e.Item.FindControl("_txt_supercom_name");
                SetControlValue(Ltd.wholesaler_supercom, _txt_lu_manufacture.Text.Trim(), _txt_supercom_sku, _txt_supercom_name);

                break;
            case "alc":
                TextBox _txt_alc_sku = (TextBox)e.Item.FindControl("_txt_alc_sku");
                TextBox _txt_alc_name = (TextBox)e.Item.FindControl("_txt_alc_name");
                SetControlValue(Ltd.wholesaler_ALC, _txt_lu_manufacture.Text.Trim(), _txt_alc_sku, _txt_alc_name);

                break;
            case "asi":
                TextBox _txt_asi_sku = (TextBox)e.Item.FindControl("_txt_asi_sku");
                TextBox _txt_asi_name = (TextBox)e.Item.FindControl("_txt_asi_name");
                SetControlValue(Ltd.wholesaler_asi, _txt_lu_manufacture.Text.Trim(), _txt_asi_sku, _txt_asi_name);

                break;
            case "comtronix":

                TextBox _txt_comtronix_sku = (TextBox)e.Item.FindControl("_txt_comtronix_sku");
                TextBox _txt_comtronix_name = (TextBox)e.Item.FindControl("_txt_comtronix_name");
                SetControlValue(Ltd.wholesaler_COMTRONIX, _txt_lu_manufacture.Text.Trim(), _txt_comtronix_sku, _txt_comtronix_name);

                break;
            case "daiwa":
                TextBox _txt_daiwa_sku = (TextBox)e.Item.FindControl("_txt_daiwa_sku");
                TextBox _txt_daiwa_name = (TextBox)e.Item.FindControl("_txt_daiwa_name");
                SetControlValue(Ltd.wholesaler_DAIWA, _txt_lu_manufacture.Text.Trim(), _txt_daiwa_sku, _txt_daiwa_name);

                break;
            case "eprom":
                TextBox _txt_eprom_sku = (TextBox)e.Item.FindControl("_txt_eprom_sku");
                TextBox _txt_eprom_name = (TextBox)e.Item.FindControl("_txt_eprom_name");
                SetControlValue(Ltd.wholesaler_EPROM, _txt_lu_manufacture.Text.Trim(), _txt_eprom_sku, _txt_eprom_name);

                break;
            case "minimicro":
                TextBox _txt_minimicro_sku = (TextBox)e.Item.FindControl("_txt_minimicro_sku");
                TextBox _txt_minimicro_name = (TextBox)e.Item.FindControl("_txt_minimicro_name");
                SetControlValue(Ltd.wholesaler_MINIMICRO, _txt_lu_manufacture.Text.Trim(), _txt_minimicro_sku, _txt_minimicro_name);

                break;
            case "mmax":
                TextBox _txt_mmax_sku = (TextBox)e.Item.FindControl("_txt_mmax_sku");
                TextBox _txt_mmax_name = (TextBox)e.Item.FindControl("_txt_mmax_name");
                SetControlValue(Ltd.wholesaler_MMAX, _txt_lu_manufacture.Text.Trim(), _txt_mmax_sku, _txt_mmax_name);

                break;
            case "OCZ":
                TextBox _txt_ocz_sku = (TextBox)e.Item.FindControl("_txt_ocz_sku");
                TextBox _txt_ocz_name = (TextBox)e.Item.FindControl("_txt_ocz_name");
                SetControlValue(Ltd.wholesaler_OCZ, _txt_lu_manufacture.Text.Trim(), _txt_ocz_sku, _txt_ocz_name);

                break;
            case "mutual":

                TextBox _txt_mutual_sku = (TextBox)e.Item.FindControl("_txt_mutual_sku");
                TextBox _txt_mutual_name = (TextBox)e.Item.FindControl("_txt_mutual_name");
                SetControlValue(Ltd.wholesaler_MUTUAL, _txt_lu_manufacture.Text.Trim(), _txt_mutual_sku, _txt_mutual_name);

                break;
            case "samtach":

                TextBox _txt_samtach_sku = (TextBox)e.Item.FindControl("_txt_samtach_sku");
                TextBox _txt_samtach_name = (TextBox)e.Item.FindControl("_txt_samtach_name");
                SetControlValue(Ltd.wholesaler_SAMTACH, _txt_lu_manufacture.Text.Trim(), _txt_samtach_sku, _txt_samtach_name);
                break;
            case "sinotech":
                TextBox _txt_sinotech_sku = (TextBox)e.Item.FindControl("_txt_sinotech_sku");
                TextBox _txt_sinotech_name = (TextBox)e.Item.FindControl("_txt_sinotech_name");
                SetControlValue(Ltd.wholesaler_SINOTECH, _txt_lu_manufacture.Text.Trim(), _txt_sinotech_sku, _txt_sinotech_name);
                break;

        }
        CH.CloseParentWatting(this.ListView_part);
    }

    private void SetControlValue(Ltd ltd, string manufacture_code, TextBox _txt_ltd_sku, TextBox _txt_ltd_part_name)
    {
        LtdHelper lh = new LtdHelper();
        if (manufacture_code == "")
        {
            CH.Alert("工厂SKU 为空，无法查询", this.ListView_part);
            return;
        }
        ProductStoreSumModel[] pssms = ProductStoreSumModel.FindByMenufactureAndLtdID(manufacture_code, lh.LtdHelperValue(ltd));
        if (pssms.Length == 1)
        {
            _txt_ltd_sku.Text = pssms[0].product_serial_no;
            _txt_ltd_part_name.Text = pssms[0].product_name;
        }
        else
        {
            CH.Alert("No match found", this.ListView_part);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ltd"></param>
    /// <param name="product_store_category"></param>
    /// <param name="ltd_sku"></param>
    /// <param name="ltd_part_name"></param>
    /// <param name="lu_sku"></param>
    /// <param name="manufacturer_part_number"></param>
    public void saveLtdInfo(int other_inc_type, string other_inc_sku, int lu_sku, int id)
    {
        if (other_inc_sku.Trim().Length > 0)
        {
            if (id > 0)
            {
                OtherIncMatchLuSkuModel pssm = OtherIncMatchLuSkuModel.GetOtherIncMatchLuSkuModel(id);
                pssm.other_inc_sku = other_inc_sku.Trim();
                pssm.Update();
            }
            else
            {
                OtherIncMatchLuSkuModel pssm = new OtherIncMatchLuSkuModel();
                pssm.lu_sku = lu_sku;
                pssm.other_inc_sku = other_inc_sku.Trim();
                pssm.other_inc_type = other_inc_type;
                pssm.Create();
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            LtdHelper lh = new LtdHelper();
            ListView list = (ListView)this.ListView_part;
            for (int i = 0; i < list.Items.Count; i++)
            {
                int lu_sku;
                int.TryParse(((Label)list.Items[i].FindControl("_lbl_lu_sku")).Text, out lu_sku);
                string _txt_lu_manufacture = ((TextBox)list.Items[i].FindControl("_txt_lu_manufacture")).Text;
                string _ltd_sku;
                //string _ltd_part_name;
                int _id;

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_supercom_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_supercom_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_supercom), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_alc_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_alc_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_ALC), _ltd_sku, lu_sku, _id);


                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_asi_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_asi_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_asi), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_comtronix_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_comtronix_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_COMTRONIX), _ltd_sku, lu_sku, _id);


                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_daiwa_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_daiwa_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_DAIWA), _ltd_sku, lu_sku, _id);


                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_eprom_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_eprom_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_EPROM), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_minimicro_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_minimicro_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_MINIMICRO), _ltd_sku, lu_sku, _id);


                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_mmax_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_mmax_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_MMAX), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_mutual_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_mutual_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_MUTUAL), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_samtach_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_samtach_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_SAMTACH), _ltd_sku, lu_sku, _id);


                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_sinotech_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_sinotech_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_SINOTECH), _ltd_sku, lu_sku, _id);


                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_ocz_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_ocz_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_OCZ), _ltd_sku, lu_sku, _id);


                //_ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_comtronic_sku")).Text;
                //int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_comtronic_id")).Value, out _id);
                //saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_COMTRONIX), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_CanadaComputer_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_CanadaComputer_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.wholesaler_CanadaComputer), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_tigerdirect_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_tigerdirect_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.Rival_TigerDirect), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_directdial_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_directdial_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.Rival_DirectDial), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_pcvonline_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_pcvonline_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.Rival_PcvOnline), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_newegg_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_newegg_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.Rival_NewEgg), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_ncix_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_ncix_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.Rival_Ncix), _ltd_sku, lu_sku, _id);

                _ltd_sku = ((TextBox)list.Items[i].FindControl("_txt_etc_sku")).Text;
                int.TryParse(((HiddenField)list.Items[i].FindControl("_hf_etc_id")).Value, out _id);
                saveLtdInfo(lh.LtdHelperValue(Ltd.Rival_ETC), _ltd_sku, lu_sku, _id);
            }
            CH.CloseParentWatting(this.ListView_part);
            CH.Alert(KeyFields.SaveIsOK, this.ListView_part);

        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.ListView_part);
            CH.Alert(ex.Message, this.ListView_part);
        }
    }
}
