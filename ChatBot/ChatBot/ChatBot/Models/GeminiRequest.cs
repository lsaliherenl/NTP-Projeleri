using System.Collections.Generic;

namespace ChatBot.Models
{
    /// <summary>
    /// Gemini API'ye gönderilecek istek modeli
    /// </summary>
    public class GeminiRequest
    {
        public List<Content> contents { get; set; }

        public GeminiRequest()
        {
            contents = new List<Content>();
        }
    }

    /// <summary>
    /// Mesaj içeriği modeli
    /// </summary>
    public class Content
    {
        public List<Part> parts { get; set; }
        public string role { get; set; }

        public Content()
        {
            parts = new List<Part>();
        }
    }

    /// <summary>
    /// Mesaj parçası modeli
    /// </summary>
    public class Part
    {
        public string text { get; set; }
    }
}

