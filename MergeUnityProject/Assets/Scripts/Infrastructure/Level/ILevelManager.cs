namespace Infrastructure.Level
{
    public interface ILevelManager
    {
        void PauseGame();
        void PlayGame();
        void LevelWin();
    }
}