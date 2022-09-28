namespace HaLowNetwork.WSG300NRC.Parse.Models;

public class StaRx
{
    public string Mcs { get; set; }
    public float Bw { get; set; } // MHz
    /// <summary>
    ///  Evm(avg:std)
    /// </summary>
    public string Evm { get; set; }

    public int Rssi { get; set; }
    public int Agc { get; set; }
    public int Cnt { get; set; }
    public int Agg { get; set; }
    public Data Data { get; set; }
    public int Dur { get; set; } // ms   
    public int Dut { get; set; } // %
    public int FcsErr { get; set; }
    public int FreqDev { get; set; } // Hz
    public string AdvBw { get; set; }

}