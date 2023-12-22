using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace FormDbCalisma
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-RSVDMT5;Initial Catalog=FormDbCalisma;Integrated Security=True");
        SqlCommand komut;
        SqlDataAdapter da;


        public Form1()
        {
            InitializeComponent();

        }

        private void listele()
        {

            baglanti.Open();
            string sorgu = "Select * From Abone";
            da = new SqlDataAdapter(sorgu, baglanti);
            DataTable Tablo = new DataTable();
            da.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
            baglanti.Close();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {


            string sorgu = "insert into Abone(Ad,Soyad,Telefon,[Doğum Tarihi])values(@isim,@soyisim,@telefon,@dogumTarihi)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("isim", txtAd.Text);
            komut.Parameters.AddWithValue("soyisim", txtSoyad.Text);
            komut.Parameters.AddWithValue("telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("dogumTarihi", dateTimePicker1.Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            MessageBox.Show("Kaydınız başarıyla gerçekleşmiştir!!");
            baglanti.Close();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {


            string sorgu = "UPDATE Abone SET Ad = @isim,Soyad = @soyisim,Telefon = @telefon,[Doğum Tarihi] = @dogumTarihi WHERE Id = @id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("isim", txtAd.Text);
            komut.Parameters.AddWithValue("soyisim", txtSoyad.Text);
            komut.Parameters.AddWithValue("telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("dogumTarihi", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("id", dataGridView1.CurrentRow.Cells[0].Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            MessageBox.Show("Güncelleme işleminiz başarıyla gerçekleşmiştir!!");
            baglanti.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {


            string sorgu = "DELETE FROM Abone WHERE Id = @id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("id", dataGridView1.CurrentRow.Cells[0].Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            MessageBox.Show("Silme işleminiz başarıyla gerçekleşmiştir!!");
            baglanti.Close();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtTelefon.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
