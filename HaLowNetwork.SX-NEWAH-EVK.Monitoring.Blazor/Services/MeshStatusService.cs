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

    private const string STATUS_CMD = "sudo iw dev wlan0 mpath dump";
    public IEnumerable<MeshPointModel> MeshPointStatus()
    {
        List<MeshPointModel> stats = new();

        var result = _ssh.UseCommand(STATUS_CMD);

        foreach (var row in result.Result.Split("\n").Skip(1))     
        {
            if (!string.IsNullOrEmpty(row))
            {
                stats.Add(row.ToMeshPointStatus());
            }
        }

        return stats;
    }
}