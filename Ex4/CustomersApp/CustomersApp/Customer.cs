/* Lab 7.1 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{
   public class Customer : IComparable<Customer> , IEquatable<Customer>       
    {
        public string Name { set; get; }
        public int ID { set; get; }
        public string Adress { set; get; }

        public Customer(){ }

        public Customer(string name, int id, string adress)
        {
            Name = name;
            ID = id;
            Adress = adress;
        }

        //implement IComparable
        public int CompareTo(Customer other)
        {
            //compare by name
           return (string.Compare(this.Name, other.Name, true));
        }
       
        //implement IEquatable
        public bool Equals(Customer other)
        {
            if (this.CompareTo(other)==0 && this.ID==other.ID)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return ( "Name: " + Name + ", ID:" + ID + ", Adress: " + Adress);
        }

    }//
}//
