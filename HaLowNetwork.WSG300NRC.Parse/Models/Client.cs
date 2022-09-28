namespace HaLowNetwork.WSG300NRC.Parse.Models;
/// <summary>
/// STA
/// </summary>
public class Client
{
    public int Id { get; set; }
    public string Address { get; set; }
    public StaTx Tx { get; set; }
    public StaRx Rx { get; set; }
}