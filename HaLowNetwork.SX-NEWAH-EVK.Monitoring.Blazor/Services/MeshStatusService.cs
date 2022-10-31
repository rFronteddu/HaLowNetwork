using HaLow.Raspbian;
using HaLowNetwork.SX_NEWAH_EVK.Parse;
using HaLowNetwork.SX_NEWAH_EVK.Parse.Models;

namespace HaLowNetwork.SX_NEWAH_EVK.Monitoring.Blazor.Services;

public class MeshStatusService
{
    private readonly SshService _ssh;


    public MeshStatusService(SshService ssh)
    {
        _ssh = ssh;
    }
    
    public IEnumerable<MeshPointModel> MeshPointStatus()
    {
        List<MeshPointModel> stats = new();

        try
        {
            var result = _ssh.UseCommand(Commands.MPATH_WLAN0);

            foreach (var row in result.Result.Split("\n").Skip(1))     
            {
                if (!string.IsNullOrEmpty(row))
                {
                    stats.Add(row.ToMeshPointStatus());
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }


        return stats;
    }
}