using ImpulseUtility;

namespace SRPG
{
    public enum GridType
    {
        Normal,
        EnemyBirth,
        UsBirth,
        Obstacle,
        CanMove
    }

    public class GridUnitData
    {
        internal GridPosition position;
        internal GridType gridType = GridType.Normal;
        private bool canMove = true;

        public GridPosition Position
        {
            get => position;
            set => position = value;
        }
        public GridType GridType
        {
            get => gridType;
            set => gridType = value;
        }
        public bool CanMove
        {
            get
            {
                if (gridType == GridType.Obstacle)
                    return false;
                return canMove;
            }
        }

        public void SetGridType(GridType type)
        {
            gridType = type;
        }
    }
}
