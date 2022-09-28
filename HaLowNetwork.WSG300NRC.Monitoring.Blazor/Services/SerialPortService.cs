using System.IO.Ports;
using HaLowNetwork.WSG300NRC.Monitoring.Blazor.Utils;
using HaLowNetwork.WSG300NRC.Parse;
using HaLowNetwork.WSG300NRC.Parse.Models;


namespace HaLowNetwork.WSG300NRC.Monitoring.Blazor.Services;

public class SerialPortService
{
    private readonly IConfiguration _config;
    private SerialPort _serial;

    private const string CONFIG_PREFIX = "SerialPort:";
    private const string PARSE_SPLITER = "LMAC STATUS:";

    private string _data = string.Empty;

    public delegate void SerialMessage(ApReport? report);

    public event SerialMessage MessageReceived;

    public SerialPortService(IConfiguration config)
    {
        _config = config;

        SerialStart();
    }

    private void SerialStart()
    {
        if (!_config.GetValue<bool>($"{CONFIG_PREFIX}Enabled"))
            return;

        _serial = new SerialPort(_config.GetValue<string>($"{CONFIG_PREFIX}Port"));

        _serial.BaudRate = _config.GetValue<int>($"{CONFIG_PREFIX}BaudRate");
        _serial.Parity = Parity.None;
        _serial.StopBits = StopBits.One;
        _serial.DataBits = 8;
        _serial.Handshake = Handshake.None;

        if (!_serial.IsOpen)
        {
            _serial.DataReceived += new SerialDataReceivedEventHandler(((sender, eventArgs) =>
            {
                var serial = (SerialPort)sender;
                var data = serial.ReadLine();


                if (data.Contains(PARSE_SPLITER))
                {
                    try
                    {
                        MessageReceived.Invoke(SerialParser.FromString(_data.Replace("\r",string.Empty)));
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Error parse serial data: {e.Message}", this);
                    }
                   
                    _data = string.Empty;
                }
                _data += data;

            }));

            try
            {
                _serial.Open();

                if (_serial.IsOpen)
                    Log.Success($"Serial connected to {_serial.PortName}", this);
                else
                    Log.Warning($"Serial was not open {_serial.PortName}", this);
            }
            catch (Exception e)
            {
                Log.Error(e.Message, this);
            }
        }
    }
}