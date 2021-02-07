namespace ImpulseUtility
{
    public interface IUnit
    {
        GridPosition Position { get; set; }
        int Distance(GridPosition position);
    }
}