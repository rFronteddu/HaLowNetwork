namespace HaLowNetwork.SX_NEWAH_EVK.Parse.Models;

public class InterfaceRxModel
{
    /// <summary>
    /// Bytes
    /// </summary>
    public int Packets { get; set; }

    public int PacketsValue { get; set; }
    public double Amount { get; set; }
    public string Units { get; set; }

    public int Errors { get; set; }
    public int Dropped { get; set; }
    public int Overruns { get; set; }
    public int Frame { get; set; }
    
    //errors 0 dropped 0 overruns 0 frame 0
}