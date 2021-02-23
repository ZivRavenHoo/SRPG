using SRPG;
using Impulse;
using System.Collections.Generic;

public class MapDataFactory
{
    private static MapDataFactory instance;
    public static MapDataFactory Instance{
        get
        {
            if (instance == null)
                instance = new MapDataFactory();
            return instance;
        }
    }

    public GridMapData CreatMapDataByJsonFile(string mapName) {
        string path = FileOperation.GetMapDataPath(mapName);
        return FileOperation.JsonFileToObject<GridMapData>(path);
    }

    public GridUnitData CreatGridUnitData(GridPosition pos)
    {
        GridUnitData data = new GridUnitData
        {
            Position = pos
        };
        return data;
    }

    public GridUnitData[,] CreatGridUnitDatas(Size size)
    {
        GridUnitData[,] gridUnitDatas = new GridUnitData[size.height, size.width];
        for (int row = 0; row < size.height; ++row)
        {
            for (int column = 0; column < size.width; ++column)
            {
                gridUnitDatas[row, column] = Instance.CreatGridUnitData(new GridPosition(row, column));
            }
        }
        return gridUnitDatas;
    }

    public GridMapData CreatGridMapData(Size size)
    {
        GridMapData data = new GridMapData
        {
            Size = size,
            GridUnitDatas = Instance.CreatGridUnitDatas(size),
            EnemyBirthPosition = new List<GridPosition>(),
            UsBirthPosition = new List<GridPosition>(),
        };
        return data;
    }
}
