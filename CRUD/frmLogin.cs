using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CRUD
{
    public partial class frmLogin : Form
    {
        classCRUDTools crudTools = new classCRUDTools();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(""+txtPass.Text.GetHashCode ()); to encrypt
            OleDbDataReader reader = null;
            string sql = "SELECT UserName, Password FROM Accounts WHERE UserName= '" + txtUser.Text + "' AND Password= '" + txtPass.Text+ "'";
            reader = crudTools.RetrieveRecords(sql, ref reader);
            if (reader.HasRows)
            {
                frmMain frm = new frmMain();
                frm.Show();
                this.Hide();
            }
            else{
                MessageBox.Show ("Invalid User Name or Password");
            }
             crudTools.CloseConnection();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
                
        }
    }

