namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class FieldProbe
    {
        public FieldProbeResult Probe(bool[,] field, int x, int y)
        {
            int neighborMineCount = 0;
            if (field[x, y])
            {
                return FieldProbeResult.Mine;
            }
            if (SpaceExistsAndHasMine(field, x - 1, y))
            {
                neighborMineCount += 1;
            }
            if (SpaceExistsAndHasMine(field, x, y - 1))
            {
                neighborMineCount += 1;
            }
            if (SpaceExistsAndHasMine(field, x - 1, y - 1))
            {
                neighborMineCount += 1;
            }
            if (SpaceExistsAndHasMine(field, x + 1, y))
            {
                neighborMineCount += 1;
            }
            if (SpaceExistsAndHasMine(field, x, y + 1))
            {
                neighborMineCount += 1;
            }
            if (SpaceExistsAndHasMine(field, x + 1, y + 1))
            {
                neighborMineCount += 1;
            }
            if(SpaceExistsAndHasMine(field, x + 1, y - 1))
            {
                neighborMineCount += 1;
            }
            if (SpaceExistsAndHasMine(field, x - 1, y + 1))
            {
                neighborMineCount += 1;
            }
            return (FieldProbeResult)neighborMineCount;
        }

        private bool SpaceExistsAndHasMine(bool[,] field, int x, int y)
        {
            return x >= 0 
                && x<field.GetLength(0)
                && y >= 0  
                && y < field.GetLength(1) 
                && field[x, y];
        }
    }
}
