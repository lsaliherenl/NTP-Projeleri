using System.Collections.Generic;

namespace ChatBot.Models
{
    /// <summary>
    /// Gemini API'den dönen yanıt modeli
    /// </summary>
    public class GeminiResponse
    {
        public List<Candidate> candidates { get; set; }
        public PromptFeedback promptFeedback { get; set; }
    }

    /// <summary>
    /// API yanıt adayı
    /// </summary>
    public class Candidate
    {
        public Content content { get; set; }
        public string finishReason { get; set; }
        public int index { get; set; }
    }

    /// <summary>
    /// Prompt geri bildirimi
    /// </summary>
    public class PromptFeedback
    {
        public List<SafetyRating> safetyRatings { get; set; }
    }

    /// <summary>
    /// Güvenlik değerlendirmesi
    /// </summary>
    public class SafetyRating
    {
        public string category { get; set; }
        public string probability { get; set; }
    }
}


