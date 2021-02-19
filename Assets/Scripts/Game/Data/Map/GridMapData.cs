using System.Collections.Generic;
using Impulse;
using UnityEngine;

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
            return GridUnitDatas[position.Row, position.Column];
        }

        public List<GridUnitData> GetNeighbor(GridUnitData node)
        {
            GridPosition target = node.Position;
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { 1, 0, -1, 0 };
            List<GridUnitData> neighbors = new List<GridUnitData>();
            GridPosition temp = new GridPosition(target);
            for (int i = 0; i < 4; ++i)
            {
                temp.Row = target.Row + dx[i];
                temp.Column = target.Column + dy[i];
                if (!Size.IsGridPositionInSize(temp))
                    break;
                neighbors.Add(GetGridUnitData(temp));
            }
            return neighbors;
        }

        public List<GridUnitData> GetCanMoveUnitsPosition(GridUnitData grid,int mov)
        {
            if (Size.IsGridPositionInSize(grid.Position) == false)
                return null;

            List<GridUnitData> units = new List<GridUnitData>();
            Queue<GridUnitData> queue = new Queue<GridUnitData>();
            queue.Enqueue(grid);
            for(int i = 0; i <= mov; ++i)
            {
                int len = queue.Count;
                for(int j = 0; j < len; ++j)
                {
                    GridUnitData node = queue.Dequeue();
                    units.Add(node);
                    List<GridUnitData> neighbors = GetNeighbor(node);
                    foreach(GridUnitData temp in neighbors)
                    {
                        if (units.Contains(temp) || !temp.CanMove)
                            continue;
                        queue.Enqueue(temp);
                    }
                }
            }
            return units;
        }
    }
}
