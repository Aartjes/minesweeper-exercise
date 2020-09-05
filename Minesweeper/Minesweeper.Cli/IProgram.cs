using Com.Github.Aartjes.Minesweeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public interface IProgram
    {
        IGame Game { get; }

        void Exit();
        void Lose();
        void Win();
        void NewGame();
    }
}
