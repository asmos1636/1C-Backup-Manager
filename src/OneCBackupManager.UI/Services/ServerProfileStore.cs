using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using OneCBackupManager.UI.Models;

namespace OneCBackupManager.UI.Services;

public sealed class ServerProfileStore
{
    private readonly string _filePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "1C Backup Manager", "servers.json");

    public List<SshServerProfile> Load()
    {
        if (!File.Exists(_filePath)) return [];
        try
        {
            return JsonSerializer.Deserialize<List<SshServerProfile>>(File.ReadAllText(_filePath)) ?? [];
        }
        catch
        {
            return [];
        }
    }

    public void Save(IEnumerable<SshServerProfile> profiles)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        var json = JsonSerializer.Serialize(profiles, new JsonSerializerOptions { WriteIndented = true });
        var temp = _filePath + ".tmp";
        File.WriteAllText(temp, json, Encoding.UTF8);
        File.Move(temp, _filePath, true);
    }

    public static string ProtectPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser));
    }

    public static string UnprotectPassword(string protectedPassword)
    {
        var bytes = ProtectedData.Unprotect(Convert.FromBase64String(protectedPassword), null, DataProtectionScope.CurrentUser);
        return Encoding.UTF8.GetString(bytes);
    }
}
