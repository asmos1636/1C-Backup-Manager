using System.Windows;
using OneCBackupManager.UI.Services;

namespace OneCBackupManager.UI;

public partial class MainWindow : Window
{
    private readonly ServerProfileStore _store = new();

    public MainWindow()
    {
        InitializeComponent();
        RefreshServerList();
    }

    private void AddServerButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new AddServerWindow { Owner = this };
        if (dialog.ShowDialog() == true)
            RefreshServerList();
    }

    private void RefreshServerList()
    {
        var profiles = _store.Load();
        ServerListText.Text = profiles.Count == 0
            ? "Серверы пока не добавлены."
            : string.Join(Environment.NewLine, profiles.Select((p, i) => $"{i + 1}. {p.Name} ({p.Host}:{p.Port}) — пользователь {p.Username}"));
    }
}
