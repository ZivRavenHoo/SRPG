using System;
using Impulse;
using UnityEngine;
using SRPG;
using System.Collections.Generic;

public class GridMapRenderer : MonoBehaviour
{
    public GridMapData Data;

    public Transform gridEffect;
    private GridUnitRenderer gridUnitPrefab;
    private Transform gridUnitRoot;

    private GridUnitRenderer[,] gridUnitRenderers;

    public Size MapSize
    {
        get
        {
            if (Data == null)
                return null;
            return Data.Size;
        }
    }

    private void Awake()
    {
        gridUnitRoot = transform.Find("GridUnitRoot");
        gridUnitPrefab = gridUnitRoot.Find("GridUnitPrefab").GetComponent<GridUnitRenderer>();
    }

    public void Bind(GridMapData gridMapData)
    {
        if (Data == gridMapData)
            return;
        Data = gridMapData;
        LoadGridUnits();
    }

    private void LoadGridUnits()
    {
        Size size = Data.Size;
        gridUnitRenderers = new GridUnitRenderer[size.height, size.width];

        GridPosition position = new GridPosition();
        for(int row = 0; row < size.height; ++row)
        {
            for(int column = 0; column < size.width; ++column)
            {
                position.Row = row;
                position.Column = column;
                gridUnitRenderers[row, column] = CreatGridUnitRenderer(position);
            }
        }
    }

    public event Action<GridUnitRenderer> PointerDownGridUnit;

    private GridUnitRenderer CreatGridUnitRenderer(GridPosition position)
    {
        GridUnitRenderer gridUnit = Instantiate(gridUnitPrefab, gridUnitRoot);
        gridUnit.Bind(Data.GetGridUnitData(position));
        gridUnit.name = string.Format("GridUnit_{0}", position.ToString());
        gridUnit.gameObject.SetActive(true);
        gridUnit.PointerDown += (GridUnitRenderer grid) => PointerDownGridUnit(grid);
        return gridUnit;
    }

    public void SetCanMoveToStreak(GridPosition position, int mov,bool active)
    {
        List<GridUnitData> grids = Data.GetCanMoveUnitsPosition(Data.GetGridUnitData(position), mov);
        foreach(var temp in grids)
        {
            SetEffectActive(temp, active);
        }
    }

    private void SetEffectActive(GridUnitData grid,bool active)
    {
        GridUnitRenderer gridUnit = GetGridUnit(grid.Position);
        if (gridUnit == null)
            return;
        gridUnit.SetStreakAnimation(active);
    }

    private GridUnitRenderer GetGridUnit(GridPosition position)
    {
        if (Data.Size.IsGridPositionInSize(position) == false)
            return null;
        return gridUnitRenderers[position.Row, position.Column];
    }

    private GridUnitRenderer GetGridUnitRenderer(GridPosition position)
    {
        return gridUnitRenderers[position.Row, position.Column];
    }

    public void Refresh()
    {
        Size size = Data.Size;
        GridPosition position = new GridPosition();
        for (int row = 0; row < size.height; ++row)
        {
            for (int column = 0; column < size.width; ++column)
            {
                position.Row = row;
                position.Column = column;
                gridUnitRenderers[row, column].Refresh();
            }
        }
    }
}
