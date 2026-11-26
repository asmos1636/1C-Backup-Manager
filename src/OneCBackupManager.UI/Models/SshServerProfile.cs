namespace OneCBackupManager.UI.Models;

public sealed class SshServerProfile
{
    public string Name { get; set; } = "";
    public string Host { get; set; } = "";
    public int Port { get; set; } = 22;
    public string Username { get; set; } = "";
    public string ProtectedPassword { get; set; } = "";
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
