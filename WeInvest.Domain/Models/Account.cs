using System.Collections.Generic;
using System.Linq;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Services;

namespace WeInvest.Domain.Models {
    public class Account {

        private readonly IDictionaryStringConverter _dictionaryStringConverter;
        private readonly IDataAccess<Investor> _investorService;

        public int Id { get; set; }
        public string ShareByInvestorString {
            get {
                var shareById = new Dictionary<int, float>();
                foreach(var entry in ShareByInvestor)
                    shareById.Add(entry.Key.Id, entry.Value);

                return _dictionaryStringConverter.DictionaryToString(shareById);
            }
            set {
                ShareByInvestor = new Dictionary<Investor, float>();

                var shareById = _dictionaryStringConverter.StringToDictionary<int, float>(value);
                foreach(var entry in shareById) {
                    var investor = _investorService.GetAsync(entry.Key).Result;
                    ShareByInvestor.Add(investor, entry.Value);
                }
            }
        }

        public IDictionary<Investor, float> ShareByInvestor { get; set; } = new Dictionary<Investor, float>();
        public float Balance {
            get {
                if(ShareByInvestor == null)
                    return 0;

                float sum = 0;
                foreach(KeyValuePair<Investor, float> entry in ShareByInvestor) 
                    sum += entry.Value;
                return sum;
            }
        }

        public Account(IDictionaryStringConverter dictionaryStringConverter, IDataAccess<Investor> investorService) {
            _dictionaryStringConverter = dictionaryStringConverter;
            _investorService = investorService;
        }

        public void AddOwners(IEnumerable<Investor> investors) {
            this.ShareByInvestor = new Dictionary<Investor, float>();
            foreach(Investor investor in investors) {
                AddOwner(investor, investor.Share);
            }
        }

        public void AddOwner(Investor investor, float balance) {
            if(ShareByInvestor.ContainsKey(investor))
                return;

            ShareByInvestor.Add(investor, balance);
        }

        public IList<KeyValuePair<Investor, float>> ToList() {
            return ShareByInvestor.ToList();
        }

    }
}
