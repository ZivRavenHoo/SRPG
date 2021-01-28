using UnityEngine.UI;
using UnityEngine;

public class CombatantPanel : MonoBehaviour
{
    private ImageBox combatantImage;
    private Bar HPBar;

    private CombatantRenderer selectedCombatant;
    public CombatantRenderer SelectedCombatant
    {
        get => selectedCombatant;
        set
        {
            if (selectedCombatant == value)
                return;
            selectedCombatant = value;
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
        combatantImage.Image.sprite = selectedCombatant.GetComponent<Image>().sprite;
        HPBar.Percent = selectedCombatant.Data.Protery.MaxHp;
    }
}
