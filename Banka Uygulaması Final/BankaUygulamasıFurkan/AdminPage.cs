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
    public partial class AdminPage : Form
    {
        public AdminPage admin;
        private DataTable dataTable;
        public AdminPage()
        {
            InitializeComponent();
        }
        public SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-2CV20TJ\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;");
        private void AdminPage_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlDataAdapter cmd = new SqlDataAdapter(@"SELECT * FROM [dbo].[Girdiler]", sqlConnection);
            dataTable = new DataTable(); // Yeni bir DataTable oluştur
            cmd.Fill(dataTable);
            sqlConnection.Close();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.FillWeight = 1;
            }
            dataGridView1.DataSource = dataTable;
            // Subscribe to the CellFormatting event
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }

        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Giriş form1 = new Giriş();
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string girilensayı = textBox1.Text;
            // TextBox'a girilen ID'yi alın
            if (radioButton1.Checked == true)
            {
                if (int.TryParse(girilensayı, out int userIDToDelete))
                {
                    // SQL Server'dan silme işlemi
                    try
                    {
                        sqlConnection.Open(); // SQL bağlantısını aç
                        SqlCommand deleteCommand = new SqlCommand("DELETE FROM [dbo].[Girdiler] WHERE ID = @ID", sqlConnection);
                        deleteCommand.Parameters.AddWithValue("@ID", userIDToDelete); // Parametreli SQL sorgusu
                        deleteCommand.ExecuteNonQuery(); // SQL sorgusunu çalıştır
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("SQL Server'dan silme işleminde bir hata oluştu: " + ex.Message); // Hata durumunda kullanıcıya bilgi ver
                        return; // Hata durumunda fonksiyondan çık
                    }
                    finally
                    {
                        sqlConnection.Close(); // SQL bağlantısını kapat (finally bloğu, hata olsun ya da olmasın her durumda çalışır)
                    }

                    // DataTable'dan silme işlemi
                    DataRow rowToDelete = dataTable.Rows.Cast<DataRow>().FirstOrDefault(row => row.Field<int>("ID") == userIDToDelete);
                    if (rowToDelete != null)
                    {
                        rowToDelete.Delete(); // DataTable'dan satırı sil
                        dataTable.AcceptChanges();  // DataTable'ı güncellemek için
                    }

                    MessageBox.Show($"Kullanıcı ID {userIDToDelete} başarıyla silindi."); // Kullanıcıya bilgi ver
                }
                else
                {
                    MessageBox.Show("Geçerli bir kullanıcı ID'si giriniz."); // Hatalı giriş durumunda kullanıcıya bilgi ver
                }
            }

            if (radioButton2.Checked)
            {
                if (int.TryParse(girilensayı, out int userIDToUpdate))
                {
                    string newSifre = textBox2.Text; // TextBox'tan yeni "Şifre" değerini al

                    UpdatePasswordInDatabase(userIDToUpdate, newSifre);
                }
                else
                {
                    MessageBox.Show("Geçerli bir kullanıcı ID'si giriniz.");
                }
            }
        }
            private void UpdatePasswordInDatabase(int userIDToUpdate, string newPassword)
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand updateCommand = new SqlCommand("UPDATE Girdiler SET Şifre = @Şifre WHERE ID = @ID", sqlConnection);
                    updateCommand.Parameters.AddWithValue("@Şifre", newPassword);
                    updateCommand.Parameters.AddWithValue("@ID", userIDToUpdate);
                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show($"Kullanıcı ID {userIDToUpdate} bilgileri başarıyla güncellendi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SQL Server'dan güncelleme işleminde bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the current column is the password column
            if (e.ColumnIndex == 6 && e.Value != null)
            {
                // Replace the password text with asterisks
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}