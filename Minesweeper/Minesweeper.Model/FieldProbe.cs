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
            return FieldProbeResult.Zero;
        }
    }
}
