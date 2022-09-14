﻿using UnityEngine;

namespace _Case.Scripts.Panels
{
    public interface ISucceedPanelDelegate : IPanelDelegate
    {
        void SucceedPanel_NextButtonTapped(SucceedPanel succeedPanel);
    }
    public class SucceedPanel : Panel<ISucceedPanelDelegate>
    {
        
    }
}