using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class FlagCommand : IGameCommand
    {
        private readonly int _x;
        private readonly int _y;

        public FlagCommand(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Execute(IGameState gameState)
        {
            gameState.ToggleFlag(_x, _y);
        }
    }
}
