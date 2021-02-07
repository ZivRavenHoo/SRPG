using System.Collections.Generic;
using ImpulseUtility;
using Newtonsoft.Json;

namespace SRPG
{
    public class GridMapData : IMap<GridUnitData>
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

        public List<GridUnitData> GetNeighbor(GridPosition target)
        {
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { 1, 0, -1, 0 };
            List<GridUnitData> neighbors = new List<GridUnitData>();
            GridPosition temp = new GridPosition(target);
            for (int i = 0; i < 4; ++i)
            {
                temp.row = target.row + dx[i];
                temp.column = target.column + dy[i];
                if (!Size.IsGridPositionInSize(temp))
                    break;
                neighbors.Add(GetGridUnitData(temp));
            }
            return neighbors;
        }
    }
}
