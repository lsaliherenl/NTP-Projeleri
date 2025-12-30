using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChatBot.Models;
using ChatBot.Helpers;
using Newtonsoft.Json;

namespace ChatBot.Services
{
    /// <summary>
    /// Google Gemini API ile iletişim kuran servis sınıfı
    /// </summary>
    public class GeminiApiService
    {
        private readonly string _apiKey;
        private const string API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";

        // Konuşma geçmişi limiti - maksimum mesaj sayısı (her mesaj çifti = 1 kullanıcı + 1 bot mesajı)
        // Bu limiti artırabilirsiniz, ancak token limitini aşmamaya dikkat edin
        private const int MAX_HISTORY_MESSAGES = 50;

        private readonly HttpClient _httpClient;
        private readonly List<Content> _conversationHistory;

        /// <summary>
        /// Constructor - HttpClient ve konuşma geçmişini başlatır
        /// </summary>
        public GeminiApiService()
        {
            _httpClient = new HttpClient();
            _conversationHistory = new List<Content>();
            var config = ConfigHelper.Load();
            _apiKey = config.ApiKey;
        }

        /// <summary>
        /// Kullanıcı mesajını Gemini API'ye gönderir ve yanıt alır
        /// </summary>
        /// <param name="userMessage">Kullanıcının gönderdiği mesaj</param>
        /// <returns>Bot yanıtı</returns>
        public async Task<string> SendMessageAsync(string userMessage)
        {
            try
            {
                // Kullanıcı mesajını konuşma geçmişine ekle
                var userContent = new Content
                {
                    role = "user",
                    parts = new List<Part> { new Part { text = userMessage } }
                };
                _conversationHistory.Add(userContent);

                // Konuşma geçmişi limitini kontrol et ve eski mesajları temizle
                // Son MAX_HISTORY_MESSAGES mesajı tut (her mesaj çifti = 2 mesaj)
                if (_conversationHistory.Count > MAX_HISTORY_MESSAGES)
                {
                    // En eski mesajları kaldır (ilk mesajlardan başla)
                    int removeCount = _conversationHistory.Count - MAX_HISTORY_MESSAGES;
                    _conversationHistory.RemoveRange(0, removeCount);
                }

                // API isteği oluştur
                var request = new GeminiRequest
                {
                    contents = new List<Content>(_conversationHistory)
                };

                // JSON serialize işlemi
                string jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // API URL'ine API key ekle
                string urlWithKey = $"{API_URL}?key={_apiKey}";

                // HTTP POST isteği gönder
                HttpResponseMessage response = await _httpClient.PostAsync(urlWithKey, content);

                // Yanıtı kontrol et
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"API Hatası: {response.StatusCode} - {errorContent}");
                }

                // Yanıt içeriğini oku
                string responseContent = await response.Content.ReadAsStringAsync();

                // JSON deserialize işlemi
                GeminiResponse geminiResponse = JsonConvert.DeserializeObject<GeminiResponse>(responseContent);

                // Bot yanıtını çıkar
                if (geminiResponse?.candidates == null || geminiResponse.candidates.Count == 0)
                {
                    throw new Exception("API'den geçerli bir yanıt alınamadı.");
                }

                string botResponse = geminiResponse.candidates[0].content.parts[0].text;

                // Bot yanıtını konuşma geçmişine ekle
                var botContent = new Content
                {
                    role = "model",
                    parts = new List<Part> { new Part { text = botResponse } }
                };
                _conversationHistory.Add(botContent);

                return botResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Bağlantı hatası: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"JSON işleme hatası: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Beklenmeyen hata: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Konuşma geçmişini temizler
        /// </summary>
        public void ClearHistory()
        {
            _conversationHistory.Clear();
        }

        /// <summary>
        /// Kaynakları temizler
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

