namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class FieldProbe
    {
        public FieldProbeResult Probe(bool[,] field, int x, int y)
        {
            if(field[x,y])
            {
                return FieldProbeResult.Mine;
            }
            else if(x>=1 && field[x-1,y])
            {
                return FieldProbeResult.One;
            }
            else if (y >= 1 && field[x, y -1])
            {
                return FieldProbeResult.One;
            }
            else if (x >= 1 && y >= 1 && field[x -1, y - 1])
            {
                return FieldProbeResult.One;
            }
            return FieldProbeResult.Zero;
        }
    }
}
