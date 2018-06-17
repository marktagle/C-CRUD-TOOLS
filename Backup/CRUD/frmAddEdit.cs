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
    public partial class frmAddEdit : Form
    {
        classCRUDTools crudTools = new classCRUDTools();
        public frmAddEdit()
        {
            InitializeComponent();
            FillProgram();
            txtSNO.Enabled = true; 
        }

        public frmAddEdit(params string[] studentData) {
            InitializeComponent();
            FillProgram();

            txtSNO.Text = studentData[0].ToString();
            txtLName.Text = studentData[1].ToString();
            txtFName.Text = studentData[2].ToString();
            txtMI.Text = studentData[3].ToString();
            cboProgram.Text = studentData[4].ToString();

            txtSNO.Enabled = false;
        }

        private void frmAddEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if(txtLName.Text.Trim() ==""){
                errorProvider1.SetError(txtLName , "Please enter your Last Name");
                txtLName.Focus();
                return;
            }

            if (txtFName.Text.Trim() == "")
            {
                errorProvider1.SetError(txtFName, "Please enter your FirstName");
                txtFName.Focus();
                return;
            }

            string sql = "";
            if (this.Text == "Add") {
                sql = string.Format(@"INSERT INTO Students
                                        VALUES('{0}','{1}','{2}','{3}','{4}')",
                                        txtSNO.Text.Trim(),
                                        txtLName.Text.Trim(),
                                        txtFName.Text.Trim(),
                                        txtMI.Text.Trim(),
                                        cboProgram.Text);
            }

            else {
                sql = string.Format(@"UPDATE Students
                                        SET LastName = '{0}', FirstName = '{1}', MI='{2}', Program = '{3}'
                                        WHERE SNO = '{4}'",
                                       
                                        txtLName.Text.Trim(),
                                        txtFName.Text.Trim(),
                                        txtMI.Text.Trim(),
                                        cboProgram.Text,
                                        txtSNO.Text.Trim());
                
            }
             
            crudTools.ExecuteQuery(sql);
            MessageBox.Show("Records Successfully Updated");
            frmMain frm = new frmMain();
            frm.Show();
            this.Hide();
        }

        private void FillProgram() {
              OleDbDataReader reader = null;
            string sql ="SELECT ProgramCode FROM Programs";
            reader = crudTools.RetrieveRecords(sql, ref reader);
            if(reader.HasRows ){
                while(reader.Read ()){
                    cboProgram.Items.Add(reader["ProgramCode"]);
                }
            }
            crudTools.CloseConnection();        
        }

        private void txtLName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz- ";
            if(validKeys .IndexOf (e.KeyChar)<0 && !char.IsControl (e.KeyChar)){
                e.Handled = true;
            }
        }

        private void txtFName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZzÑñ- ";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMI_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZzÑñ-. ";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtLName_TextChanged(object sender, EventArgs e)
        {}

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {}

        private void frmAddEdit_Load(object sender, EventArgs e)
        {}
    }
}
