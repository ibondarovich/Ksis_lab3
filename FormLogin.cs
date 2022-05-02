using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_lab3
{
    public partial class FormLogin : Form
    {
        private FormChat formChat;

        
        public FormLogin()
        { 
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text != "" && textBoxIP.Text != "")
            {
                formChat = new FormChat();
                formChat.UserName = textBoxName.Text;
                formChat.UserIP = textBoxIP.Text;

               

                formChat.ShowDialog();
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите свое имя!");
            }  
        }
    }
}
