using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class StepCommand : IGameCommand
    {
        private int _x;
        private int _y;

        public StepCommand(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Execute(IGameState gameState)
        {
            Step(_x, _y, gameState);
        }

        private void Step(int x, int y, IGameState gameState)
        {
            if (CoordinatesAreValid(x, y, gameState) && gameState[x,y] == GameSpaceState.Blank)
            {
                gameState.Step(x, y);
                if (gameState[x, y] == GameSpaceState.Zero)
                {
                    Step(x, y - 1, gameState);
                    Step(x - 1, y - 1, gameState);
                    Step(x + 1, y - 1, gameState);
                    Step(x - 1, y, gameState);
                    Step(x + 1, y, gameState);
                    Step(x, y + 1, gameState);
                    Step(x - 1, y + 1, gameState);
                    Step(x + 1, y + 1, gameState);
                }
            }
        }

        private bool CoordinatesAreValid(int x, int y, IGameState gameState)
        {
            return x >= 0
                && y >= 0
                && x < gameState.FieldWidth
                && y < gameState.FieldHeight;
        }
    }
}
