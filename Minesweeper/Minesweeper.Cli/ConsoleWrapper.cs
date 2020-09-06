using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    class ConsoleWrapper : IConsole
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
