using Weboku.Application.Enums;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public interface ISelectColorMenuItem : INumpadMenuItem
    {
        Color Color1 { get; }
        Color Color2 { get; }
    }
}