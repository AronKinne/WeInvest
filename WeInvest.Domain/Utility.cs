using System;

namespace WeInvest.Domain {
    public static class Utility {

        public static T GetProperty<T>(this object obj, string propertyName) where T : IConvertible {
            var value = obj?.GetType().GetProperty(propertyName)?.GetValue(obj, null);

            if(value == null)
                return default(T);

            return (T)Convert.ChangeType(value, typeof(T));
        }
        public static T ForceGetProperty<T>(this object obj, string propertyName) {
            return (T)(obj?.GetType().GetProperty(propertyName)?.GetValue(obj, null));
        }

        public static bool HasProperty(this object obj, string propertyName) {
            var property = obj?.GetType().GetProperty(propertyName);

            return property != null;
        }

    }
}
