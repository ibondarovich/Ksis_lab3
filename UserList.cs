using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
namespace Chat_lab3
{
    public class UserList
    {
        public Dictionary<ActiveUser, string> ListOfUsers { get; set; }

        public UserList()
        {
            ListOfUsers = new Dictionary<ActiveUser, string>();
        }
    }
}