/* Lab 8.2 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSystem
{

    // 3. Create a class called MailManager
    
    class MailManager
    {

        /* 3. This class should expose an event called MailArrived
              based on the EventHandler<T> delegate where T is a new class called MailArrivedEventArgs.*/
        public delegate void EventHandler<T>(object sender, T e) where T : MailArrivedEventArgs;
        public event EventHandler<MailArrivedEventArgs> MailArrived;



        // 5. Create a protected virtual method called OnMailArrived, 
        //    taking a MailArrivedEventArgs argument. Inside, raise the event.
        protected virtual void OnMailArrived(MailArrivedEventArgs e)
        {
            if (MailArrived != null)
            {
                MailArrived(this, e);
            }
        }

        // 6. Create a simple method called SimulateMailArrived, that calls OnMailArrived with some dummy data
        public void SimulateMailArrived()
        {
            OnMailArrived(new MailArrivedEventArgs("The dummy title","My dummy body"));
        }


    }//end class
}
