using Com.Github.Aartjes.Minesweeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public interface IGameFactory
    {
        IGame Create();
    }
}
