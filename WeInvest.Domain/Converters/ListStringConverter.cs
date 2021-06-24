using System;
using System.Collections.Generic;

namespace WeInvest.Domain.Converters {
    public class ListStringConverter : IListStringConverter {

        public char Separator { get; set; } = ' ';

        public string ListToString<T>(IList<T> list) where T : IConvertible{
            if(list.Count == 0)
                return "";

            string output = list[0].ToString();

            for(int i = 1; i < list.Count; i++) {
                output += Separator.ToString() + list[i].ToString();
            }

            return output;
        }

        public IList<T> StringToList<T>(string value) where T : IConvertible {
            string[] tokens = value.Split(Separator);
            T[] items = new T[tokens.Length];

            for(int i = 0; i < tokens.Length; i++) {
                items[i] = (T)Convert.ChangeType(tokens[i], typeof(T));
            }

            return items;
        }

    }
}
