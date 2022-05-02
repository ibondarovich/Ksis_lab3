using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_lab3
{
    interface IUserActions
    {
        string UserEntered();

        string UserLeft();
    }
}
