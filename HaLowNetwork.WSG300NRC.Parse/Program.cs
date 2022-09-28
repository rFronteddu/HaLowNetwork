using System.Text.RegularExpressions;

namespace HaLowNetwork.WSG300NRC.Parse;

public static class Program
{
    public static void Main(string[] arg)
    {
        var data = File.ReadAllText("data.txt");
        var test = SerialParser.FromString(data);

    }
}