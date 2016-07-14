using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttribDemo
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple=true)]    
    public class CodeReviewAttribute : Attribute
    {

        private readonly string _name;
        private readonly bool _isApproved;
        private readonly DateTime _date;

        public string Name
        {
            get { return _name; }
        }

        public DateTime Date
        {
            get { return _date; }
        }

        public bool IsApproved
        {
            get { return _isApproved; }
        }

        public CodeReviewAttribute(string name, string date, bool isApproved)
        {
            this._name = name;
            this._date = DateTime.Parse(date);
            this._isApproved = isApproved;
        }

        public override string ToString()
        {
            return String.Format("Name: "+ _name+"\t Date: "+_date+"\t Is approved: " + _isApproved);
        }
    }
}
