using UnityEngine;
using System.Collections.Generic;
using ImpulseUtility;

[RequireComponent(typeof(RectTransform))]
public class BattleRenderer : MonoBehaviour
{
    public static Transform gridEffect;
    private Transform combatantRoot;
    private CombatantRenderer combatantPrefab;

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
        battleData = data;
        gridMap.Bind(battleData.mapData);
        foreach(var d in battleData.usTeam)
        {
            CombatantRenderer combatant = CreatCombatant();
            combatant.Bind(d);
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
        transform.localScale *= GetAdaptRectNeedScale();
    }

    private float GetAdaptRectNeedScale()
    {
        float scale = RendererUtility.GetAdaptScreenNeedScale(GetComponent<RectTransform>().rect, gridMap.currentMapData.size);
        scale /= GameConstant.GridUnitSideLength;
        return scale;
    }
}
