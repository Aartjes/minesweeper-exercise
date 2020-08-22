namespace Com.Github.Aartjes.Minesweeper.Model
{
    public interface IGameState
    {
        GameSpaceState this[int x, int y] { get; }

        int FieldHeight { get; }
        int FieldWidth { get; }

        void Step(int x, int y);
        void ToggleFlag(int x, int y);
    }
}