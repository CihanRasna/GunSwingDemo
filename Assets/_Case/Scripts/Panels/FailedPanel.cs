using _Case.Scripts.Managers;

namespace _Case.Scripts.Panels
{
    public interface IFailedPanelDelegate : IPanelDelegate
    {
        void FailedPanel_RetryButtonTapped(FailedPanel failedPanel);
    }
    public class FailedPanel : Panel<IFailedPanelDelegate>
    {
        public void Retry()
        {
            LevelManager.Instance.LoadNextLevel();
        }
    }
}