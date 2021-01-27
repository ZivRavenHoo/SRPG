using UnityEngine;
using System.Collections.Generic;
using ImpulseUtility;

public class BattleManager : MonoBehaviour
{
    public static Transform gridEffect;

    private GridMapRenderer gridMap;
    private GridMapData currentMapData;
    private Transform combatantRoot;
    private RectTransform rectTransform;
    private CombatantRenderer combatantPrefab;
    private List<CombatantRenderer> usTeam = new List<CombatantRenderer>();

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        gridMap = GetComponentInChildren<GridMapRenderer>();
        combatantRoot = transform.Find("CombatantRoot");
        gridEffect = transform.Find("GridEffect");
        combatantPrefab = combatantRoot.Find("CombatantPrefab").GetComponent<CombatantRenderer>();

        CreatBattle();
    }

    private void CreatBattle()
    {
        CreatMap();
        CreatCombatant();
        CreatCombatant();

        AdaptCanvas();
    }

    private void CreatMap()
    {
        currentMapData = MapFactory.Instance.CreatMapDataByJsonFile("mapdata");
        gridMap.Bind(currentMapData);
    }

    private void CreatCombatant()
    {
        CombatantData data = new CombatantData();
        CombatantRenderer combatant = Instantiate(combatantPrefab,combatantRoot);
        combatant.Data = data;
        combatant.gameObject.SetActive(true);
        combatant.Position = currentMapData.size.GetRangePosition();

        usTeam.Add(combatant);
    }

    private void AdaptCanvas()
    {
        transform.localScale *= GetAdaptCanvasNeedScale();
    }

    private float GetAdaptCanvasNeedScale()
    {
        float scale = RendererUtility.GetAdaptScreenNeedScale(rectTransform.rect, currentMapData.size);
        scale /= GameConstant.GridUnitSideLength;
        return scale;
    }
}
