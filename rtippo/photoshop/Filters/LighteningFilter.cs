using System;

namespace MyPhotoshop
{
	public class LighteningFilter : PixelFilter
	{
		 
		
		public override string ToString ()
		{
			return "Осветление/затемнение";
		}
		 
		public LighteningFilter() : base(new LighteningParameters()) { }

		public override Pixel ProcessPixel(Pixel original, IParameters parameters)
        {
			return original * (parameters as LighteningParameters).Coefficient;
        }
		 

		
	}
}

