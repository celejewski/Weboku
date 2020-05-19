﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.Providers
{
    public class ClickableActionProvider : IClickableActionProvider
    {
        public IClickableAction ClickableAction { get; private set; }

        public event Action OnChanged;
        public void SetClickableAction(IClickableAction clickableAction)
        {
            ClickableAction = clickableAction;
            OnChanged?.Invoke();
        }

        public ClickableActionProvider(ClickableActionFactory clickableActionFactory)
        {
            ClickableAction = clickableActionFactory.StandardAction(1);
        }
    }
}
