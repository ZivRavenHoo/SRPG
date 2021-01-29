using ImpulseUtility;
using UnityEngine;
using System.Collections.Generic;

public class GridMapRenderer : MonoBehaviour
{

    private GridUnitRenderer gridUnitPrefab;
    private Transform gridUnitRoot;

    public GridMapData Data;
    private GridUnitRenderer[,] gridUnitRenderers;

    public Size MapSize
    {
        get
        {
            if (Data == null)
                return null;
            return Data.size;
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
        Size size = Data.size;
        gridUnitRenderers = new GridUnitRenderer[size.height, size.width];
        GridPosition position = new GridPosition();
        for(int row = 0; row < size.height; ++row)
        {
            for(int column = 0; column < size.width; ++column)
            {
                position.row = row;
                position.column = column;
                gridUnitRenderers[row, column] = CreatGridUnitRenderer(position);
            }
        }
    }

    private GridUnitRenderer CreatGridUnitRenderer(GridPosition position)
    {
        GridUnitRenderer gridUnit = Instantiate(gridUnitPrefab, gridUnitRoot);
        gridUnit.GridUnitData = Data.GetGridUnitData(position);
        gridUnit.name = string.Format("GridUnit_{0}", position.ToString());
        gridUnit.gameObject.SetActive(true);
        return gridUnit;
    }

    private GridUnitRenderer GetGridUnitRenderer(GridPosition position)
    {
        return gridUnitRenderers[position.row, position.column];
    }

    public void ShowCanMoveTo(GridPosition position, int mov)
    {
        GridPosition tempPosition = new GridPosition();
        for (int i = 1; i <= mov; ++i)
        {
            int x = -i, y = 0;
            for (int k = 0; k < 4; ++k)
            {
                for (int j=0; j < i; ++j)
                {
                    tempPosition.column = position.column + x;
                    tempPosition.row = position.row + y;
                    SetGridType(tempPosition, GridType.CanMove);
                    x += GameConstant.direction[k, 0];
                    y += GameConstant.direction[k, 1];
                }
            }
        }
    }

    private void SetGridType(GridPosition position, GridType type)
    {
        if (Data.size.IsGridPositionInSize(position) == false)
            return;
        GetGridUnit(position).SetType(type);
    }

    private GridUnitRenderer GetGridUnit(GridPosition position)
    {
        return gridUnitRenderers[position.row, position.column];
    }
}
