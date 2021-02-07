using System.Collections.Generic;

namespace ImpulseUtility
{
    public interface IMap<T>
    where T : IUnit
    {
        List<T> GetNeighbor(GridPosition position);
    }
}