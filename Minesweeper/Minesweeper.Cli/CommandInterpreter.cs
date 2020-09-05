using Com.Github.Aartjes.Minesweeper.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public CommandInterpreter()
        {
        }

        public void Interpret(string command, IProgram program)
        {
            var parts = ProcessCommand(command);
            if (string.Equals("step", parts[0], StringComparison.CurrentCultureIgnoreCase))
            {
                CreateAndExecuteGameCommand((x, y) => new StepCommand(x, y), parts[1], parts[2], program);
            }
            else if (string.Equals("flag", parts[0], StringComparison.CurrentCultureIgnoreCase))
            {
                CreateAndExecuteGameCommand((x, y) => new FlagCommand(x, y), parts[1], parts[2], program);
            }
            else if (string.Equals("exit", parts[0], StringComparison.CurrentCultureIgnoreCase))
            {
                program.Exit();
            }
        }

        private string[] ProcessCommand(string command)
        {
            return command.Split(" \t,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        private void CreateAndExecuteGameCommand(Func<int, int, IGameCommand> createCommand, string xString, string yString, IProgram program)
        {
            var command = createCommand(
                ToCoordinate(xString),
                ToCoordinate(yString));
            var state = program.Game.ExecuteCommand(command);
            switch (state)
            {
                case GameStatus.Loss:
                    program.Lose();
                    break;
                case GameStatus.Win:
                    program.Win();
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

        public void InterpretNewGameYesNo(string command, IProgram program)
        {
            if (string.Equals(ProcessCommand(command)[0], "Yes", StringComparison.OrdinalIgnoreCase))
            {
                program.NewGame();
            }
            else
            {
                program.Exit();
            }
        }
    }
}
