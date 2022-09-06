using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLotteryApp
{
    // Class for display message
    public class PlayerMonitor
    {
            private string _player;
            private string _message;

            public PlayerMonitor(string playerName, string message)
            {
                _player = playerName;
                _message = message;

            }
            public string Player
            {
                get { return this._player; }
            }
            public string Message
            {
                get { return this._message; }
            }
    }
}
