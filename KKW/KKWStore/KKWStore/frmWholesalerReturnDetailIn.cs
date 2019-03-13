using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace KKWStore
{
    public partial class frmWholesalerReturnDetailIn : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        enums.ReturnFindType _returnFindType;
        string _p_code = ""; // 如果是进货，将传批次号数据
        DateTime _beginDate;
        DateTime _endDate;
        string _brand;

        public frmWholesalerReturnDetailIn(enums.ReturnFindType _t, DateTime beginDate, DateTime endDate, string Brand, string p_code)
        {
            _returnFindType = _t;
            _p_code = p_code;
            _beginDate = beginDate;
            _endDate = endDate;
            _brand = Brand;

            InitializeComponent();
            this.Shown += new EventHandler(frmWholesalerReturnDetailIn_Shown);
        }

        void frmWholesalerReturnDetailIn_Shown(object sender, EventArgs e)
        {
            this.Text = _p_code;

            BindList();
        }

        void BindList()
        {
           // string p_code = "";
           // string p_name = "";

            if (_returnFindType == enums.ReturnFindType.Out)
            {
                var pm = db.ProductModel_p.GetModelByCode(context, _p_code);

                ListViewItem li = new ListViewItem("商品编号: " + pm.p_code);
                listView1.Items.Add(li);

                ListViewItem li2 = new ListViewItem("商品名称:" + pm.p_name);
                listView1.Items.Add(li2);
            }
            if (_returnFindType == enums.ReturnFindType.In)
            {
                DataTable dt = db.InInvoiceProductModel_p.GetProductCodeAndNameByInvoiceCode(_p_code);
                if (dt.Rows.Count > 0)
                {
                    ListViewItem li = new ListViewItem("商品编号: " + dt.Rows[0]["p_code"].ToString());
                    listView1.Items.Add(li);

                    ListViewItem li2 = new ListViewItem("商品名称:" + dt.Rows[0]["p_name"].ToString());
                    listView1.Items.Add(li2);
                }
            }

            if (_returnFindType == enums.ReturnFindType.In)
            {
                DataTable dt = db.InInvoiceProductModel_p.GetInStoreHistorySN(_beginDate, _endDate, _p_code);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ListViewItem subLi = new ListViewItem(dr["SerialNo"].ToString());
                    subLi.SubItems.Add(dr["d"].ToString());
                    listView1.Items.Add(subLi);
                }
            }
            else if (_returnFindType == enums.ReturnFindType.Out)
            {
                DataTable dt = db.SerialNoAndPCodeModel_p.GetOutListSN(_beginDate, _endDate, _brand, _p_code);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ListViewItem subLi = new ListViewItem(dr["SerialNo"].ToString());
                    subLi.SubItems.Add(dr["d"].ToString());
                    listView1.Items.Add(subLi);
                }
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            KKWStore.Helper.ExportExcel.Export(listView1, (_returnFindType == enums.ReturnFindType.In ? "进货明细" : (_returnFindType == enums.ReturnFindType.Out ? "出库明细" : "退货明细")));
        }
    }
}
