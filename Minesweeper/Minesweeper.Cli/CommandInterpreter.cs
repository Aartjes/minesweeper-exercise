using Com.Github.Aartjes.Minesweeper.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public class CommandInterpreter
    {
        private IProgram _program;

        public CommandInterpreter(IProgram @object)
        {
            _program = @object;
        }

        public void Interpret(string command)
        {
            var parts = command.Split(" \t,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (string.Equals("step",parts[0],StringComparison.CurrentCultureIgnoreCase))
            {
                _program.Game.ExecuteCommand(
                    new StepCommand(
                        int.Parse(parts[1], CultureInfo.CurrentCulture) -1,
                        int.Parse(parts[2], CultureInfo.CurrentCulture) -1));
            }
            else if (string.Equals("flag", parts[0], StringComparison.CurrentCultureIgnoreCase))
            {
                _program.Game.ExecuteCommand(
                    new FlagCommand(
                        int.Parse(parts[1], CultureInfo.CurrentCulture) -1,
                        int.Parse(parts[2], CultureInfo.CurrentCulture) -1));
            }
            else if (string.Equals("exit", parts[0], StringComparison.CurrentCultureIgnoreCase))
            {
                _program.Exit();
            }
        }
    }
}
