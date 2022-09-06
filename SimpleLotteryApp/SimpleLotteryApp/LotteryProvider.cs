using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLotteryApp
{
    //Provider or Subject
    public class LotteryProvider : IObservable<PlayerMonitor>
    {
        // create a <IObservable> observer list and a list to store player information
        private List<IObserver<PlayerMonitor>> observers;
        private List<PlayerInfo> playerInfos;
        public LotteryProvider()
        {
            observers = new List<IObserver<PlayerMonitor>>();
            playerInfos = new List<PlayerInfo>();
        }

        // Allows observers to subscribe to the LotterySystem
        public IDisposable Subscribe(IObserver<PlayerMonitor> observer)
        {
            
            // Check whether observer is already registered. If not, add it.
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<PlayerMonitor>(observers, observer);
        }
        // Fetch the players information(name, tickets, budget) and store in the list
        public virtual void SetupAccount(IObserver<PlayerMonitor> observer, string name, int[] ticket, int budget)
        {
            if (!observers.Contains(observer))
            {
                Console.WriteLine("Error. Please subscribe to Simple Lottery first.");
            }
            else
            {
                var info = new PlayerInfo(name, ticket, budget);
                playerInfos.Add(info);
                // Update a new member
                string comment = "joined Simply Lottery.";
                var message = new PlayerMonitor(name, comment);
                // Update
                foreach (var o in observers)
                {
                    o.OnNext(message);
                }
            }

        }
        // Lottery Game runs until it gets a winner
        public virtual void LotteryGame()
        {
            // a variable for checking if there is a winner
            bool isWinnerExist = false;

            // Iterate until find a winner
            while ((!isWinnerExist) && (playerInfos.Count != 0))
            {
                foreach (var member in playerInfos.ToArray())
                {
                    if (member._budget > 0)
                    {
                        // get the current budget, reduce by the ammount of the tickets and store the result.
                        int newBudget = member._budget - member._ticket.Count();
                        var modifyMember = new PlayerInfo(member._player, member._ticket, newBudget);
                        playerInfos.Add(modifyMember);
                        playerInfos.Remove(member);
                    }
                    else if (member._budget == 0)
                    {
                        // Update
                        // The message for the player for who has run out of money.
                        string messageOut = "has run out of money, so they can no longer play lottery.";
                        var messagePlayerOut = new PlayerMonitor(member._player, messageOut);
                        // Update
                        foreach (var o in observers)
                        {
                            o.OnNext(messagePlayerOut);
                        }
                        // Remove the player from the list
                        playerInfos.Remove(member);
                    }
                }

                Console.WriteLine("\n**************** Weekly Winner Draw ****************\n");
                // Create the weekly winning number and announce it to all the players 
                Random rnd = new Random();
                int winningNum = rnd.Next(1, 49);
                string messageWinNum = "Announcing this week's winning ticket.....the number is";
                string winningNumber = winningNum.ToString();
                var messagaeWinningNum = new PlayerMonitor(messageWinNum, winningNumber);
                // Update
                foreach (var o in observers)
                {
                    o.OnNext(messagaeWinningNum);
                }

                // check the weekly Lottery winner and announce it to all the players 
                foreach (var m in playerInfos.ToArray())
                {
                    foreach(var ticket in m._ticket)
                    {
                        if (ticket == winningNum)
                        {
                            Console.WriteLine("\n****************************************************");
                            Console.WriteLine("********* The Winner of the Simple Lottery *********");
                            Console.WriteLine("****************************************************\n");
                            Console.WriteLine("Congratulations {0}!!\n", m._player);

                            string commentWin = "won the Simple Lottery. Congratulations!";
                            var commentWinner = new PlayerMonitor(m._player, commentWin);
                            // Update
                            foreach (var o in observers)
                            {
                                o.OnNext(commentWinner);
                            }
                            isWinnerExist = true;
                        }
                    }
                    
                }
                Console.WriteLine();
                // Announce the result if there is no winner this week.
                if (!isWinnerExist)
                {
                    string commentNoWinner = "No winner this week. See you next week!";
                    var commentWinner = new PlayerMonitor(null, commentNoWinner);
                    // Update
                    foreach (var observer in observers)
                    {
                        observer.OnNext(commentWinner);
                    }

                }
                Console.WriteLine();
            // When all the players have run out of budget, announce it to them.
            // Then this method break.
            }
            if (playerInfos.Count == 0)
            {
                string name = "No players ";
                string comment = "left. Thank you for joining the Simple Lottery.";
                var message = new PlayerMonitor(name, comment);
                // Update
                foreach (var observer in observers)
                {
                    observer.OnNext(message);
                }
            }

        }
        // Method for provider to end the program
        public virtual void EndGame()
        {
            foreach(var observer in observers.ToArray())
            {
                if(observers.Contains(observer))
                    observer.OnCompleted();
                observers.Clear();
            }
        }

    }

}
