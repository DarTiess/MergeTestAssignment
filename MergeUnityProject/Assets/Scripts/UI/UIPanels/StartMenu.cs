﻿using System;

namespace UI.UIPanels
{
    public class StartMenu : PanelBase
    {
        public virtual event Action ClickedPanel;
        /// <summary>
        /// if clicked panel, send event
        /// </summary>
        protected override void OnClickedPanel()
        {
           ClickedPanel?.Invoke();
        }
    }
}