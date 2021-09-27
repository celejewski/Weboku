using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Enums;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectColorMenuItem : ISelectColorMenuItem
    {
        public Color Color1 { get; }
        public Color Color2 { get; }
        private readonly DomainFacade _domainFacade;
        private readonly NumpadMenuProvider _numpadMenuProvider;

        public SelectColorMenuItem(
            Color color1,
            Color color2,
            DomainFacade domainFacade,
            NumpadMenuProvider numpadMenuProvider)
        {
            Color1 = color1;
            Color2 = color2;
            _domainFacade = domainFacade;
            _numpadMenuProvider = numpadMenuProvider;
        }

        public string Tooltip => "change-color__tooltip";
        public bool IsDimmed => false;

        public bool IsSelectable => true;

        public Task Execute()
        {
            _numpadMenuProvider.ColorContainer.SelectItem(this);
            _domainFacade.SelectPrimaryColor(Color1);
            _domainFacade.SelectSecondaryColor(Color2);
            return Task.CompletedTask;
        }
    }
}