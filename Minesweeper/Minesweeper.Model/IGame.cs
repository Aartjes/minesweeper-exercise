namespace Com.Github.Aartjes.Minesweeper.Model
{
    public interface IGame
    {
        IGameState State { get; }
        int FieldWidth { get; }
        int FieldHeight { get; }
        int MineCount { get; }

        GameStatus ExecuteCommand(IGameCommand command);
    }
}