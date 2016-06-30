/*8.2*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSystem
{

    class MailArrivedEventArgs : EventArgs
    {

        // 4. The MailArrivedEventArgs should expose two read only properties called Title and Body
        public string Title { get; } 
        public string Body { get; }

        //C'tor
        public MailArrivedEventArgs(string t, string b)
        {
            Title = t;
            Body = b;
        }

    }//end class
}
