using System;
using ImpulseUtility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SRPG;

[RequireComponent(typeof(Image))]
public class GridUnitRenderer : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    private GridUnitData data;
    private Image image;
    [SerializeField] private GameObject streakAnimation;

    public GridPosition GridPosition => data.Position;
    public GridUnitData Data => data;

    public void Bind(GridUnitData data)
    {
        this.data = data;
        Refresh();
    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public event Action<GridUnitRenderer> PointerDown,PointerEnter;
    public void OnPointerDown(PointerEventData data)
    {
        if (PointerDown == null)
            return;
        PointerDown(this);
    }

    public void OnPointerEnter (PointerEventData data)
    {
        if (PointerEnter == null)
            return;
        PointerEnter(this);
    }

    public void Refresh()
    {
        RefreshLocalPosition();
        RefreshGridType();
    }

    private void RefreshLocalPosition()
    {
        transform.localPosition = RendererUtility.GridPositionToLocalPosition(data.Position, GameConstant.GridUnitSideLength);
    }

    private void RefreshGridType()
    {
        Color color = Color.white;
        switch (data.GridType)
        {
            case GridType.Normal : color = Color.white; break;
            //case GridType.EnemyBirth : color = Color.red;break;
            //case GridType.UsBirth : color = Color.blue; break;
            case GridType.Obstacle : color = Color.black; break;
        }
        if (image == null) //可能会在获取image之前调用
            return;
        image.color = color;
    }

    public void SetType(GridType type)
    {
        if (data.GridType == type)
            return;
        data.SetGridType(type);
        RefreshGridType();
    }

    public void SetStreakAnimation(bool active)
    {
        if (active == true)
            active = Data.CanMove;
        streakAnimation.SetActive(active);
    }
}
