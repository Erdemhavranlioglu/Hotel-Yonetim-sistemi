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
    public partial class Form4 : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source = DESKTOP-IL5URS6\\SQLEXPRESS; Initial Catalog = hotelproje; Integrated Security = True");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public Form4()
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
            komut = new SqlCommand("select * from hatirlatici", baglantı);
            adp = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            adp.Fill(ds, "hatirlatici");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "hatirlatici";
            dataGridView1.Columns[0].HeaderText = "Çalışan İD";
            dataGridView1.Columns[1].HeaderText = "Çalışan Durumu";
            dataGridView1.Columns[2].HeaderText = "Hatırlatıcı Detayı";
            dataGridView1.Columns[3].HeaderText = "Maaş Günü";
            dataGridView1.Columns[4].HeaderText = "Durumu";
            

        }
        private void Form4_Load(object sender, EventArgs e)
        {
            if (baglantı.State == ConnectionState.Closed) baglantı.Open();
            komut = new SqlCommand("select * from hatirlatici", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())

            {
                textBox1.Text = okuyucu["calisanid"].ToString();
                comboBox1.Text = okuyucu["calisandurum"].ToString();
                comboBox3.Text = okuyucu["hatirlaticidetay"].ToString();
                dateTimePicker1.Text = okuyucu["maasgunu"].ToString();
                comboBox2.Text = okuyucu["status"].ToString();
                

                




            }

            textBox1.ReadOnly = false;
            
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select * from hatirlatici", baglantı);
            adp.Fill(ds, "hatirlatici");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "hatirlatici";
            baglantı.Close();
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("insert into hatirlatici(calisanid,calisandurum,hatirlaticidetay,maasgunu,status) values (@calid,@dur,@det,@gun,@stat)", baglantı);
                komut.Parameters.AddWithValue("@calid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@dur", comboBox1.Text);
                komut.Parameters.AddWithValue("@det", comboBox3.Text);
                komut.Parameters.AddWithValue("@gun", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@stat", comboBox2.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();
                
                MessageBox.Show("Çalışan Başarıyla Kaydedildi");

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
                komut = new SqlCommand("Delete from hatirlatici where calisanid='" + textBox1.Text + "'", baglantı);
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
                komut = new SqlCommand("update hatirlatici set calisandurum=@dur,hatirlaticidetay=@det,maasgunu=@gun,status=@stat" + " where calisanid=@calid ", baglantı);
                komut.Parameters.AddWithValue("@calid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@dur", comboBox1.Text);
                komut.Parameters.AddWithValue("@det", comboBox3.Text);
                komut.Parameters.AddWithValue("@gun", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@stat", comboBox2.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();

                MessageBox.Show("Çalışan Başarıyla Güncellendi");

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
            komut = new SqlCommand("select * from hatirlatici where calisanid=@calid", baglantı);
            komut.Parameters.AddWithValue("@calid", textBox1.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "hatirlatici");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                int x = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();             
                comboBox1.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();


            }
            else
            {
                MessageBox.Show("Yanlış yere tıkladınız");
            }
            baglantı.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
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
