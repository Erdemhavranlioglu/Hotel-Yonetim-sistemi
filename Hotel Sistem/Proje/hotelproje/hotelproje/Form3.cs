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
    public partial class Form3 : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IL5URS6\\SQLEXPRESS;Initial Catalog=hotelproje;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        DataTable tablo=new DataTable();

        public Form3()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void verigoster()
        {
            komut = new SqlCommand("select * from calisan", baglantı);
            adp = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            adp.Fill(ds, "calisan");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "calisan";
            dataGridView1.Columns[0].HeaderText = "Çalışan İD";
            dataGridView1.Columns[1].HeaderText = "Çalışan İsmi";
            dataGridView1.Columns[2].HeaderText = "Giriş İD";
            dataGridView1.Columns[3].HeaderText = "Şifre";
            dataGridView1.Columns[4].HeaderText = "Görevi";
            dataGridView1.Columns[5].HeaderText = "Durumu";
            dataGridView1.Columns[6].HeaderText = "Telefon Numarası";
            dataGridView1.Columns[7].HeaderText = "Adres";
            dataGridView1.Columns[8].HeaderText = "Maaş";


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from calisan where calisanid=@calid", baglantı);
            komut.Parameters.AddWithValue("@calid", textBox1.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "calisan");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                int x = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[x].Cells[4].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[x].Cells[5].Value.ToString();
                textBox5.Text = dataGridView1.Rows[x].Cells[6].Value.ToString();
                textBox6.Text = dataGridView1.Rows[x].Cells[7].Value.ToString();
                textBox7.Text = dataGridView1.Rows[x].Cells[8].Value.ToString();
                
            }
            else
            {
                MessageBox.Show("Yanlış yere tıkladınız");
            }
            baglantı.Close();
        }
       

        private void Form3_Load(object sender, EventArgs e)
        {          
            if (baglantı.State == ConnectionState.Closed) baglantı.Open();
            komut = new SqlCommand("select * from calisan", baglantı);
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())

            {
                textBox1.Text = okuyucu["calisanid"].ToString();
                textBox2.Text = okuyucu["calisanisim"].ToString();
                textBox3.Text = okuyucu["girisid"].ToString();
                textBox4.Text = okuyucu["sifre"].ToString();
                textBox5.Text = okuyucu["telefonno"].ToString();
                textBox6.Text = okuyucu["adres"].ToString();
                textBox7.Text = okuyucu["maas"].ToString();

                comboBox2.Text = okuyucu["gorevi"].ToString();
                
                


            }
            
            textBox1.ReadOnly = false;
            okuyucu.Close();
            adp.SelectCommand = new SqlCommand("select * from calisan", baglantı);
            adp.Fill(ds, "calisan");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "calisan";
            baglantı.Close();

            adp.SelectCommand = new SqlCommand("select * from hatirlatici", baglantı);
            adp.Fill(ds, "hatirlatici");
            dataGridView2.DataSource = ds;
            dataGridView2.DataMember = "hatirlatici";
            baglantı.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("insert into calisan(calisanid,calisanisim,girisid,sifre,gorevi,status,telefonno,adres,maas) values (@calid,@isim,@gir,@sif,@gor,@stat,@tel,@adr,@maas)", baglantı);
                komut.Parameters.AddWithValue("@calid", textBox1.Text);
                komut.Parameters.AddWithValue("@isim", textBox2.Text);
                komut.Parameters.AddWithValue("@gir", textBox3.Text);
                komut.Parameters.AddWithValue("@sif", Convert.ToInt32(textBox4.Text));
                komut.Parameters.AddWithValue("@gor", comboBox2.Text);
                komut.Parameters.AddWithValue("@stat", Convert.ToString(comboBox1.Text));
                komut.Parameters.AddWithValue("@tel", textBox5.Text);
                komut.Parameters.AddWithValue("@adr", textBox6.Text);
                komut.Parameters.AddWithValue("@maas", Convert.ToInt32(textBox7.Text));
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();
                MessageBox.Show("Çalışan Başarıyla Kaydedildi");

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata var"+ex.Message);
                baglantı.Close();
            }
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglantı.Open();
                komut = new SqlCommand("update calisan set calisanisim=@isim,girisid=@gir,sifre=@sif,gorevi=@gor,status=@stat,telefonno=@tel,adres=@adr,maas=@maas" + " where calisanid=@calid ", baglantı);
                komut.Parameters.AddWithValue("@calid", textBox1.Text);
                komut.Parameters.AddWithValue("@isim", textBox2.Text);
                komut.Parameters.AddWithValue("@gir", textBox3.Text);
                komut.Parameters.AddWithValue("@sif", Convert.ToInt32(textBox4.Text));
                komut.Parameters.AddWithValue("@gor", comboBox2.Text);
                komut.Parameters.AddWithValue("@stat", Convert.ToString(comboBox1.Text));
                komut.Parameters.AddWithValue("@tel", textBox5.Text);
                komut.Parameters.AddWithValue("@adr", textBox6.Text);
                komut.Parameters.AddWithValue("@maas", Convert.ToInt32(textBox7.Text));
                komut.ExecuteNonQuery();
                baglantı.Close();
                verigoster();
                MessageBox.Show("Çalışan Başarıyla güncellendi");
                baglantı.Close();

                MessageBox.Show(textBox1.Text + " Çalışana ait bilgiler güncellendi");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglantı.Close();


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                komut = new SqlCommand("Delete from calisan where calisanid='" + textBox1.Text + "'", baglantı);
                komut.ExecuteNonQuery();
                MessageBox.Show("kayıt silindi");
                baglantı.Close();
                verigoster();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglantı.Close();


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand cmd = baglantı.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from calisan where calisanid='" + textBox8.Text + "'";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();



            baglantı.Open();
            SqlCommand cma = baglantı.CreateCommand();
            cma.CommandType = CommandType.Text;
            cma.CommandText = "select * from hatirlatici where calisanid='" + textBox8.Text + "'";

            DataTable df = new DataTable();
            SqlDataAdapter dc = new SqlDataAdapter(cma);
            da.Fill(df);
            dataGridView1.DataSource = df;
            baglantı.Close();









            DataSet ds = new DataSet();
            baglantı.Open();
            komut = new SqlCommand("select * from calisan where calisanid=@calid", baglantı);
            komut.Parameters.AddWithValue("@calid", textBox8.Text);
            adp = new SqlDataAdapter(komut);
            adp.Fill(ds, "calisan");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {         
                textBox1.DataBindings.Clear(); textBox1.DataBindings.Add("text", ds, "calisan.calisanid");
                textBox2.DataBindings.Clear(); textBox2.DataBindings.Add("text", ds, "calisan.calisanisim");
                textBox3.DataBindings.Clear(); textBox3.DataBindings.Add("text", ds, "calisan.girisid");
                textBox4.DataBindings.Clear(); textBox4.DataBindings.Add("text", ds, "calisan.sifre");
                comboBox2.DataBindings.Clear(); comboBox2.DataBindings.Add("text", ds, "calisan.gorevi");
                comboBox1.DataBindings.Clear(); comboBox1.DataBindings.Add("text", ds, "calisan.status");
                textBox5.DataBindings.Clear(); textBox5.DataBindings.Add("text", ds, "calisan.telefonno");
                textBox6.DataBindings.Clear(); textBox6.DataBindings.Add("text", ds, "calisan.adres");
                textBox7.DataBindings.Clear(); textBox7.DataBindings.Add("text", ds, "calisan.maas");
                MessageBox.Show("Aranan Çalışana Ait Bilgiler Listelendi");
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
                textBox7.Clear();
                MessageBox.Show("bu İD numarasına ship çaşılan kayıtlı değil");
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Close();

        }
    }
}
