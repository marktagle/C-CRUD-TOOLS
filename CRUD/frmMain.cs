using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRUD
{
    public partial class frmMain : Form
    {
        classCRUDTools crudTools = new classCRUDTools();
        public frmMain()
        {
            InitializeComponent();
        }

        
        private void frmMain_Load(object sender, EventArgs e)
        {
            crudTools.FillDataGrid("SELECT * FROM Students ORDER BY LastName", ref dataGridView1);
            cboSearch.SelectedIndex = 1;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string sql="";
            switch(cboSearch.SelectedIndex){
                case 0:
                    sql = "SELECT * FROM Students WHERE SNO LIKE '%" + txtSearch.Text + "%'";
                    break;
                case 1:
                    sql = "SELECT * FROM Students WHERE LastName LIKE '%" + txtSearch.Text + "%'";
                    break;
                case 2:
                    sql = "SELECT * FROM Students WHERE FirstName LIKE '%" + txtSearch.Text + "%'";
                    break;

            }
            crudTools.FillDataGrid(sql, ref dataGridView1);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmAddEdit frm = new frmAddEdit();
            frm.Text = "Add";
            frm.Show();
            this.Hide();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {          
            frmAddEdit frm = new frmAddEdit(
                dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                dataGridView1.CurrentRow.Cells[3].Value.ToString(),
                dataGridView1.CurrentRow.Cells[4].Value.ToString()
                );
            
            frm.Text = "Update";
            frm.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                "Delete",MessageBoxButtons.YesNo , MessageBoxIcon.Question);
            if(result == DialogResult.Yes ){
                string sql = string.Format(@"DELETE FROM Students WHERE SNO = '{0}'",
                    dataGridView1.CurrentRow.Cells[0].Value.ToString ());
                crudTools.ExecuteQuery(sql);
                crudTools.FillDataGrid("SELECT * FROM Students", ref dataGridView1);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(sender, e);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            frm.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }




       
    }
}
