using UnityEngine;
using UnityEngine.UI;
using ImpulseUtility;

public class GridMapRenderer : MonoBehaviour
{

    private GridUnitRenderer gridUnitPrefab;
    private Transform gridUnitRoot;

    public GridMapData currentMapData;
    private GridUnitRenderer[,] gridUnitRenderers;

    public Size MapSize
    {
        get
        {
            if (currentMapData == null)
                return null;
            return currentMapData.size;
        }
    }

    private void Awake()
    {
        gridUnitRoot = transform.Find("GridUnitRoot");
        gridUnitPrefab = gridUnitRoot.Find("GridUnitPrefab").GetComponent<GridUnitRenderer>();
    }

    public void Bind(GridMapData gridMapData)
    {
        if (currentMapData == gridMapData)
            return;
        currentMapData = gridMapData;
        LoadGridUnits();
    }

    private void LoadGridUnits()
    {
        Size size = currentMapData.size;
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
        gridUnit.GridUnitData = currentMapData.GetGridUnitData(position);
        gridUnit.name = string.Format("GridUnit_{0}", position.ToString());
        gridUnit.gameObject.SetActive(true);
        return gridUnit;
    }

    private GridUnitRenderer GetGridUnitRenderer(GridPosition position)
    {
        return gridUnitRenderers[position.row, position.column];
    }
}
