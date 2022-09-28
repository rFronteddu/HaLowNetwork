using System.Runtime.Intrinsics.X86;

namespace HaLowNetwork.WSG300NRC.Parse.Models;

public class StaTx
{
    public string Mcs { get; set; }
    public float Bw { get; set; } // MHz
    public int Snr { get; set; }
    public int Cnt { get; set; }
    public int Agg { get; set; }
    public Data Data { get; set; }
    public int Dur { get; set; } // ms   
    public int Dut { get; set; } // %
    public int TxQ { get; set; }
    public int Cca { get; set; }
    public SizeCountValue Ack { get; set; }
    public SizeCountValue Drop { get; set; }
    public int Per { get; set; } // %
    public int EstRate { get; set; } // kbps
    
    
}