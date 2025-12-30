using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatBot.Models;
using ChatBot.Services;

namespace ChatBot
{
    public partial class Form1 : Form
    {
        private readonly GeminiApiService _apiService;
        private bool _isProcessing = false;

        /// <summary>
        /// Form constructor - API servisini başlatır
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            _apiService = new GeminiApiService();
            InitializeChat();
        }

        /// <summary>
        /// Chat ekranını başlangıç durumuna getirir
        /// </summary>
        private void InitializeChat()
        {
            txtChat.Text = "ChatBot'a hoş geldiniz! Mesajınızı yazıp 'Gönder' butonuna tıklayın.\r\n\r\n";
            txtChat.ForeColor = Color.FromArgb(100, 100, 100);
            lblTyping.Visible = false;
        }

        /// <summary>
        /// Mesajı chat ekranına ekler (kullanıcı veya bot)
        /// </summary>
        /// <param name="message">Mesaj metni</param>
        /// <param name="sender">Mesaj gönderen (User veya Bot)</param>
        private void AddMessageToChat(string message, MessageSender sender)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            // Mesaj formatı: [Zaman] Gönderen: Mesaj
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string senderName = sender == MessageSender.User ? "Siz" : "Bot";
            Color messageColor = sender == MessageSender.User ? Color.FromArgb(0, 122, 204) : Color.FromArgb(50, 50, 50);

            // Mesajı ekle
            string formattedMessage = $"[{timestamp}] {senderName}: {message}\r\n\r\n";
            
            // Mevcut mesajların rengini koru, yeni mesajı ekle
            txtChat.Text += formattedMessage;
            
            // Scroll'u en alta al
            txtChat.SelectionStart = txtChat.Text.Length;
            txtChat.ScrollToCaret();
        }

        /// <summary>
        /// "Yazıyor..." durumunu gösterir/gizler
        /// </summary>
        /// <param name="show">Gösterilecek mi?</param>
        private void ShowTypingIndicator(bool show)
        {
            if (show)
            {
                lblTyping.Text = "Bot yazıyor...";
                lblTyping.Visible = true;
            }
            else
            {
                lblTyping.Text = "";
                lblTyping.Visible = false;
            }
            Application.DoEvents();
        }

        /// <summary>
        /// Mesaj gönderme işlemini async olarak gerçekleştirir
        /// </summary>
        /// <param name="userMessage">Kullanıcı mesajı</param>
        private async Task SendMessageAsync(string userMessage)
        {
            if (_isProcessing)
                return;

            _isProcessing = true;
            btnSend.Enabled = false;
            txtMessage.Enabled = false;

            try
            {
                // Kullanıcı mesajını chat ekranına ekle
                AddMessageToChat(userMessage, MessageSender.User);

                // "Yazıyor..." durumunu göster
                ShowTypingIndicator(true);

                // API'ye istek gönder (async)
                string botResponse = await _apiService.SendMessageAsync(userMessage);

                // "Yazıyor..." durumunu gizle
                ShowTypingIndicator(false);

                // Bot yanıtını chat ekranına ekle
                AddMessageToChat(botResponse, MessageSender.Bot);
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bilgi ver
                ShowTypingIndicator(false);
                string errorMessage = $"Hata: {ex.Message}";
                AddMessageToChat(errorMessage, MessageSender.Bot);
                
                // Hata mesajını kırmızı renkte göster
                MessageBox.Show($"Mesaj gönderilirken bir hata oluştu:\n\n{ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isProcessing = false;
                btnSend.Enabled = true;
                txtMessage.Enabled = true;
                txtMessage.Focus();
            }
        }

        /// <summary>
        /// Gönder butonu tıklama event handler'ı
        /// </summary>
        private async void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Lütfen bir mesaj yazın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mesaj kutusunu temizle
            txtMessage.Text = "";

            // Mesajı gönder
            await SendMessageAsync(message);
        }

        /// <summary>
        /// Enter tuşu ile mesaj gönderme
        /// </summary>
        private async void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                string message = txtMessage.Text.Trim();
                
                if (!string.IsNullOrWhiteSpace(message))
                {
                    txtMessage.Text = "";
                    await SendMessageAsync(message);
                }
            }
        }

        /// <summary>
        /// Form kapatılırken kaynakları temizle
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _apiService?.Dispose();
            base.OnFormClosing(e);
        }
    }
}


