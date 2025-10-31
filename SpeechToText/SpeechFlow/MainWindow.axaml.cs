using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace SpeechFlow;

public partial class MainWindow : Window, IAsyncDisposable
{
    private bool _isListening;
    private readonly Button _microphoneButton;
    private readonly Border _microphoneButtonBorder;
    private readonly TextBlock _recognizedText;
    private readonly SpeechRecognizer? _speechRecognizer;
    private readonly StringBuilder _recognizedStringBuilder = new();

    public MainWindow()
    {
        InitializeComponent();
        _microphoneButton = this.FindControl<Button>("MicrophoneButton") ?? throw new InvalidOperationException("Cannot find MicrophoneButton");
        _microphoneButtonBorder = this.FindControl<Border>("MicrophoneButtonBorder") ?? throw new InvalidOperationException("Cannot find MicrophoneButtonBorder");
        _recognizedText = this.FindControl<TextBlock>("RecognizedText") ?? throw new InvalidOperationException("Cannot find RecognizedText");
        _microphoneButton.Click += MicrophoneButton_Click;

        var speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
        var speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");

        if (string.IsNullOrEmpty(speechKey) || string.IsNullOrEmpty(speechRegion))
        {
            _recognizedText.Text = "Error: SPEECH_KEY and SPEECH_REGION environment variables are not set.";
            _microphoneButton.IsEnabled = false;
            return;
        }

        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
        _speechRecognizer = new SpeechRecognizer(speechConfig);

        _speechRecognizer.Recognizing += (s, e) =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                var text = $"{_recognizedStringBuilder} {e.Result.Text}";
                _recognizedText.Text = text;
            });
        };

        _speechRecognizer.Recognized += (s, e) =>
        {
            if (e.Result.Reason == ResultReason.RecognizedSpeech)
            {
                _recognizedStringBuilder.Append($"{e.Result.Text} ");
                Dispatcher.UIThread.Post(() =>
                {
                    _recognizedText.Text = _recognizedStringBuilder.ToString();
                });
            }
            else if (e.Result.Reason == ResultReason.NoMatch)
            {
                // Handle no match
            }
        };
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void MicrophoneButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_speechRecognizer == null) return;

        _isListening = !_isListening;
        if (_isListening)
        {
            _microphoneButtonBorder.Classes.Add("listening");
            await _speechRecognizer.StartContinuousRecognitionAsync();
        }
        else
        {
            _microphoneButtonBorder.Classes.Remove("listening");
            await _speechRecognizer.StopContinuousRecognitionAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_speechRecognizer != null)
        {
            await _speechRecognizer.StopContinuousRecognitionAsync();
            _speechRecognizer.Dispose();
        }
    }
}
