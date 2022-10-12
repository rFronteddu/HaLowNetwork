using System.Text.RegularExpressions;
using HaLowNetwork.SX_NEWAH_EVK.Parse.Models;

namespace HaLowNetwork.SX_NEWAH_EVK.Parse;

public static class InputParser
{
    public static DhcpModel? ToDhcpLease(this string row)
    {
        var data = row.Split(" ");

        if (data.Length >= 5)
        {
            return new DhcpModel()
            {
                Ip = data[2],
                Mac = data[1],
                LeaseEnd = data[0] == "0" ? null : DateTime.Parse(data[0]),
                Static = data[0] == "0",
                Name = data[3]
            };
        }

        return null;
    }

    public static MeshPointModel? ToMeshPointStatus(this string row)
    {
        var data = row.Split(" ");

        if (data.Length < 3)
            return null;
        var status = data[2].Split("\t");

        if (status.Length < 9)
            return null;

        return new MeshPointModel()
        {
            DstMac = data[0],
            NextHopMac = data[1],
            Interface = status[0],
            SN = int.Parse(status[1]),
            Metric = int.Parse(status[2]),
            QLen = int.Parse(status[3]),
            ExpTime = $"{status[4]} {status[5]}",
            DTime = int.Parse(status[6]),
            DRet = status[7],
            Flags = status[8],
            HopCount = int.Parse(status[9]),
            PathChange = status.Length > 10 ? status[10] : ""
        };
    }

    private const string INTERFACE_REGEX =
        @"(\S+)[:] flags=(\S+) mtu (\S+) inet (\S+) netmask (\S+) broadcast (\S+) inet6 (\S+) prefixlen (\S+) scopeid (\S+) ether (\S+) txqueuelen (.+) RX packets (\S+) bytes (\S+) [(](\S+) (\S+)[)] RX errors (\S+) dropped (\S+) overruns (\S+) frame (\S+) TX packets (\S+) bytes (\S+) [(](\S+) (\S+)[)] TX errors (\S+) dropped (\S+) overruns (\S+) carrier (\S+) collisions (\S+)";


    public static InterfaceModel? ToInterface(this string data)
    {
        System.Globalization.CultureInfo customCulture =
            (System.Globalization.CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";

        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        
        Regex regex = new Regex(INTERFACE_REGEX);

        data= Regex.Replace(data, @"\s+", " ");
        var match = regex.Match(data);

        if (match.Success)
        {
            if (match.Groups.Count >= 28)
            {
                var stats = match.Groups;

                return new InterfaceModel()
                {
                    Name = stats[1].Value,
                    Flags = stats[2].Value,
                    Mtu = stats[3].Value,
                    Ip = stats[4].Value,
                    Mask = stats[5].Value,
                    Broadcast = stats[6].Value,
                    Mac = stats[10].Value,
                    Rx = new InterfaceRxModel()
                    {
                        Packets = int.Parse(stats[12].Value),
                        PacketsValue = int.Parse(stats[13].Value),
                        Amount = double.Parse(stats[14].Value),
                        Units = stats[15].Value,
                        Errors = int.Parse(stats[16].Value),
                        Dropped = int.Parse(stats[17].Value),
                        Overruns = int.Parse(stats[18].Value),
                        Frame = int.Parse(stats[19].Value)
                    },
                    Tx = new InterfaceTxModel()
                    {
                        Packets = int.Parse(stats[20].Value),
                        PacketsValue = int.Parse(stats[21].Value),
                        Amount = double.Parse(stats[22].Value),
                        Units = stats[23].Value,
                        Errors = int.Parse(stats[24].Value),
                        Dropped = int.Parse(stats[25].Value),
                        Overruns = int.Parse(stats[26].Value),
                        Carrier = int.Parse(stats[27].Value),
                        Collisions = int.Parse(stats[28].Value)
                    },
                };
            }
        }

        return null;
    }
}