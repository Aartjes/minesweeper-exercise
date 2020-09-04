namespace Com.Github.Aartjes.Minesweeper.Model
{
    public interface IGame
    {
        IGameState State { get; }

        GameStatus ExecuteCommand(IGameCommand command);
    }
}