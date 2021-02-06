using System;
using System.Collections.Generic;
using ImpulseUtility;

public interface IMap<Unit>
    where Unit : GridPosition
{
    List<Unit> GetNeighbor(GridPosition position);
}
