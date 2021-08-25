using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace stock_mng_system
{
    public partial class CustomerModule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UIBGCCB\SQLEXPRESS;Initial Catalog=stockdb;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        public CustomerModule()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure!", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbCustomer(customerName,email,mobileNo)VALUES(@customerName,@email,@mobileNo)", con);
                    cm.Parameters.AddWithValue("@customerName", customerName.Text);
                    cm.Parameters.AddWithValue("@email", email.Text);                 
                    cm.Parameters.AddWithValue("@mobileNo", mobileNo.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer Successfully Saved");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnCreate.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void Clear()
        {
            customerName.Clear();
            email.Clear();          
            mobileNo.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure!", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbCustomer SET customerName=@customerName, email=@email, mobileNo=@mobileNo WHERE customerId LIKE '" +customerId.Text+"'", con);
                    cm.Parameters.AddWithValue("@customerName", customerName.Text);
                    cm.Parameters.AddWithValue("@email", email.Text);
                    cm.Parameters.AddWithValue("@mobileNo", mobileNo.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer Successfully Updated");
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
