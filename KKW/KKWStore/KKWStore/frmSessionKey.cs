using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmSessionKey : Form
    {
        public frmSessionKey()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmSessionKey_Shown);
        }

        void frmSessionKey_Shown(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri(Helper.TaobaoConfig.LoginUrl);
            //string s = Helper.HttpHelper.HttpGet(Helper.TaobaoConfig.GetAuthorize);
            //Helper.TaobaoConfig.AuthorizeCode = s;
            ////MessageBox.Show(s);
            //this.richTextBox1.Text = s;
            this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
     
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Helper.TaobaoConfig.AuthorizeCode = this.toolStripComboBox1.Text;
            this.webBrowser1.Url = new Uri(Helper.TaobaoConfig.GetAuthorize);
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            MessageBox.Show(this.webBrowser1.Document.Url.PathAndQuery);
        }
    }
}
