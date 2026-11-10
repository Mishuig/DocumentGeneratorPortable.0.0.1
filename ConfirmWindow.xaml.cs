using System.Windows;
using System.Windows.Media;

namespace DocumentGenerator;

/// <summary>
/// Єдине вікно підтвердження в стилі застосунку.
/// destructive = true → червона кнопка підтвердження, фокус за замовчуванням на «Скасувати»
/// (випадковий Enter не видаляє дані).
/// </summary>
public partial class ConfirmWindow : Window
{
    public ConfirmWindow(string title, string message, string confirmText, bool destructive)
    {
        InitializeComponent();
        Title = title;
        TitleText.Text = title;
        MessageText.Text = message;
        ConfirmButton.Content = confirmText;

        if (destructive)
        {
            ConfirmButton.Style = (Style)FindResource("DialogDangerButton");
            CancelButton.IsDefault = true;
            CancelButton.Loaded += (_, _) => CancelButton.Focus();
        }
        else
        {
            IconBox.Background = (Brush)FindResource("AccentTint");
            IconText.Text = "?";
            IconText.Foreground = (Brush)FindResource("Accent");
            ConfirmButton.IsDefault = true;
        }
    }

    public static bool Ask(Window? owner, string title, string message, string confirmText, bool destructive = false)
    {
        var dlg = new ConfirmWindow(title, message, confirmText, destructive);
        if (owner is { IsLoaded: true }) dlg.Owner = owner;
        return dlg.ShowDialog() == true;
    }

    private void Confirm_Click(object sender, RoutedEventArgs e) => DialogResult = true;
}
