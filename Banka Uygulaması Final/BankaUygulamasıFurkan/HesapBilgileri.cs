using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaUygulamasıFurkan
{
    public partial class HesapBilgileri : Form
    {
        public HesapBilgileri()
        {
            InitializeComponent();
        }

        private void HesapBilgileri_Load(object sender, EventArgs e)
        {
            string ad = Giriş.giriş.TC;
            string connectionString = @"Data Source=DESKTOP-2CV20TJ\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

            // Bağlantı açma
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Komut oluşturma
                string query = "SELECT IBAN, Ad, TC, Para FROM Girdiler WHERE TC = @TC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Parametre ekleme
                    command.Parameters.AddWithValue("@TC", ad);

                    try
                    {
                        // Bağlantıyı aç
                        connection.Open();

                        // Sorguyu çalıştır ve sonucu al
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                label5.Text = reader["IBAN"].ToString(); // IBAN etiketine yazdır
                                label6.Text = reader["TC"].ToString();  // TC etiketine yazdır
                                label7.Text = reader["Ad"].ToString();//Ad etiketi
                                label8.Text = (reader["Para"].ToString())+" TL"; // Para etiketine yazdır
                            }
                        }
                        else
                        {
                            label5.Text = "Kayıt bulunamadı.";
                            label6.Text = "Kayıt bulunamadı.";
                            label7.Text = "Kayıt bulunamadı.";
                            label8.Text = "Kayıt bulunamadı.";
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Hata durumunda işlemler
                        label7.Text = "Hata: " + ex.Message;
                    }
                }
            }
        }
        private void bilgileriGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BilgileriGüncelle güncelleme = new BilgileriGüncelle();
            güncelleme.Show();
            this.Close();
        }
        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            İşlemler işlemler = new İşlemler();
            işlemler.Show();
            this.Close();
        }
    }
}