using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.IO;
using System.ComponentModel;

namespace ChatClientWPF
{
    public partial class MainWindow : Window
    {
        TcpClient? client;
        NetworkStream? stream;
        StreamWriter? writer;
        StreamReader? reader;
        Thread? listenThread;

        private string? currentUserName;
        private string currentHost = "127.0.0.1";
        private int currentPort = 5000;
        private string? currentAvatarPath;
        private int unreadCount = 0;
        private bool windowIsActive = true;
        private bool isConnected = false;
        private bool reconnecting = false;

        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            this.Activated += (s, e) => { windowIsActive = true; ResetUnread(); };
            this.Deactivated += (s, e) => { windowIsActive = false; };

            // Başlangıçta otomatik bağlanma kaldırıldı; Ayarlar penceresinden karar verilecek.
        }

        

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            string msg = messageBox.Text.Trim();
            if (!string.IsNullOrEmpty(msg))
            {
                try
                {
                    if (writer != null)
                    {
                        writer.WriteLine("MSG:" + msg);
                        AppendMessageItem(currentUserName ?? "", msg, true);
                    }
                }
                catch
                {
                    AppendText("⚠️ Mesaj gönderilemedi.\n");
                }
                messageBox.Clear();
            }
        }

        private void ListenForMessages()
        {
            try
            {
                string? line;
                while (reader != null && (line = reader.ReadLine()) != null)
                {
                    var msg = line;
                    if (msg.StartsWith("USERS:"))
                    {
                        var part = msg.Substring(6);
                        var names = part.Length == 0 ? Array.Empty<string>() : part.Split('|');
                        Dispatcher.Invoke(() => {
                            // Mevcut listeyi koru; eksik kullanıcıları ekle
                            var existing = new HashSet<string>();
                            for (int i = 0; i < usersList.Items.Count; i++)
                            {
                            var nm = usersList.Items[i]?.GetType().GetProperty("Name")?.GetValue(usersList.Items[i])?.ToString();
                                if (!string.IsNullOrEmpty(nm)) existing.Add(nm);
                            }
                            foreach (var n in names)
                            {
                                if (!string.IsNullOrWhiteSpace(n) && !existing.Contains(n))
                                    usersList.Items.Add(new UserItem(n, null));
                            }
                        });
                    }
                    else if (msg.StartsWith("JOIN:"))
                    {
                        var name = msg.Substring(5);
                    Dispatcher.Invoke(() => usersList.Items.Add(new UserItem(name, null)));
                        Dispatcher.Invoke(() => AppendMessage($"↗ {name} katıldı"));
                    }
                    else if (msg.StartsWith("LEAVE:"))
                    {
                        var name = msg.Substring(6);
                        Dispatcher.Invoke(() => {
                            for (int i = usersList.Items.Count - 1; i >= 0; i--)
                            {
                                var nm = usersList.Items[i]?.GetType().GetProperty("Name")?.GetValue(usersList.Items[i])?.ToString();
                                if (string.Equals(nm, name, StringComparison.Ordinal))
                                    usersList.Items.RemoveAt(i);
                            }
                        });
                        Dispatcher.Invoke(() => AppendMessage($"↘ {name} ayrıldı"));
                    }
                    else if (msg.StartsWith("MSG:"))
                    {
                        var rest = msg.Substring(4);
                        var sep = rest.IndexOf(':');
                        if (sep > 0)
                        {
                            var from = rest.Substring(0, sep);
                            var text = rest.Substring(sep + 1);
                            bool self = !string.IsNullOrWhiteSpace(currentUserName) && string.Equals(from, currentUserName, StringComparison.Ordinal);
                            Dispatcher.Invoke(() => AppendMessageItem(from, text, self));
                        }
                    }
                else if (msg.StartsWith("AVATAR:"))
                {
                    // AVATAR:<name>:<base64>
                    var payload = msg.Substring(7);
                    var sep = payload.IndexOf(':');
                    if (sep > 0)
                    {
                        var name = payload.Substring(0, sep);
                        var b64 = payload.Substring(sep + 1);
                        Dispatcher.Invoke(() =>
                        {
                            for (int i = 0; i < usersList.Items.Count; i++)
                            {
                                var nm = usersList.Items[i]?.GetType().GetProperty("Name")?.GetValue(usersList.Items[i])?.ToString();
                                if (string.Equals(nm, name, StringComparison.Ordinal))
                                {
                                    // replace item keeping same index
                                    usersList.Items.RemoveAt(i);
                                    usersList.Items.Insert(i, new UserItem(name, "data:image/png;base64," + b64));
                                    break;
                                }
                            }
                        });
                    }
                }
                    else if (!msg.StartsWith("USER:")) // beklenmedik USER satırlarını gösterme
                    {
                        Dispatcher.Invoke(() => AppendMessage("→ " + msg));
                    }
                }
            }
            catch
            {
                Dispatcher.Invoke(() => AppendText("⚠️ Sunucu bağlantısı kesildi.\n"));
                isConnected = false;
                Dispatcher.Invoke(UpdateStatusText);
                TriggerReconnect();
            }
        }

        private void AppendText(string text)
        {
            AppendMessage(text.TrimEnd('\n'));
        }

        private void AppendMessage(string text)
        {
            // Geriye dönük: eskiden "→ ad: içerik" şeklinde geliyordu
            bool isSelf = !string.IsNullOrWhiteSpace(currentUserName) && text.StartsWith("← ");
            string content = text;
            int idx = text.IndexOf(':');
            if (idx > 0 && idx + 1 < text.Length)
            {
                int start = idx + 1;
                if (text[start] == ' ') start++;
                if (start < text.Length) content = text.Substring(start);
            }
            var ts = DateTime.Now.ToString("HH:mm");
            messagesList.Items.Add(new MessageItem($"{content}  [{ts}]", isSelf));
            messagesList.ScrollIntoView(messagesList.Items[messagesList.Items.Count - 1]);
            if (!isSelf) IncrementUnreadIfBackground();
        }

        private void AppendMessageItem(string from, string content, bool isSelf)
        {
            var ts = DateTime.Now.ToString("HH:mm");
            var name = string.IsNullOrWhiteSpace(from) ? (currentUserName ?? "") : from;
            messagesList.Items.Add(new MessageItem($"{name}: {content}  [{ts}]", isSelf));
            messagesList.ScrollIntoView(messagesList.Items[messagesList.Items.Count - 1]);
            if (!isSelf) IncrementUnreadIfBackground();
        }

        private void IncrementUnreadIfBackground()
        {
            if (!windowIsActive)
            {
                unreadCount++;
                UpdateWindowTitle();
            }
        }

        private void ResetUnread()
        {
            if (unreadCount != 0)
            {
                unreadCount = 0;
                UpdateWindowTitle();
            }
        }

        private void UpdateWindowTitle()
        {
            var baseTitle = "Chat Uygulaması";
            this.Title = unreadCount > 0 ? $"({unreadCount}) " + baseTitle : baseTitle;
        }

        private void UpdateStatusText()
        {
            if (statusText == null) return;
            statusText.Text = isConnected ? "Bağlı" : (reconnecting ? "Yeniden bağlanılıyor..." : "Bağlı değil");
            statusText.Foreground = isConnected ? new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(16, 185, 129)) : new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(203, 213, 225));
        }

        private void TriggerReconnect()
        {
            if (reconnecting) return;
            reconnecting = true;
            UpdateStatusText();
            new Thread(() =>
            {
                int attempts = 0;
                while (!isConnected && attempts < 10)
                {
                    attempts++;
                    Thread.Sleep(Math.Min(1000 * attempts, 5000));
                    try
                    {
                        Dispatcher.Invoke(() => TryConnect());
                    }
                    catch { }
                }
                reconnecting = false;
                Dispatcher.Invoke(UpdateStatusText);
            }) { IsBackground = true }.Start();
        }

        private void MessageBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift) && !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift))
            {
                e.Handled = true;
                SendMessage(sender, new RoutedEventArgs());
            }
        }

        private void SettingsToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SettingsWindow(currentUserName, currentHost, currentPort)
            {
                Owner = this
            };
            var result = dialog.ShowDialog();
            if (result == true)
            {
                currentUserName = dialog.UserName;
                currentHost = dialog.Host ?? currentHost;
                currentPort = dialog.Port ?? currentPort;
                currentAvatarPath = dialog.AvatarPath;

                TryConnect();
            }
        }

        private void TryConnect()
        {
            try
            {
                client?.Close();
                client = new TcpClient(currentHost, currentPort);
                stream = client.GetStream();
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                reader = new StreamReader(stream, Encoding.UTF8);

                listenThread = new Thread(ListenForMessages);
                listenThread.IsBackground = true;
                listenThread.Start();

                if (!string.IsNullOrWhiteSpace(currentUserName))
                {
                    writer.WriteLine("USER:" + currentUserName);
                    if (!string.IsNullOrWhiteSpace(currentAvatarPath) && File.Exists(currentAvatarPath))
                    {
                        try
                        {
                            var bytes = File.ReadAllBytes(currentAvatarPath);
                            var b64 = Convert.ToBase64String(bytes);
                            writer.WriteLine("AVATAR:" + b64);
                        }
                        catch { }
                    }
                }
                AppendMessage("✅ Sunucuya bağlanıldı: " + currentHost + ":" + currentPort + (string.IsNullOrWhiteSpace(currentUserName) ? "" : (" (" + currentUserName + ")")));
                sendButton.IsEnabled = true;
                messageBox.IsEnabled = true;
                isConnected = true;
                UpdateStatusText();
                // Kullanıcı listesinde kendini hemen göster (USERS yanıtını beklemeden)
                if (!string.IsNullOrWhiteSpace(currentUserName))
                {
                    usersList.Items.Clear();
                    usersList.Items.Add(new { Name = currentUserName, Avatar = currentAvatarPath });
                }
            }
            catch
            {
                AppendMessage("❌ Sunucuya bağlanılamadı: " + currentHost + ":" + currentPort);
                isConnected = false;
                UpdateStatusText();
                TriggerReconnect();
            }
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            try { writer?.Dispose(); } catch { }
            try { reader?.Dispose(); } catch { }
            try { stream?.Close(); } catch { }
            try { client?.Close(); } catch { }
            if (listenThread != null && listenThread.IsAlive)
            {
                try { listenThread.Join(200); } catch { }
            }
        }
    }
}
