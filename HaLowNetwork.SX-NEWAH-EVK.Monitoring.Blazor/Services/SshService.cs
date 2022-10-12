using Renci.SshNet;

namespace HaLowNetwork.SX_NEWAH_EVK.Monitoring.Blazor.Services;

public class SshService
{
    public const string SSH_CONF_PATH = "SshConnection:";

    private readonly string _host;
    private readonly string _user;
    private readonly string _password;

    public SshService(IConfiguration config)
    {
        _host = config.GetValue<string>($"{SSH_CONF_PATH}Host");
        _user = config.GetValue<string>($"{SSH_CONF_PATH}User");
        _password = config.GetValue<string>($"{SSH_CONF_PATH}Password");
    }

    public SshService(string host, string user, string password)
    {
        _host = host;
        _user = user;
        _password = password;
    }
    

    public SshCommand? UseCommand(string command)
    {
        SshCommand? result = null;
        using (SshClient client = new(_host, _user, _password))
        {
            try
            {
                client.Connect();
                result = client.RunCommand(command);
                client.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return result;
    }
}