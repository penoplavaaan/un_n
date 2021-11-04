using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        public Rational(int Numerator, int Denominator)
        {
            (this.numerator, this.denominator) = (Numerator, Denominator);

            if (Denominator % Numerator == 0 && Numerator != 1)
            {
                (this.numerator, this.denominator) = (Numerator / Numerator, Denominator / Numerator);

                if(this.denominator < 0 && this.numerator > 0)
                {
                    this.numerator *= -1;
                    this.denominator *= -1;
                }
            }

            this.isNan = this.Denominator == 0;
         
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

        public Rational DivideOnInt(Rational a, int divider)
        {
            return new Rational(a.Numerator / divider, a.Denominator / divider);
        }

        public static Rational operator +(Rational a, Rational b)
        {
            return new Rational(a.Numerator * b.Denominator + b.Numerator*a.Denominator, a.Denominator * b.Denominator);
        }

        public static Rational operator *(Rational a, Rational b)
        {
            return new Rational(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static Rational operator /(Rational a, Rational b)
        {
            return new Rational(a.Numerator * b.Denominator, a.Denominator * b.Numerator); 
        }

        

        
    }
}
