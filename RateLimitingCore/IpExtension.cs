using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RateLimitingCore
{
    public static class IpExtension
    {
        public static string GetIp()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
               .Select(p => p.GetIPProperties())
               .SelectMany(p => p.UnicastAddresses)
               .Where(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !System.Net.IPAddress.IsLoopback(p.Address))
               .FirstOrDefault()?.Address.ToString();


        }
    }
}
