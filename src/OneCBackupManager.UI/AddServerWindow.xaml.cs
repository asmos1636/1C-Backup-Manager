using System.Windows;
using OneCBackupManager.UI.Models;
using OneCBackupManager.UI.Services;

namespace OneCBackupManager.UI;

public partial class AddServerWindow : Window
{
    private readonly SshConnectionService _ssh = new();
    private readonly ServerProfileStore _store = new();
    private string? _lastPassword;

    public SshServerProfile? SavedProfile { get; private set; }

    public AddServerWindow()
    {
        InitializeComponent();
    }

    private async void TestButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(PortBox.Text, out var port) || port is < 1 or > 65535)
        {
            MessageBox.Show("Укажи корректный порт SSH (1–65535).", "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(HostBox.Text) || string.IsNullOrWhiteSpace(UsernameBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
        {
            MessageBox.Show("Заполни адрес, пользователя и пароль SSH.", "Не хватает данных", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        TestButton.IsEnabled = false;
        SaveButton.IsEnabled = false;
        StatusText.Text = "Подключение...";
        ResultBox.Text = "";
        try
        {
            var info = await _ssh.TestAndGetServerInfoAsync(HostBox.Text.Trim(), port, UsernameBox.Text.Trim(), PasswordBox.Password);
            ResultBox.Text = info;
            _lastPassword = PasswordBox.Password;
            StatusText.Text = "SSH-подключение успешно. Сведения получены.";
            SaveButton.IsEnabled = true;
        }
        catch (Exception ex)
        {
            StatusText.Text = "Не удалось подключиться.";
            ResultBox.Text = ex.Message;
        }
        finally
        {
            TestButton.IsEnabled = true;
        }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (_lastPassword is null || !int.TryParse(PortBox.Text, out var port)) return;
        try
        {
            SavedProfile = new SshServerProfile
            {
                Name = HostBox.Text.Trim(),
                Host = HostBox.Text.Trim(),
                Port = port,
                Username = UsernameBox.Text.Trim(),
                ProtectedPassword = ServerProfileStore.ProtectPassword(_lastPassword)
            };
            var profiles = _store.Load();
            profiles.RemoveAll(x => string.Equals(x.Host, SavedProfile.Host, StringComparison.OrdinalIgnoreCase) && x.Port == port && x.Username == SavedProfile.Username);
            profiles.Add(SavedProfile);
            _store.Save(profiles);
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Не удалось сохранить настройки сервера: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
