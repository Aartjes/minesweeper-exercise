using Com.Github.Aartjes.Minesweeper.Model;
using System;

namespace Com.Github.Aartjes.Minesweeper.Cli
{
    public class SpaceStateToStringConvertor
    {
        public string Convert(GameSpaceState input)
        {
            return input switch
            {
                GameSpaceState.Blank => ".",
                GameSpaceState.Flag => "P",
                GameSpaceState.Mine => "*",
                GameSpaceState.Zero => "0",
                GameSpaceState.One => "1",
                GameSpaceState.Two => "2",
                GameSpaceState.Three => "3",
                GameSpaceState.Four => "4",
                GameSpaceState.Five => "5",
                GameSpaceState.Six => "6",
                GameSpaceState.Seven => "7",
                GameSpaceState.Eight => "8",
                _ => throw new NotSupportedException(),
            };
        }
    }
}
