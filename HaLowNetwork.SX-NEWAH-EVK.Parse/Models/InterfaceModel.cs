namespace HaLowNetwork.SX_NEWAH_EVK.Parse.Models;

public class InterfaceModel
{
    public string Name { get; set; }
    public string Flags { get; set; }
    public string Mtu { get; set; }
    public string Ip { get; set; }
    public string Mask { get; set; }
    public string Broadcast { get; set; }
    public string Mac { get; set; }

    public InterfaceRxModel Rx { get; set; }
    public InterfaceTxModel Tx { get; set; }
}