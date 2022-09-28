namespace HaLowNetwork.WSG300NRC.Parse.Models;

/// <summary>
/// Local
/// </summary>
public class ApReport
{
    public DateTime TimeStamp { get; set; }
    public Local Local { get; set; }
    public Chanals Chanals { get; set; }
    public Bgr Bgr { get; set; }
    public Buffer Buffer { get; set; }
    public Irq Irq { get; set; }
    public LocalTx Tx { get; set; }
    public LocalRx Rx { get; set; }
    public LocalDbg Dbg { get; set; }
    public LocalCca Cca { get; set; }

    public float Temperature { get; set; }
    public float Vcc { get; set; }

    public List<Client> Clients { get; set; }
}