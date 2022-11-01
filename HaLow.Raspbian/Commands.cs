namespace HaLow.Raspbian;

public static class Commands
{
    public const string IFCONFIG_WLAN0 = "sudo /sbin/ifconfig wlan0";
    public const string MPATH_WLAN0 = "sudo iw dev wlan0 mpath dump";
    public const string REBOOT = "sudo reboot";
    public const string WPA_STATUS = "sudo wpa_cli status";
    
    public const string NRC_PATH = "cd nrc_pkg/script/";
    
}