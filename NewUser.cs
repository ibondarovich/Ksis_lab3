using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace Chat_lab3
{   public class NewUser 
    {
        private UserInfo userInfo;
        private int portUDP = 5555;
        private IPEndPoint iPEndPoint;
        public UserList userList;
        private Task receiveTask;
        private Task receiveTCPTask;

        private string receiveMessage;

        private TextBox textBox;


        public NewUser(TextBox textBox, UserInfo userInfo)
        {
            userList = new UserList();

            this.userInfo = userInfo;
            this.textBox = textBox;
            iPEndPoint = new IPEndPoint(userInfo.IpAddress, portUDP);

            SendName();
            receiveTask = Task.Factory.StartNew(ReceiveNameOFUser);
            receiveTCPTask = Task.Factory.StartNew(NewUserConnect);
        }

        private void SendName()
        {
            UdpClient udpClient = new UdpClient(iPEndPoint);
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(userInfo.userName);
                udpClient.Send(data, data.Length, IPAddress.Broadcast.ToString(), portUDP);
            }
            catch
            {

            }
            finally
            {
                udpClient.Close();
            }
        }

        private void ReceiveNameOFUser()
        {
            UdpClient udpClient = new UdpClient(iPEndPoint);
            IPEndPoint remoteIp = new IPEndPoint(IPAddress.Any, portUDP);
            while (true)
            {
                try
                {
                    byte[] data = udpClient.Receive(ref remoteIp);
                    receiveMessage = Encoding.UTF8.GetString(data);
                    

                    ActiveUser activeUser = new ActiveUser(remoteIp.Address, receiveMessage);
                    activeUser.SetConnectionWithNewUser(remoteIp);
                    userList.ListOfUsers.Add( activeUser, receiveMessage);
                    activeUser.SendMessageTCP("0"+userInfo.userName);
                    
                    textBox.Invoke((MethodInvoker)delegate
                    {
                        // Running on the UI thread
                        textBox.Text += DateTime.Now + " " +  receiveMessage + " входит в чат\r\n";
                    });
                    Task.Factory.StartNew(() => ListenClient(activeUser));
                }
                catch
                {

                }
            }
            udpClient.Close();
        }

        private void NewUserConnect()
        {
            TcpListener tcpListener = new TcpListener(userInfo.IpAddress, 5556);
            tcpListener.Start();
            try
            {
                while (true)
                {
                    TcpClient tcpNewClient = tcpListener.AcceptTcpClient();

                    ActiveUser newActiveUser = new ActiveUser(tcpNewClient);

                    Task.Factory.StartNew(() => ListenClient(newActiveUser));
                }
            }
            catch
            {

            }
            finally
            {
                if (tcpListener != null)
                {
                    tcpListener.Stop();
                }
            }
        }

        public void ListenClient(ActiveUser activeUser)
        {
            while (true)
            {
                string tcpMessage = activeUser.ReceiveMessageTCP();


                switch (tcpMessage[0])
                {
                    case '0':
                        {
                            tcpMessage =tcpMessage.Substring(1);
                           /* textBox.Invoke((MethodInvoker)delegate
                            {
                                // Running on the UI thread
                                textBox.Text += "Add"+ tcpMessage + "\r\n";
                            });*/
                            userList.ListOfUsers.Add( activeUser, tcpMessage);
                            break;
                        }
                    case '1':
                        textBox.Invoke(new MethodInvoker(() =>
                        {
                            string name;
                            userList.ListOfUsers.TryGetValue(activeUser, out name);
                            textBox.Text +=  DateTime.Now.ToString()+ " " +name + " " + activeUser.ipAddress + " покидает чат" + "\r\n";

                        }));
                        userList.ListOfUsers.Remove(activeUser);
                        return;
                    case '2':
                        textBox.Invoke((MethodInvoker)delegate
                        {
                            // Running on the UI thread
                            string name;
                            userList.ListOfUsers.TryGetValue(activeUser, out name);
                            textBox.Text +=  DateTime.Now.ToString()+ " " +name + " "+ activeUser.ipAddress+ " "+ tcpMessage.Substring(1) + "\r\n";
                        }); 
                        break;
                }
                /*textBox.Invoke((MethodInvoker)delegate
                {
                    // Running on the UI thread
                    textBox.Text += tcpMessage+ "\r\n";
                });*/
            }
        }

        public void Disconnect()
        {
            foreach(var connect in userList.ListOfUsers.Keys)
            {
                connect.StopTCP();
            }
        }
    }
}