namespace Models.Enemies
{
    public class SmallEnemyModel : IEnemy
    {
        public EnemyType Type
        {
            get { return EnemyType.Small; }
        }

        public int Health
        {
            get { return 5; }
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
            get { return 1; }
        }
    }
}