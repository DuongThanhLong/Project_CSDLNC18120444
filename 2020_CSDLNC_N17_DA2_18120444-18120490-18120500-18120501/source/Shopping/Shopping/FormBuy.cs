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
    public partial class FormBuy : Form
    {
        public FormBuy()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data Source=MIRINDACOCA;Initial Catalog=AnotherShopee;Integrated Security=True");


        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cbmShip_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(cbmShip.SelectedValue);
            //string sql = @"SELECT DVVC.*
            //               FROM DON_VI_VAN_CHUYEN DVVC
            //               WHERE DVVC.MA_DVVC = " + id + "";
            //var dap = new SqlDataAdapter(sql, cn);
            //var table = new DataTable();
            //dap.Fill(table);
            //dgvBuy.DataSource = table;
        }

        private void FormBuy_Load(object sender, EventArgs e)
        {
            string query = @"SELECT GH.MANM, GH.MAGIOHANG, GH.TENSP, GH.MASP, GH.SOLUONG, TT.HINH_THUC, DVVC.MA_DVVC
                             FROM GIO_HANG GH, DON_HANG DH, THANH_TOAN TT, CHI_TIET_DON_HANG CT, PHIEU_GIAO_HANG PGH, DON_VI_VAN_CHUYEN DVVC
                             WHERE GH.MANM = 1673 AND GH.MADH = DH.MA_DH AND DH.MATT = TT.MATT AND DH.MA_DH = CT.MA_DH AND CT.ID = PGH.ID AND PGH.MA_DVVC = DVVC.MA_DVVC";
            var dap = new SqlDataAdapter(query, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvBuy.DataSource = table;
            txtMaNM.DataBindings.Clear();
            txtMaNM.DataBindings.Add("Text", dgvBuy.DataSource, "MANM");
            txtMaSP.DataBindings.Clear();
            txtMaSP.DataBindings.Add("Text", dgvBuy.DataSource, "MASP");
            txtTenSP.DataBindings.Clear();
            txtTenSP.DataBindings.Add("Text", dgvBuy.DataSource, "TENSP");
            txtSLSP.DataBindings.Clear();
            txtSLSP.DataBindings.Add("Text", dgvBuy.DataSource, "SOLUONG");
            txtHinhThucTT.DataBindings.Clear();
            txtHinhThucTT.DataBindings.Add("Text", dgvBuy.DataSource, "HINH_THUC");
            txtDVVC.DataBindings.Clear();
            txtDVVC.DataBindings.Add("Text", dgvBuy.DataSource, "MA_DVVC");

            txtMaNM.Text = "";
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtSLSP.Text = "";
            txtHinhThucTT.Text = "";
            txtDVVC.Text = "";
            txtMaNM.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dap = new SqlDataAdapter("SELECT * FROM DON_VI_VAN_CHUYEN", cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvBuy.DataSource = table;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            cn.Open();
            var cmd = new SqlCommand("addToCart", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MANM", SqlDbType.Int).Value = txtMaNM.Text;
            cmd.Parameters.Add("@MASP", SqlDbType.Int).Value = txtMaSP.Text;
            cmd.Parameters.Add("@TENSP", SqlDbType.Char).Value = txtTenSP.Text;
            cmd.Parameters.Add("@SOLUONG", SqlDbType.Int).Value = txtSLSP.Text;
            cmd.Parameters.Add("@HINHTHUCTHANHTOAN", SqlDbType.Char).Value = txtHinhThucTT.Text;
            cmd.Parameters.Add("@MADVVC", SqlDbType.Int).Value = txtDVVC.Text;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            var table = new DataTable();
            dap.Fill(table);
            dgvBuy.DataSource = table;
        }
    }
}
