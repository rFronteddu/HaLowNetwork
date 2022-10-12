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
            //HopCount = int.Parse(status[9]),
           // PathChange = status.Length > 10 ? status[10] : ""
        };
    }
}