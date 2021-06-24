using System;
using System.Collections.Generic;

namespace WeInvest.Domain.Converters {
    public interface IListStringConverter {

        string ListToString<T>(IList<T> list) where T : IConvertible;
        IList<T> StringToList<T>(string value) where T : IConvertible;

    }
}
