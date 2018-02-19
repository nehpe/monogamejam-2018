using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJam.Utilities
{
    public class Line
    {
        public Vector2 start;
        public Vector2 end;
        public Line(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        // Check if the line intersects the given rectangle
        public Vector2? Intersects(Rectangle rectangle)
        {
            // Rectangle Points
            Vector2 p0, p1, p2, p3;
            p0 = new Vector2(rectangle.X, rectangle.X + rectangle.Width);
            p1 = new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Width + rectangle.Height);
            p2 = new Vector2(rectangle.X + rectangle.Height, rectangle.Y + rectangle.Width + rectangle.Height);
            p3 = new Vector2(rectangle.X, rectangle.Y + rectangle.Height);

            Line l0 = new Line(p0, p1);
            Line l1 = new Line(p1, p2);
            Line l2 = new Line(p2, p3);
            Line l3 = new Line(p3, p0);

            Vector2? intersection = null;
            intersection = this.Intersects(l0);
            if (intersection != null) return intersection;
            intersection = this.Intersects(l1);
            if (intersection != null) return intersection;
            intersection = this.Intersects(l2);
            if (intersection != null) return intersection;
            intersection = this.Intersects(l3);
            if (intersection != null) return intersection;

            return null;
        }

        // Check if the line intersects another line, 
        // and if it does, return the intersection
        public Vector2? Intersects(Line line)
        {
            float x1 = this.start.X;
            float y1 = this.start.Y;
            float x2 = this.end.X;
            float y2 = this.end.Y;

            float x3 = line.start.X;
            float y3 = line.start.Y;
            float x4 = line.end.X;
            float y4 = line.end.Y;

            float denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (denominator == 0)
                return null;

            float xNominator = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
            float yNominator = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);

            float px = xNominator / denominator;
            float py = yNominator / denominator;

            return new Vector2(px, py);
        }
    }
}
