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
    public partial class Form1 : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source = DESKTOP-IL5URS6\\SQLEXPRESS; Initial Catalog = hotelproje; Integrated Security = True");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public Form1()
        {
            InitializeComponent();
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void girisbtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="Admin"||textBox2.Text=="Admin")
            {
                Form2 frm2 = new Form2();
                frm2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Yanlış Şifre Veya Kullanıcı Adı  ");
            }



            
        }

        private void textBox2_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void kadilbl_Click(object sender, EventArgs e)
        {

        }
    }
}
