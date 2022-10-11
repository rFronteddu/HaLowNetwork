namespace HaLowNetwork.SX_NEWAH_EVK.Parse.Models;

public class MeshPointModel
{
    public string DstMac { get; set; }
    public string NextHopMac { get; set; }
    public string Interface { get; set; }
    public int SN { get; set; }
    public int Metric { get; set; }
    public int QLen { get; set; }
    public string ExpTime { get; set; }
    public int DTime { get; set; }
    public int DRet { get; set; }
    public string Flags { get; set; }
    public int HopCount { get; set; }
    public string PathChange { get; set; }
}