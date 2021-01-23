using ImpulseUtility;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    [SerializeField] private GridMapRenderer gridMap = null;
    [SerializeField] private RectTransform editorPanel;

    private Canvas canvas;

    private Size size = new Size(16, 9);

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();

        GridMapData gridMapData = new GridMapData(size);
        gridMap.Bind(gridMapData);
    }
}
