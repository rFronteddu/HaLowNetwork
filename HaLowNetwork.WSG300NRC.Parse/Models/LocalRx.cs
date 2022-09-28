namespace HaLowNetwork.WSG300NRC.Parse.Models;

public class LocalRx
{
    public int Cnt { get; set; }
    public int Bus { get; set; } // ms
    public string CtsBm { get; set; }
    public int Pks { get; set; }
    public Data Data { get; set; }
    public int Dur { get; set; } //ms
    public Error Error { get; set; }
    public string Ecode { get; set; }
}