namespace HaLowNetwork.WSG300NRC.Parse.Models;

public class Irq
{
    public int Ac { get; set; }
    public int Bkn { get; set; }
    /// <summary>
    /// Bo(rts:frm)
    /// </summary>
    public string Bo { get; set; }
    /// <summary>
    /// To(rts:frm)
    /// </summary>
    public string To { get; set; }

    public float Rx { get; set; }
}