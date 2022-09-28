namespace HaLowNetwork.WSG300NRC.Parse.Models;

public class LocalTx
{
    public int Cnt { get; set; }
    public string Dly { get; set; }
    public string CtsBe { get; set; }
    public string Agg { get; set; }
    public Data Data { get; set; }
    public int Dur { get; set; } // ms
    public int Cca { get; set; }
    public int Pre { get; set; } // %
    public int Fail { get; set; }
    public int Drop { get; set; }
}