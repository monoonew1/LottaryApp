using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLotteryApp
{
    // Player information class
    public class PlayerInfo
    {
        public string _player;
        public int[] _ticket;
        public int _budget;


        public PlayerInfo(string playerName, int[] ticket, int budget )
        {
            _player = playerName;
            _ticket = ticket;
            _budget = budget;
        }
        public PlayerInfo()
        {
                
        }

        public string Player { get; set; }
        public int[] Ticket { get; set; }
        public int Budget { get; set; }

    }
    
}
