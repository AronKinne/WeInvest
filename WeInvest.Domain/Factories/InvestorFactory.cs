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

            investor.Id = parameter.GetProperty<int>(nameof(Investor.Id));
            investor.Name = parameter.GetProperty<string>(nameof(Investor.Name));

            var colorHex = parameter.GetProperty<string>(nameof(Investor.ColorHex));
            if(parameter.HasProperty(nameof(Investor.Brush)))
                colorHex = _brushConvertingService.BrushToString(parameter.ForceGetProperty<Brush>(nameof(Investor.Brush)));
            investor.ColorHex = colorHex;

            if(parameter.HasProperty(nameof(Investor.ShareHistoryString)))
                investor.ShareHistoryString = parameter.ForceGetProperty<string>(nameof(Investor.ShareHistoryString));

            return investor;
        }
    }
}
