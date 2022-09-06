using System;
using System.Collections.Generic;

namespace SimpleLotteryApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("***************************************************");
            Console.WriteLine("********** Welcome to the Simple Lottery **********");
            Console.WriteLine("***************************************************\n");

            // create observable object that will call out 
            LotteryProvider provider = new LotteryProvider();

            Console.WriteLine("******************** Players **********************\n");
            // create observer (Player) object and subscribe to Simply Lottery
            LotterySubscriber observer1 = new LotterySubscriber("Tama");
            observer1.Subscribe(provider);
            // Each player setup thier own account with name, ticket numbers and budget(NZD)
            int[] ticket1 = { 3, 33, 17, 46, 27 };
            provider.SetupAccount(observer1, observer1.Name, ticket1, 50);

            LotterySubscriber observer2 = new LotterySubscriber("Bob");
            observer2.Subscribe(provider);
            int[] ticket2 = { 7, 19, 22, 23 };
            provider.SetupAccount(observer2, observer2.Name, ticket2, 8);

            LotterySubscriber observer3 = new LotterySubscriber("Masami");
            observer3.Subscribe(provider);
            int[] ticket3 = { 33, 13 };
            provider.SetupAccount(observer3, observer3.Name, ticket3, 35);

            // Provider provides lottery game
            provider.LotteryGame();

            // observer(Player) unsubscribe from Simple Lottery
            observer1.Unsubscribe();
            observer2.Unsubscribe();
            observer3.Unsubscribe();


            //////////////////////////////////////////// Comment ///////////////////////////////////////////////
            /// I have used observer pattern for my software design,
            /// which enables to multiple players (observers) to subscribe to the Simple Lottery and they can also unsubscribe from it.
            /// When one object changes state, all players are automatically notified and updated.



        }
    }
}
