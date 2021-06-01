using System.Collections.Generic;
using System.Windows.Media;

namespace WeInvest.Models {
    public class Investor {

        public string Name { get; set; }
        public Brush Color { get; set; }
        public List<float> ShareHistory { get; protected set; }
        public float Share { get => ShareHistory == null ? -1 : ShareHistory[ShareHistory.Count - 1]; }

        public Investor(string name, Brush color) {
            this.Name = name;
            this.Color = color;
            this.ShareHistory = new List<float>();
            ShareHistory.Add(0);
        }

        public void Deposit(float amount) {
            if(ShareHistory.Count == 1 && Share == 0) {
                ShareHistory[0] = amount;
                return;
            }

            ShareHistory.Add(Share + amount);
        }

        public override string ToString() {
            string output = Name + " (";

            for(int i = 0; i < ShareHistory.Count; i++) {
                var share = ShareHistory[i];
                output += share + (i == ShareHistory.Count - 1 ? ")" : ", ");
            }

            return output;
        }


    }
}
