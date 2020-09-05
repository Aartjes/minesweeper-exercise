using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public interface ICommunicator
    {
        string AskForCommand();
        void DisplayState(string state);
        void CommunicateWin();
        void CommunicateLoss();
        string AskForNewGame();
    }
}
