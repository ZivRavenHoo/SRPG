using ImpulseUtility;

public class CombatantData
{
    private bool isEnemy = false;
    private GridPosition position = new GridPosition();
    private CombatantProtery protery = new CombatantProtery();

    public bool IsEnemy => isEnemy;
    public GridPosition Position
    {
        get { return position; }
        set
        {
            position = value;
        }
    }
    public CombatantProtery Protery => protery;
}
