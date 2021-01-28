using System.Collections.Generic;
using ImpulseUtility;

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
        data.usTeam.Add(new CombatantData());
        return data;
    }
}