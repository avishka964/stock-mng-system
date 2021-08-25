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
    public partial class Users : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UIBGCCB\SQLEXPRESS;Initial Catalog=stockdb;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Users()
        {
            InitializeComponent();
            GetUsers();
        }


        public void GetUsers()
        {
            int i = 0;
            dataGridUser.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbUser", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridUser.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();
        }
        
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            UserModule userModuleForm = new UserModule();
            userModuleForm.btnCreate.Enabled = true;
            userModuleForm.btnUpdate.Enabled = false;
            userModuleForm.ShowDialog();
            GetUsers();
        }

        private void dataGridUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridUser.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                UserModule userModuleForm = new UserModule();
                userModuleForm.userName.Text = dataGridUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModuleForm.fullName.Text = dataGridUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModuleForm.password.Text = dataGridUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModuleForm.mobileNo.Text = dataGridUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModuleForm.btnCreate.Enabled = false;
                userModuleForm.btnUpdate.Enabled = true;
                userModuleForm.userName.Enabled = false;
                userModuleForm.ShowDialog();
            }else if(colName == "Delete")
            {
                if (MessageBox.Show("Are You Sure", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbUser WHERE userName LIKE'" + dataGridUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User Successfully Deleted");
                }
            }
            GetUsers();
        }
    }
}
