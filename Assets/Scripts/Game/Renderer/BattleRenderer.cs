using System.Collections.Generic;
using Impulse;
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
        gridMap.PointerDownGridUnit += OnPinterDownGridUnit;
        foreach(var usData in battleData.UsTeam)
        {
            CombatantRenderer combatant = CreatCombatant();
            combatant.Bind(usData);
            combatant.PointerDown += OnPointerDownCombatant;
            usTeam.Add(combatant);
        }
        AdaptRect();
        Refresh();
    }

    public void Refresh()
    {
        gridMap.Refresh();
    }

    private CombatantRenderer CreatCombatant()
    {
        CombatantRenderer combatant = Instantiate(combatantPrefab, combatantRoot);
        combatant.gameObject.SetActive(true);
        return combatant;
    }

    #region 回调函数
    private void OnPointerDownCombatant(CombatantRenderer combatant)
    {
        if (SelectedCombatant == combatant)
            return;
        SelectedCombatant = combatant;
        RefreshGridEffect(combatant.transform.position);
        SetStreak(true);
    }

    private void OnPinterDownGridUnit(GridUnitRenderer gridUnit)
    {
        RefreshGridEffect(gridUnit.transform.position);
        if (SelectedCombatant != null)
        {
            SetStreak(false);
            if (SelectedCombatant.Data.CanToGridUnit(gridUnit.Data))
                Navigator(gridUnit.Data);
            SelectedCombatant = null;
        }
        else
        {
            Debug.Log(gridUnit.Data.Position);
        }
    }
    #endregion

    private void RefreshGridEffect(Vector3 position)
    {
        gridEffect.position = position;
        gridEffect.gameObject.SetActive(true);
    }

    private void SetStreak(bool active)
    {
        gridMap.SetCanMoveToStreak(SelectedCombatant.GridPosition, SelectedCombatant.Data.Protery.MOV, active);
    }

    private void RefreshComtatantPanel()
    {
        if (combatantPanel == null)
            return;
        combatantPanel.Combatant = SelectedCombatant;
    }

    private void Navigator(GridUnitData to)
    {
        List<GridUnitData> path, searched;
        Navigator<GridMapData, GridUnitData>.Instance.Navigate(battleData.MapData,
            battleData.MapData.GetGridUnitData(SelectedCombatant.GridPosition), to,
            out path, out searched);
        for(int i = 0; i < path.Count; ++i)
        {
            path[i].SetGridType(GridType.Obstacle);
        }
        gridMap.Refresh();
    }

    #region 屏幕适应
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
    #endregion
}
