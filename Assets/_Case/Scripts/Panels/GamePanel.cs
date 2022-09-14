using UnityEngine;

namespace _Case.Scripts.Panels
{
    public interface IGamePanelDelegate : IPanelDelegate
    {
            
    }
    
    public class GamePanel : Panel<IGamePanelDelegate>
    {
        
    }
}
