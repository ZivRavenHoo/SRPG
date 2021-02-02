using System;
using ImpulseUtility;
using UnityEngine;
using SRPG;

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
                position.row = row;
                position.column = column;
                gridUnitRenderers[row, column] = CreatGridUnitRenderer(position);
            }
        }
    }

    public event Action<GridUnitRenderer> PointerEnterGridUnit;

    private GridUnitRenderer CreatGridUnitRenderer(GridPosition position)
    {
        GridUnitRenderer gridUnit = Instantiate(gridUnitPrefab, gridUnitRoot);
        gridUnit.Bind(Data.GetGridUnitData(position));
        gridUnit.name = string.Format("GridUnit_{0}", position.ToString());
        gridUnit.gameObject.SetActive(true);
        gridUnit.PointerDown += RefreshGridEffect;
        gridUnit.PointerEnter += OnPointerEnter;
        return gridUnit;
    }

    private void OnPointerEnter(GridUnitRenderer gridUnit)
    {
        if (PointerEnterGridUnit == null)
            return;
        PointerEnterGridUnit(gridUnit);
    }

    public void ShowCanMoveTo(GridPosition position, int mov)
    {
        GridPosition tempPosition = new GridPosition();
        ShowEffect(position);
        for (int i = 1; i <= mov; ++i)
        {
            int x = -i, y = 0;
            for (int k = 0; k < 4; ++k)
            {
                for (int j=0; j < i; ++j)
                {
                    tempPosition.column = position.column + x;
                    tempPosition.row = position.row + y;
                    ShowEffect(tempPosition);
                    x += GameConstant.direction[k, 0];
                    y += GameConstant.direction[k, 1];
                }
            }
        }
    }

    private void ShowEffect(GridPosition position)
    {
        GridUnitRenderer gridUnit = GetGridUnit(position);
        if (gridUnit == null)
            return;
        gridUnit.ShowStreakAnimation();
    }

    private GridUnitRenderer GetGridUnit(GridPosition position)
    {
        if (Data.Size.IsGridPositionInSize(position) == false)
            return null;
        return gridUnitRenderers[position.row, position.column];
    }

    private void RefreshGridEffect(GridUnitRenderer gridUnit)
    {
        if (gridEffect == null)
            return;
        gridEffect.position = gridUnit.transform.position;
        gridEffect.gameObject.SetActive(true);
    }

    private GridUnitRenderer GetGridUnitRenderer(GridPosition position)
    {
        return gridUnitRenderers[position.row, position.column];
    }

    public void Refresh()
    {
        Size size = Data.Size;
        GridPosition position = new GridPosition();
        for (int row = 0; row < size.height; ++row)
        {
            for (int column = 0; column < size.width; ++column)
            {
                position.row = row;
                position.column = column;
                gridUnitRenderers[row, column].Refresh();
            }
        }
    }
}
