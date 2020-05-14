using Core.Data;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.BlazorWASM.ClickableActions
{
    public interface IClickableAction
    {
        void LeftClickAction(MouseEventArgs e, int x, int y);
        void RightClickAction(MouseEventArgs e, int x, int y);
    }
}
