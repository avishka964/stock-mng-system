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
    public partial class Sales : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UIBGCCB\SQLEXPRESS;Initial Catalog=stockdb;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Sales()
        {
            InitializeComponent();
            GetOrders();
        }

        public void GetOrders()
        {
            int i = 0;
            dataGridSales.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbSales", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridSales.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnMngOrder_Click(object sender, EventArgs e)
        {
            SalesModule salesModuleForm = new SalesModule();
            salesModuleForm.btnCreate.Enabled = true;            
            salesModuleForm.ShowDialog();
            GetOrders();
           
        }

    }
}
