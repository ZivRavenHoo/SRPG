using UnityEngine;
using ImpulseUtility;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GridUnitRenderer : MonoBehaviour
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

    public void Refresh()
    {
        RefreshLocalPosition();
        //RefreshGridType();
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
        image.color = color;
    }
}
