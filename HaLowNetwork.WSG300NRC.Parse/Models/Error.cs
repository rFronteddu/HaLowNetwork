using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HaLowNetwork.WSG300NRC.Parse.Models;

[TypeConverter(typeof(ErrorTypeConverter))]
public class Error
{
    public int Phy { get; set; }
    public int Fcs { get; set; }
}
public class ErrorTypeConverter : TypeConverter
{
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
            var data = value.ToString().Split(':');

            if (data.Length==2)
            {
                return new Error()
                {
                    Phy = int.Parse(data[0]),
                    Fcs = int.Parse(data[1])
                };
            }

        }
        
        return base.ConvertFrom(context, culture, value);
    }
}