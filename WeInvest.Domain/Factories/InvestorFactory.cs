using System.Windows.Media;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Models;

namespace WeInvest.Domain.Factories {
    public class InvestorFactory : IFactory<Investor> {

        private readonly IListStringConverter _listConvertingService;
        private readonly IBrushStringConverter _brushConvertingService;

        public InvestorFactory(IListStringConverter listConvertingService, IBrushStringConverter brushConvertingService) {
            _listConvertingService = listConvertingService;
            _brushConvertingService = brushConvertingService;
        }

        public Investor Create() {
            return new Investor(_listConvertingService, _brushConvertingService);
        }

        public Investor Create(object parameter) {
            Investor investor = Create();
            investor.Name = parameter.GetProperty<string>("Name");
            investor.Brush = parameter.GetProperty<Brush>("Brush");
            return investor;
        }
    }
}
