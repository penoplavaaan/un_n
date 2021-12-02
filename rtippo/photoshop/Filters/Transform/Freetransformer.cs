using System; 
using System.Drawing; 

namespace MyPhotoshop
{
    public class Freetransformer : ITransformer<EmptyParameters>
    {
        Func<Size, Size> sizeTransformer;
        Func<Point, Size, Point> pointTransformer;

        Size oldSize; 

        public Freetransformer(Func<Size, Size> sizeTransformer, Func<Point, Size, Point> pointTransformer)
        {
            this.sizeTransformer = sizeTransformer;
            this.pointTransformer = pointTransformer;
        }

        public Size ResultSize
        {
            get;
            private set;
        }

        public Point? MapPoint(Point newPoint)
        {
            return pointTransformer(newPoint, oldSize);
        }

        public void Prepare(Size size, EmptyParameters parameters)
        {
            oldSize = size;
            ResultSize = sizeTransformer(oldSize);
        }
    }
}
