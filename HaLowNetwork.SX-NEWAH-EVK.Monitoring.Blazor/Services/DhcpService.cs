using HaLowNetwork.SX_NEWAH_EVK.Parse;
using HaLowNetwork.SX_NEWAH_EVK.Parse.Models;

namespace HaLowNetwork.SX_NEWAH_EVK.Monitoring.Blazor.Services;

public class DhcpService
{
    private const string FILE_LEASES = "/var/lib/misc/dnsmasq.leases";
    public DateTime LastUpdate { get; private set; }
    private readonly List<DhcpModel> _leases;

    public DhcpService()
    {
        LastUpdate = new();
        _leases = new();
    }


    public IEnumerable<DhcpModel> Leases(bool forceUpdate = false)
    {
        if ((DateTime.Now - LastUpdate).TotalMinutes > 2 || forceUpdate)
        {
            ReadFile();
            LastUpdate = DateTime.Now;
        }

        return _leases;
    }

    private void ReadFile()
    {
        _leases.Clear();

        try
        {
            foreach (var line in File.ReadLines(FILE_LEASES))
            {
                _leases.Add(line.ToDhcpLease());
            }
        }
        catch (Exception e)
        {
          //TODO: message
        }

    }
}