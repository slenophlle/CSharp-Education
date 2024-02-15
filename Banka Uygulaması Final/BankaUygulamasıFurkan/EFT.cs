using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaUygulamasıFurkan
{
    public partial class EFT : Form
    {
        public EFT()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-2CV20TJ\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;");
        public class Kisi
        {
            public string TC { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public string IBAN { get; set; }
            public string Para { get; set; }

            public override string ToString()
            {
                return $"{IBAN}";
            }
        }
        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            İşlemler işlemler = new İşlemler();
            işlemler.Show();
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string gönderileniban = AIBANbox.Text;

            if (gönderileniban.Length == 25 && tutarbox.Text !="")
            {
                try
                {
                    sqlConnection.Open();

                    //PARA GÖNDEREN HESABIN PARASINI GÜNCELLE
                    string göndereniban = comboBox1.SelectedItem.ToString();

                    SqlCommand cmd = new SqlCommand(@"SELECT Para FROM Girdiler WHERE [IBAN]= @göndereniban", sqlConnection);
                    cmd.Parameters.AddWithValue("@göndereniban", göndereniban);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    sqlDataAdapter.Fill(datatable);

                    int gönderilenparamiktarı = Convert.ToInt32(tutarbox.Text);
                    int gönderenPARAeski = Convert.ToInt32(datatable.Rows[0]["PARA"]);
                    int gönderenPARAyeni = gönderenPARAeski - gönderilenparamiktarı;

                    string miktargöndereneksipara = Convert.ToString(gönderenPARAyeni);

                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Girdiler SET [PARA]= @eksipara WHERE [IBAN] = @ibangönderen", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@eksipara", miktargöndereneksipara);
                    sqlCommand.Parameters.AddWithValue("@ibangönderen", göndereniban);
                    sqlCommand.ExecuteNonQuery();

                    //PARA GÖNDERİLEN HESABIN PARASINI GÜNCELLE 


                    SqlCommand cmd2 = new SqlCommand(@"SELECT Para FROM Girdiler WHERE [IBAN]= @gönderileniban", sqlConnection);
                    cmd2.Parameters.AddWithValue("@gönderileniban", gönderileniban);
                    SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(cmd2);
                    DataTable datatable2 = new DataTable();
                    sqlDataAdapter2.Fill(datatable2);

                    int gönderilenPARAeski = Convert.ToInt32(datatable2.Rows[0]["Para"]);
                    int gönderilenPARAyeni = gönderilenPARAeski + gönderilenparamiktarı;

                    string miktargönderilenartıpara = Convert.ToString(gönderilenPARAyeni);

                    SqlCommand sqlCommand1 = new SqlCommand(@"UPDATE Girdiler SET Para= @artıpara WHERE [IBAN] = @ibangönderilen", sqlConnection);
                    sqlCommand1.Parameters.AddWithValue("@artıpara", miktargönderilenartıpara);
                    sqlCommand1.Parameters.AddWithValue("@ibangönderilen", gönderileniban);
                    sqlCommand1.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    sqlConnection.Close(); // Bağlantıyı kapat
                }
                DialogResult result = MessageBox.Show("İşlem başarıyla tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    // OK tuşuna basıldığında başka bir forma geç
                    HesapBilgileri hesapBilgileriForm = new HesapBilgileri();
                    hesapBilgileriForm.Show();
                    this.Close();
                }
            }
            else { MessageBox.Show("Girilen Bilgiler Hatalı"); }
        }
        

        private void EFT_Load(object sender, EventArgs e)
        {
            try
            {
                string tc = Giriş.giriş.TC;
                string connectionString = @"Data Source=DESKTOP-2CV20TJ\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TC, Ad, Soyad, IBAN, Para FROM Girdiler WHERE TC = @tc";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@tc", tc);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Kisi kisi = new Kisi
                        {
                            TC = reader["TC"].ToString(),
                            Ad = reader["Ad"].ToString(),
                            Soyad = reader["Soyad"].ToString(),
                            IBAN = reader["IBAN"].ToString(),
                            Para = reader["Para"].ToString(),
                        };

                        comboBox1.Items.Add(kisi);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem is Kisi selectedKisi)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add($"Ad: {selectedKisi.Ad}");
                listBox1.Items.Add($"Soyad: {selectedKisi.Soyad}");
                listBox1.Items.Add($"TC: {selectedKisi.TC}");
                listBox1.Items.Add($"IBAN: {selectedKisi.IBAN}");
                listBox1.Items.Add($"Hesap Durumu: {selectedKisi.Para}");
            }
        }

        private void İşlemler(object sender, EventArgs e)
        {
            HesapBilgileri form3 = new HesapBilgileri();
            this.Visible = false;
            form3.Visible = true;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
