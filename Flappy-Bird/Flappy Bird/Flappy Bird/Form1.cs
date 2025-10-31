using System.Media;
using System.IO;

namespace Flappy_Bird
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8;
        int basePipeSpeed = 8;
        int gravity = 15;
        int score = 0;
        bool isGameOver = false;
        bool isStarted = false;
        bool scoreAwardedForThisPair = false;
        int pipeGap = 160;
        readonly Random random = new Random();
        int birdFrameIndex = 0;
        string? audioDirectory;
        int highScore = 0;
        string? highScoreFilePath;


        public Form1()
        {
            InitializeComponent();
            audioDirectory = FindAudioDir();
            highScoreFilePath = BuildHighScorePath();
            LoadHighScore();
            UpdateScoreText();
            // Başlangıçta duraklatılmış mod
            gameTimer.Enabled = false;
            birdAnimTimer.Enabled = false;
            if (startOverlay != null) startOverlay.Visible = true;
            if (gameOverOverlay != null) gameOverOverlay.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            UpdateScoreText();

            // Borular ekran dışına çıktıysa birlikte yeniden konumlandır
            if (pipeTop.Right < 0 || pipeBottom.Right < 0)
            {
                RepositionPipesWithRandomGap();
            }

            // Skoru yalnızca kuş boru çiftini geçtiğinde bir kez artır
            if (!scoreAwardedForThisPair && pipeTop.Right < flappyBird.Left)
            {
                score++;
                scoreAwardedForThisPair = true;

                // Zorluk ölçekleme: her 5 skorda hızı artır (üst sınır 18)
                pipeSpeed = Math.Min(18, basePipeSpeed + score / 5);

                // Puan sesi
                PlaySound("point");
                UpdateScoreText();
            }

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds))
            {
                endGame();
            }
            if (flappyBird.Top < -25)
            {
                endGame();
            }


        }

        private void gameKeyIsDown(object sender, KeyEventArgs e)
        {
            if (!isStarted && e.KeyCode == Keys.Space)
            {
                StartRun();
                return;
            }
            if (e.KeyCode == Keys.R && isGameOver)
            {
                resetGame();
                return;
            }

            if (e.KeyCode == Keys.Space)
            {
                if (isGameOver)
                {
                    resetGame();
                    return;
                }
                gravity = -13;
                PlaySound("wing");
            }
        }

        private void gameKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                gravity = 13;
        }

        private void endGame()
        {
            gameTimer.Stop();
            SaveHighScoreIfNeeded();
            scoreText.Text += " Oyun Bitti ";
            isGameOver = true;
            PlaySound("hit");
            if (gameOverOverlay != null) gameOverOverlay.Visible = true;
            if (startOverlay != null) startOverlay.Visible = false;
            birdAnimTimer.Enabled = false;
        }

        private void scoreText_Click(object sender, EventArgs e)
        {

        }

        private void resetGame()
        {
            // başlangıç değerleri
            gravity = 15;
            score = 0;
            isGameOver = false;
            UpdateScoreText();
            pipeSpeed = basePipeSpeed;
            scoreAwardedForThisPair = false;

            // kuşu ve boruları başlangıç/sağ tarafa al
            flappyBird.Location = new Point(280, 220);
            RepositionPipesWithRandomGap(initial: true);

            // başlama ekranına dön
            isStarted = false;
            gameTimer.Enabled = false;
            birdAnimTimer.Enabled = false;
            if (startOverlay != null) startOverlay.Visible = true;
            if (gameOverOverlay != null) gameOverOverlay.Visible = false;
        }

        private void RepositionPipesWithRandomGap(bool initial = false)
        {
            // Borular aynı x'de hizalı, y konumları rastgele ve aralarında sabit boşluk var
            // Daha sık boru gelişi için ekrana daha yakın yeniden doğsun
            int x = (initial ? 780 : 700) + random.Next(0, 140);
            int topY = random.Next(-220, -60); // üst boruyu yukarı-aşağı oynat
            pipeTop.Location = new Point(initial ? x : pipeTop.Right < 0 ? x : x, topY);

            int bottomY = topY + pipeTop.Height + pipeGap;
            pipeBottom.Location = new Point(x, bottomY);

            // Yeni çift için skor ödülü sıfırlansın
            scoreAwardedForThisPair = false;
        }

        private void birdAnimTimer_Tick(object? sender, EventArgs e)
        {
            // 3 frame: downflap, midflap, upflap
            birdFrameIndex = (birdFrameIndex + 1) % 3;
            switch (birdFrameIndex)
            {
                case 0:
                    flappyBird.Image = Properties.Resources.redbird_downflap;
                    break;
                case 1:
                    flappyBird.Image = Properties.Resources.redbird_midflap;
                    break;
                case 2:
                    flappyBird.Image = Properties.Resources.redbird_upflap;
                    break;
            }
        }

        private string? FindAudioDir()
        {
            string? dir = AppContext.BaseDirectory;
            for (int i = 0; i < 10 && dir != null; i++)
            {
                string candidate = Path.Combine(dir, "flappy-bird-assets-master", "audio");
                if (Directory.Exists(candidate))
                {
                    return candidate;
                }
                dir = Directory.GetParent(dir)?.FullName;
            }
            return null;
        }

        private void PlaySound(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(audioDirectory)) return;
                string path = Path.Combine(audioDirectory, name + ".wav");
                if (!File.Exists(path)) return;
                using SoundPlayer player = new SoundPlayer(path);
                player.Play();
            }
            catch
            {
                // sessizce geç
            }
        }

        private void StartRun()
        {
            isStarted = true;
            gameTimer.Enabled = true;
            birdAnimTimer.Enabled = true;
            if (startOverlay != null) startOverlay.Visible = false;
            if (gameOverOverlay != null) gameOverOverlay.Visible = false;
        }

        private string? BuildHighScorePath()
        {
            try
            {
                string root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = Path.Combine(root, "FlappyBirdSEC");
                Directory.CreateDirectory(dir);
                return Path.Combine(dir, "highscore.txt");
            }
            catch
            {
                return null;
            }
        }

        private void LoadHighScore()
        {
            try
            {
                if (string.IsNullOrEmpty(highScoreFilePath)) return;
                if (File.Exists(highScoreFilePath))
                {
                    string content = File.ReadAllText(highScoreFilePath).Trim();
                    if (int.TryParse(content, out int parsed))
                    {
                        highScore = Math.Max(0, parsed);
                    }
                }
            }
            catch
            {
            }
        }

        private void SaveHighScoreIfNeeded()
        {
            try
            {
                if (score > highScore)
                {
                    highScore = score;
                    if (!string.IsNullOrEmpty(highScoreFilePath))
                    {
                        File.WriteAllText(highScoreFilePath, highScore.ToString());
                    }
                }
            }
            catch
            {
            }
        }

        private void UpdateScoreText()
        {
            scoreText.Text = $"Score : {score}   Best : {highScore}";
        }
    }
}
