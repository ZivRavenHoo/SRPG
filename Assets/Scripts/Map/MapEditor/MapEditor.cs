using ImpulseUtility;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    [SerializeField] private GridMapRenderer gridMap;
    [SerializeField] private RectTransform editorPanel;

    private Canvas canvas;
    private RectTransform canvasRect;

    private Size size = new Size(16, 9);

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();

        GridMapData gridMapData = new GridMapData(size);
        gridMap.Bind(gridMapData);
    }
}
