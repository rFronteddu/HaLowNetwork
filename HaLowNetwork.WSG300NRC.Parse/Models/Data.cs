using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HaLowNetwork.WSG300NRC.Parse.Models;

[TypeConverter(typeof(DataTypeConverter))]
public class Data
{
    public int Size { get; set; }
    public string SizeUnits { get; set; }
    public int Speed { get; set; }
    public string SpeedUnits { get; set; }
}

public class DataTypeConverter : TypeConverter
{

    private const string DATA_REGEX = @"(\d+)(\w+)";
    
    public override bool CanConvertFrom(ITypeDescriptorContext? context,
        Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string)
        {
            var regex = new Regex(DATA_REGEX);

            var matches = regex.Matches(value.ToString());

            return new Data()
            {
                Size = Convert.ToInt32(matches[0].Groups[1].Value),
                Speed = Convert.ToInt32(matches[1].Groups[1].Value),
                SizeUnits = matches[0].Groups[2].Value,
                SpeedUnits = matches[1].Groups[2].Value
            };

        }
        
        return base.ConvertFrom(context, culture, value);
    }
}