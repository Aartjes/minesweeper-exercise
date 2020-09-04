using Com.Github.Aartjes.Minesweeper.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private IProgram _program;

        public CommandInterpreter(IProgram @object)
        {
            _program = @object;
        }

        public void Interpret(string command)
        {
            var parts = command.Split(" \t,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (string.Equals("step", parts[0], StringComparison.CurrentCultureIgnoreCase))
            {
                CreateAndExecuteGameCommand((x, y) => new StepCommand(x, y), parts[1], parts[2]);
            }
            else if (string.Equals("flag", parts[0], StringComparison.CurrentCultureIgnoreCase))
            {
                CreateAndExecuteGameCommand((x, y) => new FlagCommand(x, y), parts[1], parts[2]);
            }
            else if (string.Equals("exit", parts[0], StringComparison.CurrentCultureIgnoreCase))
            {
                _program.Exit();
            }
        }

        private void CreateAndExecuteGameCommand(Func<int, int, IGameCommand> createCommand, string xString, string yString)
        {
            var command = createCommand(
                ToCoordinate(xString),
                ToCoordinate(yString));
            var state = _program.Game.ExecuteCommand(command);
            switch (state)
            {
                case GameStatus.Loss:
                    _program.Lose();
                    break;
                case GameStatus.Win:
                    _program.Win();
                    break;
                case GameStatus.Ongoing:
                default:
                    break;
            }
        }

        private int ToCoordinate(string coordinateString)
        {
            return int.Parse(coordinateString, CultureInfo.CurrentCulture) - 1;
        }
    }
}
