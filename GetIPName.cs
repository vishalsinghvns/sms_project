using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace demo.smart_school
{
    public class GetIPName
    {
        public static string GetHostName()
        {
            var hostName = Dns.GetHostName();
            return hostName;
        }
        public static string getIp()
        {
            var hostName = Dns.GetHostName();
            var myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }
    }
}