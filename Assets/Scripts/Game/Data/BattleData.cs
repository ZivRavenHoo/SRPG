using System.Collections.Generic;
using ImpulseUtility;
using UnityEngine;

namespace SRPG
{
    public class BattleData
    {
        private GridMapData mapData;
        private List<CombatantData> usTeam = new List<CombatantData>();
        private List<CombatantData> enemyTeam = new List<CombatantData>();

        #region 属性
        public GridMapData MapData => mapData;
        public List<CombatantData> UsTeam => usTeam;
        public List<CombatantData> EnemyTeam => enemyTeam;
        #endregion

        private BattleData() { }

        public static BattleData GetBattleData()
        {
            BattleData data = new BattleData
            {
                mapData = MapDataFactory.Instance.CreatMapDataByJsonFile("mapdata")
            };
            Debug.Log(data.mapData.Size);
            int index = Random.Range(0, data.mapData.UsBirthPosition.Count);
            GridPosition position = data.mapData.UsBirthPosition[index];
            data.usTeam.Add(CombatantData.CreatCombatant(position));
            return data;
        }
    }
}