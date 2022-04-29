using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_lab3
{
    public partial class FormLogin : Form
    {
        public string userName;
        public string userIP;

        public FormLogin()
        { 
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text != "" && textBoxIP.Text != "")
            {
                userName = textBoxName.Text;
                userIP = textBoxIP.Text;

                FormChat formChat = new FormChat();
                formChat.ShowDialog();
            }
            else
            {
                MessageBox.Show("Введите свое имя!");
            }
            
        }
    }
}
