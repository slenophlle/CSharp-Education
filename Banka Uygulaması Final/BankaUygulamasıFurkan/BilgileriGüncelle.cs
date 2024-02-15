using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaUygulamasıFurkan
{
    public partial class BilgileriGüncelle : Form
    {
        public BilgileriGüncelle güncelleme;
        public BilgileriGüncelle()
        {
            InitializeComponent();
            güncelleme = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newAd = textBox1.Text;// Yeni Ad değeri alınıyor (label7 kullanıcı tarafından güncellenecek)

            string newSifre = textBox2.Text;// Yeni Para değeri alınıyor (label10 kullanıcı tarafından güncellenecek)

            string userTC = Giriş.giriş.TC; // TC değeri alınıyor

            string connectionString = @"Data Source=DESKTOP-2CV20TJ\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Girdiler SET Ad = @NewAd, Şifre = @NewSifre WHERE TC = @TC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewAd", newAd);
                    command.Parameters.AddWithValue("@NewSifre", newSifre);
                    command.Parameters.AddWithValue("@TC", userTC);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Bilgiler güncellendi!");
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme başarısız oldu.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }

        }
        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            İşlemler işlemler = new İşlemler();
            işlemler.Show();
            this.Close();
        }

        private void BilgileriGüncelle_Load(object sender, EventArgs e)
        {

        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}