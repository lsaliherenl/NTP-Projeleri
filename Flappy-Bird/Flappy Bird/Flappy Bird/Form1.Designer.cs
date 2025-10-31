namespace Flappy_Bird
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pipeBottom = new PictureBox();
            flappyBird = new PictureBox();
            pipeTop = new PictureBox();
            ground = new PictureBox();
            scoreText = new Label();
            gameTimer = new System.Windows.Forms.Timer(components);
            birdAnimTimer = new System.Windows.Forms.Timer(components);
            startOverlay = new PictureBox();
            gameOverOverlay = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pipeBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)flappyBird).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pipeTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ground).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startOverlay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gameOverOverlay).BeginInit();
            SuspendLayout();
            // 
            // pipeBottom
            // 
            pipeBottom.BackColor = Color.Transparent;
            pipeBottom.Image = Properties.Resources.pipe_green_bottom;
            pipeBottom.Location = new Point(676, 319);
            pipeBottom.Name = "pipeBottom";
            pipeBottom.Size = new Size(109, 290);
            pipeBottom.SizeMode = PictureBoxSizeMode.StretchImage;
            pipeBottom.TabIndex = 0;
            pipeBottom.TabStop = false;
            // 
            // flappyBird
            // 
            flappyBird.BackColor = Color.Transparent;
            flappyBird.Image = Properties.Resources.redbird_upflap;
            flappyBird.Location = new Point(280, 220);
            flappyBird.Name = "flappyBird";
            flappyBird.Size = new Size(70, 53);
            flappyBird.SizeMode = PictureBoxSizeMode.StretchImage;
            flappyBird.TabIndex = 1;
            flappyBird.TabStop = false;
            flappyBird.Click += pictureBox2_Click;
            // 
            // pipeTop
            // 
            pipeTop.BackColor = Color.Transparent;
            pipeTop.Image = Properties.Resources.pipe_green_top;
            pipeTop.Location = new Point(676, -137);
            pipeTop.Name = "pipeTop";
            pipeTop.Size = new Size(109, 290);
            pipeTop.SizeMode = PictureBoxSizeMode.StretchImage;
            pipeTop.TabIndex = 2;
            pipeTop.TabStop = false;
            pipeTop.Click += pictureBox3_Click;
            // 
            // ground
            // 
            ground.Image = Properties.Resources._base;
            ground.Location = new Point(-2, 512);
            ground.Name = "ground";
            ground.Size = new Size(1110, 169);
            ground.SizeMode = PictureBoxSizeMode.StretchImage;
            ground.TabIndex = 3;
            ground.TabStop = false;
            ground.Click += pictureBox4_Click;
            // 
            // scoreText
            // 
            scoreText.AutoSize = true;
            scoreText.BackColor = Color.Silver;
            scoreText.Font = new Font("Arial Black", 28F, FontStyle.Bold, GraphicsUnit.Point, 162);
            scoreText.Location = new Point(12, 9);
            scoreText.Name = "scoreText";
            scoreText.Size = new Size(257, 67);
            scoreText.TabIndex = 4;
            scoreText.Text = "Score : 0";
            scoreText.Click += scoreText_Click;
            // 
            // gameTimer
            // 
            gameTimer.Enabled = true;
            gameTimer.Interval = 20;
            gameTimer.Tick += gameTimerEvent;
            // 
            // birdAnimTimer
            // 
            birdAnimTimer.Enabled = true;
            birdAnimTimer.Tick += birdAnimTimer_Tick;
            // 
            // startOverlay
            // 
            startOverlay.BackColor = Color.Transparent;
            startOverlay.Image = Properties.Resources.message;
            startOverlay.Location = new Point(280, 111);
            startOverlay.Name = "startOverlay";
            startOverlay.Size = new Size(333, 243);
            startOverlay.SizeMode = PictureBoxSizeMode.StretchImage;
            startOverlay.TabIndex = 5;
            startOverlay.TabStop = false;
            // 
            // gameOverOverlay
            // 
            gameOverOverlay.BackColor = Color.Transparent;
            gameOverOverlay.Image = Properties.Resources.gameover;
            gameOverOverlay.Location = new Point(316, 200);
            gameOverOverlay.Name = "gameOverOverlay";
            gameOverOverlay.Size = new Size(250, 100);
            gameOverOverlay.SizeMode = PictureBoxSizeMode.StretchImage;
            gameOverOverlay.TabIndex = 6;
            gameOverOverlay.TabStop = false;
            gameOverOverlay.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cyan;
            BackgroundImage = Properties.Resources.background_night;
            ClientSize = new Size(882, 606);
            Controls.Add(scoreText);
            Controls.Add(startOverlay);
            Controls.Add(gameOverOverlay);
            Controls.Add(ground);
            Controls.Add(flappyBird);
            Controls.Add(pipeBottom);
            Controls.Add(pipeTop);
            ForeColor = Color.Black;
            Name = "Form1";
            Text = "Flappy Bird - SEC";
            Load += Form1_Load;
            KeyDown += gameKeyIsDown;
            KeyUp += gameKeyIsUp;
            ((System.ComponentModel.ISupportInitialize)pipeBottom).EndInit();
            ((System.ComponentModel.ISupportInitialize)flappyBird).EndInit();
            ((System.ComponentModel.ISupportInitialize)pipeTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)ground).EndInit();
            ((System.ComponentModel.ISupportInitialize)startOverlay).EndInit();
            ((System.ComponentModel.ISupportInitialize)gameOverOverlay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pipeBottom;
        private PictureBox flappyBird;
        private PictureBox pipeTop;
        private PictureBox ground;
        private Label scoreText;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer birdAnimTimer;
        private PictureBox startOverlay;
        private PictureBox gameOverOverlay;
    }
}
