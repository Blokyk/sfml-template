public static class VectorUtils
{

    public static float SqLength(this Vector2f u) => u.X * u.X + u.Y * u.Y;
    public static float Length(this Vector2f u) => MathF.Sqrt(SqLength(u));

    public static Vector2f Normalize(this Vector2f u) => u / Length(u);

    public static float Dot(this Vector2f u, Vector2f v) => u.X * v.X + u.Y * v.Y;
    public static float AngleWith(this Vector2f u, Vector2f v) => (v - u).Phase();//MathF.Acos(Dot(u, v) / (Length(u) * Length(v)));
    public static float Phase(this Vector2f u) => MathF.Atan2(u.Y, u.X);

    public static Vector2f FromAngle(float angle, float length = 1f) => length * new Vector2f(MathF.Cos(angle), MathF.Sin(angle));

    public static bool InBounds(this Vector2f z, Vector2f topleft, Vector2f bottomright) {
        if (bottomright.Y > topleft.Y) {
            var tmp = topleft.Y;
            topleft = new Vector2f(topleft.X, bottomright.Y);
            bottomright = new Vector2f(bottomright.X, tmp);
        }

        if (bottomright.X < topleft.X) {
            var tmp = topleft.X;
            topleft = new Vector2f(bottomright.X, topleft.Y);
            bottomright = new Vector2f(tmp, bottomright.X);
        }

        return z.X >= topleft.X && z.X <= bottomright.X && z.Y <= topleft.Y && z.Y >= bottomright.Y;
    }


    public static Vector2f ToCenteredPosition(this Vector2f coords, Shape shape) {
        var bounds = shape.GetGlobalBounds();

        return new Vector2f(coords.X - bounds.Width / 2, coords.Y - bounds.Height / 2);
    }
}