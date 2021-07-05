using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Shopping
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data Source=MIRINDACOCA;Initial Catalog=AnotherShopee;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        { 
            var dap2 = new SqlDataAdapter("SELECT * FROM LOAI_SAN_PHAM", cn);
            var table2 = new DataTable();
            dap2.Fill(table2);
            cbmFilter.DisplayMember = "TENLOAI";
            cbmFilter.ValueMember = "MALOAI";
            cbmFilter.DataSource = table2;
            cbmFilter.SelectedIndex = -1;

            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void movePanelSide(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            movePanelSide(btnBuy);
            FormBuy frmBuy = new FormBuy();
            frmBuy.ShowDialog();
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            movePanelSide(btnDelivery);
            //FormDelivery frmDelivery = new FormDelivery();
            //frmDelivery.Show();
            cn.Open();
            var cmd = new SqlCommand("sp_DeliveringOrders1673", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            var table = new DataTable();
            dap.Fill(table);
            dgvBuyer.DataSource = table;
        }

        private void btnRate_Click(object sender, EventArgs e)
        {
            movePanelSide(btnRate);
            cn.Open();
            var cmd = new SqlCommand("sp_ViewPurchaseOrders1673", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            var table = new DataTable();
            dap.Fill(table);
            dgvBuyer.DataSource = table;
        }

        private void btnMe_Click(object sender, EventArgs e)
        {
            movePanelSide(btnMe);
            Form1_Me me1 = new Form1_Me();
            me1.ShowDialog();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            movePanelSide(btnHome);
            string query1 = @"SELECT L.TENLOAI, SP.MASP, SP.TENSP, SP.MOTA, SP.SOLUONGTON, SP.GIAHIENTHOI, SP.DANHGIA
                           FROM SAN_PHAM SP, LOAI_SAN_PHAM L
                           WHERE SP.MALOAI = L.MALOAI";
            var dap1 = new SqlDataAdapter(query1, cn);
            var table1 = new DataTable();
            dap1.Fill(table1);
            dgvBuyer.DataSource = table1;

            txtTenSP.DataBindings.Clear();
            txtTenSP.DataBindings.Add("Text", dgvBuyer.DataSource, "TENSP");
            txtTenSP.Text = "";
            txtTenSP.Focus();
        }

        private void cbmFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cbmFilter.SelectedValue);
            string sql = @"SELECT L.TENLOAI, SP.MASP, SP.TENSP, SP.MOTA, SP.SOLUONGTON, SP.GIAHIENTHOI, SP.DANHGIA
                           FROM SAN_PHAM SP, LOAI_SAN_PHAM L
                           WHERE SP.MALOAI = L.MALOAI AND L.MALOAI = " + id + "";
            var dap = new SqlDataAdapter(sql, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvBuyer.DataSource = table;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dap = new SqlDataAdapter("SELECT * FROM DON_VI_VAN_CHUYEN", cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvBuyer.DataSource = table;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            cn.Open();
            var cmd = new SqlCommand("searchByProductName", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TENSP", SqlDbType.Char).Value = txtTenSP.Text;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            var table = new DataTable();
            dap.Fill(table);
            dgvBuyer.DataSource = table;
        }
    }
}
