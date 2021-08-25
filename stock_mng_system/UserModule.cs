﻿using System;
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
    public partial class UserModule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UIBGCCB\SQLEXPRESS;Initial Catalog=stockdb;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        public UserModule()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are You Sure!", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbUser(userName,fullName,password,mobileNo)VALUES(@userName,@fullName,@password,@mobileNo)", con);
                    cm.Parameters.AddWithValue("@userName", userName.Text);
                    cm.Parameters.AddWithValue("@fullName", fullName.Text);
                    cm.Parameters.AddWithValue("@password", password.Text);
                    cm.Parameters.AddWithValue("@mobileNo", mobileNo.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User Successfully Saved");
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
            userName.Clear();
            fullName.Clear();
            password.Clear();
            mobileNo.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                if (MessageBox.Show("Are You Sure!", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbUser SET fullName=@fullName, password=@password, mobileNo=@mobileNo WHERE userName LIKE '"+userName.Text+"'", con);                  
                    cm.Parameters.AddWithValue("@fullName", fullName.Text);
                    cm.Parameters.AddWithValue("@password", password.Text);
                    cm.Parameters.AddWithValue("@mobileNo", mobileNo.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User Successfully Updated");
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
