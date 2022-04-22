public class Union<T, U> {
    readonly T? t;
    readonly U? u;
    readonly int tag;

    public Union(T item) { t = item; tag = 0; }
    public Union(U item) { u = item; tag = 1; }

    public TResult Match<TResult>(Func<T, TResult> f, Func<U, TResult> g)
    {
        switch (tag)
        {
            case 0: return f(t!);
            case 1: return g(u!);
            default: throw new Exception("Unrecognized tag value: " + tag);
        }
    }

    public void Match(Action<T> f, Action<U> g)
    {
        switch (tag)
        {
            case 0: f(t!); break;
            case 1: g(u!); break;
            default: throw new Exception("Unrecognized tag value: " + tag);
        }
    }

    public static implicit operator Union<T, U>(T t) => new(t);
    public static implicit operator Union<T, U>(U u) => new(u);

    public static explicit operator T(Union<T, U> union) => union.t!;
    public static explicit operator U(Union<T, U> union) => union.u!;



    public override string ToString() => Match(t => t!.ToString(), u => u!.ToString())!;
}