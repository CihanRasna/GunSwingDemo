namespace _Case.Scripts.Panels
{
    public interface IFailedPanelDelegate : IPanelDelegate
    {
        void FailedPanel_RetryButtonTapped(FailedPanel failedPanel);
    }
    public class FailedPanel : Panel<IFailedPanelDelegate>
    {
        
    }
}