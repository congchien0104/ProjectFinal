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
using System.Data.Linq;
using System.IO;

namespace _18147171_QLNV
{
    public partial class Form1 : Form
    {
        qlnv1 ql = new qlnv1();
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'oPPDataSet.qlnv1' table. You can move, or remove it, as needed.
            //this.qlnv1TableAdapter.Fill(this.oPPDataSet.qlnv1);
            var list = dataGridView1.DataSource = db.qlnv1s.Select(s => s).ToList();
            dataGridView1.Refresh();

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            qlnv1 ql = new qlnv1();
            try
            {
                ql.emID = txtemID.Text;
                ql.Name = txtName.Text;
                ql.Dept = txtDept.Text;
                db.qlnv1s.InsertOnSubmit(ql);
                db.SubmitChanges();
                Form1_Load(sender, e);
            }
            catch(DuplicateKeyException ie)
            {
                MessageBox.Show(ie.Message, "Thong Bao", MessageBoxButtons.OK);
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.Message, "Thong Bao", MessageBoxButtons.OK);
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            //qlnv1 ql = new qlnv1();
            try
            {
                ql = db.qlnv1s.Where(p => p.emID == txtemID.Text).Single();
                ql.Name = txtName.Text;
                ql.Dept = txtDept.Text;
                db.SubmitChanges();
                Form1_Load(sender, e);
            }
            catch(InvalidOperationException ne)
            {
                MessageBox.Show(ne.Message, "Thong Bao", MessageBoxButtons.OK);
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            //qlnv1 ql = new qlnv1();
            try
            {
                ql = db.qlnv1s.Where(p => p.emID == txtemID.Text).Single();
                ql.Name = txtName.Text;
                ql.Dept = txtDept.Text;
                db.qlnv1s.DeleteOnSubmit(ql);
                db.SubmitChanges();
                Form1_Load(sender, e);
            }
            catch (InvalidOperationException ne)
            {
                MessageBox.Show(ne.Message, "Thong Bao", MessageBoxButtons.OK);
            }
        }

        private void txtFind_KeyUp(object sender, KeyEventArgs e)
        {
            var list = dataGridView1.DataSource = (from s in db.qlnv1s where s.emID.Contains(txtFind.Text) select s).ToList();
            txtemID.DataBindings.Clear();
            txtName.DataBindings.Clear();
            txtDept.DataBindings.Clear();
            txtemID.DataBindings.Add("text", list, "emID");
            txtName.DataBindings.Add("text", list, "Name");
            txtDept.DataBindings.Add("text", list, "Dept");
        }

        private void btLuuFile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = @"C:\Users\User\source\repos\18147171_QLNV\congchien.txt";
                var list_yr = (from s in db.qlnv1s /*where s.DOB.value >= dTP02*/ select s).ToList();
                dataGridView1.DataSource = list_yr;

                TextWriter wrt = new StreamWriter(path);
                wrt.WriteLine("  ID Number \t\t\t      Name \t\t\t\t     Deparment");
                for(int i=0; i < dataGridView1.Rows.Count; i++)
                {
                    for(int j=0;j <dataGridView1.Rows.Count; j++)
                    {
                        wrt.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString()+"|");
                    }
                    wrt.WriteLine("");
                    wrt.WriteLine("=================================================================================================");
                }
                wrt.Close();
                MessageBox.Show("Xem ket qua", "Thong Bao", MessageBoxButtons.OK);
            }
            catch(FileLoadException fe)
            {
                MessageBox.Show(fe.Message, "Thong Bao", MessageBoxButtons.OK);
            }
            catch(ArgumentOutOfRangeException ae)
            {
                MessageBox.Show(ae.Message, "Thong bao", MessageBoxButtons.OK);
            }
        }
    }
}
