using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public interface ISpaceStateConvertor
    {
        GameSpaceState Convert(FieldProbeResult spaceState);
    }
}
