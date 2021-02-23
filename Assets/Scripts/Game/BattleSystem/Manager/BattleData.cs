using System.Collections.Generic;
using Impulse;
using UnityEngine;

namespace SRPG
{
    public class BattleData
    {
        private GridMapData mapData;
        private List<Combatant> usTeam = new List<Combatant>();
        private List<Combatant> enemyTeam = new List<Combatant>();

        #region 属性
        public GridMapData MapData => mapData;
        public List<Combatant> UsTeam => usTeam;
        public List<Combatant> EnemyTeam => enemyTeam;
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
            data.usTeam.Add(Combatant.CreatCombatant(position));
            return data;
        }
    }
}