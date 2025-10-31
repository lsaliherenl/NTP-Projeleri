using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System;
using System.Threading.Tasks;

namespace SpeechFlow;

public partial class WelcomeWindow : Window
{
    public WelcomeWindow()
    {
        InitializeComponent();
        Loaded += WelcomeWindow_Loaded;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void WelcomeWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var animation = new Animation
        {
            Duration = TimeSpan.FromSeconds(2),
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0),
                    Setters =
                    {
                        new Setter(OpacityProperty, 0.0)
                    }
                },
                new KeyFrame
                {
                    Cue = new Cue(1),
                    Setters =
                    {
                        new Setter(OpacityProperty, 1.0)
                    }
                }
            }
        };

        await animation.RunAsync(this);
        await Task.Delay(2000);

        var mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }
}