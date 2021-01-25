using UnityEngine;
using UnityEngine.UI;
using ImpulseUtility;

public class GridMapRenderer : MonoBehaviour
{

    private Transform enemyBrithPrefab;
    private Transform usBrithPrefab;
    private GridUnitRenderer gridUnitPrefab;

    private Transform enemyBrithRoot;
    private Transform usBrithRoot;
    private Transform gridUnitRoot;

    private RectTransform rectTransform;

    private GridMapData currentMapData;
    private GridUnitRenderer[,] gridUnitRenderers;

    private void Start()
    {

        gridUnitRoot = transform.Find("GridUnitRoot");
        enemyBrithRoot = transform.Find("EnemyBrithRoot");
        usBrithRoot = transform.Find("UsBrithRoot");

        gridUnitPrefab = transform.Find("GridUnitPrefab").GetComponent<GridUnitRenderer>();
        enemyBrithPrefab = transform.Find("EnemyBrithPrefab");
        usBrithPrefab = transform.Find("UsBrithPrefab");

        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MapEditor.EditorMode == EditorMode.PushObstacle || MapEditor.EditorMode == EditorMode.Eraser)
                return;
            switch (MapEditor.EditorMode)
            {
                case EditorMode.PushEnemy: PushEnemyBrithPosition(); break;
                case EditorMode.PushUs: PushUsBrithPosition(); break;
            }
        }
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
        gridUnit.gameObject.SetActive(true);
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

    private void PushEnemyBrithPosition()
    {
        GameObject enemy = Instantiate(enemyBrithPrefab.gameObject, enemyBrithRoot);
        enemy.transform.localPosition = RendererUtility.LocalPositionRangeGrid(Camera.main, gridUnitRoot, GameConstant.GridUnitSideLength);
        enemy.gameObject.SetActive(true);
    }

    private void PushUsBrithPosition()
    {
        GameObject us = Instantiate(usBrithPrefab.gameObject, usBrithRoot);
        us.transform.localPosition = RendererUtility.LocalPositionRangeGrid(Camera.main, gridUnitRoot, GameConstant.GridUnitSideLength);
        us.gameObject.SetActive(true);
    }
}
