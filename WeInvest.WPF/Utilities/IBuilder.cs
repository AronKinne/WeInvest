namespace WeInvest.WPF.Utilities {
    public interface IBuilder<T> {

        IBuilder<T> Build();
        T Get();

    }
}
