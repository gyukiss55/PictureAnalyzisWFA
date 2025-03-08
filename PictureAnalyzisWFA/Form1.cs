using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PictureAnalyzisWFA
{
    public partial class Form1 : Form
    {
        private PictureBox? pictureBox1;
        private PictureBox? pictureBox2;
        private Bitmap? bitmap1;
        private Bitmap? bitmap2;

        public void CreatePictureBox()
        {
            // Initialize PictureBox
            pictureBox1 = new PictureBox
            {
                Width = 600,
                Height = 800,
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            this.Controls.Add(pictureBox1);

            pictureBox2 = new PictureBox
            {
                Width = 600,
                Height = 800,
                Location = new Point(620, 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            this.Controls.Add(pictureBox2);

            // Load and display a Bitmap
            LoadBitmap();
        }

        private void LoadBitmap()
        {
            // Create a new Bitmap
            bitmap1 = new Bitmap(600, 800);

            // Draw on the bitmap
            using (Graphics g = Graphics.FromImage(bitmap1))
            {
                g.Clear(Color.White);
                g.DrawEllipse(Pens.Blue, 50, 50, 200, 150);
            }

            // Assign the bitmap to the PictureBox
            if (pictureBox1 != null)
                pictureBox1.Image = bitmap1;

            Size size = new Size(600, 800);

            bitmap2 = null;
            //bitmap2 = new Bitmap(600, 800);

            //Bitmap? originalBitmap = null;

            JPGReader jPGReader = new JPGReader ();

            jPGReader.ReadJPGFile("..\\..\\..\\..\\Images\\picture.jpg", out bitmap2, size);
            
            if (bitmap2 != null && pictureBox2 != null)
            {
                //using (Graphics g = Graphics.FromImage(bitmap2))
                {
                    pictureBox2.Image = bitmap2;
                    bitmap1 = new Bitmap(bitmap2);

                    JPGReader.SetPixelFast(bitmap2, 50, 60, 80, 90, Color.Blue);
                    JPGReader.SetPixelFast(bitmap2, 50, 110, 80, 140, Color.Green);
                    JPGReader.SetPixelFast(bitmap2, 50, 160, 80, 190, Color.Red);
                    if (pictureBox1 != null)
                        pictureBox1.Image = bitmap1;
                }
                
            }
            if (pictureBox1 != null)
                pictureBox1.MouseClick += new MouseEventHandler(Form_MouseClick);
            if (pictureBox2 != null)
                pictureBox2.MouseClick += new MouseEventHandler(Form_MouseClick);
        }




        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dispose the bitmap properly
            bitmap1?.Dispose();
            bitmap2?.Dispose();
            base.OnFormClosing(e);
        }
        
        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            int source = 0;
            int cR = 0;
            int cG = 0;
            int cB = 0;
            Color pixelColor = Color.Black;
            if (sender == pictureBox1 && bitmap1 != null)
            {
                source = 1;
                pixelColor = bitmap1.GetPixel(e.X, e.Y);
            }
            if (sender == pictureBox2)
            {
                source = 2;
                pixelColor = bitmap2.GetPixel(e.X, e.Y);
            }
            //MessageBox.Show($"Mouse clicked {source} at: X={e.X}, Y={e.Y}, Button={e.Button}, Color:{pixelColor}");
            Debug.WriteLine($"Mouse clicked {source} at: X={e.X}, Y={e.Y}, Button={e.Button}, Color:{pixelColor}");

        }

        public Form1()
        {
            this.Text = "Image handling";
            this.MouseClick += new MouseEventHandler(Form_MouseClick);
            InitializeComponent();
            CreatePictureBox();
        }
    }
}