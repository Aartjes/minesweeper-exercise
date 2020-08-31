using Com.Github.Aartjes.Minesweeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public interface ISpaceStateToStringConvertor
    {
        string Convert(GameSpaceState input);
    }
}
