using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLotteryApp
{
    // Observer class
    public class LotterySubscriber : IObserver<PlayerMonitor>
    {
        private string _nameRegister;
        private string _message;
        
        private IDisposable cancellation;

        public string Name
        {
            get { return _nameRegister; }
            set { _nameRegister = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        // set name for observer to subscribe to the simple Lottery
        public LotterySubscriber(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("The player must be assigned a name.");
            this._nameRegister = name;
        }

        // subscribe the observer to the lottery game
        public virtual void Subscribe(LotteryProvider provider)
        {
            cancellation = provider.Subscribe(this);
            Console.WriteLine("Welcome...{0}", Name);
        }
        // unsubscribe the observer from the lottery game
        public virtual void Unsubscribe()
        {
            cancellation.Dispose();
        }
        public virtual void OnCompleted()
        {
            Console.WriteLine("Completed");
        }
        public virtual void OnError(Exception e)
        {
            // No implimentaiton needed
            Console.WriteLine("Error");
        }
        // supplies the observer with new or current information
        public virtual void OnNext(PlayerMonitor info)
        {

            Console.WriteLine("[ {0}'s Device ]: {1} {2}", Name, info.Player, info.Message);

        }


    }
}
