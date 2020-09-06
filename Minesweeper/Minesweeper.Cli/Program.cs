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

        public IGame Game { get; private set; }

        static void Main(string[] args)
        {
            var program = new Program(
                new Communicator(
                    new TextResources(),
                    new ConsoleWrapper()),
                new GameFactory(
                    new FieldProbe(),
                    new SpaceStateConvertor()),
                new GameStatePrinter(
                    new SpaceStateToStringConvertor()),
                new CommandInterpreter());

            program.Execute();
        }

        public void Exit()
        {
            _exited = true;
        }

        public void Lose()
        {
            HandleGameEnd(_communicator.CommunicateLoss);
        }

        public void Win()
        {
            HandleGameEnd(_communicator.CommunicateWin);
        }

        private void HandleGameEnd(Action communicateGameEnd)
        {
            CommunicateState();
            communicateGameEnd();
            _interpreter.InterpretNewGameYesNo(_communicator.AskForNewGame(), this);
        }

        public void Execute()
        {
            while (!_exited)
            {
                CommunicateState();
                _interpreter.Interpret(_communicator.AskForCommand(), this);
            }
        }

        private void CommunicateState()
        {
            _communicator.DisplayState(_gameStatePrinter.Print(Game.State));
        }

        public void NewGame()
        {
            Game = _gameFactory.Create();
        }
    }
}
