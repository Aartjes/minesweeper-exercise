using Com.Github.Aartjes.Minesweeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public class GameStatePrinter
    {
        private ISpaceStateToStringConvertor _convertor;

        public GameStatePrinter(ISpaceStateToStringConvertor convertor)
        {
            _convertor = convertor;
        }

        public string Print(IGameState state)
        {
            //first line
            var builder = new StringBuilder();
            builder.Append("\t|");
            for (int x = 1; x <= state.FieldWidth; x++)
            {
                builder.Append("\t");
                builder.Append(x);
            }
            builder.AppendLine();
            builder.Append("-\t-");
            for (int x = 0; x < state.FieldWidth; x++)
            {
                builder.Append("\t-");
            }
            builder.AppendLine();
            for (int y = 0; y < state.FieldHeight; y++)
            {
                builder.Append(y + 1);
                builder.Append("\t|");
                for (int x = 0; x < state.FieldWidth; x++)
                {
                    builder.Append("\t");
                    builder.Append(
                        _convertor.Convert(
                            state[x, y]));
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
