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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data Source=MIRINDACOCA;Initial Catalog=AnotherShopee;Integrated Security=True");

        private void btnMe_Click(object sender, EventArgs e)
        {
            Form2_Me me2 = new Form2_Me();
            me2.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //string query = @"SELECT NB.MANB, SP.*
            //                  FROM NGUOI_BAN NB, BAN B, SAN_PHAM SP, LOAI_SAN_PHAM L
            //                  WHERE NB.MANB = B.MANB AND B.MASP = SP.MASP AND SP.MALOAI = L.MALOAI";
            //var dap = new SqlDataAdapter(query, cn);
            //var table = new DataTable();
            //dap.Fill(table);
            //dgvSeller.DataSource = table;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            Form_AddProduct add = new Form_AddProduct();
            add.ShowDialog();
        }

        private void btnEditProducts_Click(object sender, EventArgs e)
        {
            string query = @"SELECT SP.*
                              FROM NGUOI_BAN NB, BAN B, SAN_PHAM SP, LOAI_SAN_PHAM L
                              WHERE NB.MANB = 26 AND NB.MANB = B.MANB AND B.MASP = SP.MASP AND SP.MALOAI = L.MALOAI";
            var dap = new SqlDataAdapter(query, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvSeller.DataSource = table;

            //txtMaNB.DataBindings.Clear();
            //txtMaNB.DataBindings.Add("Text", dgvSeller.DataSource, "MALOAI");
            //txtTenSP.DataBindings.Clear();
            //txtTenSP.DataBindings.Add("Text", dgvSeller.DataSource, "TENSP");
            //txtSL.DataBindings.Clear();
            //txtSL.DataBindings.Add("Text", dgvSeller.DataSource, "SOLUONGTON");
            //txtGiaSP.DataBindings.Clear();
            //txtGiaSP.DataBindings.Add("Text", dgvSeller.DataSource, "GIAHIENTHOI");
            //txtMoTa.DataBindings.Clear();
            //txtMoTa.DataBindings.Add("Text", dgvSeller.DataSource, "MOTA");

            //txtMaNB.Text = "";
            //txtTenSP.Text = "";
            //txtSL.Text = "";
            //txtGiaSP.Text = "";
            //txtMoTa.Text = "";
            //txtMaNB.Focus();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cn.Open();
            var cmd = new SqlCommand("PRINTPRODUCTS", cn);
            cmd.Parameters.Add("@MANB", SqlDbType.Int).Value = txtMaNB.Text;
            cmd.CommandType = CommandType.StoredProcedure;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            var table = new DataTable();
            dap.Fill(table);
            dgvSeller.DataSource = table;

            txtMaSP.DataBindings.Clear();
            txtMaSP.DataBindings.Add("Text", dgvSeller.DataSource, "MASP");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            string query = @"SELECT NB.MANB, SP.*
                              FROM NGUOI_BAN NB, BAN B, SAN_PHAM SP, LOAI_SAN_PHAM L
                              WHERE NB.MANB = B.MANB AND B.MASP = SP.MASP AND SP.MALOAI = L.MALOAI";
            var dap = new SqlDataAdapter(query, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvSeller.DataSource = table;

            txtMaNB.DataBindings.Clear();
            txtMaNB.DataBindings.Add("Text", dgvSeller.DataSource, "MANB");
            txtMaNB.Text = "";
            txtMaNB.Focus();
        }

        private void btnDeleteProducts_Click(object sender, EventArgs e)
        {
            txtMaSP.Text = "";
            txtMaSP.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cn.Open();
            var cmd = new SqlCommand("DEL_SP", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MASP", SqlDbType.Int).Value = txtMaSP.Text;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            var table = new DataTable();
            dap.Fill(table);
            dgvSeller.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cn.Open();
            var cmd = new SqlCommand("Orders26", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            var table = new DataTable();
            dap.Fill(table);
            dgvSeller.DataSource = table;
        }
    }
}
