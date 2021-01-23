using UnityEngine;
using ImpulseUtility;

public class GridMapRenderer : MonoBehaviour
{
    [SerializeField] private Transform gridUnitRoot;
    [SerializeField] private GridUnitRenderer gridUnitPrefab;

    private GridMapData currentMapData;
    private GridUnitRenderer[,] gridUnitRenderers;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Bind(GridMapData gridMapData)
    {
        currentMapData = gridMapData;
        LoadGridUnits();
    }

    private void LoadGridUnits()
    {
        Size size = currentMapData.size;
        gridUnitRenderers = new GridUnitRenderer[size.height, size.width];
        GridPosition position;
        for(int row = 0; row < size.height; ++row)
        {
            for(int column = 0; column < size.width; ++column)
            {
                position.row = row;
                position.column = column;
                gridUnitRenderers[row, column] = CreatGridUnitRenderer(position);
            }
        }

        AdaptCanvas();
    }

    private GridUnitRenderer CreatGridUnitRenderer(GridPosition position)
    {
        GridUnitRenderer gridUnit = Instantiate(gridUnitPrefab,gridUnitRoot);
        gridUnit.GridUnitData = currentMapData.GetGridUnitData(position);
        gridUnit.name = string.Format("GridUnit_{0}", position.ToString());
        return gridUnit;
    }

    private void AdaptCanvas()
    {
        transform.localScale *= GetAdaptCanvasNeedScale();
    }

    private float GetAdaptCanvasNeedScale()
    {

        float scale = RendererUtility.GetAdaptScreenNeedScale(rectTransform.rect, currentMapData.size);
        scale /= GameConstant.GridUnitSideLength;
        return scale;
    }

    private GridUnitRenderer GetGridUnitRenderer(GridPosition position)
    {
        return gridUnitRenderers[position.row, position.column];
    }
}
