using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat_lab3
{   public class NewUser : IUser
    {
        string userName;
        int port = 8888;
        IPEndPoint IPEndPoint;
        UdpClient udpClient;
        Thread reciveThread;
        public NewUser(IPAddress iPAddress, int port, string name)
        {
            userName = name;
            IPEndPoint = new IPEndPoint(iPAddress, port);
        }
        public void SendMessage()
        {
            udpClient = new UdpClient(IPEndPoint);
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(userName);
                udpClient.Send(data, data.Length, IPAddress.Broadcast.ToString(), port);
            }
            catch
            {

            }
            finally
            {
                udpClient.Close();
            }
           
        }

        public void ReceiveMessage()
        {
            udpClient = new UdpClient(IPEndPoint);
            IPEndPoint remoteIp = new IPEndPoint(IPAddress.Any, port);
            try
            {
                byte[] data = udpClient.Receive(ref remoteIp);
                string message = Encoding.Unicode.GetString(data);
            }
            catch
            {

            }
            finally
            {
                udpClient.Close();
            }
        }
    }
}