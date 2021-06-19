using System.Windows.Media;
using WeInvest.Models;
using WeInvest.Utilities.Services;

namespace WeInvest.Utilities.Factories {
    public class InvestorFactory : IFactory<Investor> {

        private readonly IListConvertingService _listConvertingService;
        private readonly IBrushConvertingService _brushConvertingService;

        public InvestorFactory(IListConvertingService listConvertingService, IBrushConvertingService brushConvertingService) {
            _listConvertingService = listConvertingService;
            _brushConvertingService = brushConvertingService;
        }

        public Investor Create() {
            return new Investor(_listConvertingService, _brushConvertingService);
        }

        public Investor Create(string name, Brush brush) {
            Investor investor = Create();
            investor.Name = name;
            investor.Brush = brush;
            return investor;
        }

    }
}
