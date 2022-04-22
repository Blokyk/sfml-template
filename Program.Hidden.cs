public partial class Program {
    public static VideoMode props;
    public static DisplayMode mode;

    public static RenderTarget render;

    /// <summary>
    /// maxFPS = -1 <=> VSync
    /// </summary>
    public static int maxFPS = -1;
    public static bool clearFrame = true;

    public static Time deltaTime;

    public static Clock frameClock;
    public static Clock globalClock;

    public static void Main(string[] args) {
        globalClock.Restart();
        Setup();

        Utils.W = (int)props.Width;
        Utils.H = (int)props.Height;

        void coreLoop() {
            if (clearFrame) {
                render.Clear();
            }

            frameClock.Restart();

            Loop(render, deltaTime);

            deltaTime = frameClock.ElapsedTime;
        }

        if (mode is DisplayMode.Texture texMode) {
            var tex = new RenderTexture(props.Width, props.Height);
            var render = tex;

            var frameCounter = 0;

            while (frameCounter < texMode.frameCount*texMode.saveEvery) {
                coreLoop();

                tex.Display();

                if (frameCounter % texMode.saveEvery == 0) {
                    using (var img = tex.Texture.CopyToImage()) {
                        img.SaveToFile($"./out/{texMode.filePrefix}{frameCounter, 5}.png");
                    }
                }

                frameCounter++;
            }
        } else {
            var wnd = new RenderWindow(props, "SFMLTemplate");

            if (maxFPS <= 0) {
                wnd.SetVerticalSyncEnabled(true);
            }
            else {
                wnd.SetFramerateLimit((uint)maxFPS);
            }

            wnd.Closed += (_, _) => wnd.Close();

            render = wnd;

            while (wnd.IsOpen) {
                wnd.DispatchEvents();

                coreLoop();

                wnd.Display();
            }
        }
    }

    static Program() {
        render = new RenderTexture(1, 1);
        mode = new DisplayMode.Texture(0);
        frameClock = new Clock();
        globalClock = new Clock();
    }

    public abstract record DisplayMode()
    {

        public record Window() : DisplayMode { }

        public record Texture(int frameCount, string filePrefix = "", int saveEvery = 1) : DisplayMode { }
    }
}