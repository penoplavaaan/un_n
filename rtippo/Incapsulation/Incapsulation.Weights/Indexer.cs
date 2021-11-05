using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Weights
{
	public class Indexer
    {
        private double[] array;
        private int start;
        private int length;
        public int Length
        {
            get { return this.length; }
        }

        public Indexer(double[] arr, int start = 0, int length=0)
        {
            if (ValidateRange(arr, start, length))
            {
                array = arr;
                this.start = start;
                this.length = length;
            }
            else
            {
                throw new ArgumentException("Range is invalid.");
            }    
        }

        

        public double this[int ind]
        {
            get {
                int lastIndex = GetLastIndex(ind);

                if (LastIndexIsCorrect(lastIndex))
                {
                    return array[lastIndex];
                }

                else
                {
                    throw new IndexOutOfRangeException("Index is incorrect!");
                }
            }
            set {
                int lastIndex = GetLastIndex(ind);

                if (LastIndexIsCorrect(lastIndex))
                {
                    array[lastIndex] = value;
                }

                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }


        private bool ValidateRange(double[] arr, int start, int length)
        {
            return !(start < 0 || length < 0 || ((start + length) > arr.Length));
        }

        private bool LastIndexIsCorrect(int lastIndex)
        {
            return !(lastIndex > length || lastIndex < start);
        }

        private int GetLastIndex(int index)
        {
            return start + index;
        }
    }
}
