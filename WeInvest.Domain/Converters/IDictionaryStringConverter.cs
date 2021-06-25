using System.Collections.Generic;

namespace WeInvest.Domain.Converters {
    public interface IDictionaryStringConverter {

        char Separator { get; set; }

        string DictionaryToString<K, V>(IDictionary<K, V> dictionary);
        IDictionary<K, V> StringToDictionary<K, V>(string value);
    
    }
}
