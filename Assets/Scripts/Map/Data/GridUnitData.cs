using ImpulseUtility;

public enum GridType
{
    Normal,
    Obstacle
}

public class GridUnitData
{
    public GridPosition position;
    public GridType gridType = GridType.Normal;

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
