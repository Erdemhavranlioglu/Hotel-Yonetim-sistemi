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
    public partial class Form6 : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source = DESKTOP-IL5URS6\\SQLEXPRESS; Initial Catalog = hotelproje; Integrated Security = True");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public Form6()
        {
            InitializeComponent();
        }

        private void geri_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }
        void verigoster()
        {
            komut = new SqlCommand("select * from musteri", baglantı);
            adp = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            adp.Fill(ds, "musteri");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "musteri";
            dataGridView1.Columns[0].HeaderText = "Müşteri İD";
            dataGridView1.Columns[1].HeaderText = "Müşteri İsmi";
            dataGridView1.Columns[2].HeaderText = "Yakın Telefon'u";
            dataGridView1.Columns[3].HeaderText = "Telefon Numarası";
            dataGridView1.Columns[4].HeaderText = "Cinsiyet";
            dataGridView1.Columns[5].HeaderText = "Doğum Tarihi";
            dataGridView1.Columns[6].HeaderText = "Ülke";
            dataGridView1.Columns[7].HeaderText = "Kimlik Numarası";
            dataGridView1.Columns[8].HeaderText = "Durumu";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("insert into musteri(musteriid,musteriismi,yakin_no,telno,cinsiyet,dogumtarih,ulke,kimlikno,status) values (@musid,@musis,@yakin,@no,@cins,@dogum,@ulke,@kimlik,@stat)", baglantı);
                komut.Parameters.AddWithValue("@musid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@musis", textBox2.Text);
                komut.Parameters.AddWithValue("@kimlik", textBox3.Text);
                komut.Parameters.AddWithValue("@ulke", textBox4.Text);
                komut.Parameters.AddWithValue("@yakin", textBox5.Text);
                komut.Parameters.AddWithValue("@no", textBox6.Text);
                komut.Parameters.AddWithValue("@dogum", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@cins", comboBox1.Text);                                              
                komut.Parameters.AddWithValue("@stat", comboBox2.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();

                MessageBox.Show("Müşteri Eklendi");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata var" + ex.Message);
                baglantı.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("update musteri set musteriismi=@musis,yakin_no=@yakin,telno=@no,cinsiyet=@cins,dogumtarih=@dogum,ulke=@ulke,kimlikno=@kimlik,status=@stat" + " where musteriid=@musid ", baglantı);
                komut.Parameters.AddWithValue("@musid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@musis", textBox2.Text);
                komut.Parameters.AddWithValue("@kimlik", textBox3.Text);
                komut.Parameters.AddWithValue("@ulke", textBox4.Text);
                komut.Parameters.AddWithValue("@yakin", textBox5.Text);
                komut.Parameters.AddWithValue("@no", textBox6.Text);
                komut.Parameters.AddWithValue("@dogum", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@cins", comboBox1.Text);
                komut.Parameters.AddWithValue("@stat", comboBox2.Text);
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

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                komut = new SqlCommand("Delete from musteri where musteriid='" + textBox1.Text + "'", baglantı);
                komut.ExecuteNonQuery();
                MessageBox.Show("Müşteri Silindi");
                baglantı.Close();
                verigoster();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglantı.Close();


            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            if (baglantı.State == ConnectionState.Closed) baglantı.Open();
            komut = new SqlCommand("select * from musteri", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                textBox1.Text = okuyucu["musteriid"].ToString();
                textBox2.Text = okuyucu["musteriismi"].ToString();
                textBox3.Text = okuyucu["kimlikno"].ToString();
                textBox4.Text = okuyucu["ulke"].ToString();
                textBox5.Text = okuyucu["yakin_no"].ToString();
                textBox6.Text = okuyucu["telno"].ToString();
                dateTimePicker1.Text = okuyucu["dogumtarih"].ToString();
                comboBox1.Text = okuyucu["cinsiyet"].ToString();
                comboBox2.Text = okuyucu["status"].ToString();







            }

            textBox1.ReadOnly = false;

            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select * from musteri", baglantı);
            adp.Fill(ds, "musteri");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "musteri";
            baglantı.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[x].Cells[7].Value.ToString();
            textBox4.Text = dataGridView1.Rows[x].Cells[6].Value.ToString();
            textBox5.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[x].Cells[4].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[x].Cells[8].Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand cmd = baglantı.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from musteri where musteriid='" + textBox8.Text + "'";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();



            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from musteri where musteriid=@musid", baglantı);
            komut.Parameters.AddWithValue("@musid", textBox8.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "musteri");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                textBox1.DataBindings.Clear(); textBox1.DataBindings.Add("text", ds, "musteri.musteriid");
                textBox2.DataBindings.Clear(); textBox2.DataBindings.Add("text", ds, "musteri.musteriismi");
                textBox3.DataBindings.Clear(); textBox3.DataBindings.Add("text", ds, "musteri.kimlikno");
                textBox4.DataBindings.Clear(); textBox4.DataBindings.Add("text", ds, "musteri.ulke");
                textBox5.DataBindings.Clear(); textBox5.DataBindings.Add("text", ds, "musteri.yakin_no");
                textBox6.DataBindings.Clear(); textBox6.DataBindings.Add("text", ds, "musteri.telno");
                comboBox1.DataBindings.Clear(); comboBox1.DataBindings.Add("text", ds, "musteri.cinsiyet");
                comboBox2.DataBindings.Clear(); comboBox2.DataBindings.Add("text", ds, "musteri.status");

                MessageBox.Show("Aranan Müşterinin Bilgileri Listelendi");
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
                comboBox1.Hide();
                comboBox2.Hide();
                MessageBox.Show("Bu İD Numarasına Ait kayıtlı fatura bulunmamaktadır");
            }
            baglantı.Close();
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

        private void button10_Click(object sender, EventArgs e)
        {
            
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
    }
}
