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
    public partial class Form8 : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IL5URS6\\SQLEXPRESS;Initial Catalog=hotelproje;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();


        public Form8()
        {
            InitializeComponent();
        }


        void verigoster()
        {
            komut = new SqlCommand("select * from odeme", baglantı);
            adp = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            adp.Fill(ds, "odeme");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "odeme";
            dataGridView1.Columns[0].HeaderText = "Ödeme İD";
            dataGridView1.Columns[1].HeaderText = "Rezarvasyon İD";
            dataGridView1.Columns[2].HeaderText = "Tutar";
            dataGridView1.Columns[3].HeaderText = "Ödeme Şekli";
            dataGridView1.Columns[4].HeaderText = "Ödeme Tarihi";
            dataGridView1.Columns[5].HeaderText = "Durumu";
            dataGridView1.Columns[6].HeaderText = "Ödeme Detayı";
           


        }





        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            if (baglantı.State == ConnectionState.Closed) baglantı.Open();
            komut = new SqlCommand("select * from odeme", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                textBox1.Text = okuyucu["odemeid"].ToString();
                textBox2.Text = okuyucu["rezarvasyonid"].ToString();
                textBox3.Text = okuyucu["tutar"].ToString();
                comboBox1.Text = okuyucu["odemeturu"].ToString();
                dateTimePicker1.Text = okuyucu["odemetarihi"].ToString();
                comboBox2.Text = okuyucu["status"].ToString();
                textBox4.Text = okuyucu["odemedetay"].ToString();


            }
            textBox1.ReadOnly = false;
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select * from odeme",baglantı);
            adp.Fill(ds,"odeme");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "odeme";
            baglantı.Close(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("insert into odeme(odemeid,rezarvasyonid,tutar,odemeturu,odemedetay,odemetarihi,status) values (@odid,@reid,@tutar,@tur,@detay,@tarih,@stat)", baglantı);
                komut.Parameters.AddWithValue("@odid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@reid", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@tutar", Convert.ToInt32(textBox3.Text));
                komut.Parameters.AddWithValue("@tur",comboBox1.Text);
                komut.Parameters.AddWithValue("@tarih", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@stat", comboBox2.Text);
                komut.Parameters.AddWithValue("@detay", textBox4.Text);

                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();

                MessageBox.Show("Fatura Başarı İle Kaydedildi");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata var" + ex.Message);
                baglantı.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                komut = new SqlCommand("Delete from odeme where odemeid='" + textBox1.Text + "'", baglantı);
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("update odeme set rezarvasyonid=@reid,tutar=@tutar,odemeturu=@tur,odemedetay=@detay,odemetarihi=@tarih,status=@stat " + " where odemeid=@odid ", baglantı);
                komut.Parameters.AddWithValue("@odid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@reid", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@tutar", Convert.ToInt32(textBox3.Text));
                komut.Parameters.AddWithValue("@tur", comboBox1.Text);
                komut.Parameters.AddWithValue("@tarih", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@stat", comboBox2.Text);
                komut.Parameters.AddWithValue("@detay", textBox4.Text); ;
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();
                MessageBox.Show("Fatura Başarıyla güncellendi");
                baglantı.Close();

                MessageBox.Show(textBox1.Text + " Fatura'ya ait bilgiler güncellendi");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglantı.Close();


            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from odeme where odemeid=@odid", baglantı);
            komut.Parameters.AddWithValue("@odid", textBox1.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "odeme");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                int x = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();                
                comboBox2.Text = dataGridView1.Rows[x].Cells[5].Value.ToString();
                textBox4.Text = dataGridView1.Rows[x].Cells[6].Value.ToString();

            }
            else
            {
                MessageBox.Show("Yanlış yere tıkladınız");
            }
            baglantı.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand cmd = baglantı.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from odeme where odemeid='" + textBox5.Text + "'";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();

            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from odeme where odemeid=@odid", baglantı);
            komut.Parameters.AddWithValue("@odid", textBox5.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "odeme");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                textBox1.DataBindings.Clear(); textBox1.DataBindings.Add("text", ds, "odeme.odemeid");
                textBox2.DataBindings.Clear(); textBox2.DataBindings.Add("text", ds, "odeme.rezarvasyonid");
                textBox3.DataBindings.Clear(); textBox3.DataBindings.Add("text", ds, "odeme.tutar");
                comboBox1.DataBindings.Clear(); comboBox1.DataBindings.Add("text", ds, "odeme.odemeturu");            
                comboBox2.DataBindings.Clear(); comboBox2.DataBindings.Add("text", ds, "odeme.status");
                textBox4.DataBindings.Clear(); textBox4.DataBindings.Add("text", ds, "odeme.odemedetay");



                MessageBox.Show("Aranan Fatura'ya Ait Bilgiler Listelendi");
                textBox1.Enabled = true;
                dataGridView1.Show();

            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                
                textBox4.Clear();

                MessageBox.Show("Bu İD numarasına sahip Fatura kayıtlı değil");
            }

            baglantı.Close();
        }

        private void button6_Click(object sender, EventArgs e)
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
