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
        public Indexer(double[] arr, int start, int length)
        {
            
        }

        public double this[int ind]
        {
            get { return array[ind]; }
        }
    }
}
