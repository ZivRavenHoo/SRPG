using ImpulseUtility;

public class CombatantData
{
    private bool isEnemy = false;
    private CombatantProtery protery = new CombatantProtery();

    public bool IsEnemy => isEnemy;
    public CombatantProtery Protery => protery;
}
