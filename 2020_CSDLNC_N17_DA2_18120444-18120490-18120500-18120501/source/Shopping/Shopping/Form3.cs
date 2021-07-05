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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data Source=MIRINDACOCA;Initial Catalog=AnotherShopee;Integrated Security=True");

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBuyer_Click(object sender, EventArgs e)
        {
            string query = @"SELECT *
                                FROM NGUOI_MUA";
            var dap = new SqlDataAdapter(query, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvAdmin.DataSource = table;
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            string query = @"SELECT *
                                FROM NGUOI_BAN";
            var dap = new SqlDataAdapter(query, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvAdmin.DataSource = table;
        }

        private void btnRate_Click(object sender, EventArgs e)
        {
            string query = @"SELECT L.*, SP.*
                             FROM SAN_PHAM SP, LOAI_SAN_PHAM L
                             WHERE SP.MALOAI = L.MALOAI";
            var dap = new SqlDataAdapter(query, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvAdmin.DataSource = table;
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            string query = @"SELECT DH.MA_DH, CT.MA_DH, DH.MANM, CT.MASP, CT.SO_LUONG, CT.DANHGIA, CT.COMMENT, DH.NGAYDATHANG, DH.MATT, DH.MAUD, TT.HINH_THUC, TT.SO_TIEN
                                FROM DON_HANG DH, CHI_TIET_DON_HANG CT, THANH_TOAN TT
                                WHERE DH.MA_DH = CT.MA_DH AND TT.MATT = DH.MATT";
            var dap = new SqlDataAdapter(query, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvAdmin.DataSource = table;

            txtMonth.Text = "";
            txtDay.Text = "";
            txtMonth.Focus();
        }

        private void btnPGH_Click(object sender, EventArgs e)
        {
            string query = @"SELECT GH.MAPHIEUGH, GH.ID, CT.ID, CT.MA_DH, DH.NGAYDATHANG, CT.MASP, CT.SO_LUONG, GH.LANGIAO, GH.NGAYGIAO, GH.TINHTRANG, DVVC.MA_DVVC, TEN_DVVC
                                FROM PHIEU_GIAO_HANG GH, CHI_TIET_DON_HANG CT, DON_HANG DH, DON_VI_VAN_CHUYEN DVVC
                                WHERE GH.ID = CT.ID AND GH.MA_DVVC = DVVC.MA_DVVC AND CT.MA_DH = DH.MA_DH";
            var dap = new SqlDataAdapter(query, cn);
            var table = new DataTable();
            dap.Fill(table);
            dgvAdmin.DataSource = table;

            txtDelivery.DataBindings.Clear();
            txtDelivery.DataBindings.Add("Text", dgvAdmin.DataSource, "MAPHIEUGH");
            txtTinhTrang.DataBindings.Clear();
            txtTinhTrang.DataBindings.Add("Text", dgvAdmin.DataSource, "TINHTRANG");
            //txtDelivery.Text = "";
            txtTinhTrang.Text = "";
            txtTinhTrang.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cn.Open();
            var cmd = new SqlCommand("CAPNHAT_TINH_TRANG_DON_HANG", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MAPGH", SqlDbType.Int).Value = txtDelivery.Text;
            cmd.Parameters.Add("@TINHTRANG", SqlDbType.VarChar).Value = txtTinhTrang.Text;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            var table = new DataTable();
            dap.Fill(table);
            dgvAdmin.DataSource = table;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            cn.Open();
            var cmd = new SqlCommand("TheNumberOfOrders", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = txtMonth.Text;
            cmd.Parameters.Add("@day", SqlDbType.Int).Value = txtDay.Text;
            var dap = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            cn.Close();
            var table = new DataTable();
            dap.Fill(table);
            dgvAdmin.DataSource = table;
        }
    }
}
