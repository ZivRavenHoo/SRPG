using UnityEngine;
using System.Collections;
using System.IO;

public class BattleManager : MonoBehaviour
{
    private GridMapData currentMapData;
    private GridMapRenderer gridMap;

    private void Start()
    {
        gridMap = GetComponentInChildren<GridMapRenderer>();
        CreatMap();
    }

    private void CreatMap()
    {
        currentMapData = MapFactory.Instance.CreatMapDataByJsonFile("mapdata");
        gridMap.Bind(currentMapData);
    }

}
