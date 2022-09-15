using TMPro;
using UnityEngine;

namespace _Case.Scripts.Panels
{
    public interface IGamePanelDelegate : IPanelDelegate
    {
            
    }
    
    public class GamePanel : Panel<IGamePanelDelegate>
    {
        [SerializeField] private TextMeshProUGUI levelText;

        public void GetLevelIdx(int idx)
        {
            levelText.text = $"Level {idx}";
        }
    }
}
