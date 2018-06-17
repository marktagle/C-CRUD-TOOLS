using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace CRUD
{
    class classCRUDTools
    {
        string cnString = "";
        OleDbConnection cn;
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter adptr = new OleDbDataAdapter();
        DataSet ds = new DataSet();

        public classCRUDTools() {
            cnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= MyDB.accdb";
            cn = new OleDbConnection(cnString );
        }

        public void FillDataGrid(string sql, ref DataGridView dg) {
            try
            {
                cn.Open();
                cmd = new OleDbCommand(sql,cn);
                adptr = new OleDbDataAdapter(cmd);

                ds = new DataSet();
                adptr.Fill(ds);
                dg.DataSource = "";
                dg.DataSource = ds.Tables [0];
                dg.AutoResizeColumns();

            }
            catch (Exception e){
                MessageBox.Show(""+ e.Message);
            }
            cn.Close();
        }

        //call it everytime you have a query
        public void ExecuteQuery(string sql) {
            try {
                cn.Open();
                cmd = new OleDbCommand(sql, cn);
                cmd.ExecuteNonQuery(); //executes sql
            }
            catch(Exception e){
                MessageBox.Show(""+e.Message);
            }
            cn.Close();
        }

        public OleDbDataReader RetrieveRecords(string sql, ref OleDbDataReader reader) {
            try {
                cn.Open();
                cmd = new OleDbCommand(sql, cn);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception e){
                MessageBox.Show(""+e.Message );
                return null;
            }
        }

        public void CloseConnection() {
            cn.Close();
        }

    }
}
