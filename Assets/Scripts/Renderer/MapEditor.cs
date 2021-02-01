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

public class MapEditor : MonoBehaviour
{
    private GridMapRenderer gridMap;
    private Toggle[] toggles;
    private Button saveButton;

    private EditorMode EditorMode = EditorMode.None;

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
        gridMap.PointerEnterGridUnit += OnPointerEnterGridUnit;

        AdaptRect();
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

    private void SaveMap()
    {
        string path = FileOperation.GetMapDataPath("mapdata");
        FileOperation.ObjectToJsonFile(path, currentMapData);
        Debug.Log("地图保存成功!");
    }

    private void AdaptRect()
    {
        gridMap.transform.localScale = Vector3.one * GetAdaptRectNeedScale();
    }

    private float GetAdaptRectNeedScale()
    {
        float scale = RendererUtility.GetAdaptScreenNeedScale(gridMap.GetComponent<RectTransform>().rect, gridMap.Data.size);
        scale /= GameConstant.GridUnitSideLength;
        return scale;
    }

    private void OnPointerEnterGridUnit(GridUnitRenderer gridUnit)
    {
        if (EditorMode == EditorMode.None)
            return;
        switch (EditorMode)
        {
            case EditorMode.PushObstacle: gridUnit.SetType(GridType.Obstacle); break;
            case EditorMode.PushEnemy: gridUnit.SetType(GridType.EnemyBirth); break;
            case EditorMode.PushUs: gridUnit.SetType(GridType.UsBirth); gridMap.Data.usBirthPosition.Add(gridUnit.GridPosition); break;
            case EditorMode.Eraser: gridUnit.SetType(GridType.Normal); gridMap.Data.enemyBirthPosition.Add(gridUnit.GridPosition); break;
        }
    }
}
