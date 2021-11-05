using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        public Rational(int numerator, int denominator = 1, bool isNan = false)
        {
            this.isNan = isNan;
            if (denominator == 0)
            {
                this.isNan = true;
                (this.numerator, this.denominator) = (numerator, denominator);
            } 
            else
            {
                int gcd = Get_Greatest_Common_Divisor(numerator, denominator);
                (this.numerator, this.denominator) = (numerator / gcd, denominator / gcd);
                
                if (this.denominator < 0 && this.numerator > 0)
                {
                    this.numerator *= -1;
                    this.denominator *= -1;
                } 
            }
        }

        private int numerator;
        public int Numerator
        {
            get { return this.numerator; }
            set { this.numerator = value; }
        }

        private int denominator;
        public int Denominator
        {
            get { return this.denominator; }
            set { this.denominator = value; }
        }

        private bool isNan;
        public bool IsNan
        {
            get { return this.isNan; }
            set { this.isNan = value; }
        }
         

        public static Rational operator +(Rational a, Rational b)
        { 
            return new Rational(
                a.Numerator * b.Denominator + b.Numerator*a.Denominator, 
                a.Denominator * b.Denominator, 
                (a.IsNan || b.IsNan)
                );
        }

        public static Rational operator -(Rational a, Rational b)
        {
            return new Rational(
                a.Numerator * b.Denominator - b.Numerator * a.Denominator, 
                a.Denominator * b.Denominator, 
                (a.IsNan || b.IsNan)
                );
        }

        public static Rational operator *(Rational a, Rational b)
        {
            return new Rational(
                a.Numerator * b.Numerator, 
                a.Denominator * b.Denominator, 
                (a.IsNan || b.IsNan)
                );
        }

        public static Rational operator /(Rational a, Rational b)
        {
            return new Rational(
                a.Numerator * b.Denominator, 
                a.Denominator * b.Numerator, 
                (a.IsNan  || b.IsNan )
                ); 
        }

        public static implicit operator double(Rational a)
        {
            return a.Denominator == 0 ? double.NaN : (double)(a.Numerator) / a.Denominator;
        }

        public static explicit operator int(Rational a)
        {
            if (a.Numerator % a.Denominator == 0)
            {
                return a.Numerator / a.Denominator;
            }
            else
            {
                throw new System.Exception();
            }
        }

        public static implicit operator Rational(int a)
        {
            return new Rational(a);
        }

        public int Get_Greatest_Common_Divisor(int a, int b)
        {
            if (a % b == 0)
                return b;
            if (b % a == 0)
                return a;
            if (a > b)
                return Get_Greatest_Common_Divisor(a % b, b);
            return Get_Greatest_Common_Divisor(a, b % a);
        }
    }
}
