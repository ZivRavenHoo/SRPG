using System.Collections.Generic;
using ImpulseUtility;
using UnityEngine;
using SRPG;

[RequireComponent(typeof(RectTransform))]
public class BattleRenderer : MonoBehaviour
{
    private BattleData battleData;

    private Transform gridEffect;
    private Transform combatantRoot;
    private CombatantRenderer combatantPrefab;
    [SerializeField] private CombatantPanel combatantPanel;

    private GridMapRenderer gridMap;
    private List<CombatantRenderer> usTeam = new List<CombatantRenderer>();
    private List<CombatantRenderer> enemyTeam = new List<CombatantRenderer>();

    private CombatantRenderer selectedCombatant;
    public CombatantRenderer SelectedCombatant
    {
        get => selectedCombatant;
        set
        {
            if (selectedCombatant == value)
                return;
            selectedCombatant = value;
            RefreshComtatantPanel();
        }
    }

    private void Start()
    {
        gridEffect = transform.Find("GridEffect");
        combatantRoot = transform.Find("CombatantRoot");
        combatantPrefab = combatantRoot.Find("CombatantPrefab").GetComponent<CombatantRenderer>();

        gridMap = GetComponentInChildren<GridMapRenderer>();
    }

    public void Bind(BattleData data)
    {
        if (battleData == data)
            return;
        battleData = data;
        gridMap.Bind(battleData.MapData);
        foreach(var usData in battleData.UsTeam)
        {
            CombatantRenderer combatant = CreatCombatant();
            combatant.Bind(usData);

            combatant.PointerDown += RefreshGridEffect;
            combatant.PointerDown += RefreshSelectedCombatant;

            usTeam.Add(combatant);
        }
        AdaptRect();
        Refresh();
    }

    private CombatantRenderer CreatCombatant()
    {
        CombatantRenderer combatant = Instantiate(combatantPrefab, combatantRoot);
        combatant.gameObject.SetActive(true);
        return combatant;
    }

    private void AdaptRect()
    {
        transform.localScale = Vector3.one * GetAdaptRectNeedScale();
    }

    private float GetAdaptRectNeedScale()
    {
        float scale = RendererUtility.GetAdaptScreenNeedScale(GetComponent<RectTransform>().rect, gridMap.Data.Size);
        scale /= GameConstant.GridUnitSideLength;
        return scale;
    }

    private void RefreshGridEffect(CombatantRenderer grid)
    {
        gridEffect.position = grid.transform.position;
        gridEffect.gameObject.SetActive(true);
    }

    private void RefreshSelectedCombatant(CombatantRenderer combatant)
    {
        SelectedCombatant = combatant;
        gridMap.ShowCanMoveTo(SelectedCombatant.GridPosition, SelectedCombatant.Data.Protery.MOV);
    }

    private void RefreshComtatantPanel()
    {
        if (combatantPanel == null)
            return;
        combatantPanel.Combatant = SelectedCombatant;
    }

    public void Refresh()
    {
        gridMap.Refresh();
    }
}
