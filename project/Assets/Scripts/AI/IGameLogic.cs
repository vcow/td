using Models.Enemies;

namespace AI
{
    public interface IGameLogic : ILogic
    {
        void WaveFinished(WaveLogic wave);
        void SpawnEnemy(EnemyType type);
        void EnemyDie(EnemyLogic enemy);
        void Shoot(ShotLogic shot);
        void HitTarget(int hitPoints);
    }
}