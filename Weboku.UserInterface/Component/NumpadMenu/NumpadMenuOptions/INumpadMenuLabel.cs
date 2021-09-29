namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class NumpadMenuLabel : BaseMenuOption
    {
        public string Label { get; init; }

        public NumpadMenuLabel(MenuOptionSettings menuOptionSettings) : base(menuOptionSettings)
        {
            Label = menuOptionSettings.Label;
        }
    }
}