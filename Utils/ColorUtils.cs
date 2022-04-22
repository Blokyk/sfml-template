
public static class ColorUtils
{
    delegate byte ComponentSelector(Color color);
    static ComponentSelector _redSelector = color => color.R;
    static ComponentSelector _greenSelector = color => color.G;
    static ComponentSelector _blueSelector = color => color.B;

    public static Color Grayscale(byte val) => new(val, val, val);

    public static Color ColorFromHSL(double h, double s, double l)
    {
        double r = 0, g = 0, b = 0;

        if (l > 1) l = 1;
        else if (l < 0) l = 0;
        if (s > 1) s = 1;
        else if (s < 0) s = 0;

        if (l != 0)
        {
            if (s == 0)
                r = g = b = l;
            else
            {
                double temp2;
                if (l < 0.5)
                    temp2 = l * (1.0 + s);
                else
                    temp2 = l + s - (l * s);

                double temp1 = 2.0 * l - temp2;

                r = GetColorComponent(temp1, temp2, h + 1.0 / 3.0);
                g = GetColorComponent(temp1, temp2, h);
                b = GetColorComponent(temp1, temp2, h - 1.0 / 3.0);
            }
        }
        return new Color((byte)(255 * r), (byte)(255 * g), (byte)(255 * b));

    }

    private static double GetColorComponent(double temp1, double temp2, double temp3)
    {
        if (temp3 < 0.0)
            temp3 += 1.0;
        else if (temp3 > 1.0)
            temp3 -= 1.0;

        if (temp3 < 1.0 / 6.0)
            return temp1 + (temp2 - temp1) * 6.0 * temp3;
        else if (temp3 < 0.5)
            return temp2;
        else if (temp3 < 2.0 / 3.0)
            return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);
        else
            return temp1;
    }

    public static Color InterpolateBetween(
        Color endPoint1,
        Color endPoint2,
        double lambda)
    {
        if (lambda <= 0 || lambda > 1)
        {
            lambda = 1;
        }
        Color color = new Color(
            (byte)(endPoint1.R + (endPoint2.R - endPoint1.R) * lambda),
            (byte)(endPoint1.G + (endPoint2.G - endPoint1.G) * lambda),
            (byte)(endPoint1.B + (endPoint2.B - endPoint1.B) * lambda)
        );

        return color;
    }

    static byte InterpolateComponent(
        Color endPoint1,
        Color endPoint2,
        double lambda,
        ComponentSelector selector)
    {
        return (byte)(selector(endPoint1)
            + (selector(endPoint2) - selector(endPoint1)) * lambda);
    }
}