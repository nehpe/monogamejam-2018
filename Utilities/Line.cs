using Microsoft.Xna.Framework;

namespace MonoGameJam.Utilities
{
    public class Line
    {
        public Vector2 start;
        public Vector2 end;

        public Line(Vector2 a, Vector2 b)
        {
            this.start = a;
            this.end = b;
        }

        public Vector2? Intersects(Rectangle rect)
        {
            Vector2 p0, p1, p2, p3;

            p0 = new Vector2(rect.X, rect.X+rect.Width);
            p1 = new Vector2(rect.X+rect.Width, rect.Y+rect.Height+rect.Width);
            p2 = new Vector2(rect.Y+rect.Height+rect.Width, rect.X+rect.Height);
            p3 = new Vector2(rect.X+rect.Height, rect.X);

            Line l0, l1, l2, l3;
            l0 = new Line(p0, p1);
            l1 = new Line(p1, p2);
            l2 = new Line(p2, p3);
            l3 = new Line(p3, p0);

            Vector2? intersection;

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

        public Vector2? Intersects(Line line)
        {
            float x1 = this.start.X;
            float y1 = this.start.Y;
            float x2 = this.end.X;
            float y2 = this.end.Y;

            float x3 = line.start.X;
            float y3 = line.end.X;
            float x4 = line.start.Y;
            float y4 = line.end.Y;


            float denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (denominator == 0)
                return null;
            
            float xNominator = (x1*y2 - y1*x2)*(x3 - x4) - (x1 - x2)*(x3*y4 - y3*x4);
            float yNominator = (x1*y2 - y1*x2)*(y3 - y4) - (y1 - y2)*(x3*y4 - y3*x4);

            float px = xNominator / denominator;
            float py = yNominator / denominator;

            return new Vector2(px, py); 
        }
    }
}