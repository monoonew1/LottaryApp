using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLotteryApp
{
    // internal class for unsubscribe from the Lottery
    internal class Unsubscriber<PlayerMoniter> : IDisposable
    {
        private List<IObserver<PlayerMoniter>> _observers;
        private IObserver<PlayerMoniter> _observer;

        internal Unsubscriber(List<IObserver<PlayerMoniter>> observers, IObserver<PlayerMoniter> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }
        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
