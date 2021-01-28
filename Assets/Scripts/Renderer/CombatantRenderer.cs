using UnityEngine;
using ImpulseUtility;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class CombatantRenderer : MonoBehaviour
{
    private CombatantData data;

    public void Bind(CombatantData data)
    {
        this.data = data;
        Refresh();
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
