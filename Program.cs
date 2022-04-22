public partial class Program
{
    public static void Setup() {

        /* Available values
        *   - maxFPS        (vsync)
        *   - clearWnd      (true)
        */

        props = new VideoMode(800, 800);
        mode = new DisplayMode.Window();  //mode = new DisplayMode.Texture(100, "frame-", 1);
    }

    public static void Loop(RenderTarget target, Time deltaTime) {

        /* Available values
        *   - Props
        *   - frameClock
        *   - globalClock
        */

        var time = globalClock.ElapsedTime.AsSeconds();

        target.Draw(TextUtils.ToCenteredText("Hello world!", target.GetView().Center + 100 * new Vector2f(MathF.Sin(time), MathF.Cos(time))));
    }
}