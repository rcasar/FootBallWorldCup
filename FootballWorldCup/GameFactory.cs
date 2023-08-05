using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballWorldCup
{
    public class GameFactory : IGameFactory
    {
        public IGame CreateGame(string homeTeam, string awayTeam)
        {
            return new Game(homeTeam, awayTeam);
        }
    }
}
