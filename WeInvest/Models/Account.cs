using System.Collections.Generic;
using System.Linq;

namespace WeInvest.Models {
    public class Account {

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

        public void AddInvestors(IEnumerable<Investor> investors) {
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
