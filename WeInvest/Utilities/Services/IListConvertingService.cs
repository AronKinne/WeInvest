using System;
using System.Collections.Generic;

namespace WeInvest.Utilities.Services {
    public interface IListConvertingService {

        string ListToString<T>(IList<T> list) where T : IConvertible;
        IList<T> StringToList<T>(string value) where T : IConvertible;

    }
}
