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
    public partial class Customers : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UIBGCCB\SQLEXPRESS;Initial Catalog=stockdb;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Customers()
        {
            InitializeComponent();
            GetCustomers();
        }

        public void GetCustomers()
        {
            int i = 0;
            dataGridCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCustomer", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerModule customerModuleForm = new CustomerModule();
            customerModuleForm.btnCreate.Enabled = true;
            customerModuleForm.btnUpdate.Enabled = false;
            customerModuleForm.ShowDialog();
            GetCustomers();
        }

        private void dataGridCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CustomerModule customerModuleForm = new CustomerModule();
                customerModuleForm.customerId.Text = dataGridCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModuleForm.customerName.Text = dataGridCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerModuleForm.email.Text = dataGridCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                customerModuleForm.mobileNo.Text = dataGridCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();

                customerModuleForm.btnCreate.Enabled = false;
                customerModuleForm.btnUpdate.Enabled = true;              
                customerModuleForm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are You Sure", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbCustomer WHERE customerId LIKE'" + dataGridCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer Successfully Deleted");
                }
            }
            GetCustomers();
        }
    }
}
