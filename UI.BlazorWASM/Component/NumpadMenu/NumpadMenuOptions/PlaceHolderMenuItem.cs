using System.Threading.Tasks;
using Weboku.Application.Enums;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class PlaceHolderMenuItem : NumpadMenuLabel, ISelectColorMenuItem
    {
        public string Label => string.Empty;

        public bool IsDimmed => false;

        public bool IsSelectable => false;

        public Color Color1 => Color.None;

        public Color Color2 => Color.None;

        public string Tooltip => "";

        public Task Execute() => Task.CompletedTask;
        public bool CanExecute() => true;

        public PlaceHolderMenuItem(MenuOptionSettings menuOptionSettings) : base(menuOptionSettings)
        {
        }
    }
}