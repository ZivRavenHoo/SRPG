namespace Impulse
{
    public interface INode
    {
        GridPosition Position { get; set; }
        int Distance(GridPosition position);
    }
}