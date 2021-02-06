using UnityEngine;
using System.Collections.Generic;
using ImpulseUtility;
using SRPG;

public class BattleManager : MonoSingleton<BattleManager>
{
    private BattleData battleData;
    [SerializeField] private BattleRenderer battleRenderer;

    public BattleData BattleData => battleData;

    private void Start()
    {
        battleData = BattleData.GetBattleData(); //要在Start或者Awake调用，不然会报错
    }

    private void OnGUI()
    {
        if (GUILayout.Button("开始战斗！"))
        {
            battleRenderer.Bind(battleData);//注意不能在Start调用，因为BattleRenderer还没获取GridMap
        }
    }
}
