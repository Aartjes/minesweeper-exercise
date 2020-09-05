namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public interface ICommandInterpreter
    {
        void Interpret(string command, IProgram program);
    }
}