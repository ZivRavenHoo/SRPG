using System.Collections.Generic;
using ImpulseUtility;

public class GridMapData
{
    public Size size;
    public GridUnitData[,] gridUnitDatas;

    public GridMapData(Size size)
    {
        this.size = size;
        gridUnitDatas = GridUnitData.CreatGridUnitDatas(size);
    }

    public GridUnitData GetGridUnitData(GridPosition position)
    {
        return gridUnitDatas[position.row, position.column];
    }
}
