using ImpulseUtility;

namespace SRPG
{
    public class CombatantData
    {
        internal bool isEnemy = false;
        internal GridPosition position = new GridPosition(0, 0);
        internal CombatantProtery protery = new CombatantProtery();
        internal string name;

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
    }
}
