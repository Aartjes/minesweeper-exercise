using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class Game : IGame
    {
        public Game(IGameState state)
        {
            State = state;
        }

        public IGameState State { get; }

        public int FieldWidth => State.FieldWidth;

        public int FieldHeight => State.FieldHeight;

        public int MineCount => State.MineCount;

        public GameStatus ExecuteCommand(IGameCommand command)
        {
            command.Execute(State);
            if (State.Any(spaceState => spaceState == GameSpaceState.Mine))
            {
                return GameStatus.Loss;
            }
            else if (State.Count(spaceState =>
                     spaceState == GameSpaceState.Blank
                     || spaceState == GameSpaceState.Flag)
                == State.MineCount)
            {
                return GameStatus.Win;
            }
            else return GameStatus.Ongoing;
        }
    }
}
