using ImpulseUtility;

namespace SRPG
{
    public enum GridType
    {
        Normal,
        EnemyBirth,
        UsBirth,
        Obstacle
    }

    public class GridUnitData
    {
        public GridPosition Position;
        public GridType GridType = GridType.Normal;
        private bool canMove = true;

        public bool CanMove
        {
            get
            {
                if (GridType == GridType.Obstacle)
                    return false;
                return canMove;
            }
        }

        public void SetGridType(GridType type)
        {
            GridType = type;
        }
    }
}
