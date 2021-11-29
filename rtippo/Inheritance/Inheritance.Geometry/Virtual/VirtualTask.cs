using System;
using System.Collections.Generic;
using System.Linq;

namespace Inheritance.Geometry.Virtual
{
    abstract public class Body
    {
        public Vector3 Position { get; }

        protected Body(Vector3 position)
        {
            Position = position;
        }

        abstract public bool ContainsPoint(Vector3 point);

        abstract public RectangularCuboid GetBoundingBox();
    }

    public class Ball : Body
    {
        public double Radius { get; }

        public Ball(Vector3 position, double radius) : base(position)
        {
            Radius = radius;
        }

        public override bool ContainsPoint(Vector3 point)
        {
            var vector = point - Position;
            var length2 = vector.GetLength2();
            return length2 <= Radius * Radius;
        }

        public override RectangularCuboid GetBoundingBox()
        {
            return new RectangularCuboid(Position, 2 * Radius, 2 * Radius, 2 * Radius);
        }
    }

    public class RectangularCuboid : Body
    {
        public double SizeX { get; }
        public double SizeY { get; }
        public double SizeZ { get; }
        public Vector3 minPoint { get; }
        public Vector3 maxPoint { get; }

        public RectangularCuboid(Vector3 position, double sizeX, double sizeY, double sizeZ) : base(position)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
            minPoint = new Vector3(
               Position.X - SizeX / 2,
               Position.Y - SizeY / 2,
               Position.Z - SizeZ / 2
               );
            maxPoint = new Vector3(
               Position.X + SizeX / 2,
               Position.Y + SizeY / 2,
               Position.Z + SizeZ / 2
               );
        }

        public override bool ContainsPoint(Vector3 point)
        {
            return point >= minPoint && point <= maxPoint;
        }

        public override RectangularCuboid GetBoundingBox()
        {
            return new RectangularCuboid(Position, SizeX, SizeY, SizeZ);
        }
    }

    public class Cylinder : Body
    {
        public double SizeZ { get; }

        public double Radius { get; }

        public Cylinder(Vector3 position, double sizeZ, double radius) : base(position)
        {
            SizeZ = sizeZ;
            Radius = radius;
        }

        public override bool ContainsPoint(Vector3 point)
        {
            var vectorX = point.X - Position.X;
            var vectorY = point.Y - Position.Y;
            var length2 = vectorX * vectorX + vectorY * vectorY;
            var minZ = Position.Z - SizeZ / 2;
            var maxZ = minZ + SizeZ;

            return length2 <= Radius * Radius && point.Z >= minZ && point.Z <= maxZ;
        }

        public override RectangularCuboid GetBoundingBox()
        {
            return new RectangularCuboid(Position, 2 * Radius, 2 * Radius, SizeZ);
        }
    }

    public class CompoundBody : Body
    {
        public IReadOnlyList<Body> Parts { get; }
        RectangularCuboid[] rectangularCuboids;
        public Vector3[] extremePoints { get; set; }

        public CompoundBody(IReadOnlyList<Body> parts) : base(parts[0].Position)
        {
            Parts = parts;

            rectangularCuboids = new RectangularCuboid[Parts.Count];
            for (int i = 0; i < Parts.Count; i++)
            {
                rectangularCuboids[i] = Parts[i].GetBoundingBox();
            }

            extremePoints = new Vector3[] { rectangularCuboids[0].minPoint, rectangularCuboids[0].maxPoint };
        }

        public override bool ContainsPoint(Vector3 point)
        {
            return Parts.Any(body => body.ContainsPoint(point));
        }

        public override RectangularCuboid GetBoundingBox()
        {
            for (int i = 1; i < rectangularCuboids.Length; i++)
            {
                Vector3 t_minPoint = rectangularCuboids[i].minPoint;
                Vector3 t_maxPoint = rectangularCuboids[i].maxPoint;
                extremePoints[0] = new Vector3(
                    Math.Min(t_minPoint.X, extremePoints[0].X),
                    Math.Min(t_minPoint.Y, extremePoints[0].Y),
                    Math.Min(t_minPoint.Z, extremePoints[0].Z)
                );
                extremePoints[1] = new Vector3(
                    Math.Max(t_maxPoint.X, extremePoints[1].X),
                    Math.Max(t_maxPoint.Y, extremePoints[1].Y),
                    Math.Max(t_maxPoint.Z, extremePoints[1].Z)
                );
            }
            Vector3 resultSize = extremePoints[1] - extremePoints[0];
            Vector3 resultPosition = extremePoints[0].CreatePoint(
                resultSize.X / 2,
                resultSize.Y / 2,
                resultSize.Z / 2
            );
            return new RectangularCuboid(resultPosition, resultSize.X, resultSize.Y, resultSize.Z);
        }
    }
}