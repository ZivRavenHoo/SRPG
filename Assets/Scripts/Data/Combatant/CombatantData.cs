using ImpulseUtility;

public class CombatantData
{
    private bool isEnemy = false;
    private GridPosition position = new GridPosition(0,0);
    private CombatantProtery protery = new CombatantProtery();
    private string name;

    public bool IsEnemy => isEnemy;
    public GridPosition Position
    {
        get => position;
        set => position = value;
    }
    public CombatantProtery Protery => protery;
    public string Name => name;
}
