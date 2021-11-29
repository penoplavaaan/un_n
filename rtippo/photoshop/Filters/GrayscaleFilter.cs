using System;

namespace MyPhotoshop
{
	public class GrayscaleFilter : PixelFilter<EmptyParameters>
	{
		 
		
		public override string ToString ()
		{
			return "Оттенки серого";
		}

		 

		public override Pixel ProcessPixel(Pixel original, EmptyParameters parameters)
		{
			var lighttness = original.R + original.G + original.B;
			lighttness /= 3;
			return new Pixel(lighttness, lighttness, lighttness);
		}


	}
}

