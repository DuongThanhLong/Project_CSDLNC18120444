using SLRDbConnector;
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
    public partial class Form_Login : Form
    {
        //DbConnector db;
        public Form_Login()
        {
            InitializeComponent();
           // db = new DbConnector();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(isFormValid())
            {
                if(txtUsername.Text == "buy1673")
                {
                    if(txtPassword.Text == "buy1673")
                    {
                        Form1 buy = new Form1();
                        buy.ShowDialog();
                    }
                    else
                        MessageBox.Show("Username or Password is incorrect", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(txtUsername.Text == "sell26")
                {
                    if(txtPassword.Text == "sell26")
                    {
                        Form2 sell = new Form2();
                        sell.ShowDialog();
                    }
                    else
                        MessageBox.Show("Username or Password is incorrect", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

                } else if (txtUsername.Text == "admin")
                {
                    if(txtPassword.Text == "admin")
                    {
                        Form3 ad = new Form3();
                        ad.ShowDialog();
                    }
                }
                else
                    MessageBox.Show("Username or Password is incorrect", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool isFormValid()
        {
            if (txtUsername.Text.ToString().Trim() == string.Empty || txtPassword.Text.ToString().Trim() == string.Empty)
            {
                MessageBox.Show("Required fields must not empty!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {

        }
    }
}
