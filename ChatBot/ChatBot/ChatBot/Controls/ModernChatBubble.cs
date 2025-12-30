using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ChatBot.Controls
{
    public class ModernChatBubble : UserControl
    {
        private string _message;
        private string _timestamp;
        private bool _isUserMessage;
        
        // ChatGPT Colors
        private readonly Color _userBackColor = Color.FromArgb(0, 122, 255); // Blue
        private readonly Color _botBackColor = Color.FromArgb(68, 70, 84);   // Dark Gray
        private readonly Color _textColor = Color.FromArgb(236, 236, 241);    // Light Text
        private readonly Font _messageFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        private readonly Font _timeFont = new Font("Segoe UI", 8F, FontStyle.Regular);

        public ModernChatBubble(string message, string timestamp, bool isUserMessage)
        {
            _message = message;
            _timestamp = timestamp;
            _isUserMessage = isUserMessage;

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.Padding = new Padding(15);
            this.BackColor = Color.Transparent;
            
            // Calculate size based on content
            CalculateSize();
        }

        public void SetMarkdownText(string text)
        {
            _message = text;
            CalculateSize();
            this.Invalidate();
        }

        private Size _bubbleSize;
        private const int AVATAR_SIZE = 35;
        private const int HORIZONTAL_PADDING = 20;
        private const int VERTICAL_PADDING = 20;
        private const int BOTTOM_MARGIN = 15; // Space between messages

        private void CalculateSize()
        {
            // Define a max width for the text bubble itself (e.g., 70% of parent/screen)
            // Since we are docked, this.Width might be large (window width). 
            // We'll trust the measurement to be reasonable or limit it.
            int maxBubbleWidth = 600; 

            using (Graphics g = CreateGraphics())
            {
                // Measure the text exactly
                SizeF textSize = g.MeasureString(_message, _messageFont, maxBubbleWidth);
                
                // Calculate dimensions of the colored bubble part
                int bubbleWidth = (int)textSize.Width + HORIZONTAL_PADDING * 2;
                int bubbleHeight = (int)textSize.Height + VERTICAL_PADDING * 2 + 15; // +15 for time space

                _bubbleSize = new Size(bubbleWidth, bubbleHeight);

                // Control Height = Bubble Height + Bottom Margin
                // Control Width is handled by Dock=Bottom, but we need to set Height explicitly
                this.Height = bubbleHeight + BOTTOM_MARGIN;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Colors
            Color backColor = _isUserMessage ? _userBackColor : _botBackColor;
            
            // Calculate Bubble Position
            // The control width is full width (due to Dock=Bottom). 
            // We place the bubble left or right based on that.
            
            Rectangle bubbleRect;
            Rectangle avatarRect;
            
            if (_isUserMessage)
            {
                // Right aligned
                // Avatar on absolute right
                avatarRect = new Rectangle(this.Width - AVATAR_SIZE - 10, 0, AVATAR_SIZE, AVATAR_SIZE);
                
                // Bubble to the left of avatar
                int bubbleX = this.Width - AVATAR_SIZE - 10 - 10 - _bubbleSize.Width;
                bubbleRect = new Rectangle(bubbleX, 0, _bubbleSize.Width, _bubbleSize.Height);
            }
            else
            {
                // Left aligned
                // Avatar on absolute left
                avatarRect = new Rectangle(10, 0, AVATAR_SIZE, AVATAR_SIZE);
                
                // Bubble to the right of avatar
                bubbleRect = new Rectangle(10 + AVATAR_SIZE + 10, 0, _bubbleSize.Width, _bubbleSize.Height);
            }

            // Draw Avatar Background (Circle)
            using (Brush avatarBrush = new SolidBrush(backColor))
            {
                g.FillEllipse(avatarBrush, avatarRect);
            }
            
            // Draw Avatar Initials
            string initials = _isUserMessage ? "U" : "AI";
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.DrawString(initials, new Font("Segoe UI", 9, FontStyle.Bold), Brushes.White, avatarRect, sf);
            }

            // Draw Bubble Background (Rounded)
            int radius = 18;
            using (GraphicsPath path = GetRoundedPath(bubbleRect, radius))
            using (Brush brush = new SolidBrush(backColor))
            {
                g.FillPath(brush, path);
            }

            // Draw Text
            // Center text somewhat within the padding
            Rectangle textRect = new Rectangle(
                bubbleRect.X + HORIZONTAL_PADDING, 
                bubbleRect.Y + VERTICAL_PADDING - 5, 
                bubbleRect.Width - (HORIZONTAL_PADDING * 2), 
                bubbleRect.Height - (VERTICAL_PADDING * 2) 
            );
            
            TextRenderer.DrawText(g, _message, _messageFont, textRect, _textColor, TextFormatFlags.WordBreak | TextFormatFlags.Left);

            // Draw Timestamp
            Rectangle timeRect = new Rectangle(
                bubbleRect.X + 10, 
                bubbleRect.Bottom - 20, 
                bubbleRect.Width - 25, 
                15
            );
            TextRenderer.DrawText(g, _timestamp, _timeFont, timeRect, Color.FromArgb(180, 180, 180), TextFormatFlags.Right | TextFormatFlags.Bottom);
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
