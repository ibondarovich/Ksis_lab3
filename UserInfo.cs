using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Chat_lab3
{
    public class UserInfo
    {
        public IPAddress IpAddress { get; set; }
        public string userName { get; set; }
        public UserInfo(IPAddress iPAddress, string name)
        {
            IpAddress = iPAddress;
            userName = name;
        }
    }
}
