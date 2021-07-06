using System;

namespace WeInvest.WPF.State {
    public interface IStore {

        event EventHandler StateChanged;

    }
}
