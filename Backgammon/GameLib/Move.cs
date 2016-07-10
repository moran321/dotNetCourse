using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class Move
    {
        public int From { get; }
        public int To { get; }
        public enum MoveType { Regular, Eat, Eaten, Out }
        public MoveType Type { get; }
        public int Length
        {
            get
            {
                return Math.Abs(From - To);
            }
        }/****************************************/

        public Move(int from, int to, MoveType t)
        {
            From = from;
            To = to;
            if (from == 0 || from==25)
                Type = MoveType.Eaten;
            else
                Type = t;
        }/****************************************/

        public override string ToString()
        {
            if (From==25 || From == 0)
            {
                return string.Format("From: Out, To: {1}", To);
            }
            if (To == 25 || To == 0)
            {
                return string.Format("From: {0}, To: Out", From);
            }
            return string.Format("From: {0}, To: {1}", From, To);
        } /****************************************/

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        } /****************************************/

        public override bool Equals(object obj)
        {
            Move other = obj as Move;
            if (other != null)
            {
                return (this.From == other.From && this.To == other.To);
            }
             return false;
        }  /****************************************/


    }
}
