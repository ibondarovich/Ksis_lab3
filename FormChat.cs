using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Chat_lab3
{
    public partial class FormChat : Form
    {
        IPAddress iPAddress;
        NewUser newUser;
        
        public FormChat()
        {
            InitializeComponent();
        }

        private void FormChat_Load(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            string userName = formLogin.userName;
            string userIP = formLogin.userIP;

            iPAddress = IPAddress.Parse(userIP);

            //newUser = new NewUser(userIP);
        }
    }
}
