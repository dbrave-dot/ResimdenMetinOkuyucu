using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tesseract;

namespace ResimdenMetinOkuyucu
{
    public partial class Form1 : Form
    {
        private string secilenResimYolu = "";
        
        private Button btnResimSec;
        private Button btnMetinOku;
        private Button btnKopyala;
        private PictureBox pictureBox1;
        private RichTextBox richTextBox1;
        private Label lblDurum;
        private Label lblVersion;
        private OpenFileDialog openFileDialog1;

        public Form1()
        {
            // === FORM AYARLARI ===
            this.Text = "OCR Reader v0.1 - Test Aşaması";
            this.Size = new Size(1000, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 32);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;  // Simge durumuna küçültme butonu AÇIK (istersen false yap)

            // === VERSİYON ETİKETİ ===
            lblVersion = new Label();
            lblVersion.Text = "v0.1 | TEST AŞAMASI";
            lblVersion.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblVersion.ForeColor = Color.FromArgb(200, 200, 200);
            lblVersion.Size = new Size(150, 25);
            lblVersion.Location = new Point(15, 15);
            this.Controls.Add(lblVersion);

            // === BUTONLAR ===
            
            btnResimSec = new Button();
            btnResimSec.Text = "Resim Seç";
            btnResimSec.Font = new Font("Segoe UI", 10);
            btnResimSec.Size = new Size(100, 35);
            btnResimSec.Location = new Point(20, 55);
            btnResimSec.BackColor = Color.FromArgb(45, 45, 48);
            btnResimSec.ForeColor = Color.White;
            btnResimSec.FlatStyle = FlatStyle.Flat;
            btnResimSec.FlatAppearance.BorderColor = Color.FromArgb(70, 70, 75);
            btnResimSec.FlatAppearance.BorderSize = 1;
            btnResimSec.Cursor = Cursors.Hand;
            btnResimSec.Click += BtnResimSec_Click;
            this.Controls.Add(btnResimSec);

            btnMetinOku = new Button();
            btnMetinOku.Text = "Metin Oku";
            btnMetinOku.Font = new Font("Segoe UI", 10);
            btnMetinOku.Size = new Size(100, 35);
            btnMetinOku.Location = new Point(130, 55);
            btnMetinOku.BackColor = Color.FromArgb(45, 45, 48);
            btnMetinOku.ForeColor = Color.White;
            btnMetinOku.FlatStyle = FlatStyle.Flat;
            btnMetinOku.FlatAppearance.BorderColor = Color.FromArgb(70, 70, 75);
            btnMetinOku.FlatAppearance.BorderSize = 1;
            btnMetinOku.Cursor = Cursors.Hand;
            btnMetinOku.Click += BtnMetinOku_Click;
            this.Controls.Add(btnMetinOku);

            btnKopyala = new Button();
            btnKopyala.Text = "Kopyala";
            btnKopyala.Font = new Font("Segoe UI", 10);
            btnKopyala.Size = new Size(100, 35);
            btnKopyala.Location = new Point(240, 55);
            btnKopyala.BackColor = Color.FromArgb(45, 45, 48);
            btnKopyala.ForeColor = Color.White;
            btnKopyala.FlatStyle = FlatStyle.Flat;
            btnKopyala.FlatAppearance.BorderColor = Color.FromArgb(70, 70, 75);
            btnKopyala.FlatAppearance.BorderSize = 1;
            btnKopyala.Cursor = Cursors.Hand;
            btnKopyala.Click += BtnKopyala_Click;
            this.Controls.Add(btnKopyala);

            // === DURUM LABEL (daha geniş ve sağda) ===
            lblDurum = new Label();
            lblDurum.Text = "Hazır. Bir resim seçin.";
            lblDurum.Font = new Font("Segoe UI", 9);
            lblDurum.Size = new Size(350, 25);
            lblDurum.Location = new Point(370, 62);
            lblDurum.ForeColor = Color.FromArgb(160, 160, 165);
            this.Controls.Add(lblDurum);

            // === PICTUREBOX ===
            pictureBox1 = new PictureBox();
            pictureBox1.Size = new Size(420, 420);
            pictureBox1.Location = new Point(20, 110);
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.BackColor = Color.FromArgb(20, 20, 22);
            this.Controls.Add(pictureBox1);

            // === RICHTEXTBOX ===
            richTextBox1 = new RichTextBox();
            richTextBox1.Size = new Size(490, 420);
            richTextBox1.Location = new Point(460, 110);
            richTextBox1.Font = new Font("Consolas", 10);
            richTextBox1.BackColor = Color.FromArgb(20, 20, 22);
            richTextBox1.ForeColor = Color.FromArgb(210, 210, 215);
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(richTextBox1);

            // === DIALOG ===
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.Title = "Bir resim seçin";
        }

        private void BtnResimSec_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                secilenResimYolu = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(secilenResimYolu);
                lblDurum.Text = "Resim yüklendi. 'Metin Oku'ya tıklayın.";
                lblDurum.ForeColor = Color.FromArgb(120, 200, 120);
            }
        }

        private void BtnMetinOku_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(secilenResimYolu))
            {
                MessageBox.Show("Önce bir resim seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                lblDurum.Text = "Metin okunuyor... Lütfen bekleyin.";
                lblDurum.ForeColor = Color.FromArgb(240, 180, 80);
                Application.DoEvents();

                string tessDataPath = Path.Combine(Application.StartupPath, "tessdata");

                if (!Directory.Exists(tessDataPath))
                {
                    MessageBox.Show("tessdata klasörü bulunamadı.\n" +
                                    $"Beklenen konum: {tessDataPath}", 
                                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblDurum.Text = "Hata: tessdata eksik.";
                    lblDurum.ForeColor = Color.FromArgb(240, 100, 100);
                    return;
                }

                using (var engine = new TesseractEngine(tessDataPath, "tur+eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(secilenResimYolu))
                    {
                        using (var page = engine.Process(img))
                        {
                            string metin = page.GetText();
                            richTextBox1.Text = metin;
                            lblDurum.Text = "Metin okuma tamamlandı.";
                            lblDurum.ForeColor = Color.FromArgb(120, 200, 120);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblDurum.Text = "Hata oluştu.";
                lblDurum.ForeColor = Color.FromArgb(240, 100, 100);
            }
        }

        private void BtnKopyala_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                Clipboard.SetText(richTextBox1.Text);
                lblDurum.Text = "Metin panoya kopyalandı.";
                lblDurum.ForeColor = Color.FromArgb(120, 200, 200);
            }
            else
            {
                MessageBox.Show("Kopyalanacak metin yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}