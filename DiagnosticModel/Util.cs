using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace DiagnosticModel
{
    public static class Util
    {
        public static string GetServerIps()
        {
            NetworkInterface[] networks = NetworkInterface.GetAllNetworkInterfaces();
            string serverIpAddresses = string.Empty;

            foreach (var network in networks)
            {
                var ipAddress = network.GetIPProperties().UnicastAddresses.Where(p => p.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(p.Address)).FirstOrDefault()?.Address.ToString();

                serverIpAddresses += network.Name + ":" + ipAddress + "|";
            }
            return serverIpAddresses;
        }
    }

    public static class Url {
        

        public static string Combine(string baseUri, string relativeOrAbsoluteUri)
        {
            var uri= new Uri(new Uri(baseUri), relativeOrAbsoluteUri);
            return uri.AbsoluteUri;
        }
    }
}
