using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using ImpulseUtility;

public enum EditorMode
{
    None,
    PushObstacle,
    Eraser,
    PushEnemy,
    PushUs
}

public class MapEditor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GridMapRenderer gridMap;
    private Toggle[] toggles;
    private Button saveButton;

    public static EditorMode EditorMode = EditorMode.PushObstacle;

    private Size size = new Size(16, 9);
    GridMapData currentMapData;

    private void Start()
    {
        gridMap = GetComponentInChildren<GridMapRenderer>();
        toggles = GetComponentsInChildren<Toggle>();
        saveButton = GetComponentInChildren<Button>();

        AddTogglesListener();
        saveButton.onClick.AddListener(SaveMap);

        currentMapData = MapDataFactory.Instance.CreatGridMapData(size);
        gridMap.Bind(currentMapData);
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

    private void SaveMap()
    {
        string path = FileOperation.GetMapDataPath("mapdata");
        FileOperation.ObjectToJsonFile(path, currentMapData);
        Debug.Log("地图保存成功!");
    }
}
