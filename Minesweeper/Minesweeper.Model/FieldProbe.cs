namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class FieldProbe
    {
        public FieldProbeResult Probe(bool[,] field, int x, int y)
        {
            if (field[x, y])
            {
                return FieldProbeResult.Mine;
            }
            else if (SpaceExistsAndHasMine(field, x - 1, y))
            {
                return FieldProbeResult.One;
            }
            else if (SpaceExistsAndHasMine(field, x, y - 1))
            {
                return FieldProbeResult.One;
            }
            else if (SpaceExistsAndHasMine(field, x - 1, y - 1))
            {
                return FieldProbeResult.One;
            }
            return FieldProbeResult.Zero;
        }

        private bool SpaceExistsAndHasMine(bool[,] field, int x, int y)
        {
            return x >= 0 && y >= 0 && field[x, y];
        }
    }
}
