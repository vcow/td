namespace Models.Enemies
{
    public class LargeEnemyModel : IEnemy
    {
        public EnemyType Type
        {
            get { return EnemyType.Large; }
        }

        public int Health
        {
            get { return 20; }
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
            get { return 5; }
        }
    }
}