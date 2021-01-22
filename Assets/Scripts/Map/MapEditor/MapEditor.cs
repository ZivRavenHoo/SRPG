using ImpulseUtility;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    [SerializeField] GridMapRenderer gridMapPrefab;

    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();

        GridMapData gridMapData = new GridMapData(new Size(18, 10));
        GridMapRenderer gridMap = Instantiate(gridMapPrefab, canvas.transform);
        gridMap.Bind(gridMapData);
    }
}
