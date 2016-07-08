using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttribDemo
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple=true)]    
    class CodeReviewAttribute : Attribute
    {
        public string Name { get; }
        public string Date { get; }
        public bool IsApproved { get; }

        public CodeReviewAttribute(string name, string date, bool isApproved)
        {
            this.Name = name;
            this.Date = date;
            this.IsApproved = isApproved;
        }

        public override string ToString()
        {
            return String.Format("Name: "+Name+"\t Date: "+Date+"\t Is approved: " + IsApproved);
        }
    }
}
