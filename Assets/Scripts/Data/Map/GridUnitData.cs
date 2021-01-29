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
}
