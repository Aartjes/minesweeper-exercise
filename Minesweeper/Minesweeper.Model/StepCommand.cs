using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class StepCommand
    {
        private int _x;
        private int _y;
        private readonly IGameState _gameState;

        public StepCommand(int x, int y, IGameState gameState)
        {
            _x = x;
            _y = y;
            _gameState = gameState;
        }

        public void Execute()
        {
            Step(_x, _y);
        }

        private void Step(int x, int y)
        {
            if (CoordinatesAreValid(x, y) && _gameState[x,y] == GameSpaceState.Blank)
            {
                _gameState.Step(x, y);
                if (_gameState[x, y] == GameSpaceState.Zero)
                {
                    Step(x, y - 1);
                    Step(x - 1, y - 1);
                    Step(x + 1, y - 1);
                    Step(x - 1, y);
                    Step(x + 1, y);
                    Step(x, y + 1);
                    Step(x - 1, y + 1);
                    Step(x + 1, y + 1);
                }
            }
        }

        private bool CoordinatesAreValid(int x, int y)
        {
            return x >= 0
                && y >= 0
                && x < _gameState.FieldWidth
                && y < _gameState.FieldHeight;
        }
    }
}
