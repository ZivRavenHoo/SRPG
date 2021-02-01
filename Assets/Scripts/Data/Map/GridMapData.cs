using System.Collections.Generic;
using ImpulseUtility;

public class GridMapData
{
    public Size size;
    public GridUnitData[,] gridUnitDatas;
    public List<GridPosition> enemyBirthPosition;
    public List<GridPosition> usBirthPosition;

    public GridUnitData GetGridUnitData(GridPosition position)
    {
        if (size.IsGridPositionInSize(position) == false)
            return null;
        return gridUnitDatas[position.row, position.column];
    }
}
