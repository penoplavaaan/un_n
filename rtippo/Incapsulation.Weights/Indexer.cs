using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Weights
{
	public class Indexer
    {
        
        public Indexer(double[] arr, int start, int length)
        {
            this.start = start;
            this.length = length;
            try
            { 
                if (this.length != 0)
                {
                    if (this.length > arr.Length)
                    {
                        throw new ArgumentException("range is invalid. Start = " + start + ", Length = " + length);
                    }
                    else
                    {
                        array = arr;
                    }
                    
                }
                
                else array = new double[1];
                 

                if (length > arr.Length - this.start && this.length != 0 && this.start != 0) { 
                    throw new ArgumentException("range is invalid. Start = " + start + ", Length = " + length);
                }

                Console.WriteLine();
                int count = 0;
                for (int i = this.start; i < this.start + this.length; i++)
                {
                    array[count] = arr[i];
                    count++; 
                }

                ///
                
                Console.WriteLine("Valid range: " + this.start + " " + this.length);
                foreach(double elem in array)
                {
                    Console.Write(elem + " ");
                }
                Console.WriteLine();
                ///
            }
            catch(Exception e)
            { 
                throw new ArgumentException("range is invalid. Start = " + this.start + ", Length = " + this.length +"  Type is: " + e);
            }  
        }

        private double[] array; 

        private int start;
        public int Start
        {
            get { return start; }
            set { start = value; }
        }

        private int length;
        public int Get_Length
        {
            get { return length; }
            set { length = value; }
        }


        public double this[int ind]
        {
            get { return array[ind]; }
            set { array[ind] = value; }
        }

        public int Length
        {
            get { return this.length; }
        }
    }
}
