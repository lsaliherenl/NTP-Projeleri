using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatBot.Models;
using ChatBot.Services;
using ChatBot.Controls;

namespace ChatBot
{
    public partial class Form1 : Form
    {
        private readonly GeminiApiService _apiService;
        private bool _isProcessing = false;

        public Form1()
        {
            InitializeComponent();
            _apiService = new GeminiApiService();
            ApplyRoundedInput();
            InitializeChat();
        }

        private void InitializeChat()
        {
            scrollPanel.Controls.Clear();
            AddWelcomeMessage();
            lblTyping.Visible = false;
        }

        private void AddWelcomeMessage()
        {
            string welcomeText = "👋 ChatBot'a hoş geldiniz!\n\nMesajınızı yazıp 'Gönder' butonuna tıklayın veya Enter tuşuna basın.";
            AddChatBubble(welcomeText, DateTime.Now.ToString("HH:mm"), false);
        }

        private void ApplyRoundedInput()
        {
            // Input panel rounded corners (optional, but nice)
            GraphicsPath path = new GraphicsPath();
            int radius = 20;
            Rectangle rect = btnSend.ClientRectangle;
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            btnSend.Region = new Region(path);

            // Textbox rounded styling is simulated by the parent panel color + flat style
        }

        private void AddChatBubble(string message, string timestamp, bool isUserMessage)
        {
            var bubble = new ModernChatBubble(message, timestamp, isUserMessage);
            
            // Dock top ensures they stack correctly in the Flow (or docked panel)
            // But we are using a scrollPanel with Dock=Top logic. 
            // Better to simple add them to scrollPanel with Dock=Top and BringToFront to order them (or reverse order).
            // Actually, Dock=Top stacks from bottom up if you don't be careful. 
            // Let's use standard flow: Add to Controls. set Dock=Top. But for Dock=Top to work as "latest at bottom", we usually need to reverse order or use FlowLayoutPanel.
            // The original code used Dock = Top + BringToFront. This adds new control to top of collection (index 0), so it appears at top?
            // Wait, Dock=Top items: The last added control with Dock=Top appears at the TOP.
            // So to have new messages appear at the BOTTOM, we normally use Dock=Bottom ? No.
            // Let's look at previous logic: "scrollPanel.Controls.Add(bubble); bubble.BringToFront();"
            // If you Add() a control, it is at end of collection. BringToFront moves it to index 0.
            // Top-docked controls are stacked based on Z-order. Index 0 is at the Top. 
            // So valid logic for a chat is usually Dock=Bottom (stack up) or use FlowLayoutPanel (stack down).
            
            // Let's switch to Flow logic for better reliability, but let's stick to existing container for now to minimize breakage 
            // unless previous code was weird. 
            // Previous code: bubble.Dock = DockStyle.Top; scrollPanel.Controls.Add(bubble); bubble.BringToFront(); 
            // If new msg is at Index 0, and Dock=Top, it pushes everything else DOWN. So new message is at TOP. 
            // Chat usually wants new message at BOTTOM. 
            
            // To fix this simply:
            bubble.Dock = DockStyle.Bottom; // Newest message at the bottom
            
            scrollPanel.Controls.Add(bubble);
            // Height logic is handled by AutoSize of the bubble + scrollPanel growing? 
            // scrollPanel is inside panelChatContainer (AutoScroll).
            
            ScrollToBottom();
        }

        private void AddBotMessageWithMarkdown(string message, string timestamp)
        {
            var bubble = new ModernChatBubble("", timestamp, false);
            bubble.SetMarkdownText(message);
            bubble.Dock = DockStyle.Bottom; 

            scrollPanel.Controls.Add(bubble);
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            // Allow layout to process
            panelChatContainer.PerformLayout();
            
            // Scroll to bottom
            if(panelChatContainer.VerticalScroll.Visible)
            {
                 panelChatContainer.VerticalScroll.Value = panelChatContainer.VerticalScroll.Maximum;
            }
        }

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
            Application.DoEvents(); // Force UI update
        }

        private async Task SendMessageAsync(string userMessage)
        {
            if (_isProcessing)
                return;

            _isProcessing = true;
            btnSend.Enabled = false;
            txtMessage.Enabled = false;

            try
            {
                string timestamp = DateTime.Now.ToString("HH:mm");
                AddChatBubble(userMessage, timestamp, true);

                ShowTypingIndicator(true);

                string botResponse = await _apiService.SendMessageAsync(userMessage);

                ShowTypingIndicator(false);

                string botTimestamp = DateTime.Now.ToString("HH:mm");
                AddBotMessageWithMarkdown(botResponse, botTimestamp);
            }
            catch (Exception ex)
            {
                ShowTypingIndicator(false);
                string errorMessage = $"❌ Hata: {ex.Message}";
                AddChatBubble(errorMessage, DateTime.Now.ToString("HH:mm"), false);
            }
            finally
            {
                _isProcessing = false;
                btnSend.Enabled = true;
                txtMessage.Enabled = true;
                txtMessage.Focus();
                ScrollToBottom();
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            txtMessage.Text = "";
            await SendMessageAsync(message);
        }

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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _apiService?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
