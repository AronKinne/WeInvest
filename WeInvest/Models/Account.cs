using System;
using System.Collections.Generic;
using System.Linq;

namespace WeInvest.Models {
    public class Account {

        public Dictionary<Investor, float> ShareByInvestor { get; set; }
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

        public Account() : this(new List<Investor>()) { }

        public Account(List<Investor> investors) {
            if(investors == null)
                throw new ArgumentNullException(nameof(investors));

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

        public List<KeyValuePair<Investor, float>> ToList() {
            return ShareByInvestor.ToList();
        }

    }
}
