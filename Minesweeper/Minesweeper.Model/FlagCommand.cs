using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class FlagCommand : IGameCommand
    {
        public int X { get; }
        public int Y { get; }

        public FlagCommand(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Execute(IGameState gameState)
        {
            gameState.ToggleFlag(X, Y);
        }
    }
}
