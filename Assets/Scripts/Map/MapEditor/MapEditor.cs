using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using ImpulseUtility;
using LitJson;
using System.IO;
using UnityEditor;

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
    private Button saveButton;
    private Canvas canvas;

    public static EditorMode EditorMode = EditorMode.PushObstacle;

    private Size size = new Size(16, 9);
    GridMapData currentMapData;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        toggles = GetComponentsInChildren<Toggle>();
        saveButton = GetComponentInChildren<Button>();

        AddTogglesListener();
        saveButton.onClick.AddListener(SaveMap);

        currentMapData = new GridMapData(size);
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
        string json = JsonMapper.ToJson(currentMapData);
        string path = Application.dataPath + @"/GameResources/LevelMap/mapdata.json";

        FileInfo file = new FileInfo(path);
        StreamWriter sw = file.CreateText();
        sw.Write(json);
        sw.Close();
        sw.Dispose();

        AssetDatabase.Refresh();
        Debug.Log("地图保存成功!");
    }
}
