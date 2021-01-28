using UnityEngine;
using System.Collections.Generic;
using ImpulseUtility;

[RequireComponent(typeof(RectTransform))]
public class BattleRenderer : MonoBehaviour
{
    public static Transform gridEffect;
    private Transform combatantRoot;
    private CombatantRenderer combatantPrefab;
    [SerializeField] CombatantPanel combatantPanel;

    private BattleData battleData;

    private GridMapRenderer gridMap;
    private List<CombatantRenderer> usTeam = new List<CombatantRenderer>();
    private List<CombatantRenderer> enemyTeam = new List<CombatantRenderer>();

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
        gridMap.Bind(battleData.mapData);
        foreach(var d in battleData.usTeam)
        {
            CombatantRenderer combatant = CreatCombatant();
            combatant.Bind(d);

            combatant.PointerDown += RefreshGridEffect;
            combatant.PointerDown += SetComtatantPanel;

            usTeam.Add(combatant);
        }

        AdaptRect();
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
        float scale = RendererUtility.GetAdaptScreenNeedScale(GetComponent<RectTransform>().rect, gridMap.currentMapData.size);
        scale /= GameConstant.GridUnitSideLength;
        return scale;
    }

    private void RefreshGridEffect(CombatantRenderer grid)
    {
        gridEffect.position = grid.transform.position;
        gridEffect.gameObject.SetActive(true);
    }

    private void SetComtatantPanel(CombatantRenderer combatant)
    {
        combatantPanel.SelectedCombatant = combatant;
    }
}
