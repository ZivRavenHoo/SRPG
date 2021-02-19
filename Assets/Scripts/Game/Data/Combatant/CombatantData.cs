using Impulse;

namespace SRPG
{
    public class CombatantData
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

        public static CombatantData CreatCombatant(GridPosition position)
        {
            CombatantData data = new CombatantData();
            data.Position = position;
            return data;
        }

        public bool CanToGridUnit(GridUnitData grid)
        {
            if (Position.Distance(grid.Position) > Protery.MOV)
                return false;
            return true;
        }
    }
}
