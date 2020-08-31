using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class StepCommand : IGameCommand
    {
        public int X { get; }
        public int Y { get; }

        public StepCommand(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Execute(IGameState gameState)
        {
            Step(X, Y, gameState);
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
