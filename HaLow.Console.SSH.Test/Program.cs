using Renci.SshNet;

string user = "pi";
string pass = "raspberry";
string host = "192.168.2.47";

//Set up the SSH connection
        
using (var client = new SshClient(host, user, pass))
{
    //Start the connection
    
    client.Connect();
    Console.WriteLine(client.IsConnected);
   /* Console.WriteLine("cd");
    client.RunCommand("cd /home/pi/nrc_pkg/script/");
    Console.WriteLine(
        client.RunCommand("ls").Result);*/
    var output = client.RunCommand("sudo wpa_cli status"); //start.py 4 3 US 0
    Console.WriteLine(output.Result);
    Console.WriteLine(output.Error);
    client.Disconnect();
    
}