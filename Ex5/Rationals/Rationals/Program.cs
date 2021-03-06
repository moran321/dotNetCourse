﻿/** Moran Ankori Lab 3.2 **/
/** Lab Appendix **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rationals
{
    public struct Rational
    {
        //fields:
        private int numerator;
        private int denominator;

        //properties:
        public int Numerator
        {
            get
            {
                return numerator;
            }
        }
        public int Denominator
        {
            get
            {
                return denominator;
            }
        }
        public double Number
        {
            get
            {
                return numerator / denominator;
            }
        }

        //methods:

        //Ctor
        public Rational(int n, int d)
        {
            numerator = n;
            denominator = d;
        }
        //Ctor
        public Rational(int num)
        {
            numerator = num;
            denominator = 1;
        }

        public Rational Add(Rational another_numer)
        {
            int numer = another_numer.denominator * this.numerator
                + this.denominator * another_numer.numerator;
            int denom = this.denominator * another_numer.denominator;

            return (new Rational(numer, denom));
        }

        public Rational Sub(Rational another_numer)
        {
            int numer = another_numer.denominator * this.numerator
                - this.denominator * another_numer.numerator;
            int denom = this.denominator * another_numer.denominator;

            return (new Rational(numer, denom));
        }

        public Rational Mul(Rational another_numer)
        {
            int numer = another_numer.numerator * this.numerator;
            int denom = this.denominator * another_numer.denominator;

            return (new Rational(numer, denom));
        }

        public Rational Div(Rational another_numer)
        {
            int numer = another_numer.denominator * this.numerator;
            int denom = this.denominator * another_numer.numerator;

            return (new Rational(numer, denom));
        }

        public void Reduce()
        {
            int g = Gcd(numerator, denominator);
            numerator /= g;
            denominator /= g;
        }

        //return the greatest common divider
        private int Gcd(int m, int n)
        {
            if (m < 0) m = -m;
            if (n < 0) n = -n;
            if (0 == n) return m;
            else return Gcd(n, m % n);
        }

        public override String ToString()
        {
            if (denominator == 1)
            {
                return (numerator + "");
            }
            else
            {
                return numerator + "/" + denominator;
            }
        }

        public override bool Equals(object obj)
        {
            Rational another_rational;
            if (obj is Rational)
            {
                another_rational = (Rational)obj;
            }
            else
            {
                return false;
            }
            int num1 = this.numerator * another_rational.denominator;
            int num2 = this.denominator * another_rational.numerator;
            if (num1 < num2)
            {
                return false;
            }
            if (num1 > num2)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            //int result = this.denominator ^ this.numerator;
            return this.ToString().GetHashCode();
        }


        /********************* Lab Appendix **********************/

        public static Rational operator +(Rational r, Rational other)
        {
            return r.Add(other);
        }


        public static Rational operator *(Rational r, Rational other)
        {
            return r.Mul(other);
        }

        public static Rational operator -(Rational r, Rational other)
        {
            return r.Sub(other);
        }

        public static Rational operator /(Rational r, Rational other)
        {
            return r.Div(other);
        }

        //explicit casting from Rational to double
        public static explicit operator double(Rational r)
        {
            double d = r.numerator / r.denominator;
            return d;
        }
        //explicit casting from int to Rational 
        public static explicit operator Rational(int num)
        {
            Rational r = new Rational(num);
            return r;
        }


    } /*********************/

    class Program
    {
        static void Main(string[] args)
        {

            //// Lab 3 test:
            Rational number = new Rational(4, 5);
            Rational number2 = new Rational(16, 20);
            Rational number3 = new Rational(5);

            Console.WriteLine("numbe1: {0}, number2: {1} , number3: {2}", number.ToString(), number2.ToString(), number3.ToString());
            number2.Reduce();
            Console.WriteLine("reduced number2: " + number2.ToString());
            number.Mul(number);
            Console.WriteLine("number1 * number2: " + number.Mul(number).ToString());
            Console.WriteLine("Is equal? " + number.Equals(number2));
            Console.WriteLine("Is number equals to 7? " + number.Equals(7));
            Console.WriteLine("number1 + number2: " + number.Add(number2).ToString());

            //Lab appendix test:
            Console.WriteLine("operator + : number+number2= " + (number + number2).ToString());
            Console.WriteLine("operator * : number*number2= " + (number * number2).ToString());
            Console.WriteLine("operator / : number/number2= " + (number / number2).ToString());
            Console.WriteLine("operator - : number-number2= " + (number - number2).ToString());
            Console.WriteLine("(double) casting : (double)(number2)= " + ((double)(number2)).ToString());
            Console.WriteLine("casting : (Rational)number3= " + ((Rational)(number3)).ToString());

            Console.Read();
        }/*********************/




    }
}
