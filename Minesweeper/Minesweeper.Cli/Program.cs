using Com.Github.Aartjes.Minesweeper.Model;
using System;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public class Program : IProgram
    {
        private ICommunicator _communicator;
        private readonly IGameFactory _gameFactory;
        private readonly IGameStatePrinter _gameStatePrinter;
        private bool _exited;
        ICommandInterpreter _interpreter;

        public Program(ICommunicator communicator, IGameFactory gameFactory, IGameStatePrinter gameStatePrinter, ICommandInterpreter interpreter)
        {
            _communicator = communicator;
            _gameFactory = gameFactory;
            _gameStatePrinter = gameStatePrinter;
            _interpreter = interpreter;
            Game = _gameFactory.Create();
        }

        public IGame Game { get; }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public void Exit()
        {
            _exited = true;
        }

        public void Lose()
        {
            throw new NotImplementedException();
        }

        public void Win()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            while (!_exited)
            {
                _communicator.DisplayState(_gameStatePrinter.Print(Game.State));
                _interpreter.Interpret(_communicator.AskForCommand(), this);
            }
        }
    }
}
