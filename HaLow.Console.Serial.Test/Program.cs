// See https://aka.ms/new-console-template for more information

using System.IO.Ports;

var _serial = new SerialPort("/dev/cu.usbserial-1330");

_serial.BaudRate = 115200;
_serial.Parity = Parity.None;
_serial.StopBits = StopBits.One;
_serial.DataBits = 8;
_serial.Handshake = Handshake.None;



_serial.DataReceived+=  new SerialDataReceivedEventHandler(((sender, eventArgs) =>
{
    var serial = (SerialPort)sender;

    Console.WriteLine(serial.ReadLine());
}));

_serial.Open();

Console.ReadKey();