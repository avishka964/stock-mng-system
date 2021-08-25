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
    public partial class Categories : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UIBGCCB\SQLEXPRESS;Initial Catalog=stockdb;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Categories()
        {
            InitializeComponent();
            GetCategories();
        }

        public void GetCategories()
        {
            int i = 0;
            dataGridCategory.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            CategoryModule categoryModuleForm = new CategoryModule();
            categoryModuleForm.btnCreate.Enabled = true;
            categoryModuleForm.btnUpdate.Enabled = false;
            categoryModuleForm.ShowDialog();
            GetCategories();
        }

        private void dataGridCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryModule categoryModuleForm = new CategoryModule();
                categoryModuleForm.categoryId.Text = dataGridCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                categoryModuleForm.categoryName.Text = dataGridCategory.Rows[e.RowIndex].Cells[2].Value.ToString();

                categoryModuleForm.btnCreate.Enabled = false;
                categoryModuleForm.btnUpdate.Enabled = true;
                categoryModuleForm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are You Sure", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbCategory WHERE categoryId LIKE'" + dataGridCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Category Successfully Deleted");
                }
            }
            GetCategories();
        }
    }
}
