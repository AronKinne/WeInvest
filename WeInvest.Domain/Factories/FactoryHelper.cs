namespace WeInvest.Domain.Factories {
    public static class FactoryHelper {
    
        public static T GetProperty<T>(this object parameter, string propertyName) {
            return (T)parameter.GetType().GetProperty(propertyName).GetValue(parameter);
        }
    
    }
}
