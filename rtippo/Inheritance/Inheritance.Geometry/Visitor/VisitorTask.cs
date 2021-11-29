
using System;
using System.Collections.Generic;

namespace Inheritance.Geometry.Visitor
{
    public interface IVisitor
    {
        Body Visit(Ball ball);
        Body Visit(RectangularCuboid cube);
        Body Visit(Cylinder cylinder);
        Body Visit(CompoundBody compoundBody);

    }


    public abstract class Body
    {
        public Vector3 Position { get; }

        protected Body(Vector3 position)
        {
            Position = position;
        }

        public abstract Body Accept(IVisitor visitor);
    }

    public class Ball : Body
    {
        public double Radius { get; }

        public Ball(Vector3 position, double radius) : base(position)
        {
            Radius = radius;
        }

        public override Body Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
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

        public override Body Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
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

        public override Body Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class CompoundBody : Body
    {
        public IReadOnlyList<Body> Parts { get; }
        public RectangularCuboid[] rectangularCuboids { get; }
        public Vector3[] extremePoints { get; set; }


        public CompoundBody(IReadOnlyList<Body> parts) : base(parts[0].Position)
        {
            Parts = parts;

            rectangularCuboids = new RectangularCuboid[Parts.Count];
            for (int i = 0; i < Parts.Count; i++)
            {
                var boundingBoxVisitor = new BoundingBoxVisitor();
                rectangularCuboids[i] = (RectangularCuboid)Parts[i].Accept(boundingBoxVisitor);
            }

            extremePoints = new Vector3[] { rectangularCuboids[0].minPoint, rectangularCuboids[0].maxPoint };
        }

        public override Body Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class BoundingBoxVisitor : IVisitor
    {
        public Body Visit(Ball ball)
        {
            return new RectangularCuboid(
                ball.Position,
                2 * ball.Radius,
                2 * ball.Radius,
                2 * ball.Radius
                );
        }

        public Body Visit(RectangularCuboid rectangularCuboid)
        {
            return new RectangularCuboid(
                rectangularCuboid.Position,
                rectangularCuboid.SizeX,
                rectangularCuboid.SizeY,
                rectangularCuboid.SizeZ
                );
        }

        public Body Visit(Cylinder cylinder)
        {
            return new RectangularCuboid(
                cylinder.Position,
                2 * cylinder.Radius,
                2 * cylinder.Radius,
                cylinder.SizeZ
                );
        }

        public Body Visit(CompoundBody compound)
        {
            for (int i = 1; i < compound.rectangularCuboids.Length; i++)
            {
                Vector3 t_minPoint = compound.rectangularCuboids[i].minPoint;
                Vector3 t_maxPoint = compound.rectangularCuboids[i].maxPoint;
                compound.extremePoints[0] = new Vector3(
                    Math.Min(t_minPoint.X, compound.extremePoints[0].X),
                    Math.Min(t_minPoint.Y, compound.extremePoints[0].Y),
                    Math.Min(t_minPoint.Z, compound.extremePoints[0].Z)
                );
                compound.extremePoints[1] = new Vector3(
                    Math.Max(t_maxPoint.X, compound.extremePoints[1].X),
                    Math.Max(t_maxPoint.Y, compound.extremePoints[1].Y),
                    Math.Max(t_maxPoint.Z, compound.extremePoints[1].Z)
                );
            }
            Vector3 resultSize = compound.extremePoints[1] - compound.extremePoints[0];
            Vector3 resultPosition = compound.extremePoints[0].CreatePoint(
                resultSize.X / 2,
                resultSize.Y / 2,
                resultSize.Z / 2
            );
            return new RectangularCuboid(resultPosition, resultSize.X, resultSize.Y, resultSize.Z);
        }
    }

    public class BoxifyVisitor : IVisitor
    {
        BoundingBoxVisitor boundingBoxVisitor = new BoundingBoxVisitor();

        public Body Visit(Ball ball)
        {
            return ball.Accept(boundingBoxVisitor);
        }

        public Body Visit(RectangularCuboid rectangularCuboid)
        {
            return rectangularCuboid.Accept(boundingBoxVisitor);
        }

        public Body Visit(Cylinder cylinder)
        {
            return cylinder.Accept(boundingBoxVisitor);
        }

        public Body Visit(CompoundBody compoundBody)
        {
            var figureList = new List<Body> { };
            foreach (Body figure in compoundBody.Parts)
            {
                BoxifyVisitor boxifyVisitor = new BoxifyVisitor();
                figureList.Add(figure.Accept(boxifyVisitor));
            }
            return new CompoundBody(figureList);
        }
    }
}