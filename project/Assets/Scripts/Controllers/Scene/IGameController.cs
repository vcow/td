using AI;

namespace Controllers.Scene
{
    public interface IGameController
    {
        void Pause(bool value);
        void Win(GameResult result);
        void Lose(GameResult result);
    }
}