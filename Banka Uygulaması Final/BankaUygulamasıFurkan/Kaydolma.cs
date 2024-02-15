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
    public partial class Kaydolma : Form
    {
        public Kaydolma form2;
        public string tc;
        public Kaydolma()
        {
            InitializeComponent();
            form2 = this;
        }
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-2CV20TJ\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True");
      
        /*public string GenerateRandomAmount()
        {
            Random random = new Random();

            // Rastgele para miktarı oluştur
            int lira = random.Next(0, 1000); // Lira kısmı için 0 ile 999 arasında bir sayı
            int kurus = random.Next(0, 100); // Kuruş kısmı için 0 ile 99 arasında bir sayı

            // Oluşturulan miktarı istenen formatta göster
            //double formattedAmount = $"{lira:N0}.{kurus:D2}".Replace(",", "."); // N0 lira kısmını virgülle ayırır, D2 ise kuruş kısmını iki basamağa tamamlar

            return lira;
        }*/

        // Rastgele bir IBAN ve Para oluşturmak için
        public Tuple<string, string> GenerateRandomIBANs()
        {
            Random random = new Random();
            string iban1 = "TR ";
            string Miktar=""; // Rastgele miktar oluşturuluyor

            for (int i = 0; i < 22; i++)
            {
                iban1 += random.Next(10);
            }

            for (int i = 0; i < 4; i++)
            {
                Miktar += random.Next(10);
            }

            return new Tuple<string, string>(iban1, Miktar);
        }

        // IBAN'ı ve Parayı SQL veritabanına kaydetmek için
        public void SaveIBANsToDatabase(string iban1, string Miktar)
        {
            try
            {
                string connectionString = @"Data Source=DESKTOP-2CV20TJ\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // İlk IBAN'ı kaydet
                    string query1 = "UPDATE Girdiler SET IBAN = @IBAN WHERE TC = '" + tc + "'";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("@IBAN", iban1);
                    command1.ExecuteNonQuery();

                    // Hesap Durumu kaydet
                    string query2 = "UPDATE Girdiler SET Para = @Para WHERE TC = '" + tc + "'";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@Para", Miktar);
                    command2.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void RegisterButton_Click_1(object sender, EventArgs e)
        {
            tc = textBoxTC.Text;
            if (!string.IsNullOrEmpty(textBoxName.Text) && textBoxName.Text.Length >= 3 && textBoxName.Text.Length <= 20 &&
                !string.IsNullOrEmpty(textBoxSurname.Text) && textBoxSurname.Text.Length >= 3 && textBoxSurname.Text.Length <= 20 &&
                !string.IsNullOrEmpty(tc) && tc.Length == 11 &&
                !string.IsNullOrEmpty(textBoxPassword.Text) && textBoxPassword.Text.Length >= 8 && textBoxPassword.Text.Length <= 20)
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Girdiler]
           ([TC],[Şifre],[Ad],[Soyad])
     VALUES
           ('" + tc + "', '" + textBoxPassword.Text + "', '" + textBoxName.Text + "', '" + textBoxSurname.Text + "')", sqlConnection);
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                MessageBox.Show("Kaydınız tamamlanmıştır.", "Bilgi", MessageBoxButtons.OK);
                textBoxName.Clear();
                textBoxSurname.Clear();
                textBoxTC.Clear();
                textBoxPassword.Clear();
                this.Visible = false;
                Giriş form1 = new Giriş();
                form1.Visible = true;
                Tuple<string, string> generatedIBANs = GenerateRandomIBANs(); // Generate the IBAN
                SaveIBANsToDatabase(generatedIBANs.Item1, generatedIBANs.Item2); // Save the generated IBAN to the database
            }
            else
            {
                MessageBox.Show("Lütfen bilgilerinizi doğru girip boş bir kısım bırakmadığınıza emin olunuz.");
                textBoxName.Clear();
                textBoxSurname.Clear();
                textBoxTC.Clear();
                textBoxPassword.Clear();
            }
        }

        private void girişPenceresiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Giriş form1 = new Giriş();
            form1.Show();
            this.Close();
        }
        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}