using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace bird2
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8;
        int gravity = 15;
        int score = 0;
        private int backgroundPositionX = 0;
        private int background2PositionX = 642;

        int birdFrame = 0;
        List<Image> birdFrames = new List<Image>();
        private string skin;

        public Form1(string selectedSkin)
        {
            InitializeComponent();
            skin = selectedSkin;
            LoadBirdFrames();
            ConfigureRestartButton();
        }

        private void LoadBirdFrames()
        {
            birdFrames.Clear();

            if (skin == "yellow")
            {
                birdFrames.Add(Properties.Resources.yellowbirdupflap);
                birdFrames.Add(Properties.Resources.yellowbirdmidflap);
                birdFrames.Add(Properties.Resources.yellowbirddownflap);
            }
            else if (skin == "red")
            {
                birdFrames.Add(Properties.Resources.redbirdupflap);
                birdFrames.Add(Properties.Resources.redbirdmidflap);
                birdFrames.Add(Properties.Resources.redbirddownflap);
            }
            else if (skin == "blue")
            {
                birdFrames.Add(Properties.Resources.bluebirdupflap);
                birdFrames.Add(Properties.Resources.bluebirdmidflap);
                birdFrames.Add(Properties.Resources.bluebirddownflap);
            }
            else if (skin == "pink")
            {
                birdFrames.Add(Properties.Resources.pink0);
                birdFrames.Add(Properties.Resources.pink2);
                birdFrames.Add(Properties.Resources.pink1);
            }
        }

        private void ConfigureRestartButton()
        {
            restartButton.Image = Properties.Resources.restart;
            restartButton.BackColor = Color.Transparent;
            restartButton.Visible = false;
            Home.Visible = false;
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -14;
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 14;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game over!!!";
            Home.Visible = true;
            restartButton.Visible = true;
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            flappyBird.Location = new Point(50, 50);
            pipeBottom.Location = new Point(800, ground.Location.Y - pipeBottom.Height);
            pipeTop.Location = new Point(486, -97);
            score = 0;
            pipeSpeed = 8;
            gameTimer.Start();
            scoreText.Text = "Score: " + score;
            restartButton.Visible = false;
        }

        private void PlaySound(string filePath)
        {
            try
            {
                System.Media.SoundPlayer sound = new System.Media.SoundPlayer(filePath);
                sound.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing sound: " + ex.Message);
            }
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            try
            {
                flappyBird.Top += gravity;
                pipeBottom.Left -= pipeSpeed;
                pipeTop.Left -= pipeSpeed;
                scoreText.Text = "Score: " + score;

                if (pipeSpeed == 0)
                {
                    pipeSpeed = 8;
                }

                if (pipeBottom.Left < -150)
                {
                    pipeBottom.Left = 800;
                    score++;
                    ShowScorePopup("+1", pipeBottom.Location);
                }

                if (pipeTop.Left < -180)
                {
                    pipeTop.Left = 950;
                    score++;
                    ShowScorePopup("+1", pipeTop.Location);
                }

                if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                    flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                    flappyBird.Bounds.IntersectsWith(ground.Bounds) || flappyBird.Top < -25)
                {
                    endGame();
                }

                if (score > 5)
                {
                    pipeSpeed = 15;
                }

                backgroundPositionX -= 2;
                background2PositionX -= 2;

                if (backgroundPositionX < -background.Width)
                    backgroundPositionX = background2PositionX + background2.Width;

                if (background2PositionX < -background2.Width)
                    background2PositionX = backgroundPositionX + background.Width;

                background.Location = new Point(backgroundPositionX, background.Location.Y);
                background2.Location = new Point(background2PositionX, background2.Location.Y);

                birdFrame = (birdFrame + 1) % birdFrames.Count;
                flappyBird.Image = birdFrames[birdFrame];
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("DivideByZeroException: " + ex.Message);
            }
        }

        private void ShowScorePopup(string text, Point location)
        {
            Label scorePopup = new Label
            {
                Text = text,
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.Yellow,
                Location = location
            };
            this.Controls.Add(scorePopup);
            Timer popupTimer = new Timer { Interval = 500 };
            popupTimer.Tick += (s, e) =>
            {
                this.Controls.Remove(scorePopup);
                popupTimer.Stop();
            };
            popupTimer.Start();
        }

        private void ShowMenu()
        {
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
            this.Close();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            ShowMenu();
        }
    }
}
