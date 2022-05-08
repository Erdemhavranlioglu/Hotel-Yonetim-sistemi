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
    public partial class Form7 : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IL5URS6\\SQLEXPRESS;Initial Catalog=hotelproje;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        DataTable tablo = new DataTable();
        public Form7()
        {
            InitializeComponent();
        }
        void verigoster()
        {
            komut = new SqlCommand("select * from oda", baglantı);
            adp = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            adp.Fill(ds, "oda");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "oda";
            dataGridView1.Columns[0].HeaderText = "Oda İD";
            dataGridView1.Columns[1].HeaderText = "Oda Türü";
            dataGridView1.Columns[2].HeaderText = "Oda Numarası";
            dataGridView1.Columns[3].HeaderText = "Açıklama";
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[5].HeaderText = "Tutar";
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from oda where odaid=@id", baglantı);
            komut.Parameters.AddWithValue("@id", textBox1.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "oda");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                int x = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[x].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.Rows[x].Cells[5].Value.ToString();
            }
            else
            {
                MessageBox.Show("Yanlış yere tıkladınız");
            }
            baglantı.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            if (baglantı.State == ConnectionState.Closed) baglantı.Open();
            komut = new SqlCommand("select * from oda", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if(okuyucu.Read())
            {
                textBox1.Text = okuyucu["odaid"].ToString();
                comboBox1.Text = okuyucu["odaturu"].ToString();
                textBox2.Text = okuyucu["odanumarasi"].ToString();
                textBox3.Text = okuyucu["tutar"].ToString();
                comboBox2.Text = okuyucu["status"].ToString();
                textBox6.Text = okuyucu["aciklama"].ToString();

            
            
            
            
            }
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select * from oda", baglantı);
            adp.Fill(ds, "oda");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "oda";
            baglantı.Close();

        }

        private void Ekle_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("insert into oda(odaid,odaturu,odanumarasi,aciklama,status,tutar) values (@id,@tur,@no,@acik,@stat,@tutar)", baglantı);
                komut.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@tur", comboBox1.Text);
                komut.Parameters.AddWithValue("@no", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@tutar", Convert.ToInt32(textBox3.Text));
                komut.Parameters.AddWithValue("@stat", comboBox2.Text);
                komut.Parameters.AddWithValue("@acik", textBox6.Text);
                
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();

                MessageBox.Show("Oda Başarı İle Kaydedildi");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata var" + ex.Message);
                baglantı.Close();
            }
        }

        private void Sil_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                komut = new SqlCommand("Delete from oda where odaid='" + textBox1.Text + "'", baglantı);
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
                komut = new SqlCommand("update oda set odaturu=@tur,odanumarasi=@no,aciklama=@acik,status=@stat,tutar=@tutar " + " where odaid=@id ", baglantı);
                komut.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@tur", comboBox1.Text);
                komut.Parameters.AddWithValue("@no", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@tutar", Convert.ToInt32(textBox3.Text));
                komut.Parameters.AddWithValue("@stat", comboBox2.Text);
                komut.Parameters.AddWithValue("@acik", textBox6.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();
                MessageBox.Show("Oda Başarıyla güncellendi");
                baglantı.Close();

                MessageBox.Show(textBox1.Text + " Oda ait bilgiler güncellendi");


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
            cmd.CommandText = "select * from oda where odaid='" + textBox4.Text + "'";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();

            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from oda where odaid=@id", baglantı);
            komut.Parameters.AddWithValue("@id", textBox4.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "oda");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                textBox1.DataBindings.Clear(); textBox1.DataBindings.Add("text", ds, "oda.odaid");
                comboBox1.DataBindings.Clear(); comboBox1.DataBindings.Add("text", ds, "oda.odaturu");
                textBox2.DataBindings.Clear(); textBox2.DataBindings.Add("text", ds, "oda.odanumarasi");
                textBox3.DataBindings.Clear(); textBox3.DataBindings.Add("text", ds, "oda.tutar");
                comboBox2.DataBindings.Clear(); comboBox2.DataBindings.Add("text", ds, "oda.status");
                textBox6.DataBindings.Clear(); textBox6.DataBindings.Add("text", ds, "oda.aciklama");



                MessageBox.Show("Aranan Oda'ya Ait Bilgiler Listelendi");
                textBox1.Enabled = true;
                dataGridView1.Show();

            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();                        
                textBox6.Clear();
                
                MessageBox.Show("Bu İD numarasına sahip Oda kayıtlı değil");
            }

            baglantı.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
