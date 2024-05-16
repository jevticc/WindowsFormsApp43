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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.a2lConnectionString);
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'a2DataSet.Kombo1' table. You can move, or remove it, as needed.
            this.kombo1TableAdapter.Fill(this.a2lDataSet.Kombo1);

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Morate da odaberete autora");
                }
                else
                {
                    con.Open();
                    SqlCommand cm = new SqlCommand("select year(DatumUzimanja) as Godina,count(*) as Broj  from Na_Citanju n, Knjiga k, Napisali na where n.KnjigaID=k.KnjigaID and k.KnjigaID=na.KnjigaID and AutorID=@au and year(getdate())-year(DatumUzimanja)<@gd group by year(DatumUzimanja)", con);
                    cm.Parameters.AddWithValue("au", (int)comboBox1.SelectedValue);
                    cm.Parameters.AddWithValue("gd", (int)numericUpDown1.Value);
                    SqlDataAdapter ad = new SqlDataAdapter(cm);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                  Crtaj();
                }

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            con.Close();
        }
        private void Crtaj()
        {
            try
            {
                chart1.Series[0].Points.Clear();
                int n = dataGridView1.RowCount-1;
                int[] brojevi = new int[n];
                int[] godine = new int[n];

                chart1.Series[0].LegendText = "Broj Knjiga";
                chart1.Series[0].Color = Color.Red;
                for (int i = 0; i < n; i++)
                {
                    brojevi[i] = (int)dataGridView1.Rows[i].Cells[1].Value;
                    godine[i] = (int)dataGridView1.Rows[i].Cells[0].Value;
                    chart1.Series[0].Points.Add(brojevi[i]);
                    chart1.Series[0].Points[i].LegendText = godine[i].ToString();
                    chart1.Series[0].Points[i].AxisLabel = godine[i].ToString();
                }

            }
            catch
            {
            }
        }
    }
}
