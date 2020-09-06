using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public class Communicator : ICommunicator
    {
        private ITextResources _textResources;
        private IConsole _console;


        public Communicator(ITextResources textResources, IConsole console)
        {
            _textResources = textResources;
            _console = console;
        }

        public string AskForCommand()
        {
            _console.WriteLine(_textResources.CommandQuery);
            return _console.ReadLine();
        }

        public string AskForNewGame()
        {
            _console.WriteLine(_textResources.NewGameQuery);
            return _console.ReadLine();
        }

        public void CommunicateLoss()
        {
            _console.WriteLine(_textResources.LossMessage);
        }

        public void CommunicateWin()
        {
            _console.WriteLine(_textResources.VictoryMessage);
        }

        public void DisplayState(string state)
        {
            _console.WriteLine(state);
        }
    }
}
