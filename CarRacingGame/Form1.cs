using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace CarRacingGame
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private List<Obstacle> obstacles = new List<Obstacle>();
        private int obstacleSpawnInterval = 100;
        private int obstacleSpeed = 7;
        private int frameCount = 0;
        private static Random random = new Random();
        private bool isGameOver = false;
        private int coinsCollected = 0;
        private List<Coins> coins = new List<Coins>();
        private int coinsSpawnInterval = 100;
        private int coinsSpeed = 7;
        private SoundPlayer coinSound;
        private SoundPlayer crashSound;
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Interval = 20;
            timer.Start();
            // Initialize sound players with correct file paths
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            coinSound = new SoundPlayer(basePath + "coin.wav");
            crashSound = new SoundPlayer(basePath + "crash.wav");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isGameOver)
            {
                return;
            }

            moveLine(5);
            frameCount++;

            //spawn new obstacle at regular intervals
            if (frameCount % obstacleSpawnInterval == 0)
            {
                SpawnObstacle();
            }

            //spawn new coins at regular intervals
            if (frameCount % coinsSpawnInterval == 0)
            {
                SpawnCoins();
            }

            MoveObstacles();
            MoveCoins();

            CheckCollisions();
            CheckForCoins();
        }

        void moveLine(int speed)
        {
            if (pictureBox1.Top >= 1070)
            {
                pictureBox1.Top = 0;
            }
            else
            {
                pictureBox1.Top += speed;
            }

            if (pictureBox2.Top >= 1070)
            {
                pictureBox2.Top = 0;
            }
            else
            {
                pictureBox2.Top += speed;
            }

            if (pictureBox3.Top >= 1070)
            {
                pictureBox3.Top = 0;
            }
            else
            {
                pictureBox3.Top += speed;
            }

            if (pictureBox4.Top >= 1070)
            {
                pictureBox4.Top = 0;
            }
            else
            {
                pictureBox4.Top += speed;
            }

            if (pictureBox5.Top >= 1070)
            {
                pictureBox5.Top = 0;
            }
            else
            {
                pictureBox5.Top += speed;
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            BackColor = Color.Transparent;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isGameOver)
            {
                return;
            }

            int x = pictureBoxCar.Location.X;
            int y = pictureBoxCar.Location.Y;

            switch (e.KeyCode) 
            {
                case Keys.Left:
                    x -= 10;
                    break;
                case Keys.Right:
                    x += 10;
                    break;
                /*case Keys.Up:
                    y -= 10;
                    break;
                case Keys.Down:
                    y += 10;
                    break;*/
            }

            pictureBoxCar.Location = new Point(x, y);
        }

        private void SpawnObstacle()
        {
            Obstacle newObstacle = new Obstacle(this);
            obstacles.Add(newObstacle);
        }

        private void MoveObstacles()
        {
            //Move existing obstacles
            for (int i = obstacles.Count - 1; i >= 0; i--)
            {
                obstacles[i].Move(obstacleSpeed);

                //remove obstacle if it goes off screen
                if (obstacles[i].IsOffScreen())
                {
                    obstacles[i].Remove();
                    obstacles.RemoveAt(i);
                }
            }
        }

        private void MoveCoins()
        {
            //Move existing coins
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                coins[i].Move(coinsSpeed);

                //remove coins if it goes off screen
                if (coins[i].IsOffScreen())
                {
                    coins[i].Remove();
                    coins.RemoveAt(i);
                }
            }
        }

        private void CheckCollisions()
        {
            foreach (var obstacle in obstacles)
            {
                if (pictureBoxCar.Bounds.IntersectsWith(obstacle.PictureBoxObstacle.Bounds))
                {
                    crashSound.Play();
                    isGameOver = true;
                    timer.Stop();
                    MessageBox.Show("GAME OVER!");
                    break;
                }
            }
        }

        private void SpawnCoins()
        {
            Coins newCoin = new Coins(this);
            coins.Add(newCoin);
        }

        private void CheckForCoins()
        {
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                var coin = coins[i];
                if (pictureBoxCar.Bounds.IntersectsWith(coin.PictureBoxCoins.Bounds))
                {
                    CollectCoin(coin);
                }
            }
        }

        private void CollectCoin(Coins coin)
        {
            coinsCollected++;
            labelCoinCount.Text = "Coins Collected: " + coinsCollected.ToString();
            coinSound.Play();
            coin.PictureBoxCoins.Visible = false;
            coins.Remove(coin);
        }
    }
}
