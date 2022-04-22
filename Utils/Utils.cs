using System.Diagnostics;

public static class Utils
{
    public static int H, W;

    public static Random globalRnd = new Random();


    [DebuggerStepThrough]
    public static int AbsMod(int a, int b) => (a % b + b) % b;

    [DebuggerStepThrough]
    public static int idX(int x) => AbsMod(x, W);
    [DebuggerStepThrough]
    public static int idY(int y) => AbsMod(y, H);
    [DebuggerStepThrough]
    public static int idx(int i, int j) {
        return idX(i) + idY(j) * W;
    }
}