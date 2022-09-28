using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using HaLowNetwork.WSG300NRC.Monitoring.Blazor.Utils;
using HaLowNetwork.WSG300NRC.Parse.Models;
using Buffer = HaLowNetwork.WSG300NRC.Parse.Models.Buffer;

namespace HaLowNetwork.WSG300NRC.Parse;

public static class SerialParser
{
    private const string BASE_SPLIT = "STA";

    private const string STATUS_REGEX =
        @"local[:](.+) (AID.+)chn[:](.+) bgr[:](.+) buf[:](.+)irq[:](.+)tx [:](.+)rx [:](.+)dbg[:](.+) cca[:](.+)s (.+)chip-temperature[:](.+), vcc[:](.+)";

    private const string CLEAR_NUMBER_REGEX = @"(\d+)";
    private const string KEY_VALUE_REGEX = @"(\S+)[=](\S+| \S+)\s";

    public static ApReport? FromString(string input)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        
        System.Globalization.CultureInfo customCulture =
            (System.Globalization.CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";

        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

        var array = input.Split(BASE_SPLIT);

        if (array.Length < 2)
            throw new FormatException();

        var report = ProcessStatus(array[1]);

        foreach (string sta in array.Skip(2))
        {
            report.Clients.Add(ProcessSta(sta));
        }

        report.TimeStamp = DateTime.Now;

        watch.Stop();
        Log.Info($"Data parse - elapsed: {watch.Elapsed.TotalMilliseconds} ms", typeof(SerialParser));
        return report;
    }

    private static T[] StringToArray<T>(string input, char splitter)
    {
        var output = new List<T>();

        var array = input.Split(splitter);
        var converter = TypeDescriptor.GetConverter(typeof(T));

        foreach (string item in array)
        {
            if (string.IsNullOrWhiteSpace(item))
                continue;

            output.Add((T)converter.ConvertFromString(item));
        }

        return output.ToArray();
    }

    private static Client ProcessSta(string sta)
    {
        var client = new Client();
        client.Id = int.Parse(ClearValue(sta));

        var regex = new Regex(@": (\S+)tx" + client.Id + "[:](.+)rx" + client.Id + "[:](.+)");
        var match = regex.Match(sta);
        client.Rx = SetValues<StaRx>(match.Groups[3].Value);
        client.Tx = SetValues<StaTx>(match.Groups[2].Value);
        client.Address = match.Groups[1].Value;


        return client;
    }

    private static Regex _universal = new Regex(KEY_VALUE_REGEX);

    private static ApReport ProcessStatus(string status)
    {
        var report = new ApReport()
        {
            Clients = new List<Client>(),
        };

        var regexStatus =
            new Regex(STATUS_REGEX);

        var match = regexStatus.Match(status);

        report.Local = SetValues<Local>(match.Groups[2].Value);
        report.Local.Address = match.Groups[1].Value;

        report.Chanals = new Chanals()
        {
            Frequencies = StringToArray<float>(match.Groups[3].Value, ' ')
        };

        report.Bgr = new Bgr()
        {
            Gains = StringToArray<int>(match.Groups[4].Value, ' ')
        };

        report.Buffer = SetValues<Buffer>(match.Groups[5].Value);

        report.Irq = SetValues<Irq>(match.Groups[6].Value);

        report.Tx = SetValues<LocalTx>(match.Groups[7].Value);
        report.Rx = SetValues<LocalRx>(match.Groups[8].Value);

        report.Dbg = SetValues<LocalDbg>(match.Groups[9].Value);

        report.Cca = SetValues<LocalCca>(match.Groups[11].Value);
        report.Cca.Val = int.Parse(match.Groups[10].Value);

        report.Temperature = float.Parse(match.Groups[12].Value);
        report.Vcc = float.Parse(match.Groups[13].Value);


        return report;
    }

    private static T? SetValues<T>(string data)
    {
        var model = (T)Activator.CreateInstance(typeof(T));

        var matches = _universal.Matches(data);

        var props = typeof(T).GetProperties();

        foreach (Match match in matches)
        {
            var name = match.Groups[1].Value.ToLower().Replace(":", "");
            var val = match.Groups[2].Value;

            var prop = props
                .FirstOrDefault(x => (x.Name.ToLower().Replace("_", "")) == name);

            if (prop != null)
            {
                var converter = TypeDescriptor.GetConverter(prop.PropertyType);

                if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(float))
                {
                    val = ClearValue(val);
                }

                var convertedValue = converter.ConvertFromString(val);
                prop.SetValue(model, convertedValue, null);
            }
        }


        return model;
    }

    private static Regex _clearNumber = new Regex(CLEAR_NUMBER_REGEX);

    private static string ClearValue(string value)
    {
        return _clearNumber.Match(value).Groups[0].Value;
    }
}