using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {

        int gravity = 10;
        int pipeSpeed = 6;
        int score = 0;
        int scoreUpperBound = 5;

        Random rnd = new Random();

        bool increasedScore = false;

        public Form1()
        {
            InitializeComponent();
            restartGame();
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeDown.Left -= pipeSpeed;
            pipeUp.Left -= pipeSpeed;
            scoreLabel.Text = "Score: " + score.ToString();

            if (flappyBird.Left > pipeDown.Right && !increasedScore)
            {
                score++;
                increasedScore = true;
            }

            if (flappyBird.Left > pipeUp.Right && !increasedScore)
            {
                score++;
                increasedScore = true;
            }
            
            if (pipeDown.Left < -120)
            {
                pipeDown.Left = rnd.Next(800,1300);

                increasedScore = false;
            }

            if (pipeUp.Left < -135)
            {
                pipeUp.Left = rnd.Next(1000, 1300);
                increasedScore = false;
            }

            if (score == scoreUpperBound)
            {
                pipeSpeed += 3;
                scoreUpperBound += 3;
            }


            if (flappyBird.Bounds.IntersectsWith(pipeDown.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeUp.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) ||
                flappyBird.Top < -25)
            {
                endGame();
            }
        }

        private void gameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -10;
            }
        }

        private void gameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 10;
            }
        }

        private void restartGame()
        {
            flappyBird.Location = new Point(75,57);

            pipeDown.Left = 653;
            pipeUp.Left = 819;

            score = 0;
            scoreUpperBound = 5;
            pipeSpeed = 6;
            scoreLabel.Text = "Score: " + score;
            increasedScore = false;
            gameTimer.Start();

            restartImage.Enabled = false;
            restartImage.Visible = false;
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreLabel.Text += " Game Over !!!!";

            restartImage.Enabled = true;
            restartImage.Visible = true;
        }

        private void restartClickEvent(object sender, EventArgs e)
        {
            restartGame();
        }
    }
}
