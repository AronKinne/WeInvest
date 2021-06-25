using System;
using System.Collections.Generic;

namespace WeInvest.Domain.Converters {
    public class DictionaryStringConverter : IDictionaryStringConverter {

        private readonly IListStringConverter _listStringConverter;

        public char Separator { get; set; } = '|';

        public DictionaryStringConverter(IListStringConverter listStringConverter) {
            _listStringConverter = listStringConverter;
        }

        public string DictionaryToString<K, V>(IDictionary<K, V> dictionary) {
            IList<string> entryStrings = new List<string>();
            
            foreach(KeyValuePair<K, V> entry in dictionary) {
                entryStrings.Add(entry.Key.ToString() + Separator + entry.Value.ToString());
            }

            return _listStringConverter.ListToString(entryStrings);
        }

        public IDictionary<K, V> StringToDictionary<K, V>(string dictionaryString) {
            IDictionary<K, V> dictionary = new Dictionary<K, V>();

            IList<string> entryStrings = _listStringConverter.StringToList<string>(dictionaryString);
            foreach(var entryString in entryStrings) {
                int separatorIndex = entryString.IndexOf(Separator);

                string keyString = entryString.Substring(0, separatorIndex);
                K key = (K)Convert.ChangeType(keyString, typeof(K));

                string valueString = entryString.Substring(separatorIndex + 1, entryString.Length - separatorIndex - 1);
                V value = (V)Convert.ChangeType(valueString, typeof(V));

                dictionary.Add(key, value);
            }

            return dictionary;
        }

    }
}
