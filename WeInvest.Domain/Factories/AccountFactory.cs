using WeInvest.Domain.Converters;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;

namespace WeInvest.Domain.Factories {
    public class AccountFactory : IFactory<Account> {

        private readonly IDictionaryStringConverter _dictionaryStringConverter;
        private readonly IDataService<Investor> _investorService;

        public AccountFactory(IDictionaryStringConverter dictionaryStringConverter, IDataService<Investor> investorService) {
            _dictionaryStringConverter = dictionaryStringConverter;
            _investorService = investorService;
        }

        public Account Create() {
            return new Account(_dictionaryStringConverter, _investorService);
        }

        public Account Create(object parameter) {
            var account = Create();

            if(parameter.HasProperty(nameof(Account.Id)))
                account.Id = parameter.GetProperty<int>(nameof(Account.Id));

            if(parameter.HasProperty(nameof(Account.ShareByInvestorString)))
                account.ShareByInvestorString = parameter.ForceGetProperty<string>(nameof(Account.ShareByInvestorString));

            return account;
        }
    }
}
