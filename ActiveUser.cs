using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Chat_lab3
{
    public class ActiveUser
    {
        public IPAddress ipAddress;
        public string userName;
        private UserInfo userInfo;
        private NetworkStream dataStream;
        private UserList userList;

        private int portTCP = 5556;
        public string UserAction { get; set; }

        TcpClient tcpClient;

        private Task receiveTCPTask;

        public ActiveUser(IPAddress ipAddress, string userName)
        {
            this.ipAddress = ipAddress;
            this.userName = userName;
        }

        public ActiveUser(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            this.ipAddress = ((IPEndPoint)this.tcpClient.Client.RemoteEndPoint).Address;
            dataStream = this.tcpClient.GetStream();
        }

        public void SendMessageTCP(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            dataStream.Write(data, 0, data.Length);
        }

        public void SetConnectionWithNewUser(IPEndPoint remoteIP)
        {
            //установка tcp-соединения с новым узлом
            tcpClient = new TcpClient();
            tcpClient.Connect(new IPEndPoint(ipAddress, portTCP));
            dataStream = tcpClient.GetStream();
        }
        public string ReceiveMessageTCP()   
        {
            StringBuilder message = new StringBuilder();
            byte[] data = new byte[1024];
            do
            {
                try
                {
                    int size = dataStream.Read(data, 0, data.Length);
                    message.Append(Encoding.UTF8.GetString(data, 0, size));
                }
                catch
                {
                    
                }
            }
            while (dataStream.DataAvailable);
            return message.ToString();
        }

        public void StopTCP()
        {
            tcpClient.Close();
        }
    }
}
