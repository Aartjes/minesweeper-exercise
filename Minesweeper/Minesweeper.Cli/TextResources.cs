using Com.Github.Aartjes.Minesweeper.Cli.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public class TextResources : ITextResources
    {
        public string CommandQuery => Resources.CommandQuery;

        public string NewGameQuery => Resources.NewGameQuery;

        public string LossMessage => Resources.LossMessage;

        public string VictoryMessage => VictoryMessage;
    }
}
