namespace WeInvest.Domain.Factories {
    public interface IFactory<T> {

        T Create();
        T Create(object parameter);

    }
}
