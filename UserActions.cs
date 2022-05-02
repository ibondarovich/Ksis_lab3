using System;

namespace Chat_lab3
{
    class UserActions : IUserActions
    {
        private string userName;
        public UserActions(string userName)
        {
            this.userName = userName;
        }

        public string UserEntered()
        {
            return DateTime.Now.ToShortTimeString()+ " " +  userName + " входит в чат\r\n";
            
        }

        public string UserLeft()
        {
            throw new Exception();
        }

    }
}
