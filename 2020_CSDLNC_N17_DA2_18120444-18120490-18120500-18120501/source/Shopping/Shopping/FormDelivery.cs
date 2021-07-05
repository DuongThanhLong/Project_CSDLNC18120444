using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shopping
{
    public partial class FormDelivery : Form
    {
        public FormDelivery()
        {
            InitializeComponent();
        }

        private void FormDelivery_Load(object sender, EventArgs e)
        {
            //string query = @"SELECT *
            //                 FROM NGUOI_MUA
            //                 WHERE MANM = 818";
            //var dap = new SqlDataAdapter(query, cn);
            //var table = new DataTable();
            //dap.Fill(table);
            //dgvBuyer.DataSource = table;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
