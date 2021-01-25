using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using ImpulseUtility;

public enum EditorMode
{
    PushObstacle,
    Eraser,
    PushEnemy,
    PushUs
}

public class MapEditor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
                if (mode == "PushObstacle")
                    EditorMode = EditorMode.PushObstacle;
                else if (mode == "Eraser")
                    EditorMode = EditorMode.Eraser;
                else if (mode == "PushEnemy")
                    EditorMode = EditorMode.PushEnemy;
                else if (mode == "PushUs")
                    EditorMode = EditorMode.PushUs;
                Debug.Log(EditorMode);
            });
        }
    }

    public static bool isPress = false;
    public void OnPointerDown(PointerEventData data)
    {
        isPress = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        isPress = false;
    }
}
