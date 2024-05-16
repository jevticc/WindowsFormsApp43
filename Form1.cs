using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp43
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.a2lConnectionString);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'a2DataSet.Autor' table. You can move, or remove it, as needed.
            this.autorTableAdapter.Fill(this.a2lDataSet.Autor);
            kopiraj(this.a2lDataSet.Autor, listView1);

        }
        public void kopiraj(DataTable dt, ListView LV)
        {
            LV.View = View.Details;
            LV.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                LV.Columns.Add(col.ColumnName);
            }
            LV.Items.Clear();
            LV.FullRowSelect = true;
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < dt.Columns.Count; i++)
                    item.SubItems.Add(row[i].ToString());
                LV.Items.Add(item);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string AutorID = listView1.SelectedItems[0].Text;
                textBox1.Text = AutorID;
                SqlCommand cm = new SqlCommand("select * from Autor where AutorID=@au ", con);
                cm.Parameters.AddWithValue("au", AutorID);
                SqlDataReader r = cm.ExecuteReader();
                while (r.Read())
                {
                    textBox2.Text = r[1].ToString();
                    textBox3.Text = r[2].ToString();
                    textBox4.Text = r[3].ToString();
                }
                r.Close();
            }
            catch
            {
            }
            con.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cm = new SqlCommand("delete from Autor where AutorID=@au ", con);
                cm.Parameters.AddWithValue("au", int.Parse(textBox1.Text));
                cm.ExecuteNonQuery();
                this.autorTableAdapter.Fill(this.a2lDataSet.Autor);
                listView1.Items.Clear();
                kopiraj(this.a2lDataSet.Autor, listView1);
                MessageBox.Show("uspesno obrisano");
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            con.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }
    }
}
