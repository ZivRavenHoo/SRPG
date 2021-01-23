using UnityEngine;
using ImpulseUtility;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class GridUnitRenderer : MonoBehaviour, IPointerDownHandler
{
    private GridUnitData gridUnitData;

    private Image image;

    public GridUnitData GridUnitData
    {
        get { return gridUnitData; }
        set
        {
            gridUnitData = value;
            Refresh();
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        SetType(GridType.Obstacle);
    }

    public void Refresh()
    {
        RefreshLocalPosition();
        RefreshGridType();
    }

    private void RefreshLocalPosition()
    {
        transform.localPosition = RendererUtility.GridPositionToLocalPosition(gridUnitData.position, GameConstant.GridUnitSideLength);
    }

    private void RefreshGridType()
    {
        Color color = Color.white;
        switch (gridUnitData.gridType)
        {
            case GridType.Normal : color = Color.white; break;
            case GridType.Obstacle: color = Color.black; break;
        }
        if (image == null) //可能会在获取image之前调用
            return;
        image.color = color;
    }

    private void SetType(GridType type)
    {
        gridUnitData.gridType = type;
        RefreshGridType();
    }
}
