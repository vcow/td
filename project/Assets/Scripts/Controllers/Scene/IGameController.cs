using AI;
using UnityEngine;

namespace Controllers.Scene
{
    public interface IGameController
    {
        void Pause(bool value);
        void Win(GameResult result);
        void Lose(GameResult result);

        void DebugMarkCell(Vector2Int coordinate);
    }
}