using Com.Github.Aartjes.Minesweeper.Model;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public interface IGameStatePrinter
    {
        string Print(IGameState state);
    }
}