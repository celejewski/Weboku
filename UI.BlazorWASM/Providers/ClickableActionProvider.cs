using Microsoft.AspNetCore.Components.Web;
using System;
using Weboku.Application;
using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.UserInterface.ClickableActions;
using Weboku.UserInterface.Enums;

namespace Weboku.UserInterface.Providers
{
    public class ClickableActionProvider
    {
        private readonly DomainFacade _domainFacade;
        private IClickableAction _clickableAction;

        public ClickableActionProvider(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public void SetValue(Value value) => _domainFacade.SelectValue(value);

        public Color Color1 { get; set; }
        public Color Color2 { get; set; }

        public void SelectClickableAction(Tool tool)
        {
            _domainFacade.SelectTool(tool);
        }


        private ClickableActionArgs CreateArgs(MouseEventArgs e, Position position)
        {
            return new()
            {
                MouseEventArgs = e,
                Position = position,
                Color1 = Color1,
                Color2 = Color2
            };
        }

        public void OnLeftClick(MouseEventArgs e, Position position)
        {
            var clickableActionArgs = CreateArgs(e, position);
            _domainFacade.UsePrimaryTool(clickableActionArgs.Position);
        }

        public void OnRightClick(MouseEventArgs e, Position position)
        {
            var clickableActionArgs = CreateArgs(e, position);
            _domainFacade.UseSecondaryTool(clickableActionArgs.Position);
        }
    }
}