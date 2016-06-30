/*Moran Ankori*/
/* Lab 7.1 */
/* Lab 8.1 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{
    class Program
    {
        // 8.1) 2. Define a delegate named CustomerFilter that accepts a Customer object and returns a Boolean.
        public delegate bool CustomerFilter(Customer cs); 

        static void Main(string[] args)
        {
            //////////////////// Lab 7 ////////////////////////////////////////////

            /*
            Customer[] customers = new Customer[7];
            customers[0] = new Customer("Stas", 220, "Yokneam");
            customers[1] = new Customer("Moran", 201, "Ramat-Gan");
            customers[2] = new Customer("Guy", 245, "Ramat-Ilan");
            customers[3] = new Customer("Efrat", 322, "Qiryat-Ono");
            customers[4] = new Customer("Stas", 139, "Herzelia");
            customers[5] = new Customer("Stas", 220, "Herzelia");
            customers[6] = new Customer("Stas", 220, "Herzelia");

            int index = 0;
            foreach (Customer customer in customers)
            {
                Console.WriteLine((index++) + ") "+customer.ToString());
            }

            Console.Write("\nIs customers[5] & customers[6] is equal? ");
            Console.WriteLine(customers[5].Equals(customers[6]));
            Console.Write("Is customers[4] & customers[6] is equal? ");
            Console.WriteLine(customers[4].Equals(customers[6]));
            Console.Write("Is customers[0] & customers[6] is equal? ");
            Console.WriteLine(customers[0].Equals(customers[6]));

            Array.Sort(customers);
            Console.WriteLine("\nAfter sort:");
          
            foreach (Customer customer in customers)
            {
                Console.WriteLine(customer.ToString());
            }

            AnotherCustomerComparer comparer = new AnotherCustomerComparer();
           
            Array.Sort(customers, comparer);

            Console.WriteLine("\nAfter sort with comparer:");
            foreach (Customer customer in customers)
            {
                Console.WriteLine(customer.ToString());
            }

            */

            //////////////////// Lab 8 ////////////////////////////////////////////

            // 5) a. Create a list or array of Customer objects.
            Customer[] customers = new Customer[8];
            customers[0] = new Customer("Stas", 220, "Yokneam");
            customers[1] = new Customer("Moran", 91, "Ramat-Gan");
            customers[2] = new Customer("Guy", 245, "Ramat-Ilan");
            customers[3] = new Customer("Efrat", 322, "Qiryat-Ono");
            customers[4] = new Customer("Alex", 139, "Herzelia");
            customers[5] = new Customer("Yarin", 22, "Herzelia");
            customers[6] = new Customer("Liron", 1100, "Bat-Yam");
            customers[7] = new Customer("Ziv", 400, "Tel-Aviv");

            List<Customer> list;

            // 5) b. Create a delegate of type CustomerFilter that should return *true* if its name starts with the letters A-K. 
            //       Use a separate method to implement the delegate.
            CustomerFilter delegate_b = new CustomerFilter(get_AtoK_caustomers);

            // 5) c.  Call GetCustomers with the delegate you created in (b) and display the result.
            list = GetCustomers(customers, delegate_b);
            //display results 
            Console.WriteLine("\nA-K results:");
            foreach (Customer c in list)
            {
                Console.WriteLine(c.ToString());
            }

            // 5) d. Create another such delegate that returns all customers whose names begin with the letters L-Z. Use an anonymous delegate.
            CustomerFilter delegete_d = delegate (Customer cs)
            {
                //letters L-Z
                char firstChar = cs.Name.ToCharArray()[0];
                if ((firstChar >= 'L' && firstChar <= 'Z') || (firstChar >= 'l' && firstChar <= 'z'))
                {
                    return true;
                }
                return false;
            };

            // 5) e. Call GetCustomers again with the new delegate and display the results.
            list = GetCustomers(customers, delegete_d);
            //display results 
            Console.WriteLine("\nL-Z results:");
            foreach (Customer c in list)
            {
                Console.WriteLine(c.ToString());
            }

            // 5) f. Create another such delegate that returns all customers whose ID is less than 100. Use a lambda expression.
            CustomerFilter delegete_f = n =>
            {
                // ID is less than 100
                if (n.ID < 100)
                {
                    return true;
                }
                return false;
            };
            // 5) g. Call GetCustomers again with the new delegate and display the results.  
           list =  GetCustomers(customers, delegete_f);
            //display results 
            Console.WriteLine("\n<100 results:");
            foreach (Customer c in list)
            {
                Console.WriteLine(c.ToString());
            }

        } //************************ end main method ************************ //


        // 5) b. implement the delegate.
        // letters A - K
        private static bool get_AtoK_caustomers(Customer cs)
        {
            char firstChar = cs.Name.ToCharArray()[0];
            if ((firstChar >= 'A' && firstChar <= 'K') || (firstChar >= 'a' && firstChar <= 'k'))
            {
                return true;
            }
            return false;
        }////************************


        /* 8.1) 3.  add static method named GetCustomers that accepts a collection of customers
                    and an instance of the CustomFilter delegate and returns a collection of Customers. 
                4.  This method should use the supplied delegate to filter the customer list so that only certain customers are returned */
        private static List<Customer> GetCustomers(ICollection<Customer> coll, CustomerFilter cf)
        {
            List<Customer> list = new List<Customer>();
            foreach (Customer c in coll)
            {
                if (cf(c) == true) //invoke the delegate method
                {
                    list.Add(c); 
                }
            }

            return list;

        }//end GetCustomers//***********************


    }//end class//************************
}//
