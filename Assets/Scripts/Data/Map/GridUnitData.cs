using ImpulseUtility;

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
    public GridPosition position;
    public GridType gridType = GridType.Normal;
    private bool canMove = true;
    public bool CanMove
    {
        set
        {
            canMove = value;
        }
        get
        {
            if (gridType == GridType.Obstacle)
                return false;
            return canMove;
        }
    }
}
