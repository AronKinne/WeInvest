using System;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;

namespace WeInvest.WPF.ViewModels.Factories {
    public class StockViewModelFactory : IFactory<StockViewModel> {

        public StockViewModel Create() {
            throw new NotImplementedException();
        }

        public StockViewModel Create(object parameter) {
            var stock = parameter as Stock;

            return new StockViewModel(stock);
        }

    }
}
