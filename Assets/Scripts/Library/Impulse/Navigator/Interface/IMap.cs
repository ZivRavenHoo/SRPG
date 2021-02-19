using System.Collections.Generic;

namespace Impulse
{
    public interface IMap<T>
        where T : INode
    {
        List<T> GetNeighbor(T position);
    }
}