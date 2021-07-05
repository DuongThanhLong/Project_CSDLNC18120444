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
    public partial class Form2_Me : Form
    {
        public Form2_Me()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data Source=MIRINDACOCA;Initial Catalog=AnotherShopee;Integrated Security=True");


        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form2_Me_Load(object sender, EventArgs e)
        {
            string query = @"SELECT *
                              FROM NGUOI_BAN
                              WHERE MANB = 80";
            var dap = new SqlDataAdapter(query, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvMe2.DataSource = table;

            txtMaNB.DataBindings.Clear();
            txtMaNB.DataBindings.Add("Text", dgvMe2.DataSource, "MANB");
            txtTenNB.DataBindings.Clear();
            txtTenNB.DataBindings.Add("Text", dgvMe2.DataSource, "TENNB");
            txtSDTNB.DataBindings.Clear();
            txtSDTNB.DataBindings.Add("Text", dgvMe2.DataSource, "SDT");
            txtDiaChiNB.DataBindings.Clear();
            txtDiaChiNB.DataBindings.Add("Text", dgvMe2.DataSource, "DIACHINB");
            txtNgayDKB.DataBindings.Clear();
            txtNgayDKB.DataBindings.Add("Text", dgvMe2.DataSource, "NGAYDANGKYBAN");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string query = @"SELECT *
            //                  FROM NGUOI_BAN
            //                  WHERE MANB = 80";
            //var dap = new SqlDataAdapter(query, cn);
            //var table = new DataTable();
            //dap.Fill(table);
            //dgvMe2.DataSource = table;

            txtDiaChiNB.Text = "";
            txtSDTNB.Text = "";
            txtDiaChiNB.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cn.Open();
            var cmd = new SqlCommand("updateSeller", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MANB", SqlDbType.Int).Value = txtMaNB.Text;
            cmd.Parameters.Add("@DIACHI", SqlDbType.Char).Value = txtDiaChiNB.Text;
            cmd.Parameters.Add("@SDT", SqlDbType.Char).Value = txtSDTNB.Text;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            Form2_Me_Load(sender, e);
        }
    }
}
