using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChatBot.Helpers
{
    /// <summary>
    /// Markdown formatını RichTextBox'a uygulayan yardımcı sınıf
    /// </summary>
    public static class MarkdownFormatter
    {
        /// <summary>
        /// Markdown formatlı metni RichTextBox'a ekler
        /// </summary>
        /// <param name="rtb">RichTextBox kontrolü</param>
        /// <param name="text">Markdown formatlı metin</param>
        /// <param name="textColor">Varsayılan metin rengi</param>
        public static void AppendMarkdownText(RichTextBox rtb, string text, Color textColor)
        {
            if (string.IsNullOrEmpty(text))
                return;

            Font defaultFont = rtb.Font;
            int startPos = rtb.TextLength;
            
            // Indent ayarlarını koru (padding için)
            int savedIndent = rtb.SelectionIndent;
            int savedRightIndent = rtb.SelectionRightIndent;

            // Önce kod bloklarını işle ve yer tutucularla değiştir
            var codeBlocks = new System.Collections.Generic.List<string>();
            var codeBlockPattern = @"```(\w+)?\s*\n(.*?)```";
            var codeMatches = Regex.Matches(text, codeBlockPattern, RegexOptions.Singleline);
            
            int blockIndex = 0;
            foreach (Match match in codeMatches)
            {
                codeBlocks.Add(match.Groups[2].Value);
                text = text.Replace(match.Value, $"{{CODE_BLOCK_{blockIndex}}}");
                blockIndex++;
            }

            // Satır satır işle
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                
                if (string.IsNullOrEmpty(line))
                {
                    rtb.AppendText(Environment.NewLine);
                    continue;
                }

                // Kod bloğu placeholder kontrolü
                var codeBlockMatch = Regex.Match(line, @"{CODE_BLOCK_(\d+)}");
                if (codeBlockMatch.Success)
                {
                    int blockIdx = int.Parse(codeBlockMatch.Groups[1].Value);
                    AppendCodeBlock(rtb, codeBlocks[blockIdx], defaultFont);
                    continue;
                }

                // Liste kontrolü
                if (Regex.IsMatch(line, @"^[\*\-\+]\s+"))
                {
                    AppendBulletListItem(rtb, line, defaultFont, textColor);
                }
                else if (Regex.IsMatch(line, @"^\d+\.\s+"))
                {
                    AppendNumberedListItem(rtb, line, defaultFont, textColor);
                }
                else if (line.TrimStart().StartsWith("#"))
                {
                    AppendHeader(rtb, line, defaultFont, textColor);
                }
                else
                {
                    // Normal metin - Markdown formatlarını uygula
                    AppendFormattedLine(rtb, line, defaultFont, textColor);
                }

                if (i < lines.Length - 1)
                    rtb.AppendText(Environment.NewLine);
            }
        }

        /// <summary>
        /// Kod bloğu ekler
        /// </summary>
        private static void AppendCodeBlock(RichTextBox rtb, string code, Font defaultFont)
        {
            rtb.SelectionStart = rtb.TextLength;
            rtb.SelectionFont = new Font("Consolas", defaultFont.Size, FontStyle.Regular);
            rtb.SelectionColor = Color.FromArgb(100, 200, 100); // Açık yeşil (dark mode için)
            rtb.SelectionBackColor = Color.FromArgb(50, 50, 50); // Koyu gri arka plan
            rtb.AppendText(code);
            rtb.SelectionBackColor = rtb.BackColor;
            rtb.SelectionFont = defaultFont;
            rtb.SelectionColor = rtb.ForeColor;
        }

        /// <summary>
        /// Bullet list öğesi ekler
        /// </summary>
        private static void AppendBulletListItem(RichTextBox rtb, string line, Font defaultFont, Color defaultColor)
        {
            string content = Regex.Replace(line, @"^[\*\-\+]\s+", "• ");
            AppendFormattedLine(rtb, content, defaultFont, defaultColor);
        }

        /// <summary>
        /// Numaralı list öğesi ekler
        /// </summary>
        private static void AppendNumberedListItem(RichTextBox rtb, string line, Font defaultFont, Color defaultColor)
        {
            AppendFormattedLine(rtb, line, defaultFont, defaultColor);
        }

        /// <summary>
        /// Başlık ekler
        /// </summary>
        private static void AppendHeader(RichTextBox rtb, string line, Font defaultFont, Color defaultColor)
        {
            int level = 0;
            string trimmed = line.TrimStart();
            while (level < trimmed.Length && trimmed[level] == '#')
                level++;

            string content = trimmed.Substring(level).Trim();
            float fontSize = Math.Max(defaultFont.Size, defaultFont.Size + (4 - level) * 2);

            rtb.SelectionStart = rtb.TextLength;
            rtb.SelectionFont = new Font(defaultFont.FontFamily, fontSize, FontStyle.Bold);
            rtb.SelectionColor = Color.FromArgb(100, 150, 255); // Açık mavi (dark mode için)
            rtb.AppendText(content);
            rtb.SelectionFont = defaultFont;
            rtb.SelectionColor = defaultColor;
        }

        /// <summary>
        /// Markdown formatlarını (kalın, italik, inline kod) uygular
        /// </summary>
        private static void AppendFormattedLine(RichTextBox rtb, string line, Font defaultFont, Color defaultColor)
        {
            if (string.IsNullOrEmpty(line))
                return;

            // Basit parsing: sırayla işle
            int pos = 0;
            while (pos < line.Length)
            {
                // Kod bloğu placeholder kontrolü
                if (line.Substring(pos).StartsWith("{CODE_BLOCK_"))
                {
                    pos += 15; // "{CODE_BLOCK_" uzunluğu + index
                    continue;
                }

                // Inline kod `kod`
                int codeStart = line.IndexOf('`', pos);
                if (codeStart >= 0)
                {
                    // Önceki metni ekle
                    if (codeStart > pos)
                    {
                        AppendPlainText(rtb, line.Substring(pos, codeStart - pos), defaultFont, defaultColor);
                    }

                    int codeEnd = line.IndexOf('`', codeStart + 1);
                    if (codeEnd > codeStart)
                    {
                        string code = line.Substring(codeStart + 1, codeEnd - codeStart - 1);
                        rtb.SelectionStart = rtb.TextLength;
                        rtb.SelectionFont = new Font("Consolas", defaultFont.Size, FontStyle.Regular);
                        rtb.SelectionColor = Color.FromArgb(255, 150, 150); // Açık kırmızı (dark mode için)
                        rtb.SelectionBackColor = Color.FromArgb(50, 50, 50); // Koyu gri arka plan
                        // Indent ayarlarını koru
                        int savedIndent = rtb.SelectionIndent;
                        int savedRightIndent = rtb.SelectionRightIndent;
                        rtb.AppendText(code);
                        rtb.SelectionBackColor = rtb.BackColor;
                        rtb.SelectionIndent = savedIndent;
                        rtb.SelectionRightIndent = savedRightIndent;
                        pos = codeEnd + 1;
                        continue;
                    }
                }

                // Kalın metin **text**
                int boldStart = line.IndexOf("**", pos);
                if (boldStart >= 0)
                {
                    int boldEnd = line.IndexOf("**", boldStart + 2);
                    if (boldEnd > boldStart)
                    {
                        // Önceki metni ekle
                        if (boldStart > pos)
                        {
                            AppendPlainText(rtb, line.Substring(pos, boldStart - pos), defaultFont, defaultColor);
                        }

                        string boldText = line.Substring(boldStart + 2, boldEnd - boldStart - 2);
                        rtb.SelectionStart = rtb.TextLength;
                        rtb.SelectionFont = new Font(defaultFont, FontStyle.Bold);
                        rtb.SelectionColor = defaultColor;
                        // Indent ayarlarını koru
                        int savedIndent = rtb.SelectionIndent;
                        int savedRightIndent = rtb.SelectionRightIndent;
                        rtb.AppendText(boldText);
                        rtb.SelectionIndent = savedIndent;
                        rtb.SelectionRightIndent = savedRightIndent;
                        pos = boldEnd + 2;
                        continue;
                    }
                }

                // İtalik metin *text* (tek yıldız, ** değil)
                int italicStart = -1;
                for (int i = pos; i < line.Length - 1; i++)
                {
                    if (line[i] == '*' && (i == 0 || line[i - 1] != '*') && (i == line.Length - 1 || line[i + 1] != '*'))
                    {
                        italicStart = i;
                        break;
                    }
                }

                if (italicStart >= 0)
                {
                    int italicEnd = -1;
                    for (int i = italicStart + 1; i < line.Length; i++)
                    {
                        if (line[i] == '*' && (i == line.Length - 1 || line[i + 1] != '*'))
                        {
                            italicEnd = i;
                            break;
                        }
                    }

                    if (italicEnd > italicStart)
                    {
                        // Önceki metni ekle
                        if (italicStart > pos)
                        {
                            AppendPlainText(rtb, line.Substring(pos, italicStart - pos), defaultFont, defaultColor);
                        }

                        string italicText = line.Substring(italicStart + 1, italicEnd - italicStart - 1);
                        rtb.SelectionStart = rtb.TextLength;
                        rtb.SelectionFont = new Font(defaultFont, FontStyle.Italic);
                        rtb.SelectionColor = defaultColor;
                        // Indent ayarlarını koru
                        int savedIndent = rtb.SelectionIndent;
                        int savedRightIndent = rtb.SelectionRightIndent;
                        rtb.AppendText(italicText);
                        rtb.SelectionIndent = savedIndent;
                        rtb.SelectionRightIndent = savedRightIndent;
                        pos = italicEnd + 1;
                        continue;
                    }
                }

                // Formatlanmamış metin kaldı
                if (pos < line.Length)
                {
                    AppendPlainText(rtb, line.Substring(pos), defaultFont, defaultColor);
                    break;
                }
            }
        }

        /// <summary>
        /// Düz metin ekler
        /// </summary>
        private static void AppendPlainText(RichTextBox rtb, string text, Font font, Color color)
        {
            if (string.IsNullOrEmpty(text))
                return;

            rtb.SelectionStart = rtb.TextLength;
            rtb.SelectionFont = font;
            rtb.SelectionColor = color;
            rtb.AppendText(text);
        }
    }
}
