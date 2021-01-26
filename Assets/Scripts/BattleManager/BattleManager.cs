using UnityEngine;
using System.Collections;
using System.IO;

public class BattleManager : MonoBehaviour
{
    private void Start()
    {
        CreatMap();
    }

    private void CreatMap()
    {
        Debug.Log(MapFactory.Instance.CreatMapDataByJsonFile("mapdata"));
    }
}
