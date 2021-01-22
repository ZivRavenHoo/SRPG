using ImpulseUtility;

public enum GridType
{
    Normal,
    Obstacle
}

public class GridUnitData
{
    private GridPosition position;
    private GridType gridType = GridType.Normal;

    public GridPosition Position { get { return position; } }

    public GridUnitData(GridPosition position)
    {
        this.position = position;
    }

    public static GridUnitData[,] CreatGridUnitDatas(Size size)
    {
        GridUnitData[,] gridUnitDatas = new GridUnitData[size.height, size.width];
        for (int row = 0; row < size.height; ++row)
        {
            for (int column = 0; column < size.width; ++column)
            {
                gridUnitDatas[row, column] = new GridUnitData(new GridPosition(row, column));
            }
        }

        return gridUnitDatas;
    }
}
