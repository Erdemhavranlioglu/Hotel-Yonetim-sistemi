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
    public partial class Form11 : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source = DESKTOP-IL5URS6\\SQLEXPRESS; Initial Catalog = hotelproje; Integrated Security = True");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public Form11()
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
            komut = new SqlCommand("select * from yemek", baglantı);
            adp = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            adp.Fill(ds, "yemek");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "yemek";
            dataGridView1.Columns[0].HeaderText = "Yemek İD";
            dataGridView1.Columns[1].HeaderText = "Yemek Çeşidi";
            dataGridView1.Columns[2].HeaderText = "Yemek İsmi";
            dataGridView1.Columns[3].HeaderText = "Yemek Tutarı";
            dataGridView1.Columns[4].HeaderText = "Durumu";
            dataGridView1.Columns[5].HeaderText = "Yemek Detayı";

        }

        private void Form11_Load(object sender, EventArgs e)
        {
            if (baglantı.State == ConnectionState.Closed) baglantı.Open();
            komut = new SqlCommand("select * from yemek", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                textBox1.Text = okuyucu["yemekid"].ToString();
                comboBox1.Text = okuyucu["yemektip"].ToString();
                textBox2.Text = okuyucu["yemekismi"].ToString();
                textBox3.Text = okuyucu["yemektutari"].ToString();
                comboBox2.Text = okuyucu["yemekdetay"].ToString();
                textBox4.Text = okuyucu["status"].ToString();


            }
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select * from yemek", baglantı);
            adp.Fill(ds, "yemek");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "yemek";
            baglantı.Close();

        }

        private void ekle_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("insert into yemek(yemekid,yemektip,yemekismi,yemektutari,yemekdetay,status) values (@id,@tip,@ismi,@tutar,@detay,@stat)", baglantı);
                komut.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@tip", comboBox1.Text);
                komut.Parameters.AddWithValue("@ismi", textBox2.Text);
                komut.Parameters.AddWithValue("@tutar", textBox3.Text);
                komut.Parameters.AddWithValue("@detay", comboBox2.Text);
                komut.Parameters.AddWithValue("@stat", textBox4.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();
                MessageBox.Show("Yemek Kaydedildi");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata Var" + ex.Message);
                baglantı.Close();
            }
        }

        private void sil_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                komut = new SqlCommand("Delete from yemek where yemekid='" + textBox1.Text + "'", baglantı);
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
                komut = new SqlCommand("update yemek set yemektip=@tip,yemekismi=@ismi,yemektutari=@tutar,yemekdetay=@detay,status=@stat" + " where yemekid=@id ", baglantı);
                komut.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@tip", comboBox1.Text);
                komut.Parameters.AddWithValue("@ismi", textBox2.Text);
                komut.Parameters.AddWithValue("@tutar", textBox3.Text);
                komut.Parameters.AddWithValue("@detay", comboBox2.Text);
                komut.Parameters.AddWithValue("@stat", textBox4.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();

                MessageBox.Show("Sipariş Başarıyla Güncellendi");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata var" + ex.Message);
                baglantı.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from yemek where yemekid=@id", baglantı);
            komut.Parameters.AddWithValue("@id", textBox1.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "yemek");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                int x = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[x].Cells[4].Value.ToString();
                textBox4.Text= dataGridView1.Rows[x].Cells[5].Value.ToString();




            }
            else
            {
                MessageBox.Show("Yanlış yere tıkladınız");
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
    }
}
