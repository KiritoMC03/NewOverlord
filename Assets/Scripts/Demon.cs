namespace NewOverlord
{
    public class Demon
    {
        internal DemonsHierarchy.Levels level;
        internal float health = 10;

        private float healthMultiplier = 100;

        public Demon(DemonsHierarchy.Levels level, float additionalHealth)
        {
            this.level = level;

            health = ((float)level + 1) * healthMultiplier + additionalHealth;
        }
    }
}