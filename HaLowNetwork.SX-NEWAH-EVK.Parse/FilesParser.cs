using HaLowNetwork.SX_NEWAH_EVK.Parse.Models;

namespace HaLowNetwork.SX_NEWAH_EVK.Parse;

public static class FilesParser
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
}