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

namespace stock_mng_system
{
    public partial class Products : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UIBGCCB\SQLEXPRESS;Initial Catalog=stockdb;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Products()
        {
            InitializeComponent();
            GetProducts();

        }
        public void GetProducts()
        {
            int i = 0;
            dataGridProducts.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridProducts.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ProductModule productModuleForm = new ProductModule();
            productModuleForm.btnCreate.Enabled = true;
            productModuleForm.btnUpdate.Enabled = false;
            productModuleForm.ShowDialog();
            GetProducts();
        }

        private void dataGridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridProducts.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModule productModuleForm = new ProductModule();
                productModuleForm.productId.Text = dataGridProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModuleForm.productName.Text = dataGridProducts.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModuleForm.quantity.Text = dataGridProducts.Rows[e.RowIndex].Cells[3].Value.ToString();
                productModuleForm.price.Text = dataGridProducts.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModuleForm.category.Text = dataGridProducts.Rows[e.RowIndex].Cells[5].Value.ToString();

                productModuleForm.btnCreate.Enabled = false;
                productModuleForm.btnUpdate.Enabled = true;
                productModuleForm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are You Sure", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE productId LIKE'" + dataGridProducts.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product Successfully Deleted");
                }
            }
            GetProducts();
        }
    }
}
