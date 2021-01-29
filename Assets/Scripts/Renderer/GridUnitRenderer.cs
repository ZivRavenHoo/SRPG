using System;
using ImpulseUtility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GridUnitRenderer : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    private GridUnitData data;
    private Image image;

    public GridUnitData GridUnitData
    {
        get { return data; }
        set
        {
            data = value;
            Refresh();
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
    }


    public event Action<GridUnitRenderer> PointerDown;
    public void OnPointerDown(PointerEventData data)
    {
        PointerDown(this);
    }

    public void OnPointerEnter (PointerEventData data)
    {
        if (MapEditor.isPress == false || MapEditor.EditorMode == EditorMode.None)
            return;
        switch (MapEditor.EditorMode)
        {
            case EditorMode.PushObstacle : SetType(GridType.Obstacle); break;
            case EditorMode.PushEnemy :SetType(GridType.EnemyBirth);break;
            case EditorMode.PushUs :SetType(GridType.UsBirth);break;
            case EditorMode.Eraser : SetType(GridType.Normal); break;
        }
    }

    public void Refresh()
    {
        RefreshLocalPosition();
        RefreshGridType();
    }

    private void RefreshLocalPosition()
    {
        transform.localPosition = RendererUtility.GridPositionToLocalPosition(data.position, GameConstant.GridUnitSideLength);
    }

    private void RefreshGridType()
    {
        Color color = Color.white;
        switch (data.gridType)
        {
            case GridType.Normal : color = Color.white; break;
            case GridType.EnemyBirth : color = Color.red;break;
            case GridType.UsBirth : color = Color.blue; break;
            case GridType.Obstacle : color = Color.black; break;
            case GridType.CanMove : color = Color.red;break;
        }
        if (image == null) //可能会在获取image之前调用
            return;
        image.color = color;
    }

    public void SetType(GridType type)
    {
        if (data.gridType == type)
            return;
        data.gridType = type;
        RefreshGridType();
    }
}
