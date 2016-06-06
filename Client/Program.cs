using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Jarloo.Listener
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Socket clinetSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
            clinetSocket.ExclusiveAddressUse = false;
            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 11000);
            EndPoint ep = (EndPoint)localEp;

            clinetSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            clinetSocket.ExclusiveAddressUse = false;

            clinetSocket.Bind(localEp);

            clinetSocket.SetSocketOption(SocketOptionLevel.IP,
            SocketOptionName.AddMembership,
            new MulticastOption(IPAddress.Parse("239.0.0.222")));
           

            Console.WriteLine("Listening this will never quit so you will need to ctrl-c it");

            byte[] data = new byte[1024];

            while (true)
            {
                // this blocks until some bytes are received
                int recv = clinetSocket.ReceiveFrom(data, ref ep);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine("received: {0} from: {1}", stringData, ep.ToString());
            }
        }
    }
}