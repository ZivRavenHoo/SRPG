using UnityEngine;
using System.Collections.Generic;
using ImpulseUtility;

public class BattleManager : MonoBehaviour
{
    private BattleData battleData;
    [SerializeField] private BattleRenderer battleRenderer;

    private void Start()
    {
        battleData = BattleDataFactory.Instance.GetBattleData(); //要在Start或者Awake调用，不然会报错
    }

    private void OnGUI()
    {
        if (GUILayout.Button("开始战斗！"))
        {
            battleRenderer.Bind(battleData);//注意不能在Start调用，因为BattleRenderer还没获取GridMap
        }
    }
}
