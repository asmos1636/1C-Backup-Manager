using Renci.SshNet;

namespace OneCBackupManager.UI.Services;

public sealed class SshConnectionService
{
    public Task<string> TestAndGetServerInfoAsync(string host, int port, string username, string password, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            using var client = new SshClient(host, port, username, password);
            client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(12);
            client.Connect();
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                const string command = "printf 'HOSTNAME='; hostname; printf '\\nOS='; . /etc/os-release 2>/dev/null; printf '%s %s\\n' \"$NAME\" \"$VERSION\"; printf '\\n1C_SERVICES\\n'; systemctl list-units --type=service --all --no-legend 2>/dev/null | grep -Ei 'srv1cv8|1c' || true; printf '\\nRAC_PATH='; command -v rac || true; printf '\\n1C_PROCESSES\\n'; pgrep -a -f 'ragent|rmngr| rphost' 2>/dev/null | head -20 || true";
                var result = client.RunCommand(command);
                if (result.ExitStatus != 0 && string.IsNullOrWhiteSpace(result.Result))
                    throw new InvalidOperationException(string.IsNullOrWhiteSpace(result.Error) ? "Не удалось получить сведения о сервере." : result.Error);
                return result.Result;
            }
            finally
            {
                client.Disconnect();
            }
        }, cancellationToken);
    }
}
