using System;
using System.Collections.Generic;
using UnityEngine;

public class GameConstant
{
    public const float GridUnitSideLength = 100f;
    public const string MapDataFilePath = @"/GameResources/LevelMap/";
    public static readonly int[,] direction = new int[4, 2] { { 1, 1 }, { 1, -1 }, { -1, -1 }, { -1, 1 } };
};
