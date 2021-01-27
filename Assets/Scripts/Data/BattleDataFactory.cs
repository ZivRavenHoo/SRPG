using System;

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
        BattleData data = new BattleData()
        {
            mapData = MapFactory.Instance.CreatMapDataByJsonFile("mapdata")
        };
        return data;
    }
}
