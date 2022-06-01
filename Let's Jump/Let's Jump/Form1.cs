using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Let_s_Jump
{
    public partial class Starting : Form
    {
        //private const string V = "obstacle";
        bool jumping = false;
        bool dead = false;
        int jumpSpeed = 18; 
        int force = 20; 
        int obstacleSpeed = 150; 
        Random rnd = new Random();
        int score = 0, bestScore = 0;
       

        public void resetGame()
        {
            dead = false;
            force = 20; 
            pictureBox4.Top = pictureBox8.Top - pictureBox4.Height; 
            jumpSpeed = 0; 
            jumping = false;
            score = 0;
            obstacleSpeed = 15;
            pictureBox4.Image = Properties.Resources.Running;
         
            label2.Visible = true;
            label2.Text = $"{score}";
           
                foreach (Control x in this.Controls)
            {
                if (x is PictureBox &&(string) x.Tag=="obstacle"){
                  
                    int position = rnd.Next(300, 600);
                    
                    x.Left = 300 + (x.Left + position + x.Width * 3);
                    }
           
            }

         timer3.Start(); 
        }
        
        public Starting()
        {
            InitializeComponent();
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox4.BackColor= Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            button_WOC1.Visible = false;
            button_WOC2.Visible = false;
            button_WOC1.Enabled = false;
            button_WOC2.Enabled = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
          //  pictureBox5.Visible = true;
           //pictureBox3.Visible = false;
            timer1.Start();
            resetGame();
            label1.Visible = false;
            label2.Visible = true;
            label3.Visible = true;



        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
          
            if (MessageBox.Show("Are You Sure You Want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            pictureBox3.Visible = false;
            pictureBox4.Visible=true;
           // pictureBox5.Visible=true;
            pictureBox6.Visible = true;
            pictureBox7.Visible = true;
       

            timer1.Stop();
        }

        private void Starting_FormClosing(object sender, FormClosingEventArgs e)
        {
            

            if (MessageBox.Show("Are You Sure You Want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            
        }

        private void Starting_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void gameEvent(object sender, EventArgs e)
        {

            pictureBox4.Top += jumpSpeed;

            
            if (jumping)
            {
                jumpSpeed = -18;
                force -= 1;
            }
            else
            {
                jumpSpeed = 18;
            }

            if (jumping && force < 0)
            {
                jumping = false;
            }

            foreach (Control x in this.Controls )
            {

                if (x is PictureBox && (string)x.Tag == "obstacle" )
                {
                    {
                        x.Left -= obstacleSpeed;
                    }

                    if (x.Left + x.Width < -120)
                    {
                        x.Left = this.ClientSize.Width + rnd.Next(200, 800);
                     score+=10;
                        label2.Text =score.ToString();
                    }
               
                    
                    if (pictureBox4.Bounds.IntersectsWith(x.Bounds))
                    {
                        
                            timer3.Stop();
                            dead = true;
                            pictureBox4.Image = Properties.Resources.dizzy;
                            //pictureBox5.Visible = false;
                            label1.Visible = true;
                            label6.Visible = true;
                        
                        
                        if (score > bestScore)

                        {
                            bestScore = score;
                            label4.Visible = true;
                            label5.Visible = true;
                            label5.Text = score.ToString();
                        }
                    }
                }
            }

            
            if (pictureBox4.Top >= 360 && !jumping)
            {
               
                force = 20; 
                pictureBox4.Top = pictureBox8.Top - pictureBox4.Height; 
                jumpSpeed = 0; 
            }

            if (score >= 50 && score<100)
                obstacleSpeed = 20;
            
            if (score >= 100 && score<150)
                obstacleSpeed = 25;

            if (score >= 150 && score < 200)
                obstacleSpeed = 30;

            if (score >= 200)
                obstacleSpeed = 35;
        }
                
        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R && dead)
            {
                resetGame();
                label1.Visible = false;
                label6.Visible = false;
            }
            
            if (jumping)
            {
                jumping = false;
                pictureBox4.Image = Properties.Resources.Running;
            }
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !dead)
            {
                jumping = true;
                pictureBox4.Image = Properties.Resources.jumping;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e) { 

        }
    }
}
 