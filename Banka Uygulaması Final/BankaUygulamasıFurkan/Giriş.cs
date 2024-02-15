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
    public partial class Giriş : Form
    {
        public static Giriş giriş;
        public string TC;
        public string TCGiris { get; set; }
        public Giriş()
        {
            InitializeComponent();
            giriş = this;
        }
        SqlConnection SqlConnection = new SqlConnection(@"Data Source=DESKTOP-2CV20TJ\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;");
        private void OpenAccount_Click(object sender, EventArgs e)
        {
            Kaydolma form2 = new Kaydolma();
            form2.Show();
            giriş.Visible = false;
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {

            TC = textBox1.Text;
            string Password = textBox2.Text;
            try
            {
                SqlConnection.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) FROM Girdiler WHERE [TC] = '" + TC + "' AND Şifre='" + Password + "' ", SqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                cmd.ExecuteNonQuery();
                if (!string.IsNullOrEmpty(textBox1.Text) && textBox1.Text.Length == 11 &&
                    !string.IsNullOrEmpty(textBox2.Text) && textBox2.Text.Length <= 20)
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show("Giriş Başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        İşlemler form3 = new İşlemler();
                        textBox1.Clear();
                        textBox2.Clear();
                        form3.Show();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Giriş Başarısız lütfen bilgilerinizi doğru girdiğinize emin olunuz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen TC kimlik numaranızın doğru olduğuna emin olunuz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Query sırasında hata oluştu!" + ex.ToString());
            }
            finally
            {
                SqlConnection.Close();
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                AdminPage admin = new AdminPage();
                admin.Show();
                this.Visible = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Giriş_Load(object sender, EventArgs e)
        {

        }
    }
}