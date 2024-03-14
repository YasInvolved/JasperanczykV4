using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JasperanczykV4
{
    public partial class JasperanczykV4 : Form
    {
        public JasperanczykV4(string url, int x, int y)
        {
            this.Location = new Point(x, y);
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Load(url);
        }
    }
}
