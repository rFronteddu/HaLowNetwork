using Renci.SshNet;

namespace HaLowNetwork.SX_NEWAH_EVK.Monitoring.Blazor.Services;

public class SshService
{
    private IConfiguration _config;


    public SshService(IConfiguration config)
    {
        _config = config;
    }

    private const string CONF_PATH = "SshConnection:";

    public SshCommand? UseCommand(string command)
    {
        SshCommand? result = null;
        using (SshClient client = new(
                   _config.GetValue<string>($"{CONF_PATH}Host"),
                   _config.GetValue<string>($"{CONF_PATH}User"),
                   _config.GetValue<string>($"{CONF_PATH}Password")))
        {

            try
            {
                client.Connect();
                result = client.RunCommand(command);
                client.Disconnect();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

        }

        return result;
    }
}