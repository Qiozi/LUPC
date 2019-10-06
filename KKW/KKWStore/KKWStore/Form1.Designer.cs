namespace KKWStore
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_inStore = new System.Windows.Forms.Button();
            this.button_outStore = new System.Windows.Forms.Button();
            this.button_return = new System.Windows.Forms.Button();
            this.button_stocktake = new System.Windows.Forms.Button();
            this.button_store = new System.Windows.Forms.Button();
            this.btn_order = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonModifyPwd = new System.Windows.Forms.Button();
            this.btn_export_excel_out_store = new System.Windows.Forms.Button();
            this.button_udpate_saleTotal = new System.Windows.Forms.Button();
            this.buttonPay = new System.Windows.Forms.Button();
            this.buttonStat = new System.Windows.Forms.Button();
            this.buttonProxy = new System.Windows.Forms.Button();
            this.buttonChangeSN = new System.Windows.Forms.Button();
            this.buttonNoexistOut = new System.Windows.Forms.Button();
            this.buttonUploadRemote = new System.Windows.Forms.Button();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.buttonUser = new System.Windows.Forms.Button();
            this.buttonChange = new System.Windows.Forms.Button();
            this.buttonIn = new System.Windows.Forms.Button();
            this.buttonOut2 = new System.Windows.Forms.Button();
            this.btnStockTakeClear = new System.Windows.Forms.Button();
            this.btnBalanceCashRecord = new System.Windows.Forms.Button();
            this.button_cash = new System.Windows.Forms.Button();
            this.buttonCashComein = new System.Windows.Forms.Button();
            this.buttonStoreTotal = new System.Windows.Forms.Button();
            this.buttonCashTotal = new System.Windows.Forms.Button();
            this.buttonMoney = new System.Windows.Forms.Button();
            this.button_inStoreYun = new System.Windows.Forms.Button();
            this.buttonBargainMoney = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonUpdateOrder = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonOutRecord = new System.Windows.Forms.Button();
            this.buttonExportQuantity = new System.Windows.Forms.Button();
            this.buttonYunAsync = new System.Windows.Forms.Button();
            this.buttontUpdateYunPrice = new System.Windows.Forms.Button();
            this.buttonUpdateOut = new System.Windows.Forms.Button();
            this.buttonChangeStore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_inStore
            // 
            this.button_inStore.Location = new System.Drawing.Point(244, 194);
            this.button_inStore.Name = "button_inStore";
            this.button_inStore.Size = new System.Drawing.Size(90, 70);
            this.button_inStore.TabIndex = 0;
            this.button_inStore.Text = "入库【公司】";
            this.button_inStore.UseVisualStyleBackColor = true;
            this.button_inStore.Visible = false;
            this.button_inStore.Click += new System.EventHandler(this.button_inStore_Click);
            // 
            // button_outStore
            // 
            this.button_outStore.Enabled = false;
            this.button_outStore.Location = new System.Drawing.Point(667, 268);
            this.button_outStore.Name = "button_outStore";
            this.button_outStore.Size = new System.Drawing.Size(90, 70);
            this.button_outStore.TabIndex = 1;
            this.button_outStore.Text = "出库【条码扫描】";
            this.button_outStore.UseVisualStyleBackColor = true;
            this.button_outStore.Visible = false;
            this.button_outStore.Click += new System.EventHandler(this.button_outStore_Click);
            // 
            // button_return
            // 
            this.button_return.Location = new System.Drawing.Point(52, 268);
            this.button_return.Name = "button_return";
            this.button_return.Size = new System.Drawing.Size(90, 70);
            this.button_return.TabIndex = 2;
            this.button_return.Text = "退货";
            this.button_return.UseVisualStyleBackColor = true;
            this.button_return.Click += new System.EventHandler(this.button_return_Click);
            // 
            // button_stocktake
            // 
            this.button_stocktake.Location = new System.Drawing.Point(244, 268);
            this.button_stocktake.Name = "button_stocktake";
            this.button_stocktake.Size = new System.Drawing.Size(90, 70);
            this.button_stocktake.TabIndex = 3;
            this.button_stocktake.Text = "盘点";
            this.button_stocktake.UseVisualStyleBackColor = true;
            this.button_stocktake.Visible = false;
            this.button_stocktake.Click += new System.EventHandler(this.button_stocktake_Click);
            // 
            // button_store
            // 
            this.button_store.Location = new System.Drawing.Point(52, 44);
            this.button_store.Name = "button_store";
            this.button_store.Size = new System.Drawing.Size(90, 70);
            this.button_store.TabIndex = 4;
            this.button_store.Text = "仓库";
            this.button_store.UseVisualStyleBackColor = true;
            this.button_store.Click += new System.EventHandler(this.button_store_Click);
            // 
            // btn_order
            // 
            this.btn_order.Location = new System.Drawing.Point(667, 117);
            this.btn_order.Name = "btn_order";
            this.btn_order.Size = new System.Drawing.Size(90, 70);
            this.btn_order.TabIndex = 6;
            this.btn_order.Text = "订单";
            this.btn_order.UseVisualStyleBackColor = true;
            this.btn_order.Visible = false;
            this.btn_order.Click += new System.EventHandler(this.btn_order_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(667, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 70);
            this.button1.TabIndex = 7;
            this.button1.Text = "删除SN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // buttonModifyPwd
            // 
            this.buttonModifyPwd.Location = new System.Drawing.Point(52, 369);
            this.buttonModifyPwd.Name = "buttonModifyPwd";
            this.buttonModifyPwd.Size = new System.Drawing.Size(90, 70);
            this.buttonModifyPwd.TabIndex = 8;
            this.buttonModifyPwd.Text = "修改密码";
            this.buttonModifyPwd.UseVisualStyleBackColor = true;
            this.buttonModifyPwd.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_export_excel_out_store
            // 
            this.btn_export_excel_out_store.Location = new System.Drawing.Point(148, 369);
            this.btn_export_excel_out_store.Name = "btn_export_excel_out_store";
            this.btn_export_excel_out_store.Size = new System.Drawing.Size(90, 70);
            this.btn_export_excel_out_store.TabIndex = 9;
            this.btn_export_excel_out_store.Text = "导出出库统计";
            this.btn_export_excel_out_store.UseVisualStyleBackColor = true;
            this.btn_export_excel_out_store.Visible = false;
            this.btn_export_excel_out_store.Click += new System.EventHandler(this.btn_export_excel_out_store_Click);
            // 
            // button_udpate_saleTotal
            // 
            this.button_udpate_saleTotal.Location = new System.Drawing.Point(52, 443);
            this.button_udpate_saleTotal.Name = "button_udpate_saleTotal";
            this.button_udpate_saleTotal.Size = new System.Drawing.Size(90, 70);
            this.button_udpate_saleTotal.TabIndex = 10;
            this.button_udpate_saleTotal.Text = "上传销售额";
            this.button_udpate_saleTotal.UseVisualStyleBackColor = true;
            this.button_udpate_saleTotal.Visible = false;
            this.button_udpate_saleTotal.Click += new System.EventHandler(this.button_udpate_saleTotal_Click);
            // 
            // buttonPay
            // 
            this.buttonPay.Location = new System.Drawing.Point(148, 443);
            this.buttonPay.Name = "buttonPay";
            this.buttonPay.Size = new System.Drawing.Size(90, 70);
            this.buttonPay.TabIndex = 11;
            this.buttonPay.Text = "支出纪录";
            this.buttonPay.UseVisualStyleBackColor = true;
            this.buttonPay.Visible = false;
            this.buttonPay.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonStat
            // 
            this.buttonStat.Location = new System.Drawing.Point(244, 443);
            this.buttonStat.Name = "buttonStat";
            this.buttonStat.Size = new System.Drawing.Size(90, 70);
            this.buttonStat.TabIndex = 12;
            this.buttonStat.Text = "统计";
            this.buttonStat.UseVisualStyleBackColor = true;
            this.buttonStat.Visible = false;
            this.buttonStat.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonProxy
            // 
            this.buttonProxy.Location = new System.Drawing.Point(244, 369);
            this.buttonProxy.Name = "buttonProxy";
            this.buttonProxy.Size = new System.Drawing.Size(90, 70);
            this.buttonProxy.TabIndex = 13;
            this.buttonProxy.Text = "上传代销成本";
            this.buttonProxy.UseVisualStyleBackColor = true;
            this.buttonProxy.Visible = false;
            this.buttonProxy.Click += new System.EventHandler(this.buttonProxy_Click);
            // 
            // buttonChangeSN
            // 
            this.buttonChangeSN.Location = new System.Drawing.Point(178, 117);
            this.buttonChangeSN.Name = "buttonChangeSN";
            this.buttonChangeSN.Size = new System.Drawing.Size(109, 70);
            this.buttonChangeSN.TabIndex = 14;
            this.buttonChangeSN.Text = "修正条码所属";
            this.buttonChangeSN.UseVisualStyleBackColor = true;
            this.buttonChangeSN.Visible = false;
            this.buttonChangeSN.Click += new System.EventHandler(this.buttonChangeSN_Click);
            // 
            // buttonNoexistOut
            // 
            this.buttonNoexistOut.Location = new System.Drawing.Point(340, 369);
            this.buttonNoexistOut.Name = "buttonNoexistOut";
            this.buttonNoexistOut.Size = new System.Drawing.Size(90, 70);
            this.buttonNoexistOut.TabIndex = 15;
            this.buttonNoexistOut.Text = "手动出库";
            this.buttonNoexistOut.UseVisualStyleBackColor = true;
            this.buttonNoexistOut.Visible = false;
            this.buttonNoexistOut.Click += new System.EventHandler(this.buttonNoexistOut_Click);
            // 
            // buttonUploadRemote
            // 
            this.buttonUploadRemote.Location = new System.Drawing.Point(667, 194);
            this.buttonUploadRemote.Name = "buttonUploadRemote";
            this.buttonUploadRemote.Size = new System.Drawing.Size(90, 70);
            this.buttonUploadRemote.TabIndex = 16;
            this.buttonUploadRemote.Text = "库存上传";
            this.buttonUploadRemote.UseVisualStyleBackColor = true;
            this.buttonUploadRemote.Visible = false;
            this.buttonUploadRemote.Click += new System.EventHandler(this.buttonUploadRemote_Click);
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(148, 268);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(90, 70);
            this.buttonReturn.TabIndex = 17;
            this.buttonReturn.Text = "进/退货历史";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Visible = false;
            this.buttonReturn.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // buttonUser
            // 
            this.buttonUser.Location = new System.Drawing.Point(340, 443);
            this.buttonUser.Name = "buttonUser";
            this.buttonUser.Size = new System.Drawing.Size(90, 70);
            this.buttonUser.TabIndex = 18;
            this.buttonUser.Text = "员工管理";
            this.buttonUser.UseVisualStyleBackColor = true;
            this.buttonUser.Click += new System.EventHandler(this.buttonUser_Click);
            // 
            // buttonChange
            // 
            this.buttonChange.Enabled = false;
            this.buttonChange.Location = new System.Drawing.Point(52, 118);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(120, 70);
            this.buttonChange.TabIndex = 21;
            this.buttonChange.Text = "转库【条码扫描】";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // buttonIn
            // 
            this.buttonIn.Location = new System.Drawing.Point(52, 194);
            this.buttonIn.Name = "buttonIn";
            this.buttonIn.Size = new System.Drawing.Size(186, 70);
            this.buttonIn.TabIndex = 22;
            this.buttonIn.Text = "采购【中转】";
            this.buttonIn.UseVisualStyleBackColor = true;
            this.buttonIn.Click += new System.EventHandler(this.buttonIn_Click);
            // 
            // buttonOut2
            // 
            this.buttonOut2.Location = new System.Drawing.Point(292, 44);
            this.buttonOut2.Name = "buttonOut2";
            this.buttonOut2.Size = new System.Drawing.Size(138, 70);
            this.buttonOut2.TabIndex = 23;
            this.buttonOut2.Text = "出库【不需扫描】";
            this.buttonOut2.UseVisualStyleBackColor = true;
            this.buttonOut2.Click += new System.EventHandler(this.buttonOut2_Click);
            // 
            // btnStockTakeClear
            // 
            this.btnStockTakeClear.Location = new System.Drawing.Point(435, 369);
            this.btnStockTakeClear.Name = "btnStockTakeClear";
            this.btnStockTakeClear.Size = new System.Drawing.Size(92, 70);
            this.btnStockTakeClear.TabIndex = 24;
            this.btnStockTakeClear.Text = "盘点Xls";
            this.btnStockTakeClear.UseVisualStyleBackColor = true;
            this.btnStockTakeClear.Click += new System.EventHandler(this.btnStockTakeClear_Click);
            // 
            // btnBalanceCashRecord
            // 
            this.btnBalanceCashRecord.Location = new System.Drawing.Point(435, 443);
            this.btnBalanceCashRecord.Name = "btnBalanceCashRecord";
            this.btnBalanceCashRecord.Size = new System.Drawing.Size(92, 70);
            this.btnBalanceCashRecord.TabIndex = 25;
            this.btnBalanceCashRecord.Text = "进货支付纪录";
            this.btnBalanceCashRecord.UseVisualStyleBackColor = true;
            this.btnBalanceCashRecord.Click += new System.EventHandler(this.btnBalanceCashRecord_Click);
            // 
            // button_cash
            // 
            this.button_cash.Location = new System.Drawing.Point(532, 443);
            this.button_cash.Name = "button_cash";
            this.button_cash.Size = new System.Drawing.Size(90, 70);
            this.button_cash.TabIndex = 26;
            this.button_cash.Text = "现金流查看";
            this.button_cash.UseVisualStyleBackColor = true;
            this.button_cash.Visible = false;
            this.button_cash.Click += new System.EventHandler(this.button_cash_Click);
            // 
            // buttonCashComein
            // 
            this.buttonCashComein.Location = new System.Drawing.Point(532, 369);
            this.buttonCashComein.Name = "buttonCashComein";
            this.buttonCashComein.Size = new System.Drawing.Size(90, 70);
            this.buttonCashComein.TabIndex = 27;
            this.buttonCashComein.Text = "现金流入帐";
            this.buttonCashComein.UseVisualStyleBackColor = true;
            this.buttonCashComein.Visible = false;
            this.buttonCashComein.Click += new System.EventHandler(this.buttonCashComein_Click);
            // 
            // buttonStoreTotal
            // 
            this.buttonStoreTotal.Location = new System.Drawing.Point(435, 44);
            this.buttonStoreTotal.Name = "buttonStoreTotal";
            this.buttonStoreTotal.Size = new System.Drawing.Size(226, 70);
            this.buttonStoreTotal.TabIndex = 28;
            this.buttonStoreTotal.Text = "当前有效总库存";
            this.buttonStoreTotal.UseVisualStyleBackColor = true;
            this.buttonStoreTotal.Click += new System.EventHandler(this.buttonStoreTotal_Click);
            // 
            // buttonCashTotal
            // 
            this.buttonCashTotal.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCashTotal.Enabled = false;
            this.buttonCashTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCashTotal.Location = new System.Drawing.Point(435, 118);
            this.buttonCashTotal.Name = "buttonCashTotal";
            this.buttonCashTotal.Size = new System.Drawing.Size(226, 70);
            this.buttonCashTotal.TabIndex = 29;
            this.buttonCashTotal.Text = "当前现金流";
            this.buttonCashTotal.UseVisualStyleBackColor = false;
            // 
            // buttonMoney
            // 
            this.buttonMoney.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonMoney.Enabled = false;
            this.buttonMoney.Location = new System.Drawing.Point(435, 268);
            this.buttonMoney.Name = "buttonMoney";
            this.buttonMoney.Size = new System.Drawing.Size(226, 70);
            this.buttonMoney.TabIndex = 30;
            this.buttonMoney.Text = "当前资产";
            this.buttonMoney.UseVisualStyleBackColor = false;
            this.buttonMoney.Click += new System.EventHandler(this.buttonMoney_Click);
            // 
            // button_inStoreYun
            // 
            this.button_inStoreYun.Location = new System.Drawing.Point(339, 194);
            this.button_inStoreYun.Name = "button_inStoreYun";
            this.button_inStoreYun.Size = new System.Drawing.Size(90, 70);
            this.button_inStoreYun.TabIndex = 31;
            this.button_inStoreYun.Text = "入库【云仓】";
            this.button_inStoreYun.UseVisualStyleBackColor = true;
            this.button_inStoreYun.Visible = false;
            this.button_inStoreYun.Click += new System.EventHandler(this.button_inStoreYun_Click);
            // 
            // buttonBargainMoney
            // 
            this.buttonBargainMoney.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonBargainMoney.Enabled = false;
            this.buttonBargainMoney.Location = new System.Drawing.Point(435, 194);
            this.buttonBargainMoney.Name = "buttonBargainMoney";
            this.buttonBargainMoney.Size = new System.Drawing.Size(226, 70);
            this.buttonBargainMoney.TabIndex = 32;
            this.buttonBargainMoney.Text = "压金";
            this.buttonBargainMoney.UseVisualStyleBackColor = false;
            this.buttonBargainMoney.Visible = false;
            this.buttonBargainMoney.Click += new System.EventHandler(this.buttonBargainMoney_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(339, 268);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 70);
            this.button2.TabIndex = 33;
            this.button2.Text = "压金";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.UseWaitCursor = true;
            this.button2.Visible = false;
            // 
            // buttonUpdateOrder
            // 
            this.buttonUpdateOrder.Location = new System.Drawing.Point(627, 369);
            this.buttonUpdateOrder.Name = "buttonUpdateOrder";
            this.buttonUpdateOrder.Size = new System.Drawing.Size(90, 70);
            this.buttonUpdateOrder.TabIndex = 34;
            this.buttonUpdateOrder.Text = "订单统计";
            this.buttonUpdateOrder.UseVisualStyleBackColor = true;
            this.buttonUpdateOrder.Visible = false;
            this.buttonUpdateOrder.Click += new System.EventHandler(this.buttonUpdateOrder_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(722, 361);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 35;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // buttonOutRecord
            // 
            this.buttonOutRecord.Location = new System.Drawing.Point(148, 44);
            this.buttonOutRecord.Name = "buttonOutRecord";
            this.buttonOutRecord.Size = new System.Drawing.Size(139, 70);
            this.buttonOutRecord.TabIndex = 36;
            this.buttonOutRecord.Text = "出库纪录";
            this.buttonOutRecord.UseVisualStyleBackColor = true;
            this.buttonOutRecord.Click += new System.EventHandler(this.buttonOutRecord_Click);
            // 
            // buttonExportQuantity
            // 
            this.buttonExportQuantity.Location = new System.Drawing.Point(627, 443);
            this.buttonExportQuantity.Name = "buttonExportQuantity";
            this.buttonExportQuantity.Size = new System.Drawing.Size(90, 70);
            this.buttonExportQuantity.TabIndex = 37;
            this.buttonExportQuantity.Text = "导出库存数量";
            this.buttonExportQuantity.UseVisualStyleBackColor = true;
            this.buttonExportQuantity.Visible = false;
            this.buttonExportQuantity.Click += new System.EventHandler(this.buttonExportQuantity_Click);
            // 
            // buttonYunAsync
            // 
            this.buttonYunAsync.Location = new System.Drawing.Point(52, 542);
            this.buttonYunAsync.Name = "buttonYunAsync";
            this.buttonYunAsync.Size = new System.Drawing.Size(90, 70);
            this.buttonYunAsync.TabIndex = 38;
            this.buttonYunAsync.Text = "云仓同步库存";
            this.buttonYunAsync.UseVisualStyleBackColor = true;
            this.buttonYunAsync.Click += new System.EventHandler(this.buttonYunAsync_Click);
            // 
            // buttontUpdateYunPrice
            // 
            this.buttontUpdateYunPrice.Location = new System.Drawing.Point(148, 542);
            this.buttontUpdateYunPrice.Name = "buttontUpdateYunPrice";
            this.buttontUpdateYunPrice.Size = new System.Drawing.Size(90, 70);
            this.buttontUpdateYunPrice.TabIndex = 39;
            this.buttontUpdateYunPrice.Text = "上传云仓价格";
            this.buttontUpdateYunPrice.UseVisualStyleBackColor = true;
            this.buttontUpdateYunPrice.Click += new System.EventHandler(this.buttontUpdateYunPrice_Click);
            // 
            // buttonUpdateOut
            // 
            this.buttonUpdateOut.Location = new System.Drawing.Point(244, 542);
            this.buttonUpdateOut.Name = "buttonUpdateOut";
            this.buttonUpdateOut.Size = new System.Drawing.Size(90, 70);
            this.buttonUpdateOut.TabIndex = 40;
            this.buttonUpdateOut.Text = "云仓出库同步";
            this.buttonUpdateOut.UseVisualStyleBackColor = true;
            this.buttonUpdateOut.Visible = false;
            this.buttonUpdateOut.Click += new System.EventHandler(this.buttonUpdateOut_Click);
            // 
            // buttonChangeStore
            // 
            this.buttonChangeStore.Location = new System.Drawing.Point(292, 117);
            this.buttonChangeStore.Name = "buttonChangeStore";
            this.buttonChangeStore.Size = new System.Drawing.Size(137, 70);
            this.buttonChangeStore.TabIndex = 41;
            this.buttonChangeStore.Text = "转库";
            this.buttonChangeStore.UseVisualStyleBackColor = true;
            this.buttonChangeStore.Visible = false;
            this.buttonChangeStore.Click += new System.EventHandler(this.buttonChangeStore_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 624);
            this.Controls.Add(this.buttonChangeStore);
            this.Controls.Add(this.buttonUpdateOut);
            this.Controls.Add(this.buttontUpdateYunPrice);
            this.Controls.Add(this.buttonYunAsync);
            this.Controls.Add(this.buttonExportQuantity);
            this.Controls.Add(this.buttonOutRecord);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonUpdateOrder);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonBargainMoney);
            this.Controls.Add(this.button_inStoreYun);
            this.Controls.Add(this.buttonMoney);
            this.Controls.Add(this.buttonCashTotal);
            this.Controls.Add(this.buttonStoreTotal);
            this.Controls.Add(this.buttonCashComein);
            this.Controls.Add(this.button_cash);
            this.Controls.Add(this.btnBalanceCashRecord);
            this.Controls.Add(this.btnStockTakeClear);
            this.Controls.Add(this.buttonOut2);
            this.Controls.Add(this.buttonIn);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.buttonUser);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.buttonUploadRemote);
            this.Controls.Add(this.buttonNoexistOut);
            this.Controls.Add(this.buttonChangeSN);
            this.Controls.Add(this.buttonProxy);
            this.Controls.Add(this.buttonStat);
            this.Controls.Add(this.buttonPay);
            this.Controls.Add(this.button_udpate_saleTotal);
            this.Controls.Add(this.btn_export_excel_out_store);
            this.Controls.Add(this.buttonModifyPwd);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_order);
            this.Controls.Add(this.button_store);
            this.Controls.Add(this.button_stocktake);
            this.Controls.Add(this.button_return);
            this.Controls.Add(this.button_outStore);
            this.Controls.Add(this.button_inStore);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "酷可维厨具[2018-1-3]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_inStore;
        private System.Windows.Forms.Button button_outStore;
        private System.Windows.Forms.Button button_return;
        private System.Windows.Forms.Button button_stocktake;
        private System.Windows.Forms.Button button_store;
        private System.Windows.Forms.Button btn_order;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonModifyPwd;
        private System.Windows.Forms.Button btn_export_excel_out_store;
        private System.Windows.Forms.Button button_udpate_saleTotal;
        private System.Windows.Forms.Button buttonPay;
        private System.Windows.Forms.Button buttonStat;
        private System.Windows.Forms.Button buttonProxy;
        private System.Windows.Forms.Button buttonChangeSN;
        private System.Windows.Forms.Button buttonNoexistOut;
        private System.Windows.Forms.Button buttonUploadRemote;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.Button buttonUser;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.Button buttonIn;
        private System.Windows.Forms.Button buttonOut2;
        private System.Windows.Forms.Button btnStockTakeClear;
        private System.Windows.Forms.Button btnBalanceCashRecord;
        private System.Windows.Forms.Button button_cash;
        private System.Windows.Forms.Button buttonCashComein;
        private System.Windows.Forms.Button buttonStoreTotal;
        private System.Windows.Forms.Button buttonCashTotal;
        private System.Windows.Forms.Button buttonMoney;
        private System.Windows.Forms.Button button_inStoreYun;
        private System.Windows.Forms.Button buttonBargainMoney;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonUpdateOrder;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonOutRecord;
        private System.Windows.Forms.Button buttonExportQuantity;
        private System.Windows.Forms.Button buttonYunAsync;
        private System.Windows.Forms.Button buttontUpdateYunPrice;
        private System.Windows.Forms.Button buttonUpdateOut;
        private System.Windows.Forms.Button buttonChangeStore;
    }
}

