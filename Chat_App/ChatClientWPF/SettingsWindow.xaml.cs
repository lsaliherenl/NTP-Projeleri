using System;
using System.Windows;
using Microsoft.Win32;

namespace ChatClientWPF
{
    public partial class SettingsWindow : Window
    {
        public string? UserName { get; private set; }
        public string? Host { get; private set; }
        public int? Port { get; private set; }
        public string? AvatarPath { get; private set; }

        public SettingsWindow(string? currentUserName, string? currentHost, int? currentPort)
        {
            InitializeComponent();
            UserNameTextBox.Text = currentUserName ?? string.Empty;
            HostTextBox.Text = currentHost ?? "127.0.0.1";
            PortTextBox.Text = (currentPort ?? 5000).ToString();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var name = UserNameTextBox.Text.Trim();
            var host = HostTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Lütfen bir kullanıcı adı girin.");
                return;
            }
            if (string.IsNullOrWhiteSpace(host))
            {
                MessageBox.Show("Lütfen sunucu host bilgisini girin.");
                return;
            }
            if (!int.TryParse(PortTextBox.Text.Trim(), out var port) || port <= 0 || port > 65535)
            {
                MessageBox.Show("Geçerli bir port girin (1-65535).");
                return;
            }

            UserName = name;
            Host = host;
            Port = port;
            DialogResult = true;
            Close();
        }

        private void PickImage_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Title = "Profil Fotoğrafı Seç",
                Filter = "Resim Dosyaları|*.png;*.jpg;*.jpeg;*.bmp|Tüm Dosyalar|*.*"
            };
            if (ofd.ShowDialog(this) == true)
            {
                AvatarPath = ofd.FileName;
                ImagePathText.Text = AvatarPath;
            }
        }
    }
}

