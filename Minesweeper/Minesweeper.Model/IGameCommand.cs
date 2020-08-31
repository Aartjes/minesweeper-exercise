namespace Com.Github.Aartjes.Minesweeper.Model
{
    public interface IGameCommand
    {
        void Execute(IGameState state);
        int X { get; }
        int Y { get; }
    }
}