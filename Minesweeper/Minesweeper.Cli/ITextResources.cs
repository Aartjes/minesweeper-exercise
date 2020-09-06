using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public interface ITextResources
    {
        string CommandQuery { get; }
        string NewGameQuery { get; }
        string LossMessage { get; }
        string VictoryMessage { get; }
    }
}
