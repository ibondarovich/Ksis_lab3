using System;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace Chat_lab3
{
    public partial class FormChat : Form
    {
        private IPAddress iPAddress;
        private NewUser newUser;
        
        public string UserName { get; set; }
        public string UserIP { get; set; }

        public FormChat()
        {
            InitializeComponent();
        }

        private void FormChat_Load(object sender, EventArgs e)
        {
            iPAddress = IPAddress.Parse(UserIP);
            newUser = new NewUser(textBoxChat, new UserInfo(iPAddress, UserName));
            this.Text = UserName + " " + iPAddress;
        }

        private void FormChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var user in newUser.userList.ListOfUsers.Keys)
            {
                user.SendMessageTCP("1" + textBoxMessage.Text);
            }
            newUser.Disconnect();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            textBoxChat.Text += UserName + " " + UserIP + " " + textBoxMessage.Text+"\r\n";
            foreach(var user in newUser.userList.ListOfUsers.Keys)
            {
                user.SendMessageTCP("2"+textBoxMessage.Text);
            }

            textBoxMessage.Text = string.Empty;
        }
    }
}
