using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Data.SqlClient;

namespace ItemModify
{
    public partial class Form1 : Form
    {
        string connStr = @"Data Source=192.168.168.168;Database=mst_fatek;
            User ID=sa;Password=hcu1762;";
        private class item
        {
            public string item_no { get; set; }
            public string item_nm { get; set; }
            public string item_sp { get; set; }
            public double price { get; set; }

        }
        public Form1()
        {
            InitializeComponent();
            buildAutocomplete();
            txtPrice.LostFocus += TxtPrice_LostFocus;
            txtSale.LostFocus += TxtPrice_LostFocus;
            txtCost.LostFocus += TxtPrice_LostFocus;
        }
        #region 控制項

        private void btnSearch_Click(object sender, EventArgs e)
        {
            checkItemNo();
            string item_no = txtItemNo.Text;
            //todo 檢查料號是否存在，帶出品名、規格，進價、售價、成本
            using (var cn = new SqlConnection(connStr))
            {
                var list = cn.Query<item>("select distinct ......");
                foreach(var row in list)
                {
                    lblItem.Text = string.Format(@"品名：{0}\r\n規格：{1}\r\n",
                        row.item_nm,row.item_sp);
                    setMoneyFormat(txtPrice, row.price);
                }
            }
        }
        private void TxtPrice_LostFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = string.Format("{0:#,##0.00}", double.Parse(txt.Text));
        }
        #endregion

        #region 函數
        /// <summary>
        /// 檢查料號
        /// </summary>
        private void checkItemNo()
        {
            if (txtItemNo.Text == "")
                MessageBox.Show("請輸入料號");

        }
        /// <summary>
        /// 料號自動完成
        /// </summary>
        private void buildAutocomplete()
        {
            using (var cn = new SqlConnection(connStr))
            {
                var list = cn.Query<string>("SELECT item_no from item");
                AutoCompleteStringCollection acc = new AutoCompleteStringCollection();
                acc.AddRange(list.ToArray());
                txtItemNo.AutoCompleteCustomSource = acc;
            }
        }
        /// <summary>
        /// 設定textbox貨幣格式
        /// </summary>
        /// <param name="txt">textbox object</param>
        /// <param name="value">值</param>
        private void setMoneyFormat(TextBox txt,double value)
        {
            txt.Text = string.Format("{0:#,##0.00}", value);
        }
        #endregion

    }
}
