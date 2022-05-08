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

namespace hotelproje
{
    public partial class Form9 : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IL5URS6\\SQLEXPRESS;Initial Catalog=hotelproje;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand komut = new SqlCommand();



        public Form9()
        {
            InitializeComponent();
        }
        void verigoster()
        {
            komut = new SqlCommand("select * from rezarvasyon", baglantı);
            adp = new SqlDataAdapter(komut);
            adp = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            adp.Fill(ds, "rezarvasyon");
            dataGridView1.DataSource = ds.Tables["rezarvasyon"];
            
            //dataGridView1.Columns[0].HeaderText = "Rezarvasyon İD";
            //dataGridView1.Columns[1].HeaderText = "Oda Numarası";
            //dataGridView1.Columns[2].HeaderText = "Kimlik Numarası";
            //dataGridView1.Columns[3].HeaderText = "Rezarvasyon Tarihi";
            //dataGridView1.Columns[4].HeaderText = "Giriş Tarihi";
            //dataGridView1.Columns[5].HeaderText = "Çıkış Tarihi";
            //dataGridView1.Columns[6].HeaderText = "Durumu";



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            if (baglantı.State == ConnectionState.Closed) baglantı.Open();
            komut = new SqlCommand("select * from rezarvasyon", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                textBox1.Text = okuyucu["rezarvasyonid"].ToString();
                textBox2.Text = okuyucu["odano"].ToString();
                textBox3.Text = okuyucu["kimlikno"].ToString();
                dateTimePicker1.Text = okuyucu["rezarvasyontarih"].ToString();
                dateTimePicker2.Text = okuyucu["giristarih"].ToString();
                dateTimePicker3.Text = okuyucu["cikistarih"].ToString();
                textBox4.Text = okuyucu["status"].ToString();
            }
            textBox1.ReadOnly = false;
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select * from rezarvasyon", baglantı);
            adp.Fill(ds, "rezarvasyon");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "rezarvasyon";
            baglantı.Close();

        }

        private void Ekle_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("insert into rezarvasyon(rezarvasyonid,odano,kimlikno,rezarvasyontarih,giristarih,cikistarih,status) values (@reid,@ono,@kno,@retarih,@girtarih,@ciktarih,@stat)", baglantı);
                komut.Parameters.AddWithValue("@reid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@ono", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@kno", textBox3.Text);
                komut.Parameters.AddWithValue("@retarih", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@girtarih", Convert.ToDateTime(dateTimePicker2.Text));
                komut.Parameters.AddWithValue("@ciktarih", Convert.ToDateTime(dateTimePicker3.Text));
                komut.Parameters.AddWithValue("@stat", textBox4.Text);

                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();

                MessageBox.Show("Rezarvasyon Başarı İle Kaydedildi");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata var" + ex.Message);
                baglantı.Close();
            }
        }

        private void sil_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                komut = new SqlCommand("Delete from rezarvasyon where rezarvasyonid='" + textBox1.Text + "'", baglantı);
                komut.ExecuteNonQuery();
                MessageBox.Show("kayıt silindi");
                baglantı.Close();
                verigoster();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglantı.Close();


            }

        }

        private void guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("update rezarvasyon set odano=@ono, kimlikno=@kno,  rezarvasyontarih=@retarih,giristarih=@girtarih,cikistarih=@ciktarih,status=@stat " + " where rezarvasyonid=@reid ", baglantı);
                komut.Parameters.AddWithValue("@reid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@ono", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@kno", textBox3.Text);
                komut.Parameters.AddWithValue("@retarih", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@girtarih", Convert.ToDateTime(dateTimePicker2.Text));
                komut.Parameters.AddWithValue("@ciktarih", Convert.ToDateTime(dateTimePicker3.Text));
                komut.Parameters.AddWithValue("@stat", textBox4.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();
                MessageBox.Show("Rezarvasyon'a ait Bilgiler Başarıyla güncellendi");
                baglantı.Close();

                MessageBox.Show(textBox1.Text + " Rezarvasyon'a ait bilgiler güncellendi");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglantı.Close();


            }
        }

        private void ara_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand cmd = baglantı.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from rezarvasyon where rezarvasyonid='" + textBox5.Text + "'";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();

            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from rezarvasyon where rezarvasyonid=@reid", baglantı);
            komut.Parameters.AddWithValue("@reid", textBox5.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "rezarvasyon");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                textBox1.DataBindings.Clear(); textBox1.DataBindings.Add("text", ds, "rezarvasyon.rezarvasyonid");
                textBox2.DataBindings.Clear(); textBox2.DataBindings.Add("text", ds, "rezarvasyon.odano");
                textBox3.DataBindings.Clear(); textBox3.DataBindings.Add("text", ds, "rezarvasyon.kimlikno");
                dateTimePicker1.DataBindings.Clear(); dateTimePicker1.DataBindings.Add("text", ds, "rezarvasyon.rezarvasyontarih");
                dateTimePicker2.DataBindings.Clear(); dateTimePicker2.DataBindings.Add("text", ds, "rezarvasyon.giristarih");
                dateTimePicker3.DataBindings.Clear(); dateTimePicker3.DataBindings.Add("text", ds, "rezarvasyon.cikistarih");
                textBox4.DataBindings.Clear(); textBox4.DataBindings.Add("text", ds, "rezarvasyon.status");



                MessageBox.Show("Aranan Rezarvasyon'a Ait Bilgiler Listelendi");
                textBox1.Enabled = true;
                dataGridView1.Show();

            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();

                MessageBox.Show("Bu İD numarasına sahip Rezarvasyon kayıtlı değil");
            }

            baglantı.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from rezarvasyon where rezarvasyonid=@reid", baglantı);
            komut.Parameters.AddWithValue("@reid", textBox1.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "rezarvasyon");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                int x = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();             
                textBox3.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[x].Cells[6].Value.ToString();




            }
            else
            {
                MessageBox.Show("Yanlış yere tıkladınız");
            }
            baglantı.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int i, j, x, y;
            y = 30;
            for (j = 0; j <= dataGridView1.Rows.Count - 2; j++)
            {
                x = 30;
                for (i = 0; i <= 5; i++)
                {
                    e.Graphics.DrawString(dataGridView1.Rows[j].Cells[i].Value.ToString(), new Font("Times New Roman", 10), Brushes.Black, x, y);
                    x = x + 80;
                }
                y = y + 30;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void yazdırToolStripButton_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.Document = printDocument1;
            pageSetupDialog1.PageSettings = printDocument1.DefaultPageSettings;
            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.DefaultPageSettings = pageSetupDialog1.PageSettings;

            }
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
    }
}
