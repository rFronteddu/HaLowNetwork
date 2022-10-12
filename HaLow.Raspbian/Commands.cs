namespace HaLow.Raspbian;

public static class Commands
{
    public const string IFCONFIG_WLAN0 = "sudo /sbin/ifconfig wlan0";
    public const string MPATH_WLAN0 = "sudo iw dev wlan0 mpath dump";
}