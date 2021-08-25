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
    public partial class SalesModule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UIBGCCB\SQLEXPRESS;Initial Catalog=stockdb;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public SalesModule()
        {
            InitializeComponent();
            GetCustomers();
            GetProducts();
        }

        public void GetCustomers()
        {
            int i = 0;
            dataGridCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT customerId,customerName FROM tbCustomer WHERE CONCAT(customerId,customerName) LIKE '%"+searchCustomer.Text +"%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void GetProducts()
        {
            int i = 0;
            dataGridProducts.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(productId,productName,quantity,price,category) LIKE '%" + searchProduct.Text + "%'", con);
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

        private void searchCustomer_TextChanged(object sender, EventArgs e)
        {
            GetCustomers();
        }

        private void searchProduct_TextChanged(object sender, EventArgs e)
        {
            GetProducts();
        }

        private void dataGridCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            customerId.Text = dataGridCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            customerName.Text = dataGridCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();

        }

        int quantity = 0;
        private void dataGridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            productId.Text = dataGridProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
            productName.Text = dataGridProducts.Rows[e.RowIndex].Cells[2].Value.ToString();
            quantity = Convert.ToInt16(dataGridProducts.Rows[e.RowIndex].Cells[3].Value.ToString());
            price.Text = dataGridProducts.Rows[e.RowIndex].Cells[4].Value.ToString();          

        }

      
        private void numericQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(numericQuantity.Value) > quantity)
            {
                MessageBox.Show("Invalid Quantity", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToInt16(numericQuantity.Value) > 0)
            {
                int total = Convert.ToInt16(price.Text) * Convert.ToInt16(numericQuantity.Value);
                totalAmount.Text = total.ToString();
            }
          
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerId.Text == "")
                {
                    MessageBox.Show("Please Select Customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (productId.Text == "")
                {
                    MessageBox.Show("Please Select Product", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are You Sure!", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbSales(productId,customerId,quantity,price,totalAmount)VALUES(@productId,@customerId,@quantity,@price,@totalAmount)", con);
                    cm.Parameters.AddWithValue("@productId", Convert.ToInt16(productId.Text));
                    cm.Parameters.AddWithValue("@customerId", Convert.ToInt16(customerId.Text));
                    cm.Parameters.AddWithValue("@quantity", Convert.ToInt16(numericQuantity.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt16(price.Text));
                    cm.Parameters.AddWithValue("@totalAmount", Convert.ToInt16(totalAmount.Text));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order Successfully Saved");

                    cm = new SqlCommand("UPDATE tbProduct SET quantity = (quantity- @quantity) WHERE productId LIKE '" + productId.Text + "'", con);
                    cm.Parameters.AddWithValue("@quantity", Convert.ToInt16(numericQuantity.Text));                  
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    GetProducts();
                 
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
        }

        public void Clear()
        {
            customerId.Clear();
            customerName.Clear();
            productId.Clear();
            productName.Clear();
            price.Clear();
            numericQuantity.Value = 0;
            totalAmount.Clear();
            price.Clear();
        }
    }
}
