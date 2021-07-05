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
    public partial class Form_AddProduct : Form
    {
        public Form_AddProduct()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data Source=MIRINDACOCA;Initial Catalog=AnotherShopee;Integrated Security=True");

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_AddProduct_Load(object sender, EventArgs e)
        {
            string query = @"SELECT NB.MANB, SP.*
                              FROM NGUOI_BAN NB, BAN B, SAN_PHAM SP, LOAI_SAN_PHAM L
                              WHERE NB.MANB = B.MANB AND B.MASP = SP.MASP AND SP.MALOAI = L.MALOAI";
            var dap = new SqlDataAdapter(query, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvAdd.DataSource = table;

            txtMaNB.DataBindings.Clear();
            txtMaNB.DataBindings.Add("Text", dgvAdd.DataSource, "MANB");
            txtMaLoai.DataBindings.Clear();
            txtMaLoai.DataBindings.Add("Text", dgvAdd.DataSource, "MALOAI");
            txtMaSP.DataBindings.Clear();
            txtMaSP.DataBindings.Add("Text", dgvAdd.DataSource, "MASP");
            txtTenSP.DataBindings.Clear();
            txtTenSP.DataBindings.Add("Text", dgvAdd.DataSource, "TENSP");
            txtSL.DataBindings.Clear();
            txtSL.DataBindings.Add("Text", dgvAdd.DataSource, "SOLUONGTON");
            txtGiaSP.DataBindings.Clear();
            txtGiaSP.DataBindings.Add("Text", dgvAdd.DataSource, "GIAHIENTHOI");
            txtMoTa.DataBindings.Clear();
            txtMoTa.DataBindings.Add("Text", dgvAdd.DataSource, "MOTA");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                var cmd = new SqlCommand("addProduct", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MASP", SqlDbType.Int).Value = txtMaSP.Text;
                cmd.Parameters.Add("@MALOAI", SqlDbType.Int).Value = txtMaLoai.Text;
                cmd.Parameters.Add("@TENSP", SqlDbType.Char).Value = txtTenSP.Text;
                cmd.Parameters.Add("@MOTA", SqlDbType.Char).Value = txtMoTa.Text;
                cmd.Parameters.Add("@SOLUONGTON", SqlDbType.Int).Value = txtSL.Text;
                cmd.Parameters.Add("@GIAHIENTHOI", SqlDbType.Float).Value = txtGiaSP.Text;
                cmd.Parameters.Add("@MANB", SqlDbType.Int).Value = txtMaNB.Text;
                var dap = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                cn.Close();
                Form_AddProduct_Load(sender, e);
                MessageBox.Show("Successfully added", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Failed added", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtMaNB.Text = "";
            txtMaLoai.Text = "";
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtSL.Text = "";
            txtGiaSP.Text = "";
            txtMoTa.Text = "";
            txtMaNB.Focus();
        }
    }
}
