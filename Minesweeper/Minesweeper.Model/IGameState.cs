using System.Collections.Generic;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public interface IGameState : IEnumerable<GameSpaceState>
    {
        GameSpaceState this[int x, int y] { get; }

        int FieldHeight { get; }
        int FieldWidth { get; }
        int MineCount { get; }

        void Step(int x, int y);
        void ToggleFlag(int x, int y);
    }
}