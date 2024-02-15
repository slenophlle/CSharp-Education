using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaUygulamasıFurkan
{
    public partial class İşlemler : Form
    {
        public İşlemler işlemler;
        public İşlemler()
        {
            InitializeComponent();
            panel1.Paint += panel1_Paint;
            panel2.Paint += panel2_Paint;
            
            işlemler = this;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 20; // Köşe yarıçapı
            Rectangle panelRect = new Rectangle(0, 0, panel1.Width, panel1.Height);
            int diameter = borderRadius * 2;

            GraphicsPath panelPath = new GraphicsPath();
            panelPath.AddArc(panelRect.X, panelRect.Y, diameter, diameter, 180, 90); // Sol üst köşe
            panelPath.AddArc(panelRect.Right - diameter, panelRect.Y, diameter, diameter, 270, 90); // Sağ üst köşe
            panelPath.AddArc(panelRect.Right - diameter, panelRect.Bottom - diameter, diameter, diameter, 0, 90); // Sağ alt köşe
            panelPath.AddArc(panelRect.X, panelRect.Bottom - diameter, diameter, diameter, 90, 90); // Sol alt köşe
            panelPath.CloseAllFigures();

            panel1.Region = new Region(panelPath);
            using (Pen pen = new Pen(Color.White, 2)) // İsteğe bağlı: Köşelerin etrafında bir çizgi çizebilirsiniz
            {
                e.Graphics.DrawPath(pen, panelPath);
            }

            using (Font font = new Font("Bahnschrift SemiCondensed", 14))
            {
                using (Brush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.DrawString("HESAP BİLGİLERİ", font, brush, new PointF(13, 20));
                }
            }

        }
        private void panel1_Resize(object sender, EventArgs e)
        {
                panel1.Invalidate();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 20; // Köşe yarıçapı
            Rectangle panelRect = new Rectangle(0, 0, panel2.Width, panel2.Height);
            int diameter = borderRadius * 2;

            GraphicsPath panelPath = new GraphicsPath();
            panelPath.AddArc(panelRect.X, panelRect.Y, diameter, diameter, 180, 90); // Sol üst köşe
            panelPath.AddArc(panelRect.Right - diameter, panelRect.Y, diameter, diameter, 270, 90); // Sağ üst köşe
            panelPath.AddArc(panelRect.Right - diameter, panelRect.Bottom - diameter, diameter, diameter, 0, 90); // Sağ alt köşe
            panelPath.AddArc(panelRect.X, panelRect.Bottom - diameter, diameter, diameter, 90, 90); // Sol alt köşe
            panelPath.CloseAllFigures();

            panel2.Region = new Region(panelPath);
            using (Pen pen = new Pen(Color.White, 2)) // İsteğe bağlı: Köşelerin etrafında bir çizgi çizebilirsiniz
            {
                e.Graphics.DrawPath(pen, panelPath);
            }

            using (Font font = new Font("Bahnschrift SemiCondensed", 16))
            {
                using (Brush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.DrawString("EFT / HAVALE", font, brush, new PointF(20, 20));
                }
            }

        }
        private void panel2_Resize(object sender, EventArgs e)
        {
            panel2.Invalidate();
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            HesapBilgileri form4 = new HesapBilgileri();
            form4.Show();
            this.Visible = false;
        }
        private void panel2_Click(object sender, EventArgs e)
        {
            EFT eFT = new EFT();
            eFT.Show();
            this.Visible = false;
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 20; // Köşe yarıçapı
            Rectangle panelRect = new Rectangle(0, 0, panel1.Width, panel1.Height);
            int diameter = borderRadius * 2;

            GraphicsPath panelPath = new GraphicsPath();
            panelPath.AddArc(panelRect.X, panelRect.Y, diameter, diameter, 180, 90); // Sol üst köşe
            panelPath.AddArc(panelRect.Right - diameter, panelRect.Y, diameter, diameter, 270, 90); // Sağ üst köşe
            panelPath.AddArc(panelRect.Right - diameter, panelRect.Bottom - diameter, diameter, diameter, 0, 90); // Sağ alt köşe
            panelPath.AddArc(panelRect.X, panelRect.Bottom - diameter, diameter, diameter, 90, 90); // Sol alt köşe
            panelPath.CloseAllFigures();

            panel3.Region = new Region(panelPath);
            using (Pen pen = new Pen(Color.White, 2)) // İsteğe bağlı: Köşelerin etrafında bir çizgi çizebilirsiniz
            {
                e.Graphics.DrawPath(pen, panelPath);
            }

            using (Font font = new Font("Bahnschrift SemiCondensed", 16))
            {
                using (Brush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.DrawString("ÇIKIŞ", font, brush, new PointF(50, 20));
                }
            }
        }
        private void panel3_Resize(object sender, EventArgs e)
        {
            panel3.Invalidate();
        }
    }
}