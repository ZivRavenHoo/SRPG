using System;
using UnityEngine;
using ImpulseUtility;

public class MapFactory
{
    private static MapFactory instance;
    public static MapFactory Instance{
        get
        {
            if (instance == null)
                instance = new MapFactory();
            return instance;
        }
    }

    public GridMapData CreatGridMapData(Size size)
    {
        GridMapData data = new GridMapData
        {
            size = size,
            gridUnitDatas = CreatGridUnitDatas(size)
        };
        return data;
    }

    public GridMapData CreatMapDataByJsonFile(string mapName) {
        string path = FileOperation.GetMapDataPath("mapdata");
        return FileOperation.JsonFileToObject<GridMapData>(path);
    }

    public GridUnitData CreatGridUnitData(GridPosition pos)
    {
        GridUnitData data = new GridUnitData
        {
            position = pos,
            gridType = GridType.Normal
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
                gridUnitDatas[row, column] = CreatGridUnitData(new GridPosition(row, column));
            }
        }
        return gridUnitDatas;
    }
}
