using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class Game : IGame
    {
        private IGameState _state;

        public Game(IGameState state)
        {
            _state = state;
        }

        public GameStatus ExecuteCommand(IGameCommand command)
        {
            command.Execute(_state);
            if (_state.Any(spaceState => spaceState == GameSpaceState.Mine))
            {
                return GameStatus.Loss;
            }
            else if (_state.Count(spaceState =>
                     spaceState == GameSpaceState.Blank
                     || spaceState == GameSpaceState.Flag)
                == _state.MineCount)
            {
                return GameStatus.Win;
            }
            else return GameStatus.Ongoing;
        }
    }
}
