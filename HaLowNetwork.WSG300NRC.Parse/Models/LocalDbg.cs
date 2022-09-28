namespace HaLowNetwork.WSG300NRC.Parse.Models;

public class LocalDbg
{
    public string Dtmd { get; set; }
    public string Stamap { get; set; }
    public string Flag { get; set; }
    public int RxDp { get; set; }
    public int Kerr { get; set; }
    public int Mic { get; set; }
    public int Lerr { get; set; }
    public int Kick { get; set; }
    public int Csc { get; set; }
    public int Rst { get; set; }
    public int Ofv { get; set; }
    public int Nob { get; set; }
    public int Tsnr { get; set; }
    public int Rssi { get; set; }
    public int RxDut { get; set; } // %
    public int Txp { get; set; }
    public int Rxg { get; set; }
}