using UnityEngine;
using ImpulseUtility;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class CombatantRenderer : MonoBehaviour
{
    private CombatantData data;
    private GridPosition position;

    public CombatantData Data
    {
        get { return data; }
        set
        {
            data = value;
            Refresh();
        }
    }
    public GridPosition Position
    {
        get { return position; }
        set
        {
            position = value;
            RefreshLocalPosition();
        }
    }

    public void Refresh()
    {
        RefreshLocalPosition();
    }

    private void RefreshLocalPosition()
    {
        transform.localPosition = RendererUtility.GridPositionToLocalPosition(position, GameConstant.GridUnitSideLength);
    }
}
