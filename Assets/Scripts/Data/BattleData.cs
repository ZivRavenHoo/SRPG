using System.Collections.Generic;
using ImpulseUtility;
using System;

public class BattleData
{
    public GridMapData mapData;
    public List<CombatantData> usTeam = new List<CombatantData>();
    public List<CombatantData> enemyTeam = new List<CombatantData>();
}

public class BattleDataFactory
{
    private static BattleDataFactory instance;
    public static BattleDataFactory Instance
    {
        get
        {
            if (instance == null)
                instance = new BattleDataFactory();
            return instance;
        }
    }

    public BattleData GetBattleData()
    {
        BattleData data = new BattleData();
        data.mapData = MapDataFactory.Instance.CreatMapDataByJsonFile("mapdata");
        Random random = new Random();
        int index = random.Next(data.mapData.usBirthPosition.Count);
        GridPosition position = data.mapData.usBirthPosition[index];
        data.usTeam.Add(CreatCombatant(position));
        return data;
    }

    private CombatantData CreatCombatant(GridPosition position)
    {
        CombatantData data = new CombatantData();
        data.Position = position;
        return data;
    }
}