using AI;
using UnityEngine;

namespace Controllers.Scene
{
    public interface IGameController
    {
        void Pause(bool value);
        void Win(GameResult result);
        void Lose(GameResult result);

        Vector3 Coord2World(Vector2Int coord);

        void AddEnemy(EnemyLogic enemy);
        void AddShot(ShotLogic shot);

        void DebugMarkCell(Vector2Int coord);
    }
}