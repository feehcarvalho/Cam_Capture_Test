using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;

namespace WindowsFormsApplication4
{
    public partial class Form1 : MetroForm
    {
        private DirectShowLib.Capture _camera;
        public Form1()
        {
            InitializeComponent();
            // Considerar a primeira câmera que for encontrada no sistema
            const int VIDEODEVICE = 0;
            // Resolucao de 640x480, 24 bits por pixel. A sua câmera tem que suportar essa resolução, senão, altere para uma resolução suportada.
            const int VIDEOWIDTH = 640;
            const int VIDEOHEIGHT = 480;
            const int VIDEOBITSPERPIXEL = 24;

            _camera = new DirectShowLib.Capture(VIDEODEVICE, VIDEOWIDTH, VIDEOHEIGHT, VIDEOBITSPERPIXEL, pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var webCamPoint = pictureBox1.PointToScreen(new Point(0, 0));
                graphics.CopyFromScreen(webCamPoint.X, webCamPoint.Y, 0, 0, bitmap.Size);
            }

            try
            {
                bitmap.Save("fotografia.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            catch
            {
                MessageBox.Show("Deu ruim!");
            }

           
           

        }

        protected override void OnClosed(EventArgs e)
        {
            _camera.Dispose();
            base.OnClosed(e);
        }
    }
}
