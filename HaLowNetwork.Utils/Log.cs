namespace HaLowNetwork.WSG300NRC.Monitoring.Blazor.Utils;

public static class Log
{
    public static void Error(string text, object? sender = null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Write("Error", text, sender);
    }

    public static void Info(string text, object? sender = null)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Write("Info", text, sender);
    }

    public static void Warning(string text, object? sender = null)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Write("Warning", text, sender);
    }

    public static void Success(string text, object? sender = null)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Write("Success", text, sender);
    }

    private static void Write(string action, string text, object? sender)
    {
        string trace = sender != null ? $"({sender})" : string.Empty;
        Console.WriteLine($"{DateTime.Now}[{action}]{trace}: {text}");

        Console.ResetColor();
    }
}