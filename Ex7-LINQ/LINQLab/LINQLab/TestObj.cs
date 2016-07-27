using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLab
{
    public class TestObj
    {
        public string Name { get; }
        public int ID { get; }
        public string Adress { get; set; }

        public TestObj(string name, int id, string adress)
        {
            Name = name;
            ID = id;
            Adress = adress;
        }

        public override string ToString()
        {
            return string.Format($"Name:{Name}, ID: {ID}, Adress: {Adress}");
        }
    }
}
