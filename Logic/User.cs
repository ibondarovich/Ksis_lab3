using System;
using System.Net;
using System.Net.Sockets;
namespace Chat_lab3
{
    class User: IUser
    {
        UdpClient udpClient;   

        public User(IPEndPoint localEP)
        {
            udpClient = new UdpClient(localEP);
        }
        public void SendMessage()
        {
            
        }
        
        public void ReceiveMessage()
        {

        }


    
    }
}