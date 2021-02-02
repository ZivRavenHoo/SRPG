using UnityEngine;
using ImpulseUtility;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using SRPG;

[RequireComponent(typeof(Image))]
public class CombatantRenderer : MonoBehaviour, IPointerDownHandler
{
    private CombatantData data;
    public CombatantData Data => data;

    public GridPosition GridPosition => data.Position;

    public void Bind(CombatantData data)
    {
        this.data = data;
        Refresh();
    }

    public event Action<CombatantRenderer> PointerDown;

    public void OnPointerDown(PointerEventData data)
    {
        PointerDown(this);
    }

    private void Refresh()
    {
        RefreshLocalPosition();
    }

    private void RefreshLocalPosition()
    {
        transform.localPosition = RendererUtility.GridPositionToLocalPosition(data.Position, GameConstant.GridUnitSideLength);
    }
}
