using Impulse;

namespace SRPG
{
    public class Combatant
    {
        private bool isEnemy = false;
        private GridPosition position = new GridPosition(0, 0);
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

        public static Combatant CreatCombatant(GridPosition position)
        {
            Combatant data = new Combatant();
            data.Position = position;
            return data;
        }

        public bool CanToGridUnit(GridUnitData grid)
        {
            if (BattleManager.Instance.BattleData.MapData.GetCanMoveUnitsPosition(grid, Protery.MOV).Contains(grid))
                return true;
            return false;
        }
    }
}
