using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public interface IConsole
    {
        string ReadLine();
        void WriteLine(string v);
    }
}
