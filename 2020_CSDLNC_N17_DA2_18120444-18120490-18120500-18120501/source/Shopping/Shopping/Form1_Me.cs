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
    public partial class Form1_Me : Form
    {
        public Form1_Me()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data Source=MIRINDACOCA;Initial Catalog=AnotherShopee;Integrated Security=True");


        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form1_Me_Load(object sender, EventArgs e)
        {
            var dap = new SqlDataAdapter("SELECT * FROM NGUOI_MUA WHERE MANM = 1673", cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvMe1.DataSource = table;

            txtMaNM.DataBindings.Clear();
            txtMaNM.DataBindings.Add("Text", dgvMe1.DataSource, "MANM");
            txtTenNM.DataBindings.Clear();
            txtTenNM.DataBindings.Add("Text", dgvMe1.DataSource, "TENNM");
            txtNgaySinh.DataBindings.Clear();
            txtNgaySinh.DataBindings.Add("Text", dgvMe1.DataSource, "NGAYSINH");
            txtGioiTinh.DataBindings.Clear();
            txtGioiTinh.DataBindings.Add("Text", dgvMe1.DataSource, "GIOITINH");
            txtEmail.DataBindings.Clear();
            txtEmail.DataBindings.Add("Text", dgvMe1.DataSource, "EMAIL");
            txtDiaChiNM.DataBindings.Clear();
            txtDiaChiNM.DataBindings.Add("Text", dgvMe1.DataSource, "DIACHINM");
            txtSDTNM.DataBindings.Clear();
            txtSDTNM.DataBindings.Add("Text", dgvMe1.DataSource, "SDT");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            txtSDTNM.Text = "";
            txtDiaChiNM.Text = "";
            txtSDTNM.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            //cn.Open();
            //var cmd = new SqlCommand("CAPNHAT_DIACHI_SDTNM", cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@MANM", SqlDbType.Int).Value = txtMaNM.Text;
            //cmd.Parameters.Add("@DIACHI", SqlDbType.Char).Value = txtDiaChiNM.Text;
            //cmd.Parameters.Add("@SDT", SqlDbType.Char).Value = txtSDTNM.Text;
            //var dap = new SqlDataAdapter(cmd);
            //cmd.ExecuteNonQuery();
            //cn.Close();
            //Form1_Me_Load(sender, e);
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            cn.Open();
            var cmd = new SqlCommand("updateBuyer", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MANM", SqlDbType.Int).Value = txtMaNM.Text;
            cmd.Parameters.Add("@DIACHI", SqlDbType.Char).Value = txtDiaChiNM.Text;
            cmd.Parameters.Add("@SDT", SqlDbType.Char).Value = txtSDTNM.Text;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            Form1_Me_Load(sender, e);
        }
    }
}
