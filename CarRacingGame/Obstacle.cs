using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CarRacingGame
{
    public class Obstacle
    {
        public PictureBox PictureBoxObstacle {  get; private set; }
        private Form gameForm;
        private static Random random = new Random();

        public Obstacle(Form form)
        {
            gameForm = form;
            PictureBoxObstacle = new PictureBox
            {
                Size = new Size(50, 50),
                BackColor = Color.Red,
                Location = new Point(random.Next(form.ClientSize.Width - 50), -50) // Random x position, above the form
            };
            gameForm.Controls.Add(PictureBoxObstacle);
        }

        public void Move(int speed)
        {
            PictureBoxObstacle.Top += speed;
        }

        public bool IsOffScreen()
        {
            return PictureBoxObstacle.Top > gameForm.ClientSize.Height;
        }

        public void Remove()
        {
            gameForm.Controls.Remove(PictureBoxObstacle);
            PictureBoxObstacle.Dispose();
        }
    }
}
