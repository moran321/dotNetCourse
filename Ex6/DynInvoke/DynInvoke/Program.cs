/* Moran Ankori */
/* Lab 1.1 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            B b = new B();
            C c = new C();
            D d = new D();

            string output = "";
            try {
                output += (string)InvokeHello(a, " A object")+"\n";
                output += (string)InvokeHello(b, " B object") + "\n";
                output += (string)InvokeHello(c, " C object") + "\n";
                output += (string)InvokeHello(d, " D object") + "\n";

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
               
            }catch (MissingMethodException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR: This object doesn't have Hello method\n");
            }

            Console.WriteLine(output);

        }


        public static object InvokeHello(object obj, string str)
        {
            Type objType = obj.GetType();
            object returnedStr = objType.InvokeMember("Hello", System.Reflection.BindingFlags.InvokeMethod , 
                null, obj, new object[] {str});
            return returnedStr;
        }



    }
}
