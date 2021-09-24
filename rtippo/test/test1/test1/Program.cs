using System;

namespace test1
{
    class Program
    {
        public static void Main()
        {
            var a = new A();
            a.Number = 1;
        }

        class A
        {
            int number;
            public int Number
            {
                set
                {
                    number = value;
                    Console.WriteLine("Hello, world!");
                }
            }
        }
    }
}
