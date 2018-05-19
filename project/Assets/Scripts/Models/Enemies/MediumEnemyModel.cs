namespace Models.Enemies
{
    public class MediumEnemyModel : IEnemy
    {
        public EnemyType Type
        {
            get { return EnemyType.Medium; }
        }

        public int Health
        {
            get { return 10; }
        }

        public int Damage
        {
            get { return 1; }
        }

        public float Speed
        {
            get { return 1; }
        }

        public decimal Price
        {
            get { return 3; }
        }
    }
}