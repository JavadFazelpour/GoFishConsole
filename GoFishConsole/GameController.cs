using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFishConsole
{
    public class GameController
    {
        public static Random Random = new Random();

        private GameState gameState;
        public bool GameOver { get => gameState.GameOver; }
        public Player HumanPlayer { get => gameState.HumanPlayer; }
        public IEnumerable<Player> Opponents { get => gameState.Opponents; }
        public string Status { get; private set; }

        public GameController(string humanPlayerName,IEnumerable<string> computerPlayerNames)
        {
            throw new NotImplementedException();
        }

        public void NextRound(Player playerToAsk,Values valueToAskFor)
        {
            throw new NotImplementedException();
        }
        private void ComputerPlayersPlayNextRound()
        {
            throw new NotImplementedException();
        }
        public void NewGame()
        {
            throw new NotImplementedException();
        }
    }
}
