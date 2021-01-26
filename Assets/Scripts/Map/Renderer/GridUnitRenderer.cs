using UnityEngine;
using ImpulseUtility;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class GridUnitRenderer : MonoBehaviour, IPointerEnterHandler
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
        transform.localPosition = RendererUtility.GridPositionToLocalPosition(gridUnitData.position, GameConstant.GridUnitSideLength);
    }

    private void RefreshGridType()
    {
        Color color = Color.white;
        switch (gridUnitData.gridType)
        {
            case GridType.Normal : color = Color.white; break;
            case GridType.EnemyBirth : color = Color.red;break;
            case GridType.UsBirth : color = Color.blue; break;
            case GridType.Obstacle : color = Color.black; break;
        }
        if (image == null) //可能会在获取image之前调用
            return;
        image.color = color;
    }

    private void SetType(GridType type)
    {
        if (gridUnitData.gridType == type)
            return;
        gridUnitData.gridType = type;
        RefreshGridType();
    }
}
