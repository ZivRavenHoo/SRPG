using System.Collections.Generic;
using ImpulseUtility;
using Newtonsoft.Json;

namespace SRPG
{
    public class GridMapData
    {
        public Size Size { get; set; }
        public GridUnitData[,] GridUnitDatas { get; set; }
        public List<GridPosition> EnemyBirthPosition { get; set; }
        public List<GridPosition> UsBirthPosition { get; set; }

        public GridUnitData GetGridUnitData(GridPosition position)
        {
            if (Size.IsGridPositionInSize(position) == false)
                return null;
            return GridUnitDatas[position.row, position.column];
        }
    }
}
