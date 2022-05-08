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
    public partial class Form10 : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source = DESKTOP-IL5URS6\\SQLEXPRESS; Initial Catalog = hotelproje; Integrated Security = True");
        SqlCommand komut = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        public Form10()
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
            komut = new SqlCommand("select * from siparis", baglantı);
            adp = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            adp.Fill(ds, "siparis");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "siparis";
            dataGridView1.Columns[0].HeaderText = "Sipariş İD";
            dataGridView1.Columns[1].HeaderText = "Yemek İD";
            dataGridView1.Columns[2].HeaderText = "Sipariş Tarihi";
            dataGridView1.Columns[3].HeaderText = "Kalite";
            dataGridView1.Columns[4].HeaderText = "Tutar";
            dataGridView1.Columns[4].HeaderText = "Durumu";

        }

        private void Form10_Load(object sender, EventArgs e)
        {
            if (baglantı.State == ConnectionState.Closed) baglantı.Open();
            komut = new SqlCommand("select * from siparis", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                textBox1.Text = okuyucu["siparisid"].ToString();
                textBox2.Text = okuyucu["yemekid"].ToString();
                dateTimePicker1.Text = okuyucu["siparistarihi"].ToString();
                comboBox1.Text = okuyucu["kalite"].ToString();
                textBox3.Text = okuyucu["tutar"].ToString();
                comboBox2.Text = okuyucu["status"].ToString();



            }
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select * from siparis", baglantı);
            adp.Fill(ds, "siparis");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "siparis";
            baglantı.Close();


            adp.SelectCommand = new SqlCommand("select * from yemek", baglantı);
            adp.Fill(ds, "yemek");
            dataGridView2.DataSource = ds;
            dataGridView2.DataMember = "yemek";
            baglantı.Close();




        }

        private void ekle_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("insert into siparis(siparisid,yemekid,siparistarihi,kalite,tutar,status) values (@sid,@yid,@sit,@kalite,@tutar,@stat)", baglantı);
                komut.Parameters.AddWithValue("@sid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@yid", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@sit", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@kalite", comboBox1.Text);
                komut.Parameters.AddWithValue("@tutar", textBox3.Text);
                komut.Parameters.AddWithValue("@stat", comboBox2.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();
                MessageBox.Show("Sipariş Kaydedildi");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata Var"+ex.Message);
                baglantı.Close();
            }
            
        }

        private void sil_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                komut = new SqlCommand("Delete from siparis where siparisid='" + textBox1.Text + "'", baglantı);
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
                komut = new SqlCommand("update siparis set yemekid=@yid,siparistarihi=@sit,kalite=@kalite,tutar=@tutar,status=@stat" + " where siparisid=@sid ", baglantı);
                komut.Parameters.AddWithValue("@sid", Convert.ToInt32(textBox1.Text));
                komut.Parameters.AddWithValue("@yid", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@sit", Convert.ToDateTime(dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@kalite", comboBox1.Text);
                komut.Parameters.AddWithValue("@tutar", textBox3.Text);
                komut.Parameters.AddWithValue("@stat", comboBox2.Text);
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
            komut = new SqlCommand("select * from siparis where siparisid=@sid", baglantı);
            komut.Parameters.AddWithValue("@sid", textBox1.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "siparis");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                int x = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[x].Cells[4].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[x].Cells[5].Value.ToString();
                


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
