using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using ImpulseUtility;

public enum EditorMode
{
    PushObstacle,
    Eraser
}

public class MapEditor : MonoBehaviour
{
    [SerializeField] private GridMapRenderer gridMap = null;
    [SerializeField] private RectTransform editorPanel;
    private Toggle[] toggles;
    private Canvas canvas;

    public static EditorMode EditorMode = EditorMode.PushObstacle;

    private Size size = new Size(16, 9);

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        toggles = GetComponentsInChildren<Toggle>();
        AddTogglesListener();

        GridMapData gridMapData = new GridMapData(size);
        gridMap.Bind(gridMapData);
    }

    private void AddTogglesListener()
    {
        foreach(Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener((isOn) => {
                string mode = toggle.name;
                if (mode == "Obstacle")
                    EditorMode = EditorMode.PushObstacle;
                else
                    EditorMode = EditorMode.Eraser;
                Debug.Log(EditorMode);
            });
        }
    }
}
