namespace Com.Github.Aartjes.Minesweeper.Model
{
    public interface IGame
    {
        GameStatus ExecuteCommand(IGameCommand command);
    }
}