public static class TextUtils
{
    public static readonly Font UbuntuMono = new Font("./UbuntuMono-Regular.ttf");

    public static Text ToText(string text, Vector2f pos, Color color, float scale = 1)
        =>  new Text(text, UbuntuMono) {
                Position = pos,
                FillColor = color,
                OutlineColor = color,
                Scale = new Vector2f(scale, scale)
            };

    public static Text ToText(string text, Vector2f pos, float scale = 1)
        => ToText(text, pos, Color.White, scale);

    public static Text ToCenteredText(string text, Vector2f pos, Color color, float scale = 1) {
        var baseText = ToText(text, pos, color, scale);

        var bounds = baseText.GetGlobalBounds();

        baseText.Position -= new Vector2f(bounds.Width, bounds.Height) / 2;

        return baseText;
    }

    public static Text ToCenteredText(string text, Vector2f pos, float scale = 1)
        => ToCenteredText(text, pos, Color.White, scale);
}
