using System;
using Application;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.ClickableActions
{
    public class ClickableActionFactory
    {
        private readonly CellColorProvider _cellColorProvider;
        private readonly DomainFacade _domainFacade;

        public ClickableActionFactory(CellColorProvider cellColorProvider, DomainFacade domainFacade)
        {
            _cellColorProvider = cellColorProvider;
            _domainFacade = domainFacade;
        }

        public IClickableAction MakeClickableAction(ClickableAction clickableAction)
        {
            return clickableAction switch
            {
                ClickableAction.Brush => new BrushAction(_cellColorProvider),
                ClickableAction.Eraser => new EraserAction(_domainFacade),
                ClickableAction.Marker => new MarkerAction(_domainFacade),
                ClickableAction.Pencil => new PencilAction(_domainFacade),
                _ => throw new ArgumentException("Invalid action"),
            };
        }
    }
}