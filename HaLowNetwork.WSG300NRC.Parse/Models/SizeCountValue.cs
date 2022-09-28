using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HaLowNetwork.WSG300NRC.Parse.Models;

[TypeConverter(typeof(SizeCountValueTypeConverter))]
public class SizeCountValue
{
    public int Size { get; set; }
    public int Count { get; set; }      
}

public class SizeCountValueTypeConverter : TypeConverter
{

    private const string DATA_REGEX = @"(\d+)";

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

            return new SizeCountValue()
            {
                Size = Convert.ToInt32(matches[0].Groups[1].Value),
                Count = Convert.ToInt32(matches[1].Groups[1].Value),
            };

        }

        return base.ConvertFrom(context, culture, value);
    }
}