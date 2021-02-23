using UnityEngine.UI;
using UnityEngine;

public class CombatantPanel : MonoBehaviour
{
    private ImageBox combatantImage;
    private Bar HPBar;

    private CombatantRenderer combatant;
    public CombatantRenderer Combatant
    {
        get => combatant;
        set
        {
            if (combatant == value)
                return;
            combatant = value;
            Refresh();
        }
    }

    private void Start()
    {
        combatantImage = GetComponentInChildren<ImageBox>();
        HPBar = transform.Find("HPBar").GetComponent<Bar>();
    }

    private void Refresh()
    {
        if (Combatant == null)
            return;
        combatantImage.Image.sprite = combatant.GetComponent<Image>().sprite;
        HPBar.Percent = combatant.Data.Protery.MaxHp;
    }
}
