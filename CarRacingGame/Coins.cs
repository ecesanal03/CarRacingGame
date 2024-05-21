using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CarRacingGame
{
    public class CircularPictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, this.Width - 1, this.Height - 1);
                this.Region = new Region(gp);
                pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                pe.Graphics.FillEllipse(new SolidBrush(this.BackColor), 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
    public class Coins
    {
        public CircularPictureBox PictureBoxCoins { get; set; }
        private Form gameForm;
        private static Random random = new Random();

        public Coins(Form form)
        {
            gameForm = form;
            PictureBoxCoins = new CircularPictureBox
            {
                Size = new Size(25, 25),
                BackColor = Color.Gold,
                Location = new Point(random.Next(form.ClientSize.Width - 25), -25) // Random x position, above the form
            };
            gameForm.Controls.Add(PictureBoxCoins);
        }

        public void Move(int speed)
        {
            PictureBoxCoins.Top += speed;
        }

        public bool IsOffScreen()
        {
            return PictureBoxCoins.Top > gameForm.ClientSize.Height;
        }

        public void Remove()
        {
            gameForm.Controls.Remove(PictureBoxCoins);
            PictureBoxCoins.Dispose();
        }
    }
}
