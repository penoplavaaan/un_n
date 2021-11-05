using System;

namespace MyPhotoshop
{
	public class GrayscaleFilter : PixelFilter
	{
		 
		
		public override string ToString ()
		{
			return "Оттенки серого";
		}

		public GrayscaleFilter() : base(new EmptyParameters()) { }

		public override Pixel ProcessPixel(Pixel original, IParameters parameters)
		{
			var lighttness = original.R + original.G + original.B;
			lighttness /= 3;
			return new Pixel(lighttness, lighttness, lighttness);
		}


	}
}

