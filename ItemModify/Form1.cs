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
                //acc.Add("432-167-55");
                //acc.Add("432-165-56");
                //acc.Add("432-164-57");
                //acc.Add("432-163-58");
                acc.AddRange(list.ToArray());
                txtItemNo.AutoCompleteCustomSource = acc;
            }
        }
        #endregion

    }
}
