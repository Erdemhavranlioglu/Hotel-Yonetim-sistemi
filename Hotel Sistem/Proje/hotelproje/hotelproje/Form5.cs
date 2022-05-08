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
    public partial class Form5 : Form
    {
        
        SqlConnection baglantı = new SqlConnection("Data Source = DESKTOP-IL5URS6\\SQLEXPRESS; Initial Catalog = hotelproje; Integrated Security = True");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }
        void verigoster()
        {
            komut = new SqlCommand("select * from gider", baglantı);
            adp = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            adp.Fill(ds, "gider");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "gider";
            dataGridView1.Columns[0].HeaderText = "Gider İD";
            dataGridView1.Columns[1].HeaderText = "Çalışan Gideri";
            dataGridView1.Columns[2].HeaderText = "Elektrik";
            dataGridView1.Columns[3].HeaderText = "Su";
            dataGridView1.Columns[4].HeaderText = "DoğalGaz";
            dataGridView1.Columns[5].HeaderText = "Fatura Tarihi";
            dataGridView1.Columns[6].HeaderText = "Bar Ücretleri";


        }
        private void Form5_Load(object sender, EventArgs e)
        {
            if (baglantı.State == ConnectionState.Closed) baglantı.Open();
            komut = new SqlCommand("select * from gider", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                textBox1.Text = okuyucu["giderid"].ToString();
                textBox2.Text = okuyucu["calisan_gider"].ToString();
                textBox3.Text = okuyucu["elektrik"].ToString();
                textBox4.Text = okuyucu["su"].ToString();
                textBox5.Text = okuyucu["dogalgaz"].ToString();
                textBox6.Text = okuyucu["bar"].ToString();
                dateTimePicker1.Text = okuyucu["faturatarih"].ToString();
                textBox10.Text = okuyucu["otelmalzeme"].ToString();             
                






            }

            textBox1.ReadOnly = false;

            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select * from gider", baglantı);
            adp.Fill(ds, "gider");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "gider";
            baglantı.Close();
        }

        private void ekle_Click(object sender, EventArgs e)
        {
             try
            {
                baglantı.Open();
                komut = new SqlCommand("insert into gider(giderid,calisan_gider,elektrik,su,dogalgaz,faturatarih,bar,otelmalzeme) values (@gidid,@calisan,@elek,@su,@gaz,@fatura,@bar,@malz)", baglantı);
                komut.Parameters.AddWithValue("@gidid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@calisan", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@elek", Convert.ToInt32(textBox3.Text));
                komut.Parameters.AddWithValue("@su", Convert.ToInt32(textBox4.Text));
                komut.Parameters.AddWithValue("@gaz", Convert.ToInt32(textBox5.Text));
                komut.Parameters.AddWithValue("@fatura", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@bar", Convert.ToInt32(textBox6.Text));
                komut.Parameters.AddWithValue("@malz", Convert.ToInt32(textBox10.Text));
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();
                
                MessageBox.Show("Aylık Otel Gideri Eklendi");

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
                komut = new SqlCommand("Delete from gider where giderid='" + textBox1.Text + "'", baglantı);
                komut.ExecuteNonQuery();
                MessageBox.Show("kayıt Ödendi");
                baglantı.Close();
                verigoster();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglantı.Close();


            }
        }

        private void yazdir_Click(object sender, EventArgs e)
        {
            
        }

        private void guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("update gider set calisan_gider=@calisan,elektrik=@elek,su=@su,dogalgaz=@gaz,faturatarih=@fatura,bar=@bar,otelmalzeme=@malz" + " where giderid=@gidid ", baglantı);
                komut.Parameters.AddWithValue("@gidid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@calisan", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@elek", Convert.ToInt32(textBox3.Text));
                komut.Parameters.AddWithValue("@su", Convert.ToInt32(textBox4.Text));
                komut.Parameters.AddWithValue("@gaz", Convert.ToInt32(textBox5.Text));
                komut.Parameters.AddWithValue("@fatura", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@bar", Convert.ToInt32(textBox6.Text));
                komut.Parameters.AddWithValue("@malz", Convert.ToInt32(textBox10.Text));
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();

                MessageBox.Show("Kayıt Başarıyla Güncellendi");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata var" + ex.Message);
                baglantı.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[x].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[x].Cells[6].Value.ToString();
            
            textBox10.Text = dataGridView1.Rows[x].Cells[7].Value.ToString();



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

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void ara_Click(object sender, EventArgs e)
        {

            baglantı.Open();
            SqlCommand cmd = baglantı.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from gider where giderid='" + textBox8.Text + "'";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();

















            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from gider where giderid=@gidid", baglantı);
            komut.Parameters.AddWithValue("@gidid", textBox8.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "gider");
            SqlDataReader oku = komut.ExecuteReader();
            
            











            
            if (oku.Read())
            {
    
                textBox1.DataBindings.Clear(); textBox1.DataBindings.Add("text", ds, "gider.giderid");
                textBox2.DataBindings.Clear(); textBox2.DataBindings.Add("text", ds, "gider.calisan_gider");
                textBox3.DataBindings.Clear(); textBox3.DataBindings.Add("text", ds, "gider.elektrik");
                textBox4.DataBindings.Clear(); textBox4.DataBindings.Add("text", ds, "gider.su");           
                textBox5.DataBindings.Clear(); textBox5.DataBindings.Add("text", ds, "gider.dogalgaz");
                textBox6.DataBindings.Clear(); textBox6.DataBindings.Add("text", ds, "gider.bar");
                dateTimePicker1.DataBindings.Clear();dateTimePicker1.DataBindings.Add("text", ds, "gider.faturatarih");
                textBox10.DataBindings.Clear(); textBox10.DataBindings.Add("text", ds, "gider.otelmalzeme");

                MessageBox.Show("Aranan Fiyat Bilgileri Listelendi");
                textBox1.Enabled = true;
                dataGridView1.Show();

            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox10.Clear();
                MessageBox.Show("Bu İD Numarasına Ait kayıtlı fatura bulunmamaktadır");
            }

            baglantı.Close();
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

        private void Toplam_Click(object sender, EventArgs e)
        {
            int s1, s2, s3, s4, s5, s6, s10;
                   s1 = Convert.ToInt32(textBox1.Text);
                   s2 = Convert.ToInt32(textBox2.Text);
                   s3 = Convert.ToInt32(textBox3.Text);
                   s4 = Convert.ToInt32(textBox4.Text);
                   s5 = Convert.ToInt32(textBox5.Text);
                   s6 = Convert.ToInt32(textBox6.Text);
                   s10 = Convert.ToInt32(textBox10.Text);
            int sonuc = s1 + s2 + s3 + s4 + s5 + s6 + s10;
            label9.Text = sonuc.ToString();


        }
    }
}
