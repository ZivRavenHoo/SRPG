using System;
using Impulse;
using SRPG;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CombatantRenderer : MonoBehaviour, IPointerDownHandler
{
    private Combatant data;
    public Combatant Data => data;

    public GridPosition GridPosition => data.Position;

    public void Bind(Combatant data)
    {
        this.data = data;
        Refresh();
    }

    public event Action<CombatantRenderer> PointerDown;

    public void OnPointerDown(PointerEventData data)
    {
        if (PointerDown == null)
            return;
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
