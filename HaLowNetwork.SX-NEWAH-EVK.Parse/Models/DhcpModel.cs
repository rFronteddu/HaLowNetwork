namespace HaLowNetwork.SX_NEWAH_EVK.Parse.Models;

public class DhcpModel
{
    public string Ip { get; set; }
    public string Mac { get; set; }
    public string Name { get; set; }
    public DateTime? LeaseEnd { get; set; }
    public bool Static { get; set; }
}