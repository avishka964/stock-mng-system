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

    public partial class ProductModule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UIBGCCB\SQLEXPRESS;Initial Catalog=stockdb;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModule()
        {
            InitializeComponent();
            GetCategories();
        }


        public void GetCategories()
        {
            category.Items.Clear();
            cm = new SqlCommand("SELECT categoryName FROM tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                category.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure!", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbProduct(productName,quantity,price,category)VALUES(@productName,@quantity,@price,@category)", con);
                    cm.Parameters.AddWithValue("@productName", productName.Text);
                    cm.Parameters.AddWithValue("@quantity", Convert.ToInt16(quantity.Text));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt16(price.Text));
                    cm.Parameters.AddWithValue("@category", category.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product Successfully Saved");
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
            productName.Clear();
            quantity.Clear();
            price.Clear();            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure!", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbProduct SET productName=@productName, quantity=@quantity, price=@price, category=@category WHERE productId LIKE '" + productId.Text + "'", con);
                    cm.Parameters.AddWithValue("@productName", productName.Text);
                    cm.Parameters.AddWithValue("@quantity", Convert.ToInt16(quantity.Text));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt16(price.Text));
                    cm.Parameters.AddWithValue("@category", category.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product Successfully Updated");
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
